using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__1828;
        public void FormatTo(T::StringBuilder builder__1830)
        {
            string t___8157;
            if (this.value__1828)
            {
                t___8157 = "TRUE";
            }
            else
            {
                t___8157 = "FALSE";
            }
            builder__1830.Append(t___8157);
        }
        public SqlBoolean(bool value__1833)
        {
            this.value__1828 = value__1833;
        }
        public bool Value
        {
            get
            {
                return this.value__1828;
            }
        }
    }
}
