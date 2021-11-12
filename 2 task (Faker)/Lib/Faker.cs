using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lib
{
    public class Faker
    {
        private readonly Stack<Type> _typesDependenciesParameters;
        private readonly Stack<object> _typesDependenciesClass;
        private readonly ValueGenerator _generator;
        private readonly FakerConfig _config;

        public Faker()
        {
            _typesDependenciesParameters = new Stack<Type>();
            _typesDependenciesClass = new Stack<object>();
            _generator = new ValueGenerator(this);
        }

        public Faker(FakerConfig config) : this()
        {
            _config = config;
        }

        public T Create<T>()
        {
            T instance = (T) Checking(typeof(T));

            if (_typesDependenciesParameters.Count > 0 || _typesDependenciesClass.Count > 0)
                throw new Exception("Stacks are not clear.");

            return instance;
        }

        internal object Checking(Type type)
        {
            ValidateForType(type);

            object value = _generator.Generate(type);
            if (value != null)
                return value;

            CheckCyclingDependency(type);

            foreach (var obj in _typesDependenciesClass)
                if (obj.GetType() == type)
                    return obj;

            object any = Construct(type);
            _typesDependenciesClass.Push(any);
            InjectFields(any);
            InjectProperties(any);
            _typesDependenciesClass.Pop();
            return any;
        }

        private void ValidateForType(Type type)
        {
            if (type.IsAbstract || type.IsInterface)
                throw new Exception("Cannot instantiate this type: " + type.Name);
        }

        private void CheckCyclingDependency(Type type)
        {
            Type prevObjType = null;
            foreach (var objType in _typesDependenciesParameters)
            {
                if (objType == type)
                    throw new Exception("Cycling depending: " +
                                        (prevObjType == null ? objType.Name : prevObjType.Name) + " <-> " + type.Name);
                prevObjType = objType;
            }
        }

        private object Construct(Type type)
        {
            List<ConstructorInfo> bestConstructors = type.GetConstructors().ToList();
            bestConstructors.Sort(
                (a, b) => b.GetParameters().Length.CompareTo(a.GetParameters().Length)
            );

            if (bestConstructors.Count == 0)
                return Activator.CreateInstance(type);

            foreach (var constructor in bestConstructors)
            {
                try
                {
                    return constructor.Invoke(
                        GetParameters(constructor)
                    );
                }
                catch (Exception)
                {
                }
            }

            throw new Exception("Cannot construct instance of type: " + type.Name);
        }

        private void InjectFields(object any)
        {
            FieldInfo[] fields = any.GetType().GetFields().Where(field => field.IsPublic && !field.IsInitOnly)
                .ToArray();

            foreach (var field in fields)
            {
                if (ValueNotInitialized(field.GetValue(any)))
                    field.SetValue(any, Checking(field.FieldType));
            }
        }

        private void InjectProperties(object any)
        {
            PropertyInfo[] properties = any.GetType().GetProperties().Where(property => property.CanWrite).ToArray();
            foreach (var property in properties)
            {
                IGeneration strategy = _config?.GetGenerator(property);
                if (strategy == null)
                {
                    if (ValueNotInitialized(property.GetValue(any)))
                        property.SetValue(any, Checking(property.PropertyType));
                }
                else
                {
                    if (strategy.IsDefaultValue(property.GetValue(any)))
                        property.SetValue(any, strategy.Generate());
                }
            }
        }

        private bool ValueNotInitialized(object value)
        {
            return _generator.IsDefaultValue(value);
        }

        private object[] GetParameters(ConstructorInfo constructor)
        {
            _typesDependenciesParameters.Push(constructor.DeclaringType);
            object[] parameters = new object[constructor.GetParameters().Length];
            int i = 0;
            foreach (var parameter in constructor.GetParameters())
                parameters[i++] = Checking(parameter.ParameterType);
            _typesDependenciesParameters.Pop();
            return parameters;
        }
    }
}