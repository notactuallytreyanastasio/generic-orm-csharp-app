using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1860;
        public void FormatTo(T::StringBuilder builder__1862)
        {
            builder__1862.Append("'");
            void fn__14268(int c__1864)
            {
                if (c__1864 == 39) builder__1862.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1862, c__1864);
            }
            C::StringUtil.ForEach(this.value__1860, (S::Action<int>) fn__14268);
            builder__1862.Append("'");
        }
        public SqlString(string value__1866)
        {
            this.value__1860 = value__1866;
        }
        public string Value
        {
            get
            {
                return this.value__1860;
            }
        }
    }
}
