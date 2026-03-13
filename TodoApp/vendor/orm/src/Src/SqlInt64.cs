using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt64: ISqlPart
    {
        readonly long value__1196;
        public void FormatTo(T::StringBuilder builder__1198)
        {
            string t___8779 = S::Convert.ToString(this.value__1196);
            builder__1198.Append(t___8779);
        }
        public SqlInt64(long value__1201)
        {
            this.value__1196 = value__1201;
        }
        public long Value
        {
            get
            {
                return this.value__1196;
            }
        }
    }
}
