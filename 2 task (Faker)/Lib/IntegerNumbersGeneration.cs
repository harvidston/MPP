using System;
using System.Collections.Generic;

namespace Lib
{
    public class IntegerNumbersGeneration : ITypeGeneration
    {
        private static readonly List<Type> ValueTypeList = new List<Type>
        {
            typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), 
            typeof(int), typeof(uint), typeof(long), typeof(ulong)
        };

        private readonly Random _random;

        public IntegerNumbersGeneration()
        {
            _random = new Random();
        }
        
        public object Generate()
        {
            return _random.Next();
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