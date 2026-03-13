using T = System.Text;
namespace Orm.Src
{
    public class SqlSource: ISqlPart
    {
        readonly string source__870;
        public void FormatTo(T::StringBuilder builder__872)
        {
            builder__872.Append(this.source__870);
        }
        public SqlSource(string source__875)
        {
            this.source__870 = source__875;
        }
        public string Source
        {
            get
            {
                return this.source__870;
            }
        }
    }
}
