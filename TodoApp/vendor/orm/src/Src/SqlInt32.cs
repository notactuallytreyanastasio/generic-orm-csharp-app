using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__896;
        public void FormatTo(T::StringBuilder builder__898)
        {
            string t___5772 = S::Convert.ToString(this.value__896);
            builder__898.Append(t___5772);
        }
        public SqlInt32(int value__901)
        {
            this.value__896 = value__901;
        }
        public int Value
        {
            get
            {
                return this.value__896;
            }
        }
    }
}
