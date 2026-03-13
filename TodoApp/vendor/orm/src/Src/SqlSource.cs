using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1248;
        public void FormatTo(T::StringBuilder builder__1250)
        {
            builder__1250.Append(this.source__1248);
        }
        public SqlSource(string source__1253)
        {
            this.source__1248 = source__1253;
        }
        public string Source
        {
            get
            {
                return this.source__1248;
            }
        }
    }
}
