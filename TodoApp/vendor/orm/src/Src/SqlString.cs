using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1202;
        public void FormatTo(T::StringBuilder builder__1204)
        {
            builder__1204.Append("'");
            void fn__8784(int c__1206)
            {
                if (c__1206 == 39) builder__1204.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1204, c__1206);
            }
            C::StringUtil.ForEach(this.value__1202, (S::Action<int>) fn__8784);
            builder__1204.Append("'");
        }
        public SqlString(string value__1208)
        {
            this.value__1202 = value__1208;
        }
        public string Value
        {
            get
            {
                return this.value__1202;
            }
        }
    }
}
