using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1032;
        public void FormatTo(T::StringBuilder builder__1034)
        {
            string t___4218;
            if (this.value__1032)
            {
                t___4218 = "TRUE";
            }
            else
            {
                t___4218 = "FALSE";
            }
            builder__1034.Append(t___4218);
        }
        public SqlBoolean(bool value__1037)
        {
            this.value__1032 = value__1037;
        }
        public bool Value
        {
            get
            {
                return this.value__1032;
            }
        }
    }
}
