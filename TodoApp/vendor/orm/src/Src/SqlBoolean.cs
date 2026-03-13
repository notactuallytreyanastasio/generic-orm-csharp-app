using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__876;
        public void FormatTo(T::StringBuilder builder__878)
        {
            string t___3437;
            if (this.value__876)
            {
                t___3437 = "TRUE";
            }
            else
            {
                t___3437 = "FALSE";
            }
            builder__878.Append(t___3437);
        }
        public SqlBoolean(bool value__881)
        {
            this.value__876 = value__881;
        }
        public bool Value
        {
            get
            {
                return this.value__876;
            }
        }
    }
}
