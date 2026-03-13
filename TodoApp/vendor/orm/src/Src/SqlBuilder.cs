using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1984;
        public void AppendSafe(string sqlSource__1986)
        {
            SqlSource t___17339 = new SqlSource(sqlSource__1986);
            C::Listed.Add(this.buffer__1984, t___17339);
        }
        public void AppendFragment(SqlFragment fragment__1989)
        {
            G::IReadOnlyList<ISqlPart> t___17337 = fragment__1989.Parts;
            C::Listed.AddAll(this.buffer__1984, t___17337);
        }
        public void AppendPart(ISqlPart part__1992)
        {
            C::Listed.Add(this.buffer__1984, part__1992);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1995)
        {
            void fn__17333(ISqlPart x__1997)
            {
                this.AppendPart(x__1997);
            }
            this.AppendList(values__1995, (S::Action<ISqlPart>) fn__17333);
        }
        public void AppendBoolean(bool value__1999)
        {
            SqlBoolean t___17330 = new SqlBoolean(value__1999);
            C::Listed.Add(this.buffer__1984, t___17330);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__2002)
        {
            void fn__17327(bool x__2004)
            {
                this.AppendBoolean(x__2004);
            }
            this.AppendList(values__2002, (S::Action<bool>) fn__17327);
        }
        public void AppendDate(S::DateTime value__2006)
        {
            SqlDate t___17324 = new SqlDate(value__2006);
            C::Listed.Add(this.buffer__1984, t___17324);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__2009)
        {
            void fn__17321(S::DateTime x__2011)
            {
                this.AppendDate(x__2011);
            }
            this.AppendList(values__2009, (S::Action<S::DateTime>) fn__17321);
        }
        public void AppendFloat64(double value__2013)
        {
            SqlFloat64 t___17318 = new SqlFloat64(value__2013);
            C::Listed.Add(this.buffer__1984, t___17318);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__2016)
        {
            void fn__17315(double x__2018)
            {
                this.AppendFloat64(x__2018);
            }
            this.AppendList(values__2016, (S::Action<double>) fn__17315);
        }
        public void AppendInt32(int value__2020)
        {
            SqlInt32 t___17312 = new SqlInt32(value__2020);
            C::Listed.Add(this.buffer__1984, t___17312);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__2023)
        {
            void fn__17309(int x__2025)
            {
                this.AppendInt32(x__2025);
            }
            this.AppendList(values__2023, (S::Action<int>) fn__17309);
        }
        public void AppendInt64(long value__2027)
        {
            SqlInt64 t___17306 = new SqlInt64(value__2027);
            C::Listed.Add(this.buffer__1984, t___17306);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__2030)
        {
            void fn__17303(long x__2032)
            {
                this.AppendInt64(x__2032);
            }
            this.AppendList(values__2030, (S::Action<long>) fn__17303);
        }
        public void AppendString(string value__2034)
        {
            SqlString t___17300 = new SqlString(value__2034);
            C::Listed.Add(this.buffer__1984, t___17300);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__2037)
        {
            void fn__17297(string x__2039)
            {
                this.AppendString(x__2039);
            }
            this.AppendList(values__2037, (S::Action<string>) fn__17297);
        }
        void AppendList<T__394>(G::IReadOnlyList<T__394> values__2041, S::Action<T__394> appendValue__2042)
        {
            int t___17292;
            T__394 t___17294;
            int i__2044 = 0;
            while (true)
            {
                t___17292 = values__2041.Count;
                if (!(i__2044 < t___17292)) break;
                if (i__2044 > 0) this.AppendSafe(", ");
                t___17294 = values__2041[i__2044];
                appendValue__2042(t___17294);
                i__2044 = i__2044 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1984));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___17289 = new G::List<ISqlPart>();
            this.buffer__1984 = t___17289;
        }
    }
}
