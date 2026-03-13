using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__774;
        public void FormatTo(T::StringBuilder builder__776)
        {
            builder__776.Append("'");
            string t___5077 = this.value__774.ToString("yyyy-MM-dd");
            void fn__5075(int c__778)
            {
                if (c__778 == 39) builder__776.Append("''");
                else C::StringUtil.AppendCodePoint(builder__776, c__778);
            }
            C::StringUtil.ForEach(t___5077, (S::Action<int>) fn__5075);
            builder__776.Append("'");
        }
        public SqlDate(S::DateTime value__780)
        {
            this.value__774 = value__780;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__774;
            }
        }
    }
}
