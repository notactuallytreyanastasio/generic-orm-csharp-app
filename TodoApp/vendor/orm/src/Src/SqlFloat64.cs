using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1045;
        public void FormatTo(T::StringBuilder builder__1047)
        {
            bool t___4207;
            bool t___4208;
            string s__1049 = C::Float64.Format(this.value__1045);
            if (s__1049 == "NaN")
            {
                t___4208 = true;
            }
            else
            {
                if (s__1049 == "Infinity")
                {
                    t___4207 = true;
                }
                else
                {
                    t___4207 = s__1049 == "-Infinity";
                }
                t___4208 = t___4207;
            }
            if (t___4208) builder__1047.Append("NULL");
            else builder__1047.Append(s__1049);
        }
        public SqlFloat64(double value__1051)
        {
            this.value__1045 = value__1051;
        }
        public double Value
        {
            get
            {
                return this.value__1045;
            }
        }
    }
}
