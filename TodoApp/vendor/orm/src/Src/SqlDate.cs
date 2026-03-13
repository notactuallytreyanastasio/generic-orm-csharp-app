using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__882;
        public void FormatTo(T::StringBuilder builder__884)
        {
            builder__884.Append("'");
            string t___5763 = this.value__882.ToString("yyyy-MM-dd");
            void fn__5761(int c__886)
            {
                if (c__886 == 39) builder__884.Append("''");
                else C::StringUtil.AppendCodePoint(builder__884, c__886);
            }
            C::StringUtil.ForEach(t___5763, (S::Action<int>) fn__5761);
            builder__884.Append("'");
        }
        public SqlDate(S::DateTime value__888)
        {
            this.value__882 = value__888;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__882;
            }
        }
    }
}
