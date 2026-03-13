using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__2082;
        public void FormatTo(T::StringBuilder builder__2084)
        {
            bool t___9781;
            bool t___9782;
            string s__2086 = C::Float64.Format(this.value__2082);
            if (s__2086 == "NaN")
            {
                t___9782 = true;
            }
            else
            {
                if (s__2086 == "Infinity")
                {
                    t___9781 = true;
                }
                else
                {
                    t___9781 = s__2086 == "-Infinity";
                }
                t___9782 = t___9781;
            }
            if (t___9782) builder__2084.Append("NULL");
            else builder__2084.Append(s__2086);
        }
        public SqlFloat64(double value__2088)
        {
            this.value__2082 = value__2088;
        }
        public double Value
        {
            get
            {
                return this.value__2082;
            }
        }
    }
}
