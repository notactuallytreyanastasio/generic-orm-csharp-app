using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1236;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___9665;
            T::StringBuilder builder__1241 = new T::StringBuilder();
            int i__1242 = 0;
            while (true)
            {
                t___9665 = this.parts__1236.Count;
                if (!(i__1242 < t___9665)) break;
                this.parts__1236[i__1242].FormatTo(builder__1241);
                i__1242 = i__1242 + 1;
            }
            return builder__1241.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1244)
        {
            this.parts__1236 = parts__1244;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1236;
            }
        }
    }
}
