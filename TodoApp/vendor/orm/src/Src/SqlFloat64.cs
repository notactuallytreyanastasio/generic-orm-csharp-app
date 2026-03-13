using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1841;
        public void FormatTo(T::StringBuilder builder__1843)
        {
            bool t___8146;
            bool t___8147;
            string s__1845 = C::Float64.Format(this.value__1841);
            if (s__1845 == "NaN")
            {
                t___8147 = true;
            }
            else
            {
                if (s__1845 == "Infinity")
                {
                    t___8146 = true;
                }
                else
                {
                    t___8146 = s__1845 == "-Infinity";
                }
                t___8147 = t___8146;
            }
            if (t___8147) builder__1843.Append("NULL");
            else builder__1843.Append(s__1845);
        }
        public SqlFloat64(double value__1847)
        {
            this.value__1841 = value__1847;
        }
        public double Value
        {
            get
            {
                return this.value__1841;
            }
        }
    }
}
