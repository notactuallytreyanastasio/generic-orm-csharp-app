using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1419;
        public void FormatTo(T::StringBuilder builder__1421)
        {
            builder__1421.Append("'");
            void fn__10912(int c__1423)
            {
                if (c__1423 == 39) builder__1421.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1421, c__1423);
            }
            C::StringUtil.ForEach(this.value__1419, (S::Action<int>) fn__10912);
            builder__1421.Append("'");
        }
        public SqlString(string value__1425)
        {
            this.value__1419 = value__1425;
        }
        public string Value
        {
            get
            {
                return this.value__1419;
            }
        }
    }
}
