using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1169;
        public void AppendSafe(string sqlSource__1171)
        {
            SqlSource t___9641 = new SqlSource(sqlSource__1171);
            C::Listed.Add(this.buffer__1169, t___9641);
        }
        public void AppendFragment(SqlFragment fragment__1174)
        {
            G::IReadOnlyList<ISqlPart> t___9639 = fragment__1174.Parts;
            C::Listed.AddAll(this.buffer__1169, t___9639);
        }
        public void AppendPart(ISqlPart part__1177)
        {
            C::Listed.Add(this.buffer__1169, part__1177);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1180)
        {
            void fn__9635(ISqlPart x__1182)
            {
                this.AppendPart(x__1182);
            }
            this.AppendList(values__1180, (S::Action<ISqlPart>) fn__9635);
        }
        public void AppendBoolean(bool value__1184)
        {
            SqlBoolean t___9632 = new SqlBoolean(value__1184);
            C::Listed.Add(this.buffer__1169, t___9632);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__1187)
        {
            void fn__9629(bool x__1189)
            {
                this.AppendBoolean(x__1189);
            }
            this.AppendList(values__1187, (S::Action<bool>) fn__9629);
        }
        public void AppendDate(S::DateTime value__1191)
        {
            SqlDate t___9626 = new SqlDate(value__1191);
            C::Listed.Add(this.buffer__1169, t___9626);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__1194)
        {
            void fn__9623(S::DateTime x__1196)
            {
                this.AppendDate(x__1196);
            }
            this.AppendList(values__1194, (S::Action<S::DateTime>) fn__9623);
        }
        public void AppendFloat64(double value__1198)
        {
            SqlFloat64 t___9620 = new SqlFloat64(value__1198);
            C::Listed.Add(this.buffer__1169, t___9620);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__1201)
        {
            void fn__9617(double x__1203)
            {
                this.AppendFloat64(x__1203);
            }
            this.AppendList(values__1201, (S::Action<double>) fn__9617);
        }
        public void AppendInt32(int value__1205)
        {
            SqlInt32 t___9614 = new SqlInt32(value__1205);
            C::Listed.Add(this.buffer__1169, t___9614);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__1208)
        {
            void fn__9611(int x__1210)
            {
                this.AppendInt32(x__1210);
            }
            this.AppendList(values__1208, (S::Action<int>) fn__9611);
        }
        public void AppendInt64(long value__1212)
        {
            SqlInt64 t___9608 = new SqlInt64(value__1212);
            C::Listed.Add(this.buffer__1169, t___9608);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__1215)
        {
            void fn__9605(long x__1217)
            {
                this.AppendInt64(x__1217);
            }
            this.AppendList(values__1215, (S::Action<long>) fn__9605);
        }
        public void AppendString(string value__1219)
        {
            SqlString t___9602 = new SqlString(value__1219);
            C::Listed.Add(this.buffer__1169, t___9602);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1222)
        {
            void fn__9599(string x__1224)
            {
                this.AppendString(x__1224);
            }
            this.AppendList(values__1222, (S::Action<string>) fn__9599);
        }
        void AppendList<T__237>(G::IReadOnlyList<T__237> values__1226, S::Action<T__237> appendValue__1227)
        {
            int t___9594;
            T__237 t___9596;
            int i__1229 = 0;
            while (true)
            {
                t___9594 = values__1226.Count;
                if (!(i__1229 < t___9594)) break;
                if (i__1229 > 0) this.AppendSafe(", ");
                t___9596 = values__1226[i__1229];
                appendValue__1227(t___9596);
                i__1229 = i__1229 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1169));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___9591 = new G::List<ISqlPart>();
            this.buffer__1169 = t___9591;
        }
    }
}
