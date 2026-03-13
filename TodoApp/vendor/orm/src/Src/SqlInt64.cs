using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1510;
        public void FormatTo(T::StringBuilder builder__1512)
        {
            string t___11433 = S::Convert.ToString(this.value__1510);
            builder__1512.Append(t___11433);
        }
        public SqlInt64(long value__1515)
        {
            this.value__1510 = value__1515;
        }
        public long Value
        {
            get
            {
                return this.value__1510;
            }
        }
    }
}
