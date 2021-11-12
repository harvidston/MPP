using System;
using System.Collections.Generic;

namespace Lib
{
    public class FloatNumbersGeneration : ITypeGeneration
    {
        private static readonly List<Type> ValueTypeList = new List<Type>
        {
            typeof(float), typeof(double)
        };

        private readonly Random _random;

        public FloatNumbersGeneration()
        {
            _random = new Random();
        }
        
        public object Generate()
        {
            return _random.NextDouble();
        }

        public bool IsDefaultValue(object value)
        {
            return "0".Equals(value?.ToString());
        }

        public List<Type> ValueTypes()
        {
            return ValueTypeList;
        }
    }
}