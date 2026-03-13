using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1929;
        public void FormatTo(T::StringBuilder builder__1931)
        {
            builder__1931.Append("'");
            string t___15525 = this.value__1929.ToString("yyyy-MM-dd");
            void fn__15523(int c__1933)
            {
                if (c__1933 == 39) builder__1931.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1931, c__1933);
            }
            C::StringUtil.ForEach(t___15525, (S::Action<int>) fn__15523);
            builder__1931.Append("'");
        }
        public SqlDate(S::DateTime value__1935)
        {
            this.value__1929 = value__1935;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1929;
            }
        }
    }
}
