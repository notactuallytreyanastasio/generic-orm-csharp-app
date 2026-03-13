using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__2105;
        public void FormatTo(T::StringBuilder builder__2107)
        {
            builder__2107.Append("'");
            void fn__17356(int c__2109)
            {
                if (c__2109 == 39) builder__2107.Append("''");
                else C::StringUtil.AppendCodePoint(builder__2107, c__2109);
            }
            C::StringUtil.ForEach(this.value__2105, (S::Action<int>) fn__17356);
            builder__2107.Append("'");
        }
        public SqlString(string value__2111)
        {
            this.value__2105 = value__2111;
        }
        public string Value
        {
            get
            {
                return this.value__2105;
            }
        }
    }
}
