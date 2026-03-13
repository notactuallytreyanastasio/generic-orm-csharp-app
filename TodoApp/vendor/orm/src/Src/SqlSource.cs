using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1917;
        public void FormatTo(T::StringBuilder builder__1919)
        {
            builder__1919.Append(this.source__1917);
        }
        public SqlSource(string source__1922)
        {
            this.source__1917 = source__1922;
        }
        public string Source
        {
            get
            {
                return this.source__1917;
            }
        }
    }
}
