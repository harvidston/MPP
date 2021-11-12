using System;
using System.Collections.Generic;

namespace Lib
{
    public interface ITypeGeneration : IGeneration
    {
        List<Type> ValueTypes();
    }
}