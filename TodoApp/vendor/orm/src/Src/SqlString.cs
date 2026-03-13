using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__1516;
        public void FormatTo(T::StringBuilder builder__1518)
        {
            builder__1518.Append("'");
            void fn__11438(int c__1520)
            {
                if (c__1520 == 39) builder__1518.Append("''");
                else C::StringUtil.AppendCodePoint(builder__1518, c__1520);
            }
            C::StringUtil.ForEach(this.value__1516, (S::Action<int>) fn__11438);
            builder__1518.Append("'");
        }
        public SqlString(string value__1522)
        {
            this.value__1516 = value__1522;
        }
        public string Value
        {
            get
            {
                return this.value__1516;
            }
        }
    }
}
