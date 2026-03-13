using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__2063;
        public void FormatTo(T::StringBuilder builder__2065)
        {
            builder__2065.Append(this.source__2063);
        }
        public SqlSource(string source__2068)
        {
            this.source__2063 = source__2068;
        }
        public string Source
        {
            get
            {
                return this.source__2063;
            }
        }
    }
}
