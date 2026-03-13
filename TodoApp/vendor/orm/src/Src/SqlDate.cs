using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1176;
        public void FormatTo(T::StringBuilder builder__1178)
        {
            builder__1178.Append("'");
            string t___8772 = this.value__1176.ToString("yyyy-MM-dd");
            void fn__8770(int c__1180)
            {
                if (c__1180 == 39) builder__1178.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1178, c__1180);
            }
            C::StringUtil.ForEach(t___8772, (S::Action<int>) fn__8770);
            builder__1178.Append("'");
        }
        public SqlDate(S::DateTime value__1182)
        {
            this.value__1176 = value__1182;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1176;
            }
        }
    }
}
