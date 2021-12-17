using System.Reflection;

namespace Core.Browser.SignaturePrinters
{
    public interface ISignaturePrinter
    {
        bool CanPrint(MemberInfo info);

        string Print(MemberInfo info);
    }
}