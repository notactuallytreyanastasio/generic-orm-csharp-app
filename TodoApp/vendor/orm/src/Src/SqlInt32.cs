using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1848;
        public void FormatTo(T::StringBuilder builder__1850)
        {
            string t___14265 = S::Convert.ToString(this.value__1848);
            builder__1850.Append(t___14265);
        }
        public SqlInt32(int value__1853)
        {
            this.value__1848 = value__1853;
        }
        public int Value
        {
            get
            {
                return this.value__1848;
            }
        }
    }
}
