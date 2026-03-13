using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1949;
        public void FormatTo(T::StringBuilder builder__1951)
        {
            string t___15532 = S::Convert.ToString(this.value__1949);
            builder__1951.Append(t___15532);
        }
        public SqlInt64(long value__1954)
        {
            this.value__1949 = value__1954;
        }
        public long Value
        {
            get
            {
                return this.value__1949;
            }
        }
    }
}
