using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1369;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___10919;
            T::StringBuilder builder__1374 = new T::StringBuilder();
            int i__1375 = 0;
            while (true)
            {
                t___10919 = this.parts__1369.Count;
                if (!(i__1375 < t___10919)) break;
                this.parts__1369[i__1375].FormatTo(builder__1374);
                i__1375 = i__1375 + 1;
            }
            return builder__1374.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1377)
        {
            this.parts__1369 = parts__1377;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1369;
            }
        }
    }
}
