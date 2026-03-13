using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1838;
        public void AppendSafe(string sqlSource__1840)
        {
            SqlSource t___15520 = new SqlSource(sqlSource__1840);
            C::Listed.Add(this.buffer__1838, t___15520);
        }
        public void AppendFragment(SqlFragment fragment__1843)
        {
            G::IReadOnlyList<ISqlPart> t___15518 = fragment__1843.Parts;
            C::Listed.AddAll(this.buffer__1838, t___15518);
        }
        public void AppendPart(ISqlPart part__1846)
        {
            C::Listed.Add(this.buffer__1838, part__1846);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1849)
        {
            void fn__15514(ISqlPart x__1851)
            {
                this.AppendPart(x__1851);
            }
            this.AppendList(values__1849, (S::Action<ISqlPart>) fn__15514);
        }
        public void AppendBoolean(bool value__1853)
        {
            SqlBoolean t___15511 = new SqlBoolean(value__1853);
            C::Listed.Add(this.buffer__1838, t___15511);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__1856)
        {
            void fn__15508(bool x__1858)
            {
                this.AppendBoolean(x__1858);
            }
            this.AppendList(values__1856, (S::Action<bool>) fn__15508);
        }
        public void AppendDate(S::DateTime value__1860)
        {
            SqlDate t___15505 = new SqlDate(value__1860);
            C::Listed.Add(this.buffer__1838, t___15505);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__1863)
        {
            void fn__15502(S::DateTime x__1865)
            {
                this.AppendDate(x__1865);
            }
            this.AppendList(values__1863, (S::Action<S::DateTime>) fn__15502);
        }
        public void AppendFloat64(double value__1867)
        {
            SqlFloat64 t___15499 = new SqlFloat64(value__1867);
            C::Listed.Add(this.buffer__1838, t___15499);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__1870)
        {
            void fn__15496(double x__1872)
            {
                this.AppendFloat64(x__1872);
            }
            this.AppendList(values__1870, (S::Action<double>) fn__15496);
        }
        public void AppendInt32(int value__1874)
        {
            SqlInt32 t___15493 = new SqlInt32(value__1874);
            C::Listed.Add(this.buffer__1838, t___15493);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__1877)
        {
            void fn__15490(int x__1879)
            {
                this.AppendInt32(x__1879);
            }
            this.AppendList(values__1877, (S::Action<int>) fn__15490);
        }
        public void AppendInt64(long value__1881)
        {
            SqlInt64 t___15487 = new SqlInt64(value__1881);
            C::Listed.Add(this.buffer__1838, t___15487);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__1884)
        {
            void fn__15484(long x__1886)
            {
                this.AppendInt64(x__1886);
            }
            this.AppendList(values__1884, (S::Action<long>) fn__15484);
        }
        public void AppendString(string value__1888)
        {
            SqlString t___15481 = new SqlString(value__1888);
            C::Listed.Add(this.buffer__1838, t___15481);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1891)
        {
            void fn__15478(string x__1893)
            {
                this.AppendString(x__1893);
            }
            this.AppendList(values__1891, (S::Action<string>) fn__15478);
        }
        void AppendList<T__356>(G::IReadOnlyList<T__356> values__1895, S::Action<T__356> appendValue__1896)
        {
            int t___15473;
            T__356 t___15475;
            int i__1898 = 0;
            while (true)
            {
                t___15473 = values__1895.Count;
                if (!(i__1898 < t___15473)) break;
                if (i__1898 > 0) this.AppendSafe(", ");
                t___15475 = values__1895[i__1898];
                appendValue__1896(t___15475);
                i__1898 = i__1898 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1838));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___15470 = new G::List<ISqlPart>();
            this.buffer__1838 = t___15470;
        }
    }
}
