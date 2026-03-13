using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__762;
        public void FormatTo(T::StringBuilder builder__764)
        {
            builder__764.Append(this.source__762);
        }
        public SqlSource(string source__767)
        {
            this.source__762 = source__767;
        }
        public string Source
        {
            get
            {
                return this.source__762;
            }
        }
    }
}
