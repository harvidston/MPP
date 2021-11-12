using System;
using System.Collections.Generic;

namespace Lib
{
    public class CharGeneration : ITypeGeneration
    {

        private readonly Random _random;

        public CharGeneration()
        {
            _random = new Random();
        }
        
        public object Generate()
        {
            return (char) (_random.Next() % 60 + 65);
        }

        public bool IsDefaultValue(object value)
        {
            return (char) value == '\0';
        }

        public List<Type> ValueTypes()
        {
            return new List<Type> { typeof(char) };
        }
    }
}