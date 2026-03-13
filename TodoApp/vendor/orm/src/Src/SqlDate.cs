using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1490;
        public void FormatTo(T::StringBuilder builder__1492)
        {
            builder__1492.Append("'");
            string t___11426 = this.value__1490.ToString("yyyy-MM-dd");
            void fn__11424(int c__1494)
            {
                if (c__1494 == 39) builder__1492.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1492, c__1494);
            }
            C::StringUtil.ForEach(t___11426, (S::Action<int>) fn__11424);
            builder__1492.Append("'");
        }
        public SqlDate(S::DateTime value__1496)
        {
            this.value__1490 = value__1496;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1490;
            }
        }
    }
}
