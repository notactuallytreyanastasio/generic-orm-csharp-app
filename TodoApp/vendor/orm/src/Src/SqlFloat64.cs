using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__889;
        public void FormatTo(T::StringBuilder builder__891)
        {
            bool t___3426;
            bool t___3427;
            string s__893 = C::Float64.Format(this.value__889);
            if (s__893 == "NaN")
            {
                t___3427 = true;
            }
            else
            {
                if (s__893 == "Infinity")
                {
                    t___3426 = true;
                }
                else
                {
                    t___3426 = s__893 == "-Infinity";
                }
                t___3427 = t___3426;
            }
            if (t___3427) builder__891.Append("NULL");
            else builder__891.Append(s__893);
        }
        public SqlFloat64(double value__895)
        {
            this.value__889 = value__895;
        }
        public double Value
        {
            get
            {
                return this.value__889;
            }
        }
    }
}
