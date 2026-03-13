using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__2069;
        public void FormatTo(T::StringBuilder builder__2071)
        {
            string t___9792;
            if (this.value__2069)
            {
                t___9792 = "TRUE";
            }
            else
            {
                t___9792 = "FALSE";
            }
            builder__2071.Append(t___9792);
        }
        public SqlBoolean(bool value__2074)
        {
            this.value__2069 = value__2074;
        }
        public bool Value
        {
            get
            {
                return this.value__2069;
            }
        }
    }
}
