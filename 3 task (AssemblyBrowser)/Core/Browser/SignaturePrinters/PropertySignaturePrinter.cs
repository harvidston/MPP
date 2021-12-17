using System.Reflection;
using System.Text;

namespace Core.Browser.SignaturePrinters
{
    public class PropertySignaturePrinter : ISignaturePrinter
    {
        public bool CanPrint(MemberInfo info)
        {
            return info is PropertyInfo;
        }

        public string Print(MemberInfo info)
        {
            bool g = false;
            bool s = false;
            string gv = "";
            string sv = "";
            string t = "";
            var inf = info as PropertyInfo;
            var accessors = inf.GetAccessors(true);
            foreach (var m in accessors)
            {
                if (m.ReturnType == typeof(void))
                {
                    s = true;
                    sv = m.IsPublic ? "" : "private ";
                    t = m.GetParameters()[0].ParameterType.Name;
                }
                else
                {
                    g = true;
                    gv = m.IsPublic ? "public" : "private";
                    t = m.ReturnType.Name;
                }
            }

            return $"{(g ? gv : sv)} {t} {info.Name} {{ {(g ? "get;" : "")} {sv}{(s ? "set; " : "")}}}";
        }
    }
}