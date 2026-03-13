using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1466;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___11445;
            T::StringBuilder builder__1471 = new T::StringBuilder();
            int i__1472 = 0;
            while (true)
            {
                t___11445 = this.parts__1466.Count;
                if (!(i__1472 < t___11445)) break;
                this.parts__1466[i__1472].FormatTo(builder__1471);
                i__1472 = i__1472 + 1;
            }
            return builder__1471.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1474)
        {
            this.parts__1466 = parts__1474;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1466;
            }
        }
    }
}
