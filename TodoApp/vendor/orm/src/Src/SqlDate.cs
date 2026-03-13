using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlDate: ISqlPart
    {
        readonly S::DateTime value__1038;
        public void FormatTo(T::StringBuilder builder__1040)
        {
            builder__1040.Append("'");
            string t___7195 = this.value__1038.ToString("yyyy-MM-dd");
            void fn__7193(int c__1042)
            {
                if (c__1042 == 39) builder__1040.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1040, c__1042);
            }
            C::StringUtil.ForEach(t___7195, (S::Action<int>) fn__7193);
            builder__1040.Append("'");
        }
        public SqlDate(S::DateTime value__1044)
        {
            this.value__1038 = value__1044;
        }
        public S::DateTime Value
        {
            get
            {
                return this.value__1038;
            }
        }
    }
}
