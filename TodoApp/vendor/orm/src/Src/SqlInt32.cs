using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1407;
        public void FormatTo(T::StringBuilder builder__1409)
        {
            string t___10909 = S::Convert.ToString(this.value__1407);
            builder__1409.Append(t___10909);
        }
        public SqlInt32(int value__1412)
        {
            this.value__1407 = value__1412;
        }
        public int Value
        {
            get
            {
                return this.value__1407;
            }
        }
    }
}
