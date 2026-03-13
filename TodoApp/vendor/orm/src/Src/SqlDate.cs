using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__2075;
        public void FormatTo(T::StringBuilder builder__2077)
        {
            builder__2077.Append("'");
            string t___17344 = this.value__2075.ToString("yyyy-MM-dd");
            void fn__17342(int c__2079)
            {
                if (c__2079 == 39) builder__2077.Append("''");
                else C::StringUtil.AppendCodePoint(builder__2077, c__2079);
            }
            C::StringUtil.ForEach(t___17344, (S::Action<int>) fn__17342);
            builder__2077.Append("'");
        }
        public SqlDate(S::DateTime value__2081)
        {
            this.value__2075 = value__2081;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__2075;
            }
        }
    }
}
