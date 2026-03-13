using T = System.Text;
namespace Orm.Src
{
    public class SqlDefault: ISqlPart
    {
        public void FormatTo(T::StringBuilder builder__2102)
        {
            builder__2102.Append("DEFAULT");
        }
        public SqlDefault()
        {
        }
    }
}
