using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1085;
        public void AppendSafe(string sqlSource__1087)
        {
            SqlSource t___8767 = new SqlSource(sqlSource__1087);
            C::Listed.Add(this.buffer__1085, t___8767);
        }
        public void AppendFragment(SqlFragment fragment__1090)
        {
            G::IReadOnlyList<ISqlPart> t___8765 = fragment__1090.Parts;
            C::Listed.AddAll(this.buffer__1085, t___8765);
        }
        public void AppendPart(ISqlPart part__1093)
        {
            C::Listed.Add(this.buffer__1085, part__1093);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1096)
        {
            void fn__8761(ISqlPart x__1098)
            {
                this.AppendPart(x__1098);
            }
            this.AppendList(values__1096, (S::Action<ISqlPart>) fn__8761);
        }
        public void AppendBoolean(bool value__1100)
        {
            SqlBoolean t___8758 = new SqlBoolean(value__1100);
            C::Listed.Add(this.buffer__1085, t___8758);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__1103)
        {
            void fn__8755(bool x__1105)
            {
                this.AppendBoolean(x__1105);
            }
            this.AppendList(values__1103, (S::Action<bool>) fn__8755);
        }
        public void AppendDate(S::DateTime value__1107)
        {
            SqlDate t___8752 = new SqlDate(value__1107);
            C::Listed.Add(this.buffer__1085, t___8752);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__1110)
        {
            void fn__8749(S::DateTime x__1112)
            {
                this.AppendDate(x__1112);
            }
            this.AppendList(values__1110, (S::Action<S::DateTime>) fn__8749);
        }
        public void AppendFloat64(double value__1114)
        {
            SqlFloat64 t___8746 = new SqlFloat64(value__1114);
            C::Listed.Add(this.buffer__1085, t___8746);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__1117)
        {
            void fn__8743(double x__1119)
            {
                this.AppendFloat64(x__1119);
            }
            this.AppendList(values__1117, (S::Action<double>) fn__8743);
        }
        public void AppendInt32(int value__1121)
        {
            SqlInt32 t___8740 = new SqlInt32(value__1121);
            C::Listed.Add(this.buffer__1085, t___8740);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__1124)
        {
            void fn__8737(int x__1126)
            {
                this.AppendInt32(x__1126);
            }
            this.AppendList(values__1124, (S::Action<int>) fn__8737);
        }
        public void AppendInt64(long value__1128)
        {
            SqlInt64 t___8734 = new SqlInt64(value__1128);
            C::Listed.Add(this.buffer__1085, t___8734);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__1131)
        {
            void fn__8731(long x__1133)
            {
                this.AppendInt64(x__1133);
            }
            this.AppendList(values__1131, (S::Action<long>) fn__8731);
        }
        public void AppendString(string value__1135)
        {
            SqlString t___8728 = new SqlString(value__1135);
            C::Listed.Add(this.buffer__1085, t___8728);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1138)
        {
            void fn__8725(string x__1140)
            {
                this.AppendString(x__1140);
            }
            this.AppendList(values__1138, (S::Action<string>) fn__8725);
        }
        void AppendList<T__226>(G::IReadOnlyList<T__226> values__1142, S::Action<T__226> appendValue__1143)
        {
            int t___8720;
            T__226 t___8722;
            int i__1145 = 0;
            while (true)
            {
                t___8720 = values__1142.Count;
                if (!(i__1145 < t___8720)) break;
                if (i__1145 > 0) this.AppendSafe(", ");
                t___8722 = values__1142[i__1145];
                appendValue__1143(t___8722);
                i__1145 = i__1145 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1085));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___8717 = new G::List<ISqlPart>();
            this.buffer__1085 = t___8717;
        }
    }
}
