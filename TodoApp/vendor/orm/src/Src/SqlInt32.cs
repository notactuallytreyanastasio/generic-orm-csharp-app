using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__2089;
        public void FormatTo(T::StringBuilder builder__2091)
        {
            string t___17353 = S::Convert.ToString(this.value__2089);
            builder__2091.Append(t___17353);
        }
        public SqlInt32(int value__2094)
        {
            this.value__2089 = value__2094;
        }
        public int Value
        {
            get
            {
                return this.value__2089;
            }
        }
    }
}
