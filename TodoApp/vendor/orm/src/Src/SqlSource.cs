using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1164;
        public void FormatTo(T::StringBuilder builder__1166)
        {
            builder__1166.Append(this.source__1164);
        }
        public SqlSource(string source__1169)
        {
            this.source__1164 = source__1169;
        }
        public string Source
        {
            get
            {
                return this.source__1164;
            }
        }
    }
}
