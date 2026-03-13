using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1413;
        public void FormatTo(T::StringBuilder builder__1415)
        {
            string t___10907 = S::Convert.ToString(this.value__1413);
            builder__1415.Append(t___10907);
        }
        public SqlInt64(long value__1418)
        {
            this.value__1413 = value__1418;
        }
        public long Value
        {
            get
            {
                return this.value__1413;
            }
        }
    }
}
