using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1387;
        public void FormatTo(T::StringBuilder builder__1389)
        {
            string t___6281;
            if (this.value__1387)
            {
                t___6281 = "TRUE";
            }
            else
            {
                t___6281 = "FALSE";
            }
            builder__1389.Append(t___6281);
        }
        public SqlBoolean(bool value__1392)
        {
            this.value__1387 = value__1392;
        }
        public bool Value
        {
            get
            {
                return this.value__1387;
            }
        }
    }
}
