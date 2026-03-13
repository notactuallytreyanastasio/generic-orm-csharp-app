using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1286;
        public void FormatTo(T::StringBuilder builder__1288)
        {
            builder__1288.Append("'");
            void fn__9658(int c__1290)
            {
                if (c__1290 == 39) builder__1288.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1288, c__1290);
            }
            C::StringUtil.ForEach(this.value__1286, (S::Action<int>) fn__9658);
            builder__1288.Append("'");
        }
        public SqlString(string value__1292)
        {
            this.value__1286 = value__1292;
        }
        public string Value
        {
            get
            {
                return this.value__1286;
            }
        }
    }
}
