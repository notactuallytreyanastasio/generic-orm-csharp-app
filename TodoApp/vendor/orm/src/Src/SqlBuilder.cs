using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1399;
        public void AppendSafe(string sqlSource__1401)
        {
            SqlSource t___11421 = new SqlSource(sqlSource__1401);
            C::Listed.Add(this.buffer__1399, t___11421);
        }
        public void AppendFragment(SqlFragment fragment__1404)
        {
            G::IReadOnlyList<ISqlPart> t___11419 = fragment__1404.Parts;
            C::Listed.AddAll(this.buffer__1399, t___11419);
        }
        public void AppendPart(ISqlPart part__1407)
        {
            C::Listed.Add(this.buffer__1399, part__1407);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1410)
        {
            void fn__11415(ISqlPart x__1412)
            {
                this.AppendPart(x__1412);
            }
            this.AppendList(values__1410, (S::Action<ISqlPart>) fn__11415);
        }
        public void AppendBoolean(bool value__1414)
        {
            SqlBoolean t___11412 = new SqlBoolean(value__1414);
            C::Listed.Add(this.buffer__1399, t___11412);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__1417)
        {
            void fn__11409(bool x__1419)
            {
                this.AppendBoolean(x__1419);
            }
            this.AppendList(values__1417, (S::Action<bool>) fn__11409);
        }
        public void AppendDate(S::DateTime value__1421)
        {
            SqlDate t___11406 = new SqlDate(value__1421);
            C::Listed.Add(this.buffer__1399, t___11406);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__1424)
        {
            void fn__11403(S::DateTime x__1426)
            {
                this.AppendDate(x__1426);
            }
            this.AppendList(values__1424, (S::Action<S::DateTime>) fn__11403);
        }
        public void AppendFloat64(double value__1428)
        {
            SqlFloat64 t___11400 = new SqlFloat64(value__1428);
            C::Listed.Add(this.buffer__1399, t___11400);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__1431)
        {
            void fn__11397(double x__1433)
            {
                this.AppendFloat64(x__1433);
            }
            this.AppendList(values__1431, (S::Action<double>) fn__11397);
        }
        public void AppendInt32(int value__1435)
        {
            SqlInt32 t___11394 = new SqlInt32(value__1435);
            C::Listed.Add(this.buffer__1399, t___11394);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__1438)
        {
            void fn__11391(int x__1440)
            {
                this.AppendInt32(x__1440);
            }
            this.AppendList(values__1438, (S::Action<int>) fn__11391);
        }
        public void AppendInt64(long value__1442)
        {
            SqlInt64 t___11388 = new SqlInt64(value__1442);
            C::Listed.Add(this.buffer__1399, t___11388);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__1445)
        {
            void fn__11385(long x__1447)
            {
                this.AppendInt64(x__1447);
            }
            this.AppendList(values__1445, (S::Action<long>) fn__11385);
        }
        public void AppendString(string value__1449)
        {
            SqlString t___11382 = new SqlString(value__1449);
            C::Listed.Add(this.buffer__1399, t___11382);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1452)
        {
            void fn__11379(string x__1454)
            {
                this.AppendString(x__1454);
            }
            this.AppendList(values__1452, (S::Action<string>) fn__11379);
        }
        void AppendList<T__277>(G::IReadOnlyList<T__277> values__1456, S::Action<T__277> appendValue__1457)
        {
            int t___11374;
            T__277 t___11376;
            int i__1459 = 0;
            while (true)
            {
                t___11374 = values__1456.Count;
                if (!(i__1459 < t___11374)) break;
                if (i__1459 > 0) this.AppendSafe(", ");
                t___11376 = values__1456[i__1459];
                appendValue__1457(t___11376);
                i__1459 = i__1459 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1399));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___11371 = new G::List<ISqlPart>();
            this.buffer__1399 = t___11371;
        }
    }
}
