using System;
using System.Collections.Generic;

namespace Lib
{
    public class BooleanGeneration : ITypeGeneration
    {

        private readonly Random _random;

        public BooleanGeneration()
        {
            _random = new Random();
        }
        
        public object Generate()
        {
            return (_random.Next() & 1) == 1;
        }

        public bool IsDefaultValue(object value)
        {
            return true;
        }

        public List<Type> ValueTypes()
        {
            return new List<Type> { typeof(bool) };
        }
    }
}