using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__683;
        public void AppendSafe(string sqlSource__685)
        {
            SqlSource t___5072 = new SqlSource(sqlSource__685);
            C::Listed.Add(this.buffer__683, t___5072);
        }
        public void AppendFragment(SqlFragment fragment__688)
        {
            G::IReadOnlyList<ISqlPart> t___5070 = fragment__688.Parts;
            C::Listed.AddAll(this.buffer__683, t___5070);
        }
        public void AppendPart(ISqlPart part__691)
        {
            C::Listed.Add(this.buffer__683, part__691);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__694)
        {
            void fn__5066(ISqlPart x__696)
            {
                this.AppendPart(x__696);
            }
            this.AppendList(values__694, (S::Action<ISqlPart>) fn__5066);
        }
        public void AppendBoolean(bool value__698)
        {
            SqlBoolean t___5063 = new SqlBoolean(value__698);
            C::Listed.Add(this.buffer__683, t___5063);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__701)
        {
            void fn__5060(bool x__703)
            {
                this.AppendBoolean(x__703);
            }
            this.AppendList(values__701, (S::Action<bool>) fn__5060);
        }
        public void AppendDate(S::DateTime value__705)
        {
            SqlDate t___5057 = new SqlDate(value__705);
            C::Listed.Add(this.buffer__683, t___5057);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__708)
        {
            void fn__5054(S::DateTime x__710)
            {
                this.AppendDate(x__710);
            }
            this.AppendList(values__708, (S::Action<S::DateTime>) fn__5054);
        }
        public void AppendFloat64(double value__712)
        {
            SqlFloat64 t___5051 = new SqlFloat64(value__712);
            C::Listed.Add(this.buffer__683, t___5051);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__715)
        {
            void fn__5048(double x__717)
            {
                this.AppendFloat64(x__717);
            }
            this.AppendList(values__715, (S::Action<double>) fn__5048);
        }
        public void AppendInt32(int value__719)
        {
            SqlInt32 t___5045 = new SqlInt32(value__719);
            C::Listed.Add(this.buffer__683, t___5045);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__722)
        {
            void fn__5042(int x__724)
            {
                this.AppendInt32(x__724);
            }
            this.AppendList(values__722, (S::Action<int>) fn__5042);
        }
        public void AppendInt64(long value__726)
        {
            SqlInt64 t___5039 = new SqlInt64(value__726);
            C::Listed.Add(this.buffer__683, t___5039);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__729)
        {
            void fn__5036(long x__731)
            {
                this.AppendInt64(x__731);
            }
            this.AppendList(values__729, (S::Action<long>) fn__5036);
        }
        public void AppendString(string value__733)
        {
            SqlString t___5033 = new SqlString(value__733);
            C::Listed.Add(this.buffer__683, t___5033);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__736)
        {
            void fn__5030(string x__738)
            {
                this.AppendString(x__738);
            }
            this.AppendList(values__736, (S::Action<string>) fn__5030);
        }
        void AppendList<T__145>(G::IReadOnlyList<T__145> values__740, S::Action<T__145> appendValue__741)
        {
            int t___5025;
            T__145 t___5027;
            int i__743 = 0;
            while (true)
            {
                t___5025 = values__740.Count;
                if (!(i__743 < t___5025)) break;
                if (i__743 > 0) this.AppendSafe(", ");
                t___5027 = values__740[i__743];
                appendValue__741(t___5027);
                i__743 = i__743 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__683));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___5022 = new G::List<ISqlPart>();
            this.buffer__683 = t___5022;
        }
    }
}
