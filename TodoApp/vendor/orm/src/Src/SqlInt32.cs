using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__788;
        public void FormatTo(T::StringBuilder builder__790)
        {
            string t___5086 = S::Convert.ToString(this.value__788);
            builder__790.Append(t___5086);
        }
        public SqlInt32(int value__793)
        {
            this.value__788 = value__793;
        }
        public int Value
        {
            get
            {
                return this.value__788;
            }
        }
    }
}
