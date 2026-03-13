using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1504;
        public void FormatTo(T::StringBuilder builder__1506)
        {
            string t___11435 = S::Convert.ToString(this.value__1504);
            builder__1506.Append(t___11435);
        }
        public SqlInt32(int value__1509)
        {
            this.value__1504 = value__1509;
        }
        public int Value
        {
            get
            {
                return this.value__1504;
            }
        }
    }
}
