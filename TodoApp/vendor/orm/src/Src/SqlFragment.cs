using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__858;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___5782;
            T::StringBuilder builder__863 = new T::StringBuilder();
            int i__864 = 0;
            while (true)
            {
                t___5782 = this.parts__858.Count;
                if (!(i__864 < t___5782)) break;
                this.parts__858[i__864].FormatTo(builder__863);
                i__864 = i__864 + 1;
            }
            return builder__863.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__866)
        {
            this.parts__858 = parts__866;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__858;
            }
        }
    }
}
