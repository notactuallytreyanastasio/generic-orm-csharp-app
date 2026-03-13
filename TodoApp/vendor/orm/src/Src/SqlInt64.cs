using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__902;
        public void FormatTo(T::StringBuilder builder__904)
        {
            string t___5770 = S::Convert.ToString(this.value__902);
            builder__904.Append(t___5770);
        }
        public SqlInt64(long value__907)
        {
            this.value__902 = value__907;
        }
        public long Value
        {
            get
            {
                return this.value__902;
            }
        }
    }
}
