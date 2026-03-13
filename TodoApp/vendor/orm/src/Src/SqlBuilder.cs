using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__791;
        public void AppendSafe(string sqlSource__793)
        {
            SqlSource t___5758 = new SqlSource(sqlSource__793);
            C::Listed.Add(this.buffer__791, t___5758);
        }
        public void AppendFragment(SqlFragment fragment__796)
        {
            G::IReadOnlyList<ISqlPart> t___5756 = fragment__796.Parts;
            C::Listed.AddAll(this.buffer__791, t___5756);
        }
        public void AppendPart(ISqlPart part__799)
        {
            C::Listed.Add(this.buffer__791, part__799);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__802)
        {
            void fn__5752(ISqlPart x__804)
            {
                this.AppendPart(x__804);
            }
            this.AppendList(values__802, (S::Action<ISqlPart>) fn__5752);
        }
        public void AppendBoolean(bool value__806)
        {
            SqlBoolean t___5749 = new SqlBoolean(value__806);
            C::Listed.Add(this.buffer__791, t___5749);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__809)
        {
            void fn__5746(bool x__811)
            {
                this.AppendBoolean(x__811);
            }
            this.AppendList(values__809, (S::Action<bool>) fn__5746);
        }
        public void AppendDate(S::DateTime value__813)
        {
            SqlDate t___5743 = new SqlDate(value__813);
            C::Listed.Add(this.buffer__791, t___5743);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__816)
        {
            void fn__5740(S::DateTime x__818)
            {
                this.AppendDate(x__818);
            }
            this.AppendList(values__816, (S::Action<S::DateTime>) fn__5740);
        }
        public void AppendFloat64(double value__820)
        {
            SqlFloat64 t___5737 = new SqlFloat64(value__820);
            C::Listed.Add(this.buffer__791, t___5737);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__823)
        {
            void fn__5734(double x__825)
            {
                this.AppendFloat64(x__825);
            }
            this.AppendList(values__823, (S::Action<double>) fn__5734);
        }
        public void AppendInt32(int value__827)
        {
            SqlInt32 t___5731 = new SqlInt32(value__827);
            C::Listed.Add(this.buffer__791, t___5731);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__830)
        {
            void fn__5728(int x__832)
            {
                this.AppendInt32(x__832);
            }
            this.AppendList(values__830, (S::Action<int>) fn__5728);
        }
        public void AppendInt64(long value__834)
        {
            SqlInt64 t___5725 = new SqlInt64(value__834);
            C::Listed.Add(this.buffer__791, t___5725);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__837)
        {
            void fn__5722(long x__839)
            {
                this.AppendInt64(x__839);
            }
            this.AppendList(values__837, (S::Action<long>) fn__5722);
        }
        public void AppendString(string value__841)
        {
            SqlString t___5719 = new SqlString(value__841);
            C::Listed.Add(this.buffer__791, t___5719);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__844)
        {
            void fn__5716(string x__846)
            {
                this.AppendString(x__846);
            }
            this.AppendList(values__844, (S::Action<string>) fn__5716);
        }
        void AppendList<T__163>(G::IReadOnlyList<T__163> values__848, S::Action<T__163> appendValue__849)
        {
            int t___5711;
            T__163 t___5713;
            int i__851 = 0;
            while (true)
            {
                t___5711 = values__848.Count;
                if (!(i__851 < t___5711)) break;
                if (i__851 > 0) this.AppendSafe(", ");
                t___5713 = values__848[i__851];
                appendValue__849(t___5713);
                i__851 = i__851 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__791));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___5708 = new G::List<ISqlPart>();
            this.buffer__791 = t___5708;
        }
    }
}
