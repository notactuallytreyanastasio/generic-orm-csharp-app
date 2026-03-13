using T = System.Text;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlFloat64: ISqlPart
    {
        readonly double value__781;
        public void FormatTo(T::StringBuilder builder__783)
        {
            bool t___3021;
            bool t___3022;
            string s__785 = C::Float64.Format(this.value__781);
            if (s__785 == "NaN")
            {
                t___3022 = true;
            }
            else
            {
                if (s__785 == "Infinity")
                {
                    t___3021 = true;
                }
                else
                {
                    t___3021 = s__785 == "-Infinity";
                }
                t___3022 = t___3021;
            }
            if (t___3022) builder__783.Append("NULL");
            else builder__783.Append(s__785);
        }
        public SqlFloat64(double value__787)
        {
            this.value__781 = value__787;
        }
        public double Value
        {
            get
            {
                return this.value__781;
            }
        }
    }
}
