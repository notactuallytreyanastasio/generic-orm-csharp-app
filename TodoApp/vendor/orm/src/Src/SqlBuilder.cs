using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class SqlBuilder
    {
        readonly G::IList<ISqlPart> buffer__1302;
        public void AppendSafe(string sqlSource__1304)
        {
            SqlSource t___10895 = new SqlSource(sqlSource__1304);
            C::Listed.Add(this.buffer__1302, t___10895);
        }
        public void AppendFragment(SqlFragment fragment__1307)
        {
            G::IReadOnlyList<ISqlPart> t___10893 = fragment__1307.Parts;
            C::Listed.AddAll(this.buffer__1302, t___10893);
        }
        public void AppendPart(ISqlPart part__1310)
        {
            C::Listed.Add(this.buffer__1302, part__1310);
        }
        public void AppendPartList(G::IReadOnlyList<ISqlPart> values__1313)
        {
            void fn__10889(ISqlPart x__1315)
            {
                this.AppendPart(x__1315);
            }
            this.AppendList(values__1313, (S::Action<ISqlPart>) fn__10889);
        }
        public void AppendBoolean(bool value__1317)
        {
            SqlBoolean t___10886 = new SqlBoolean(value__1317);
            C::Listed.Add(this.buffer__1302, t___10886);
        }
        public void AppendBooleanList(G::IReadOnlyList<bool> values__1320)
        {
            void fn__10883(bool x__1322)
            {
                this.AppendBoolean(x__1322);
            }
            this.AppendList(values__1320, (S::Action<bool>) fn__10883);
        }
        public void AppendDate(S::DateTime value__1324)
        {
            SqlDate t___10880 = new SqlDate(value__1324);
            C::Listed.Add(this.buffer__1302, t___10880);
        }
        public void AppendDateList(G::IReadOnlyList<S::DateTime> values__1327)
        {
            void fn__10877(S::DateTime x__1329)
            {
                this.AppendDate(x__1329);
            }
            this.AppendList(values__1327, (S::Action<S::DateTime>) fn__10877);
        }
        public void AppendFloat64(double value__1331)
        {
            SqlFloat64 t___10874 = new SqlFloat64(value__1331);
            C::Listed.Add(this.buffer__1302, t___10874);
        }
        public void AppendFloat64_List(G::IReadOnlyList<double> values__1334)
        {
            void fn__10871(double x__1336)
            {
                this.AppendFloat64(x__1336);
            }
            this.AppendList(values__1334, (S::Action<double>) fn__10871);
        }
        public void AppendInt32(int value__1338)
        {
            SqlInt32 t___10868 = new SqlInt32(value__1338);
            C::Listed.Add(this.buffer__1302, t___10868);
        }
        public void AppendInt32_List(G::IReadOnlyList<int> values__1341)
        {
            void fn__10865(int x__1343)
            {
                this.AppendInt32(x__1343);
            }
            this.AppendList(values__1341, (S::Action<int>) fn__10865);
        }
        public void AppendInt64(long value__1345)
        {
            SqlInt64 t___10862 = new SqlInt64(value__1345);
            C::Listed.Add(this.buffer__1302, t___10862);
        }
        public void AppendInt64_List(G::IReadOnlyList<long> values__1348)
        {
            void fn__10859(long x__1350)
            {
                this.AppendInt64(x__1350);
            }
            this.AppendList(values__1348, (S::Action<long>) fn__10859);
        }
        public void AppendString(string value__1352)
        {
            SqlString t___10856 = new SqlString(value__1352);
            C::Listed.Add(this.buffer__1302, t___10856);
        }
        public void AppendStringList(G::IReadOnlyList<string> values__1355)
        {
            void fn__10853(string x__1357)
            {
                this.AppendString(x__1357);
            }
            this.AppendList(values__1355, (S::Action<string>) fn__10853);
        }
        void AppendList<T__259>(G::IReadOnlyList<T__259> values__1359, S::Action<T__259> appendValue__1360)
        {
            int t___10848;
            T__259 t___10850;
            int i__1362 = 0;
            while (true)
            {
                t___10848 = values__1359.Count;
                if (!(i__1362 < t___10848)) break;
                if (i__1362 > 0) this.AppendSafe(", ");
                t___10850 = values__1359[i__1362];
                appendValue__1360(t___10850);
                i__1362 = i__1362 + 1;
            }
        }
        public SqlFragment Accumulated
        {
            get
            {
                return new SqlFragment(C::Listed.ToReadOnlyList(this.buffer__1302));
            }
        }
        public SqlBuilder()
        {
            G::IList<ISqlPart> t___10845 = new G::List<ISqlPart>();
            this.buffer__1302 = t___10845;
        }
    }
}
