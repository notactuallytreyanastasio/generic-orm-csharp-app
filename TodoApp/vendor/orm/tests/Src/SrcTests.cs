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
        internal static ISafeIdentifier csid__636(string name__924)
        {
            ISafeIdentifier t___7681;
            t___7681 = S0::SrcGlobal.SafeIdentifier(name__924);
            return t___7681;
        }
        internal static TableDef userTable__637()
        {
            return new TableDef(csid__636("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__636("name"), new StringField(), false), new FieldDef(csid__636("email"), new StringField(), false), new FieldDef(csid__636("age"), new IntField(), true), new FieldDef(csid__636("score"), new FloatField(), true), new FieldDef(csid__636("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__2009()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__928 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___13823 = userTable__637();
                ISafeIdentifier t___13824 = csid__636("name");
                ISafeIdentifier t___13825 = csid__636("email");
                IChangeset cs__929 = S0::SrcGlobal.Changeset(t___13823, params__928).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13824, t___13825));
                bool t___13828 = C::Mapped.ContainsKey(cs__929.Changes, "name");
                string fn__13818()
                {
                    return "name should be in changes";
                }
                test___31.Assert(t___13828, (S1::Func<string>) fn__13818);
                bool t___13832 = C::Mapped.ContainsKey(cs__929.Changes, "email");
                string fn__13817()
                {
                    return "email should be in changes";
                }
                test___31.Assert(t___13832, (S1::Func<string>) fn__13817);
                bool t___13838 = !C::Mapped.ContainsKey(cs__929.Changes, "admin");
                string fn__13816()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___31.Assert(t___13838, (S1::Func<string>) fn__13816);
                bool t___13840 = cs__929.IsValid;
                string fn__13815()
                {
                    return "should still be valid";
                }
                test___31.Assert(t___13840, (S1::Func<string>) fn__13815);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__2010()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__931 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___13801 = userTable__637();
                ISafeIdentifier t___13802 = csid__636("name");
                IChangeset cs__932 = S0::SrcGlobal.Changeset(t___13801, params__931).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13802)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("email")));
                bool t___13809 = !C::Mapped.ContainsKey(cs__932.Changes, "name");
                string fn__13797()
                {
                    return "name must be excluded by second cast";
                }
                test___32.Assert(t___13809, (S1::Func<string>) fn__13797);
                bool t___13812 = C::Mapped.ContainsKey(cs__932.Changes, "email");
                string fn__13796()
                {
                    return "email should be present";
                }
                test___32.Assert(t___13812, (S1::Func<string>) fn__13796);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__2011()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__934 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___13783 = userTable__637();
                ISafeIdentifier t___13784 = csid__636("name");
                ISafeIdentifier t___13785 = csid__636("email");
                IChangeset cs__935 = S0::SrcGlobal.Changeset(t___13783, params__934).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13784, t___13785));
                bool t___13790 = !C::Mapped.ContainsKey(cs__935.Changes, "name");
                string fn__13779()
                {
                    return "empty name should not be in changes";
                }
                test___33.Assert(t___13790, (S1::Func<string>) fn__13779);
                bool t___13793 = C::Mapped.ContainsKey(cs__935.Changes, "email");
                string fn__13778()
                {
                    return "email should be in changes";
                }
                test___33.Assert(t___13793, (S1::Func<string>) fn__13778);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__2012()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__937 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13765 = userTable__637();
                ISafeIdentifier t___13766 = csid__636("name");
                IChangeset cs__938 = S0::SrcGlobal.Changeset(t___13765, params__937).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13766)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name")));
                bool t___13770 = cs__938.IsValid;
                string fn__13762()
                {
                    return "should be valid";
                }
                test___34.Assert(t___13770, (S1::Func<string>) fn__13762);
                bool t___13776 = cs__938.Errors.Count == 0;
                string fn__13761()
                {
                    return "no errors expected";
                }
                test___34.Assert(t___13776, (S1::Func<string>) fn__13761);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__2013()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__940 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13741 = userTable__637();
                ISafeIdentifier t___13742 = csid__636("name");
                IChangeset cs__941 = S0::SrcGlobal.Changeset(t___13741, params__940).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13742)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name")));
                bool t___13748 = !cs__941.IsValid;
                string fn__13739()
                {
                    return "should be invalid";
                }
                test___35.Assert(t___13748, (S1::Func<string>) fn__13739);
                bool t___13753 = cs__941.Errors.Count == 1;
                string fn__13738()
                {
                    return "should have one error";
                }
                test___35.Assert(t___13753, (S1::Func<string>) fn__13738);
                bool t___13759 = cs__941.Errors[0].Field == "name";
                string fn__13737()
                {
                    return "error should name the field";
                }
                test___35.Assert(t___13759, (S1::Func<string>) fn__13737);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__2014()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__943 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13729 = userTable__637();
                ISafeIdentifier t___13730 = csid__636("name");
                IChangeset cs__944 = S0::SrcGlobal.Changeset(t___13729, params__943).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13730)).ValidateLength(csid__636("name"), 2, 50);
                bool t___13734 = cs__944.IsValid;
                string fn__13726()
                {
                    return "should be valid";
                }
                test___36.Assert(t___13734, (S1::Func<string>) fn__13726);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__2015()
        {
            T::Test test___37 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__946 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___13717 = userTable__637();
                ISafeIdentifier t___13718 = csid__636("name");
                IChangeset cs__947 = S0::SrcGlobal.Changeset(t___13717, params__946).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13718)).ValidateLength(csid__636("name"), 2, 50);
                bool t___13724 = !cs__947.IsValid;
                string fn__13714()
                {
                    return "should be invalid";
                }
                test___37.Assert(t___13724, (S1::Func<string>) fn__13714);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__2016()
        {
            T::Test test___38 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__949 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___13705 = userTable__637();
                ISafeIdentifier t___13706 = csid__636("name");
                IChangeset cs__950 = S0::SrcGlobal.Changeset(t___13705, params__949).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13706)).ValidateLength(csid__636("name"), 2, 10);
                bool t___13712 = !cs__950.IsValid;
                string fn__13702()
                {
                    return "should be invalid";
                }
                test___38.Assert(t___13712, (S1::Func<string>) fn__13702);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__2017()
        {
            T::Test test___39 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__952 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___13694 = userTable__637();
                ISafeIdentifier t___13695 = csid__636("age");
                IChangeset cs__953 = S0::SrcGlobal.Changeset(t___13694, params__952).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13695)).ValidateInt(csid__636("age"));
                bool t___13699 = cs__953.IsValid;
                string fn__13691()
                {
                    return "should be valid";
                }
                test___39.Assert(t___13699, (S1::Func<string>) fn__13691);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__2018()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__955 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___13682 = userTable__637();
                ISafeIdentifier t___13683 = csid__636("age");
                IChangeset cs__956 = S0::SrcGlobal.Changeset(t___13682, params__955).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13683)).ValidateInt(csid__636("age"));
                bool t___13689 = !cs__956.IsValid;
                string fn__13679()
                {
                    return "should be invalid";
                }
                test___40.Assert(t___13689, (S1::Func<string>) fn__13679);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__2019()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__958 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___13671 = userTable__637();
                ISafeIdentifier t___13672 = csid__636("score");
                IChangeset cs__959 = S0::SrcGlobal.Changeset(t___13671, params__958).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13672)).ValidateFloat(csid__636("score"));
                bool t___13676 = cs__959.IsValid;
                string fn__13668()
                {
                    return "should be valid";
                }
                test___41.Assert(t___13676, (S1::Func<string>) fn__13668);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__2020()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__961 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___13660 = userTable__637();
                ISafeIdentifier t___13661 = csid__636("age");
                IChangeset cs__962 = S0::SrcGlobal.Changeset(t___13660, params__961).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13661)).ValidateInt64(csid__636("age"));
                bool t___13665 = cs__962.IsValid;
                string fn__13657()
                {
                    return "should be valid";
                }
                test___42.Assert(t___13665, (S1::Func<string>) fn__13657);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__2021()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__964 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___13648 = userTable__637();
                ISafeIdentifier t___13649 = csid__636("age");
                IChangeset cs__965 = S0::SrcGlobal.Changeset(t___13648, params__964).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13649)).ValidateInt64(csid__636("age"));
                bool t___13655 = !cs__965.IsValid;
                string fn__13645()
                {
                    return "should be invalid";
                }
                test___43.Assert(t___13655, (S1::Func<string>) fn__13645);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__2022()
        {
            T::Test test___44 = new T::Test();
            try
            {
                void fn__13642(string v__967)
                {
                    G::IReadOnlyDictionary<string, string> params__968 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__967)));
                    TableDef t___13634 = userTable__637();
                    ISafeIdentifier t___13635 = csid__636("active");
                    IChangeset cs__969 = S0::SrcGlobal.Changeset(t___13634, params__968).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13635)).ValidateBool(csid__636("active"));
                    bool t___13639 = cs__969.IsValid;
                    string fn__13631()
                    {
                        return "should accept: " + v__967;
                    }
                    test___44.Assert(t___13639, (S1::Func<string>) fn__13631);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__13642);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__2023()
        {
            T::Test test___45 = new T::Test();
            try
            {
                void fn__13628(string v__971)
                {
                    G::IReadOnlyDictionary<string, string> params__972 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__971)));
                    TableDef t___13620 = userTable__637();
                    ISafeIdentifier t___13621 = csid__636("active");
                    IChangeset cs__973 = S0::SrcGlobal.Changeset(t___13620, params__972).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13621)).ValidateBool(csid__636("active"));
                    bool t___13625 = cs__973.IsValid;
                    string fn__13617()
                    {
                        return "should accept: " + v__971;
                    }
                    test___45.Assert(t___13625, (S1::Func<string>) fn__13617);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__13628);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__2024()
        {
            T::Test test___46 = new T::Test();
            try
            {
                void fn__13614(string v__975)
                {
                    G::IReadOnlyDictionary<string, string> params__976 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__975)));
                    TableDef t___13605 = userTable__637();
                    ISafeIdentifier t___13606 = csid__636("active");
                    IChangeset cs__977 = S0::SrcGlobal.Changeset(t___13605, params__976).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13606)).ValidateBool(csid__636("active"));
                    bool t___13612 = !cs__977.IsValid;
                    string fn__13602()
                    {
                        return "should reject ambiguous: " + v__975;
                    }
                    test___46.Assert(t___13612, (S1::Func<string>) fn__13602);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__13614);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__2025()
        {
            T::Test test___47 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__979 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___13590 = userTable__637();
                ISafeIdentifier t___13591 = csid__636("name");
                ISafeIdentifier t___13592 = csid__636("email");
                IChangeset cs__980 = S0::SrcGlobal.Changeset(t___13590, params__979).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13591, t___13592)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name"), csid__636("email")));
                SqlFragment t___7482;
                t___7482 = cs__980.ToInsertSql();
                SqlFragment sqlFrag__981 = t___7482;
                string s__982 = sqlFrag__981.ToString();
                bool t___13599 = s__982.IndexOf("''") >= 0;
                string fn__13586()
                {
                    return "single quote must be doubled: " + s__982;
                }
                test___47.Assert(t___13599, (S1::Func<string>) fn__13586);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__2026()
        {
            T::Test test___48 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__984 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___13570 = userTable__637();
                ISafeIdentifier t___13571 = csid__636("name");
                ISafeIdentifier t___13572 = csid__636("email");
                IChangeset cs__985 = S0::SrcGlobal.Changeset(t___13570, params__984).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13571, t___13572)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name"), csid__636("email")));
                SqlFragment t___7461;
                t___7461 = cs__985.ToInsertSql();
                SqlFragment sqlFrag__986 = t___7461;
                string s__987 = sqlFrag__986.ToString();
                bool t___13579 = s__987.IndexOf("INSERT INTO users") >= 0;
                string fn__13566()
                {
                    return "has INSERT INTO: " + s__987;
                }
                test___48.Assert(t___13579, (S1::Func<string>) fn__13566);
                bool t___13583 = s__987.IndexOf("'Alice'") >= 0;
                string fn__13565()
                {
                    return "has quoted name: " + s__987;
                }
                test___48.Assert(t___13583, (S1::Func<string>) fn__13565);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__2027()
        {
            T::Test test___49 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__989 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___13552 = userTable__637();
                ISafeIdentifier t___13553 = csid__636("name");
                ISafeIdentifier t___13554 = csid__636("email");
                ISafeIdentifier t___13555 = csid__636("age");
                IChangeset cs__990 = S0::SrcGlobal.Changeset(t___13552, params__989).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13553, t___13554, t___13555)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name"), csid__636("email")));
                SqlFragment t___7444;
                t___7444 = cs__990.ToInsertSql();
                SqlFragment sqlFrag__991 = t___7444;
                string s__992 = sqlFrag__991.ToString();
                bool t___13562 = s__992.IndexOf("25") >= 0;
                string fn__13547()
                {
                    return "age rendered unquoted: " + s__992;
                }
                test___49.Assert(t___13562, (S1::Func<string>) fn__13547);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__2028()
        {
            T::Test test___50 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__994 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13540 = userTable__637();
                ISafeIdentifier t___13541 = csid__636("name");
                IChangeset cs__995 = S0::SrcGlobal.Changeset(t___13540, params__994).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13541)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name")));
                bool didBubble__996;
                try
                {
                    cs__995.ToInsertSql();
                    didBubble__996 = false;
                }
                catch
                {
                    didBubble__996 = true;
                }
                string fn__13538()
                {
                    return "invalid changeset should bubble";
                }
                test___50.Assert(didBubble__996, (S1::Func<string>) fn__13538);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__2029()
        {
            T::Test test___51 = new T::Test();
            try
            {
                TableDef strictTable__998 = new TableDef(csid__636("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__636("title"), new StringField(), false), new FieldDef(csid__636("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__999 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___13531 = csid__636("body");
                IChangeset cs__1000 = S0::SrcGlobal.Changeset(strictTable__998, params__999).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13531));
                bool t___13533 = cs__1000.IsValid;
                string fn__13520()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___51.Assert(t___13533, (S1::Func<string>) fn__13520);
                bool didBubble__1001;
                try
                {
                    cs__1000.ToInsertSql();
                    didBubble__1001 = false;
                }
                catch
                {
                    didBubble__1001 = true;
                }
                string fn__13519()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___51.Assert(didBubble__1001, (S1::Func<string>) fn__13519);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__2030()
        {
            T::Test test___52 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1003 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___13510 = userTable__637();
                ISafeIdentifier t___13511 = csid__636("name");
                IChangeset cs__1004 = S0::SrcGlobal.Changeset(t___13510, params__1003).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13511)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name")));
                SqlFragment t___7404;
                t___7404 = cs__1004.ToUpdateSql(42);
                SqlFragment sqlFrag__1005 = t___7404;
                string s__1006 = sqlFrag__1005.ToString();
                bool t___13517 = s__1006 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__13507()
                {
                    return "got: " + s__1006;
                }
                test___52.Assert(t___13517, (S1::Func<string>) fn__13507);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__2031()
        {
            T::Test test___53 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1008 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13500 = userTable__637();
                ISafeIdentifier t___13501 = csid__636("name");
                IChangeset cs__1009 = S0::SrcGlobal.Changeset(t___13500, params__1008).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13501)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__636("name")));
                bool didBubble__1010;
                try
                {
                    cs__1009.ToUpdateSql(1);
                    didBubble__1010 = false;
                }
                catch
                {
                    didBubble__1010 = true;
                }
                string fn__13498()
                {
                    return "invalid changeset should bubble";
                }
                test___53.Assert(didBubble__1010, (S1::Func<string>) fn__13498);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeAddsANewField__2032()
        {
            T::Test test___54 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1012 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13484 = userTable__637();
                ISafeIdentifier t___13485 = csid__636("name");
                IChangeset cs__1013 = S0::SrcGlobal.Changeset(t___13484, params__1012).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13485)).PutChange(csid__636("email"), "alice@example.com");
                bool t___13490 = C::Mapped.ContainsKey(cs__1013.Changes, "email");
                string fn__13481()
                {
                    return "email should be in changes";
                }
                test___54.Assert(t___13490, (S1::Func<string>) fn__13481);
                bool t___13496 = C::Mapped.GetOrDefault(cs__1013.Changes, "email", "") == "alice@example.com";
                string fn__13480()
                {
                    return "email value";
                }
                test___54.Assert(t___13496, (S1::Func<string>) fn__13480);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeOverwritesExistingField__2033()
        {
            T::Test test___55 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1015 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13470 = userTable__637();
                ISafeIdentifier t___13471 = csid__636("name");
                IChangeset cs__1016 = S0::SrcGlobal.Changeset(t___13470, params__1015).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13471)).PutChange(csid__636("name"), "Bob");
                bool t___13478 = C::Mapped.GetOrDefault(cs__1016.Changes, "name", "") == "Bob";
                string fn__13467()
                {
                    return "name should be overwritten";
                }
                test___55.Assert(t___13478, (S1::Func<string>) fn__13467);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeValueAppearsInToInsertSql__2034()
        {
            T::Test test___56 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1018 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___13456 = userTable__637();
                ISafeIdentifier t___13457 = csid__636("name");
                ISafeIdentifier t___13458 = csid__636("email");
                IChangeset cs__1019 = S0::SrcGlobal.Changeset(t___13456, params__1018).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13457, t___13458)).PutChange(csid__636("name"), "Bob");
                SqlFragment t___7359;
                t___7359 = cs__1019.ToInsertSql();
                SqlFragment t___7360 = t___7359;
                string s__1020 = t___7360.ToString();
                bool t___13464 = s__1020.IndexOf("'Bob'") >= 0;
                string fn__13452()
                {
                    return "should use putChange value: " + s__1020;
                }
                test___56.Assert(t___13464, (S1::Func<string>) fn__13452);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void getChangeReturnsValueForExistingField__2035()
        {
            T::Test test___57 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1022 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13445 = userTable__637();
                ISafeIdentifier t___13446 = csid__636("name");
                IChangeset cs__1023 = S0::SrcGlobal.Changeset(t___13445, params__1022).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13446));
                string t___7347;
                t___7347 = cs__1023.GetChange(csid__636("name"));
                string val__1024 = t___7347;
                bool t___13450 = val__1024 == "Alice";
                string fn__13442()
                {
                    return "should return Alice";
                }
                test___57.Assert(t___13450, (S1::Func<string>) fn__13442);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void getChangeBubblesOnMissingField__2036()
        {
            T::Test test___58 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1026 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13436 = userTable__637();
                ISafeIdentifier t___13437 = csid__636("name");
                IChangeset cs__1027 = S0::SrcGlobal.Changeset(t___13436, params__1026).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13437));
                bool didBubble__1028;
                try
                {
                    cs__1027.GetChange(csid__636("email"));
                    didBubble__1028 = false;
                }
                catch
                {
                    didBubble__1028 = true;
                }
                string fn__13433()
                {
                    return "should bubble for missing field";
                }
                test___58.Assert(didBubble__1028, (S1::Func<string>) fn__13433);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeRemovesField__2037()
        {
            T::Test test___59 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1030 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___13418 = userTable__637();
                ISafeIdentifier t___13419 = csid__636("name");
                ISafeIdentifier t___13420 = csid__636("email");
                IChangeset cs__1031 = S0::SrcGlobal.Changeset(t___13418, params__1030).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13419, t___13420)).DeleteChange(csid__636("email"));
                bool t___13427 = !C::Mapped.ContainsKey(cs__1031.Changes, "email");
                string fn__13414()
                {
                    return "email should be removed";
                }
                test___59.Assert(t___13427, (S1::Func<string>) fn__13414);
                bool t___13430 = C::Mapped.ContainsKey(cs__1031.Changes, "name");
                string fn__13413()
                {
                    return "name should remain";
                }
                test___59.Assert(t___13430, (S1::Func<string>) fn__13413);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeOnNonexistentFieldIsNoOp__2038()
        {
            T::Test test___60 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1033 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13401 = userTable__637();
                ISafeIdentifier t___13402 = csid__636("name");
                IChangeset cs__1034 = S0::SrcGlobal.Changeset(t___13401, params__1033).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13402)).DeleteChange(csid__636("email"));
                bool t___13407 = C::Mapped.ContainsKey(cs__1034.Changes, "name");
                string fn__13398()
                {
                    return "name should still be present";
                }
                test___60.Assert(t___13407, (S1::Func<string>) fn__13398);
                bool t___13410 = cs__1034.IsValid;
                string fn__13397()
                {
                    return "should still be valid";
                }
                test___60.Assert(t___13410, (S1::Func<string>) fn__13397);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionPassesWhenValueInList__2039()
        {
            T::Test test___61 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1036 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "admin")));
                TableDef t___13389 = userTable__637();
                ISafeIdentifier t___13390 = csid__636("name");
                IChangeset cs__1037 = S0::SrcGlobal.Changeset(t___13389, params__1036).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13390)).ValidateInclusion(csid__636("name"), C::Listed.CreateReadOnlyList<string>("admin", "user", "guest"));
                bool t___13394 = cs__1037.IsValid;
                string fn__13386()
                {
                    return "should be valid";
                }
                test___61.Assert(t___13394, (S1::Func<string>) fn__13386);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionFailsWhenValueNotInList__2040()
        {
            T::Test test___62 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1039 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "hacker")));
                TableDef t___13371 = userTable__637();
                ISafeIdentifier t___13372 = csid__636("name");
                IChangeset cs__1040 = S0::SrcGlobal.Changeset(t___13371, params__1039).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13372)).ValidateInclusion(csid__636("name"), C::Listed.CreateReadOnlyList<string>("admin", "user", "guest"));
                bool t___13378 = !cs__1040.IsValid;
                string fn__13368()
                {
                    return "should be invalid";
                }
                test___62.Assert(t___13378, (S1::Func<string>) fn__13368);
                bool t___13384 = cs__1040.Errors[0].Field == "name";
                string fn__13367()
                {
                    return "error on name";
                }
                test___62.Assert(t___13384, (S1::Func<string>) fn__13367);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionSkipsWhenFieldNotInChanges__2041()
        {
            T::Test test___63 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1042 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13359 = userTable__637();
                ISafeIdentifier t___13360 = csid__636("name");
                IChangeset cs__1043 = S0::SrcGlobal.Changeset(t___13359, params__1042).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13360)).ValidateInclusion(csid__636("name"), C::Listed.CreateReadOnlyList<string>("admin", "user"));
                bool t___13364 = cs__1043.IsValid;
                string fn__13357()
                {
                    return "should be valid when field absent";
                }
                test___63.Assert(t___13364, (S1::Func<string>) fn__13357);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionPassesWhenValueNotInList__2042()
        {
            T::Test test___64 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1045 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___13349 = userTable__637();
                ISafeIdentifier t___13350 = csid__636("name");
                IChangeset cs__1046 = S0::SrcGlobal.Changeset(t___13349, params__1045).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13350)).ValidateExclusion(csid__636("name"), C::Listed.CreateReadOnlyList<string>("root", "admin", "superuser"));
                bool t___13354 = cs__1046.IsValid;
                string fn__13346()
                {
                    return "should be valid";
                }
                test___64.Assert(t___13354, (S1::Func<string>) fn__13346);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionFailsWhenValueInList__2043()
        {
            T::Test test___65 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1048 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "admin")));
                TableDef t___13331 = userTable__637();
                ISafeIdentifier t___13332 = csid__636("name");
                IChangeset cs__1049 = S0::SrcGlobal.Changeset(t___13331, params__1048).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13332)).ValidateExclusion(csid__636("name"), C::Listed.CreateReadOnlyList<string>("root", "admin", "superuser"));
                bool t___13338 = !cs__1049.IsValid;
                string fn__13328()
                {
                    return "should be invalid";
                }
                test___65.Assert(t___13338, (S1::Func<string>) fn__13328);
                bool t___13344 = cs__1049.Errors[0].Field == "name";
                string fn__13327()
                {
                    return "error on name";
                }
                test___65.Assert(t___13344, (S1::Func<string>) fn__13327);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionSkipsWhenFieldNotInChanges__2044()
        {
            T::Test test___66 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1051 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13319 = userTable__637();
                ISafeIdentifier t___13320 = csid__636("name");
                IChangeset cs__1052 = S0::SrcGlobal.Changeset(t___13319, params__1051).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13320)).ValidateExclusion(csid__636("name"), C::Listed.CreateReadOnlyList<string>("root", "admin"));
                bool t___13324 = cs__1052.IsValid;
                string fn__13317()
                {
                    return "should be valid when field absent";
                }
                test___66.Assert(t___13324, (S1::Func<string>) fn__13317);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanPasses__2045()
        {
            T::Test test___67 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1054 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___13308 = userTable__637();
                ISafeIdentifier t___13309 = csid__636("age");
                IChangeset cs__1055 = S0::SrcGlobal.Changeset(t___13308, params__1054).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13309)).ValidateNumber(csid__636("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___13314 = cs__1055.IsValid;
                string fn__13305()
                {
                    return "25 > 18 should pass";
                }
                test___67.Assert(t___13314, (S1::Func<string>) fn__13305);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanFails__2046()
        {
            T::Test test___68 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1057 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "15")));
                TableDef t___13295 = userTable__637();
                ISafeIdentifier t___13296 = csid__636("age");
                IChangeset cs__1058 = S0::SrcGlobal.Changeset(t___13295, params__1057).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13296)).ValidateNumber(csid__636("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___13303 = !cs__1058.IsValid;
                string fn__13292()
                {
                    return "15 > 18 should fail";
                }
                test___68.Assert(t___13303, (S1::Func<string>) fn__13292);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanPasses__2047()
        {
            T::Test test___69 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1060 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "8.5")));
                TableDef t___13283 = userTable__637();
                ISafeIdentifier t___13284 = csid__636("score");
                IChangeset cs__1061 = S0::SrcGlobal.Changeset(t___13283, params__1060).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13284)).ValidateNumber(csid__636("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___13289 = cs__1061.IsValid;
                string fn__13280()
                {
                    return "8.5 < 10 should pass";
                }
                test___69.Assert(t___13289, (S1::Func<string>) fn__13280);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanFails__2048()
        {
            T::Test test___70 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1063 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "12.0")));
                TableDef t___13270 = userTable__637();
                ISafeIdentifier t___13271 = csid__636("score");
                IChangeset cs__1064 = S0::SrcGlobal.Changeset(t___13270, params__1063).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13271)).ValidateNumber(csid__636("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___13278 = !cs__1064.IsValid;
                string fn__13267()
                {
                    return "12 < 10 should fail";
                }
                test___70.Assert(t___13278, (S1::Func<string>) fn__13267);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanOrEqualBoundary__2049()
        {
            T::Test test___71 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1066 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "18")));
                TableDef t___13258 = userTable__637();
                ISafeIdentifier t___13259 = csid__636("age");
                IChangeset cs__1067 = S0::SrcGlobal.Changeset(t___13258, params__1066).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13259)).ValidateNumber(csid__636("age"), new NumberValidationOpts(null, null, 18.0, null, null));
                bool t___13264 = cs__1067.IsValid;
                string fn__13255()
                {
                    return "18 >= 18 should pass";
                }
                test___71.Assert(t___13264, (S1::Func<string>) fn__13255);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberCombinedOptions__2050()
        {
            T::Test test___72 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1069 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "5.0")));
                TableDef t___13246 = userTable__637();
                ISafeIdentifier t___13247 = csid__636("score");
                IChangeset cs__1070 = S0::SrcGlobal.Changeset(t___13246, params__1069).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13247)).ValidateNumber(csid__636("score"), new NumberValidationOpts(0.0, 10.0, null, null, null));
                bool t___13252 = cs__1070.IsValid;
                string fn__13243()
                {
                    return "5 > 0 and < 10 should pass";
                }
                test___72.Assert(t___13252, (S1::Func<string>) fn__13243);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberNonNumericValue__2051()
        {
            T::Test test___73 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1072 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "abc")));
                TableDef t___13227 = userTable__637();
                ISafeIdentifier t___13228 = csid__636("age");
                IChangeset cs__1073 = S0::SrcGlobal.Changeset(t___13227, params__1072).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13228)).ValidateNumber(csid__636("age"), new NumberValidationOpts(0.0, null, null, null, null));
                bool t___13235 = !cs__1073.IsValid;
                string fn__13224()
                {
                    return "non-numeric should fail";
                }
                test___73.Assert(t___13235, (S1::Func<string>) fn__13224);
                bool t___13241 = cs__1073.Errors[0].Message == "must be a number";
                string fn__13223()
                {
                    return "correct error message";
                }
                test___73.Assert(t___13241, (S1::Func<string>) fn__13223);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberSkipsWhenFieldNotInChanges__2052()
        {
            T::Test test___74 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1075 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13214 = userTable__637();
                ISafeIdentifier t___13215 = csid__636("age");
                IChangeset cs__1076 = S0::SrcGlobal.Changeset(t___13214, params__1075).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13215)).ValidateNumber(csid__636("age"), new NumberValidationOpts(0.0, null, null, null, null));
                bool t___13220 = cs__1076.IsValid;
                string fn__13212()
                {
                    return "should be valid when field absent";
                }
                test___74.Assert(t___13220, (S1::Func<string>) fn__13212);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptancePassesForTrueValues__2053()
        {
            T::Test test___75 = new T::Test();
            try
            {
                void fn__13209(string v__1078)
                {
                    G::IReadOnlyDictionary<string, string> params__1079 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1078)));
                    TableDef t___13201 = userTable__637();
                    ISafeIdentifier t___13202 = csid__636("active");
                    IChangeset cs__1080 = S0::SrcGlobal.Changeset(t___13201, params__1079).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13202)).ValidateAcceptance(csid__636("active"));
                    bool t___13206 = cs__1080.IsValid;
                    string fn__13198()
                    {
                        return "should accept: " + v__1078;
                    }
                    test___75.Assert(t___13206, (S1::Func<string>) fn__13198);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__13209);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptanceFailsForNonTrueValues__2054()
        {
            T::Test test___76 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1082 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", "false")));
                TableDef t___13183 = userTable__637();
                ISafeIdentifier t___13184 = csid__636("active");
                IChangeset cs__1083 = S0::SrcGlobal.Changeset(t___13183, params__1082).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13184)).ValidateAcceptance(csid__636("active"));
                bool t___13190 = !cs__1083.IsValid;
                string fn__13180()
                {
                    return "false should not be accepted";
                }
                test___76.Assert(t___13190, (S1::Func<string>) fn__13180);
                bool t___13196 = cs__1083.Errors[0].Message == "must be accepted";
                string fn__13179()
                {
                    return "correct message";
                }
                test___76.Assert(t___13196, (S1::Func<string>) fn__13179);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationPassesWhenFieldsMatch__2055()
        {
            T::Test test___77 = new T::Test();
            try
            {
                TableDef tbl__1085 = new TableDef(csid__636("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__636("password"), new StringField(), false), new FieldDef(csid__636("password_confirmation"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__1086 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123"), new G::KeyValuePair<string, string>("password_confirmation", "secret123")));
                ISafeIdentifier t___13170 = csid__636("password");
                ISafeIdentifier t___13171 = csid__636("password_confirmation");
                IChangeset cs__1087 = S0::SrcGlobal.Changeset(tbl__1085, params__1086).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13170, t___13171)).ValidateConfirmation(csid__636("password"), csid__636("password_confirmation"));
                bool t___13176 = cs__1087.IsValid;
                string fn__13158()
                {
                    return "matching fields should pass";
                }
                test___77.Assert(t___13176, (S1::Func<string>) fn__13158);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationFailsWhenFieldsDiffer__2056()
        {
            T::Test test___78 = new T::Test();
            try
            {
                TableDef tbl__1089 = new TableDef(csid__636("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__636("password"), new StringField(), false), new FieldDef(csid__636("password_confirmation"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__1090 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123"), new G::KeyValuePair<string, string>("password_confirmation", "wrong456")));
                ISafeIdentifier t___13142 = csid__636("password");
                ISafeIdentifier t___13143 = csid__636("password_confirmation");
                IChangeset cs__1091 = S0::SrcGlobal.Changeset(tbl__1089, params__1090).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13142, t___13143)).ValidateConfirmation(csid__636("password"), csid__636("password_confirmation"));
                bool t___13150 = !cs__1091.IsValid;
                string fn__13130()
                {
                    return "mismatched fields should fail";
                }
                test___78.Assert(t___13150, (S1::Func<string>) fn__13130);
                bool t___13156 = cs__1091.Errors[0].Field == "password_confirmation";
                string fn__13129()
                {
                    return "error on confirmation field";
                }
                test___78.Assert(t___13156, (S1::Func<string>) fn__13129);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationFailsWhenConfirmationMissing__2057()
        {
            T::Test test___79 = new T::Test();
            try
            {
                TableDef tbl__1093 = new TableDef(csid__636("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__636("password"), new StringField(), false), new FieldDef(csid__636("password_confirmation"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__1094 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123")));
                ISafeIdentifier t___13120 = csid__636("password");
                IChangeset cs__1095 = S0::SrcGlobal.Changeset(tbl__1093, params__1094).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13120)).ValidateConfirmation(csid__636("password"), csid__636("password_confirmation"));
                bool t___13127 = !cs__1095.IsValid;
                string fn__13109()
                {
                    return "missing confirmation should fail";
                }
                test___79.Assert(t___13127, (S1::Func<string>) fn__13109);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsPassesWhenSubstringFound__2058()
        {
            T::Test test___80 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1097 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___13101 = userTable__637();
                ISafeIdentifier t___13102 = csid__636("email");
                IChangeset cs__1098 = S0::SrcGlobal.Changeset(t___13101, params__1097).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13102)).ValidateContains(csid__636("email"), "@");
                bool t___13106 = cs__1098.IsValid;
                string fn__13098()
                {
                    return "should pass when @ present";
                }
                test___80.Assert(t___13106, (S1::Func<string>) fn__13098);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsFailsWhenSubstringNotFound__2059()
        {
            T::Test test___81 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1100 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice-example.com")));
                TableDef t___13089 = userTable__637();
                ISafeIdentifier t___13090 = csid__636("email");
                IChangeset cs__1101 = S0::SrcGlobal.Changeset(t___13089, params__1100).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13090)).ValidateContains(csid__636("email"), "@");
                bool t___13096 = !cs__1101.IsValid;
                string fn__13086()
                {
                    return "should fail when @ absent";
                }
                test___81.Assert(t___13096, (S1::Func<string>) fn__13086);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsSkipsWhenFieldNotInChanges__2060()
        {
            T::Test test___82 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1103 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___13078 = userTable__637();
                ISafeIdentifier t___13079 = csid__636("email");
                IChangeset cs__1104 = S0::SrcGlobal.Changeset(t___13078, params__1103).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13079)).ValidateContains(csid__636("email"), "@");
                bool t___13083 = cs__1104.IsValid;
                string fn__13076()
                {
                    return "should be valid when field absent";
                }
                test___82.Assert(t___13083, (S1::Func<string>) fn__13076);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithPasses__2061()
        {
            T::Test test___83 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1106 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Dr. Smith")));
                TableDef t___13068 = userTable__637();
                ISafeIdentifier t___13069 = csid__636("name");
                IChangeset cs__1107 = S0::SrcGlobal.Changeset(t___13068, params__1106).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13069)).ValidateStartsWith(csid__636("name"), "Dr.");
                bool t___13073 = cs__1107.IsValid;
                string fn__13065()
                {
                    return "should pass for Dr. prefix";
                }
                test___83.Assert(t___13073, (S1::Func<string>) fn__13065);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithFails__2062()
        {
            T::Test test___84 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1109 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Mr. Smith")));
                TableDef t___13056 = userTable__637();
                ISafeIdentifier t___13057 = csid__636("name");
                IChangeset cs__1110 = S0::SrcGlobal.Changeset(t___13056, params__1109).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13057)).ValidateStartsWith(csid__636("name"), "Dr.");
                bool t___13063 = !cs__1110.IsValid;
                string fn__13053()
                {
                    return "should fail for Mr. prefix";
                }
                test___84.Assert(t___13063, (S1::Func<string>) fn__13053);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithPasses__2063()
        {
            T::Test test___85 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1112 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___13045 = userTable__637();
                ISafeIdentifier t___13046 = csid__636("email");
                IChangeset cs__1113 = S0::SrcGlobal.Changeset(t___13045, params__1112).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13046)).ValidateEndsWith(csid__636("email"), ".com");
                bool t___13050 = cs__1113.IsValid;
                string fn__13042()
                {
                    return "should pass for .com suffix";
                }
                test___85.Assert(t___13050, (S1::Func<string>) fn__13042);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithFails__2064()
        {
            T::Test test___86 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1115 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.org")));
                TableDef t___13033 = userTable__637();
                ISafeIdentifier t___13034 = csid__636("email");
                IChangeset cs__1116 = S0::SrcGlobal.Changeset(t___13033, params__1115).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13034)).ValidateEndsWith(csid__636("email"), ".com");
                bool t___13040 = !cs__1116.IsValid;
                string fn__13030()
                {
                    return "should fail for .org when expecting .com";
                }
                test___86.Assert(t___13040, (S1::Func<string>) fn__13030);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithHandlesRepeatedSuffixCorrectly__2065()
        {
            T::Test test___87 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1118 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "abcabc")));
                TableDef t___13022 = userTable__637();
                ISafeIdentifier t___13023 = csid__636("name");
                IChangeset cs__1119 = S0::SrcGlobal.Changeset(t___13022, params__1118).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13023)).ValidateEndsWith(csid__636("name"), "abc");
                bool t___13027 = cs__1119.IsValid;
                string fn__13019()
                {
                    return "abcabc should end with abc";
                }
                test___87.Assert(t___13027, (S1::Func<string>) fn__13019);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__638(string name__1464)
        {
            ISafeIdentifier t___6485;
            t___6485 = S0::SrcGlobal.SafeIdentifier(name__1464);
            return t___6485;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__2147()
        {
            T::Test test___88 = new T::Test();
            try
            {
                Query q__1467 = S0::SrcGlobal.From(sid__638("users"));
                bool t___12504 = q__1467.ToSql().ToString() == "SELECT * FROM users";
                string fn__12499()
                {
                    return "bare query";
                }
                test___88.Assert(t___12504, (S1::Func<string>) fn__12499);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__2148()
        {
            T::Test test___89 = new T::Test();
            try
            {
                ISafeIdentifier t___12490 = sid__638("users");
                ISafeIdentifier t___12491 = sid__638("id");
                ISafeIdentifier t___12492 = sid__638("name");
                Query q__1469 = S0::SrcGlobal.From(t___12490).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12491, t___12492));
                bool t___12497 = q__1469.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__12489()
                {
                    return "select columns";
                }
                test___89.Assert(t___12497, (S1::Func<string>) fn__12489);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__2149()
        {
            T::Test test___90 = new T::Test();
            try
            {
                ISafeIdentifier t___12478 = sid__638("users");
                SqlBuilder t___12479 = new SqlBuilder();
                t___12479.AppendSafe("age > ");
                t___12479.AppendInt32(18);
                SqlFragment t___12482 = t___12479.Accumulated;
                Query q__1471 = S0::SrcGlobal.From(t___12478).Where(t___12482);
                bool t___12487 = q__1471.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__12477()
                {
                    return "where int";
                }
                test___90.Assert(t___12487, (S1::Func<string>) fn__12477);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__2151()
        {
            T::Test test___91 = new T::Test();
            try
            {
                ISafeIdentifier t___12466 = sid__638("users");
                SqlBuilder t___12467 = new SqlBuilder();
                t___12467.AppendSafe("active = ");
                t___12467.AppendBoolean(true);
                SqlFragment t___12470 = t___12467.Accumulated;
                Query q__1473 = S0::SrcGlobal.From(t___12466).Where(t___12470);
                bool t___12475 = q__1473.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__12465()
                {
                    return "where bool";
                }
                test___91.Assert(t___12475, (S1::Func<string>) fn__12465);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__2153()
        {
            T::Test test___92 = new T::Test();
            try
            {
                ISafeIdentifier t___12449 = sid__638("users");
                SqlBuilder t___12450 = new SqlBuilder();
                t___12450.AppendSafe("age > ");
                t___12450.AppendInt32(18);
                SqlFragment t___12453 = t___12450.Accumulated;
                Query t___12454 = S0::SrcGlobal.From(t___12449).Where(t___12453);
                SqlBuilder t___12455 = new SqlBuilder();
                t___12455.AppendSafe("active = ");
                t___12455.AppendBoolean(true);
                Query q__1475 = t___12454.Where(t___12455.Accumulated);
                bool t___12463 = q__1475.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__12448()
                {
                    return "chained where";
                }
                test___92.Assert(t___12463, (S1::Func<string>) fn__12448);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__2156()
        {
            T::Test test___93 = new T::Test();
            try
            {
                ISafeIdentifier t___12440 = sid__638("users");
                ISafeIdentifier t___12441 = sid__638("name");
                Query q__1477 = S0::SrcGlobal.From(t___12440).OrderBy(t___12441, true);
                bool t___12446 = q__1477.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__12439()
                {
                    return "order asc";
                }
                test___93.Assert(t___12446, (S1::Func<string>) fn__12439);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__2157()
        {
            T::Test test___94 = new T::Test();
            try
            {
                ISafeIdentifier t___12431 = sid__638("users");
                ISafeIdentifier t___12432 = sid__638("created_at");
                Query q__1479 = S0::SrcGlobal.From(t___12431).OrderBy(t___12432, false);
                bool t___12437 = q__1479.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__12430()
                {
                    return "order desc";
                }
                test___94.Assert(t___12437, (S1::Func<string>) fn__12430);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__2158()
        {
            T::Test test___95 = new T::Test();
            try
            {
                Query t___6419;
                t___6419 = S0::SrcGlobal.From(sid__638("users")).Limit(10);
                Query t___6420;
                t___6420 = t___6419.Offset(20);
                Query q__1481 = t___6420;
                bool t___12428 = q__1481.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__12423()
                {
                    return "limit/offset";
                }
                test___95.Assert(t___12428, (S1::Func<string>) fn__12423);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__2159()
        {
            T::Test test___96 = new T::Test();
            try
            {
                bool didBubble__1483;
                try
                {
                    S0::SrcGlobal.From(sid__638("users")).Limit(-1);
                    didBubble__1483 = false;
                }
                catch
                {
                    didBubble__1483 = true;
                }
                string fn__12419()
                {
                    return "negative limit should bubble";
                }
                test___96.Assert(didBubble__1483, (S1::Func<string>) fn__12419);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__2160()
        {
            T::Test test___97 = new T::Test();
            try
            {
                bool didBubble__1485;
                try
                {
                    S0::SrcGlobal.From(sid__638("users")).Offset(-1);
                    didBubble__1485 = false;
                }
                catch
                {
                    didBubble__1485 = true;
                }
                string fn__12415()
                {
                    return "negative offset should bubble";
                }
                test___97.Assert(didBubble__1485, (S1::Func<string>) fn__12415);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__2161()
        {
            T::Test test___98 = new T::Test();
            try
            {
                int minAge__1487 = 21;
                ISafeIdentifier t___12393 = sid__638("users");
                ISafeIdentifier t___12394 = sid__638("id");
                ISafeIdentifier t___12395 = sid__638("name");
                ISafeIdentifier t___12396 = sid__638("email");
                Query t___12397 = S0::SrcGlobal.From(t___12393).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___12394, t___12395, t___12396));
                SqlBuilder t___12398 = new SqlBuilder();
                t___12398.AppendSafe("age >= ");
                t___12398.AppendInt32(21);
                Query t___12402 = t___12397.Where(t___12398.Accumulated);
                SqlBuilder t___12403 = new SqlBuilder();
                t___12403.AppendSafe("active = ");
                t___12403.AppendBoolean(true);
                Query t___6405;
                t___6405 = t___12402.Where(t___12403.Accumulated).OrderBy(sid__638("name"), true).Limit(25);
                Query t___6406;
                t___6406 = t___6405.Offset(0);
                Query q__1488 = t___6406;
                bool t___12413 = q__1488.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__12392()
                {
                    return "complex query";
                }
                test___98.Assert(t___12413, (S1::Func<string>) fn__12392);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__2164()
        {
            T::Test test___99 = new T::Test();
            try
            {
                Query q__1490 = S0::SrcGlobal.From(sid__638("users"));
                SqlFragment t___6382;
                t___6382 = q__1490.SafeToSql(100);
                SqlFragment t___6383 = t___6382;
                string s__1491 = t___6383.ToString();
                bool t___12390 = s__1491 == "SELECT * FROM users LIMIT 100";
                string fn__12386()
                {
                    return "should have limit: " + s__1491;
                }
                test___99.Assert(t___12390, (S1::Func<string>) fn__12386);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__2165()
        {
            T::Test test___100 = new T::Test();
            try
            {
                Query t___6374;
                t___6374 = S0::SrcGlobal.From(sid__638("users")).Limit(5);
                Query q__1493 = t___6374;
                SqlFragment t___6377;
                t___6377 = q__1493.SafeToSql(100);
                SqlFragment t___6378 = t___6377;
                string s__1494 = t___6378.ToString();
                bool t___12384 = s__1494 == "SELECT * FROM users LIMIT 5";
                string fn__12380()
                {
                    return "explicit limit preserved: " + s__1494;
                }
                test___100.Assert(t___12384, (S1::Func<string>) fn__12380);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__2166()
        {
            T::Test test___101 = new T::Test();
            try
            {
                bool didBubble__1496;
                try
                {
                    S0::SrcGlobal.From(sid__638("users")).SafeToSql(-1);
                    didBubble__1496 = false;
                }
                catch
                {
                    didBubble__1496 = true;
                }
                string fn__12376()
                {
                    return "negative defaultLimit should bubble";
                }
                test___101.Assert(didBubble__1496, (S1::Func<string>) fn__12376);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__2167()
        {
            T::Test test___102 = new T::Test();
            try
            {
                string evil__1498 = "'; DROP TABLE users; --";
                ISafeIdentifier t___12360 = sid__638("users");
                SqlBuilder t___12361 = new SqlBuilder();
                t___12361.AppendSafe("name = ");
                t___12361.AppendString("'; DROP TABLE users; --");
                SqlFragment t___12364 = t___12361.Accumulated;
                Query q__1499 = S0::SrcGlobal.From(t___12360).Where(t___12364);
                string s__1500 = q__1499.ToSql().ToString();
                bool t___12369 = s__1500.IndexOf("''") >= 0;
                string fn__12359()
                {
                    return "quotes must be doubled: " + s__1500;
                }
                test___102.Assert(t___12369, (S1::Func<string>) fn__12359);
                bool t___12373 = s__1500.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__12358()
                {
                    return "structure intact: " + s__1500;
                }
                test___102.Assert(t___12373, (S1::Func<string>) fn__12358);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__2169()
        {
            T::Test test___103 = new T::Test();
            try
            {
                string attack__1502 = "users; DROP TABLE users; --";
                bool didBubble__1503;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__1503 = false;
                }
                catch
                {
                    didBubble__1503 = true;
                }
                string fn__12355()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___103.Assert(didBubble__1503, (S1::Func<string>) fn__12355);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__2170()
        {
            T::Test test___104 = new T::Test();
            try
            {
                ISafeIdentifier t___12344 = sid__638("users");
                ISafeIdentifier t___12345 = sid__638("orders");
                SqlBuilder t___12346 = new SqlBuilder();
                t___12346.AppendSafe("users.id = orders.user_id");
                SqlFragment t___12348 = t___12346.Accumulated;
                Query q__1505 = S0::SrcGlobal.From(t___12344).InnerJoin(t___12345, t___12348);
                bool t___12353 = q__1505.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__12343()
                {
                    return "inner join";
                }
                test___104.Assert(t___12353, (S1::Func<string>) fn__12343);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__2172()
        {
            T::Test test___105 = new T::Test();
            try
            {
                ISafeIdentifier t___12332 = sid__638("users");
                ISafeIdentifier t___12333 = sid__638("profiles");
                SqlBuilder t___12334 = new SqlBuilder();
                t___12334.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___12336 = t___12334.Accumulated;
                Query q__1507 = S0::SrcGlobal.From(t___12332).LeftJoin(t___12333, t___12336);
                bool t___12341 = q__1507.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__12331()
                {
                    return "left join";
                }
                test___105.Assert(t___12341, (S1::Func<string>) fn__12331);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__2174()
        {
            T::Test test___106 = new T::Test();
            try
            {
                ISafeIdentifier t___12320 = sid__638("orders");
                ISafeIdentifier t___12321 = sid__638("users");
                SqlBuilder t___12322 = new SqlBuilder();
                t___12322.AppendSafe("orders.user_id = users.id");
                SqlFragment t___12324 = t___12322.Accumulated;
                Query q__1509 = S0::SrcGlobal.From(t___12320).RightJoin(t___12321, t___12324);
                bool t___12329 = q__1509.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__12319()
                {
                    return "right join";
                }
                test___106.Assert(t___12329, (S1::Func<string>) fn__12319);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__2176()
        {
            T::Test test___107 = new T::Test();
            try
            {
                ISafeIdentifier t___12308 = sid__638("users");
                ISafeIdentifier t___12309 = sid__638("orders");
                SqlBuilder t___12310 = new SqlBuilder();
                t___12310.AppendSafe("users.id = orders.user_id");
                SqlFragment t___12312 = t___12310.Accumulated;
                Query q__1511 = S0::SrcGlobal.From(t___12308).FullJoin(t___12309, t___12312);
                bool t___12317 = q__1511.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__12307()
                {
                    return "full join";
                }
                test___107.Assert(t___12317, (S1::Func<string>) fn__12307);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__2178()
        {
            T::Test test___108 = new T::Test();
            try
            {
                ISafeIdentifier t___12291 = sid__638("users");
                ISafeIdentifier t___12292 = sid__638("orders");
                SqlBuilder t___12293 = new SqlBuilder();
                t___12293.AppendSafe("users.id = orders.user_id");
                SqlFragment t___12295 = t___12293.Accumulated;
                Query t___12296 = S0::SrcGlobal.From(t___12291).InnerJoin(t___12292, t___12295);
                ISafeIdentifier t___12297 = sid__638("profiles");
                SqlBuilder t___12298 = new SqlBuilder();
                t___12298.AppendSafe("users.id = profiles.user_id");
                Query q__1513 = t___12296.LeftJoin(t___12297, t___12298.Accumulated);
                bool t___12305 = q__1513.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__12290()
                {
                    return "chained joins";
                }
                test___108.Assert(t___12305, (S1::Func<string>) fn__12290);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__2181()
        {
            T::Test test___109 = new T::Test();
            try
            {
                ISafeIdentifier t___12272 = sid__638("users");
                ISafeIdentifier t___12273 = sid__638("orders");
                SqlBuilder t___12274 = new SqlBuilder();
                t___12274.AppendSafe("users.id = orders.user_id");
                SqlFragment t___12276 = t___12274.Accumulated;
                Query t___12277 = S0::SrcGlobal.From(t___12272).InnerJoin(t___12273, t___12276);
                SqlBuilder t___12278 = new SqlBuilder();
                t___12278.AppendSafe("orders.total > ");
                t___12278.AppendInt32(100);
                Query t___6289;
                t___6289 = t___12277.Where(t___12278.Accumulated).OrderBy(sid__638("name"), true).Limit(10);
                Query q__1515 = t___6289;
                bool t___12288 = q__1515.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__12271()
                {
                    return "join with where/order/limit";
                }
                test___109.Assert(t___12288, (S1::Func<string>) fn__12271);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__2184()
        {
            T::Test test___110 = new T::Test();
            try
            {
                SqlFragment c__1517 = S0::SrcGlobal.Col(sid__638("users"), sid__638("id"));
                bool t___12269 = c__1517.ToString() == "users.id";
                string fn__12263()
                {
                    return "col helper";
                }
                test___110.Assert(t___12269, (S1::Func<string>) fn__12263);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__2185()
        {
            T::Test test___111 = new T::Test();
            try
            {
                SqlFragment onCond__1519 = S0::SrcGlobal.Col(sid__638("users"), sid__638("id"));
                SqlBuilder b__1520 = new SqlBuilder();
                b__1520.AppendFragment(onCond__1519);
                b__1520.AppendSafe(" = ");
                b__1520.AppendFragment(S0::SrcGlobal.Col(sid__638("orders"), sid__638("user_id")));
                ISafeIdentifier t___12254 = sid__638("users");
                ISafeIdentifier t___12255 = sid__638("orders");
                SqlFragment t___12256 = b__1520.Accumulated;
                Query q__1521 = S0::SrcGlobal.From(t___12254).InnerJoin(t___12255, t___12256);
                bool t___12261 = q__1521.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__12243()
                {
                    return "join with col";
                }
                test___111.Assert(t___12261, (S1::Func<string>) fn__12243);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__2186()
        {
            T::Test test___112 = new T::Test();
            try
            {
                ISafeIdentifier t___12232 = sid__638("users");
                SqlBuilder t___12233 = new SqlBuilder();
                t___12233.AppendSafe("status = ");
                t___12233.AppendString("active");
                SqlFragment t___12236 = t___12233.Accumulated;
                Query q__1523 = S0::SrcGlobal.From(t___12232).OrWhere(t___12236);
                bool t___12241 = q__1523.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__12231()
                {
                    return "orWhere basic";
                }
                test___112.Assert(t___12241, (S1::Func<string>) fn__12231);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__2188()
        {
            T::Test test___113 = new T::Test();
            try
            {
                ISafeIdentifier t___12215 = sid__638("users");
                SqlBuilder t___12216 = new SqlBuilder();
                t___12216.AppendSafe("age > ");
                t___12216.AppendInt32(18);
                SqlFragment t___12219 = t___12216.Accumulated;
                Query t___12220 = S0::SrcGlobal.From(t___12215).Where(t___12219);
                SqlBuilder t___12221 = new SqlBuilder();
                t___12221.AppendSafe("vip = ");
                t___12221.AppendBoolean(true);
                Query q__1525 = t___12220.OrWhere(t___12221.Accumulated);
                bool t___12229 = q__1525.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__12214()
                {
                    return "where then orWhere";
                }
                test___113.Assert(t___12229, (S1::Func<string>) fn__12214);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__2191()
        {
            T::Test test___114 = new T::Test();
            try
            {
                ISafeIdentifier t___12193 = sid__638("users");
                SqlBuilder t___12194 = new SqlBuilder();
                t___12194.AppendSafe("active = ");
                t___12194.AppendBoolean(true);
                SqlFragment t___12197 = t___12194.Accumulated;
                Query t___12198 = S0::SrcGlobal.From(t___12193).Where(t___12197);
                SqlBuilder t___12199 = new SqlBuilder();
                t___12199.AppendSafe("role = ");
                t___12199.AppendString("admin");
                Query t___12203 = t___12198.OrWhere(t___12199.Accumulated);
                SqlBuilder t___12204 = new SqlBuilder();
                t___12204.AppendSafe("role = ");
                t___12204.AppendString("moderator");
                Query q__1527 = t___12203.OrWhere(t___12204.Accumulated);
                bool t___12212 = q__1527.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__12192()
                {
                    return "multiple orWhere";
                }
                test___114.Assert(t___12212, (S1::Func<string>) fn__12192);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__2195()
        {
            T::Test test___115 = new T::Test();
            try
            {
                ISafeIdentifier t___12171 = sid__638("users");
                SqlBuilder t___12172 = new SqlBuilder();
                t___12172.AppendSafe("age > ");
                t___12172.AppendInt32(18);
                SqlFragment t___12175 = t___12172.Accumulated;
                Query t___12176 = S0::SrcGlobal.From(t___12171).Where(t___12175);
                SqlBuilder t___12177 = new SqlBuilder();
                t___12177.AppendSafe("active = ");
                t___12177.AppendBoolean(true);
                Query t___12181 = t___12176.Where(t___12177.Accumulated);
                SqlBuilder t___12182 = new SqlBuilder();
                t___12182.AppendSafe("vip = ");
                t___12182.AppendBoolean(true);
                Query q__1529 = t___12181.OrWhere(t___12182.Accumulated);
                bool t___12190 = q__1529.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__12170()
                {
                    return "mixed where and orWhere";
                }
                test___115.Assert(t___12190, (S1::Func<string>) fn__12170);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__2199()
        {
            T::Test test___116 = new T::Test();
            try
            {
                ISafeIdentifier t___12162 = sid__638("users");
                ISafeIdentifier t___12163 = sid__638("deleted_at");
                Query q__1531 = S0::SrcGlobal.From(t___12162).WhereNull(t___12163);
                bool t___12168 = q__1531.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__12161()
                {
                    return "whereNull";
                }
                test___116.Assert(t___12168, (S1::Func<string>) fn__12161);
            }
            finally
            {
                test___116.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__2200()
        {
            T::Test test___117 = new T::Test();
            try
            {
                ISafeIdentifier t___12153 = sid__638("users");
                ISafeIdentifier t___12154 = sid__638("email");
                Query q__1533 = S0::SrcGlobal.From(t___12153).WhereNotNull(t___12154);
                bool t___12159 = q__1533.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__12152()
                {
                    return "whereNotNull";
                }
                test___117.Assert(t___12159, (S1::Func<string>) fn__12152);
            }
            finally
            {
                test___117.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__2201()
        {
            T::Test test___118 = new T::Test();
            try
            {
                ISafeIdentifier t___12139 = sid__638("users");
                SqlBuilder t___12140 = new SqlBuilder();
                t___12140.AppendSafe("active = ");
                t___12140.AppendBoolean(true);
                SqlFragment t___12143 = t___12140.Accumulated;
                Query q__1535 = S0::SrcGlobal.From(t___12139).Where(t___12143).WhereNull(sid__638("deleted_at"));
                bool t___12150 = q__1535.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__12138()
                {
                    return "whereNull chained";
                }
                test___118.Assert(t___12150, (S1::Func<string>) fn__12138);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__2203()
        {
            T::Test test___119 = new T::Test();
            try
            {
                ISafeIdentifier t___12125 = sid__638("users");
                ISafeIdentifier t___12126 = sid__638("deleted_at");
                Query t___12127 = S0::SrcGlobal.From(t___12125).WhereNull(t___12126);
                SqlBuilder t___12128 = new SqlBuilder();
                t___12128.AppendSafe("role = ");
                t___12128.AppendString("admin");
                Query q__1537 = t___12127.OrWhere(t___12128.Accumulated);
                bool t___12136 = q__1537.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__12124()
                {
                    return "whereNotNull with orWhere";
                }
                test___119.Assert(t___12136, (S1::Func<string>) fn__12124);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__2205()
        {
            T::Test test___120 = new T::Test();
            try
            {
                ISafeIdentifier t___12113 = sid__638("users");
                ISafeIdentifier t___12114 = sid__638("id");
                SqlInt32 t___12115 = new SqlInt32(1);
                SqlInt32 t___12116 = new SqlInt32(2);
                SqlInt32 t___12117 = new SqlInt32(3);
                Query q__1539 = S0::SrcGlobal.From(t___12113).WhereIn(t___12114, C::Listed.CreateReadOnlyList<SqlInt32>(t___12115, t___12116, t___12117));
                bool t___12122 = q__1539.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__12112()
                {
                    return "whereIn ints";
                }
                test___120.Assert(t___12122, (S1::Func<string>) fn__12112);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__2206()
        {
            T::Test test___121 = new T::Test();
            try
            {
                ISafeIdentifier t___12102 = sid__638("users");
                ISafeIdentifier t___12103 = sid__638("name");
                SqlString t___12104 = new SqlString("Alice");
                SqlString t___12105 = new SqlString("Bob's");
                Query q__1541 = S0::SrcGlobal.From(t___12102).WhereIn(t___12103, C::Listed.CreateReadOnlyList<SqlString>(t___12104, t___12105));
                bool t___12110 = q__1541.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__12101()
                {
                    return "whereIn strings";
                }
                test___121.Assert(t___12110, (S1::Func<string>) fn__12101);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__2207()
        {
            T::Test test___122 = new T::Test();
            try
            {
                ISafeIdentifier t___12093 = sid__638("users");
                ISafeIdentifier t___12094 = sid__638("id");
                Query q__1543 = S0::SrcGlobal.From(t___12093).WhereIn(t___12094, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___12099 = q__1543.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__12092()
                {
                    return "whereIn empty";
                }
                test___122.Assert(t___12099, (S1::Func<string>) fn__12092);
            }
            finally
            {
                test___122.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__2208()
        {
            T::Test test___123 = new T::Test();
            try
            {
                ISafeIdentifier t___12077 = sid__638("users");
                SqlBuilder t___12078 = new SqlBuilder();
                t___12078.AppendSafe("active = ");
                t___12078.AppendBoolean(true);
                SqlFragment t___12081 = t___12078.Accumulated;
                Query q__1545 = S0::SrcGlobal.From(t___12077).Where(t___12081).WhereIn(sid__638("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___12090 = q__1545.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__12076()
                {
                    return "whereIn chained";
                }
                test___123.Assert(t___12090, (S1::Func<string>) fn__12076);
            }
            finally
            {
                test___123.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__2210()
        {
            T::Test test___124 = new T::Test();
            try
            {
                ISafeIdentifier t___12067 = sid__638("users");
                ISafeIdentifier t___12068 = sid__638("id");
                SqlInt32 t___12069 = new SqlInt32(42);
                Query q__1547 = S0::SrcGlobal.From(t___12067).WhereIn(t___12068, C::Listed.CreateReadOnlyList<SqlInt32>(t___12069));
                bool t___12074 = q__1547.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__12066()
                {
                    return "whereIn single";
                }
                test___124.Assert(t___12074, (S1::Func<string>) fn__12066);
            }
            finally
            {
                test___124.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__2211()
        {
            T::Test test___125 = new T::Test();
            try
            {
                ISafeIdentifier t___12055 = sid__638("users");
                SqlBuilder t___12056 = new SqlBuilder();
                t___12056.AppendSafe("active = ");
                t___12056.AppendBoolean(true);
                SqlFragment t___12059 = t___12056.Accumulated;
                Query q__1549 = S0::SrcGlobal.From(t___12055).WhereNot(t___12059);
                bool t___12064 = q__1549.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__12054()
                {
                    return "whereNot";
                }
                test___125.Assert(t___12064, (S1::Func<string>) fn__12054);
            }
            finally
            {
                test___125.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__2213()
        {
            T::Test test___126 = new T::Test();
            try
            {
                ISafeIdentifier t___12038 = sid__638("users");
                SqlBuilder t___12039 = new SqlBuilder();
                t___12039.AppendSafe("age > ");
                t___12039.AppendInt32(18);
                SqlFragment t___12042 = t___12039.Accumulated;
                Query t___12043 = S0::SrcGlobal.From(t___12038).Where(t___12042);
                SqlBuilder t___12044 = new SqlBuilder();
                t___12044.AppendSafe("banned = ");
                t___12044.AppendBoolean(true);
                Query q__1551 = t___12043.WhereNot(t___12044.Accumulated);
                bool t___12052 = q__1551.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__12037()
                {
                    return "whereNot chained";
                }
                test___126.Assert(t___12052, (S1::Func<string>) fn__12037);
            }
            finally
            {
                test___126.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__2216()
        {
            T::Test test___127 = new T::Test();
            try
            {
                ISafeIdentifier t___12027 = sid__638("users");
                ISafeIdentifier t___12028 = sid__638("age");
                SqlInt32 t___12029 = new SqlInt32(18);
                SqlInt32 t___12030 = new SqlInt32(65);
                Query q__1553 = S0::SrcGlobal.From(t___12027).WhereBetween(t___12028, t___12029, t___12030);
                bool t___12035 = q__1553.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__12026()
                {
                    return "whereBetween ints";
                }
                test___127.Assert(t___12035, (S1::Func<string>) fn__12026);
            }
            finally
            {
                test___127.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__2217()
        {
            T::Test test___128 = new T::Test();
            try
            {
                ISafeIdentifier t___12011 = sid__638("users");
                SqlBuilder t___12012 = new SqlBuilder();
                t___12012.AppendSafe("active = ");
                t___12012.AppendBoolean(true);
                SqlFragment t___12015 = t___12012.Accumulated;
                Query q__1555 = S0::SrcGlobal.From(t___12011).Where(t___12015).WhereBetween(sid__638("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___12024 = q__1555.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__12010()
                {
                    return "whereBetween chained";
                }
                test___128.Assert(t___12024, (S1::Func<string>) fn__12010);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__2219()
        {
            T::Test test___129 = new T::Test();
            try
            {
                ISafeIdentifier t___12002 = sid__638("users");
                ISafeIdentifier t___12003 = sid__638("name");
                Query q__1557 = S0::SrcGlobal.From(t___12002).WhereLike(t___12003, "John%");
                bool t___12008 = q__1557.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__12001()
                {
                    return "whereLike";
                }
                test___129.Assert(t___12008, (S1::Func<string>) fn__12001);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__2220()
        {
            T::Test test___130 = new T::Test();
            try
            {
                ISafeIdentifier t___11993 = sid__638("users");
                ISafeIdentifier t___11994 = sid__638("email");
                Query q__1559 = S0::SrcGlobal.From(t___11993).WhereILike(t___11994, "%@gmail.com");
                bool t___11999 = q__1559.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__11992()
                {
                    return "whereILike";
                }
                test___130.Assert(t___11999, (S1::Func<string>) fn__11992);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__2221()
        {
            T::Test test___131 = new T::Test();
            try
            {
                ISafeIdentifier t___11979 = sid__638("users");
                ISafeIdentifier t___11980 = sid__638("name");
                Query q__1561 = S0::SrcGlobal.From(t___11979).WhereLike(t___11980, "'; DROP TABLE users; --");
                string s__1562 = q__1561.ToSql().ToString();
                bool t___11985 = s__1562.IndexOf("''") >= 0;
                string fn__11978()
                {
                    return "like injection escaped: " + s__1562;
                }
                test___131.Assert(t___11985, (S1::Func<string>) fn__11978);
                bool t___11989 = s__1562.IndexOf("LIKE") >= 0;
                string fn__11977()
                {
                    return "like structure intact: " + s__1562;
                }
                test___131.Assert(t___11989, (S1::Func<string>) fn__11977);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__2222()
        {
            T::Test test___132 = new T::Test();
            try
            {
                ISafeIdentifier t___11969 = sid__638("users");
                ISafeIdentifier t___11970 = sid__638("name");
                Query q__1564 = S0::SrcGlobal.From(t___11969).WhereLike(t___11970, "%son%");
                bool t___11975 = q__1564.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__11968()
                {
                    return "whereLike wildcard";
                }
                test___132.Assert(t___11975, (S1::Func<string>) fn__11968);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__2223()
        {
            T::Test test___133 = new T::Test();
            try
            {
                SqlFragment f__1566 = S0::SrcGlobal.CountAll();
                bool t___11966 = f__1566.ToString() == "COUNT(*)";
                string fn__11962()
                {
                    return "countAll";
                }
                test___133.Assert(t___11966, (S1::Func<string>) fn__11962);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__2224()
        {
            T::Test test___134 = new T::Test();
            try
            {
                SqlFragment f__1568 = S0::SrcGlobal.CountCol(sid__638("id"));
                bool t___11960 = f__1568.ToString() == "COUNT(id)";
                string fn__11955()
                {
                    return "countCol";
                }
                test___134.Assert(t___11960, (S1::Func<string>) fn__11955);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__2225()
        {
            T::Test test___135 = new T::Test();
            try
            {
                SqlFragment f__1570 = S0::SrcGlobal.SumCol(sid__638("amount"));
                bool t___11953 = f__1570.ToString() == "SUM(amount)";
                string fn__11948()
                {
                    return "sumCol";
                }
                test___135.Assert(t___11953, (S1::Func<string>) fn__11948);
            }
            finally
            {
                test___135.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__2226()
        {
            T::Test test___136 = new T::Test();
            try
            {
                SqlFragment f__1572 = S0::SrcGlobal.AvgCol(sid__638("price"));
                bool t___11946 = f__1572.ToString() == "AVG(price)";
                string fn__11941()
                {
                    return "avgCol";
                }
                test___136.Assert(t___11946, (S1::Func<string>) fn__11941);
            }
            finally
            {
                test___136.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__2227()
        {
            T::Test test___137 = new T::Test();
            try
            {
                SqlFragment f__1574 = S0::SrcGlobal.MinCol(sid__638("created_at"));
                bool t___11939 = f__1574.ToString() == "MIN(created_at)";
                string fn__11934()
                {
                    return "minCol";
                }
                test___137.Assert(t___11939, (S1::Func<string>) fn__11934);
            }
            finally
            {
                test___137.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__2228()
        {
            T::Test test___138 = new T::Test();
            try
            {
                SqlFragment f__1576 = S0::SrcGlobal.MaxCol(sid__638("score"));
                bool t___11932 = f__1576.ToString() == "MAX(score)";
                string fn__11927()
                {
                    return "maxCol";
                }
                test___138.Assert(t___11932, (S1::Func<string>) fn__11927);
            }
            finally
            {
                test___138.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__2229()
        {
            T::Test test___139 = new T::Test();
            try
            {
                ISafeIdentifier t___11919 = sid__638("orders");
                SqlFragment t___11920 = S0::SrcGlobal.CountAll();
                Query q__1578 = S0::SrcGlobal.From(t___11919).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___11920));
                bool t___11925 = q__1578.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__11918()
                {
                    return "selectExpr count";
                }
                test___139.Assert(t___11925, (S1::Func<string>) fn__11918);
            }
            finally
            {
                test___139.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__2230()
        {
            T::Test test___140 = new T::Test();
            try
            {
                SqlFragment nameFrag__1580 = S0::SrcGlobal.Col(sid__638("users"), sid__638("name"));
                ISafeIdentifier t___11910 = sid__638("users");
                SqlFragment t___11911 = S0::SrcGlobal.CountAll();
                Query q__1581 = S0::SrcGlobal.From(t___11910).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1580, t___11911));
                bool t___11916 = q__1581.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__11906()
                {
                    return "selectExpr multi";
                }
                test___140.Assert(t___11916, (S1::Func<string>) fn__11906);
            }
            finally
            {
                test___140.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__2231()
        {
            T::Test test___141 = new T::Test();
            try
            {
                ISafeIdentifier t___11895 = sid__638("users");
                ISafeIdentifier t___11896 = sid__638("id");
                ISafeIdentifier t___11897 = sid__638("name");
                Query q__1583 = S0::SrcGlobal.From(t___11895).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11896, t___11897)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___11904 = q__1583.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__11894()
                {
                    return "selectExpr overrides select";
                }
                test___141.Assert(t___11904, (S1::Func<string>) fn__11894);
            }
            finally
            {
                test___141.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__2232()
        {
            T::Test test___142 = new T::Test();
            try
            {
                ISafeIdentifier t___11881 = sid__638("orders");
                SqlFragment t___11884 = S0::SrcGlobal.Col(sid__638("orders"), sid__638("status"));
                SqlFragment t___11885 = S0::SrcGlobal.CountAll();
                Query q__1585 = S0::SrcGlobal.From(t___11881).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___11884, t___11885)).GroupBy(sid__638("status"));
                bool t___11892 = q__1585.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__11880()
                {
                    return "groupBy single";
                }
                test___142.Assert(t___11892, (S1::Func<string>) fn__11880);
            }
            finally
            {
                test___142.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__2233()
        {
            T::Test test___143 = new T::Test();
            try
            {
                ISafeIdentifier t___11870 = sid__638("orders");
                ISafeIdentifier t___11871 = sid__638("status");
                Query q__1587 = S0::SrcGlobal.From(t___11870).GroupBy(t___11871).GroupBy(sid__638("category"));
                bool t___11878 = q__1587.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__11869()
                {
                    return "groupBy multiple";
                }
                test___143.Assert(t___11878, (S1::Func<string>) fn__11869);
            }
            finally
            {
                test___143.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__2234()
        {
            T::Test test___144 = new T::Test();
            try
            {
                ISafeIdentifier t___11851 = sid__638("orders");
                SqlFragment t___11854 = S0::SrcGlobal.Col(sid__638("orders"), sid__638("status"));
                SqlFragment t___11855 = S0::SrcGlobal.CountAll();
                Query t___11858 = S0::SrcGlobal.From(t___11851).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___11854, t___11855)).GroupBy(sid__638("status"));
                SqlBuilder t___11859 = new SqlBuilder();
                t___11859.AppendSafe("COUNT(*) > ");
                t___11859.AppendInt32(5);
                Query q__1589 = t___11858.Having(t___11859.Accumulated);
                bool t___11867 = q__1589.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__11850()
                {
                    return "having basic";
                }
                test___144.Assert(t___11867, (S1::Func<string>) fn__11850);
            }
            finally
            {
                test___144.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__2236()
        {
            T::Test test___145 = new T::Test();
            try
            {
                ISafeIdentifier t___11832 = sid__638("orders");
                ISafeIdentifier t___11833 = sid__638("status");
                Query t___11834 = S0::SrcGlobal.From(t___11832).GroupBy(t___11833);
                SqlBuilder t___11835 = new SqlBuilder();
                t___11835.AppendSafe("COUNT(*) > ");
                t___11835.AppendInt32(5);
                Query t___11839 = t___11834.Having(t___11835.Accumulated);
                SqlBuilder t___11840 = new SqlBuilder();
                t___11840.AppendSafe("SUM(total) > ");
                t___11840.AppendInt32(1000);
                Query q__1591 = t___11839.OrHaving(t___11840.Accumulated);
                bool t___11848 = q__1591.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__11831()
                {
                    return "orHaving";
                }
                test___145.Assert(t___11848, (S1::Func<string>) fn__11831);
            }
            finally
            {
                test___145.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__2239()
        {
            T::Test test___146 = new T::Test();
            try
            {
                ISafeIdentifier t___11822 = sid__638("users");
                ISafeIdentifier t___11823 = sid__638("name");
                Query q__1593 = S0::SrcGlobal.From(t___11822).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11823)).Distinct();
                bool t___11829 = q__1593.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__11821()
                {
                    return "distinct";
                }
                test___146.Assert(t___11829, (S1::Func<string>) fn__11821);
            }
            finally
            {
                test___146.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__2240()
        {
            T::Test test___147 = new T::Test();
            try
            {
                ISafeIdentifier t___11807 = sid__638("users");
                ISafeIdentifier t___11808 = sid__638("email");
                Query t___11809 = S0::SrcGlobal.From(t___11807).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11808));
                SqlBuilder t___11810 = new SqlBuilder();
                t___11810.AppendSafe("active = ");
                t___11810.AppendBoolean(true);
                Query q__1595 = t___11809.Where(t___11810.Accumulated).Distinct();
                bool t___11819 = q__1595.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__11806()
                {
                    return "distinct with where";
                }
                test___147.Assert(t___11819, (S1::Func<string>) fn__11806);
            }
            finally
            {
                test___147.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__2242()
        {
            T::Test test___148 = new T::Test();
            try
            {
                Query q__1597 = S0::SrcGlobal.From(sid__638("users"));
                bool t___11804 = q__1597.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__11799()
                {
                    return "countSql bare";
                }
                test___148.Assert(t___11804, (S1::Func<string>) fn__11799);
            }
            finally
            {
                test___148.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__2243()
        {
            T::Test test___149 = new T::Test();
            try
            {
                ISafeIdentifier t___11788 = sid__638("users");
                SqlBuilder t___11789 = new SqlBuilder();
                t___11789.AppendSafe("active = ");
                t___11789.AppendBoolean(true);
                SqlFragment t___11792 = t___11789.Accumulated;
                Query q__1599 = S0::SrcGlobal.From(t___11788).Where(t___11792);
                bool t___11797 = q__1599.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__11787()
                {
                    return "countSql with where";
                }
                test___149.Assert(t___11797, (S1::Func<string>) fn__11787);
            }
            finally
            {
                test___149.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__2245()
        {
            T::Test test___150 = new T::Test();
            try
            {
                ISafeIdentifier t___11771 = sid__638("users");
                ISafeIdentifier t___11772 = sid__638("orders");
                SqlBuilder t___11773 = new SqlBuilder();
                t___11773.AppendSafe("users.id = orders.user_id");
                SqlFragment t___11775 = t___11773.Accumulated;
                Query t___11776 = S0::SrcGlobal.From(t___11771).InnerJoin(t___11772, t___11775);
                SqlBuilder t___11777 = new SqlBuilder();
                t___11777.AppendSafe("orders.total > ");
                t___11777.AppendInt32(100);
                Query q__1601 = t___11776.Where(t___11777.Accumulated);
                bool t___11785 = q__1601.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__11770()
                {
                    return "countSql with join";
                }
                test___150.Assert(t___11785, (S1::Func<string>) fn__11770);
            }
            finally
            {
                test___150.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__2248()
        {
            T::Test test___151 = new T::Test();
            try
            {
                ISafeIdentifier t___11757 = sid__638("users");
                SqlBuilder t___11758 = new SqlBuilder();
                t___11758.AppendSafe("active = ");
                t___11758.AppendBoolean(true);
                SqlFragment t___11761 = t___11758.Accumulated;
                Query t___5865;
                t___5865 = S0::SrcGlobal.From(t___11757).Where(t___11761).OrderBy(sid__638("name"), true).Limit(10);
                Query t___5866;
                t___5866 = t___5865.Offset(20);
                Query q__1603 = t___5866;
                string s__1604 = q__1603.CountSql().ToString();
                bool t___11768 = s__1604 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__11756()
                {
                    return "countSql drops extras: " + s__1604;
                }
                test___151.Assert(t___11768, (S1::Func<string>) fn__11756);
            }
            finally
            {
                test___151.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__2250()
        {
            T::Test test___152 = new T::Test();
            try
            {
                ISafeIdentifier t___11724 = sid__638("orders");
                SqlFragment t___11727 = S0::SrcGlobal.Col(sid__638("orders"), sid__638("status"));
                SqlFragment t___11728 = S0::SrcGlobal.CountAll();
                SqlFragment t___11730 = S0::SrcGlobal.SumCol(sid__638("total"));
                Query t___11731 = S0::SrcGlobal.From(t___11724).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___11727, t___11728, t___11730));
                ISafeIdentifier t___11732 = sid__638("users");
                SqlBuilder t___11733 = new SqlBuilder();
                t___11733.AppendSafe("orders.user_id = users.id");
                Query t___11736 = t___11731.InnerJoin(t___11732, t___11733.Accumulated);
                SqlBuilder t___11737 = new SqlBuilder();
                t___11737.AppendSafe("users.active = ");
                t___11737.AppendBoolean(true);
                Query t___11743 = t___11736.Where(t___11737.Accumulated).GroupBy(sid__638("status"));
                SqlBuilder t___11744 = new SqlBuilder();
                t___11744.AppendSafe("COUNT(*) > ");
                t___11744.AppendInt32(3);
                Query q__1606 = t___11743.Having(t___11744.Accumulated).OrderBy(sid__638("status"), true);
                string expected__1607 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___11754 = q__1606.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__11723()
                {
                    return "full aggregation";
                }
                test___152.Assert(t___11754, (S1::Func<string>) fn__11723);
            }
            finally
            {
                test___152.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionSql__2254()
        {
            T::Test test___153 = new T::Test();
            try
            {
                ISafeIdentifier t___11706 = sid__638("users");
                SqlBuilder t___11707 = new SqlBuilder();
                t___11707.AppendSafe("role = ");
                t___11707.AppendString("admin");
                SqlFragment t___11710 = t___11707.Accumulated;
                Query a__1609 = S0::SrcGlobal.From(t___11706).Where(t___11710);
                ISafeIdentifier t___11712 = sid__638("users");
                SqlBuilder t___11713 = new SqlBuilder();
                t___11713.AppendSafe("role = ");
                t___11713.AppendString("moderator");
                SqlFragment t___11716 = t___11713.Accumulated;
                Query b__1610 = S0::SrcGlobal.From(t___11712).Where(t___11716);
                string s__1611 = S0::SrcGlobal.UnionSql(a__1609, b__1610).ToString();
                bool t___11721 = s__1611 == "(SELECT * FROM users WHERE role = 'admin') UNION (SELECT * FROM users WHERE role = 'moderator')";
                string fn__11705()
                {
                    return "unionSql: " + s__1611;
                }
                test___153.Assert(t___11721, (S1::Func<string>) fn__11705);
            }
            finally
            {
                test___153.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionAllSql__2257()
        {
            T::Test test___154 = new T::Test();
            try
            {
                ISafeIdentifier t___11694 = sid__638("users");
                ISafeIdentifier t___11695 = sid__638("name");
                Query a__1613 = S0::SrcGlobal.From(t___11694).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11695));
                ISafeIdentifier t___11697 = sid__638("contacts");
                ISafeIdentifier t___11698 = sid__638("name");
                Query b__1614 = S0::SrcGlobal.From(t___11697).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11698));
                string s__1615 = S0::SrcGlobal.UnionAllSql(a__1613, b__1614).ToString();
                bool t___11703 = s__1615 == "(SELECT name FROM users) UNION ALL (SELECT name FROM contacts)";
                string fn__11693()
                {
                    return "unionAllSql: " + s__1615;
                }
                test___154.Assert(t___11703, (S1::Func<string>) fn__11693);
            }
            finally
            {
                test___154.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void intersectSql__2258()
        {
            T::Test test___155 = new T::Test();
            try
            {
                ISafeIdentifier t___11682 = sid__638("users");
                ISafeIdentifier t___11683 = sid__638("email");
                Query a__1617 = S0::SrcGlobal.From(t___11682).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11683));
                ISafeIdentifier t___11685 = sid__638("subscribers");
                ISafeIdentifier t___11686 = sid__638("email");
                Query b__1618 = S0::SrcGlobal.From(t___11685).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11686));
                string s__1619 = S0::SrcGlobal.IntersectSql(a__1617, b__1618).ToString();
                bool t___11691 = s__1619 == "(SELECT email FROM users) INTERSECT (SELECT email FROM subscribers)";
                string fn__11681()
                {
                    return "intersectSql: " + s__1619;
                }
                test___155.Assert(t___11691, (S1::Func<string>) fn__11681);
            }
            finally
            {
                test___155.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void exceptSql__2259()
        {
            T::Test test___156 = new T::Test();
            try
            {
                ISafeIdentifier t___11670 = sid__638("users");
                ISafeIdentifier t___11671 = sid__638("id");
                Query a__1621 = S0::SrcGlobal.From(t___11670).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11671));
                ISafeIdentifier t___11673 = sid__638("banned");
                ISafeIdentifier t___11674 = sid__638("id");
                Query b__1622 = S0::SrcGlobal.From(t___11673).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11674));
                string s__1623 = S0::SrcGlobal.ExceptSql(a__1621, b__1622).ToString();
                bool t___11679 = s__1623 == "(SELECT id FROM users) EXCEPT (SELECT id FROM banned)";
                string fn__11669()
                {
                    return "exceptSql: " + s__1623;
                }
                test___156.Assert(t___11679, (S1::Func<string>) fn__11669);
            }
            finally
            {
                test___156.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void subqueryWithAlias__2260()
        {
            T::Test test___157 = new T::Test();
            try
            {
                ISafeIdentifier t___11655 = sid__638("orders");
                ISafeIdentifier t___11656 = sid__638("user_id");
                Query t___11657 = S0::SrcGlobal.From(t___11655).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11656));
                SqlBuilder t___11658 = new SqlBuilder();
                t___11658.AppendSafe("total > ");
                t___11658.AppendInt32(100);
                Query inner__1625 = t___11657.Where(t___11658.Accumulated);
                string s__1626 = S0::SrcGlobal.Subquery(inner__1625, sid__638("big_orders")).ToString();
                bool t___11667 = s__1626 == "(SELECT user_id FROM orders WHERE total > 100) AS big_orders";
                string fn__11654()
                {
                    return "subquery: " + s__1626;
                }
                test___157.Assert(t___11667, (S1::Func<string>) fn__11654);
            }
            finally
            {
                test___157.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSql__2262()
        {
            T::Test test___158 = new T::Test();
            try
            {
                ISafeIdentifier t___11644 = sid__638("orders");
                SqlBuilder t___11645 = new SqlBuilder();
                t___11645.AppendSafe("orders.user_id = users.id");
                SqlFragment t___11647 = t___11645.Accumulated;
                Query inner__1628 = S0::SrcGlobal.From(t___11644).Where(t___11647);
                string s__1629 = S0::SrcGlobal.ExistsSql(inner__1628).ToString();
                bool t___11652 = s__1629 == "EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__11643()
                {
                    return "existsSql: " + s__1629;
                }
                test___158.Assert(t___11652, (S1::Func<string>) fn__11643);
            }
            finally
            {
                test___158.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubquery__2264()
        {
            T::Test test___159 = new T::Test();
            try
            {
                ISafeIdentifier t___11627 = sid__638("orders");
                ISafeIdentifier t___11628 = sid__638("user_id");
                Query t___11629 = S0::SrcGlobal.From(t___11627).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11628));
                SqlBuilder t___11630 = new SqlBuilder();
                t___11630.AppendSafe("total > ");
                t___11630.AppendInt32(1000);
                Query sub__1631 = t___11629.Where(t___11630.Accumulated);
                ISafeIdentifier t___11635 = sid__638("users");
                ISafeIdentifier t___11636 = sid__638("id");
                Query q__1632 = S0::SrcGlobal.From(t___11635).WhereInSubquery(t___11636, sub__1631);
                string s__1633 = q__1632.ToSql().ToString();
                bool t___11641 = s__1633 == "SELECT * FROM users WHERE id IN (SELECT user_id FROM orders WHERE total > 1000)";
                string fn__11626()
                {
                    return "whereInSubquery: " + s__1633;
                }
                test___159.Assert(t___11641, (S1::Func<string>) fn__11626);
            }
            finally
            {
                test___159.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void setOperationWithWhereOnEachSide__2266()
        {
            T::Test test___160 = new T::Test();
            try
            {
                ISafeIdentifier t___11604 = sid__638("users");
                SqlBuilder t___11605 = new SqlBuilder();
                t___11605.AppendSafe("age > ");
                t___11605.AppendInt32(18);
                SqlFragment t___11608 = t___11605.Accumulated;
                Query t___11609 = S0::SrcGlobal.From(t___11604).Where(t___11608);
                SqlBuilder t___11610 = new SqlBuilder();
                t___11610.AppendSafe("active = ");
                t___11610.AppendBoolean(true);
                Query a__1635 = t___11609.Where(t___11610.Accumulated);
                ISafeIdentifier t___11615 = sid__638("users");
                SqlBuilder t___11616 = new SqlBuilder();
                t___11616.AppendSafe("role = ");
                t___11616.AppendString("vip");
                SqlFragment t___11619 = t___11616.Accumulated;
                Query b__1636 = S0::SrcGlobal.From(t___11615).Where(t___11619);
                string s__1637 = S0::SrcGlobal.UnionSql(a__1635, b__1636).ToString();
                bool t___11624 = s__1637 == "(SELECT * FROM users WHERE age > 18 AND active = TRUE) UNION (SELECT * FROM users WHERE role = 'vip')";
                string fn__11603()
                {
                    return "union with where: " + s__1637;
                }
                test___160.Assert(t___11624, (S1::Func<string>) fn__11603);
            }
            finally
            {
                test___160.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubqueryChainedWithWhere__2270()
        {
            T::Test test___161 = new T::Test();
            try
            {
                ISafeIdentifier t___11587 = sid__638("orders");
                ISafeIdentifier t___11588 = sid__638("user_id");
                Query sub__1639 = S0::SrcGlobal.From(t___11587).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11588));
                ISafeIdentifier t___11590 = sid__638("users");
                SqlBuilder t___11591 = new SqlBuilder();
                t___11591.AppendSafe("active = ");
                t___11591.AppendBoolean(true);
                SqlFragment t___11594 = t___11591.Accumulated;
                Query q__1640 = S0::SrcGlobal.From(t___11590).Where(t___11594).WhereInSubquery(sid__638("id"), sub__1639);
                string s__1641 = q__1640.ToSql().ToString();
                bool t___11601 = s__1641 == "SELECT * FROM users WHERE active = TRUE AND id IN (SELECT user_id FROM orders)";
                string fn__11586()
                {
                    return "whereInSubquery chained: " + s__1641;
                }
                test___161.Assert(t___11601, (S1::Func<string>) fn__11586);
            }
            finally
            {
                test___161.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSqlUsedInWhere__2272()
        {
            T::Test test___162 = new T::Test();
            try
            {
                ISafeIdentifier t___11573 = sid__638("orders");
                SqlBuilder t___11574 = new SqlBuilder();
                t___11574.AppendSafe("orders.user_id = users.id");
                SqlFragment t___11576 = t___11574.Accumulated;
                Query sub__1643 = S0::SrcGlobal.From(t___11573).Where(t___11576);
                ISafeIdentifier t___11578 = sid__638("users");
                SqlFragment t___11579 = S0::SrcGlobal.ExistsSql(sub__1643);
                Query q__1644 = S0::SrcGlobal.From(t___11578).Where(t___11579);
                string s__1645 = q__1644.ToSql().ToString();
                bool t___11584 = s__1645 == "SELECT * FROM users WHERE EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__11572()
                {
                    return "exists in where: " + s__1645;
                }
                test___162.Assert(t___11584, (S1::Func<string>) fn__11572);
            }
            finally
            {
                test___162.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBasic__2274()
        {
            T::Test test___163 = new T::Test();
            try
            {
                ISafeIdentifier t___11559 = sid__638("users");
                ISafeIdentifier t___11560 = sid__638("name");
                SqlString t___11561 = new SqlString("Alice");
                UpdateQuery t___11562 = S0::SrcGlobal.Update(t___11559).Set(t___11560, t___11561);
                SqlBuilder t___11563 = new SqlBuilder();
                t___11563.AppendSafe("id = ");
                t___11563.AppendInt32(1);
                SqlFragment t___5687;
                t___5687 = t___11562.Where(t___11563.Accumulated).ToSql();
                SqlFragment q__1647 = t___5687;
                bool t___11570 = q__1647.ToString() == "UPDATE users SET name = 'Alice' WHERE id = 1";
                string fn__11558()
                {
                    return "update basic";
                }
                test___163.Assert(t___11570, (S1::Func<string>) fn__11558);
            }
            finally
            {
                test___163.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleSet__2276()
        {
            T::Test test___164 = new T::Test();
            try
            {
                ISafeIdentifier t___11542 = sid__638("users");
                ISafeIdentifier t___11543 = sid__638("name");
                SqlString t___11544 = new SqlString("Bob");
                UpdateQuery t___11548 = S0::SrcGlobal.Update(t___11542).Set(t___11543, t___11544).Set(sid__638("age"), new SqlInt32(30));
                SqlBuilder t___11549 = new SqlBuilder();
                t___11549.AppendSafe("id = ");
                t___11549.AppendInt32(2);
                SqlFragment t___5672;
                t___5672 = t___11548.Where(t___11549.Accumulated).ToSql();
                SqlFragment q__1649 = t___5672;
                bool t___11556 = q__1649.ToString() == "UPDATE users SET name = 'Bob', age = 30 WHERE id = 2";
                string fn__11541()
                {
                    return "update multi set";
                }
                test___164.Assert(t___11556, (S1::Func<string>) fn__11541);
            }
            finally
            {
                test___164.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleWhere__2278()
        {
            T::Test test___165 = new T::Test();
            try
            {
                ISafeIdentifier t___11523 = sid__638("users");
                ISafeIdentifier t___11524 = sid__638("active");
                SqlBoolean t___11525 = new SqlBoolean(false);
                UpdateQuery t___11526 = S0::SrcGlobal.Update(t___11523).Set(t___11524, t___11525);
                SqlBuilder t___11527 = new SqlBuilder();
                t___11527.AppendSafe("age < ");
                t___11527.AppendInt32(18);
                UpdateQuery t___11531 = t___11526.Where(t___11527.Accumulated);
                SqlBuilder t___11532 = new SqlBuilder();
                t___11532.AppendSafe("role = ");
                t___11532.AppendString("guest");
                SqlFragment t___5654;
                t___5654 = t___11531.Where(t___11532.Accumulated).ToSql();
                SqlFragment q__1651 = t___5654;
                bool t___11539 = q__1651.ToString() == "UPDATE users SET active = FALSE WHERE age < 18 AND role = 'guest'";
                string fn__11522()
                {
                    return "update multi where";
                }
                test___165.Assert(t___11539, (S1::Func<string>) fn__11522);
            }
            finally
            {
                test___165.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryOrWhere__2281()
        {
            T::Test test___166 = new T::Test();
            try
            {
                ISafeIdentifier t___11504 = sid__638("users");
                ISafeIdentifier t___11505 = sid__638("status");
                SqlString t___11506 = new SqlString("banned");
                UpdateQuery t___11507 = S0::SrcGlobal.Update(t___11504).Set(t___11505, t___11506);
                SqlBuilder t___11508 = new SqlBuilder();
                t___11508.AppendSafe("spam_count > ");
                t___11508.AppendInt32(10);
                UpdateQuery t___11512 = t___11507.Where(t___11508.Accumulated);
                SqlBuilder t___11513 = new SqlBuilder();
                t___11513.AppendSafe("reported = ");
                t___11513.AppendBoolean(true);
                SqlFragment t___5633;
                t___5633 = t___11512.OrWhere(t___11513.Accumulated).ToSql();
                SqlFragment q__1653 = t___5633;
                bool t___11520 = q__1653.ToString() == "UPDATE users SET status = 'banned' WHERE spam_count > 10 OR reported = TRUE";
                string fn__11503()
                {
                    return "update orWhere";
                }
                test___166.Assert(t___11520, (S1::Func<string>) fn__11503);
            }
            finally
            {
                test___166.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutWhere__2284()
        {
            T::Test test___167 = new T::Test();
            try
            {
                ISafeIdentifier t___11497;
                ISafeIdentifier t___11498;
                SqlInt32 t___11499;
                bool didBubble__1655;
                try
                {
                    t___11497 = sid__638("users");
                    t___11498 = sid__638("x");
                    t___11499 = new SqlInt32(1);
                    S0::SrcGlobal.Update(t___11497).Set(t___11498, t___11499).ToSql();
                    didBubble__1655 = false;
                }
                catch
                {
                    didBubble__1655 = true;
                }
                string fn__11496()
                {
                    return "update without WHERE should bubble";
                }
                test___167.Assert(didBubble__1655, (S1::Func<string>) fn__11496);
            }
            finally
            {
                test___167.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutSet__2285()
        {
            T::Test test___168 = new T::Test();
            try
            {
                ISafeIdentifier t___11488;
                SqlBuilder t___11489;
                SqlFragment t___11492;
                bool didBubble__1657;
                try
                {
                    t___11488 = sid__638("users");
                    t___11489 = new SqlBuilder();
                    t___11489.AppendSafe("id = ");
                    t___11489.AppendInt32(1);
                    t___11492 = t___11489.Accumulated;
                    S0::SrcGlobal.Update(t___11488).Where(t___11492).ToSql();
                    didBubble__1657 = false;
                }
                catch
                {
                    didBubble__1657 = true;
                }
                string fn__11487()
                {
                    return "update without SET should bubble";
                }
                test___168.Assert(didBubble__1657, (S1::Func<string>) fn__11487);
            }
            finally
            {
                test___168.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryWithLimit__2287()
        {
            T::Test test___169 = new T::Test();
            try
            {
                ISafeIdentifier t___11474 = sid__638("users");
                ISafeIdentifier t___11475 = sid__638("active");
                SqlBoolean t___11476 = new SqlBoolean(false);
                UpdateQuery t___11477 = S0::SrcGlobal.Update(t___11474).Set(t___11475, t___11476);
                SqlBuilder t___11478 = new SqlBuilder();
                t___11478.AppendSafe("last_login < ");
                t___11478.AppendString("2024-01-01");
                UpdateQuery t___5596;
                t___5596 = t___11477.Where(t___11478.Accumulated).Limit(100);
                SqlFragment t___5597;
                t___5597 = t___5596.ToSql();
                SqlFragment q__1659 = t___5597;
                bool t___11485 = q__1659.ToString() == "UPDATE users SET active = FALSE WHERE last_login < '2024-01-01' LIMIT 100";
                string fn__11473()
                {
                    return "update limit";
                }
                test___169.Assert(t___11485, (S1::Func<string>) fn__11473);
            }
            finally
            {
                test___169.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryEscaping__2289()
        {
            T::Test test___170 = new T::Test();
            try
            {
                ISafeIdentifier t___11460 = sid__638("users");
                ISafeIdentifier t___11461 = sid__638("bio");
                SqlString t___11462 = new SqlString("It's a test");
                UpdateQuery t___11463 = S0::SrcGlobal.Update(t___11460).Set(t___11461, t___11462);
                SqlBuilder t___11464 = new SqlBuilder();
                t___11464.AppendSafe("id = ");
                t___11464.AppendInt32(1);
                SqlFragment t___5581;
                t___5581 = t___11463.Where(t___11464.Accumulated).ToSql();
                SqlFragment q__1661 = t___5581;
                bool t___11471 = q__1661.ToString() == "UPDATE users SET bio = 'It''s a test' WHERE id = 1";
                string fn__11459()
                {
                    return "update escaping";
                }
                test___170.Assert(t___11471, (S1::Func<string>) fn__11459);
            }
            finally
            {
                test___170.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBasic__2291()
        {
            T::Test test___171 = new T::Test();
            try
            {
                ISafeIdentifier t___11449 = sid__638("users");
                SqlBuilder t___11450 = new SqlBuilder();
                t___11450.AppendSafe("id = ");
                t___11450.AppendInt32(1);
                SqlFragment t___11453 = t___11450.Accumulated;
                SqlFragment t___5566;
                t___5566 = S0::SrcGlobal.DeleteFrom(t___11449).Where(t___11453).ToSql();
                SqlFragment q__1663 = t___5566;
                bool t___11457 = q__1663.ToString() == "DELETE FROM users WHERE id = 1";
                string fn__11448()
                {
                    return "delete basic";
                }
                test___171.Assert(t___11457, (S1::Func<string>) fn__11448);
            }
            finally
            {
                test___171.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryMultipleWhere__2293()
        {
            T::Test test___172 = new T::Test();
            try
            {
                ISafeIdentifier t___11433 = sid__638("logs");
                SqlBuilder t___11434 = new SqlBuilder();
                t___11434.AppendSafe("created_at < ");
                t___11434.AppendString("2024-01-01");
                SqlFragment t___11437 = t___11434.Accumulated;
                DeleteQuery t___11438 = S0::SrcGlobal.DeleteFrom(t___11433).Where(t___11437);
                SqlBuilder t___11439 = new SqlBuilder();
                t___11439.AppendSafe("level = ");
                t___11439.AppendString("debug");
                SqlFragment t___5554;
                t___5554 = t___11438.Where(t___11439.Accumulated).ToSql();
                SqlFragment q__1665 = t___5554;
                bool t___11446 = q__1665.ToString() == "DELETE FROM logs WHERE created_at < '2024-01-01' AND level = 'debug'";
                string fn__11432()
                {
                    return "delete multi where";
                }
                test___172.Assert(t___11446, (S1::Func<string>) fn__11432);
            }
            finally
            {
                test___172.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBubblesWithoutWhere__2296()
        {
            T::Test test___173 = new T::Test();
            try
            {
                bool didBubble__1667;
                try
                {
                    S0::SrcGlobal.DeleteFrom(sid__638("users")).ToSql();
                    didBubble__1667 = false;
                }
                catch
                {
                    didBubble__1667 = true;
                }
                string fn__11428()
                {
                    return "delete without WHERE should bubble";
                }
                test___173.Assert(didBubble__1667, (S1::Func<string>) fn__11428);
            }
            finally
            {
                test___173.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryOrWhere__2297()
        {
            T::Test test___174 = new T::Test();
            try
            {
                ISafeIdentifier t___11413 = sid__638("sessions");
                SqlBuilder t___11414 = new SqlBuilder();
                t___11414.AppendSafe("expired = ");
                t___11414.AppendBoolean(true);
                SqlFragment t___11417 = t___11414.Accumulated;
                DeleteQuery t___11418 = S0::SrcGlobal.DeleteFrom(t___11413).Where(t___11417);
                SqlBuilder t___11419 = new SqlBuilder();
                t___11419.AppendSafe("created_at < ");
                t___11419.AppendString("2023-01-01");
                SqlFragment t___5533;
                t___5533 = t___11418.OrWhere(t___11419.Accumulated).ToSql();
                SqlFragment q__1669 = t___5533;
                bool t___11426 = q__1669.ToString() == "DELETE FROM sessions WHERE expired = TRUE OR created_at < '2023-01-01'";
                string fn__11412()
                {
                    return "delete orWhere";
                }
                test___174.Assert(t___11426, (S1::Func<string>) fn__11412);
            }
            finally
            {
                test___174.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryWithLimit__2300()
        {
            T::Test test___175 = new T::Test();
            try
            {
                ISafeIdentifier t___11402 = sid__638("logs");
                SqlBuilder t___11403 = new SqlBuilder();
                t___11403.AppendSafe("level = ");
                t___11403.AppendString("debug");
                SqlFragment t___11406 = t___11403.Accumulated;
                DeleteQuery t___5514;
                t___5514 = S0::SrcGlobal.DeleteFrom(t___11402).Where(t___11406).Limit(1000);
                SqlFragment t___5515;
                t___5515 = t___5514.ToSql();
                SqlFragment q__1671 = t___5515;
                bool t___11410 = q__1671.ToString() == "DELETE FROM logs WHERE level = 'debug' LIMIT 1000";
                string fn__11401()
                {
                    return "delete limit";
                }
                test___175.Assert(t___11410, (S1::Func<string>) fn__11401);
            }
            finally
            {
                test___175.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsFirst__2302()
        {
            T::Test test___176 = new T::Test();
            try
            {
                ISafeIdentifier t___11392 = sid__638("users");
                ISafeIdentifier t___11393 = sid__638("email");
                NullsFirst t___11394 = new NullsFirst();
                Query q__1673 = S0::SrcGlobal.From(t___11392).OrderByNulls(t___11393, true, t___11394);
                bool t___11399 = q__1673.ToSql().ToString() == "SELECT * FROM users ORDER BY email ASC NULLS FIRST";
                string fn__11391()
                {
                    return "nulls first";
                }
                test___176.Assert(t___11399, (S1::Func<string>) fn__11391);
            }
            finally
            {
                test___176.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsLast__2303()
        {
            T::Test test___177 = new T::Test();
            try
            {
                ISafeIdentifier t___11382 = sid__638("users");
                ISafeIdentifier t___11383 = sid__638("score");
                NullsLast t___11384 = new NullsLast();
                Query q__1675 = S0::SrcGlobal.From(t___11382).OrderByNulls(t___11383, false, t___11384);
                bool t___11389 = q__1675.ToSql().ToString() == "SELECT * FROM users ORDER BY score DESC NULLS LAST";
                string fn__11381()
                {
                    return "nulls last";
                }
                test___177.Assert(t___11389, (S1::Func<string>) fn__11381);
            }
            finally
            {
                test___177.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedOrderByAndOrderByNulls__2304()
        {
            T::Test test___178 = new T::Test();
            try
            {
                ISafeIdentifier t___11370 = sid__638("users");
                ISafeIdentifier t___11371 = sid__638("name");
                Query q__1677 = S0::SrcGlobal.From(t___11370).OrderBy(t___11371, true).OrderByNulls(sid__638("email"), true, new NullsFirst());
                bool t___11379 = q__1677.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC, email ASC NULLS FIRST";
                string fn__11369()
                {
                    return "mixed order";
                }
                test___178.Assert(t___11379, (S1::Func<string>) fn__11369);
            }
            finally
            {
                test___178.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoin__2305()
        {
            T::Test test___179 = new T::Test();
            try
            {
                ISafeIdentifier t___11361 = sid__638("users");
                ISafeIdentifier t___11362 = sid__638("colors");
                Query q__1679 = S0::SrcGlobal.From(t___11361).CrossJoin(t___11362);
                bool t___11367 = q__1679.ToSql().ToString() == "SELECT * FROM users CROSS JOIN colors";
                string fn__11360()
                {
                    return "cross join";
                }
                test___179.Assert(t___11367, (S1::Func<string>) fn__11360);
            }
            finally
            {
                test___179.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoinCombinedWithOtherJoins__2306()
        {
            T::Test test___180 = new T::Test();
            try
            {
                ISafeIdentifier t___11347 = sid__638("users");
                ISafeIdentifier t___11348 = sid__638("orders");
                SqlBuilder t___11349 = new SqlBuilder();
                t___11349.AppendSafe("users.id = orders.user_id");
                SqlFragment t___11351 = t___11349.Accumulated;
                Query q__1681 = S0::SrcGlobal.From(t___11347).InnerJoin(t___11348, t___11351).CrossJoin(sid__638("colors"));
                bool t___11358 = q__1681.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id CROSS JOIN colors";
                string fn__11346()
                {
                    return "cross + inner join";
                }
                test___180.Assert(t___11358, (S1::Func<string>) fn__11346);
            }
            finally
            {
                test___180.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForUpdate__2308()
        {
            T::Test test___181 = new T::Test();
            try
            {
                ISafeIdentifier t___11333 = sid__638("users");
                SqlBuilder t___11334 = new SqlBuilder();
                t___11334.AppendSafe("id = ");
                t___11334.AppendInt32(1);
                SqlFragment t___11337 = t___11334.Accumulated;
                Query q__1683 = S0::SrcGlobal.From(t___11333).Where(t___11337).Lock(new ForUpdate());
                bool t___11344 = q__1683.ToSql().ToString() == "SELECT * FROM users WHERE id = 1 FOR UPDATE";
                string fn__11332()
                {
                    return "for update";
                }
                test___181.Assert(t___11344, (S1::Func<string>) fn__11332);
            }
            finally
            {
                test___181.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForShare__2310()
        {
            T::Test test___182 = new T::Test();
            try
            {
                ISafeIdentifier t___11322 = sid__638("users");
                ISafeIdentifier t___11323 = sid__638("name");
                Query q__1685 = S0::SrcGlobal.From(t___11322).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11323)).Lock(new ForShare());
                bool t___11330 = q__1685.ToSql().ToString() == "SELECT name FROM users FOR SHARE";
                string fn__11321()
                {
                    return "for share";
                }
                test___182.Assert(t___11330, (S1::Func<string>) fn__11321);
            }
            finally
            {
                test___182.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockWithFullQuery__2311()
        {
            T::Test test___183 = new T::Test();
            try
            {
                ISafeIdentifier t___11308 = sid__638("accounts");
                SqlBuilder t___11309 = new SqlBuilder();
                t___11309.AppendSafe("id = ");
                t___11309.AppendInt32(42);
                SqlFragment t___11312 = t___11309.Accumulated;
                Query t___5438;
                t___5438 = S0::SrcGlobal.From(t___11308).Where(t___11312).Limit(1);
                Query t___11315 = t___5438.Lock(new ForUpdate());
                Query q__1687 = t___11315;
                bool t___11319 = q__1687.ToSql().ToString() == "SELECT * FROM accounts WHERE id = 42 LIMIT 1 FOR UPDATE";
                string fn__11307()
                {
                    return "lock full query";
                }
                test___183.Assert(t___11319, (S1::Func<string>) fn__11307);
            }
            finally
            {
                test___183.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__2313()
        {
            T::Test test___190 = new T::Test();
            try
            {
                ISafeIdentifier t___5427;
                t___5427 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1725 = t___5427;
                bool t___11305 = id__1725.SqlValue == "user_name";
                string fn__11302()
                {
                    return "value should round-trip";
                }
                test___190.Assert(t___11305, (S1::Func<string>) fn__11302);
            }
            finally
            {
                test___190.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__2314()
        {
            T::Test test___191 = new T::Test();
            try
            {
                bool didBubble__1727;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1727 = false;
                }
                catch
                {
                    didBubble__1727 = true;
                }
                string fn__11299()
                {
                    return "empty string should bubble";
                }
                test___191.Assert(didBubble__1727, (S1::Func<string>) fn__11299);
            }
            finally
            {
                test___191.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__2315()
        {
            T::Test test___192 = new T::Test();
            try
            {
                bool didBubble__1729;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1729 = false;
                }
                catch
                {
                    didBubble__1729 = true;
                }
                string fn__11296()
                {
                    return "leading digit should bubble";
                }
                test___192.Assert(didBubble__1729, (S1::Func<string>) fn__11296);
            }
            finally
            {
                test___192.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__2316()
        {
            T::Test test___193 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1731 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__11293(string c__1732)
                {
                    bool didBubble__1733;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1732);
                        didBubble__1733 = false;
                    }
                    catch
                    {
                        didBubble__1733 = true;
                    }
                    string fn__11290()
                    {
                        return "should reject: " + c__1732;
                    }
                    test___193.Assert(didBubble__1733, (S1::Func<string>) fn__11290);
                }
                C::Listed.ForEach(cases__1731, (S1::Action<string>) fn__11293);
            }
            finally
            {
                test___193.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__2317()
        {
            T::Test test___194 = new T::Test();
            try
            {
                ISafeIdentifier t___5404;
                t___5404 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___5405 = t___5404;
                ISafeIdentifier t___5406;
                t___5406 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5407 = t___5406;
                StringField t___11280 = new StringField();
                FieldDef t___11281 = new FieldDef(t___5407, t___11280, false);
                ISafeIdentifier t___5410;
                t___5410 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___5411 = t___5410;
                IntField t___11282 = new IntField();
                FieldDef t___11283 = new FieldDef(t___5411, t___11282, false);
                TableDef td__1735 = new TableDef(t___5405, C::Listed.CreateReadOnlyList<FieldDef>(t___11281, t___11283));
                FieldDef t___5415;
                t___5415 = td__1735.Field("age");
                FieldDef f__1736 = t___5415;
                bool t___11288 = f__1736.Name.SqlValue == "age";
                string fn__11279()
                {
                    return "should find age field";
                }
                test___194.Assert(t___11288, (S1::Func<string>) fn__11279);
            }
            finally
            {
                test___194.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__2318()
        {
            T::Test test___195 = new T::Test();
            try
            {
                ISafeIdentifier t___5395;
                t___5395 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___5396 = t___5395;
                ISafeIdentifier t___5397;
                t___5397 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___5398 = t___5397;
                StringField t___11274 = new StringField();
                FieldDef t___11275 = new FieldDef(t___5398, t___11274, false);
                TableDef td__1738 = new TableDef(t___5396, C::Listed.CreateReadOnlyList<FieldDef>(t___11275));
                bool didBubble__1739;
                try
                {
                    td__1738.Field("nonexistent");
                    didBubble__1739 = false;
                }
                catch
                {
                    didBubble__1739 = true;
                }
                string fn__11273()
                {
                    return "unknown field should bubble";
                }
                test___195.Assert(didBubble__1739, (S1::Func<string>) fn__11273);
            }
            finally
            {
                test___195.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__2319()
        {
            T::Test test___196 = new T::Test();
            try
            {
                ISafeIdentifier t___5383;
                t___5383 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___5384 = t___5383;
                StringField t___11262 = new StringField();
                FieldDef required__1741 = new FieldDef(t___5384, t___11262, false);
                ISafeIdentifier t___5387;
                t___5387 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___5388 = t___5387;
                StringField t___11264 = new StringField();
                FieldDef optional__1742 = new FieldDef(t___5388, t___11264, true);
                bool t___11268 = !required__1741.Nullable;
                string fn__11261()
                {
                    return "required field should not be nullable";
                }
                test___196.Assert(t___11268, (S1::Func<string>) fn__11261);
                bool t___11270 = optional__1742.Nullable;
                string fn__11260()
                {
                    return "optional field should be nullable";
                }
                test___196.Assert(t___11270, (S1::Func<string>) fn__11260);
            }
            finally
            {
                test___196.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__2320()
        {
            T::Test test___200 = new T::Test();
            try
            {
                string build__1868(string name__1870)
                {
                    SqlBuilder t___11242 = new SqlBuilder();
                    t___11242.AppendSafe("select * from hi where name = ");
                    t___11242.AppendString(name__1870);
                    return t___11242.Accumulated.ToString();
                }
                string buildWrong__1869(string name__1872)
                {
                    return "select * from hi where name = '" + name__1872 + "'";
                }
                string actual___2322 = build__1868("world");
                bool t___11252 = actual___2322 == "select * from hi where name = 'world'";
                string fn__11249()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___2322 + ")";
                }
                test___200.Assert(t___11252, (S1::Func<string>) fn__11249);
                string bobbyTables__1874 = "Robert'); drop table hi;--";
                string actual___2324 = build__1868("Robert'); drop table hi;--");
                bool t___11256 = actual___2324 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__11248()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___2324 + ")";
                }
                test___200.Assert(t___11256, (S1::Func<string>) fn__11248);
                string fn__11247()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___200.Assert(true, (S1::Func<string>) fn__11247);
            }
            finally
            {
                test___200.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__2328()
        {
            T::Test test___201 = new T::Test();
            try
            {
                SqlBuilder t___11210 = new SqlBuilder();
                t___11210.AppendSafe("v = ");
                t___11210.AppendString("");
                string actual___2329 = t___11210.Accumulated.ToString();
                bool t___11216 = actual___2329 == "v = ''";
                string fn__11209()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___2329 + ")";
                }
                test___201.Assert(t___11216, (S1::Func<string>) fn__11209);
                SqlBuilder t___11218 = new SqlBuilder();
                t___11218.AppendSafe("v = ");
                t___11218.AppendString("a''b");
                string actual___2332 = t___11218.Accumulated.ToString();
                bool t___11224 = actual___2332 == "v = 'a''''b'";
                string fn__11208()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___2332 + ")";
                }
                test___201.Assert(t___11224, (S1::Func<string>) fn__11208);
                SqlBuilder t___11226 = new SqlBuilder();
                t___11226.AppendSafe("v = ");
                t___11226.AppendString("Hello 世界");
                string actual___2335 = t___11226.Accumulated.ToString();
                bool t___11232 = actual___2335 == "v = 'Hello 世界'";
                string fn__11207()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___2335 + ")";
                }
                test___201.Assert(t___11232, (S1::Func<string>) fn__11207);
                SqlBuilder t___11234 = new SqlBuilder();
                t___11234.AppendSafe("v = ");
                t___11234.AppendString("Line1\nLine2");
                string actual___2338 = t___11234.Accumulated.ToString();
                bool t___11240 = actual___2338 == "v = 'Line1\nLine2'";
                string fn__11206()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___2338 + ")";
                }
                test___201.Assert(t___11240, (S1::Func<string>) fn__11206);
            }
            finally
            {
                test___201.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__2341()
        {
            T::Test test___202 = new T::Test();
            try
            {
                SqlBuilder t___11181 = new SqlBuilder();
                t___11181.AppendSafe("select ");
                t___11181.AppendInt32(42);
                t___11181.AppendSafe(", ");
                t___11181.AppendInt64(43);
                t___11181.AppendSafe(", ");
                t___11181.AppendFloat64(19.99);
                t___11181.AppendSafe(", ");
                t___11181.AppendBoolean(true);
                t___11181.AppendSafe(", ");
                t___11181.AppendBoolean(false);
                string actual___2342 = t___11181.Accumulated.ToString();
                bool t___11195 = actual___2342 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__11180()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___2342 + ")";
                }
                test___202.Assert(t___11195, (S1::Func<string>) fn__11180);
                S1::DateTime t___5328;
                t___5328 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1877 = t___5328;
                SqlBuilder t___11197 = new SqlBuilder();
                t___11197.AppendSafe("insert into t values (");
                t___11197.AppendDate(date__1877);
                t___11197.AppendSafe(")");
                string actual___2345 = t___11197.Accumulated.ToString();
                bool t___11204 = actual___2345 == "insert into t values ('2024-12-25')";
                string fn__11179()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___2345 + ")";
                }
                test___202.Assert(t___11204, (S1::Func<string>) fn__11179);
            }
            finally
            {
                test___202.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__2348()
        {
            T::Test test___203 = new T::Test();
            try
            {
                SqlBuilder t___11125 = new SqlBuilder();
                t___11125.AppendSafe("v IN (");
                t___11125.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___11125.AppendSafe(")");
                string actual___2349 = t___11125.Accumulated.ToString();
                bool t___11132 = actual___2349 == "v IN ('a', 'b', 'c''d')";
                string fn__11124()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___2349 + ")";
                }
                test___203.Assert(t___11132, (S1::Func<string>) fn__11124);
                SqlBuilder t___11134 = new SqlBuilder();
                t___11134.AppendSafe("v IN (");
                t___11134.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___11134.AppendSafe(")");
                string actual___2352 = t___11134.Accumulated.ToString();
                bool t___11141 = actual___2352 == "v IN (1, 2, 3)";
                string fn__11123()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___2352 + ")";
                }
                test___203.Assert(t___11141, (S1::Func<string>) fn__11123);
                SqlBuilder t___11143 = new SqlBuilder();
                t___11143.AppendSafe("v IN (");
                t___11143.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___11143.AppendSafe(")");
                string actual___2355 = t___11143.Accumulated.ToString();
                bool t___11150 = actual___2355 == "v IN (1, 2)";
                string fn__11122()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___2355 + ")";
                }
                test___203.Assert(t___11150, (S1::Func<string>) fn__11122);
                SqlBuilder t___11152 = new SqlBuilder();
                t___11152.AppendSafe("v IN (");
                t___11152.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___11152.AppendSafe(")");
                string actual___2358 = t___11152.Accumulated.ToString();
                bool t___11159 = actual___2358 == "v IN (1.0, 2.0)";
                string fn__11121()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___2358 + ")";
                }
                test___203.Assert(t___11159, (S1::Func<string>) fn__11121);
                SqlBuilder t___11161 = new SqlBuilder();
                t___11161.AppendSafe("v IN (");
                t___11161.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___11161.AppendSafe(")");
                string actual___2361 = t___11161.Accumulated.ToString();
                bool t___11168 = actual___2361 == "v IN (TRUE, FALSE)";
                string fn__11120()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___2361 + ")";
                }
                test___203.Assert(t___11168, (S1::Func<string>) fn__11120);
                S1::DateTime t___5300;
                t___5300 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___5301 = t___5300;
                S1::DateTime t___5302;
                t___5302 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___5303 = t___5302;
                G::IReadOnlyList<S1::DateTime> dates__1879 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___5301, t___5303);
                SqlBuilder t___11170 = new SqlBuilder();
                t___11170.AppendSafe("v IN (");
                t___11170.AppendDateList(dates__1879);
                t___11170.AppendSafe(")");
                string actual___2364 = t___11170.Accumulated.ToString();
                bool t___11177 = actual___2364 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__11119()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___2364 + ")";
                }
                test___203.Assert(t___11177, (S1::Func<string>) fn__11119);
            }
            finally
            {
                test___203.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__2367()
        {
            T::Test test___204 = new T::Test();
            try
            {
                double nan__1881;
                nan__1881 = 0.0 / 0.0;
                SqlBuilder t___11111 = new SqlBuilder();
                t___11111.AppendSafe("v = ");
                t___11111.AppendFloat64(nan__1881);
                string actual___2368 = t___11111.Accumulated.ToString();
                bool t___11117 = actual___2368 == "v = NULL";
                string fn__11110()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___2368 + ")";
                }
                test___204.Assert(t___11117, (S1::Func<string>) fn__11110);
            }
            finally
            {
                test___204.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__2371()
        {
            T::Test test___205 = new T::Test();
            try
            {
                double inf__1883;
                inf__1883 = 1.0 / 0.0;
                SqlBuilder t___11102 = new SqlBuilder();
                t___11102.AppendSafe("v = ");
                t___11102.AppendFloat64(inf__1883);
                string actual___2372 = t___11102.Accumulated.ToString();
                bool t___11108 = actual___2372 == "v = NULL";
                string fn__11101()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___2372 + ")";
                }
                test___205.Assert(t___11108, (S1::Func<string>) fn__11101);
            }
            finally
            {
                test___205.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__2375()
        {
            T::Test test___206 = new T::Test();
            try
            {
                double ninf__1885;
                ninf__1885 = -1.0 / 0.0;
                SqlBuilder t___11093 = new SqlBuilder();
                t___11093.AppendSafe("v = ");
                t___11093.AppendFloat64(ninf__1885);
                string actual___2376 = t___11093.Accumulated.ToString();
                bool t___11099 = actual___2376 == "v = NULL";
                string fn__11092()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___2376 + ")";
                }
                test___206.Assert(t___11099, (S1::Func<string>) fn__11092);
            }
            finally
            {
                test___206.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__2379()
        {
            T::Test test___207 = new T::Test();
            try
            {
                SqlBuilder t___11068 = new SqlBuilder();
                t___11068.AppendSafe("v = ");
                t___11068.AppendFloat64(3.14);
                string actual___2380 = t___11068.Accumulated.ToString();
                bool t___11074 = actual___2380 == "v = 3.14";
                string fn__11067()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___2380 + ")";
                }
                test___207.Assert(t___11074, (S1::Func<string>) fn__11067);
                SqlBuilder t___11076 = new SqlBuilder();
                t___11076.AppendSafe("v = ");
                t___11076.AppendFloat64(0.0);
                string actual___2383 = t___11076.Accumulated.ToString();
                bool t___11082 = actual___2383 == "v = 0.0";
                string fn__11066()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___2383 + ")";
                }
                test___207.Assert(t___11082, (S1::Func<string>) fn__11066);
                SqlBuilder t___11084 = new SqlBuilder();
                t___11084.AppendSafe("v = ");
                t___11084.AppendFloat64(-42.5);
                string actual___2386 = t___11084.Accumulated.ToString();
                bool t___11090 = actual___2386 == "v = -42.5";
                string fn__11065()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___2386 + ")";
                }
                test___207.Assert(t___11090, (S1::Func<string>) fn__11065);
            }
            finally
            {
                test___207.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__2389()
        {
            T::Test test___208 = new T::Test();
            try
            {
                S1::DateTime t___5196;
                t___5196 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1888 = t___5196;
                SqlBuilder t___11057 = new SqlBuilder();
                t___11057.AppendSafe("v = ");
                t___11057.AppendDate(d__1888);
                string actual___2390 = t___11057.Accumulated.ToString();
                bool t___11063 = actual___2390 == "v = '2024-06-15'";
                string fn__11056()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___2390 + ")";
                }
                test___208.Assert(t___11063, (S1::Func<string>) fn__11056);
            }
            finally
            {
                test___208.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__2393()
        {
            T::Test test___209 = new T::Test();
            try
            {
                string name__1890 = "Someone";
                SqlBuilder t___11025 = new SqlBuilder();
                t___11025.AppendSafe("where p.last_name = ");
                t___11025.AppendString("Someone");
                SqlFragment condition__1891 = t___11025.Accumulated;
                SqlBuilder t___11029 = new SqlBuilder();
                t___11029.AppendSafe("select p.id from person p ");
                t___11029.AppendFragment(condition__1891);
                string actual___2395 = t___11029.Accumulated.ToString();
                bool t___11035 = actual___2395 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__11024()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2395 + ")";
                }
                test___209.Assert(t___11035, (S1::Func<string>) fn__11024);
                SqlBuilder t___11037 = new SqlBuilder();
                t___11037.AppendSafe("select p.id from person p ");
                t___11037.AppendPart(condition__1891.ToSource());
                string actual___2398 = t___11037.Accumulated.ToString();
                bool t___11044 = actual___2398 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__11023()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2398 + ")";
                }
                test___209.Assert(t___11044, (S1::Func<string>) fn__11023);
                G::IReadOnlyList<ISqlPart> parts__1892 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___11048 = new SqlBuilder();
                t___11048.AppendSafe("select ");
                t___11048.AppendPartList(parts__1892);
                string actual___2401 = t___11048.Accumulated.ToString();
                bool t___11054 = actual___2401 == "select 'a''b', 3";
                string fn__11022()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___2401 + ")";
                }
                test___209.Assert(t___11054, (S1::Func<string>) fn__11022);
            }
            finally
            {
                test___209.SoftFailToHard();
            }
        }
    }
}
