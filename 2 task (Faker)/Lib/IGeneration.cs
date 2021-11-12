namespace Lib
{
    public interface IGeneration
    {
        object Generate();
        bool IsDefaultValue(object value);
    }
}