using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1484;
        public void FormatTo(T::StringBuilder builder__1486)
        {
            string t___6592;
            if (this.value__1484)
            {
                t___6592 = "TRUE";
            }
            else
            {
                t___6592 = "FALSE";
            }
            builder__1486.Append(t___6592);
        }
        public SqlBoolean(bool value__1489)
        {
            this.value__1484 = value__1489;
        }
        public bool Value
        {
            get
            {
                return this.value__1484;
            }
        }
    }
}
