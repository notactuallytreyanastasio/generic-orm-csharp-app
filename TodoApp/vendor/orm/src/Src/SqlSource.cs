using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__1478;
        public void FormatTo(T::StringBuilder builder__1480)
        {
            builder__1480.Append(this.source__1478);
        }
        public SqlSource(string source__1483)
        {
            this.source__1478 = source__1483;
        }
        public string Source
        {
            get
            {
                return this.source__1478;
            }
        }
    }
}
