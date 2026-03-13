using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1274;
        public void FormatTo(T::StringBuilder builder__1276)
        {
            string t___9655 = S::Convert.ToString(this.value__1274);
            builder__1276.Append(t___9655);
        }
        public SqlInt32(int value__1279)
        {
            this.value__1274 = value__1279;
        }
        public int Value
        {
            get
            {
                return this.value__1274;
            }
        }
    }
}
