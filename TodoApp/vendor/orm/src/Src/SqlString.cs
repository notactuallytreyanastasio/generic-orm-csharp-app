using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1959;
        public void FormatTo(T::StringBuilder builder__1961)
        {
            builder__1961.Append("'");
            void fn__15537(int c__1963)
            {
                if (c__1963 == 39) builder__1961.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1961, c__1963);
            }
            C::StringUtil.ForEach(this.value__1959, (S::Action<int>) fn__15537);
            builder__1961.Append("'");
        }
        public SqlString(string value__1965)
        {
            this.value__1959 = value__1965;
        }
        public string Value
        {
            get
            {
                return this.value__1959;
            }
        }
    }
}
