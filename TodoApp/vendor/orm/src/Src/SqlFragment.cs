using G = System.Collections.Generic;
using T = System.Text;
namespace Orm.Src
{
    public class SqlFragment
    {
        readonly G::IReadOnlyList<ISqlPart> parts__1152;
        public SqlSource ToSource()
        {
            return new SqlSource(this.ToString());
        }
        public string ToString()
        {
            int t___8791;
            T::StringBuilder builder__1157 = new T::StringBuilder();
            int i__1158 = 0;
            while (true)
            {
                t___8791 = this.parts__1152.Count;
                if (!(i__1158 < t___8791)) break;
                this.parts__1152[i__1158].FormatTo(builder__1157);
                i__1158 = i__1158 + 1;
            }
            return builder__1157.ToString();
        }
        public SqlFragment(G::IReadOnlyList<ISqlPart> parts__1160)
        {
            this.parts__1152 = parts__1160;
        }
        public G::IReadOnlyList<ISqlPart> Parts
        {
            get
            {
                return this.parts__1152;
            }
        }
    }
}
