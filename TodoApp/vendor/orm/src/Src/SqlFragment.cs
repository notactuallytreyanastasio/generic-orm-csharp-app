using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1014;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___7214;
            T::StringBuilder builder__1019 = new T::StringBuilder();
            int i__1020 = 0;
            while (true)
            {
                t___7214 = this.parts__1014.Count;
                if (!(i__1020 < t___7214)) break;
                this.parts__1014[i__1020].FormatTo(builder__1019);
                i__1020 = i__1020 + 1;
            }
            return builder__1019.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1022)
        {
            this.parts__1014 = parts__1022;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1014;
            }
        }
    }
}
