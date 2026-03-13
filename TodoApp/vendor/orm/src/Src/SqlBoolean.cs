using T = System.Text;
namespace Orm.Src
{
    public class SqlBoolean: ISqlPart
    {
        readonly bool value__768;
        public void FormatTo(T::StringBuilder builder__770)
        {
            string t___3032;
            if (this.value__768)
            {
                t___3032 = "TRUE";
            }
            else
            {
                t___3032 = "FALSE";
            }
            builder__770.Append(t___3032);
        }
        public SqlBoolean(bool value__773)
        {
            this.value__768 = value__773;
        }
        public bool Value
        {
            get
            {
                return this.value__768;
            }
        }
    }
}
