using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1254;
        public void FormatTo(T::StringBuilder builder__1256)
        {
            string t___5535;
            if (this.value__1254)
            {
                t___5535 = "TRUE";
            }
            else
            {
                t___5535 = "FALSE";
            }
            builder__1256.Append(t___5535);
        }
        public SqlBoolean(bool value__1259)
        {
            this.value__1254 = value__1259;
        }
        public bool Value
        {
            get
            {
                return this.value__1254;
            }
        }
    }
}
