using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib
{
    internal class ValueGenerator
    {
        private static readonly Dictionary<Type, ITypeGeneration> TypeGeneration;
        private static readonly List<IGenericGeneration> ConditionGeneration;

        static ValueGenerator()
        {
            TypeGeneration = new Dictionary<Type, ITypeGeneration>();
            typeof(ValueGenerator).Assembly
                .GetTypes()
                .Where(type => type.GetInterfaces().Contains(typeof(ITypeGeneration)))
                .ToList()
                .ForEach(generationType =>
                    {
                        var generation = 
                            (ITypeGeneration) Activator.CreateInstance(generationType);
                        generation?.ValueTypes()
                            .ForEach(valueType => TypeGeneration.Add(valueType, generation));
                    }
                );
            ConditionGeneration = typeof(ValueGenerator).Assembly
                .GetTypes()
                .Where(type => 
                    type.GetInterfaces().Contains(typeof(IGenericGeneration)))
                .Select(generationType => 
                    (IGenericGeneration) Activator.CreateInstance(generationType))
                .ToList();
        }

        private readonly Faker _faker;

        public ValueGenerator(Faker faker)
        {
            _faker = faker;
        }

        public object Generate(Type type)
        {
            if (type.GetGenericArguments().Length > 0)
                return ConditionGeneration
                    .Find(generation => generation.CanGenerate(type))
                    ?.Generate(type, incomingType => _faker.Checking(incomingType));

            return TypeGeneration
                .GetValueOrDefault(type, null)
                ?.Generate();
        }

        public bool IsDefaultValue(object value)
        {
            if (value == null)
                return true;
            
            Type type = value.GetType();
            IGeneration required;
            
            if (type.GetGenericArguments().Length > 0)
            {
                required = ConditionGeneration
                    .Find(strategy => strategy.CanGenerate(type));
            }
            else
            {
                required = TypeGeneration
                    .GetValueOrDefault(type, null);
            }

            if (required == null)
                return true;

            return required.IsDefaultValue(value);
        }
    }
}