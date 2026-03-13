using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1822;
        public void FormatTo(T::StringBuilder builder__1824)
        {
            builder__1824.Append(this.source__1822);
        }
        public SqlSource(string source__1827)
        {
            this.source__1822 = source__1827;
        }
        public string Source
        {
            get
            {
                return this.source__1822;
            }
        }
    }
}
