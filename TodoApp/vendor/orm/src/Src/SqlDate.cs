using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1834;
        public void FormatTo(T::StringBuilder builder__1836)
        {
            builder__1836.Append("'");
            string t___14256 = this.value__1834.ToString("yyyy-MM-dd");
            void fn__14254(int c__1838)
            {
                if (c__1838 == 39) builder__1836.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1836, c__1838);
            }
            C::StringUtil.ForEach(t___14256, (S::Action<int>) fn__14254);
            builder__1836.Append("'");
        }
        public SqlDate(S::DateTime value__1840)
        {
            this.value__1834 = value__1840;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1834;
            }
        }
    }
}
