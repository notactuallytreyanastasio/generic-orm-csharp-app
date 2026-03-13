using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1183;
        public void FormatTo(T::StringBuilder builder__1185)
        {
            bool t___5050;
            bool t___5051;
            string s__1187 = C::Float64.Format(this.value__1183);
            if (s__1187 == "NaN")
            {
                t___5051 = true;
            }
            else
            {
                if (s__1187 == "Infinity")
                {
                    t___5050 = true;
                }
                else
                {
                    t___5050 = s__1187 == "-Infinity";
                }
                t___5051 = t___5050;
            }
            if (t___5051) builder__1185.Append("NULL");
            else builder__1185.Append(s__1187);
        }
        public SqlFloat64(double value__1189)
        {
            this.value__1183 = value__1189;
        }
        public double Value
        {
            get
            {
                return this.value__1183;
            }
        }
    }
}
