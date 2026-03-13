using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1280;
        public void FormatTo(T::StringBuilder builder__1282)
        {
            string t___9653 = S::Convert.ToString(this.value__1280);
            builder__1282.Append(t___9653);
        }
        public SqlInt64(long value__1285)
        {
            this.value__1280 = value__1285;
        }
        public long Value
        {
            get
            {
                return this.value__1280;
            }
        }
    }
}
