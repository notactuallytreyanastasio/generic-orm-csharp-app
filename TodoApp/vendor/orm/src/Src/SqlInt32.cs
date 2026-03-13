using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1943;
        public void FormatTo(T::StringBuilder builder__1945)
        {
            string t___15534 = S::Convert.ToString(this.value__1943);
            builder__1945.Append(t___15534);
        }
        public SqlInt32(int value__1948)
        {
            this.value__1943 = value__1948;
        }
        public int Value
        {
            get
            {
                return this.value__1943;
            }
        }
    }
}
