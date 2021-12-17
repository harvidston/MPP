using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Browser.SignaturePrinters
{
    public class MethodSignaturePrinter : ISignaturePrinter
    {
        public bool CanPrint(MemberInfo info)
        {
            return info is MethodInfo;
        }

        public string Print(MemberInfo info)
        {
            var i = info as MethodInfo;
            var builder = new StringBuilder();
            builder.Append(i.IsPublic ? "public " : "private ");
            builder.Append(i.ReturnType.Name);
            builder.Append($" {i.Name}");
            var pars = i.GetParameters()
                .Select(p => p.ParameterType.Name);
            builder.Append($"({string.Join(", ", pars)})");
            return builder.ToString();
        }
    }
}