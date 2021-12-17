using System.Reflection;
using System.Text;

namespace Core.Browser.SignaturePrinters
{
    public class FieldSignaturePrinter : ISignaturePrinter
    {
        public bool CanPrint(MemberInfo info)
        {
            return info is FieldInfo;
        }

        public string Print(MemberInfo info)
        {
            var i = info as FieldInfo;
            var builder = new StringBuilder();
            builder.Append(i.IsPublic ? "public " : "private ");
            builder.Append(i.FieldType.Name);
            builder.Append(' ');
            builder.Append(i.Name);
            return builder.ToString();
        }
    }
}