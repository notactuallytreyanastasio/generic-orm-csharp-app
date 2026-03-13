using S = System;
using T = System.Text;
namespace Orm.Src
{
    public class SqlInt32: ISqlPart
    {
        readonly int value__1190;
        public void FormatTo(T::StringBuilder builder__1192)
        {
            string t___8781 = S::Convert.ToString(this.value__1190);
            builder__1192.Append(t___8781);
        }
        public SqlInt32(int value__1195)
        {
            this.value__1190 = value__1195;
        }
        public int Value
        {
            get
            {
                return this.value__1190;
            }
        }
    }
}
