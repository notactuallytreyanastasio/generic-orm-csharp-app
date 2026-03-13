using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1052;
        public void FormatTo(T::StringBuilder builder__1054)
        {
            string t___7204 = S::Convert.ToString(this.value__1052);
            builder__1054.Append(t___7204);
        }
        public SqlInt32(int value__1057)
        {
            this.value__1052 = value__1057;
        }
        public int Value
        {
            get
            {
                return this.value__1052;
            }
        }
    }
}
