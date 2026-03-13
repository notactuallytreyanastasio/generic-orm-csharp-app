using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__800;
        public void FormatTo(T::StringBuilder builder__802)
        {
            builder__802.Append("'");
            void fn__5089(int c__804)
            {
                if (c__804 == 39) builder__802.Append("''");
                else C::StringUtil.AppendCodePoint(builder__802, c__804);
            }
            C::StringUtil.ForEach(this.value__800, (S::Action<int>) fn__5089);
            builder__802.Append("'");
        }
        public SqlString(string value__806)
        {
            this.value__800 = value__806;
        }
        public string Value
        {
            get
            {
                return this.value__800;
            }
        }
    }
}
