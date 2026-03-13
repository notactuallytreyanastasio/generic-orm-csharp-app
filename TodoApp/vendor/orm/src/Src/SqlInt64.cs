using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__794;
        public void FormatTo(T::StringBuilder builder__796)
        {
            string t___5084 = S::Convert.ToString(this.value__794);
            builder__796.Append(t___5084);
        }
        public SqlInt64(long value__799)
        {
            this.value__794 = value__799;
        }
        public long Value
        {
            get
            {
                return this.value__794;
            }
        }
    }
}
