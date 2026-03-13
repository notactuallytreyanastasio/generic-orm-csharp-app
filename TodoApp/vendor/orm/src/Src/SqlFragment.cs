using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__2051;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___17363;
            T::StringBuilder builder__2056 = new T::StringBuilder();
            int i__2057 = 0;
            while (true)
            {
                t___17363 = this.parts__2051.Count;
                if (!(i__2057 < t___17363)) break;
                this.parts__2051[i__2057].FormatTo(builder__2056);
                i__2057 = i__2057 + 1;
            }
            return builder__2056.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__2059)
        {
            this.parts__2051 = parts__2059;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__2051;
            }
        }
    }
}
