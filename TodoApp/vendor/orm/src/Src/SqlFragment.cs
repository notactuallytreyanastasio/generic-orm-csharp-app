using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__750;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___5096;
            T::StringBuilder builder__755 = new T::StringBuilder();
            int i__756 = 0;
            while (true)
            {
                t___5096 = this.parts__750.Count;
                if (!(i__756 < t___5096)) break;
                this.parts__750[i__756].FormatTo(builder__755);
                i__756 = i__756 + 1;
            }
            return builder__755.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__758)
        {
            this.parts__750 = parts__758;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__750;
            }
        }
    }
}
