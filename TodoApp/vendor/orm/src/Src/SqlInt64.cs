using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1058;
        public void FormatTo(T::StringBuilder builder__1060)
        {
            string t___7202 = S::Convert.ToString(this.value__1058);
            builder__1060.Append(t___7202);
        }
        public SqlInt64(long value__1063)
        {
            this.value__1058 = value__1063;
        }
        public long Value
        {
            get
            {
                return this.value__1058;
            }
        }
    }
}
