using System;
using System.Collections.Generic;

namespace Lib
{
    public class DateGeneration : ITypeGeneration
    {
        public object Generate()
        {
            return DateTime.Now;
        }

        public bool IsDefaultValue(object value)
        {
            return true;
        }

        public List<Type> ValueTypes()
        {
            return new List<Type> {typeof(DateTime)};
        }
    }
}