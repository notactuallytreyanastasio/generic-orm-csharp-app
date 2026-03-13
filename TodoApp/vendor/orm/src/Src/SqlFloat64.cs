using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1497;
        public void FormatTo(T::StringBuilder builder__1499)
        {
            bool t___6581;
            bool t___6582;
            string s__1501 = C::Float64.Format(this.value__1497);
            if (s__1501 == "NaN")
            {
                t___6582 = true;
            }
            else
            {
                if (s__1501 == "Infinity")
                {
                    t___6581 = true;
                }
                else
                {
                    t___6581 = s__1501 == "-Infinity";
                }
                t___6582 = t___6581;
            }
            if (t___6582) builder__1499.Append("NULL");
            else builder__1499.Append(s__1501);
        }
        public SqlFloat64(double value__1503)
        {
            this.value__1497 = value__1503;
        }
        public double Value
        {
            get
            {
                return this.value__1497;
            }
        }
    }
}
