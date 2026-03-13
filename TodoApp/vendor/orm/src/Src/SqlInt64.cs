using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__2095;
        public void FormatTo(T::StringBuilder builder__2097)
        {
            string t___17351 = S::Convert.ToString(this.value__2095);
            builder__2097.Append(t___17351);
        }
        public SqlInt64(long value__2100)
        {
            this.value__2095 = value__2100;
        }
        public long Value
        {
            get
            {
                return this.value__2095;
            }
        }
    }
}
