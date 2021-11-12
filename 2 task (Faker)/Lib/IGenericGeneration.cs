using System;

namespace Lib
{
    public interface IGenericGeneration : IGeneration
    {
        object Generate(Type type, Func<Type, object> factory);
        bool CanGenerate(Type type);
    }
}