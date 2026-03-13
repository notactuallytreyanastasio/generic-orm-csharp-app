using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1905;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___15544;
            T::StringBuilder builder__1910 = new T::StringBuilder();
            int i__1911 = 0;
            while (true)
            {
                t___15544 = this.parts__1905.Count;
                if (!(i__1911 < t___15544)) break;
                this.parts__1905[i__1911].FormatTo(builder__1910);
                i__1911 = i__1911 + 1;
            }
            return builder__1910.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1913)
        {
            this.parts__1905 = parts__1913;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1905;
            }
        }
    }
}
