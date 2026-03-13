using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1267;
        public void FormatTo(T::StringBuilder builder__1269)
        {
            bool t___5524;
            bool t___5525;
            string s__1271 = C::Float64.Format(this.value__1267);
            if (s__1271 == "NaN")
            {
                t___5525 = true;
            }
            else
            {
                if (s__1271 == "Infinity")
                {
                    t___5524 = true;
                }
                else
                {
                    t___5524 = s__1271 == "-Infinity";
                }
                t___5525 = t___5524;
            }
            if (t___5525) builder__1269.Append("NULL");
            else builder__1269.Append(s__1271);
        }
        public SqlFloat64(double value__1273)
        {
            this.value__1267 = value__1273;
        }
        public double Value
        {
            get
            {
                return this.value__1267;
            }
        }
    }
}
