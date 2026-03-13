using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1026;
        public void FormatTo(T::StringBuilder builder__1028)
        {
            builder__1028.Append(this.source__1026);
        }
        public SqlSource(string source__1031)
        {
            this.source__1026 = source__1031;
        }
        public string Source
        {
            get
            {
                return this.source__1026;
            }
        }
    }
}
