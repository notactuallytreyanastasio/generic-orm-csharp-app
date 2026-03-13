using T = System.Text;
namespace Orm.Src
{
    public class SqlDefault: ISqlPart
    {
        public void FormatTo(T::StringBuilder builder__1956)
        {
            builder__1956.Append("DEFAULT");
        }
        public SqlDefault()
        {
        }
    }
}
