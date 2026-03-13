using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1393;
        public void FormatTo(T::StringBuilder builder__1395)
        {
            builder__1395.Append("'");
            string t___10900 = this.value__1393.ToString("yyyy-MM-dd");
            void fn__10898(int c__1397)
            {
                if (c__1397 == 39) builder__1395.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1395, c__1397);
            }
            C::StringUtil.ForEach(t___10900, (S::Action<int>) fn__10898);
            builder__1395.Append("'");
        }
        public SqlDate(S::DateTime value__1399)
        {
            this.value__1393 = value__1399;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1393;
            }
        }
    }
}
