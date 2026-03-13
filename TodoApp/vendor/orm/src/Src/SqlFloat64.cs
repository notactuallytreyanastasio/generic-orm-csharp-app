using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1400;
        public void FormatTo(T::StringBuilder builder__1402)
        {
            bool t___6270;
            bool t___6271;
            string s__1404 = C::Float64.Format(this.value__1400);
            if (s__1404 == "NaN")
            {
                t___6271 = true;
            }
            else
            {
                if (s__1404 == "Infinity")
                {
                    t___6270 = true;
                }
                else
                {
                    t___6270 = s__1404 == "-Infinity";
                }
                t___6271 = t___6270;
            }
            if (t___6271) builder__1402.Append("NULL");
            else builder__1402.Append(s__1404);
        }
        public SqlFloat64(double value__1406)
        {
            this.value__1400 = value__1406;
        }
        public double Value
        {
            get
            {
                return this.value__1400;
            }
        }
    }
}
