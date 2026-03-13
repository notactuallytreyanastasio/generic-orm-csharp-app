using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1810;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___14275;
            T::StringBuilder builder__1815 = new T::StringBuilder();
            int i__1816 = 0;
            while (true)
            {
                t___14275 = this.parts__1810.Count;
                if (!(i__1816 < t___14275)) break;
                this.parts__1810[i__1816].FormatTo(builder__1815);
                i__1816 = i__1816 + 1;
            }
            return builder__1815.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1818)
        {
            this.parts__1810 = parts__1818;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1810;
            }
        }
    }
}
