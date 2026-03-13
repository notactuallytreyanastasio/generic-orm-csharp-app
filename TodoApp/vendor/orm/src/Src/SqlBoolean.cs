using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1170;
        public void FormatTo(T::StringBuilder builder__1172)
        {
            string t___5061;
            if (this.value__1170)
            {
                t___5061 = "TRUE";
            }
            else
            {
                t___5061 = "FALSE";
            }
            builder__1172.Append(t___5061);
        }
        public SqlBoolean(bool value__1175)
        {
            this.value__1170 = value__1175;
        }
        public bool Value
        {
            get
            {
                return this.value__1170;
            }
        }
    }
}
