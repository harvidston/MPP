using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Lib
{
    public class FakerConfig
    {
        private readonly Dictionary<PropertyInfo, IGeneration> _generation = 
            new Dictionary<PropertyInfo, IGeneration>();

        public void Add<TSource, TPropType, TGenerator>(Expression<Func<TSource, TPropType>> expression) 
            where TGenerator : IGeneration
        {
            PropertyInfo propertyInfo = ((MemberExpression) expression.Body).Member as PropertyInfo;
            if(propertyInfo == null)
                throw new Exception("Cannot add config item.");
            _generation.Add(propertyInfo, Activator.CreateInstance<TGenerator>());
        }

        public IGeneration GetGenerator(PropertyInfo propertyInfo)
        {
            return _generation.GetValueOrDefault(propertyInfo, null);
        }
    }
}