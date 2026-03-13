using U = Microsoft.VisualStudio.TestTools.UnitTesting;
using S0 = Orm.Src;
using S1 = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
using T = TemperLang.Std.Testing;
namespace Orm.Src
{
    [U::TestClass]
    public class SrcTests
    {
        internal static ISafeIdentifier csid__660(string name__953)
        {
            ISafeIdentifier t___8354;
            t___8354 = S0::SrcGlobal.SafeIdentifier(name__953);
            return t___8354;
        }
        internal static TableDef userTable__661()
        {
            return new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("name"), new StringField(), false, null, false), new FieldDef(csid__660("email"), new StringField(), false, null, false), new FieldDef(csid__660("age"), new IntField(), true, null, false), new FieldDef(csid__660("score"), new FloatField(), true, null, false), new FieldDef(csid__660("active"), new BoolField(), true, null, false)), null);
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__2120()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__957 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___15072 = userTable__661();
                ISafeIdentifier t___15073 = csid__660("name");
                ISafeIdentifier t___15074 = csid__660("email");
                IChangeset cs__958 = S0::SrcGlobal.Changeset(t___15072, params__957).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15073, t___15074));
                bool t___15077 = C::Mapped.ContainsKey(cs__958.Changes, "name");
                string fn__15067()
                {
                    return "name should be in changes";
                }
                test___32.Assert(t___15077, (S1::Func<string>) fn__15067);
                bool t___15081 = C::Mapped.ContainsKey(cs__958.Changes, "email");
                string fn__15066()
                {
                    return "email should be in changes";
                }
                test___32.Assert(t___15081, (S1::Func<string>) fn__15066);
                bool t___15087 = !C::Mapped.ContainsKey(cs__958.Changes, "admin");
                string fn__15065()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___32.Assert(t___15087, (S1::Func<string>) fn__15065);
                bool t___15089 = cs__958.IsValid;
                string fn__15064()
                {
                    return "should still be valid";
                }
                test___32.Assert(t___15089, (S1::Func<string>) fn__15064);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__2121()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__960 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___15050 = userTable__661();
                ISafeIdentifier t___15051 = csid__660("name");
                IChangeset cs__961 = S0::SrcGlobal.Changeset(t___15050, params__960).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15051)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("email")));
                bool t___15058 = !C::Mapped.ContainsKey(cs__961.Changes, "name");
                string fn__15046()
                {
                    return "name must be excluded by second cast";
                }
                test___33.Assert(t___15058, (S1::Func<string>) fn__15046);
                bool t___15061 = C::Mapped.ContainsKey(cs__961.Changes, "email");
                string fn__15045()
                {
                    return "email should be present";
                }
                test___33.Assert(t___15061, (S1::Func<string>) fn__15045);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__2122()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__963 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___15032 = userTable__661();
                ISafeIdentifier t___15033 = csid__660("name");
                ISafeIdentifier t___15034 = csid__660("email");
                IChangeset cs__964 = S0::SrcGlobal.Changeset(t___15032, params__963).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15033, t___15034));
                bool t___15039 = !C::Mapped.ContainsKey(cs__964.Changes, "name");
                string fn__15028()
                {
                    return "empty name should not be in changes";
                }
                test___34.Assert(t___15039, (S1::Func<string>) fn__15028);
                bool t___15042 = C::Mapped.ContainsKey(cs__964.Changes, "email");
                string fn__15027()
                {
                    return "email should be in changes";
                }
                test___34.Assert(t___15042, (S1::Func<string>) fn__15027);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__2123()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__966 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___15014 = userTable__661();
                ISafeIdentifier t___15015 = csid__660("name");
                IChangeset cs__967 = S0::SrcGlobal.Changeset(t___15014, params__966).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15015)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name")));
                bool t___15019 = cs__967.IsValid;
                string fn__15011()
                {
                    return "should be valid";
                }
                test___35.Assert(t___15019, (S1::Func<string>) fn__15011);
                bool t___15025 = cs__967.Errors.Count == 0;
                string fn__15010()
                {
                    return "no errors expected";
                }
                test___35.Assert(t___15025, (S1::Func<string>) fn__15010);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__2124()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__969 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14990 = userTable__661();
                ISafeIdentifier t___14991 = csid__660("name");
                IChangeset cs__970 = S0::SrcGlobal.Changeset(t___14990, params__969).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14991)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name")));
                bool t___14997 = !cs__970.IsValid;
                string fn__14988()
                {
                    return "should be invalid";
                }
                test___36.Assert(t___14997, (S1::Func<string>) fn__14988);
                bool t___15002 = cs__970.Errors.Count == 1;
                string fn__14987()
                {
                    return "should have one error";
                }
                test___36.Assert(t___15002, (S1::Func<string>) fn__14987);
                bool t___15008 = cs__970.Errors[0].Field == "name";
                string fn__14986()
                {
                    return "error should name the field";
                }
                test___36.Assert(t___15008, (S1::Func<string>) fn__14986);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__2125()
        {
            T::Test test___37 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__972 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14978 = userTable__661();
                ISafeIdentifier t___14979 = csid__660("name");
                IChangeset cs__973 = S0::SrcGlobal.Changeset(t___14978, params__972).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14979)).ValidateLength(csid__660("name"), 2, 50);
                bool t___14983 = cs__973.IsValid;
                string fn__14975()
                {
                    return "should be valid";
                }
                test___37.Assert(t___14983, (S1::Func<string>) fn__14975);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__2126()
        {
            T::Test test___38 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__975 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___14966 = userTable__661();
                ISafeIdentifier t___14967 = csid__660("name");
                IChangeset cs__976 = S0::SrcGlobal.Changeset(t___14966, params__975).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14967)).ValidateLength(csid__660("name"), 2, 50);
                bool t___14973 = !cs__976.IsValid;
                string fn__14963()
                {
                    return "should be invalid";
                }
                test___38.Assert(t___14973, (S1::Func<string>) fn__14963);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__2127()
        {
            T::Test test___39 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__978 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___14954 = userTable__661();
                ISafeIdentifier t___14955 = csid__660("name");
                IChangeset cs__979 = S0::SrcGlobal.Changeset(t___14954, params__978).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14955)).ValidateLength(csid__660("name"), 2, 10);
                bool t___14961 = !cs__979.IsValid;
                string fn__14951()
                {
                    return "should be invalid";
                }
                test___39.Assert(t___14961, (S1::Func<string>) fn__14951);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__2128()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__981 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___14943 = userTable__661();
                ISafeIdentifier t___14944 = csid__660("age");
                IChangeset cs__982 = S0::SrcGlobal.Changeset(t___14943, params__981).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14944)).ValidateInt(csid__660("age"));
                bool t___14948 = cs__982.IsValid;
                string fn__14940()
                {
                    return "should be valid";
                }
                test___40.Assert(t___14948, (S1::Func<string>) fn__14940);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__2129()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__984 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___14931 = userTable__661();
                ISafeIdentifier t___14932 = csid__660("age");
                IChangeset cs__985 = S0::SrcGlobal.Changeset(t___14931, params__984).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14932)).ValidateInt(csid__660("age"));
                bool t___14938 = !cs__985.IsValid;
                string fn__14928()
                {
                    return "should be invalid";
                }
                test___41.Assert(t___14938, (S1::Func<string>) fn__14928);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__2130()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__987 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___14920 = userTable__661();
                ISafeIdentifier t___14921 = csid__660("score");
                IChangeset cs__988 = S0::SrcGlobal.Changeset(t___14920, params__987).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14921)).ValidateFloat(csid__660("score"));
                bool t___14925 = cs__988.IsValid;
                string fn__14917()
                {
                    return "should be valid";
                }
                test___42.Assert(t___14925, (S1::Func<string>) fn__14917);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__2131()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__990 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___14909 = userTable__661();
                ISafeIdentifier t___14910 = csid__660("age");
                IChangeset cs__991 = S0::SrcGlobal.Changeset(t___14909, params__990).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14910)).ValidateInt64(csid__660("age"));
                bool t___14914 = cs__991.IsValid;
                string fn__14906()
                {
                    return "should be valid";
                }
                test___43.Assert(t___14914, (S1::Func<string>) fn__14906);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__2132()
        {
            T::Test test___44 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__993 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___14897 = userTable__661();
                ISafeIdentifier t___14898 = csid__660("age");
                IChangeset cs__994 = S0::SrcGlobal.Changeset(t___14897, params__993).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14898)).ValidateInt64(csid__660("age"));
                bool t___14904 = !cs__994.IsValid;
                string fn__14894()
                {
                    return "should be invalid";
                }
                test___44.Assert(t___14904, (S1::Func<string>) fn__14894);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__2133()
        {
            T::Test test___45 = new T::Test();
            try
            {
                void fn__14891(string v__996)
                {
                    G::IReadOnlyDictionary<string, string> params__997 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__996)));
                    TableDef t___14883 = userTable__661();
                    ISafeIdentifier t___14884 = csid__660("active");
                    IChangeset cs__998 = S0::SrcGlobal.Changeset(t___14883, params__997).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14884)).ValidateBool(csid__660("active"));
                    bool t___14888 = cs__998.IsValid;
                    string fn__14880()
                    {
                        return "should accept: " + v__996;
                    }
                    test___45.Assert(t___14888, (S1::Func<string>) fn__14880);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__14891);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__2134()
        {
            T::Test test___46 = new T::Test();
            try
            {
                void fn__14877(string v__1000)
                {
                    G::IReadOnlyDictionary<string, string> params__1001 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1000)));
                    TableDef t___14869 = userTable__661();
                    ISafeIdentifier t___14870 = csid__660("active");
                    IChangeset cs__1002 = S0::SrcGlobal.Changeset(t___14869, params__1001).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14870)).ValidateBool(csid__660("active"));
                    bool t___14874 = cs__1002.IsValid;
                    string fn__14866()
                    {
                        return "should accept: " + v__1000;
                    }
                    test___46.Assert(t___14874, (S1::Func<string>) fn__14866);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__14877);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__2135()
        {
            T::Test test___47 = new T::Test();
            try
            {
                void fn__14863(string v__1004)
                {
                    G::IReadOnlyDictionary<string, string> params__1005 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1004)));
                    TableDef t___14854 = userTable__661();
                    ISafeIdentifier t___14855 = csid__660("active");
                    IChangeset cs__1006 = S0::SrcGlobal.Changeset(t___14854, params__1005).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14855)).ValidateBool(csid__660("active"));
                    bool t___14861 = !cs__1006.IsValid;
                    string fn__14851()
                    {
                        return "should reject ambiguous: " + v__1004;
                    }
                    test___47.Assert(t___14861, (S1::Func<string>) fn__14851);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__14863);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__2136()
        {
            T::Test test___48 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1008 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___14839 = userTable__661();
                ISafeIdentifier t___14840 = csid__660("name");
                ISafeIdentifier t___14841 = csid__660("email");
                IChangeset cs__1009 = S0::SrcGlobal.Changeset(t___14839, params__1008).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14840, t___14841)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name"), csid__660("email")));
                SqlFragment t___8155;
                t___8155 = cs__1009.ToInsertSql();
                SqlFragment sqlFrag__1010 = t___8155;
                string s__1011 = sqlFrag__1010.ToString();
                bool t___14848 = s__1011.IndexOf("''") >= 0;
                string fn__14835()
                {
                    return "single quote must be doubled: " + s__1011;
                }
                test___48.Assert(t___14848, (S1::Func<string>) fn__14835);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__2137()
        {
            T::Test test___49 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1013 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___14819 = userTable__661();
                ISafeIdentifier t___14820 = csid__660("name");
                ISafeIdentifier t___14821 = csid__660("email");
                IChangeset cs__1014 = S0::SrcGlobal.Changeset(t___14819, params__1013).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14820, t___14821)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name"), csid__660("email")));
                SqlFragment t___8134;
                t___8134 = cs__1014.ToInsertSql();
                SqlFragment sqlFrag__1015 = t___8134;
                string s__1016 = sqlFrag__1015.ToString();
                bool t___14828 = s__1016.IndexOf("INSERT INTO users") >= 0;
                string fn__14815()
                {
                    return "has INSERT INTO: " + s__1016;
                }
                test___49.Assert(t___14828, (S1::Func<string>) fn__14815);
                bool t___14832 = s__1016.IndexOf("'Alice'") >= 0;
                string fn__14814()
                {
                    return "has quoted name: " + s__1016;
                }
                test___49.Assert(t___14832, (S1::Func<string>) fn__14814);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__2138()
        {
            T::Test test___50 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1018 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___14801 = userTable__661();
                ISafeIdentifier t___14802 = csid__660("name");
                ISafeIdentifier t___14803 = csid__660("email");
                ISafeIdentifier t___14804 = csid__660("age");
                IChangeset cs__1019 = S0::SrcGlobal.Changeset(t___14801, params__1018).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14802, t___14803, t___14804)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name"), csid__660("email")));
                SqlFragment t___8117;
                t___8117 = cs__1019.ToInsertSql();
                SqlFragment sqlFrag__1020 = t___8117;
                string s__1021 = sqlFrag__1020.ToString();
                bool t___14811 = s__1021.IndexOf("25") >= 0;
                string fn__14796()
                {
                    return "age rendered unquoted: " + s__1021;
                }
                test___50.Assert(t___14811, (S1::Func<string>) fn__14796);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__2139()
        {
            T::Test test___51 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1023 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14789 = userTable__661();
                ISafeIdentifier t___14790 = csid__660("name");
                IChangeset cs__1024 = S0::SrcGlobal.Changeset(t___14789, params__1023).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14790)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name")));
                bool didBubble__1025;
                try
                {
                    cs__1024.ToInsertSql();
                    didBubble__1025 = false;
                }
                catch
                {
                    didBubble__1025 = true;
                }
                string fn__14787()
                {
                    return "invalid changeset should bubble";
                }
                test___51.Assert(didBubble__1025, (S1::Func<string>) fn__14787);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__2140()
        {
            T::Test test___52 = new T::Test();
            try
            {
                TableDef strictTable__1027 = new TableDef(csid__660("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("title"), new StringField(), false, null, false), new FieldDef(csid__660("body"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1028 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___14780 = csid__660("body");
                IChangeset cs__1029 = S0::SrcGlobal.Changeset(strictTable__1027, params__1028).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14780));
                bool t___14782 = cs__1029.IsValid;
                string fn__14769()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___52.Assert(t___14782, (S1::Func<string>) fn__14769);
                bool didBubble__1030;
                try
                {
                    cs__1029.ToInsertSql();
                    didBubble__1030 = false;
                }
                catch
                {
                    didBubble__1030 = true;
                }
                string fn__14768()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___52.Assert(didBubble__1030, (S1::Func<string>) fn__14768);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__2141()
        {
            T::Test test___53 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1032 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___14759 = userTable__661();
                ISafeIdentifier t___14760 = csid__660("name");
                IChangeset cs__1033 = S0::SrcGlobal.Changeset(t___14759, params__1032).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14760)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name")));
                SqlFragment t___8077;
                t___8077 = cs__1033.ToUpdateSql(42);
                SqlFragment sqlFrag__1034 = t___8077;
                string s__1035 = sqlFrag__1034.ToString();
                bool t___14766 = s__1035 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__14756()
                {
                    return "got: " + s__1035;
                }
                test___53.Assert(t___14766, (S1::Func<string>) fn__14756);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__2142()
        {
            T::Test test___54 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1037 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14749 = userTable__661();
                ISafeIdentifier t___14750 = csid__660("name");
                IChangeset cs__1038 = S0::SrcGlobal.Changeset(t___14749, params__1037).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14750)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__660("name")));
                bool didBubble__1039;
                try
                {
                    cs__1038.ToUpdateSql(1);
                    didBubble__1039 = false;
                }
                catch
                {
                    didBubble__1039 = true;
                }
                string fn__14747()
                {
                    return "invalid changeset should bubble";
                }
                test___54.Assert(didBubble__1039, (S1::Func<string>) fn__14747);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeAddsANewField__2143()
        {
            T::Test test___55 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1041 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14733 = userTable__661();
                ISafeIdentifier t___14734 = csid__660("name");
                IChangeset cs__1042 = S0::SrcGlobal.Changeset(t___14733, params__1041).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14734)).PutChange(csid__660("email"), "alice@example.com");
                bool t___14739 = C::Mapped.ContainsKey(cs__1042.Changes, "email");
                string fn__14730()
                {
                    return "email should be in changes";
                }
                test___55.Assert(t___14739, (S1::Func<string>) fn__14730);
                bool t___14745 = C::Mapped.GetOrDefault(cs__1042.Changes, "email", "") == "alice@example.com";
                string fn__14729()
                {
                    return "email value";
                }
                test___55.Assert(t___14745, (S1::Func<string>) fn__14729);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeOverwritesExistingField__2144()
        {
            T::Test test___56 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1044 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14719 = userTable__661();
                ISafeIdentifier t___14720 = csid__660("name");
                IChangeset cs__1045 = S0::SrcGlobal.Changeset(t___14719, params__1044).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14720)).PutChange(csid__660("name"), "Bob");
                bool t___14727 = C::Mapped.GetOrDefault(cs__1045.Changes, "name", "") == "Bob";
                string fn__14716()
                {
                    return "name should be overwritten";
                }
                test___56.Assert(t___14727, (S1::Func<string>) fn__14716);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeValueAppearsInToInsertSql__2145()
        {
            T::Test test___57 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1047 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___14705 = userTable__661();
                ISafeIdentifier t___14706 = csid__660("name");
                ISafeIdentifier t___14707 = csid__660("email");
                IChangeset cs__1048 = S0::SrcGlobal.Changeset(t___14705, params__1047).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14706, t___14707)).PutChange(csid__660("name"), "Bob");
                SqlFragment t___8032;
                t___8032 = cs__1048.ToInsertSql();
                SqlFragment t___8033 = t___8032;
                string s__1049 = t___8033.ToString();
                bool t___14713 = s__1049.IndexOf("'Bob'") >= 0;
                string fn__14701()
                {
                    return "should use putChange value: " + s__1049;
                }
                test___57.Assert(t___14713, (S1::Func<string>) fn__14701);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void getChangeReturnsValueForExistingField__2146()
        {
            T::Test test___58 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1051 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14694 = userTable__661();
                ISafeIdentifier t___14695 = csid__660("name");
                IChangeset cs__1052 = S0::SrcGlobal.Changeset(t___14694, params__1051).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14695));
                string t___8020;
                t___8020 = cs__1052.GetChange(csid__660("name"));
                string val__1053 = t___8020;
                bool t___14699 = val__1053 == "Alice";
                string fn__14691()
                {
                    return "should return Alice";
                }
                test___58.Assert(t___14699, (S1::Func<string>) fn__14691);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void getChangeBubblesOnMissingField__2147()
        {
            T::Test test___59 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1055 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14685 = userTable__661();
                ISafeIdentifier t___14686 = csid__660("name");
                IChangeset cs__1056 = S0::SrcGlobal.Changeset(t___14685, params__1055).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14686));
                bool didBubble__1057;
                try
                {
                    cs__1056.GetChange(csid__660("email"));
                    didBubble__1057 = false;
                }
                catch
                {
                    didBubble__1057 = true;
                }
                string fn__14682()
                {
                    return "should bubble for missing field";
                }
                test___59.Assert(didBubble__1057, (S1::Func<string>) fn__14682);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeRemovesField__2148()
        {
            T::Test test___60 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1059 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___14667 = userTable__661();
                ISafeIdentifier t___14668 = csid__660("name");
                ISafeIdentifier t___14669 = csid__660("email");
                IChangeset cs__1060 = S0::SrcGlobal.Changeset(t___14667, params__1059).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14668, t___14669)).DeleteChange(csid__660("email"));
                bool t___14676 = !C::Mapped.ContainsKey(cs__1060.Changes, "email");
                string fn__14663()
                {
                    return "email should be removed";
                }
                test___60.Assert(t___14676, (S1::Func<string>) fn__14663);
                bool t___14679 = C::Mapped.ContainsKey(cs__1060.Changes, "name");
                string fn__14662()
                {
                    return "name should remain";
                }
                test___60.Assert(t___14679, (S1::Func<string>) fn__14662);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeOnNonexistentFieldIsNoOp__2149()
        {
            T::Test test___61 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1062 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14650 = userTable__661();
                ISafeIdentifier t___14651 = csid__660("name");
                IChangeset cs__1063 = S0::SrcGlobal.Changeset(t___14650, params__1062).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14651)).DeleteChange(csid__660("email"));
                bool t___14656 = C::Mapped.ContainsKey(cs__1063.Changes, "name");
                string fn__14647()
                {
                    return "name should still be present";
                }
                test___61.Assert(t___14656, (S1::Func<string>) fn__14647);
                bool t___14659 = cs__1063.IsValid;
                string fn__14646()
                {
                    return "should still be valid";
                }
                test___61.Assert(t___14659, (S1::Func<string>) fn__14646);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionPassesWhenValueInList__2150()
        {
            T::Test test___62 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1065 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "admin")));
                TableDef t___14638 = userTable__661();
                ISafeIdentifier t___14639 = csid__660("name");
                IChangeset cs__1066 = S0::SrcGlobal.Changeset(t___14638, params__1065).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14639)).ValidateInclusion(csid__660("name"), C::Listed.CreateReadOnlyList<string>("admin", "user", "guest"));
                bool t___14643 = cs__1066.IsValid;
                string fn__14635()
                {
                    return "should be valid";
                }
                test___62.Assert(t___14643, (S1::Func<string>) fn__14635);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionFailsWhenValueNotInList__2151()
        {
            T::Test test___63 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1068 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "hacker")));
                TableDef t___14620 = userTable__661();
                ISafeIdentifier t___14621 = csid__660("name");
                IChangeset cs__1069 = S0::SrcGlobal.Changeset(t___14620, params__1068).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14621)).ValidateInclusion(csid__660("name"), C::Listed.CreateReadOnlyList<string>("admin", "user", "guest"));
                bool t___14627 = !cs__1069.IsValid;
                string fn__14617()
                {
                    return "should be invalid";
                }
                test___63.Assert(t___14627, (S1::Func<string>) fn__14617);
                bool t___14633 = cs__1069.Errors[0].Field == "name";
                string fn__14616()
                {
                    return "error on name";
                }
                test___63.Assert(t___14633, (S1::Func<string>) fn__14616);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionSkipsWhenFieldNotInChanges__2152()
        {
            T::Test test___64 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1071 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14608 = userTable__661();
                ISafeIdentifier t___14609 = csid__660("name");
                IChangeset cs__1072 = S0::SrcGlobal.Changeset(t___14608, params__1071).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14609)).ValidateInclusion(csid__660("name"), C::Listed.CreateReadOnlyList<string>("admin", "user"));
                bool t___14613 = cs__1072.IsValid;
                string fn__14606()
                {
                    return "should be valid when field absent";
                }
                test___64.Assert(t___14613, (S1::Func<string>) fn__14606);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionPassesWhenValueNotInList__2153()
        {
            T::Test test___65 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1074 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___14598 = userTable__661();
                ISafeIdentifier t___14599 = csid__660("name");
                IChangeset cs__1075 = S0::SrcGlobal.Changeset(t___14598, params__1074).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14599)).ValidateExclusion(csid__660("name"), C::Listed.CreateReadOnlyList<string>("root", "admin", "superuser"));
                bool t___14603 = cs__1075.IsValid;
                string fn__14595()
                {
                    return "should be valid";
                }
                test___65.Assert(t___14603, (S1::Func<string>) fn__14595);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionFailsWhenValueInList__2154()
        {
            T::Test test___66 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1077 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "admin")));
                TableDef t___14580 = userTable__661();
                ISafeIdentifier t___14581 = csid__660("name");
                IChangeset cs__1078 = S0::SrcGlobal.Changeset(t___14580, params__1077).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14581)).ValidateExclusion(csid__660("name"), C::Listed.CreateReadOnlyList<string>("root", "admin", "superuser"));
                bool t___14587 = !cs__1078.IsValid;
                string fn__14577()
                {
                    return "should be invalid";
                }
                test___66.Assert(t___14587, (S1::Func<string>) fn__14577);
                bool t___14593 = cs__1078.Errors[0].Field == "name";
                string fn__14576()
                {
                    return "error on name";
                }
                test___66.Assert(t___14593, (S1::Func<string>) fn__14576);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionSkipsWhenFieldNotInChanges__2155()
        {
            T::Test test___67 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1080 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14568 = userTable__661();
                ISafeIdentifier t___14569 = csid__660("name");
                IChangeset cs__1081 = S0::SrcGlobal.Changeset(t___14568, params__1080).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14569)).ValidateExclusion(csid__660("name"), C::Listed.CreateReadOnlyList<string>("root", "admin"));
                bool t___14573 = cs__1081.IsValid;
                string fn__14566()
                {
                    return "should be valid when field absent";
                }
                test___67.Assert(t___14573, (S1::Func<string>) fn__14566);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanPasses__2156()
        {
            T::Test test___68 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1083 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___14557 = userTable__661();
                ISafeIdentifier t___14558 = csid__660("age");
                IChangeset cs__1084 = S0::SrcGlobal.Changeset(t___14557, params__1083).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14558)).ValidateNumber(csid__660("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___14563 = cs__1084.IsValid;
                string fn__14554()
                {
                    return "25 > 18 should pass";
                }
                test___68.Assert(t___14563, (S1::Func<string>) fn__14554);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanFails__2157()
        {
            T::Test test___69 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1086 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "15")));
                TableDef t___14544 = userTable__661();
                ISafeIdentifier t___14545 = csid__660("age");
                IChangeset cs__1087 = S0::SrcGlobal.Changeset(t___14544, params__1086).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14545)).ValidateNumber(csid__660("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___14552 = !cs__1087.IsValid;
                string fn__14541()
                {
                    return "15 > 18 should fail";
                }
                test___69.Assert(t___14552, (S1::Func<string>) fn__14541);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanPasses__2158()
        {
            T::Test test___70 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1089 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "8.5")));
                TableDef t___14532 = userTable__661();
                ISafeIdentifier t___14533 = csid__660("score");
                IChangeset cs__1090 = S0::SrcGlobal.Changeset(t___14532, params__1089).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14533)).ValidateNumber(csid__660("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___14538 = cs__1090.IsValid;
                string fn__14529()
                {
                    return "8.5 < 10 should pass";
                }
                test___70.Assert(t___14538, (S1::Func<string>) fn__14529);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanFails__2159()
        {
            T::Test test___71 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1092 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "12.0")));
                TableDef t___14519 = userTable__661();
                ISafeIdentifier t___14520 = csid__660("score");
                IChangeset cs__1093 = S0::SrcGlobal.Changeset(t___14519, params__1092).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14520)).ValidateNumber(csid__660("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___14527 = !cs__1093.IsValid;
                string fn__14516()
                {
                    return "12 < 10 should fail";
                }
                test___71.Assert(t___14527, (S1::Func<string>) fn__14516);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanOrEqualBoundary__2160()
        {
            T::Test test___72 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1095 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "18")));
                TableDef t___14507 = userTable__661();
                ISafeIdentifier t___14508 = csid__660("age");
                IChangeset cs__1096 = S0::SrcGlobal.Changeset(t___14507, params__1095).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14508)).ValidateNumber(csid__660("age"), new NumberValidationOpts(null, null, 18.0, null, null));
                bool t___14513 = cs__1096.IsValid;
                string fn__14504()
                {
                    return "18 >= 18 should pass";
                }
                test___72.Assert(t___14513, (S1::Func<string>) fn__14504);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberCombinedOptions__2161()
        {
            T::Test test___73 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1098 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "5.0")));
                TableDef t___14495 = userTable__661();
                ISafeIdentifier t___14496 = csid__660("score");
                IChangeset cs__1099 = S0::SrcGlobal.Changeset(t___14495, params__1098).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14496)).ValidateNumber(csid__660("score"), new NumberValidationOpts(0.0, 10.0, null, null, null));
                bool t___14501 = cs__1099.IsValid;
                string fn__14492()
                {
                    return "5 > 0 and < 10 should pass";
                }
                test___73.Assert(t___14501, (S1::Func<string>) fn__14492);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberNonNumericValue__2162()
        {
            T::Test test___74 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1101 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "abc")));
                TableDef t___14476 = userTable__661();
                ISafeIdentifier t___14477 = csid__660("age");
                IChangeset cs__1102 = S0::SrcGlobal.Changeset(t___14476, params__1101).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14477)).ValidateNumber(csid__660("age"), new NumberValidationOpts(0.0, null, null, null, null));
                bool t___14484 = !cs__1102.IsValid;
                string fn__14473()
                {
                    return "non-numeric should fail";
                }
                test___74.Assert(t___14484, (S1::Func<string>) fn__14473);
                bool t___14490 = cs__1102.Errors[0].Message == "must be a number";
                string fn__14472()
                {
                    return "correct error message";
                }
                test___74.Assert(t___14490, (S1::Func<string>) fn__14472);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberSkipsWhenFieldNotInChanges__2163()
        {
            T::Test test___75 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1104 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14463 = userTable__661();
                ISafeIdentifier t___14464 = csid__660("age");
                IChangeset cs__1105 = S0::SrcGlobal.Changeset(t___14463, params__1104).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14464)).ValidateNumber(csid__660("age"), new NumberValidationOpts(0.0, null, null, null, null));
                bool t___14469 = cs__1105.IsValid;
                string fn__14461()
                {
                    return "should be valid when field absent";
                }
                test___75.Assert(t___14469, (S1::Func<string>) fn__14461);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptancePassesForTrueValues__2164()
        {
            T::Test test___76 = new T::Test();
            try
            {
                void fn__14458(string v__1107)
                {
                    G::IReadOnlyDictionary<string, string> params__1108 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1107)));
                    TableDef t___14450 = userTable__661();
                    ISafeIdentifier t___14451 = csid__660("active");
                    IChangeset cs__1109 = S0::SrcGlobal.Changeset(t___14450, params__1108).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14451)).ValidateAcceptance(csid__660("active"));
                    bool t___14455 = cs__1109.IsValid;
                    string fn__14447()
                    {
                        return "should accept: " + v__1107;
                    }
                    test___76.Assert(t___14455, (S1::Func<string>) fn__14447);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__14458);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptanceFailsForNonTrueValues__2165()
        {
            T::Test test___77 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1111 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", "false")));
                TableDef t___14432 = userTable__661();
                ISafeIdentifier t___14433 = csid__660("active");
                IChangeset cs__1112 = S0::SrcGlobal.Changeset(t___14432, params__1111).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14433)).ValidateAcceptance(csid__660("active"));
                bool t___14439 = !cs__1112.IsValid;
                string fn__14429()
                {
                    return "false should not be accepted";
                }
                test___77.Assert(t___14439, (S1::Func<string>) fn__14429);
                bool t___14445 = cs__1112.Errors[0].Message == "must be accepted";
                string fn__14428()
                {
                    return "correct message";
                }
                test___77.Assert(t___14445, (S1::Func<string>) fn__14428);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationPassesWhenFieldsMatch__2166()
        {
            T::Test test___78 = new T::Test();
            try
            {
                TableDef tbl__1114 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("password"), new StringField(), false, null, false), new FieldDef(csid__660("password_confirmation"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1115 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123"), new G::KeyValuePair<string, string>("password_confirmation", "secret123")));
                ISafeIdentifier t___14419 = csid__660("password");
                ISafeIdentifier t___14420 = csid__660("password_confirmation");
                IChangeset cs__1116 = S0::SrcGlobal.Changeset(tbl__1114, params__1115).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14419, t___14420)).ValidateConfirmation(csid__660("password"), csid__660("password_confirmation"));
                bool t___14425 = cs__1116.IsValid;
                string fn__14407()
                {
                    return "matching fields should pass";
                }
                test___78.Assert(t___14425, (S1::Func<string>) fn__14407);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationFailsWhenFieldsDiffer__2167()
        {
            T::Test test___79 = new T::Test();
            try
            {
                TableDef tbl__1118 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("password"), new StringField(), false, null, false), new FieldDef(csid__660("password_confirmation"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1119 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123"), new G::KeyValuePair<string, string>("password_confirmation", "wrong456")));
                ISafeIdentifier t___14391 = csid__660("password");
                ISafeIdentifier t___14392 = csid__660("password_confirmation");
                IChangeset cs__1120 = S0::SrcGlobal.Changeset(tbl__1118, params__1119).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14391, t___14392)).ValidateConfirmation(csid__660("password"), csid__660("password_confirmation"));
                bool t___14399 = !cs__1120.IsValid;
                string fn__14379()
                {
                    return "mismatched fields should fail";
                }
                test___79.Assert(t___14399, (S1::Func<string>) fn__14379);
                bool t___14405 = cs__1120.Errors[0].Field == "password_confirmation";
                string fn__14378()
                {
                    return "error on confirmation field";
                }
                test___79.Assert(t___14405, (S1::Func<string>) fn__14378);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationFailsWhenConfirmationMissing__2168()
        {
            T::Test test___80 = new T::Test();
            try
            {
                TableDef tbl__1122 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("password"), new StringField(), false, null, false), new FieldDef(csid__660("password_confirmation"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1123 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123")));
                ISafeIdentifier t___14369 = csid__660("password");
                IChangeset cs__1124 = S0::SrcGlobal.Changeset(tbl__1122, params__1123).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14369)).ValidateConfirmation(csid__660("password"), csid__660("password_confirmation"));
                bool t___14376 = !cs__1124.IsValid;
                string fn__14358()
                {
                    return "missing confirmation should fail";
                }
                test___80.Assert(t___14376, (S1::Func<string>) fn__14358);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsPassesWhenSubstringFound__2169()
        {
            T::Test test___81 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1126 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___14350 = userTable__661();
                ISafeIdentifier t___14351 = csid__660("email");
                IChangeset cs__1127 = S0::SrcGlobal.Changeset(t___14350, params__1126).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14351)).ValidateContains(csid__660("email"), "@");
                bool t___14355 = cs__1127.IsValid;
                string fn__14347()
                {
                    return "should pass when @ present";
                }
                test___81.Assert(t___14355, (S1::Func<string>) fn__14347);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsFailsWhenSubstringNotFound__2170()
        {
            T::Test test___82 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1129 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice-example.com")));
                TableDef t___14338 = userTable__661();
                ISafeIdentifier t___14339 = csid__660("email");
                IChangeset cs__1130 = S0::SrcGlobal.Changeset(t___14338, params__1129).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14339)).ValidateContains(csid__660("email"), "@");
                bool t___14345 = !cs__1130.IsValid;
                string fn__14335()
                {
                    return "should fail when @ absent";
                }
                test___82.Assert(t___14345, (S1::Func<string>) fn__14335);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsSkipsWhenFieldNotInChanges__2171()
        {
            T::Test test___83 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1132 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___14327 = userTable__661();
                ISafeIdentifier t___14328 = csid__660("email");
                IChangeset cs__1133 = S0::SrcGlobal.Changeset(t___14327, params__1132).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14328)).ValidateContains(csid__660("email"), "@");
                bool t___14332 = cs__1133.IsValid;
                string fn__14325()
                {
                    return "should be valid when field absent";
                }
                test___83.Assert(t___14332, (S1::Func<string>) fn__14325);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithPasses__2172()
        {
            T::Test test___84 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1135 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Dr. Smith")));
                TableDef t___14317 = userTable__661();
                ISafeIdentifier t___14318 = csid__660("name");
                IChangeset cs__1136 = S0::SrcGlobal.Changeset(t___14317, params__1135).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14318)).ValidateStartsWith(csid__660("name"), "Dr.");
                bool t___14322 = cs__1136.IsValid;
                string fn__14314()
                {
                    return "should pass for Dr. prefix";
                }
                test___84.Assert(t___14322, (S1::Func<string>) fn__14314);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithFails__2173()
        {
            T::Test test___85 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1138 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Mr. Smith")));
                TableDef t___14305 = userTable__661();
                ISafeIdentifier t___14306 = csid__660("name");
                IChangeset cs__1139 = S0::SrcGlobal.Changeset(t___14305, params__1138).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14306)).ValidateStartsWith(csid__660("name"), "Dr.");
                bool t___14312 = !cs__1139.IsValid;
                string fn__14302()
                {
                    return "should fail for Mr. prefix";
                }
                test___85.Assert(t___14312, (S1::Func<string>) fn__14302);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithPasses__2174()
        {
            T::Test test___86 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1141 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___14294 = userTable__661();
                ISafeIdentifier t___14295 = csid__660("email");
                IChangeset cs__1142 = S0::SrcGlobal.Changeset(t___14294, params__1141).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14295)).ValidateEndsWith(csid__660("email"), ".com");
                bool t___14299 = cs__1142.IsValid;
                string fn__14291()
                {
                    return "should pass for .com suffix";
                }
                test___86.Assert(t___14299, (S1::Func<string>) fn__14291);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithFails__2175()
        {
            T::Test test___87 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1144 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.org")));
                TableDef t___14282 = userTable__661();
                ISafeIdentifier t___14283 = csid__660("email");
                IChangeset cs__1145 = S0::SrcGlobal.Changeset(t___14282, params__1144).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14283)).ValidateEndsWith(csid__660("email"), ".com");
                bool t___14289 = !cs__1145.IsValid;
                string fn__14279()
                {
                    return "should fail for .org when expecting .com";
                }
                test___87.Assert(t___14289, (S1::Func<string>) fn__14279);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithHandlesRepeatedSuffixCorrectly__2176()
        {
            T::Test test___88 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1147 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "abcabc")));
                TableDef t___14271 = userTable__661();
                ISafeIdentifier t___14272 = csid__660("name");
                IChangeset cs__1148 = S0::SrcGlobal.Changeset(t___14271, params__1147).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14272)).ValidateEndsWith(csid__660("name"), "abc");
                bool t___14276 = cs__1148.IsValid;
                string fn__14268()
                {
                    return "abcabc should end with abc";
                }
                test___88.Assert(t___14276, (S1::Func<string>) fn__14268);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlUsesDefaultValueWhenFieldNotInChanges__2177()
        {
            T::Test test___89 = new T::Test();
            try
            {
                TableDef tbl__1150 = new TableDef(csid__660("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("title"), new StringField(), false, null, false), new FieldDef(csid__660("status"), new StringField(), false, new SqlDefault(), false)), null);
                G::IReadOnlyDictionary<string, string> params__1151 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "Hello")));
                ISafeIdentifier t___14252 = csid__660("title");
                IChangeset cs__1152 = S0::SrcGlobal.Changeset(tbl__1150, params__1151).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14252));
                SqlFragment t___7668;
                t___7668 = cs__1152.ToInsertSql();
                SqlFragment t___7669 = t___7668;
                string s__1153 = t___7669.ToString();
                bool t___14256 = s__1153.IndexOf("INSERT INTO posts") >= 0;
                string fn__14240()
                {
                    return "has INSERT INTO: " + s__1153;
                }
                test___89.Assert(t___14256, (S1::Func<string>) fn__14240);
                bool t___14260 = s__1153.IndexOf("'Hello'") >= 0;
                string fn__14239()
                {
                    return "has title value: " + s__1153;
                }
                test___89.Assert(t___14260, (S1::Func<string>) fn__14239);
                bool t___14264 = s__1153.IndexOf("DEFAULT") >= 0;
                string fn__14238()
                {
                    return "status should use DEFAULT: " + s__1153;
                }
                test___89.Assert(t___14264, (S1::Func<string>) fn__14238);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlChangeOverridesDefaultValue__2178()
        {
            T::Test test___90 = new T::Test();
            try
            {
                TableDef tbl__1155 = new TableDef(csid__660("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("title"), new StringField(), false, null, false), new FieldDef(csid__660("status"), new StringField(), false, new SqlDefault(), false)), null);
                G::IReadOnlyDictionary<string, string> params__1156 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "Hello"), new G::KeyValuePair<string, string>("status", "published")));
                ISafeIdentifier t___14230 = csid__660("title");
                ISafeIdentifier t___14231 = csid__660("status");
                IChangeset cs__1157 = S0::SrcGlobal.Changeset(tbl__1155, params__1156).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14230, t___14231));
                SqlFragment t___7648;
                t___7648 = cs__1157.ToInsertSql();
                SqlFragment t___7649 = t___7648;
                string s__1158 = t___7649.ToString();
                bool t___14235 = s__1158.IndexOf("'published'") >= 0;
                string fn__14217()
                {
                    return "should use provided value: " + s__1158;
                }
                test___90.Assert(t___14235, (S1::Func<string>) fn__14217);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlWithTimestampsUsesDefault__2179()
        {
            T::Test test___91 = new T::Test();
            try
            {
                G::IReadOnlyList<FieldDef> t___7595;
                t___7595 = S0::SrcGlobal.Timestamps();
                G::IReadOnlyList<FieldDef> ts__1160 = t___7595;
                G::IList<FieldDef> fields__1161 = new G::List<FieldDef>();
                C::Listed.Add(fields__1161, new FieldDef(csid__660("title"), new StringField(), false, null, false));
                void fn__14183(FieldDef t__1162)
                {
                    C::Listed.Add(fields__1161, t__1162);
                }
                C::Listed.ForEach(ts__1160, (S1::Action<FieldDef>) fn__14183);
                TableDef tbl__1163 = new TableDef(csid__660("articles"), C::Listed.ToReadOnlyList(fields__1161), null);
                G::IReadOnlyDictionary<string, string> params__1164 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "News")));
                ISafeIdentifier t___14196 = csid__660("title");
                IChangeset cs__1165 = S0::SrcGlobal.Changeset(tbl__1163, params__1164).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14196));
                SqlFragment t___7610;
                t___7610 = cs__1165.ToInsertSql();
                SqlFragment t___7611 = t___7610;
                string s__1166 = t___7611.ToString();
                bool t___14200 = s__1166.IndexOf("inserted_at") >= 0;
                string fn__14182()
                {
                    return "should include inserted_at: " + s__1166;
                }
                test___91.Assert(t___14200, (S1::Func<string>) fn__14182);
                bool t___14204 = s__1166.IndexOf("updated_at") >= 0;
                string fn__14181()
                {
                    return "should include updated_at: " + s__1166;
                }
                test___91.Assert(t___14204, (S1::Func<string>) fn__14181);
                bool t___14208 = s__1166.IndexOf("DEFAULT") >= 0;
                string fn__14180()
                {
                    return "timestamps should use DEFAULT: " + s__1166;
                }
                test___91.Assert(t___14208, (S1::Func<string>) fn__14180);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlSkipsVirtualFields__2180()
        {
            T::Test test___92 = new T::Test();
            try
            {
                TableDef tbl__1168 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("name"), new StringField(), false, null, false), new FieldDef(csid__660("full_name"), new StringField(), true, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1169 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("full_name", "Alice Smith")));
                ISafeIdentifier t___14166 = csid__660("name");
                ISafeIdentifier t___14167 = csid__660("full_name");
                IChangeset cs__1170 = S0::SrcGlobal.Changeset(tbl__1168, params__1169).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14166, t___14167));
                SqlFragment t___7584;
                t___7584 = cs__1170.ToInsertSql();
                SqlFragment t___7585 = t___7584;
                string s__1171 = t___7585.ToString();
                bool t___14171 = s__1171.IndexOf("'Alice'") >= 0;
                string fn__14154()
                {
                    return "name should be included: " + s__1171;
                }
                test___92.Assert(t___14171, (S1::Func<string>) fn__14154);
                bool t___14177 = !(s__1171.IndexOf("full_name") >= 0);
                string fn__14153()
                {
                    return "virtual field should be excluded: " + s__1171;
                }
                test___92.Assert(t___14177, (S1::Func<string>) fn__14153);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlAllowsMissingNonNullableVirtualField__2181()
        {
            T::Test test___93 = new T::Test();
            try
            {
                TableDef tbl__1173 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("name"), new StringField(), false, null, false), new FieldDef(csid__660("computed"), new StringField(), false, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1174 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                ISafeIdentifier t___14146 = csid__660("name");
                IChangeset cs__1175 = S0::SrcGlobal.Changeset(tbl__1173, params__1174).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14146));
                SqlFragment t___7563;
                t___7563 = cs__1175.ToInsertSql();
                SqlFragment t___7564 = t___7563;
                string s__1176 = t___7564.ToString();
                bool t___14150 = s__1176.IndexOf("'Alice'") >= 0;
                string fn__14135()
                {
                    return "should succeed: " + s__1176;
                }
                test___93.Assert(t___14150, (S1::Func<string>) fn__14135);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlSkipsVirtualFields__2182()
        {
            T::Test test___94 = new T::Test();
            try
            {
                TableDef tbl__1178 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("name"), new StringField(), false, null, false), new FieldDef(csid__660("display"), new StringField(), true, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1179 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("display", "Bobby")));
                ISafeIdentifier t___14122 = csid__660("name");
                ISafeIdentifier t___14123 = csid__660("display");
                IChangeset cs__1180 = S0::SrcGlobal.Changeset(tbl__1178, params__1179).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14122, t___14123));
                SqlFragment t___7540;
                t___7540 = cs__1180.ToUpdateSql(1);
                SqlFragment t___7541 = t___7540;
                string s__1181 = t___7541.ToString();
                bool t___14127 = s__1181.IndexOf("name = 'Bob'") >= 0;
                string fn__14110()
                {
                    return "name should be in SET: " + s__1181;
                }
                test___94.Assert(t___14127, (S1::Func<string>) fn__14110);
                bool t___14133 = !(s__1181.IndexOf("display") >= 0);
                string fn__14109()
                {
                    return "virtual field excluded from UPDATE: " + s__1181;
                }
                test___94.Assert(t___14133, (S1::Func<string>) fn__14109);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlUsesCustomPrimaryKey__2183()
        {
            T::Test test___95 = new T::Test();
            try
            {
                TableDef tbl__1183 = new TableDef(csid__660("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("title"), new StringField(), false, null, false)), csid__660("post_id"));
                G::IReadOnlyDictionary<string, string> params__1184 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "Updated")));
                ISafeIdentifier t___14103 = csid__660("title");
                IChangeset cs__1185 = S0::SrcGlobal.Changeset(tbl__1183, params__1184).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14103));
                SqlFragment t___7522;
                t___7522 = cs__1185.ToUpdateSql(99);
                SqlFragment t___7523 = t___7522;
                string s__1186 = t___7523.ToString();
                bool t___14107 = s__1186 == "UPDATE posts SET title = 'Updated' WHERE post_id = 99";
                string fn__14093()
                {
                    return "got: " + s__1186;
                }
                test___95.Assert(t___14107, (S1::Func<string>) fn__14093);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteSqlUsesCustomPrimaryKey__2184()
        {
            T::Test test___96 = new T::Test();
            try
            {
                TableDef tbl__1188 = new TableDef(csid__660("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("title"), new StringField(), false, null, false)), csid__660("post_id"));
                string s__1189 = S0::SrcGlobal.DeleteSql(tbl__1188, 42).ToString();
                bool t___14080 = s__1189 == "DELETE FROM posts WHERE post_id = 42";
                string fn__14069()
                {
                    return "got: " + s__1189;
                }
                test___96.Assert(t___14080, (S1::Func<string>) fn__14069);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteSqlUsesDefaultIdWhenPrimaryKeyNull__2185()
        {
            T::Test test___97 = new T::Test();
            try
            {
                TableDef tbl__1191 = new TableDef(csid__660("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__660("name"), new StringField(), false, null, false)), null);
                string s__1192 = S0::SrcGlobal.DeleteSql(tbl__1191, 7).ToString();
                bool t___14067 = s__1192 == "DELETE FROM users WHERE id = 7";
                string fn__14058()
                {
                    return "got: " + s__1192;
                }
                test___97.Assert(t___14067, (S1::Func<string>) fn__14058);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__662(string name__1537)
        {
            ISafeIdentifier t___6965;
            t___6965 = S0::SrcGlobal.SafeIdentifier(name__1537);
            return t___6965;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__2267()
        {
            T::Test test___98 = new T::Test();
            try
            {
                Query q__1540 = S0::SrcGlobal.From(sid__662("users"));
                bool t___13551 = q__1540.ToSql().ToString() == "SELECT * FROM users";
                string fn__13546()
                {
                    return "bare query";
                }
                test___98.Assert(t___13551, (S1::Func<string>) fn__13546);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__2268()
        {
            T::Test test___99 = new T::Test();
            try
            {
                ISafeIdentifier t___13537 = sid__662("users");
                ISafeIdentifier t___13538 = sid__662("id");
                ISafeIdentifier t___13539 = sid__662("name");
                Query q__1542 = S0::SrcGlobal.From(t___13537).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13538, t___13539));
                bool t___13544 = q__1542.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__13536()
                {
                    return "select columns";
                }
                test___99.Assert(t___13544, (S1::Func<string>) fn__13536);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__2269()
        {
            T::Test test___100 = new T::Test();
            try
            {
                ISafeIdentifier t___13525 = sid__662("users");
                SqlBuilder t___13526 = new SqlBuilder();
                t___13526.AppendSafe("age > ");
                t___13526.AppendInt32(18);
                SqlFragment t___13529 = t___13526.Accumulated;
                Query q__1544 = S0::SrcGlobal.From(t___13525).Where(t___13529);
                bool t___13534 = q__1544.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__13524()
                {
                    return "where int";
                }
                test___100.Assert(t___13534, (S1::Func<string>) fn__13524);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__2271()
        {
            T::Test test___101 = new T::Test();
            try
            {
                ISafeIdentifier t___13513 = sid__662("users");
                SqlBuilder t___13514 = new SqlBuilder();
                t___13514.AppendSafe("active = ");
                t___13514.AppendBoolean(true);
                SqlFragment t___13517 = t___13514.Accumulated;
                Query q__1546 = S0::SrcGlobal.From(t___13513).Where(t___13517);
                bool t___13522 = q__1546.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__13512()
                {
                    return "where bool";
                }
                test___101.Assert(t___13522, (S1::Func<string>) fn__13512);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__2273()
        {
            T::Test test___102 = new T::Test();
            try
            {
                ISafeIdentifier t___13496 = sid__662("users");
                SqlBuilder t___13497 = new SqlBuilder();
                t___13497.AppendSafe("age > ");
                t___13497.AppendInt32(18);
                SqlFragment t___13500 = t___13497.Accumulated;
                Query t___13501 = S0::SrcGlobal.From(t___13496).Where(t___13500);
                SqlBuilder t___13502 = new SqlBuilder();
                t___13502.AppendSafe("active = ");
                t___13502.AppendBoolean(true);
                Query q__1548 = t___13501.Where(t___13502.Accumulated);
                bool t___13510 = q__1548.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__13495()
                {
                    return "chained where";
                }
                test___102.Assert(t___13510, (S1::Func<string>) fn__13495);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__2276()
        {
            T::Test test___103 = new T::Test();
            try
            {
                ISafeIdentifier t___13487 = sid__662("users");
                ISafeIdentifier t___13488 = sid__662("name");
                Query q__1550 = S0::SrcGlobal.From(t___13487).OrderBy(t___13488, true);
                bool t___13493 = q__1550.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__13486()
                {
                    return "order asc";
                }
                test___103.Assert(t___13493, (S1::Func<string>) fn__13486);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__2277()
        {
            T::Test test___104 = new T::Test();
            try
            {
                ISafeIdentifier t___13478 = sid__662("users");
                ISafeIdentifier t___13479 = sid__662("created_at");
                Query q__1552 = S0::SrcGlobal.From(t___13478).OrderBy(t___13479, false);
                bool t___13484 = q__1552.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__13477()
                {
                    return "order desc";
                }
                test___104.Assert(t___13484, (S1::Func<string>) fn__13477);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__2278()
        {
            T::Test test___105 = new T::Test();
            try
            {
                Query t___6899;
                t___6899 = S0::SrcGlobal.From(sid__662("users")).Limit(10);
                Query t___6900;
                t___6900 = t___6899.Offset(20);
                Query q__1554 = t___6900;
                bool t___13475 = q__1554.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__13470()
                {
                    return "limit/offset";
                }
                test___105.Assert(t___13475, (S1::Func<string>) fn__13470);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__2279()
        {
            T::Test test___106 = new T::Test();
            try
            {
                bool didBubble__1556;
                try
                {
                    S0::SrcGlobal.From(sid__662("users")).Limit(-1);
                    didBubble__1556 = false;
                }
                catch
                {
                    didBubble__1556 = true;
                }
                string fn__13466()
                {
                    return "negative limit should bubble";
                }
                test___106.Assert(didBubble__1556, (S1::Func<string>) fn__13466);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__2280()
        {
            T::Test test___107 = new T::Test();
            try
            {
                bool didBubble__1558;
                try
                {
                    S0::SrcGlobal.From(sid__662("users")).Offset(-1);
                    didBubble__1558 = false;
                }
                catch
                {
                    didBubble__1558 = true;
                }
                string fn__13462()
                {
                    return "negative offset should bubble";
                }
                test___107.Assert(didBubble__1558, (S1::Func<string>) fn__13462);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__2281()
        {
            T::Test test___108 = new T::Test();
            try
            {
                int minAge__1560 = 21;
                ISafeIdentifier t___13440 = sid__662("users");
                ISafeIdentifier t___13441 = sid__662("id");
                ISafeIdentifier t___13442 = sid__662("name");
                ISafeIdentifier t___13443 = sid__662("email");
                Query t___13444 = S0::SrcGlobal.From(t___13440).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13441, t___13442, t___13443));
                SqlBuilder t___13445 = new SqlBuilder();
                t___13445.AppendSafe("age >= ");
                t___13445.AppendInt32(21);
                Query t___13449 = t___13444.Where(t___13445.Accumulated);
                SqlBuilder t___13450 = new SqlBuilder();
                t___13450.AppendSafe("active = ");
                t___13450.AppendBoolean(true);
                Query t___6885;
                t___6885 = t___13449.Where(t___13450.Accumulated).OrderBy(sid__662("name"), true).Limit(25);
                Query t___6886;
                t___6886 = t___6885.Offset(0);
                Query q__1561 = t___6886;
                bool t___13460 = q__1561.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__13439()
                {
                    return "complex query";
                }
                test___108.Assert(t___13460, (S1::Func<string>) fn__13439);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__2284()
        {
            T::Test test___109 = new T::Test();
            try
            {
                Query q__1563 = S0::SrcGlobal.From(sid__662("users"));
                SqlFragment t___6862;
                t___6862 = q__1563.SafeToSql(100);
                SqlFragment t___6863 = t___6862;
                string s__1564 = t___6863.ToString();
                bool t___13437 = s__1564 == "SELECT * FROM users LIMIT 100";
                string fn__13433()
                {
                    return "should have limit: " + s__1564;
                }
                test___109.Assert(t___13437, (S1::Func<string>) fn__13433);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__2285()
        {
            T::Test test___110 = new T::Test();
            try
            {
                Query t___6854;
                t___6854 = S0::SrcGlobal.From(sid__662("users")).Limit(5);
                Query q__1566 = t___6854;
                SqlFragment t___6857;
                t___6857 = q__1566.SafeToSql(100);
                SqlFragment t___6858 = t___6857;
                string s__1567 = t___6858.ToString();
                bool t___13431 = s__1567 == "SELECT * FROM users LIMIT 5";
                string fn__13427()
                {
                    return "explicit limit preserved: " + s__1567;
                }
                test___110.Assert(t___13431, (S1::Func<string>) fn__13427);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__2286()
        {
            T::Test test___111 = new T::Test();
            try
            {
                bool didBubble__1569;
                try
                {
                    S0::SrcGlobal.From(sid__662("users")).SafeToSql(-1);
                    didBubble__1569 = false;
                }
                catch
                {
                    didBubble__1569 = true;
                }
                string fn__13423()
                {
                    return "negative defaultLimit should bubble";
                }
                test___111.Assert(didBubble__1569, (S1::Func<string>) fn__13423);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__2287()
        {
            T::Test test___112 = new T::Test();
            try
            {
                string evil__1571 = "'; DROP TABLE users; --";
                ISafeIdentifier t___13407 = sid__662("users");
                SqlBuilder t___13408 = new SqlBuilder();
                t___13408.AppendSafe("name = ");
                t___13408.AppendString("'; DROP TABLE users; --");
                SqlFragment t___13411 = t___13408.Accumulated;
                Query q__1572 = S0::SrcGlobal.From(t___13407).Where(t___13411);
                string s__1573 = q__1572.ToSql().ToString();
                bool t___13416 = s__1573.IndexOf("''") >= 0;
                string fn__13406()
                {
                    return "quotes must be doubled: " + s__1573;
                }
                test___112.Assert(t___13416, (S1::Func<string>) fn__13406);
                bool t___13420 = s__1573.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__13405()
                {
                    return "structure intact: " + s__1573;
                }
                test___112.Assert(t___13420, (S1::Func<string>) fn__13405);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__2289()
        {
            T::Test test___113 = new T::Test();
            try
            {
                string attack__1575 = "users; DROP TABLE users; --";
                bool didBubble__1576;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__1576 = false;
                }
                catch
                {
                    didBubble__1576 = true;
                }
                string fn__13402()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___113.Assert(didBubble__1576, (S1::Func<string>) fn__13402);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__2290()
        {
            T::Test test___114 = new T::Test();
            try
            {
                ISafeIdentifier t___13391 = sid__662("users");
                ISafeIdentifier t___13392 = sid__662("orders");
                SqlBuilder t___13393 = new SqlBuilder();
                t___13393.AppendSafe("users.id = orders.user_id");
                SqlFragment t___13395 = t___13393.Accumulated;
                Query q__1578 = S0::SrcGlobal.From(t___13391).InnerJoin(t___13392, t___13395);
                bool t___13400 = q__1578.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__13390()
                {
                    return "inner join";
                }
                test___114.Assert(t___13400, (S1::Func<string>) fn__13390);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__2292()
        {
            T::Test test___115 = new T::Test();
            try
            {
                ISafeIdentifier t___13379 = sid__662("users");
                ISafeIdentifier t___13380 = sid__662("profiles");
                SqlBuilder t___13381 = new SqlBuilder();
                t___13381.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___13383 = t___13381.Accumulated;
                Query q__1580 = S0::SrcGlobal.From(t___13379).LeftJoin(t___13380, t___13383);
                bool t___13388 = q__1580.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__13378()
                {
                    return "left join";
                }
                test___115.Assert(t___13388, (S1::Func<string>) fn__13378);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__2294()
        {
            T::Test test___116 = new T::Test();
            try
            {
                ISafeIdentifier t___13367 = sid__662("orders");
                ISafeIdentifier t___13368 = sid__662("users");
                SqlBuilder t___13369 = new SqlBuilder();
                t___13369.AppendSafe("orders.user_id = users.id");
                SqlFragment t___13371 = t___13369.Accumulated;
                Query q__1582 = S0::SrcGlobal.From(t___13367).RightJoin(t___13368, t___13371);
                bool t___13376 = q__1582.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__13366()
                {
                    return "right join";
                }
                test___116.Assert(t___13376, (S1::Func<string>) fn__13366);
            }
            finally
            {
                test___116.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__2296()
        {
            T::Test test___117 = new T::Test();
            try
            {
                ISafeIdentifier t___13355 = sid__662("users");
                ISafeIdentifier t___13356 = sid__662("orders");
                SqlBuilder t___13357 = new SqlBuilder();
                t___13357.AppendSafe("users.id = orders.user_id");
                SqlFragment t___13359 = t___13357.Accumulated;
                Query q__1584 = S0::SrcGlobal.From(t___13355).FullJoin(t___13356, t___13359);
                bool t___13364 = q__1584.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__13354()
                {
                    return "full join";
                }
                test___117.Assert(t___13364, (S1::Func<string>) fn__13354);
            }
            finally
            {
                test___117.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__2298()
        {
            T::Test test___118 = new T::Test();
            try
            {
                ISafeIdentifier t___13338 = sid__662("users");
                ISafeIdentifier t___13339 = sid__662("orders");
                SqlBuilder t___13340 = new SqlBuilder();
                t___13340.AppendSafe("users.id = orders.user_id");
                SqlFragment t___13342 = t___13340.Accumulated;
                Query t___13343 = S0::SrcGlobal.From(t___13338).InnerJoin(t___13339, t___13342);
                ISafeIdentifier t___13344 = sid__662("profiles");
                SqlBuilder t___13345 = new SqlBuilder();
                t___13345.AppendSafe("users.id = profiles.user_id");
                Query q__1586 = t___13343.LeftJoin(t___13344, t___13345.Accumulated);
                bool t___13352 = q__1586.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__13337()
                {
                    return "chained joins";
                }
                test___118.Assert(t___13352, (S1::Func<string>) fn__13337);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__2301()
        {
            T::Test test___119 = new T::Test();
            try
            {
                ISafeIdentifier t___13319 = sid__662("users");
                ISafeIdentifier t___13320 = sid__662("orders");
                SqlBuilder t___13321 = new SqlBuilder();
                t___13321.AppendSafe("users.id = orders.user_id");
                SqlFragment t___13323 = t___13321.Accumulated;
                Query t___13324 = S0::SrcGlobal.From(t___13319).InnerJoin(t___13320, t___13323);
                SqlBuilder t___13325 = new SqlBuilder();
                t___13325.AppendSafe("orders.total > ");
                t___13325.AppendInt32(100);
                Query t___6769;
                t___6769 = t___13324.Where(t___13325.Accumulated).OrderBy(sid__662("name"), true).Limit(10);
                Query q__1588 = t___6769;
                bool t___13335 = q__1588.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__13318()
                {
                    return "join with where/order/limit";
                }
                test___119.Assert(t___13335, (S1::Func<string>) fn__13318);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__2304()
        {
            T::Test test___120 = new T::Test();
            try
            {
                SqlFragment c__1590 = S0::SrcGlobal.Col(sid__662("users"), sid__662("id"));
                bool t___13316 = c__1590.ToString() == "users.id";
                string fn__13310()
                {
                    return "col helper";
                }
                test___120.Assert(t___13316, (S1::Func<string>) fn__13310);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__2305()
        {
            T::Test test___121 = new T::Test();
            try
            {
                SqlFragment onCond__1592 = S0::SrcGlobal.Col(sid__662("users"), sid__662("id"));
                SqlBuilder b__1593 = new SqlBuilder();
                b__1593.AppendFragment(onCond__1592);
                b__1593.AppendSafe(" = ");
                b__1593.AppendFragment(S0::SrcGlobal.Col(sid__662("orders"), sid__662("user_id")));
                ISafeIdentifier t___13301 = sid__662("users");
                ISafeIdentifier t___13302 = sid__662("orders");
                SqlFragment t___13303 = b__1593.Accumulated;
                Query q__1594 = S0::SrcGlobal.From(t___13301).InnerJoin(t___13302, t___13303);
                bool t___13308 = q__1594.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__13290()
                {
                    return "join with col";
                }
                test___121.Assert(t___13308, (S1::Func<string>) fn__13290);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__2306()
        {
            T::Test test___122 = new T::Test();
            try
            {
                ISafeIdentifier t___13279 = sid__662("users");
                SqlBuilder t___13280 = new SqlBuilder();
                t___13280.AppendSafe("status = ");
                t___13280.AppendString("active");
                SqlFragment t___13283 = t___13280.Accumulated;
                Query q__1596 = S0::SrcGlobal.From(t___13279).OrWhere(t___13283);
                bool t___13288 = q__1596.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__13278()
                {
                    return "orWhere basic";
                }
                test___122.Assert(t___13288, (S1::Func<string>) fn__13278);
            }
            finally
            {
                test___122.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__2308()
        {
            T::Test test___123 = new T::Test();
            try
            {
                ISafeIdentifier t___13262 = sid__662("users");
                SqlBuilder t___13263 = new SqlBuilder();
                t___13263.AppendSafe("age > ");
                t___13263.AppendInt32(18);
                SqlFragment t___13266 = t___13263.Accumulated;
                Query t___13267 = S0::SrcGlobal.From(t___13262).Where(t___13266);
                SqlBuilder t___13268 = new SqlBuilder();
                t___13268.AppendSafe("vip = ");
                t___13268.AppendBoolean(true);
                Query q__1598 = t___13267.OrWhere(t___13268.Accumulated);
                bool t___13276 = q__1598.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__13261()
                {
                    return "where then orWhere";
                }
                test___123.Assert(t___13276, (S1::Func<string>) fn__13261);
            }
            finally
            {
                test___123.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__2311()
        {
            T::Test test___124 = new T::Test();
            try
            {
                ISafeIdentifier t___13240 = sid__662("users");
                SqlBuilder t___13241 = new SqlBuilder();
                t___13241.AppendSafe("active = ");
                t___13241.AppendBoolean(true);
                SqlFragment t___13244 = t___13241.Accumulated;
                Query t___13245 = S0::SrcGlobal.From(t___13240).Where(t___13244);
                SqlBuilder t___13246 = new SqlBuilder();
                t___13246.AppendSafe("role = ");
                t___13246.AppendString("admin");
                Query t___13250 = t___13245.OrWhere(t___13246.Accumulated);
                SqlBuilder t___13251 = new SqlBuilder();
                t___13251.AppendSafe("role = ");
                t___13251.AppendString("moderator");
                Query q__1600 = t___13250.OrWhere(t___13251.Accumulated);
                bool t___13259 = q__1600.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__13239()
                {
                    return "multiple orWhere";
                }
                test___124.Assert(t___13259, (S1::Func<string>) fn__13239);
            }
            finally
            {
                test___124.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__2315()
        {
            T::Test test___125 = new T::Test();
            try
            {
                ISafeIdentifier t___13218 = sid__662("users");
                SqlBuilder t___13219 = new SqlBuilder();
                t___13219.AppendSafe("age > ");
                t___13219.AppendInt32(18);
                SqlFragment t___13222 = t___13219.Accumulated;
                Query t___13223 = S0::SrcGlobal.From(t___13218).Where(t___13222);
                SqlBuilder t___13224 = new SqlBuilder();
                t___13224.AppendSafe("active = ");
                t___13224.AppendBoolean(true);
                Query t___13228 = t___13223.Where(t___13224.Accumulated);
                SqlBuilder t___13229 = new SqlBuilder();
                t___13229.AppendSafe("vip = ");
                t___13229.AppendBoolean(true);
                Query q__1602 = t___13228.OrWhere(t___13229.Accumulated);
                bool t___13237 = q__1602.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__13217()
                {
                    return "mixed where and orWhere";
                }
                test___125.Assert(t___13237, (S1::Func<string>) fn__13217);
            }
            finally
            {
                test___125.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__2319()
        {
            T::Test test___126 = new T::Test();
            try
            {
                ISafeIdentifier t___13209 = sid__662("users");
                ISafeIdentifier t___13210 = sid__662("deleted_at");
                Query q__1604 = S0::SrcGlobal.From(t___13209).WhereNull(t___13210);
                bool t___13215 = q__1604.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__13208()
                {
                    return "whereNull";
                }
                test___126.Assert(t___13215, (S1::Func<string>) fn__13208);
            }
            finally
            {
                test___126.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__2320()
        {
            T::Test test___127 = new T::Test();
            try
            {
                ISafeIdentifier t___13200 = sid__662("users");
                ISafeIdentifier t___13201 = sid__662("email");
                Query q__1606 = S0::SrcGlobal.From(t___13200).WhereNotNull(t___13201);
                bool t___13206 = q__1606.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__13199()
                {
                    return "whereNotNull";
                }
                test___127.Assert(t___13206, (S1::Func<string>) fn__13199);
            }
            finally
            {
                test___127.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__2321()
        {
            T::Test test___128 = new T::Test();
            try
            {
                ISafeIdentifier t___13186 = sid__662("users");
                SqlBuilder t___13187 = new SqlBuilder();
                t___13187.AppendSafe("active = ");
                t___13187.AppendBoolean(true);
                SqlFragment t___13190 = t___13187.Accumulated;
                Query q__1608 = S0::SrcGlobal.From(t___13186).Where(t___13190).WhereNull(sid__662("deleted_at"));
                bool t___13197 = q__1608.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__13185()
                {
                    return "whereNull chained";
                }
                test___128.Assert(t___13197, (S1::Func<string>) fn__13185);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__2323()
        {
            T::Test test___129 = new T::Test();
            try
            {
                ISafeIdentifier t___13172 = sid__662("users");
                ISafeIdentifier t___13173 = sid__662("deleted_at");
                Query t___13174 = S0::SrcGlobal.From(t___13172).WhereNull(t___13173);
                SqlBuilder t___13175 = new SqlBuilder();
                t___13175.AppendSafe("role = ");
                t___13175.AppendString("admin");
                Query q__1610 = t___13174.OrWhere(t___13175.Accumulated);
                bool t___13183 = q__1610.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__13171()
                {
                    return "whereNotNull with orWhere";
                }
                test___129.Assert(t___13183, (S1::Func<string>) fn__13171);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__2325()
        {
            T::Test test___130 = new T::Test();
            try
            {
                ISafeIdentifier t___13160 = sid__662("users");
                ISafeIdentifier t___13161 = sid__662("id");
                SqlInt32 t___13162 = new SqlInt32(1);
                SqlInt32 t___13163 = new SqlInt32(2);
                SqlInt32 t___13164 = new SqlInt32(3);
                Query q__1612 = S0::SrcGlobal.From(t___13160).WhereIn(t___13161, C::Listed.CreateReadOnlyList<SqlInt32>(t___13162, t___13163, t___13164));
                bool t___13169 = q__1612.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__13159()
                {
                    return "whereIn ints";
                }
                test___130.Assert(t___13169, (S1::Func<string>) fn__13159);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__2326()
        {
            T::Test test___131 = new T::Test();
            try
            {
                ISafeIdentifier t___13149 = sid__662("users");
                ISafeIdentifier t___13150 = sid__662("name");
                SqlString t___13151 = new SqlString("Alice");
                SqlString t___13152 = new SqlString("Bob's");
                Query q__1614 = S0::SrcGlobal.From(t___13149).WhereIn(t___13150, C::Listed.CreateReadOnlyList<SqlString>(t___13151, t___13152));
                bool t___13157 = q__1614.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__13148()
                {
                    return "whereIn strings";
                }
                test___131.Assert(t___13157, (S1::Func<string>) fn__13148);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__2327()
        {
            T::Test test___132 = new T::Test();
            try
            {
                ISafeIdentifier t___13140 = sid__662("users");
                ISafeIdentifier t___13141 = sid__662("id");
                Query q__1616 = S0::SrcGlobal.From(t___13140).WhereIn(t___13141, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___13146 = q__1616.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__13139()
                {
                    return "whereIn empty";
                }
                test___132.Assert(t___13146, (S1::Func<string>) fn__13139);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__2328()
        {
            T::Test test___133 = new T::Test();
            try
            {
                ISafeIdentifier t___13124 = sid__662("users");
                SqlBuilder t___13125 = new SqlBuilder();
                t___13125.AppendSafe("active = ");
                t___13125.AppendBoolean(true);
                SqlFragment t___13128 = t___13125.Accumulated;
                Query q__1618 = S0::SrcGlobal.From(t___13124).Where(t___13128).WhereIn(sid__662("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___13137 = q__1618.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__13123()
                {
                    return "whereIn chained";
                }
                test___133.Assert(t___13137, (S1::Func<string>) fn__13123);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__2330()
        {
            T::Test test___134 = new T::Test();
            try
            {
                ISafeIdentifier t___13114 = sid__662("users");
                ISafeIdentifier t___13115 = sid__662("id");
                SqlInt32 t___13116 = new SqlInt32(42);
                Query q__1620 = S0::SrcGlobal.From(t___13114).WhereIn(t___13115, C::Listed.CreateReadOnlyList<SqlInt32>(t___13116));
                bool t___13121 = q__1620.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__13113()
                {
                    return "whereIn single";
                }
                test___134.Assert(t___13121, (S1::Func<string>) fn__13113);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__2331()
        {
            T::Test test___135 = new T::Test();
            try
            {
                ISafeIdentifier t___13102 = sid__662("users");
                SqlBuilder t___13103 = new SqlBuilder();
                t___13103.AppendSafe("active = ");
                t___13103.AppendBoolean(true);
                SqlFragment t___13106 = t___13103.Accumulated;
                Query q__1622 = S0::SrcGlobal.From(t___13102).WhereNot(t___13106);
                bool t___13111 = q__1622.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__13101()
                {
                    return "whereNot";
                }
                test___135.Assert(t___13111, (S1::Func<string>) fn__13101);
            }
            finally
            {
                test___135.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__2333()
        {
            T::Test test___136 = new T::Test();
            try
            {
                ISafeIdentifier t___13085 = sid__662("users");
                SqlBuilder t___13086 = new SqlBuilder();
                t___13086.AppendSafe("age > ");
                t___13086.AppendInt32(18);
                SqlFragment t___13089 = t___13086.Accumulated;
                Query t___13090 = S0::SrcGlobal.From(t___13085).Where(t___13089);
                SqlBuilder t___13091 = new SqlBuilder();
                t___13091.AppendSafe("banned = ");
                t___13091.AppendBoolean(true);
                Query q__1624 = t___13090.WhereNot(t___13091.Accumulated);
                bool t___13099 = q__1624.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__13084()
                {
                    return "whereNot chained";
                }
                test___136.Assert(t___13099, (S1::Func<string>) fn__13084);
            }
            finally
            {
                test___136.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__2336()
        {
            T::Test test___137 = new T::Test();
            try
            {
                ISafeIdentifier t___13074 = sid__662("users");
                ISafeIdentifier t___13075 = sid__662("age");
                SqlInt32 t___13076 = new SqlInt32(18);
                SqlInt32 t___13077 = new SqlInt32(65);
                Query q__1626 = S0::SrcGlobal.From(t___13074).WhereBetween(t___13075, t___13076, t___13077);
                bool t___13082 = q__1626.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__13073()
                {
                    return "whereBetween ints";
                }
                test___137.Assert(t___13082, (S1::Func<string>) fn__13073);
            }
            finally
            {
                test___137.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__2337()
        {
            T::Test test___138 = new T::Test();
            try
            {
                ISafeIdentifier t___13058 = sid__662("users");
                SqlBuilder t___13059 = new SqlBuilder();
                t___13059.AppendSafe("active = ");
                t___13059.AppendBoolean(true);
                SqlFragment t___13062 = t___13059.Accumulated;
                Query q__1628 = S0::SrcGlobal.From(t___13058).Where(t___13062).WhereBetween(sid__662("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___13071 = q__1628.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__13057()
                {
                    return "whereBetween chained";
                }
                test___138.Assert(t___13071, (S1::Func<string>) fn__13057);
            }
            finally
            {
                test___138.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__2339()
        {
            T::Test test___139 = new T::Test();
            try
            {
                ISafeIdentifier t___13049 = sid__662("users");
                ISafeIdentifier t___13050 = sid__662("name");
                Query q__1630 = S0::SrcGlobal.From(t___13049).WhereLike(t___13050, "John%");
                bool t___13055 = q__1630.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__13048()
                {
                    return "whereLike";
                }
                test___139.Assert(t___13055, (S1::Func<string>) fn__13048);
            }
            finally
            {
                test___139.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__2340()
        {
            T::Test test___140 = new T::Test();
            try
            {
                ISafeIdentifier t___13040 = sid__662("users");
                ISafeIdentifier t___13041 = sid__662("email");
                Query q__1632 = S0::SrcGlobal.From(t___13040).WhereILike(t___13041, "%@gmail.com");
                bool t___13046 = q__1632.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__13039()
                {
                    return "whereILike";
                }
                test___140.Assert(t___13046, (S1::Func<string>) fn__13039);
            }
            finally
            {
                test___140.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__2341()
        {
            T::Test test___141 = new T::Test();
            try
            {
                ISafeIdentifier t___13026 = sid__662("users");
                ISafeIdentifier t___13027 = sid__662("name");
                Query q__1634 = S0::SrcGlobal.From(t___13026).WhereLike(t___13027, "'; DROP TABLE users; --");
                string s__1635 = q__1634.ToSql().ToString();
                bool t___13032 = s__1635.IndexOf("''") >= 0;
                string fn__13025()
                {
                    return "like injection escaped: " + s__1635;
                }
                test___141.Assert(t___13032, (S1::Func<string>) fn__13025);
                bool t___13036 = s__1635.IndexOf("LIKE") >= 0;
                string fn__13024()
                {
                    return "like structure intact: " + s__1635;
                }
                test___141.Assert(t___13036, (S1::Func<string>) fn__13024);
            }
            finally
            {
                test___141.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__2342()
        {
            T::Test test___142 = new T::Test();
            try
            {
                ISafeIdentifier t___13016 = sid__662("users");
                ISafeIdentifier t___13017 = sid__662("name");
                Query q__1637 = S0::SrcGlobal.From(t___13016).WhereLike(t___13017, "%son%");
                bool t___13022 = q__1637.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__13015()
                {
                    return "whereLike wildcard";
                }
                test___142.Assert(t___13022, (S1::Func<string>) fn__13015);
            }
            finally
            {
                test___142.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__2343()
        {
            T::Test test___143 = new T::Test();
            try
            {
                SqlFragment f__1639 = S0::SrcGlobal.CountAll();
                bool t___13013 = f__1639.ToString() == "COUNT(*)";
                string fn__13009()
                {
                    return "countAll";
                }
                test___143.Assert(t___13013, (S1::Func<string>) fn__13009);
            }
            finally
            {
                test___143.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__2344()
        {
            T::Test test___144 = new T::Test();
            try
            {
                SqlFragment f__1641 = S0::SrcGlobal.CountCol(sid__662("id"));
                bool t___13007 = f__1641.ToString() == "COUNT(id)";
                string fn__13002()
                {
                    return "countCol";
                }
                test___144.Assert(t___13007, (S1::Func<string>) fn__13002);
            }
            finally
            {
                test___144.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__2345()
        {
            T::Test test___145 = new T::Test();
            try
            {
                SqlFragment f__1643 = S0::SrcGlobal.SumCol(sid__662("amount"));
                bool t___13000 = f__1643.ToString() == "SUM(amount)";
                string fn__12995()
                {
                    return "sumCol";
                }
                test___145.Assert(t___13000, (S1::Func<string>) fn__12995);
            }
            finally
            {
                test___145.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__2346()
        {
            T::Test test___146 = new T::Test();
            try
            {
                SqlFragment f__1645 = S0::SrcGlobal.AvgCol(sid__662("price"));
                bool t___12993 = f__1645.ToString() == "AVG(price)";
                string fn__12988()
                {
                    return "avgCol";
                }
                test___146.Assert(t___12993, (S1::Func<string>) fn__12988);
            }
            finally
            {
                test___146.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__2347()
        {
            T::Test test___147 = new T::Test();
            try
            {
                SqlFragment f__1647 = S0::SrcGlobal.MinCol(sid__662("created_at"));
                bool t___12986 = f__1647.ToString() == "MIN(created_at)";
                string fn__12981()
                {
                    return "minCol";
                }
                test___147.Assert(t___12986, (S1::Func<string>) fn__12981);
            }
            finally
            {
                test___147.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__2348()
        {
            T::Test test___148 = new T::Test();
            try
            {
                SqlFragment f__1649 = S0::SrcGlobal.MaxCol(sid__662("score"));
                bool t___12979 = f__1649.ToString() == "MAX(score)";
                string fn__12974()
                {
                    return "maxCol";
                }
                test___148.Assert(t___12979, (S1::Func<string>) fn__12974);
            }
            finally
            {
                test___148.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__2349()
        {
            T::Test test___149 = new T::Test();
            try
            {
                ISafeIdentifier t___12966 = sid__662("orders");
                SqlFragment t___12967 = S0::SrcGlobal.CountAll();
                Query q__1651 = S0::SrcGlobal.From(t___12966).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___12967));
                bool t___12972 = q__1651.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__12965()
                {
                    return "selectExpr count";
                }
                test___149.Assert(t___12972, (S1::Func<string>) fn__12965);
            }
            finally
            {
                test___149.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__2350()
        {
            T::Test test___150 = new T::Test();
            try
            {
                SqlFragment nameFrag__1653 = S0::SrcGlobal.Col(sid__662("users"), sid__662("name"));
                ISafeIdentifier t___12957 = sid__662("users");
                SqlFragment t___12958 = S0::SrcGlobal.CountAll();
                Query q__1654 = S0::SrcGlobal.From(t___12957).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1653, t___12958));
                bool t___12963 = q__1654.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__12953()
                {
                    return "selectExpr multi";
                }
                test___150.Assert(t___12963, (S1::Func<string>) fn__12953);
            }
            finally
            {
                test___150.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__2351()
        {
            T::Test test___151 = new T::Test();
            try
            {
                ISafeIdentifier t___12942 = sid__662("users");
                ISafeIdentifier t___12943 = sid__662("id");
                ISafeIdentifier t___12944 = sid__662("name");
                Query q__1656 = S0::SrcGlobal.From(t___12942).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12943, t___12944)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___12951 = q__1656.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__12941()
                {
                    return "selectExpr overrides select";
                }
                test___151.Assert(t___12951, (S1::Func<string>) fn__12941);
            }
            finally
            {
                test___151.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__2352()
        {
            T::Test test___152 = new T::Test();
            try
            {
                ISafeIdentifier t___12928 = sid__662("orders");
                SqlFragment t___12931 = S0::SrcGlobal.Col(sid__662("orders"), sid__662("status"));
                SqlFragment t___12932 = S0::SrcGlobal.CountAll();
                Query q__1658 = S0::SrcGlobal.From(t___12928).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___12931, t___12932)).GroupBy(sid__662("status"));
                bool t___12939 = q__1658.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__12927()
                {
                    return "groupBy single";
                }
                test___152.Assert(t___12939, (S1::Func<string>) fn__12927);
            }
            finally
            {
                test___152.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__2353()
        {
            T::Test test___153 = new T::Test();
            try
            {
                ISafeIdentifier t___12917 = sid__662("orders");
                ISafeIdentifier t___12918 = sid__662("status");
                Query q__1660 = S0::SrcGlobal.From(t___12917).GroupBy(t___12918).GroupBy(sid__662("category"));
                bool t___12925 = q__1660.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__12916()
                {
                    return "groupBy multiple";
                }
                test___153.Assert(t___12925, (S1::Func<string>) fn__12916);
            }
            finally
            {
                test___153.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__2354()
        {
            T::Test test___154 = new T::Test();
            try
            {
                ISafeIdentifier t___12898 = sid__662("orders");
                SqlFragment t___12901 = S0::SrcGlobal.Col(sid__662("orders"), sid__662("status"));
                SqlFragment t___12902 = S0::SrcGlobal.CountAll();
                Query t___12905 = S0::SrcGlobal.From(t___12898).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___12901, t___12902)).GroupBy(sid__662("status"));
                SqlBuilder t___12906 = new SqlBuilder();
                t___12906.AppendSafe("COUNT(*) > ");
                t___12906.AppendInt32(5);
                Query q__1662 = t___12905.Having(t___12906.Accumulated);
                bool t___12914 = q__1662.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__12897()
                {
                    return "having basic";
                }
                test___154.Assert(t___12914, (S1::Func<string>) fn__12897);
            }
            finally
            {
                test___154.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__2356()
        {
            T::Test test___155 = new T::Test();
            try
            {
                ISafeIdentifier t___12879 = sid__662("orders");
                ISafeIdentifier t___12880 = sid__662("status");
                Query t___12881 = S0::SrcGlobal.From(t___12879).GroupBy(t___12880);
                SqlBuilder t___12882 = new SqlBuilder();
                t___12882.AppendSafe("COUNT(*) > ");
                t___12882.AppendInt32(5);
                Query t___12886 = t___12881.Having(t___12882.Accumulated);
                SqlBuilder t___12887 = new SqlBuilder();
                t___12887.AppendSafe("SUM(total) > ");
                t___12887.AppendInt32(1000);
                Query q__1664 = t___12886.OrHaving(t___12887.Accumulated);
                bool t___12895 = q__1664.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__12878()
                {
                    return "orHaving";
                }
                test___155.Assert(t___12895, (S1::Func<string>) fn__12878);
            }
            finally
            {
                test___155.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__2359()
        {
            T::Test test___156 = new T::Test();
            try
            {
                ISafeIdentifier t___12869 = sid__662("users");
                ISafeIdentifier t___12870 = sid__662("name");
                Query q__1666 = S0::SrcGlobal.From(t___12869).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12870)).Distinct();
                bool t___12876 = q__1666.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__12868()
                {
                    return "distinct";
                }
                test___156.Assert(t___12876, (S1::Func<string>) fn__12868);
            }
            finally
            {
                test___156.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__2360()
        {
            T::Test test___157 = new T::Test();
            try
            {
                ISafeIdentifier t___12854 = sid__662("users");
                ISafeIdentifier t___12855 = sid__662("email");
                Query t___12856 = S0::SrcGlobal.From(t___12854).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12855));
                SqlBuilder t___12857 = new SqlBuilder();
                t___12857.AppendSafe("active = ");
                t___12857.AppendBoolean(true);
                Query q__1668 = t___12856.Where(t___12857.Accumulated).Distinct();
                bool t___12866 = q__1668.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__12853()
                {
                    return "distinct with where";
                }
                test___157.Assert(t___12866, (S1::Func<string>) fn__12853);
            }
            finally
            {
                test___157.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__2362()
        {
            T::Test test___158 = new T::Test();
            try
            {
                Query q__1670 = S0::SrcGlobal.From(sid__662("users"));
                bool t___12851 = q__1670.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__12846()
                {
                    return "countSql bare";
                }
                test___158.Assert(t___12851, (S1::Func<string>) fn__12846);
            }
            finally
            {
                test___158.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__2363()
        {
            T::Test test___159 = new T::Test();
            try
            {
                ISafeIdentifier t___12835 = sid__662("users");
                SqlBuilder t___12836 = new SqlBuilder();
                t___12836.AppendSafe("active = ");
                t___12836.AppendBoolean(true);
                SqlFragment t___12839 = t___12836.Accumulated;
                Query q__1672 = S0::SrcGlobal.From(t___12835).Where(t___12839);
                bool t___12844 = q__1672.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__12834()
                {
                    return "countSql with where";
                }
                test___159.Assert(t___12844, (S1::Func<string>) fn__12834);
            }
            finally
            {
                test___159.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__2365()
        {
            T::Test test___160 = new T::Test();
            try
            {
                ISafeIdentifier t___12818 = sid__662("users");
                ISafeIdentifier t___12819 = sid__662("orders");
                SqlBuilder t___12820 = new SqlBuilder();
                t___12820.AppendSafe("users.id = orders.user_id");
                SqlFragment t___12822 = t___12820.Accumulated;
                Query t___12823 = S0::SrcGlobal.From(t___12818).InnerJoin(t___12819, t___12822);
                SqlBuilder t___12824 = new SqlBuilder();
                t___12824.AppendSafe("orders.total > ");
                t___12824.AppendInt32(100);
                Query q__1674 = t___12823.Where(t___12824.Accumulated);
                bool t___12832 = q__1674.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__12817()
                {
                    return "countSql with join";
                }
                test___160.Assert(t___12832, (S1::Func<string>) fn__12817);
            }
            finally
            {
                test___160.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__2368()
        {
            T::Test test___161 = new T::Test();
            try
            {
                ISafeIdentifier t___12804 = sid__662("users");
                SqlBuilder t___12805 = new SqlBuilder();
                t___12805.AppendSafe("active = ");
                t___12805.AppendBoolean(true);
                SqlFragment t___12808 = t___12805.Accumulated;
                Query t___6345;
                t___6345 = S0::SrcGlobal.From(t___12804).Where(t___12808).OrderBy(sid__662("name"), true).Limit(10);
                Query t___6346;
                t___6346 = t___6345.Offset(20);
                Query q__1676 = t___6346;
                string s__1677 = q__1676.CountSql().ToString();
                bool t___12815 = s__1677 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__12803()
                {
                    return "countSql drops extras: " + s__1677;
                }
                test___161.Assert(t___12815, (S1::Func<string>) fn__12803);
            }
            finally
            {
                test___161.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__2370()
        {
            T::Test test___162 = new T::Test();
            try
            {
                ISafeIdentifier t___12771 = sid__662("orders");
                SqlFragment t___12774 = S0::SrcGlobal.Col(sid__662("orders"), sid__662("status"));
                SqlFragment t___12775 = S0::SrcGlobal.CountAll();
                SqlFragment t___12777 = S0::SrcGlobal.SumCol(sid__662("total"));
                Query t___12778 = S0::SrcGlobal.From(t___12771).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___12774, t___12775, t___12777));
                ISafeIdentifier t___12779 = sid__662("users");
                SqlBuilder t___12780 = new SqlBuilder();
                t___12780.AppendSafe("orders.user_id = users.id");
                Query t___12783 = t___12778.InnerJoin(t___12779, t___12780.Accumulated);
                SqlBuilder t___12784 = new SqlBuilder();
                t___12784.AppendSafe("users.active = ");
                t___12784.AppendBoolean(true);
                Query t___12790 = t___12783.Where(t___12784.Accumulated).GroupBy(sid__662("status"));
                SqlBuilder t___12791 = new SqlBuilder();
                t___12791.AppendSafe("COUNT(*) > ");
                t___12791.AppendInt32(3);
                Query q__1679 = t___12790.Having(t___12791.Accumulated).OrderBy(sid__662("status"), true);
                string expected__1680 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___12801 = q__1679.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__12770()
                {
                    return "full aggregation";
                }
                test___162.Assert(t___12801, (S1::Func<string>) fn__12770);
            }
            finally
            {
                test___162.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionSql__2374()
        {
            T::Test test___163 = new T::Test();
            try
            {
                ISafeIdentifier t___12753 = sid__662("users");
                SqlBuilder t___12754 = new SqlBuilder();
                t___12754.AppendSafe("role = ");
                t___12754.AppendString("admin");
                SqlFragment t___12757 = t___12754.Accumulated;
                Query a__1682 = S0::SrcGlobal.From(t___12753).Where(t___12757);
                ISafeIdentifier t___12759 = sid__662("users");
                SqlBuilder t___12760 = new SqlBuilder();
                t___12760.AppendSafe("role = ");
                t___12760.AppendString("moderator");
                SqlFragment t___12763 = t___12760.Accumulated;
                Query b__1683 = S0::SrcGlobal.From(t___12759).Where(t___12763);
                string s__1684 = S0::SrcGlobal.UnionSql(a__1682, b__1683).ToString();
                bool t___12768 = s__1684 == "(SELECT * FROM users WHERE role = 'admin') UNION (SELECT * FROM users WHERE role = 'moderator')";
                string fn__12752()
                {
                    return "unionSql: " + s__1684;
                }
                test___163.Assert(t___12768, (S1::Func<string>) fn__12752);
            }
            finally
            {
                test___163.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionAllSql__2377()
        {
            T::Test test___164 = new T::Test();
            try
            {
                ISafeIdentifier t___12741 = sid__662("users");
                ISafeIdentifier t___12742 = sid__662("name");
                Query a__1686 = S0::SrcGlobal.From(t___12741).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12742));
                ISafeIdentifier t___12744 = sid__662("contacts");
                ISafeIdentifier t___12745 = sid__662("name");
                Query b__1687 = S0::SrcGlobal.From(t___12744).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12745));
                string s__1688 = S0::SrcGlobal.UnionAllSql(a__1686, b__1687).ToString();
                bool t___12750 = s__1688 == "(SELECT name FROM users) UNION ALL (SELECT name FROM contacts)";
                string fn__12740()
                {
                    return "unionAllSql: " + s__1688;
                }
                test___164.Assert(t___12750, (S1::Func<string>) fn__12740);
            }
            finally
            {
                test___164.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void intersectSql__2378()
        {
            T::Test test___165 = new T::Test();
            try
            {
                ISafeIdentifier t___12729 = sid__662("users");
                ISafeIdentifier t___12730 = sid__662("email");
                Query a__1690 = S0::SrcGlobal.From(t___12729).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12730));
                ISafeIdentifier t___12732 = sid__662("subscribers");
                ISafeIdentifier t___12733 = sid__662("email");
                Query b__1691 = S0::SrcGlobal.From(t___12732).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12733));
                string s__1692 = S0::SrcGlobal.IntersectSql(a__1690, b__1691).ToString();
                bool t___12738 = s__1692 == "(SELECT email FROM users) INTERSECT (SELECT email FROM subscribers)";
                string fn__12728()
                {
                    return "intersectSql: " + s__1692;
                }
                test___165.Assert(t___12738, (S1::Func<string>) fn__12728);
            }
            finally
            {
                test___165.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void exceptSql__2379()
        {
            T::Test test___166 = new T::Test();
            try
            {
                ISafeIdentifier t___12717 = sid__662("users");
                ISafeIdentifier t___12718 = sid__662("id");
                Query a__1694 = S0::SrcGlobal.From(t___12717).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12718));
                ISafeIdentifier t___12720 = sid__662("banned");
                ISafeIdentifier t___12721 = sid__662("id");
                Query b__1695 = S0::SrcGlobal.From(t___12720).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12721));
                string s__1696 = S0::SrcGlobal.ExceptSql(a__1694, b__1695).ToString();
                bool t___12726 = s__1696 == "(SELECT id FROM users) EXCEPT (SELECT id FROM banned)";
                string fn__12716()
                {
                    return "exceptSql: " + s__1696;
                }
                test___166.Assert(t___12726, (S1::Func<string>) fn__12716);
            }
            finally
            {
                test___166.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void subqueryWithAlias__2380()
        {
            T::Test test___167 = new T::Test();
            try
            {
                ISafeIdentifier t___12702 = sid__662("orders");
                ISafeIdentifier t___12703 = sid__662("user_id");
                Query t___12704 = S0::SrcGlobal.From(t___12702).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12703));
                SqlBuilder t___12705 = new SqlBuilder();
                t___12705.AppendSafe("total > ");
                t___12705.AppendInt32(100);
                Query inner__1698 = t___12704.Where(t___12705.Accumulated);
                string s__1699 = S0::SrcGlobal.Subquery(inner__1698, sid__662("big_orders")).ToString();
                bool t___12714 = s__1699 == "(SELECT user_id FROM orders WHERE total > 100) AS big_orders";
                string fn__12701()
                {
                    return "subquery: " + s__1699;
                }
                test___167.Assert(t___12714, (S1::Func<string>) fn__12701);
            }
            finally
            {
                test___167.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSql__2382()
        {
            T::Test test___168 = new T::Test();
            try
            {
                ISafeIdentifier t___12691 = sid__662("orders");
                SqlBuilder t___12692 = new SqlBuilder();
                t___12692.AppendSafe("orders.user_id = users.id");
                SqlFragment t___12694 = t___12692.Accumulated;
                Query inner__1701 = S0::SrcGlobal.From(t___12691).Where(t___12694);
                string s__1702 = S0::SrcGlobal.ExistsSql(inner__1701).ToString();
                bool t___12699 = s__1702 == "EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__12690()
                {
                    return "existsSql: " + s__1702;
                }
                test___168.Assert(t___12699, (S1::Func<string>) fn__12690);
            }
            finally
            {
                test___168.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubquery__2384()
        {
            T::Test test___169 = new T::Test();
            try
            {
                ISafeIdentifier t___12674 = sid__662("orders");
                ISafeIdentifier t___12675 = sid__662("user_id");
                Query t___12676 = S0::SrcGlobal.From(t___12674).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12675));
                SqlBuilder t___12677 = new SqlBuilder();
                t___12677.AppendSafe("total > ");
                t___12677.AppendInt32(1000);
                Query sub__1704 = t___12676.Where(t___12677.Accumulated);
                ISafeIdentifier t___12682 = sid__662("users");
                ISafeIdentifier t___12683 = sid__662("id");
                Query q__1705 = S0::SrcGlobal.From(t___12682).WhereInSubquery(t___12683, sub__1704);
                string s__1706 = q__1705.ToSql().ToString();
                bool t___12688 = s__1706 == "SELECT * FROM users WHERE id IN (SELECT user_id FROM orders WHERE total > 1000)";
                string fn__12673()
                {
                    return "whereInSubquery: " + s__1706;
                }
                test___169.Assert(t___12688, (S1::Func<string>) fn__12673);
            }
            finally
            {
                test___169.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void setOperationWithWhereOnEachSide__2386()
        {
            T::Test test___170 = new T::Test();
            try
            {
                ISafeIdentifier t___12651 = sid__662("users");
                SqlBuilder t___12652 = new SqlBuilder();
                t___12652.AppendSafe("age > ");
                t___12652.AppendInt32(18);
                SqlFragment t___12655 = t___12652.Accumulated;
                Query t___12656 = S0::SrcGlobal.From(t___12651).Where(t___12655);
                SqlBuilder t___12657 = new SqlBuilder();
                t___12657.AppendSafe("active = ");
                t___12657.AppendBoolean(true);
                Query a__1708 = t___12656.Where(t___12657.Accumulated);
                ISafeIdentifier t___12662 = sid__662("users");
                SqlBuilder t___12663 = new SqlBuilder();
                t___12663.AppendSafe("role = ");
                t___12663.AppendString("vip");
                SqlFragment t___12666 = t___12663.Accumulated;
                Query b__1709 = S0::SrcGlobal.From(t___12662).Where(t___12666);
                string s__1710 = S0::SrcGlobal.UnionSql(a__1708, b__1709).ToString();
                bool t___12671 = s__1710 == "(SELECT * FROM users WHERE age > 18 AND active = TRUE) UNION (SELECT * FROM users WHERE role = 'vip')";
                string fn__12650()
                {
                    return "union with where: " + s__1710;
                }
                test___170.Assert(t___12671, (S1::Func<string>) fn__12650);
            }
            finally
            {
                test___170.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubqueryChainedWithWhere__2390()
        {
            T::Test test___171 = new T::Test();
            try
            {
                ISafeIdentifier t___12634 = sid__662("orders");
                ISafeIdentifier t___12635 = sid__662("user_id");
                Query sub__1712 = S0::SrcGlobal.From(t___12634).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12635));
                ISafeIdentifier t___12637 = sid__662("users");
                SqlBuilder t___12638 = new SqlBuilder();
                t___12638.AppendSafe("active = ");
                t___12638.AppendBoolean(true);
                SqlFragment t___12641 = t___12638.Accumulated;
                Query q__1713 = S0::SrcGlobal.From(t___12637).Where(t___12641).WhereInSubquery(sid__662("id"), sub__1712);
                string s__1714 = q__1713.ToSql().ToString();
                bool t___12648 = s__1714 == "SELECT * FROM users WHERE active = TRUE AND id IN (SELECT user_id FROM orders)";
                string fn__12633()
                {
                    return "whereInSubquery chained: " + s__1714;
                }
                test___171.Assert(t___12648, (S1::Func<string>) fn__12633);
            }
            finally
            {
                test___171.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSqlUsedInWhere__2392()
        {
            T::Test test___172 = new T::Test();
            try
            {
                ISafeIdentifier t___12620 = sid__662("orders");
                SqlBuilder t___12621 = new SqlBuilder();
                t___12621.AppendSafe("orders.user_id = users.id");
                SqlFragment t___12623 = t___12621.Accumulated;
                Query sub__1716 = S0::SrcGlobal.From(t___12620).Where(t___12623);
                ISafeIdentifier t___12625 = sid__662("users");
                SqlFragment t___12626 = S0::SrcGlobal.ExistsSql(sub__1716);
                Query q__1717 = S0::SrcGlobal.From(t___12625).Where(t___12626);
                string s__1718 = q__1717.ToSql().ToString();
                bool t___12631 = s__1718 == "SELECT * FROM users WHERE EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__12619()
                {
                    return "exists in where: " + s__1718;
                }
                test___172.Assert(t___12631, (S1::Func<string>) fn__12619);
            }
            finally
            {
                test___172.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBasic__2394()
        {
            T::Test test___173 = new T::Test();
            try
            {
                ISafeIdentifier t___12606 = sid__662("users");
                ISafeIdentifier t___12607 = sid__662("name");
                SqlString t___12608 = new SqlString("Alice");
                UpdateQuery t___12609 = S0::SrcGlobal.Update(t___12606).Set(t___12607, t___12608);
                SqlBuilder t___12610 = new SqlBuilder();
                t___12610.AppendSafe("id = ");
                t___12610.AppendInt32(1);
                SqlFragment t___6167;
                t___6167 = t___12609.Where(t___12610.Accumulated).ToSql();
                SqlFragment q__1720 = t___6167;
                bool t___12617 = q__1720.ToString() == "UPDATE users SET name = 'Alice' WHERE id = 1";
                string fn__12605()
                {
                    return "update basic";
                }
                test___173.Assert(t___12617, (S1::Func<string>) fn__12605);
            }
            finally
            {
                test___173.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleSet__2396()
        {
            T::Test test___174 = new T::Test();
            try
            {
                ISafeIdentifier t___12589 = sid__662("users");
                ISafeIdentifier t___12590 = sid__662("name");
                SqlString t___12591 = new SqlString("Bob");
                UpdateQuery t___12595 = S0::SrcGlobal.Update(t___12589).Set(t___12590, t___12591).Set(sid__662("age"), new SqlInt32(30));
                SqlBuilder t___12596 = new SqlBuilder();
                t___12596.AppendSafe("id = ");
                t___12596.AppendInt32(2);
                SqlFragment t___6152;
                t___6152 = t___12595.Where(t___12596.Accumulated).ToSql();
                SqlFragment q__1722 = t___6152;
                bool t___12603 = q__1722.ToString() == "UPDATE users SET name = 'Bob', age = 30 WHERE id = 2";
                string fn__12588()
                {
                    return "update multi set";
                }
                test___174.Assert(t___12603, (S1::Func<string>) fn__12588);
            }
            finally
            {
                test___174.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleWhere__2398()
        {
            T::Test test___175 = new T::Test();
            try
            {
                ISafeIdentifier t___12570 = sid__662("users");
                ISafeIdentifier t___12571 = sid__662("active");
                SqlBoolean t___12572 = new SqlBoolean(false);
                UpdateQuery t___12573 = S0::SrcGlobal.Update(t___12570).Set(t___12571, t___12572);
                SqlBuilder t___12574 = new SqlBuilder();
                t___12574.AppendSafe("age < ");
                t___12574.AppendInt32(18);
                UpdateQuery t___12578 = t___12573.Where(t___12574.Accumulated);
                SqlBuilder t___12579 = new SqlBuilder();
                t___12579.AppendSafe("role = ");
                t___12579.AppendString("guest");
                SqlFragment t___6134;
                t___6134 = t___12578.Where(t___12579.Accumulated).ToSql();
                SqlFragment q__1724 = t___6134;
                bool t___12586 = q__1724.ToString() == "UPDATE users SET active = FALSE WHERE age < 18 AND role = 'guest'";
                string fn__12569()
                {
                    return "update multi where";
                }
                test___175.Assert(t___12586, (S1::Func<string>) fn__12569);
            }
            finally
            {
                test___175.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryOrWhere__2401()
        {
            T::Test test___176 = new T::Test();
            try
            {
                ISafeIdentifier t___12551 = sid__662("users");
                ISafeIdentifier t___12552 = sid__662("status");
                SqlString t___12553 = new SqlString("banned");
                UpdateQuery t___12554 = S0::SrcGlobal.Update(t___12551).Set(t___12552, t___12553);
                SqlBuilder t___12555 = new SqlBuilder();
                t___12555.AppendSafe("spam_count > ");
                t___12555.AppendInt32(10);
                UpdateQuery t___12559 = t___12554.Where(t___12555.Accumulated);
                SqlBuilder t___12560 = new SqlBuilder();
                t___12560.AppendSafe("reported = ");
                t___12560.AppendBoolean(true);
                SqlFragment t___6113;
                t___6113 = t___12559.OrWhere(t___12560.Accumulated).ToSql();
                SqlFragment q__1726 = t___6113;
                bool t___12567 = q__1726.ToString() == "UPDATE users SET status = 'banned' WHERE spam_count > 10 OR reported = TRUE";
                string fn__12550()
                {
                    return "update orWhere";
                }
                test___176.Assert(t___12567, (S1::Func<string>) fn__12550);
            }
            finally
            {
                test___176.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutWhere__2404()
        {
            T::Test test___177 = new T::Test();
            try
            {
                ISafeIdentifier t___12544;
                ISafeIdentifier t___12545;
                SqlInt32 t___12546;
                bool didBubble__1728;
                try
                {
                    t___12544 = sid__662("users");
                    t___12545 = sid__662("x");
                    t___12546 = new SqlInt32(1);
                    S0::SrcGlobal.Update(t___12544).Set(t___12545, t___12546).ToSql();
                    didBubble__1728 = false;
                }
                catch
                {
                    didBubble__1728 = true;
                }
                string fn__12543()
                {
                    return "update without WHERE should bubble";
                }
                test___177.Assert(didBubble__1728, (S1::Func<string>) fn__12543);
            }
            finally
            {
                test___177.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutSet__2405()
        {
            T::Test test___178 = new T::Test();
            try
            {
                ISafeIdentifier t___12535;
                SqlBuilder t___12536;
                SqlFragment t___12539;
                bool didBubble__1730;
                try
                {
                    t___12535 = sid__662("users");
                    t___12536 = new SqlBuilder();
                    t___12536.AppendSafe("id = ");
                    t___12536.AppendInt32(1);
                    t___12539 = t___12536.Accumulated;
                    S0::SrcGlobal.Update(t___12535).Where(t___12539).ToSql();
                    didBubble__1730 = false;
                }
                catch
                {
                    didBubble__1730 = true;
                }
                string fn__12534()
                {
                    return "update without SET should bubble";
                }
                test___178.Assert(didBubble__1730, (S1::Func<string>) fn__12534);
            }
            finally
            {
                test___178.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryWithLimit__2407()
        {
            T::Test test___179 = new T::Test();
            try
            {
                ISafeIdentifier t___12521 = sid__662("users");
                ISafeIdentifier t___12522 = sid__662("active");
                SqlBoolean t___12523 = new SqlBoolean(false);
                UpdateQuery t___12524 = S0::SrcGlobal.Update(t___12521).Set(t___12522, t___12523);
                SqlBuilder t___12525 = new SqlBuilder();
                t___12525.AppendSafe("last_login < ");
                t___12525.AppendString("2024-01-01");
                UpdateQuery t___6076;
                t___6076 = t___12524.Where(t___12525.Accumulated).Limit(100);
                SqlFragment t___6077;
                t___6077 = t___6076.ToSql();
                SqlFragment q__1732 = t___6077;
                bool t___12532 = q__1732.ToString() == "UPDATE users SET active = FALSE WHERE last_login < '2024-01-01' LIMIT 100";
                string fn__12520()
                {
                    return "update limit";
                }
                test___179.Assert(t___12532, (S1::Func<string>) fn__12520);
            }
            finally
            {
                test___179.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryEscaping__2409()
        {
            T::Test test___180 = new T::Test();
            try
            {
                ISafeIdentifier t___12507 = sid__662("users");
                ISafeIdentifier t___12508 = sid__662("bio");
                SqlString t___12509 = new SqlString("It's a test");
                UpdateQuery t___12510 = S0::SrcGlobal.Update(t___12507).Set(t___12508, t___12509);
                SqlBuilder t___12511 = new SqlBuilder();
                t___12511.AppendSafe("id = ");
                t___12511.AppendInt32(1);
                SqlFragment t___6061;
                t___6061 = t___12510.Where(t___12511.Accumulated).ToSql();
                SqlFragment q__1734 = t___6061;
                bool t___12518 = q__1734.ToString() == "UPDATE users SET bio = 'It''s a test' WHERE id = 1";
                string fn__12506()
                {
                    return "update escaping";
                }
                test___180.Assert(t___12518, (S1::Func<string>) fn__12506);
            }
            finally
            {
                test___180.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBasic__2411()
        {
            T::Test test___181 = new T::Test();
            try
            {
                ISafeIdentifier t___12496 = sid__662("users");
                SqlBuilder t___12497 = new SqlBuilder();
                t___12497.AppendSafe("id = ");
                t___12497.AppendInt32(1);
                SqlFragment t___12500 = t___12497.Accumulated;
                SqlFragment t___6046;
                t___6046 = S0::SrcGlobal.DeleteFrom(t___12496).Where(t___12500).ToSql();
                SqlFragment q__1736 = t___6046;
                bool t___12504 = q__1736.ToString() == "DELETE FROM users WHERE id = 1";
                string fn__12495()
                {
                    return "delete basic";
                }
                test___181.Assert(t___12504, (S1::Func<string>) fn__12495);
            }
            finally
            {
                test___181.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryMultipleWhere__2413()
        {
            T::Test test___182 = new T::Test();
            try
            {
                ISafeIdentifier t___12480 = sid__662("logs");
                SqlBuilder t___12481 = new SqlBuilder();
                t___12481.AppendSafe("created_at < ");
                t___12481.AppendString("2024-01-01");
                SqlFragment t___12484 = t___12481.Accumulated;
                DeleteQuery t___12485 = S0::SrcGlobal.DeleteFrom(t___12480).Where(t___12484);
                SqlBuilder t___12486 = new SqlBuilder();
                t___12486.AppendSafe("level = ");
                t___12486.AppendString("debug");
                SqlFragment t___6034;
                t___6034 = t___12485.Where(t___12486.Accumulated).ToSql();
                SqlFragment q__1738 = t___6034;
                bool t___12493 = q__1738.ToString() == "DELETE FROM logs WHERE created_at < '2024-01-01' AND level = 'debug'";
                string fn__12479()
                {
                    return "delete multi where";
                }
                test___182.Assert(t___12493, (S1::Func<string>) fn__12479);
            }
            finally
            {
                test___182.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBubblesWithoutWhere__2416()
        {
            T::Test test___183 = new T::Test();
            try
            {
                bool didBubble__1740;
                try
                {
                    S0::SrcGlobal.DeleteFrom(sid__662("users")).ToSql();
                    didBubble__1740 = false;
                }
                catch
                {
                    didBubble__1740 = true;
                }
                string fn__12475()
                {
                    return "delete without WHERE should bubble";
                }
                test___183.Assert(didBubble__1740, (S1::Func<string>) fn__12475);
            }
            finally
            {
                test___183.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryOrWhere__2417()
        {
            T::Test test___184 = new T::Test();
            try
            {
                ISafeIdentifier t___12460 = sid__662("sessions");
                SqlBuilder t___12461 = new SqlBuilder();
                t___12461.AppendSafe("expired = ");
                t___12461.AppendBoolean(true);
                SqlFragment t___12464 = t___12461.Accumulated;
                DeleteQuery t___12465 = S0::SrcGlobal.DeleteFrom(t___12460).Where(t___12464);
                SqlBuilder t___12466 = new SqlBuilder();
                t___12466.AppendSafe("created_at < ");
                t___12466.AppendString("2023-01-01");
                SqlFragment t___6013;
                t___6013 = t___12465.OrWhere(t___12466.Accumulated).ToSql();
                SqlFragment q__1742 = t___6013;
                bool t___12473 = q__1742.ToString() == "DELETE FROM sessions WHERE expired = TRUE OR created_at < '2023-01-01'";
                string fn__12459()
                {
                    return "delete orWhere";
                }
                test___184.Assert(t___12473, (S1::Func<string>) fn__12459);
            }
            finally
            {
                test___184.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryWithLimit__2420()
        {
            T::Test test___185 = new T::Test();
            try
            {
                ISafeIdentifier t___12449 = sid__662("logs");
                SqlBuilder t___12450 = new SqlBuilder();
                t___12450.AppendSafe("level = ");
                t___12450.AppendString("debug");
                SqlFragment t___12453 = t___12450.Accumulated;
                DeleteQuery t___5994;
                t___5994 = S0::SrcGlobal.DeleteFrom(t___12449).Where(t___12453).Limit(1000);
                SqlFragment t___5995;
                t___5995 = t___5994.ToSql();
                SqlFragment q__1744 = t___5995;
                bool t___12457 = q__1744.ToString() == "DELETE FROM logs WHERE level = 'debug' LIMIT 1000";
                string fn__12448()
                {
                    return "delete limit";
                }
                test___185.Assert(t___12457, (S1::Func<string>) fn__12448);
            }
            finally
            {
                test___185.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsFirst__2422()
        {
            T::Test test___186 = new T::Test();
            try
            {
                ISafeIdentifier t___12439 = sid__662("users");
                ISafeIdentifier t___12440 = sid__662("email");
                NullsFirst t___12441 = new NullsFirst();
                Query q__1746 = S0::SrcGlobal.From(t___12439).OrderByNulls(t___12440, true, t___12441);
                bool t___12446 = q__1746.ToSql().ToString() == "SELECT * FROM users ORDER BY email ASC NULLS FIRST";
                string fn__12438()
                {
                    return "nulls first";
                }
                test___186.Assert(t___12446, (S1::Func<string>) fn__12438);
            }
            finally
            {
                test___186.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsLast__2423()
        {
            T::Test test___187 = new T::Test();
            try
            {
                ISafeIdentifier t___12429 = sid__662("users");
                ISafeIdentifier t___12430 = sid__662("score");
                NullsLast t___12431 = new NullsLast();
                Query q__1748 = S0::SrcGlobal.From(t___12429).OrderByNulls(t___12430, false, t___12431);
                bool t___12436 = q__1748.ToSql().ToString() == "SELECT * FROM users ORDER BY score DESC NULLS LAST";
                string fn__12428()
                {
                    return "nulls last";
                }
                test___187.Assert(t___12436, (S1::Func<string>) fn__12428);
            }
            finally
            {
                test___187.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedOrderByAndOrderByNulls__2424()
        {
            T::Test test___188 = new T::Test();
            try
            {
                ISafeIdentifier t___12417 = sid__662("users");
                ISafeIdentifier t___12418 = sid__662("name");
                Query q__1750 = S0::SrcGlobal.From(t___12417).OrderBy(t___12418, true).OrderByNulls(sid__662("email"), true, new NullsFirst());
                bool t___12426 = q__1750.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC, email ASC NULLS FIRST";
                string fn__12416()
                {
                    return "mixed order";
                }
                test___188.Assert(t___12426, (S1::Func<string>) fn__12416);
            }
            finally
            {
                test___188.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoin__2425()
        {
            T::Test test___189 = new T::Test();
            try
            {
                ISafeIdentifier t___12408 = sid__662("users");
                ISafeIdentifier t___12409 = sid__662("colors");
                Query q__1752 = S0::SrcGlobal.From(t___12408).CrossJoin(t___12409);
                bool t___12414 = q__1752.ToSql().ToString() == "SELECT * FROM users CROSS JOIN colors";
                string fn__12407()
                {
                    return "cross join";
                }
                test___189.Assert(t___12414, (S1::Func<string>) fn__12407);
            }
            finally
            {
                test___189.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoinCombinedWithOtherJoins__2426()
        {
            T::Test test___190 = new T::Test();
            try
            {
                ISafeIdentifier t___12394 = sid__662("users");
                ISafeIdentifier t___12395 = sid__662("orders");
                SqlBuilder t___12396 = new SqlBuilder();
                t___12396.AppendSafe("users.id = orders.user_id");
                SqlFragment t___12398 = t___12396.Accumulated;
                Query q__1754 = S0::SrcGlobal.From(t___12394).InnerJoin(t___12395, t___12398).CrossJoin(sid__662("colors"));
                bool t___12405 = q__1754.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id CROSS JOIN colors";
                string fn__12393()
                {
                    return "cross + inner join";
                }
                test___190.Assert(t___12405, (S1::Func<string>) fn__12393);
            }
            finally
            {
                test___190.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForUpdate__2428()
        {
            T::Test test___191 = new T::Test();
            try
            {
                ISafeIdentifier t___12380 = sid__662("users");
                SqlBuilder t___12381 = new SqlBuilder();
                t___12381.AppendSafe("id = ");
                t___12381.AppendInt32(1);
                SqlFragment t___12384 = t___12381.Accumulated;
                Query q__1756 = S0::SrcGlobal.From(t___12380).Where(t___12384).Lock(new ForUpdate());
                bool t___12391 = q__1756.ToSql().ToString() == "SELECT * FROM users WHERE id = 1 FOR UPDATE";
                string fn__12379()
                {
                    return "for update";
                }
                test___191.Assert(t___12391, (S1::Func<string>) fn__12379);
            }
            finally
            {
                test___191.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForShare__2430()
        {
            T::Test test___192 = new T::Test();
            try
            {
                ISafeIdentifier t___12369 = sid__662("users");
                ISafeIdentifier t___12370 = sid__662("name");
                Query q__1758 = S0::SrcGlobal.From(t___12369).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12370)).Lock(new ForShare());
                bool t___12377 = q__1758.ToSql().ToString() == "SELECT name FROM users FOR SHARE";
                string fn__12368()
                {
                    return "for share";
                }
                test___192.Assert(t___12377, (S1::Func<string>) fn__12368);
            }
            finally
            {
                test___192.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockWithFullQuery__2431()
        {
            T::Test test___193 = new T::Test();
            try
            {
                ISafeIdentifier t___12355 = sid__662("accounts");
                SqlBuilder t___12356 = new SqlBuilder();
                t___12356.AppendSafe("id = ");
                t___12356.AppendInt32(42);
                SqlFragment t___12359 = t___12356.Accumulated;
                Query t___5918;
                t___5918 = S0::SrcGlobal.From(t___12355).Where(t___12359).Limit(1);
                Query t___12362 = t___5918.Lock(new ForUpdate());
                Query q__1760 = t___12362;
                bool t___12366 = q__1760.ToSql().ToString() == "SELECT * FROM accounts WHERE id = 42 LIMIT 1 FOR UPDATE";
                string fn__12354()
                {
                    return "lock full query";
                }
                test___193.Assert(t___12366, (S1::Func<string>) fn__12354);
            }
            finally
            {
                test___193.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__2433()
        {
            T::Test test___200 = new T::Test();
            try
            {
                ISafeIdentifier t___5907;
                t___5907 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1808 = t___5907;
                bool t___12352 = id__1808.SqlValue == "user_name";
                string fn__12349()
                {
                    return "value should round-trip";
                }
                test___200.Assert(t___12352, (S1::Func<string>) fn__12349);
            }
            finally
            {
                test___200.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__2434()
        {
            T::Test test___201 = new T::Test();
            try
            {
                bool didBubble__1810;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1810 = false;
                }
                catch
                {
                    didBubble__1810 = true;
                }
                string fn__12346()
                {
                    return "empty string should bubble";
                }
                test___201.Assert(didBubble__1810, (S1::Func<string>) fn__12346);
            }
            finally
            {
                test___201.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__2435()
        {
            T::Test test___202 = new T::Test();
            try
            {
                bool didBubble__1812;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1812 = false;
                }
                catch
                {
                    didBubble__1812 = true;
                }
                string fn__12343()
                {
                    return "leading digit should bubble";
                }
                test___202.Assert(didBubble__1812, (S1::Func<string>) fn__12343);
            }
            finally
            {
                test___202.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__2436()
        {
            T::Test test___203 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1814 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__12340(string c__1815)
                {
                    bool didBubble__1816;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1815);
                        didBubble__1816 = false;
                    }
                    catch
                    {
                        didBubble__1816 = true;
                    }
                    string fn__12337()
                    {
                        return "should reject: " + c__1815;
                    }
                    test___203.Assert(didBubble__1816, (S1::Func<string>) fn__12337);
                }
                C::Listed.ForEach(cases__1814, (S1::Action<string>) fn__12340);
            }
            finally
            {
                test___203.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__2437()
        {
            T::Test test___204 = new T::Test();
            try
            {
                ISafeIdentifier t___5884;
                t___5884 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___5885 = t___5884;
                ISafeIdentifier t___5886;
                t___5886 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5887 = t___5886;
                StringField t___12327 = new StringField();
                FieldDef t___12328 = new FieldDef(t___5887, t___12327, false, null, false);
                ISafeIdentifier t___5890;
                t___5890 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___5891 = t___5890;
                IntField t___12329 = new IntField();
                FieldDef t___12330 = new FieldDef(t___5891, t___12329, false, null, false);
                TableDef td__1818 = new TableDef(t___5885, C::Listed.CreateReadOnlyList<FieldDef>(t___12328, t___12330), null);
                FieldDef t___5895;
                t___5895 = td__1818.Field("age");
                FieldDef f__1819 = t___5895;
                bool t___12335 = f__1819.Name.SqlValue == "age";
                string fn__12326()
                {
                    return "should find age field";
                }
                test___204.Assert(t___12335, (S1::Func<string>) fn__12326);
            }
            finally
            {
                test___204.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__2438()
        {
            T::Test test___205 = new T::Test();
            try
            {
                ISafeIdentifier t___5875;
                t___5875 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___5876 = t___5875;
                ISafeIdentifier t___5877;
                t___5877 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5878 = t___5877;
                StringField t___12321 = new StringField();
                FieldDef t___12322 = new FieldDef(t___5878, t___12321, false, null, false);
                TableDef td__1821 = new TableDef(t___5876, C::Listed.CreateReadOnlyList<FieldDef>(t___12322), null);
                bool didBubble__1822;
                try
                {
                    td__1821.Field("nonexistent");
                    didBubble__1822 = false;
                }
                catch
                {
                    didBubble__1822 = true;
                }
                string fn__12320()
                {
                    return "unknown field should bubble";
                }
                test___205.Assert(didBubble__1822, (S1::Func<string>) fn__12320);
            }
            finally
            {
                test___205.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__2439()
        {
            T::Test test___206 = new T::Test();
            try
            {
                ISafeIdentifier t___5863;
                t___5863 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___5864 = t___5863;
                StringField t___12309 = new StringField();
                FieldDef required__1824 = new FieldDef(t___5864, t___12309, false, null, false);
                ISafeIdentifier t___5867;
                t___5867 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___5868 = t___5867;
                StringField t___12311 = new StringField();
                FieldDef optional__1825 = new FieldDef(t___5868, t___12311, true, null, false);
                bool t___12315 = !required__1824.Nullable;
                string fn__12308()
                {
                    return "required field should not be nullable";
                }
                test___206.Assert(t___12315, (S1::Func<string>) fn__12308);
                bool t___12317 = optional__1825.Nullable;
                string fn__12307()
                {
                    return "optional field should be nullable";
                }
                test___206.Assert(t___12317, (S1::Func<string>) fn__12307);
            }
            finally
            {
                test___206.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void pkNameDefaultsToIdWhenPrimaryKeyIsNull__2440()
        {
            T::Test test___207 = new T::Test();
            try
            {
                ISafeIdentifier t___5854;
                t___5854 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___5855 = t___5854;
                ISafeIdentifier t___5856;
                t___5856 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5857 = t___5856;
                StringField t___12300 = new StringField();
                FieldDef t___12301 = new FieldDef(t___5857, t___12300, false, null, false);
                TableDef td__1827 = new TableDef(t___5855, C::Listed.CreateReadOnlyList<FieldDef>(t___12301), null);
                bool t___12305 = td__1827.PkName() == "id";
                string fn__12299()
                {
                    return "default pk should be id";
                }
                test___207.Assert(t___12305, (S1::Func<string>) fn__12299);
            }
            finally
            {
                test___207.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void pkNameReturnsCustomPrimaryKey__2441()
        {
            T::Test test___208 = new T::Test();
            try
            {
                ISafeIdentifier t___5842;
                t___5842 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___5843 = t___5842;
                ISafeIdentifier t___5844;
                t___5844 = S0::SrcGlobal.SafeIdentifier("user_id");
                ISafeIdentifier t___5845 = t___5844;
                IntField t___12292 = new IntField();
                G::IReadOnlyList<FieldDef> t___5850 = C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(t___5845, t___12292, false, null, false));
                ISafeIdentifier t___5848;
                t___5848 = S0::SrcGlobal.SafeIdentifier("user_id");
                ISafeIdentifier t___5849 = t___5848;
                TableDef td__1829 = new TableDef(t___5843, t___5850, t___5849);
                bool t___12297 = td__1829.PkName() == "user_id";
                string fn__12291()
                {
                    return "custom pk should be user_id";
                }
                test___208.Assert(t___12297, (S1::Func<string>) fn__12291);
            }
            finally
            {
                test___208.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void timestampsReturnsTwoDateFieldDefs__2442()
        {
            T::Test test___209 = new T::Test();
            try
            {
                G::IReadOnlyList<FieldDef> t___5818;
                t___5818 = S0::SrcGlobal.Timestamps();
                G::IReadOnlyList<FieldDef> ts__1831 = t___5818;
                bool t___12259 = ts__1831.Count == 2;
                string fn__12256()
                {
                    return "should return 2 fields";
                }
                test___209.Assert(t___12259, (S1::Func<string>) fn__12256);
                bool t___12265 = ts__1831[0].Name.SqlValue == "inserted_at";
                string fn__12255()
                {
                    return "first should be inserted_at";
                }
                test___209.Assert(t___12265, (S1::Func<string>) fn__12255);
                bool t___12271 = ts__1831[1].Name.SqlValue == "updated_at";
                string fn__12254()
                {
                    return "second should be updated_at";
                }
                test___209.Assert(t___12271, (S1::Func<string>) fn__12254);
                bool t___12274 = ts__1831[0].Nullable;
                string fn__12253()
                {
                    return "inserted_at should be nullable";
                }
                test___209.Assert(t___12274, (S1::Func<string>) fn__12253);
                bool t___12278 = ts__1831[1].Nullable;
                string fn__12252()
                {
                    return "updated_at should be nullable";
                }
                test___209.Assert(t___12278, (S1::Func<string>) fn__12252);
                bool t___12284 = !(ts__1831[0].DefaultValue == null);
                string fn__12251()
                {
                    return "inserted_at should have default";
                }
                test___209.Assert(t___12284, (S1::Func<string>) fn__12251);
                bool t___12289 = !(ts__1831[1].DefaultValue == null);
                string fn__12250()
                {
                    return "updated_at should have default";
                }
                test___209.Assert(t___12289, (S1::Func<string>) fn__12250);
            }
            finally
            {
                test___209.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefDefaultValueField__2443()
        {
            T::Test test___210 = new T::Test();
            try
            {
                ISafeIdentifier t___5805;
                t___5805 = S0::SrcGlobal.SafeIdentifier("status");
                ISafeIdentifier t___5806 = t___5805;
                StringField t___12237 = new StringField();
                SqlDefault t___12238 = new SqlDefault();
                FieldDef withDefault__1833 = new FieldDef(t___5806, t___12237, false, t___12238, false);
                ISafeIdentifier t___5810;
                t___5810 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5811 = t___5810;
                StringField t___12240 = new StringField();
                FieldDef withoutDefault__1834 = new FieldDef(t___5811, t___12240, false, null, false);
                bool t___12244 = !(withDefault__1833.DefaultValue == null);
                string fn__12236()
                {
                    return "should have default";
                }
                test___210.Assert(t___12244, (S1::Func<string>) fn__12236);
                bool t___12248 = withoutDefault__1834.DefaultValue == null;
                string fn__12235()
                {
                    return "should not have default";
                }
                test___210.Assert(t___12248, (S1::Func<string>) fn__12235);
            }
            finally
            {
                test___210.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefVirtualFlag__2444()
        {
            T::Test test___211 = new T::Test();
            try
            {
                ISafeIdentifier t___5793;
                t___5793 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5794 = t___5793;
                StringField t___12224 = new StringField();
                FieldDef normal__1836 = new FieldDef(t___5794, t___12224, false, null, false);
                ISafeIdentifier t___5797;
                t___5797 = S0::SrcGlobal.SafeIdentifier("full_name");
                ISafeIdentifier t___5798 = t___5797;
                StringField t___12226 = new StringField();
                FieldDef virt__1837 = new FieldDef(t___5798, t___12226, true, null, true);
                bool t___12230 = !normal__1836.Virtual;
                string fn__12223()
                {
                    return "normal field should not be virtual";
                }
                test___211.Assert(t___12230, (S1::Func<string>) fn__12223);
                bool t___12232 = virt__1837.Virtual;
                string fn__12222()
                {
                    return "virtual field should be virtual";
                }
                test___211.Assert(t___12232, (S1::Func<string>) fn__12222);
            }
            finally
            {
                test___211.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__2445()
        {
            T::Test test___215 = new T::Test();
            try
            {
                string build__1967(string name__1969)
                {
                    SqlBuilder t___12204 = new SqlBuilder();
                    t___12204.AppendSafe("select * from hi where name = ");
                    t___12204.AppendString(name__1969);
                    return t___12204.Accumulated.ToString();
                }
                string buildWrong__1968(string name__1971)
                {
                    return "select * from hi where name = '" + name__1971 + "'";
                }
                string actual___2447 = build__1967("world");
                bool t___12214 = actual___2447 == "select * from hi where name = 'world'";
                string fn__12211()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___2447 + ")";
                }
                test___215.Assert(t___12214, (S1::Func<string>) fn__12211);
                string bobbyTables__1973 = "Robert'); drop table hi;--";
                string actual___2449 = build__1967("Robert'); drop table hi;--");
                bool t___12218 = actual___2449 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__12210()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___2449 + ")";
                }
                test___215.Assert(t___12218, (S1::Func<string>) fn__12210);
                string fn__12209()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___215.Assert(true, (S1::Func<string>) fn__12209);
            }
            finally
            {
                test___215.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__2453()
        {
            T::Test test___216 = new T::Test();
            try
            {
                SqlBuilder t___12172 = new SqlBuilder();
                t___12172.AppendSafe("v = ");
                t___12172.AppendString("");
                string actual___2454 = t___12172.Accumulated.ToString();
                bool t___12178 = actual___2454 == "v = ''";
                string fn__12171()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___2454 + ")";
                }
                test___216.Assert(t___12178, (S1::Func<string>) fn__12171);
                SqlBuilder t___12180 = new SqlBuilder();
                t___12180.AppendSafe("v = ");
                t___12180.AppendString("a''b");
                string actual___2457 = t___12180.Accumulated.ToString();
                bool t___12186 = actual___2457 == "v = 'a''''b'";
                string fn__12170()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___2457 + ")";
                }
                test___216.Assert(t___12186, (S1::Func<string>) fn__12170);
                SqlBuilder t___12188 = new SqlBuilder();
                t___12188.AppendSafe("v = ");
                t___12188.AppendString("Hello 世界");
                string actual___2460 = t___12188.Accumulated.ToString();
                bool t___12194 = actual___2460 == "v = 'Hello 世界'";
                string fn__12169()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___2460 + ")";
                }
                test___216.Assert(t___12194, (S1::Func<string>) fn__12169);
                SqlBuilder t___12196 = new SqlBuilder();
                t___12196.AppendSafe("v = ");
                t___12196.AppendString("Line1\nLine2");
                string actual___2463 = t___12196.Accumulated.ToString();
                bool t___12202 = actual___2463 == "v = 'Line1\nLine2'";
                string fn__12168()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___2463 + ")";
                }
                test___216.Assert(t___12202, (S1::Func<string>) fn__12168);
            }
            finally
            {
                test___216.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__2466()
        {
            T::Test test___217 = new T::Test();
            try
            {
                SqlBuilder t___12143 = new SqlBuilder();
                t___12143.AppendSafe("select ");
                t___12143.AppendInt32(42);
                t___12143.AppendSafe(", ");
                t___12143.AppendInt64(43);
                t___12143.AppendSafe(", ");
                t___12143.AppendFloat64(19.99);
                t___12143.AppendSafe(", ");
                t___12143.AppendBoolean(true);
                t___12143.AppendSafe(", ");
                t___12143.AppendBoolean(false);
                string actual___2467 = t___12143.Accumulated.ToString();
                bool t___12157 = actual___2467 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__12142()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___2467 + ")";
                }
                test___217.Assert(t___12157, (S1::Func<string>) fn__12142);
                S1::DateTime t___5738;
                t___5738 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1976 = t___5738;
                SqlBuilder t___12159 = new SqlBuilder();
                t___12159.AppendSafe("insert into t values (");
                t___12159.AppendDate(date__1976);
                t___12159.AppendSafe(")");
                string actual___2470 = t___12159.Accumulated.ToString();
                bool t___12166 = actual___2470 == "insert into t values ('2024-12-25')";
                string fn__12141()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___2470 + ")";
                }
                test___217.Assert(t___12166, (S1::Func<string>) fn__12141);
            }
            finally
            {
                test___217.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__2473()
        {
            T::Test test___218 = new T::Test();
            try
            {
                SqlBuilder t___12087 = new SqlBuilder();
                t___12087.AppendSafe("v IN (");
                t___12087.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___12087.AppendSafe(")");
                string actual___2474 = t___12087.Accumulated.ToString();
                bool t___12094 = actual___2474 == "v IN ('a', 'b', 'c''d')";
                string fn__12086()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___2474 + ")";
                }
                test___218.Assert(t___12094, (S1::Func<string>) fn__12086);
                SqlBuilder t___12096 = new SqlBuilder();
                t___12096.AppendSafe("v IN (");
                t___12096.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___12096.AppendSafe(")");
                string actual___2477 = t___12096.Accumulated.ToString();
                bool t___12103 = actual___2477 == "v IN (1, 2, 3)";
                string fn__12085()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___2477 + ")";
                }
                test___218.Assert(t___12103, (S1::Func<string>) fn__12085);
                SqlBuilder t___12105 = new SqlBuilder();
                t___12105.AppendSafe("v IN (");
                t___12105.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___12105.AppendSafe(")");
                string actual___2480 = t___12105.Accumulated.ToString();
                bool t___12112 = actual___2480 == "v IN (1, 2)";
                string fn__12084()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___2480 + ")";
                }
                test___218.Assert(t___12112, (S1::Func<string>) fn__12084);
                SqlBuilder t___12114 = new SqlBuilder();
                t___12114.AppendSafe("v IN (");
                t___12114.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___12114.AppendSafe(")");
                string actual___2483 = t___12114.Accumulated.ToString();
                bool t___12121 = actual___2483 == "v IN (1.0, 2.0)";
                string fn__12083()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___2483 + ")";
                }
                test___218.Assert(t___12121, (S1::Func<string>) fn__12083);
                SqlBuilder t___12123 = new SqlBuilder();
                t___12123.AppendSafe("v IN (");
                t___12123.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___12123.AppendSafe(")");
                string actual___2486 = t___12123.Accumulated.ToString();
                bool t___12130 = actual___2486 == "v IN (TRUE, FALSE)";
                string fn__12082()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___2486 + ")";
                }
                test___218.Assert(t___12130, (S1::Func<string>) fn__12082);
                S1::DateTime t___5710;
                t___5710 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___5711 = t___5710;
                S1::DateTime t___5712;
                t___5712 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___5713 = t___5712;
                G::IReadOnlyList<S1::DateTime> dates__1978 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___5711, t___5713);
                SqlBuilder t___12132 = new SqlBuilder();
                t___12132.AppendSafe("v IN (");
                t___12132.AppendDateList(dates__1978);
                t___12132.AppendSafe(")");
                string actual___2489 = t___12132.Accumulated.ToString();
                bool t___12139 = actual___2489 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__12081()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___2489 + ")";
                }
                test___218.Assert(t___12139, (S1::Func<string>) fn__12081);
            }
            finally
            {
                test___218.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__2492()
        {
            T::Test test___219 = new T::Test();
            try
            {
                double nan__1980;
                nan__1980 = 0.0 / 0.0;
                SqlBuilder t___12073 = new SqlBuilder();
                t___12073.AppendSafe("v = ");
                t___12073.AppendFloat64(nan__1980);
                string actual___2493 = t___12073.Accumulated.ToString();
                bool t___12079 = actual___2493 == "v = NULL";
                string fn__12072()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___2493 + ")";
                }
                test___219.Assert(t___12079, (S1::Func<string>) fn__12072);
            }
            finally
            {
                test___219.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__2496()
        {
            T::Test test___220 = new T::Test();
            try
            {
                double inf__1982;
                inf__1982 = 1.0 / 0.0;
                SqlBuilder t___12064 = new SqlBuilder();
                t___12064.AppendSafe("v = ");
                t___12064.AppendFloat64(inf__1982);
                string actual___2497 = t___12064.Accumulated.ToString();
                bool t___12070 = actual___2497 == "v = NULL";
                string fn__12063()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___2497 + ")";
                }
                test___220.Assert(t___12070, (S1::Func<string>) fn__12063);
            }
            finally
            {
                test___220.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__2500()
        {
            T::Test test___221 = new T::Test();
            try
            {
                double ninf__1984;
                ninf__1984 = -1.0 / 0.0;
                SqlBuilder t___12055 = new SqlBuilder();
                t___12055.AppendSafe("v = ");
                t___12055.AppendFloat64(ninf__1984);
                string actual___2501 = t___12055.Accumulated.ToString();
                bool t___12061 = actual___2501 == "v = NULL";
                string fn__12054()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___2501 + ")";
                }
                test___221.Assert(t___12061, (S1::Func<string>) fn__12054);
            }
            finally
            {
                test___221.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__2504()
        {
            T::Test test___222 = new T::Test();
            try
            {
                SqlBuilder t___12030 = new SqlBuilder();
                t___12030.AppendSafe("v = ");
                t___12030.AppendFloat64(3.14);
                string actual___2505 = t___12030.Accumulated.ToString();
                bool t___12036 = actual___2505 == "v = 3.14";
                string fn__12029()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___2505 + ")";
                }
                test___222.Assert(t___12036, (S1::Func<string>) fn__12029);
                SqlBuilder t___12038 = new SqlBuilder();
                t___12038.AppendSafe("v = ");
                t___12038.AppendFloat64(0.0);
                string actual___2508 = t___12038.Accumulated.ToString();
                bool t___12044 = actual___2508 == "v = 0.0";
                string fn__12028()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___2508 + ")";
                }
                test___222.Assert(t___12044, (S1::Func<string>) fn__12028);
                SqlBuilder t___12046 = new SqlBuilder();
                t___12046.AppendSafe("v = ");
                t___12046.AppendFloat64(-42.5);
                string actual___2511 = t___12046.Accumulated.ToString();
                bool t___12052 = actual___2511 == "v = -42.5";
                string fn__12027()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___2511 + ")";
                }
                test___222.Assert(t___12052, (S1::Func<string>) fn__12027);
            }
            finally
            {
                test___222.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__2514()
        {
            T::Test test___223 = new T::Test();
            try
            {
                S1::DateTime t___5606;
                t___5606 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1987 = t___5606;
                SqlBuilder t___12019 = new SqlBuilder();
                t___12019.AppendSafe("v = ");
                t___12019.AppendDate(d__1987);
                string actual___2515 = t___12019.Accumulated.ToString();
                bool t___12025 = actual___2515 == "v = '2024-06-15'";
                string fn__12018()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___2515 + ")";
                }
                test___223.Assert(t___12025, (S1::Func<string>) fn__12018);
            }
            finally
            {
                test___223.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__2518()
        {
            T::Test test___224 = new T::Test();
            try
            {
                string name__1989 = "Someone";
                SqlBuilder t___11987 = new SqlBuilder();
                t___11987.AppendSafe("where p.last_name = ");
                t___11987.AppendString("Someone");
                SqlFragment condition__1990 = t___11987.Accumulated;
                SqlBuilder t___11991 = new SqlBuilder();
                t___11991.AppendSafe("select p.id from person p ");
                t___11991.AppendFragment(condition__1990);
                string actual___2520 = t___11991.Accumulated.ToString();
                bool t___11997 = actual___2520 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__11986()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2520 + ")";
                }
                test___224.Assert(t___11997, (S1::Func<string>) fn__11986);
                SqlBuilder t___11999 = new SqlBuilder();
                t___11999.AppendSafe("select p.id from person p ");
                t___11999.AppendPart(condition__1990.ToSource());
                string actual___2523 = t___11999.Accumulated.ToString();
                bool t___12006 = actual___2523 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__11985()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2523 + ")";
                }
                test___224.Assert(t___12006, (S1::Func<string>) fn__11985);
                G::IReadOnlyList<ISqlPart> parts__1991 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___12010 = new SqlBuilder();
                t___12010.AppendSafe("select ");
                t___12010.AppendPartList(parts__1991);
                string actual___2526 = t___12010.Accumulated.ToString();
                bool t___12016 = actual___2526 == "select 'a''b', 3";
                string fn__11984()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___2526 + ")";
                }
                test___224.Assert(t___12016, (S1::Func<string>) fn__11984);
            }
            finally
            {
                test___224.SoftFailToHard();
            }
        }
    }
}
