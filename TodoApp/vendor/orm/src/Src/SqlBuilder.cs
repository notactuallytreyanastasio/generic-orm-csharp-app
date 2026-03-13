using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1743;
        public void AppendSafe(string sqlSource__1745)
        {
            SqlSource t___14251 = new SqlSource(sqlSource__1745);
            C::Listed.Add(this.buffer__1743, t___14251);
        }
        public void AppendFragment(SqlFragment fragment__1748)
        {
            G::IReadOnlyList<ISqlPart> t___14249 = fragment__1748.Parts;
            C::Listed.AddAll(this.buffer__1743, t___14249);
        }
        public void AppendPart(ISqlPart part__1751)
        {
            C::Listed.Add(this.buffer__1743, part__1751);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1754)
        {
            void fn__14245(ISqlPart x__1756)
            {
                this.AppendPart(x__1756);
            }
            this.AppendList(values__1754, (S::Action<ISqlPart>) fn__14245);
        }
        public void AppendBoolean(bool value__1758)
        {
            SqlBoolean t___14242 = new SqlBoolean(value__1758);
            C::Listed.Add(this.buffer__1743, t___14242);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__1761)
        {
            void fn__14239(bool x__1763)
            {
                this.AppendBoolean(x__1763);
            }
            this.AppendList(values__1761, (S::Action<bool>) fn__14239);
        }
        public void AppendDate(S::DateTime value__1765)
        {
            SqlDate t___14236 = new SqlDate(value__1765);
            C::Listed.Add(this.buffer__1743, t___14236);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__1768)
        {
            void fn__14233(S::DateTime x__1770)
            {
                this.AppendDate(x__1770);
            }
            this.AppendList(values__1768, (S::Action<S::DateTime>) fn__14233);
        }
        public void AppendFloat64(double value__1772)
        {
            SqlFloat64 t___14230 = new SqlFloat64(value__1772);
            C::Listed.Add(this.buffer__1743, t___14230);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__1775)
        {
            void fn__14227(double x__1777)
            {
                this.AppendFloat64(x__1777);
            }
            this.AppendList(values__1775, (S::Action<double>) fn__14227);
        }
        public void AppendInt32(int value__1779)
        {
            SqlInt32 t___14224 = new SqlInt32(value__1779);
            C::Listed.Add(this.buffer__1743, t___14224);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__1782)
        {
            void fn__14221(int x__1784)
            {
                this.AppendInt32(x__1784);
            }
            this.AppendList(values__1782, (S::Action<int>) fn__14221);
        }
        public void AppendInt64(long value__1786)
        {
            SqlInt64 t___14218 = new SqlInt64(value__1786);
            C::Listed.Add(this.buffer__1743, t___14218);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__1789)
        {
            void fn__14215(long x__1791)
            {
                this.AppendInt64(x__1791);
            }
            this.AppendList(values__1789, (S::Action<long>) fn__14215);
        }
        public void AppendString(string value__1793)
        {
            SqlString t___14212 = new SqlString(value__1793);
            C::Listed.Add(this.buffer__1743, t___14212);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1796)
        {
            void fn__14209(string x__1798)
            {
                this.AppendString(x__1798);
            }
            this.AppendList(values__1796, (S::Action<string>) fn__14209);
        }
        void AppendList<T__340>(G::IReadOnlyList<T__340> values__1800, S::Action<T__340> appendValue__1801)
        {
            int t___14204;
            T__340 t___14206;
            int i__1803 = 0;
            while (true)
            {
                t___14204 = values__1800.Count;
                if (!(i__1803 < t___14204)) break;
                if (i__1803 > 0) this.AppendSafe(", ");
                t___14206 = values__1800[i__1803];
                appendValue__1801(t___14206);
                i__1803 = i__1803 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1743));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___14201 = new G::List<ISqlPart>();
            this.buffer__1743 = t___14201;
        }
    }
}
