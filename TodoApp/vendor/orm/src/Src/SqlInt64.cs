using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1854;
        public void FormatTo(T::StringBuilder builder__1856)
        {
            string t___14263 = S::Convert.ToString(this.value__1854);
            builder__1856.Append(t___14263);
        }
        public SqlInt64(long value__1859)
        {
            this.value__1854 = value__1859;
        }
        public long Value
        {
            get
            {
                return this.value__1854;
            }
        }
    }
}
