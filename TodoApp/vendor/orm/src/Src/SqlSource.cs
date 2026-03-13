using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1381;
        public void FormatTo(T::StringBuilder builder__1383)
        {
            builder__1383.Append(this.source__1381);
        }
        public SqlSource(string source__1386)
        {
            this.source__1381 = source__1386;
        }
        public string Source
        {
            get
            {
                return this.source__1381;
            }
        }
    }
}
