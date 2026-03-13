using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1260;
        public void FormatTo(T::StringBuilder builder__1262)
        {
            builder__1262.Append("'");
            string t___9646 = this.value__1260.ToString("yyyy-MM-dd");
            void fn__9644(int c__1264)
            {
                if (c__1264 == 39) builder__1262.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1262, c__1264);
            }
            C::StringUtil.ForEach(t___9646, (S::Action<int>) fn__9644);
            builder__1262.Append("'");
        }
        public SqlDate(S::DateTime value__1266)
        {
            this.value__1260 = value__1266;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1260;
            }
        }
    }
}
