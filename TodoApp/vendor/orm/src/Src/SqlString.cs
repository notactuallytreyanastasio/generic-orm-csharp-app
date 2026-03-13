using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1064;
        public void FormatTo(T::StringBuilder builder__1066)
        {
            builder__1066.Append("'");
            void fn__7207(int c__1068)
            {
                if (c__1068 == 39) builder__1066.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1066, c__1068);
            }
            C::StringUtil.ForEach(this.value__1064, (S::Action<int>) fn__7207);
            builder__1066.Append("'");
        }
        public SqlString(string value__1070)
        {
            this.value__1064 = value__1070;
        }
        public string Value
        {
            get
            {
                return this.value__1064;
            }
        }
    }
}
