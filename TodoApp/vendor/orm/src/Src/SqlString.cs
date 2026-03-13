using S = System;
using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlString: ISqlPart
    {
        readonly string value__908;
        public void FormatTo(T::StringBuilder builder__910)
        {
            builder__910.Append("'");
            void fn__5775(int c__912)
            {
                if (c__912 == 39) builder__910.Append("''");
                else C::StringUtil.AppendCodePoint(builder__910, c__912);
            }
            C::StringUtil.ForEach(this.value__908, (S::Action<int>) fn__5775);
            builder__910.Append("'");
        }
        public SqlString(string value__914)
        {
            this.value__908 = value__914;
        }
        public string Value
        {
            get
            {
                return this.value__908;
            }
        }
    }
}
