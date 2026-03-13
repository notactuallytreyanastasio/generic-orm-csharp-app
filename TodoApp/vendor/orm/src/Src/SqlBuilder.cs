using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__947;
        public void AppendSafe(string sqlSource__949)
        {
            SqlSource t___7190 = new SqlSource(sqlSource__949);
            C::Listed.Add(this.buffer__947, t___7190);
        }
        public void AppendFragment(SqlFragment fragment__952)
        {
            G::IReadOnlyList<ISqlPart> t___7188 = fragment__952.Parts;
            C::Listed.AddAll(this.buffer__947, t___7188);
        }
        public void AppendPart(ISqlPart part__955)
        {
            C::Listed.Add(this.buffer__947, part__955);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__958)
        {
            void fn__7184(ISqlPart x__960)
            {
                this.AppendPart(x__960);
            }
            this.AppendList(values__958, (S::Action<ISqlPart>) fn__7184);
        }
        public void AppendBoolean(bool value__962)
        {
            SqlBoolean t___7181 = new SqlBoolean(value__962);
            C::Listed.Add(this.buffer__947, t___7181);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__965)
        {
            void fn__7178(bool x__967)
            {
                this.AppendBoolean(x__967);
            }
            this.AppendList(values__965, (S::Action<bool>) fn__7178);
        }
        public void AppendDate(S::DateTime value__969)
        {
            SqlDate t___7175 = new SqlDate(value__969);
            C::Listed.Add(this.buffer__947, t___7175);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__972)
        {
            void fn__7172(S::DateTime x__974)
            {
                this.AppendDate(x__974);
            }
            this.AppendList(values__972, (S::Action<S::DateTime>) fn__7172);
        }
        public void AppendFloat64(double value__976)
        {
            SqlFloat64 t___7169 = new SqlFloat64(value__976);
            C::Listed.Add(this.buffer__947, t___7169);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__979)
        {
            void fn__7166(double x__981)
            {
                this.AppendFloat64(x__981);
            }
            this.AppendList(values__979, (S::Action<double>) fn__7166);
        }
        public void AppendInt32(int value__983)
        {
            SqlInt32 t___7163 = new SqlInt32(value__983);
            C::Listed.Add(this.buffer__947, t___7163);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__986)
        {
            void fn__7160(int x__988)
            {
                this.AppendInt32(x__988);
            }
            this.AppendList(values__986, (S::Action<int>) fn__7160);
        }
        public void AppendInt64(long value__990)
        {
            SqlInt64 t___7157 = new SqlInt64(value__990);
            C::Listed.Add(this.buffer__947, t___7157);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__993)
        {
            void fn__7154(long x__995)
            {
                this.AppendInt64(x__995);
            }
            this.AppendList(values__993, (S::Action<long>) fn__7154);
        }
        public void AppendString(string value__997)
        {
            SqlString t___7151 = new SqlString(value__997);
            C::Listed.Add(this.buffer__947, t___7151);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1000)
        {
            void fn__7148(string x__1002)
            {
                this.AppendString(x__1002);
            }
            this.AppendList(values__1000, (S::Action<string>) fn__7148);
        }
        void AppendList<T__198>(G::IReadOnlyList<T__198> values__1004, S::Action<T__198> appendValue__1005)
        {
            int t___7143;
            T__198 t___7145;
            int i__1007 = 0;
            while (true)
            {
                t___7143 = values__1004.Count;
                if (!(i__1007 < t___7143)) break;
                if (i__1007 > 0) this.AppendSafe(", ");
                t___7145 = values__1004[i__1007];
                appendValue__1005(t___7145);
                i__1007 = i__1007 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__947));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___7140 = new G::List<ISqlPart>();
            this.buffer__947 = t___7140;
        }
    }
}
