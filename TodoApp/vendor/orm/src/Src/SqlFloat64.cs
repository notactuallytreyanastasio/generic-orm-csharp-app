using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__1936;
        public void FormatTo(T::StringBuilder builder__1938)
        {
            bool t___8839;
            bool t___8840;
            string s__1940 = C::Float64.Format(this.value__1936);
            if (s__1940 == "NaN")
            {
                t___8840 = true;
            }
            else
            {
                if (s__1940 == "Infinity")
                {
                    t___8839 = true;
                }
                else
                {
                    t___8839 = s__1940 == "-Infinity";
                }
                t___8840 = t___8839;
            }
            if (t___8840) builder__1938.Append("NULL");
            else builder__1938.Append(s__1940);
        }
        public SqlFloat64(double value__1942)
        {
            this.value__1936 = value__1942;
        }
        public double Value
        {
            get
            {
                return this.value__1936;
            }
        }
    }
}
