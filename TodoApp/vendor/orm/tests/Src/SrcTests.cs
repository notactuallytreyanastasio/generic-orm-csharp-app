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
        internal static ISafeIdentifier csid__703(string name__985)
        {
            ISafeIdentifier t___9421;
            t___9421 = S0::SrcGlobal.SafeIdentifier(name__985);
            return t___9421;
        }
        internal static TableDef userTable__704()
        {
            return new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("email"), new StringField(), false, null, false), new FieldDef(csid__703("age"), new IntField(), true, null, false), new FieldDef(csid__703("score"), new FloatField(), true, null, false), new FieldDef(csid__703("active"), new BoolField(), true, null, false)), null);
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__2272()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__989 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___16959 = userTable__704();
                ISafeIdentifier t___16960 = csid__703("name");
                ISafeIdentifier t___16961 = csid__703("email");
                IChangeset cs__990 = S0::SrcGlobal.Changeset(t___16959, params__989).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16960, t___16961));
                bool t___16964 = C::Mapped.ContainsKey(cs__990.Changes, "name");
                string fn__16954()
                {
                    return "name should be in changes";
                }
                test___32.Assert(t___16964, (S1::Func<string>) fn__16954);
                bool t___16968 = C::Mapped.ContainsKey(cs__990.Changes, "email");
                string fn__16953()
                {
                    return "email should be in changes";
                }
                test___32.Assert(t___16968, (S1::Func<string>) fn__16953);
                bool t___16974 = !C::Mapped.ContainsKey(cs__990.Changes, "admin");
                string fn__16952()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___32.Assert(t___16974, (S1::Func<string>) fn__16952);
                bool t___16976 = cs__990.IsValid;
                string fn__16951()
                {
                    return "should still be valid";
                }
                test___32.Assert(t___16976, (S1::Func<string>) fn__16951);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__2273()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__992 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___16937 = userTable__704();
                ISafeIdentifier t___16938 = csid__703("name");
                IChangeset cs__993 = S0::SrcGlobal.Changeset(t___16937, params__992).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16938)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("email")));
                bool t___16945 = !C::Mapped.ContainsKey(cs__993.Changes, "name");
                string fn__16933()
                {
                    return "name must be excluded by second cast";
                }
                test___33.Assert(t___16945, (S1::Func<string>) fn__16933);
                bool t___16948 = C::Mapped.ContainsKey(cs__993.Changes, "email");
                string fn__16932()
                {
                    return "email should be present";
                }
                test___33.Assert(t___16948, (S1::Func<string>) fn__16932);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__2274()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__995 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___16919 = userTable__704();
                ISafeIdentifier t___16920 = csid__703("name");
                ISafeIdentifier t___16921 = csid__703("email");
                IChangeset cs__996 = S0::SrcGlobal.Changeset(t___16919, params__995).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16920, t___16921));
                bool t___16926 = !C::Mapped.ContainsKey(cs__996.Changes, "name");
                string fn__16915()
                {
                    return "empty name should not be in changes";
                }
                test___34.Assert(t___16926, (S1::Func<string>) fn__16915);
                bool t___16929 = C::Mapped.ContainsKey(cs__996.Changes, "email");
                string fn__16914()
                {
                    return "email should be in changes";
                }
                test___34.Assert(t___16929, (S1::Func<string>) fn__16914);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__2275()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__998 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16901 = userTable__704();
                ISafeIdentifier t___16902 = csid__703("name");
                IChangeset cs__999 = S0::SrcGlobal.Changeset(t___16901, params__998).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16902)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name")));
                bool t___16906 = cs__999.IsValid;
                string fn__16898()
                {
                    return "should be valid";
                }
                test___35.Assert(t___16906, (S1::Func<string>) fn__16898);
                bool t___16912 = cs__999.Errors.Count == 0;
                string fn__16897()
                {
                    return "no errors expected";
                }
                test___35.Assert(t___16912, (S1::Func<string>) fn__16897);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__2276()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1001 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16877 = userTable__704();
                ISafeIdentifier t___16878 = csid__703("name");
                IChangeset cs__1002 = S0::SrcGlobal.Changeset(t___16877, params__1001).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16878)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name")));
                bool t___16884 = !cs__1002.IsValid;
                string fn__16875()
                {
                    return "should be invalid";
                }
                test___36.Assert(t___16884, (S1::Func<string>) fn__16875);
                bool t___16889 = cs__1002.Errors.Count == 1;
                string fn__16874()
                {
                    return "should have one error";
                }
                test___36.Assert(t___16889, (S1::Func<string>) fn__16874);
                bool t___16895 = cs__1002.Errors[0].Field == "name";
                string fn__16873()
                {
                    return "error should name the field";
                }
                test___36.Assert(t___16895, (S1::Func<string>) fn__16873);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__2277()
        {
            T::Test test___37 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1004 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16865 = userTable__704();
                ISafeIdentifier t___16866 = csid__703("name");
                IChangeset cs__1005 = S0::SrcGlobal.Changeset(t___16865, params__1004).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16866)).ValidateLength(csid__703("name"), 2, 50);
                bool t___16870 = cs__1005.IsValid;
                string fn__16862()
                {
                    return "should be valid";
                }
                test___37.Assert(t___16870, (S1::Func<string>) fn__16862);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__2278()
        {
            T::Test test___38 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1007 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___16853 = userTable__704();
                ISafeIdentifier t___16854 = csid__703("name");
                IChangeset cs__1008 = S0::SrcGlobal.Changeset(t___16853, params__1007).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16854)).ValidateLength(csid__703("name"), 2, 50);
                bool t___16860 = !cs__1008.IsValid;
                string fn__16850()
                {
                    return "should be invalid";
                }
                test___38.Assert(t___16860, (S1::Func<string>) fn__16850);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__2279()
        {
            T::Test test___39 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1010 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___16841 = userTable__704();
                ISafeIdentifier t___16842 = csid__703("name");
                IChangeset cs__1011 = S0::SrcGlobal.Changeset(t___16841, params__1010).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16842)).ValidateLength(csid__703("name"), 2, 10);
                bool t___16848 = !cs__1011.IsValid;
                string fn__16838()
                {
                    return "should be invalid";
                }
                test___39.Assert(t___16848, (S1::Func<string>) fn__16838);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__2280()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1013 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___16830 = userTable__704();
                ISafeIdentifier t___16831 = csid__703("age");
                IChangeset cs__1014 = S0::SrcGlobal.Changeset(t___16830, params__1013).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16831)).ValidateInt(csid__703("age"));
                bool t___16835 = cs__1014.IsValid;
                string fn__16827()
                {
                    return "should be valid";
                }
                test___40.Assert(t___16835, (S1::Func<string>) fn__16827);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__2281()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1016 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___16818 = userTable__704();
                ISafeIdentifier t___16819 = csid__703("age");
                IChangeset cs__1017 = S0::SrcGlobal.Changeset(t___16818, params__1016).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16819)).ValidateInt(csid__703("age"));
                bool t___16825 = !cs__1017.IsValid;
                string fn__16815()
                {
                    return "should be invalid";
                }
                test___41.Assert(t___16825, (S1::Func<string>) fn__16815);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__2282()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1019 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___16807 = userTable__704();
                ISafeIdentifier t___16808 = csid__703("score");
                IChangeset cs__1020 = S0::SrcGlobal.Changeset(t___16807, params__1019).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16808)).ValidateFloat(csid__703("score"));
                bool t___16812 = cs__1020.IsValid;
                string fn__16804()
                {
                    return "should be valid";
                }
                test___42.Assert(t___16812, (S1::Func<string>) fn__16804);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__2283()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1022 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___16796 = userTable__704();
                ISafeIdentifier t___16797 = csid__703("age");
                IChangeset cs__1023 = S0::SrcGlobal.Changeset(t___16796, params__1022).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16797)).ValidateInt64(csid__703("age"));
                bool t___16801 = cs__1023.IsValid;
                string fn__16793()
                {
                    return "should be valid";
                }
                test___43.Assert(t___16801, (S1::Func<string>) fn__16793);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__2284()
        {
            T::Test test___44 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1025 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___16784 = userTable__704();
                ISafeIdentifier t___16785 = csid__703("age");
                IChangeset cs__1026 = S0::SrcGlobal.Changeset(t___16784, params__1025).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16785)).ValidateInt64(csid__703("age"));
                bool t___16791 = !cs__1026.IsValid;
                string fn__16781()
                {
                    return "should be invalid";
                }
                test___44.Assert(t___16791, (S1::Func<string>) fn__16781);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__2285()
        {
            T::Test test___45 = new T::Test();
            try
            {
                void fn__16778(string v__1028)
                {
                    G::IReadOnlyDictionary<string, string> params__1029 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1028)));
                    TableDef t___16770 = userTable__704();
                    ISafeIdentifier t___16771 = csid__703("active");
                    IChangeset cs__1030 = S0::SrcGlobal.Changeset(t___16770, params__1029).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16771)).ValidateBool(csid__703("active"));
                    bool t___16775 = cs__1030.IsValid;
                    string fn__16767()
                    {
                        return "should accept: " + v__1028;
                    }
                    test___45.Assert(t___16775, (S1::Func<string>) fn__16767);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__16778);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__2286()
        {
            T::Test test___46 = new T::Test();
            try
            {
                void fn__16764(string v__1032)
                {
                    G::IReadOnlyDictionary<string, string> params__1033 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1032)));
                    TableDef t___16756 = userTable__704();
                    ISafeIdentifier t___16757 = csid__703("active");
                    IChangeset cs__1034 = S0::SrcGlobal.Changeset(t___16756, params__1033).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16757)).ValidateBool(csid__703("active"));
                    bool t___16761 = cs__1034.IsValid;
                    string fn__16753()
                    {
                        return "should accept: " + v__1032;
                    }
                    test___46.Assert(t___16761, (S1::Func<string>) fn__16753);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__16764);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__2287()
        {
            T::Test test___47 = new T::Test();
            try
            {
                void fn__16750(string v__1036)
                {
                    G::IReadOnlyDictionary<string, string> params__1037 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1036)));
                    TableDef t___16741 = userTable__704();
                    ISafeIdentifier t___16742 = csid__703("active");
                    IChangeset cs__1038 = S0::SrcGlobal.Changeset(t___16741, params__1037).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16742)).ValidateBool(csid__703("active"));
                    bool t___16748 = !cs__1038.IsValid;
                    string fn__16738()
                    {
                        return "should reject ambiguous: " + v__1036;
                    }
                    test___47.Assert(t___16748, (S1::Func<string>) fn__16738);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__16750);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__2288()
        {
            T::Test test___48 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1040 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___16726 = userTable__704();
                ISafeIdentifier t___16727 = csid__703("name");
                ISafeIdentifier t___16728 = csid__703("email");
                IChangeset cs__1041 = S0::SrcGlobal.Changeset(t___16726, params__1040).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16727, t___16728)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name"), csid__703("email")));
                SqlFragment t___9222;
                t___9222 = cs__1041.ToInsertSql();
                SqlFragment sqlFrag__1042 = t___9222;
                string s__1043 = sqlFrag__1042.ToString();
                bool t___16735 = s__1043.IndexOf("''") >= 0;
                string fn__16722()
                {
                    return "single quote must be doubled: " + s__1043;
                }
                test___48.Assert(t___16735, (S1::Func<string>) fn__16722);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__2289()
        {
            T::Test test___49 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1045 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___16706 = userTable__704();
                ISafeIdentifier t___16707 = csid__703("name");
                ISafeIdentifier t___16708 = csid__703("email");
                IChangeset cs__1046 = S0::SrcGlobal.Changeset(t___16706, params__1045).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16707, t___16708)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name"), csid__703("email")));
                SqlFragment t___9201;
                t___9201 = cs__1046.ToInsertSql();
                SqlFragment sqlFrag__1047 = t___9201;
                string s__1048 = sqlFrag__1047.ToString();
                bool t___16715 = s__1048.IndexOf("INSERT INTO users") >= 0;
                string fn__16702()
                {
                    return "has INSERT INTO: " + s__1048;
                }
                test___49.Assert(t___16715, (S1::Func<string>) fn__16702);
                bool t___16719 = s__1048.IndexOf("'Alice'") >= 0;
                string fn__16701()
                {
                    return "has quoted name: " + s__1048;
                }
                test___49.Assert(t___16719, (S1::Func<string>) fn__16701);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__2290()
        {
            T::Test test___50 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1050 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___16688 = userTable__704();
                ISafeIdentifier t___16689 = csid__703("name");
                ISafeIdentifier t___16690 = csid__703("email");
                ISafeIdentifier t___16691 = csid__703("age");
                IChangeset cs__1051 = S0::SrcGlobal.Changeset(t___16688, params__1050).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16689, t___16690, t___16691)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name"), csid__703("email")));
                SqlFragment t___9184;
                t___9184 = cs__1051.ToInsertSql();
                SqlFragment sqlFrag__1052 = t___9184;
                string s__1053 = sqlFrag__1052.ToString();
                bool t___16698 = s__1053.IndexOf("25") >= 0;
                string fn__16683()
                {
                    return "age rendered unquoted: " + s__1053;
                }
                test___50.Assert(t___16698, (S1::Func<string>) fn__16683);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__2291()
        {
            T::Test test___51 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1055 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16676 = userTable__704();
                ISafeIdentifier t___16677 = csid__703("name");
                IChangeset cs__1056 = S0::SrcGlobal.Changeset(t___16676, params__1055).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16677)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name")));
                bool didBubble__1057;
                try
                {
                    cs__1056.ToInsertSql();
                    didBubble__1057 = false;
                }
                catch
                {
                    didBubble__1057 = true;
                }
                string fn__16674()
                {
                    return "invalid changeset should bubble";
                }
                test___51.Assert(didBubble__1057, (S1::Func<string>) fn__16674);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__2292()
        {
            T::Test test___52 = new T::Test();
            try
            {
                TableDef strictTable__1059 = new TableDef(csid__703("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("title"), new StringField(), false, null, false), new FieldDef(csid__703("body"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1060 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___16667 = csid__703("body");
                IChangeset cs__1061 = S0::SrcGlobal.Changeset(strictTable__1059, params__1060).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16667));
                bool t___16669 = cs__1061.IsValid;
                string fn__16656()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___52.Assert(t___16669, (S1::Func<string>) fn__16656);
                bool didBubble__1062;
                try
                {
                    cs__1061.ToInsertSql();
                    didBubble__1062 = false;
                }
                catch
                {
                    didBubble__1062 = true;
                }
                string fn__16655()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___52.Assert(didBubble__1062, (S1::Func<string>) fn__16655);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__2293()
        {
            T::Test test___53 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1064 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___16646 = userTable__704();
                ISafeIdentifier t___16647 = csid__703("name");
                IChangeset cs__1065 = S0::SrcGlobal.Changeset(t___16646, params__1064).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16647)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name")));
                SqlFragment t___9144;
                t___9144 = cs__1065.ToUpdateSql(42);
                SqlFragment sqlFrag__1066 = t___9144;
                string s__1067 = sqlFrag__1066.ToString();
                bool t___16653 = s__1067 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__16643()
                {
                    return "got: " + s__1067;
                }
                test___53.Assert(t___16653, (S1::Func<string>) fn__16643);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__2294()
        {
            T::Test test___54 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1069 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16636 = userTable__704();
                ISafeIdentifier t___16637 = csid__703("name");
                IChangeset cs__1070 = S0::SrcGlobal.Changeset(t___16636, params__1069).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16637)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name")));
                bool didBubble__1071;
                try
                {
                    cs__1070.ToUpdateSql(1);
                    didBubble__1071 = false;
                }
                catch
                {
                    didBubble__1071 = true;
                }
                string fn__16634()
                {
                    return "invalid changeset should bubble";
                }
                test___54.Assert(didBubble__1071, (S1::Func<string>) fn__16634);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeAddsANewField__2295()
        {
            T::Test test___55 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1073 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16620 = userTable__704();
                ISafeIdentifier t___16621 = csid__703("name");
                IChangeset cs__1074 = S0::SrcGlobal.Changeset(t___16620, params__1073).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16621)).PutChange(csid__703("email"), "alice@example.com");
                bool t___16626 = C::Mapped.ContainsKey(cs__1074.Changes, "email");
                string fn__16617()
                {
                    return "email should be in changes";
                }
                test___55.Assert(t___16626, (S1::Func<string>) fn__16617);
                bool t___16632 = C::Mapped.GetOrDefault(cs__1074.Changes, "email", "") == "alice@example.com";
                string fn__16616()
                {
                    return "email value";
                }
                test___55.Assert(t___16632, (S1::Func<string>) fn__16616);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeOverwritesExistingField__2296()
        {
            T::Test test___56 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1076 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16606 = userTable__704();
                ISafeIdentifier t___16607 = csid__703("name");
                IChangeset cs__1077 = S0::SrcGlobal.Changeset(t___16606, params__1076).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16607)).PutChange(csid__703("name"), "Bob");
                bool t___16614 = C::Mapped.GetOrDefault(cs__1077.Changes, "name", "") == "Bob";
                string fn__16603()
                {
                    return "name should be overwritten";
                }
                test___56.Assert(t___16614, (S1::Func<string>) fn__16603);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeValueAppearsInToInsertSql__2297()
        {
            T::Test test___57 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1079 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___16592 = userTable__704();
                ISafeIdentifier t___16593 = csid__703("name");
                ISafeIdentifier t___16594 = csid__703("email");
                IChangeset cs__1080 = S0::SrcGlobal.Changeset(t___16592, params__1079).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16593, t___16594)).PutChange(csid__703("name"), "Bob");
                SqlFragment t___9099;
                t___9099 = cs__1080.ToInsertSql();
                SqlFragment t___9100 = t___9099;
                string s__1081 = t___9100.ToString();
                bool t___16600 = s__1081.IndexOf("'Bob'") >= 0;
                string fn__16588()
                {
                    return "should use putChange value: " + s__1081;
                }
                test___57.Assert(t___16600, (S1::Func<string>) fn__16588);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void getChangeReturnsValueForExistingField__2298()
        {
            T::Test test___58 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1083 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16581 = userTable__704();
                ISafeIdentifier t___16582 = csid__703("name");
                IChangeset cs__1084 = S0::SrcGlobal.Changeset(t___16581, params__1083).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16582));
                string t___9087;
                t___9087 = cs__1084.GetChange(csid__703("name"));
                string val__1085 = t___9087;
                bool t___16586 = val__1085 == "Alice";
                string fn__16578()
                {
                    return "should return Alice";
                }
                test___58.Assert(t___16586, (S1::Func<string>) fn__16578);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void getChangeBubblesOnMissingField__2299()
        {
            T::Test test___59 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1087 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16572 = userTable__704();
                ISafeIdentifier t___16573 = csid__703("name");
                IChangeset cs__1088 = S0::SrcGlobal.Changeset(t___16572, params__1087).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16573));
                bool didBubble__1089;
                try
                {
                    cs__1088.GetChange(csid__703("email"));
                    didBubble__1089 = false;
                }
                catch
                {
                    didBubble__1089 = true;
                }
                string fn__16569()
                {
                    return "should bubble for missing field";
                }
                test___59.Assert(didBubble__1089, (S1::Func<string>) fn__16569);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeRemovesField__2300()
        {
            T::Test test___60 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1091 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___16554 = userTable__704();
                ISafeIdentifier t___16555 = csid__703("name");
                ISafeIdentifier t___16556 = csid__703("email");
                IChangeset cs__1092 = S0::SrcGlobal.Changeset(t___16554, params__1091).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16555, t___16556)).DeleteChange(csid__703("email"));
                bool t___16563 = !C::Mapped.ContainsKey(cs__1092.Changes, "email");
                string fn__16550()
                {
                    return "email should be removed";
                }
                test___60.Assert(t___16563, (S1::Func<string>) fn__16550);
                bool t___16566 = C::Mapped.ContainsKey(cs__1092.Changes, "name");
                string fn__16549()
                {
                    return "name should remain";
                }
                test___60.Assert(t___16566, (S1::Func<string>) fn__16549);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeOnNonexistentFieldIsNoOp__2301()
        {
            T::Test test___61 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1094 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16537 = userTable__704();
                ISafeIdentifier t___16538 = csid__703("name");
                IChangeset cs__1095 = S0::SrcGlobal.Changeset(t___16537, params__1094).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16538)).DeleteChange(csid__703("email"));
                bool t___16543 = C::Mapped.ContainsKey(cs__1095.Changes, "name");
                string fn__16534()
                {
                    return "name should still be present";
                }
                test___61.Assert(t___16543, (S1::Func<string>) fn__16534);
                bool t___16546 = cs__1095.IsValid;
                string fn__16533()
                {
                    return "should still be valid";
                }
                test___61.Assert(t___16546, (S1::Func<string>) fn__16533);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionPassesWhenValueInList__2302()
        {
            T::Test test___62 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1097 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "admin")));
                TableDef t___16525 = userTable__704();
                ISafeIdentifier t___16526 = csid__703("name");
                IChangeset cs__1098 = S0::SrcGlobal.Changeset(t___16525, params__1097).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16526)).ValidateInclusion(csid__703("name"), C::Listed.CreateReadOnlyList<string>("admin", "user", "guest"));
                bool t___16530 = cs__1098.IsValid;
                string fn__16522()
                {
                    return "should be valid";
                }
                test___62.Assert(t___16530, (S1::Func<string>) fn__16522);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionFailsWhenValueNotInList__2303()
        {
            T::Test test___63 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1100 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "hacker")));
                TableDef t___16507 = userTable__704();
                ISafeIdentifier t___16508 = csid__703("name");
                IChangeset cs__1101 = S0::SrcGlobal.Changeset(t___16507, params__1100).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16508)).ValidateInclusion(csid__703("name"), C::Listed.CreateReadOnlyList<string>("admin", "user", "guest"));
                bool t___16514 = !cs__1101.IsValid;
                string fn__16504()
                {
                    return "should be invalid";
                }
                test___63.Assert(t___16514, (S1::Func<string>) fn__16504);
                bool t___16520 = cs__1101.Errors[0].Field == "name";
                string fn__16503()
                {
                    return "error on name";
                }
                test___63.Assert(t___16520, (S1::Func<string>) fn__16503);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInclusionSkipsWhenFieldNotInChanges__2304()
        {
            T::Test test___64 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1103 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16495 = userTable__704();
                ISafeIdentifier t___16496 = csid__703("name");
                IChangeset cs__1104 = S0::SrcGlobal.Changeset(t___16495, params__1103).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16496)).ValidateInclusion(csid__703("name"), C::Listed.CreateReadOnlyList<string>("admin", "user"));
                bool t___16500 = cs__1104.IsValid;
                string fn__16493()
                {
                    return "should be valid when field absent";
                }
                test___64.Assert(t___16500, (S1::Func<string>) fn__16493);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionPassesWhenValueNotInList__2305()
        {
            T::Test test___65 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1106 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___16485 = userTable__704();
                ISafeIdentifier t___16486 = csid__703("name");
                IChangeset cs__1107 = S0::SrcGlobal.Changeset(t___16485, params__1106).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16486)).ValidateExclusion(csid__703("name"), C::Listed.CreateReadOnlyList<string>("root", "admin", "superuser"));
                bool t___16490 = cs__1107.IsValid;
                string fn__16482()
                {
                    return "should be valid";
                }
                test___65.Assert(t___16490, (S1::Func<string>) fn__16482);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionFailsWhenValueInList__2306()
        {
            T::Test test___66 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1109 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "admin")));
                TableDef t___16467 = userTable__704();
                ISafeIdentifier t___16468 = csid__703("name");
                IChangeset cs__1110 = S0::SrcGlobal.Changeset(t___16467, params__1109).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16468)).ValidateExclusion(csid__703("name"), C::Listed.CreateReadOnlyList<string>("root", "admin", "superuser"));
                bool t___16474 = !cs__1110.IsValid;
                string fn__16464()
                {
                    return "should be invalid";
                }
                test___66.Assert(t___16474, (S1::Func<string>) fn__16464);
                bool t___16480 = cs__1110.Errors[0].Field == "name";
                string fn__16463()
                {
                    return "error on name";
                }
                test___66.Assert(t___16480, (S1::Func<string>) fn__16463);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateExclusionSkipsWhenFieldNotInChanges__2307()
        {
            T::Test test___67 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1112 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16455 = userTable__704();
                ISafeIdentifier t___16456 = csid__703("name");
                IChangeset cs__1113 = S0::SrcGlobal.Changeset(t___16455, params__1112).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16456)).ValidateExclusion(csid__703("name"), C::Listed.CreateReadOnlyList<string>("root", "admin"));
                bool t___16460 = cs__1113.IsValid;
                string fn__16453()
                {
                    return "should be valid when field absent";
                }
                test___67.Assert(t___16460, (S1::Func<string>) fn__16453);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanPasses__2308()
        {
            T::Test test___68 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1115 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___16444 = userTable__704();
                ISafeIdentifier t___16445 = csid__703("age");
                IChangeset cs__1116 = S0::SrcGlobal.Changeset(t___16444, params__1115).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16445)).ValidateNumber(csid__703("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___16450 = cs__1116.IsValid;
                string fn__16441()
                {
                    return "25 > 18 should pass";
                }
                test___68.Assert(t___16450, (S1::Func<string>) fn__16441);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanFails__2309()
        {
            T::Test test___69 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1118 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "15")));
                TableDef t___16431 = userTable__704();
                ISafeIdentifier t___16432 = csid__703("age");
                IChangeset cs__1119 = S0::SrcGlobal.Changeset(t___16431, params__1118).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16432)).ValidateNumber(csid__703("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___16439 = !cs__1119.IsValid;
                string fn__16428()
                {
                    return "15 > 18 should fail";
                }
                test___69.Assert(t___16439, (S1::Func<string>) fn__16428);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanPasses__2310()
        {
            T::Test test___70 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1121 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "8.5")));
                TableDef t___16419 = userTable__704();
                ISafeIdentifier t___16420 = csid__703("score");
                IChangeset cs__1122 = S0::SrcGlobal.Changeset(t___16419, params__1121).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16420)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___16425 = cs__1122.IsValid;
                string fn__16416()
                {
                    return "8.5 < 10 should pass";
                }
                test___70.Assert(t___16425, (S1::Func<string>) fn__16416);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanFails__2311()
        {
            T::Test test___71 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1124 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "12.0")));
                TableDef t___16406 = userTable__704();
                ISafeIdentifier t___16407 = csid__703("score");
                IChangeset cs__1125 = S0::SrcGlobal.Changeset(t___16406, params__1124).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16407)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___16414 = !cs__1125.IsValid;
                string fn__16403()
                {
                    return "12 < 10 should fail";
                }
                test___71.Assert(t___16414, (S1::Func<string>) fn__16403);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanOrEqualBoundary__2312()
        {
            T::Test test___72 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1127 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "18")));
                TableDef t___16394 = userTable__704();
                ISafeIdentifier t___16395 = csid__703("age");
                IChangeset cs__1128 = S0::SrcGlobal.Changeset(t___16394, params__1127).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16395)).ValidateNumber(csid__703("age"), new NumberValidationOpts(null, null, 18.0, null, null));
                bool t___16400 = cs__1128.IsValid;
                string fn__16391()
                {
                    return "18 >= 18 should pass";
                }
                test___72.Assert(t___16400, (S1::Func<string>) fn__16391);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberCombinedOptions__2313()
        {
            T::Test test___73 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1130 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "5.0")));
                TableDef t___16382 = userTable__704();
                ISafeIdentifier t___16383 = csid__703("score");
                IChangeset cs__1131 = S0::SrcGlobal.Changeset(t___16382, params__1130).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16383)).ValidateNumber(csid__703("score"), new NumberValidationOpts(0.0, 10.0, null, null, null));
                bool t___16388 = cs__1131.IsValid;
                string fn__16379()
                {
                    return "5 > 0 and < 10 should pass";
                }
                test___73.Assert(t___16388, (S1::Func<string>) fn__16379);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberNonNumericValue__2314()
        {
            T::Test test___74 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1133 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "abc")));
                TableDef t___16363 = userTable__704();
                ISafeIdentifier t___16364 = csid__703("age");
                IChangeset cs__1134 = S0::SrcGlobal.Changeset(t___16363, params__1133).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16364)).ValidateNumber(csid__703("age"), new NumberValidationOpts(0.0, null, null, null, null));
                bool t___16371 = !cs__1134.IsValid;
                string fn__16360()
                {
                    return "non-numeric should fail";
                }
                test___74.Assert(t___16371, (S1::Func<string>) fn__16360);
                bool t___16377 = cs__1134.Errors[0].Message == "must be a number";
                string fn__16359()
                {
                    return "correct error message";
                }
                test___74.Assert(t___16377, (S1::Func<string>) fn__16359);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberSkipsWhenFieldNotInChanges__2315()
        {
            T::Test test___75 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1136 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16350 = userTable__704();
                ISafeIdentifier t___16351 = csid__703("age");
                IChangeset cs__1137 = S0::SrcGlobal.Changeset(t___16350, params__1136).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16351)).ValidateNumber(csid__703("age"), new NumberValidationOpts(0.0, null, null, null, null));
                bool t___16356 = cs__1137.IsValid;
                string fn__16348()
                {
                    return "should be valid when field absent";
                }
                test___75.Assert(t___16356, (S1::Func<string>) fn__16348);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptancePassesForTrueValues__2316()
        {
            T::Test test___76 = new T::Test();
            try
            {
                void fn__16345(string v__1139)
                {
                    G::IReadOnlyDictionary<string, string> params__1140 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__1139)));
                    TableDef t___16337 = userTable__704();
                    ISafeIdentifier t___16338 = csid__703("active");
                    IChangeset cs__1141 = S0::SrcGlobal.Changeset(t___16337, params__1140).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16338)).ValidateAcceptance(csid__703("active"));
                    bool t___16342 = cs__1141.IsValid;
                    string fn__16334()
                    {
                        return "should accept: " + v__1139;
                    }
                    test___76.Assert(t___16342, (S1::Func<string>) fn__16334);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__16345);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptanceFailsForNonTrueValues__2317()
        {
            T::Test test___77 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1143 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", "false")));
                TableDef t___16319 = userTable__704();
                ISafeIdentifier t___16320 = csid__703("active");
                IChangeset cs__1144 = S0::SrcGlobal.Changeset(t___16319, params__1143).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16320)).ValidateAcceptance(csid__703("active"));
                bool t___16326 = !cs__1144.IsValid;
                string fn__16316()
                {
                    return "false should not be accepted";
                }
                test___77.Assert(t___16326, (S1::Func<string>) fn__16316);
                bool t___16332 = cs__1144.Errors[0].Message == "must be accepted";
                string fn__16315()
                {
                    return "correct message";
                }
                test___77.Assert(t___16332, (S1::Func<string>) fn__16315);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationPassesWhenFieldsMatch__2318()
        {
            T::Test test___78 = new T::Test();
            try
            {
                TableDef tbl__1146 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("password"), new StringField(), false, null, false), new FieldDef(csid__703("password_confirmation"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1147 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123"), new G::KeyValuePair<string, string>("password_confirmation", "secret123")));
                ISafeIdentifier t___16306 = csid__703("password");
                ISafeIdentifier t___16307 = csid__703("password_confirmation");
                IChangeset cs__1148 = S0::SrcGlobal.Changeset(tbl__1146, params__1147).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16306, t___16307)).ValidateConfirmation(csid__703("password"), csid__703("password_confirmation"));
                bool t___16312 = cs__1148.IsValid;
                string fn__16294()
                {
                    return "matching fields should pass";
                }
                test___78.Assert(t___16312, (S1::Func<string>) fn__16294);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationFailsWhenFieldsDiffer__2319()
        {
            T::Test test___79 = new T::Test();
            try
            {
                TableDef tbl__1150 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("password"), new StringField(), false, null, false), new FieldDef(csid__703("password_confirmation"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1151 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123"), new G::KeyValuePair<string, string>("password_confirmation", "wrong456")));
                ISafeIdentifier t___16278 = csid__703("password");
                ISafeIdentifier t___16279 = csid__703("password_confirmation");
                IChangeset cs__1152 = S0::SrcGlobal.Changeset(tbl__1150, params__1151).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16278, t___16279)).ValidateConfirmation(csid__703("password"), csid__703("password_confirmation"));
                bool t___16286 = !cs__1152.IsValid;
                string fn__16266()
                {
                    return "mismatched fields should fail";
                }
                test___79.Assert(t___16286, (S1::Func<string>) fn__16266);
                bool t___16292 = cs__1152.Errors[0].Field == "password_confirmation";
                string fn__16265()
                {
                    return "error on confirmation field";
                }
                test___79.Assert(t___16292, (S1::Func<string>) fn__16265);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateConfirmationFailsWhenConfirmationMissing__2320()
        {
            T::Test test___80 = new T::Test();
            try
            {
                TableDef tbl__1154 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("password"), new StringField(), false, null, false), new FieldDef(csid__703("password_confirmation"), new StringField(), true, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1155 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("password", "secret123")));
                ISafeIdentifier t___16256 = csid__703("password");
                IChangeset cs__1156 = S0::SrcGlobal.Changeset(tbl__1154, params__1155).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16256)).ValidateConfirmation(csid__703("password"), csid__703("password_confirmation"));
                bool t___16263 = !cs__1156.IsValid;
                string fn__16245()
                {
                    return "missing confirmation should fail";
                }
                test___80.Assert(t___16263, (S1::Func<string>) fn__16245);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsPassesWhenSubstringFound__2321()
        {
            T::Test test___81 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1158 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___16237 = userTable__704();
                ISafeIdentifier t___16238 = csid__703("email");
                IChangeset cs__1159 = S0::SrcGlobal.Changeset(t___16237, params__1158).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16238)).ValidateContains(csid__703("email"), "@");
                bool t___16242 = cs__1159.IsValid;
                string fn__16234()
                {
                    return "should pass when @ present";
                }
                test___81.Assert(t___16242, (S1::Func<string>) fn__16234);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsFailsWhenSubstringNotFound__2322()
        {
            T::Test test___82 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1161 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice-example.com")));
                TableDef t___16225 = userTable__704();
                ISafeIdentifier t___16226 = csid__703("email");
                IChangeset cs__1162 = S0::SrcGlobal.Changeset(t___16225, params__1161).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16226)).ValidateContains(csid__703("email"), "@");
                bool t___16232 = !cs__1162.IsValid;
                string fn__16222()
                {
                    return "should fail when @ absent";
                }
                test___82.Assert(t___16232, (S1::Func<string>) fn__16222);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateContainsSkipsWhenFieldNotInChanges__2323()
        {
            T::Test test___83 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1164 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___16214 = userTable__704();
                ISafeIdentifier t___16215 = csid__703("email");
                IChangeset cs__1165 = S0::SrcGlobal.Changeset(t___16214, params__1164).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16215)).ValidateContains(csid__703("email"), "@");
                bool t___16219 = cs__1165.IsValid;
                string fn__16212()
                {
                    return "should be valid when field absent";
                }
                test___83.Assert(t___16219, (S1::Func<string>) fn__16212);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithPasses__2324()
        {
            T::Test test___84 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1167 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Dr. Smith")));
                TableDef t___16204 = userTable__704();
                ISafeIdentifier t___16205 = csid__703("name");
                IChangeset cs__1168 = S0::SrcGlobal.Changeset(t___16204, params__1167).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16205)).ValidateStartsWith(csid__703("name"), "Dr.");
                bool t___16209 = cs__1168.IsValid;
                string fn__16201()
                {
                    return "should pass for Dr. prefix";
                }
                test___84.Assert(t___16209, (S1::Func<string>) fn__16201);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithFails__2325()
        {
            T::Test test___85 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1170 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Mr. Smith")));
                TableDef t___16192 = userTable__704();
                ISafeIdentifier t___16193 = csid__703("name");
                IChangeset cs__1171 = S0::SrcGlobal.Changeset(t___16192, params__1170).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16193)).ValidateStartsWith(csid__703("name"), "Dr.");
                bool t___16199 = !cs__1171.IsValid;
                string fn__16189()
                {
                    return "should fail for Mr. prefix";
                }
                test___85.Assert(t___16199, (S1::Func<string>) fn__16189);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithPasses__2326()
        {
            T::Test test___86 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1173 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___16181 = userTable__704();
                ISafeIdentifier t___16182 = csid__703("email");
                IChangeset cs__1174 = S0::SrcGlobal.Changeset(t___16181, params__1173).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16182)).ValidateEndsWith(csid__703("email"), ".com");
                bool t___16186 = cs__1174.IsValid;
                string fn__16178()
                {
                    return "should pass for .com suffix";
                }
                test___86.Assert(t___16186, (S1::Func<string>) fn__16178);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithFails__2327()
        {
            T::Test test___87 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1176 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("email", "alice@example.org")));
                TableDef t___16169 = userTable__704();
                ISafeIdentifier t___16170 = csid__703("email");
                IChangeset cs__1177 = S0::SrcGlobal.Changeset(t___16169, params__1176).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16170)).ValidateEndsWith(csid__703("email"), ".com");
                bool t___16176 = !cs__1177.IsValid;
                string fn__16166()
                {
                    return "should fail for .org when expecting .com";
                }
                test___87.Assert(t___16176, (S1::Func<string>) fn__16166);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithHandlesRepeatedSuffixCorrectly__2328()
        {
            T::Test test___88 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1179 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "abcabc")));
                TableDef t___16158 = userTable__704();
                ISafeIdentifier t___16159 = csid__703("name");
                IChangeset cs__1180 = S0::SrcGlobal.Changeset(t___16158, params__1179).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16159)).ValidateEndsWith(csid__703("name"), "abc");
                bool t___16163 = cs__1180.IsValid;
                string fn__16155()
                {
                    return "abcabc should end with abc";
                }
                test___88.Assert(t___16163, (S1::Func<string>) fn__16155);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlUsesDefaultValueWhenFieldNotInChanges__2329()
        {
            T::Test test___89 = new T::Test();
            try
            {
                TableDef tbl__1182 = new TableDef(csid__703("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("title"), new StringField(), false, null, false), new FieldDef(csid__703("status"), new StringField(), false, new SqlDefault(), false)), null);
                G::IReadOnlyDictionary<string, string> params__1183 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "Hello")));
                ISafeIdentifier t___16139 = csid__703("title");
                IChangeset cs__1184 = S0::SrcGlobal.Changeset(tbl__1182, params__1183).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16139));
                SqlFragment t___8735;
                t___8735 = cs__1184.ToInsertSql();
                SqlFragment t___8736 = t___8735;
                string s__1185 = t___8736.ToString();
                bool t___16143 = s__1185.IndexOf("INSERT INTO posts") >= 0;
                string fn__16127()
                {
                    return "has INSERT INTO: " + s__1185;
                }
                test___89.Assert(t___16143, (S1::Func<string>) fn__16127);
                bool t___16147 = s__1185.IndexOf("'Hello'") >= 0;
                string fn__16126()
                {
                    return "has title value: " + s__1185;
                }
                test___89.Assert(t___16147, (S1::Func<string>) fn__16126);
                bool t___16151 = s__1185.IndexOf("DEFAULT") >= 0;
                string fn__16125()
                {
                    return "status should use DEFAULT: " + s__1185;
                }
                test___89.Assert(t___16151, (S1::Func<string>) fn__16125);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlChangeOverridesDefaultValue__2330()
        {
            T::Test test___90 = new T::Test();
            try
            {
                TableDef tbl__1187 = new TableDef(csid__703("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("title"), new StringField(), false, null, false), new FieldDef(csid__703("status"), new StringField(), false, new SqlDefault(), false)), null);
                G::IReadOnlyDictionary<string, string> params__1188 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "Hello"), new G::KeyValuePair<string, string>("status", "published")));
                ISafeIdentifier t___16117 = csid__703("title");
                ISafeIdentifier t___16118 = csid__703("status");
                IChangeset cs__1189 = S0::SrcGlobal.Changeset(tbl__1187, params__1188).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16117, t___16118));
                SqlFragment t___8715;
                t___8715 = cs__1189.ToInsertSql();
                SqlFragment t___8716 = t___8715;
                string s__1190 = t___8716.ToString();
                bool t___16122 = s__1190.IndexOf("'published'") >= 0;
                string fn__16104()
                {
                    return "should use provided value: " + s__1190;
                }
                test___90.Assert(t___16122, (S1::Func<string>) fn__16104);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlWithTimestampsUsesDefault__2331()
        {
            T::Test test___91 = new T::Test();
            try
            {
                G::IReadOnlyList<FieldDef> t___8662;
                t___8662 = S0::SrcGlobal.Timestamps();
                G::IReadOnlyList<FieldDef> ts__1192 = t___8662;
                G::IList<FieldDef> fields__1193 = new G::List<FieldDef>();
                C::Listed.Add(fields__1193, new FieldDef(csid__703("title"), new StringField(), false, null, false));
                void fn__16070(FieldDef t__1194)
                {
                    C::Listed.Add(fields__1193, t__1194);
                }
                C::Listed.ForEach(ts__1192, (S1::Action<FieldDef>) fn__16070);
                TableDef tbl__1195 = new TableDef(csid__703("articles"), C::Listed.ToReadOnlyList(fields__1193), null);
                G::IReadOnlyDictionary<string, string> params__1196 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "News")));
                ISafeIdentifier t___16083 = csid__703("title");
                IChangeset cs__1197 = S0::SrcGlobal.Changeset(tbl__1195, params__1196).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16083));
                SqlFragment t___8677;
                t___8677 = cs__1197.ToInsertSql();
                SqlFragment t___8678 = t___8677;
                string s__1198 = t___8678.ToString();
                bool t___16087 = s__1198.IndexOf("inserted_at") >= 0;
                string fn__16069()
                {
                    return "should include inserted_at: " + s__1198;
                }
                test___91.Assert(t___16087, (S1::Func<string>) fn__16069);
                bool t___16091 = s__1198.IndexOf("updated_at") >= 0;
                string fn__16068()
                {
                    return "should include updated_at: " + s__1198;
                }
                test___91.Assert(t___16091, (S1::Func<string>) fn__16068);
                bool t___16095 = s__1198.IndexOf("DEFAULT") >= 0;
                string fn__16067()
                {
                    return "timestamps should use DEFAULT: " + s__1198;
                }
                test___91.Assert(t___16095, (S1::Func<string>) fn__16067);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlSkipsVirtualFields__2332()
        {
            T::Test test___92 = new T::Test();
            try
            {
                TableDef tbl__1200 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("full_name"), new StringField(), true, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1201 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("full_name", "Alice Smith")));
                ISafeIdentifier t___16053 = csid__703("name");
                ISafeIdentifier t___16054 = csid__703("full_name");
                IChangeset cs__1202 = S0::SrcGlobal.Changeset(tbl__1200, params__1201).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16053, t___16054));
                SqlFragment t___8651;
                t___8651 = cs__1202.ToInsertSql();
                SqlFragment t___8652 = t___8651;
                string s__1203 = t___8652.ToString();
                bool t___16058 = s__1203.IndexOf("'Alice'") >= 0;
                string fn__16041()
                {
                    return "name should be included: " + s__1203;
                }
                test___92.Assert(t___16058, (S1::Func<string>) fn__16041);
                bool t___16064 = !(s__1203.IndexOf("full_name") >= 0);
                string fn__16040()
                {
                    return "virtual field should be excluded: " + s__1203;
                }
                test___92.Assert(t___16064, (S1::Func<string>) fn__16040);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlAllowsMissingNonNullableVirtualField__2333()
        {
            T::Test test___93 = new T::Test();
            try
            {
                TableDef tbl__1205 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("computed"), new StringField(), false, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1206 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                ISafeIdentifier t___16033 = csid__703("name");
                IChangeset cs__1207 = S0::SrcGlobal.Changeset(tbl__1205, params__1206).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16033));
                SqlFragment t___8630;
                t___8630 = cs__1207.ToInsertSql();
                SqlFragment t___8631 = t___8630;
                string s__1208 = t___8631.ToString();
                bool t___16037 = s__1208.IndexOf("'Alice'") >= 0;
                string fn__16022()
                {
                    return "should succeed: " + s__1208;
                }
                test___93.Assert(t___16037, (S1::Func<string>) fn__16022);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlSkipsVirtualFields__2334()
        {
            T::Test test___94 = new T::Test();
            try
            {
                TableDef tbl__1210 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("display"), new StringField(), true, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1211 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("display", "Bobby")));
                ISafeIdentifier t___16009 = csid__703("name");
                ISafeIdentifier t___16010 = csid__703("display");
                IChangeset cs__1212 = S0::SrcGlobal.Changeset(tbl__1210, params__1211).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___16009, t___16010));
                SqlFragment t___8607;
                t___8607 = cs__1212.ToUpdateSql(1);
                SqlFragment t___8608 = t___8607;
                string s__1213 = t___8608.ToString();
                bool t___16014 = s__1213.IndexOf("name = 'Bob'") >= 0;
                string fn__15997()
                {
                    return "name should be in SET: " + s__1213;
                }
                test___94.Assert(t___16014, (S1::Func<string>) fn__15997);
                bool t___16020 = !(s__1213.IndexOf("display") >= 0);
                string fn__15996()
                {
                    return "virtual field excluded from UPDATE: " + s__1213;
                }
                test___94.Assert(t___16020, (S1::Func<string>) fn__15996);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlUsesCustomPrimaryKey__2335()
        {
            T::Test test___95 = new T::Test();
            try
            {
                TableDef tbl__1215 = new TableDef(csid__703("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("title"), new StringField(), false, null, false)), csid__703("post_id"));
                G::IReadOnlyDictionary<string, string> params__1216 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("title", "Updated")));
                ISafeIdentifier t___15990 = csid__703("title");
                IChangeset cs__1217 = S0::SrcGlobal.Changeset(tbl__1215, params__1216).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15990));
                SqlFragment t___8589;
                t___8589 = cs__1217.ToUpdateSql(99);
                SqlFragment t___8590 = t___8589;
                string s__1218 = t___8590.ToString();
                bool t___15994 = s__1218 == "UPDATE posts SET title = 'Updated' WHERE post_id = 99";
                string fn__15980()
                {
                    return "got: " + s__1218;
                }
                test___95.Assert(t___15994, (S1::Func<string>) fn__15980);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteSqlUsesCustomPrimaryKey__2336()
        {
            T::Test test___96 = new T::Test();
            try
            {
                TableDef tbl__1220 = new TableDef(csid__703("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("title"), new StringField(), false, null, false)), csid__703("post_id"));
                string s__1221 = S0::SrcGlobal.DeleteSql(tbl__1220, 42).ToString();
                bool t___15967 = s__1221 == "DELETE FROM posts WHERE post_id = 42";
                string fn__15956()
                {
                    return "got: " + s__1221;
                }
                test___96.Assert(t___15967, (S1::Func<string>) fn__15956);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteSqlUsesDefaultIdWhenPrimaryKeyNull__2337()
        {
            T::Test test___97 = new T::Test();
            try
            {
                TableDef tbl__1223 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false)), null);
                string s__1224 = S0::SrcGlobal.DeleteSql(tbl__1223, 7).ToString();
                bool t___15954 = s__1224 == "DELETE FROM users WHERE id = 7";
                string fn__15945()
                {
                    return "got: " + s__1224;
                }
                test___97.Assert(t___15954, (S1::Func<string>) fn__15945);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void alreadyInvalidChangesetSkipsSubsequentValidators__2338()
        {
            T::Test test___98 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1226 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___15919 = userTable__704();
                ISafeIdentifier t___15920 = csid__703("name");
                ISafeIdentifier t___15921 = csid__703("email");
                IChangeset cs__1227 = S0::SrcGlobal.Changeset(t___15919, params__1226).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15920, t___15921)).ValidateLength(csid__703("name"), 3, 50).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name"), csid__703("email"))).ValidateContains(csid__703("email"), "@");
                bool t___15932 = !cs__1227.IsValid;
                string fn__15915()
                {
                    return "should be invalid from validateLength";
                }
                test___98.Assert(t___15932, (S1::Func<string>) fn__15915);
                bool t___15937 = cs__1227.Errors.Count == 1;
                string fn__15914()
                {
                    return "should have exactly 1 error, not accumulate: " + S1::Convert.ToString(cs__1227.Errors.Count);
                }
                test___98.Assert(t___15937, (S1::Func<string>) fn__15914);
                bool t___15943 = cs__1227.Errors[0].Field == "name";
                string fn__15913()
                {
                    return "error should be on name";
                }
                test___98.Assert(t___15943, (S1::Func<string>) fn__15913);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanOrEqualPassesAtBoundary__2339()
        {
            T::Test test___99 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1229 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "10.0")));
                TableDef t___15901 = userTable__704();
                ISafeIdentifier t___15902 = csid__703("score");
                IChangeset cs__1230 = S0::SrcGlobal.Changeset(t___15901, params__1229).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15902)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, null, null, 10.0, null));
                bool t___15907 = cs__1230.IsValid;
                string fn__15898()
                {
                    return "10.0 <= 10.0 should pass";
                }
                test___99.Assert(t___15907, (S1::Func<string>) fn__15898);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanOrEqualFailsAboveBoundary__2340()
        {
            T::Test test___100 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1232 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "10.1")));
                TableDef t___15882 = userTable__704();
                ISafeIdentifier t___15883 = csid__703("score");
                IChangeset cs__1233 = S0::SrcGlobal.Changeset(t___15882, params__1232).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15883)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, null, null, 10.0, null));
                bool t___15890 = !cs__1233.IsValid;
                string fn__15879()
                {
                    return "10.1 <= 10.0 should fail";
                }
                test___100.Assert(t___15890, (S1::Func<string>) fn__15879);
                bool t___15896 = cs__1233.Errors[0].Message == "must be less than or equal to 10.0";
                string fn__15878()
                {
                    return "correct message";
                }
                test___100.Assert(t___15896, (S1::Func<string>) fn__15878);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberEqualToPassesWhenEqual__2341()
        {
            T::Test test___101 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1235 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "42.0")));
                TableDef t___15869 = userTable__704();
                ISafeIdentifier t___15870 = csid__703("score");
                IChangeset cs__1236 = S0::SrcGlobal.Changeset(t___15869, params__1235).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15870)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, null, null, null, 42.0));
                bool t___15875 = cs__1236.IsValid;
                string fn__15866()
                {
                    return "42.0 == 42.0 should pass";
                }
                test___101.Assert(t___15875, (S1::Func<string>) fn__15866);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberEqualToFailsWhenNotEqual__2342()
        {
            T::Test test___102 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1238 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "41.9")));
                TableDef t___15850 = userTable__704();
                ISafeIdentifier t___15851 = csid__703("score");
                IChangeset cs__1239 = S0::SrcGlobal.Changeset(t___15850, params__1238).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15851)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, null, null, null, 42.0));
                bool t___15858 = !cs__1239.IsValid;
                string fn__15847()
                {
                    return "41.9 == 42.0 should fail";
                }
                test___102.Assert(t___15858, (S1::Func<string>) fn__15847);
                bool t___15864 = cs__1239.Errors[0].Message == "must be equal to 42.0";
                string fn__15846()
                {
                    return "correct message";
                }
                test___102.Assert(t___15864, (S1::Func<string>) fn__15846);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberGreaterThanFailsAtExactThreshold__2343()
        {
            T::Test test___103 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1241 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "18")));
                TableDef t___15836 = userTable__704();
                ISafeIdentifier t___15837 = csid__703("age");
                IChangeset cs__1242 = S0::SrcGlobal.Changeset(t___15836, params__1241).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15837)).ValidateNumber(csid__703("age"), new NumberValidationOpts(18.0, null, null, null, null));
                bool t___15844 = !cs__1242.IsValid;
                string fn__15833()
                {
                    return "18 > 18 should fail (strict greater than)";
                }
                test___103.Assert(t___15844, (S1::Func<string>) fn__15833);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateNumberLessThanFailsAtExactThreshold__2344()
        {
            T::Test test___104 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1244 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "10.0")));
                TableDef t___15823 = userTable__704();
                ISafeIdentifier t___15824 = csid__703("score");
                IChangeset cs__1245 = S0::SrcGlobal.Changeset(t___15823, params__1244).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15824)).ValidateNumber(csid__703("score"), new NumberValidationOpts(null, 10.0, null, null, null));
                bool t___15831 = !cs__1245.IsValid;
                string fn__15820()
                {
                    return "10.0 < 10.0 should fail (strict less than)";
                }
                test___104.Assert(t___15831, (S1::Func<string>) fn__15820);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatFailsForNonFloatString__2345()
        {
            T::Test test___105 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1247 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "abc")));
                TableDef t___15805 = userTable__704();
                ISafeIdentifier t___15806 = csid__703("score");
                IChangeset cs__1248 = S0::SrcGlobal.Changeset(t___15805, params__1247).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15806)).ValidateFloat(csid__703("score"));
                bool t___15812 = !cs__1248.IsValid;
                string fn__15802()
                {
                    return "abc should not parse as float";
                }
                test___105.Assert(t___15812, (S1::Func<string>) fn__15802);
                bool t___15818 = cs__1248.Errors[0].Message == "must be a number";
                string fn__15801()
                {
                    return "correct message";
                }
                test___105.Assert(t___15818, (S1::Func<string>) fn__15801);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlWithAllSixFieldTypes__2346()
        {
            T::Test test___106 = new T::Test();
            try
            {
                TableDef tbl__1250 = new TableDef(csid__703("records"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("count"), new IntField(), false, null, false), new FieldDef(csid__703("big_id"), new Int64_Field(), false, null, false), new FieldDef(csid__703("rating"), new FloatField(), false, null, false), new FieldDef(csid__703("active"), new BoolField(), false, null, false), new FieldDef(csid__703("birthday"), new DateField(), false, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1251 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("count", "42"), new G::KeyValuePair<string, string>("big_id", "9999999999"), new G::KeyValuePair<string, string>("rating", "3.14"), new G::KeyValuePair<string, string>("active", "true"), new G::KeyValuePair<string, string>("birthday", "2000-01-15")));
                ISafeIdentifier t___15769 = csid__703("name");
                ISafeIdentifier t___15770 = csid__703("count");
                ISafeIdentifier t___15771 = csid__703("big_id");
                ISafeIdentifier t___15772 = csid__703("rating");
                ISafeIdentifier t___15773 = csid__703("active");
                ISafeIdentifier t___15774 = csid__703("birthday");
                IChangeset cs__1252 = S0::SrcGlobal.Changeset(tbl__1250, params__1251).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15769, t___15770, t___15771, t___15772, t___15773, t___15774));
                SqlFragment t___8416;
                t___8416 = cs__1252.ToInsertSql();
                SqlFragment t___8417 = t___8416;
                string s__1253 = t___8417.ToString();
                bool t___15778 = s__1253.IndexOf("'Alice'") >= 0;
                string fn__15741()
                {
                    return "string field: " + s__1253;
                }
                test___106.Assert(t___15778, (S1::Func<string>) fn__15741);
                bool t___15782 = s__1253.IndexOf("42") >= 0;
                string fn__15740()
                {
                    return "int field: " + s__1253;
                }
                test___106.Assert(t___15782, (S1::Func<string>) fn__15740);
                bool t___15786 = s__1253.IndexOf("9999999999") >= 0;
                string fn__15739()
                {
                    return "int64 field: " + s__1253;
                }
                test___106.Assert(t___15786, (S1::Func<string>) fn__15739);
                bool t___15790 = s__1253.IndexOf("3.14") >= 0;
                string fn__15738()
                {
                    return "float field: " + s__1253;
                }
                test___106.Assert(t___15790, (S1::Func<string>) fn__15738);
                bool t___15794 = s__1253.IndexOf("TRUE") >= 0;
                string fn__15737()
                {
                    return "bool field: " + s__1253;
                }
                test___106.Assert(t___15794, (S1::Func<string>) fn__15737);
                bool t___15798 = s__1253.IndexOf("'2000-01-15'") >= 0;
                string fn__15736()
                {
                    return "date field: " + s__1253;
                }
                test___106.Assert(t___15798, (S1::Func<string>) fn__15736);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteChangeOnNonNullableFieldCausesToInsertSqlToBubble__2347()
        {
            T::Test test___107 = new T::Test();
            try
            {
                TableDef tbl__1255 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("email"), new StringField(), false, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1256 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@b.com")));
                ISafeIdentifier t___15729 = csid__703("name");
                ISafeIdentifier t___15730 = csid__703("email");
                IChangeset cs__1257 = S0::SrcGlobal.Changeset(tbl__1255, params__1256).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15729, t___15730)).DeleteChange(csid__703("email"));
                bool didBubble__1258;
                try
                {
                    cs__1257.ToInsertSql();
                    didBubble__1258 = false;
                }
                catch
                {
                    didBubble__1258 = true;
                }
                string fn__15717()
                {
                    return "removing non-nullable field should make toInsertSql bubble";
                }
                test___107.Assert(didBubble__1258, (S1::Func<string>) fn__15717);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesAtExactMin__2348()
        {
            T::Test test___108 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1260 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "abc")));
                TableDef t___15709 = userTable__704();
                ISafeIdentifier t___15710 = csid__703("name");
                IChangeset cs__1261 = S0::SrcGlobal.Changeset(t___15709, params__1260).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15710)).ValidateLength(csid__703("name"), 3, 10);
                bool t___15714 = cs__1261.IsValid;
                string fn__15706()
                {
                    return "length 3 should pass for min 3";
                }
                test___108.Assert(t___15714, (S1::Func<string>) fn__15706);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesAtExactMax__2349()
        {
            T::Test test___109 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1263 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "abcdefghij")));
                TableDef t___15698 = userTable__704();
                ISafeIdentifier t___15699 = csid__703("name");
                IChangeset cs__1264 = S0::SrcGlobal.Changeset(t___15698, params__1263).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15699)).ValidateLength(csid__703("name"), 1, 10);
                bool t___15703 = cs__1264.IsValid;
                string fn__15695()
                {
                    return "length 10 should pass for max 10";
                }
                test___109.Assert(t___15703, (S1::Func<string>) fn__15695);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateAcceptanceSkipsWhenFieldNotInChanges__2350()
        {
            T::Test test___110 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1266 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___15687 = userTable__704();
                ISafeIdentifier t___15688 = csid__703("active");
                IChangeset cs__1267 = S0::SrcGlobal.Changeset(t___15687, params__1266).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15688)).ValidateAcceptance(csid__703("active"));
                bool t___15692 = cs__1267.IsValid;
                string fn__15685()
                {
                    return "should be valid when field absent";
                }
                test___110.Assert(t___15692, (S1::Func<string>) fn__15685);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleValidatorsChainCorrectlyOnValidChangeset__2351()
        {
            T::Test test___111 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1269 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___15660 = userTable__704();
                ISafeIdentifier t___15661 = csid__703("name");
                ISafeIdentifier t___15662 = csid__703("email");
                ISafeIdentifier t___15663 = csid__703("age");
                IChangeset cs__1270 = S0::SrcGlobal.Changeset(t___15660, params__1269).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15661, t___15662, t___15663)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name"), csid__703("email"))).ValidateLength(csid__703("name"), 2, 50).ValidateContains(csid__703("email"), "@").ValidateInt(csid__703("age")).ValidateNumber(csid__703("age"), new NumberValidationOpts(0.0, 150.0, null, null, null));
                bool t___15677 = cs__1270.IsValid;
                string fn__15655()
                {
                    return "all validators should pass";
                }
                test___111.Assert(t___15677, (S1::Func<string>) fn__15655);
                bool t___15683 = cs__1270.Errors.Count == 0;
                string fn__15654()
                {
                    return "no errors expected";
                }
                test___111.Assert(t___15683, (S1::Func<string>) fn__15654);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlWithMultipleNonVirtualFields__2352()
        {
            T::Test test___112 = new T::Test();
            try
            {
                TableDef tbl__1272 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("email"), new StringField(), false, null, false)), null);
                G::IReadOnlyDictionary<string, string> params__1273 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                ISafeIdentifier t___15638 = csid__703("name");
                ISafeIdentifier t___15639 = csid__703("email");
                IChangeset cs__1274 = S0::SrcGlobal.Changeset(tbl__1272, params__1273).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15638, t___15639));
                SqlFragment t___8297;
                t___8297 = cs__1274.ToUpdateSql(5);
                SqlFragment t___8298 = t___8297;
                string s__1275 = t___8298.ToString();
                bool t___15643 = s__1275.IndexOf("name = 'Bob'") >= 0;
                string fn__15626()
                {
                    return "name in SET: " + s__1275;
                }
                test___112.Assert(t___15643, (S1::Func<string>) fn__15626);
                bool t___15647 = s__1275.IndexOf("email = 'bob@example.com'") >= 0;
                string fn__15625()
                {
                    return "email in SET: " + s__1275;
                }
                test___112.Assert(t___15647, (S1::Func<string>) fn__15625);
                bool t___15651 = s__1275.IndexOf("WHERE id = 5") >= 0;
                string fn__15624()
                {
                    return "WHERE clause: " + s__1275;
                }
                test___112.Assert(t___15651, (S1::Func<string>) fn__15624);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesWhenAllChangesAreVirtualFields__2353()
        {
            T::Test test___113 = new T::Test();
            try
            {
                TableDef tbl__1277 = new TableDef(csid__703("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__703("name"), new StringField(), false, null, false), new FieldDef(csid__703("computed"), new StringField(), true, null, true)), null);
                G::IReadOnlyDictionary<string, string> params__1278 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("computed", "derived")));
                ISafeIdentifier t___15620 = csid__703("computed");
                IChangeset cs__1279 = S0::SrcGlobal.Changeset(tbl__1277, params__1278).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15620));
                bool didBubble__1280;
                try
                {
                    cs__1279.ToUpdateSql(1);
                    didBubble__1280 = false;
                }
                catch
                {
                    didBubble__1280 = true;
                }
                string fn__15608()
                {
                    return "should bubble when all changes are virtual";
                }
                test___113.Assert(didBubble__1280, (S1::Func<string>) fn__15608);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void putChangeSatisfiesSubsequentValidateRequired__2354()
        {
            T::Test test___114 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1282 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___15598 = userTable__704();
                ISafeIdentifier t___15599 = csid__703("name");
                IChangeset cs__1283 = S0::SrcGlobal.Changeset(t___15598, params__1282).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15599)).PutChange(csid__703("name"), "Injected").ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name")));
                bool t___15605 = cs__1283.IsValid;
                string fn__15596()
                {
                    return "putChange should satisfy required";
                }
                test___114.Assert(t___15605, (S1::Func<string>) fn__15596);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateStartsWithSkipsWhenFieldNotInChanges__2355()
        {
            T::Test test___115 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1285 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___15588 = userTable__704();
                ISafeIdentifier t___15589 = csid__703("name");
                IChangeset cs__1286 = S0::SrcGlobal.Changeset(t___15588, params__1285).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15589)).ValidateStartsWith(csid__703("name"), "Dr.");
                bool t___15593 = cs__1286.IsValid;
                string fn__15586()
                {
                    return "should be valid when field absent";
                }
                test___115.Assert(t___15593, (S1::Func<string>) fn__15586);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateEndsWithSkipsWhenFieldNotInChanges__2356()
        {
            T::Test test___116 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1288 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___15578 = userTable__704();
                ISafeIdentifier t___15579 = csid__703("name");
                IChangeset cs__1289 = S0::SrcGlobal.Changeset(t___15578, params__1288).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15579)).ValidateEndsWith(csid__703("name"), ".com");
                bool t___15583 = cs__1289.IsValid;
                string fn__15576()
                {
                    return "should be valid when field absent";
                }
                test___116.Assert(t___15583, (S1::Func<string>) fn__15576);
            }
            finally
            {
                test___116.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntAcceptsZero__2357()
        {
            T::Test test___117 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1291 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "0")));
                TableDef t___15568 = userTable__704();
                ISafeIdentifier t___15569 = csid__703("age");
                IChangeset cs__1292 = S0::SrcGlobal.Changeset(t___15568, params__1291).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15569)).ValidateInt(csid__703("age"));
                bool t___15573 = cs__1292.IsValid;
                string fn__15565()
                {
                    return "0 should be a valid int";
                }
                test___117.Assert(t___15573, (S1::Func<string>) fn__15565);
            }
            finally
            {
                test___117.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntAcceptsNegative__2358()
        {
            T::Test test___118 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1294 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "-5")));
                TableDef t___15557 = userTable__704();
                ISafeIdentifier t___15558 = csid__703("age");
                IChangeset cs__1295 = S0::SrcGlobal.Changeset(t___15557, params__1294).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15558)).ValidateInt(csid__703("age"));
                bool t___15562 = cs__1295.IsValid;
                string fn__15554()
                {
                    return "-5 should be a valid int";
                }
                test___118.Assert(t___15562, (S1::Func<string>) fn__15554);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void changesetImmutabilityValidatorsDoNotMutateBase__2359()
        {
            T::Test test___119 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__1297 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___15538 = userTable__704();
                ISafeIdentifier t___15539 = csid__703("name");
                ISafeIdentifier t___15540 = csid__703("email");
                IChangeset base__1298 = S0::SrcGlobal.Changeset(t___15538, params__1297).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15539, t___15540));
                IChangeset failed__1299 = base__1298.ValidateLength(csid__703("name"), 3, 50);
                IChangeset passed__1300 = base__1298.ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__703("name"), csid__703("email")));
                bool t___15549 = !failed__1299.IsValid;
                string fn__15534()
                {
                    return "failed branch should be invalid";
                }
                test___119.Assert(t___15549, (S1::Func<string>) fn__15534);
                bool t___15551 = passed__1300.IsValid;
                string fn__15533()
                {
                    return "passed branch should still be valid";
                }
                test___119.Assert(t___15551, (S1::Func<string>) fn__15533);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__709(string name__1650)
        {
            ISafeIdentifier t___7743;
            t___7743 = S0::SrcGlobal.SafeIdentifier(name__1650);
            return t___7743;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__2441()
        {
            T::Test test___120 = new T::Test();
            try
            {
                Query q__1653 = S0::SrcGlobal.From(sid__709("users"));
                bool t___15091 = q__1653.ToSql().ToString() == "SELECT * FROM users";
                string fn__15086()
                {
                    return "bare query";
                }
                test___120.Assert(t___15091, (S1::Func<string>) fn__15086);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__2442()
        {
            T::Test test___121 = new T::Test();
            try
            {
                ISafeIdentifier t___15077 = sid__709("users");
                ISafeIdentifier t___15078 = sid__709("id");
                ISafeIdentifier t___15079 = sid__709("name");
                Query q__1655 = S0::SrcGlobal.From(t___15077).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___15078, t___15079));
                bool t___15084 = q__1655.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__15076()
                {
                    return "select columns";
                }
                test___121.Assert(t___15084, (S1::Func<string>) fn__15076);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__2443()
        {
            T::Test test___122 = new T::Test();
            try
            {
                ISafeIdentifier t___15065 = sid__709("users");
                SqlBuilder t___15066 = new SqlBuilder();
                t___15066.AppendSafe("age > ");
                t___15066.AppendInt32(18);
                SqlFragment t___15069 = t___15066.Accumulated;
                Query q__1657 = S0::SrcGlobal.From(t___15065).Where(t___15069);
                bool t___15074 = q__1657.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__15064()
                {
                    return "where int";
                }
                test___122.Assert(t___15074, (S1::Func<string>) fn__15064);
            }
            finally
            {
                test___122.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__2445()
        {
            T::Test test___123 = new T::Test();
            try
            {
                ISafeIdentifier t___15053 = sid__709("users");
                SqlBuilder t___15054 = new SqlBuilder();
                t___15054.AppendSafe("active = ");
                t___15054.AppendBoolean(true);
                SqlFragment t___15057 = t___15054.Accumulated;
                Query q__1659 = S0::SrcGlobal.From(t___15053).Where(t___15057);
                bool t___15062 = q__1659.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__15052()
                {
                    return "where bool";
                }
                test___123.Assert(t___15062, (S1::Func<string>) fn__15052);
            }
            finally
            {
                test___123.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__2447()
        {
            T::Test test___124 = new T::Test();
            try
            {
                ISafeIdentifier t___15036 = sid__709("users");
                SqlBuilder t___15037 = new SqlBuilder();
                t___15037.AppendSafe("age > ");
                t___15037.AppendInt32(18);
                SqlFragment t___15040 = t___15037.Accumulated;
                Query t___15041 = S0::SrcGlobal.From(t___15036).Where(t___15040);
                SqlBuilder t___15042 = new SqlBuilder();
                t___15042.AppendSafe("active = ");
                t___15042.AppendBoolean(true);
                Query q__1661 = t___15041.Where(t___15042.Accumulated);
                bool t___15050 = q__1661.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__15035()
                {
                    return "chained where";
                }
                test___124.Assert(t___15050, (S1::Func<string>) fn__15035);
            }
            finally
            {
                test___124.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__2450()
        {
            T::Test test___125 = new T::Test();
            try
            {
                ISafeIdentifier t___15027 = sid__709("users");
                ISafeIdentifier t___15028 = sid__709("name");
                Query q__1663 = S0::SrcGlobal.From(t___15027).OrderBy(t___15028, true);
                bool t___15033 = q__1663.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__15026()
                {
                    return "order asc";
                }
                test___125.Assert(t___15033, (S1::Func<string>) fn__15026);
            }
            finally
            {
                test___125.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__2451()
        {
            T::Test test___126 = new T::Test();
            try
            {
                ISafeIdentifier t___15018 = sid__709("users");
                ISafeIdentifier t___15019 = sid__709("created_at");
                Query q__1665 = S0::SrcGlobal.From(t___15018).OrderBy(t___15019, false);
                bool t___15024 = q__1665.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__15017()
                {
                    return "order desc";
                }
                test___126.Assert(t___15024, (S1::Func<string>) fn__15017);
            }
            finally
            {
                test___126.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__2452()
        {
            T::Test test___127 = new T::Test();
            try
            {
                Query t___7677;
                t___7677 = S0::SrcGlobal.From(sid__709("users")).Limit(10);
                Query t___7678;
                t___7678 = t___7677.Offset(20);
                Query q__1667 = t___7678;
                bool t___15015 = q__1667.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__15010()
                {
                    return "limit/offset";
                }
                test___127.Assert(t___15015, (S1::Func<string>) fn__15010);
            }
            finally
            {
                test___127.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__2453()
        {
            T::Test test___128 = new T::Test();
            try
            {
                bool didBubble__1669;
                try
                {
                    S0::SrcGlobal.From(sid__709("users")).Limit(-1);
                    didBubble__1669 = false;
                }
                catch
                {
                    didBubble__1669 = true;
                }
                string fn__15006()
                {
                    return "negative limit should bubble";
                }
                test___128.Assert(didBubble__1669, (S1::Func<string>) fn__15006);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__2454()
        {
            T::Test test___129 = new T::Test();
            try
            {
                bool didBubble__1671;
                try
                {
                    S0::SrcGlobal.From(sid__709("users")).Offset(-1);
                    didBubble__1671 = false;
                }
                catch
                {
                    didBubble__1671 = true;
                }
                string fn__15002()
                {
                    return "negative offset should bubble";
                }
                test___129.Assert(didBubble__1671, (S1::Func<string>) fn__15002);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__2455()
        {
            T::Test test___130 = new T::Test();
            try
            {
                int minAge__1673 = 21;
                ISafeIdentifier t___14980 = sid__709("users");
                ISafeIdentifier t___14981 = sid__709("id");
                ISafeIdentifier t___14982 = sid__709("name");
                ISafeIdentifier t___14983 = sid__709("email");
                Query t___14984 = S0::SrcGlobal.From(t___14980).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14981, t___14982, t___14983));
                SqlBuilder t___14985 = new SqlBuilder();
                t___14985.AppendSafe("age >= ");
                t___14985.AppendInt32(21);
                Query t___14989 = t___14984.Where(t___14985.Accumulated);
                SqlBuilder t___14990 = new SqlBuilder();
                t___14990.AppendSafe("active = ");
                t___14990.AppendBoolean(true);
                Query t___7663;
                t___7663 = t___14989.Where(t___14990.Accumulated).OrderBy(sid__709("name"), true).Limit(25);
                Query t___7664;
                t___7664 = t___7663.Offset(0);
                Query q__1674 = t___7664;
                bool t___15000 = q__1674.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__14979()
                {
                    return "complex query";
                }
                test___130.Assert(t___15000, (S1::Func<string>) fn__14979);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__2458()
        {
            T::Test test___131 = new T::Test();
            try
            {
                Query q__1676 = S0::SrcGlobal.From(sid__709("users"));
                SqlFragment t___7640;
                t___7640 = q__1676.SafeToSql(100);
                SqlFragment t___7641 = t___7640;
                string s__1677 = t___7641.ToString();
                bool t___14977 = s__1677 == "SELECT * FROM users LIMIT 100";
                string fn__14973()
                {
                    return "should have limit: " + s__1677;
                }
                test___131.Assert(t___14977, (S1::Func<string>) fn__14973);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__2459()
        {
            T::Test test___132 = new T::Test();
            try
            {
                Query t___7632;
                t___7632 = S0::SrcGlobal.From(sid__709("users")).Limit(5);
                Query q__1679 = t___7632;
                SqlFragment t___7635;
                t___7635 = q__1679.SafeToSql(100);
                SqlFragment t___7636 = t___7635;
                string s__1680 = t___7636.ToString();
                bool t___14971 = s__1680 == "SELECT * FROM users LIMIT 5";
                string fn__14967()
                {
                    return "explicit limit preserved: " + s__1680;
                }
                test___132.Assert(t___14971, (S1::Func<string>) fn__14967);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__2460()
        {
            T::Test test___133 = new T::Test();
            try
            {
                bool didBubble__1682;
                try
                {
                    S0::SrcGlobal.From(sid__709("users")).SafeToSql(-1);
                    didBubble__1682 = false;
                }
                catch
                {
                    didBubble__1682 = true;
                }
                string fn__14963()
                {
                    return "negative defaultLimit should bubble";
                }
                test___133.Assert(didBubble__1682, (S1::Func<string>) fn__14963);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__2461()
        {
            T::Test test___134 = new T::Test();
            try
            {
                string evil__1684 = "'; DROP TABLE users; --";
                ISafeIdentifier t___14947 = sid__709("users");
                SqlBuilder t___14948 = new SqlBuilder();
                t___14948.AppendSafe("name = ");
                t___14948.AppendString("'; DROP TABLE users; --");
                SqlFragment t___14951 = t___14948.Accumulated;
                Query q__1685 = S0::SrcGlobal.From(t___14947).Where(t___14951);
                string s__1686 = q__1685.ToSql().ToString();
                bool t___14956 = s__1686.IndexOf("''") >= 0;
                string fn__14946()
                {
                    return "quotes must be doubled: " + s__1686;
                }
                test___134.Assert(t___14956, (S1::Func<string>) fn__14946);
                bool t___14960 = s__1686.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__14945()
                {
                    return "structure intact: " + s__1686;
                }
                test___134.Assert(t___14960, (S1::Func<string>) fn__14945);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__2463()
        {
            T::Test test___135 = new T::Test();
            try
            {
                string attack__1688 = "users; DROP TABLE users; --";
                bool didBubble__1689;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__1689 = false;
                }
                catch
                {
                    didBubble__1689 = true;
                }
                string fn__14942()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___135.Assert(didBubble__1689, (S1::Func<string>) fn__14942);
            }
            finally
            {
                test___135.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__2464()
        {
            T::Test test___136 = new T::Test();
            try
            {
                ISafeIdentifier t___14931 = sid__709("users");
                ISafeIdentifier t___14932 = sid__709("orders");
                SqlBuilder t___14933 = new SqlBuilder();
                t___14933.AppendSafe("users.id = orders.user_id");
                SqlFragment t___14935 = t___14933.Accumulated;
                Query q__1691 = S0::SrcGlobal.From(t___14931).InnerJoin(t___14932, t___14935);
                bool t___14940 = q__1691.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__14930()
                {
                    return "inner join";
                }
                test___136.Assert(t___14940, (S1::Func<string>) fn__14930);
            }
            finally
            {
                test___136.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__2466()
        {
            T::Test test___137 = new T::Test();
            try
            {
                ISafeIdentifier t___14919 = sid__709("users");
                ISafeIdentifier t___14920 = sid__709("profiles");
                SqlBuilder t___14921 = new SqlBuilder();
                t___14921.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___14923 = t___14921.Accumulated;
                Query q__1693 = S0::SrcGlobal.From(t___14919).LeftJoin(t___14920, t___14923);
                bool t___14928 = q__1693.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__14918()
                {
                    return "left join";
                }
                test___137.Assert(t___14928, (S1::Func<string>) fn__14918);
            }
            finally
            {
                test___137.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__2468()
        {
            T::Test test___138 = new T::Test();
            try
            {
                ISafeIdentifier t___14907 = sid__709("orders");
                ISafeIdentifier t___14908 = sid__709("users");
                SqlBuilder t___14909 = new SqlBuilder();
                t___14909.AppendSafe("orders.user_id = users.id");
                SqlFragment t___14911 = t___14909.Accumulated;
                Query q__1695 = S0::SrcGlobal.From(t___14907).RightJoin(t___14908, t___14911);
                bool t___14916 = q__1695.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__14906()
                {
                    return "right join";
                }
                test___138.Assert(t___14916, (S1::Func<string>) fn__14906);
            }
            finally
            {
                test___138.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__2470()
        {
            T::Test test___139 = new T::Test();
            try
            {
                ISafeIdentifier t___14895 = sid__709("users");
                ISafeIdentifier t___14896 = sid__709("orders");
                SqlBuilder t___14897 = new SqlBuilder();
                t___14897.AppendSafe("users.id = orders.user_id");
                SqlFragment t___14899 = t___14897.Accumulated;
                Query q__1697 = S0::SrcGlobal.From(t___14895).FullJoin(t___14896, t___14899);
                bool t___14904 = q__1697.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__14894()
                {
                    return "full join";
                }
                test___139.Assert(t___14904, (S1::Func<string>) fn__14894);
            }
            finally
            {
                test___139.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__2472()
        {
            T::Test test___140 = new T::Test();
            try
            {
                ISafeIdentifier t___14878 = sid__709("users");
                ISafeIdentifier t___14879 = sid__709("orders");
                SqlBuilder t___14880 = new SqlBuilder();
                t___14880.AppendSafe("users.id = orders.user_id");
                SqlFragment t___14882 = t___14880.Accumulated;
                Query t___14883 = S0::SrcGlobal.From(t___14878).InnerJoin(t___14879, t___14882);
                ISafeIdentifier t___14884 = sid__709("profiles");
                SqlBuilder t___14885 = new SqlBuilder();
                t___14885.AppendSafe("users.id = profiles.user_id");
                Query q__1699 = t___14883.LeftJoin(t___14884, t___14885.Accumulated);
                bool t___14892 = q__1699.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__14877()
                {
                    return "chained joins";
                }
                test___140.Assert(t___14892, (S1::Func<string>) fn__14877);
            }
            finally
            {
                test___140.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__2475()
        {
            T::Test test___141 = new T::Test();
            try
            {
                ISafeIdentifier t___14859 = sid__709("users");
                ISafeIdentifier t___14860 = sid__709("orders");
                SqlBuilder t___14861 = new SqlBuilder();
                t___14861.AppendSafe("users.id = orders.user_id");
                SqlFragment t___14863 = t___14861.Accumulated;
                Query t___14864 = S0::SrcGlobal.From(t___14859).InnerJoin(t___14860, t___14863);
                SqlBuilder t___14865 = new SqlBuilder();
                t___14865.AppendSafe("orders.total > ");
                t___14865.AppendInt32(100);
                Query t___7547;
                t___7547 = t___14864.Where(t___14865.Accumulated).OrderBy(sid__709("name"), true).Limit(10);
                Query q__1701 = t___7547;
                bool t___14875 = q__1701.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__14858()
                {
                    return "join with where/order/limit";
                }
                test___141.Assert(t___14875, (S1::Func<string>) fn__14858);
            }
            finally
            {
                test___141.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__2478()
        {
            T::Test test___142 = new T::Test();
            try
            {
                SqlFragment c__1703 = S0::SrcGlobal.Col(sid__709("users"), sid__709("id"));
                bool t___14856 = c__1703.ToString() == "users.id";
                string fn__14850()
                {
                    return "col helper";
                }
                test___142.Assert(t___14856, (S1::Func<string>) fn__14850);
            }
            finally
            {
                test___142.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__2479()
        {
            T::Test test___143 = new T::Test();
            try
            {
                SqlFragment onCond__1705 = S0::SrcGlobal.Col(sid__709("users"), sid__709("id"));
                SqlBuilder b__1706 = new SqlBuilder();
                b__1706.AppendFragment(onCond__1705);
                b__1706.AppendSafe(" = ");
                b__1706.AppendFragment(S0::SrcGlobal.Col(sid__709("orders"), sid__709("user_id")));
                ISafeIdentifier t___14841 = sid__709("users");
                ISafeIdentifier t___14842 = sid__709("orders");
                SqlFragment t___14843 = b__1706.Accumulated;
                Query q__1707 = S0::SrcGlobal.From(t___14841).InnerJoin(t___14842, t___14843);
                bool t___14848 = q__1707.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__14830()
                {
                    return "join with col";
                }
                test___143.Assert(t___14848, (S1::Func<string>) fn__14830);
            }
            finally
            {
                test___143.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__2480()
        {
            T::Test test___144 = new T::Test();
            try
            {
                ISafeIdentifier t___14819 = sid__709("users");
                SqlBuilder t___14820 = new SqlBuilder();
                t___14820.AppendSafe("status = ");
                t___14820.AppendString("active");
                SqlFragment t___14823 = t___14820.Accumulated;
                Query q__1709 = S0::SrcGlobal.From(t___14819).OrWhere(t___14823);
                bool t___14828 = q__1709.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__14818()
                {
                    return "orWhere basic";
                }
                test___144.Assert(t___14828, (S1::Func<string>) fn__14818);
            }
            finally
            {
                test___144.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__2482()
        {
            T::Test test___145 = new T::Test();
            try
            {
                ISafeIdentifier t___14802 = sid__709("users");
                SqlBuilder t___14803 = new SqlBuilder();
                t___14803.AppendSafe("age > ");
                t___14803.AppendInt32(18);
                SqlFragment t___14806 = t___14803.Accumulated;
                Query t___14807 = S0::SrcGlobal.From(t___14802).Where(t___14806);
                SqlBuilder t___14808 = new SqlBuilder();
                t___14808.AppendSafe("vip = ");
                t___14808.AppendBoolean(true);
                Query q__1711 = t___14807.OrWhere(t___14808.Accumulated);
                bool t___14816 = q__1711.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__14801()
                {
                    return "where then orWhere";
                }
                test___145.Assert(t___14816, (S1::Func<string>) fn__14801);
            }
            finally
            {
                test___145.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__2485()
        {
            T::Test test___146 = new T::Test();
            try
            {
                ISafeIdentifier t___14780 = sid__709("users");
                SqlBuilder t___14781 = new SqlBuilder();
                t___14781.AppendSafe("active = ");
                t___14781.AppendBoolean(true);
                SqlFragment t___14784 = t___14781.Accumulated;
                Query t___14785 = S0::SrcGlobal.From(t___14780).Where(t___14784);
                SqlBuilder t___14786 = new SqlBuilder();
                t___14786.AppendSafe("role = ");
                t___14786.AppendString("admin");
                Query t___14790 = t___14785.OrWhere(t___14786.Accumulated);
                SqlBuilder t___14791 = new SqlBuilder();
                t___14791.AppendSafe("role = ");
                t___14791.AppendString("moderator");
                Query q__1713 = t___14790.OrWhere(t___14791.Accumulated);
                bool t___14799 = q__1713.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__14779()
                {
                    return "multiple orWhere";
                }
                test___146.Assert(t___14799, (S1::Func<string>) fn__14779);
            }
            finally
            {
                test___146.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__2489()
        {
            T::Test test___147 = new T::Test();
            try
            {
                ISafeIdentifier t___14758 = sid__709("users");
                SqlBuilder t___14759 = new SqlBuilder();
                t___14759.AppendSafe("age > ");
                t___14759.AppendInt32(18);
                SqlFragment t___14762 = t___14759.Accumulated;
                Query t___14763 = S0::SrcGlobal.From(t___14758).Where(t___14762);
                SqlBuilder t___14764 = new SqlBuilder();
                t___14764.AppendSafe("active = ");
                t___14764.AppendBoolean(true);
                Query t___14768 = t___14763.Where(t___14764.Accumulated);
                SqlBuilder t___14769 = new SqlBuilder();
                t___14769.AppendSafe("vip = ");
                t___14769.AppendBoolean(true);
                Query q__1715 = t___14768.OrWhere(t___14769.Accumulated);
                bool t___14777 = q__1715.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__14757()
                {
                    return "mixed where and orWhere";
                }
                test___147.Assert(t___14777, (S1::Func<string>) fn__14757);
            }
            finally
            {
                test___147.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__2493()
        {
            T::Test test___148 = new T::Test();
            try
            {
                ISafeIdentifier t___14749 = sid__709("users");
                ISafeIdentifier t___14750 = sid__709("deleted_at");
                Query q__1717 = S0::SrcGlobal.From(t___14749).WhereNull(t___14750);
                bool t___14755 = q__1717.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__14748()
                {
                    return "whereNull";
                }
                test___148.Assert(t___14755, (S1::Func<string>) fn__14748);
            }
            finally
            {
                test___148.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__2494()
        {
            T::Test test___149 = new T::Test();
            try
            {
                ISafeIdentifier t___14740 = sid__709("users");
                ISafeIdentifier t___14741 = sid__709("email");
                Query q__1719 = S0::SrcGlobal.From(t___14740).WhereNotNull(t___14741);
                bool t___14746 = q__1719.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__14739()
                {
                    return "whereNotNull";
                }
                test___149.Assert(t___14746, (S1::Func<string>) fn__14739);
            }
            finally
            {
                test___149.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__2495()
        {
            T::Test test___150 = new T::Test();
            try
            {
                ISafeIdentifier t___14726 = sid__709("users");
                SqlBuilder t___14727 = new SqlBuilder();
                t___14727.AppendSafe("active = ");
                t___14727.AppendBoolean(true);
                SqlFragment t___14730 = t___14727.Accumulated;
                Query q__1721 = S0::SrcGlobal.From(t___14726).Where(t___14730).WhereNull(sid__709("deleted_at"));
                bool t___14737 = q__1721.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__14725()
                {
                    return "whereNull chained";
                }
                test___150.Assert(t___14737, (S1::Func<string>) fn__14725);
            }
            finally
            {
                test___150.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__2497()
        {
            T::Test test___151 = new T::Test();
            try
            {
                ISafeIdentifier t___14712 = sid__709("users");
                ISafeIdentifier t___14713 = sid__709("deleted_at");
                Query t___14714 = S0::SrcGlobal.From(t___14712).WhereNull(t___14713);
                SqlBuilder t___14715 = new SqlBuilder();
                t___14715.AppendSafe("role = ");
                t___14715.AppendString("admin");
                Query q__1723 = t___14714.OrWhere(t___14715.Accumulated);
                bool t___14723 = q__1723.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__14711()
                {
                    return "whereNotNull with orWhere";
                }
                test___151.Assert(t___14723, (S1::Func<string>) fn__14711);
            }
            finally
            {
                test___151.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__2499()
        {
            T::Test test___152 = new T::Test();
            try
            {
                ISafeIdentifier t___14700 = sid__709("users");
                ISafeIdentifier t___14701 = sid__709("id");
                SqlInt32 t___14702 = new SqlInt32(1);
                SqlInt32 t___14703 = new SqlInt32(2);
                SqlInt32 t___14704 = new SqlInt32(3);
                Query q__1725 = S0::SrcGlobal.From(t___14700).WhereIn(t___14701, C::Listed.CreateReadOnlyList<SqlInt32>(t___14702, t___14703, t___14704));
                bool t___14709 = q__1725.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__14699()
                {
                    return "whereIn ints";
                }
                test___152.Assert(t___14709, (S1::Func<string>) fn__14699);
            }
            finally
            {
                test___152.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__2500()
        {
            T::Test test___153 = new T::Test();
            try
            {
                ISafeIdentifier t___14689 = sid__709("users");
                ISafeIdentifier t___14690 = sid__709("name");
                SqlString t___14691 = new SqlString("Alice");
                SqlString t___14692 = new SqlString("Bob's");
                Query q__1727 = S0::SrcGlobal.From(t___14689).WhereIn(t___14690, C::Listed.CreateReadOnlyList<SqlString>(t___14691, t___14692));
                bool t___14697 = q__1727.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__14688()
                {
                    return "whereIn strings";
                }
                test___153.Assert(t___14697, (S1::Func<string>) fn__14688);
            }
            finally
            {
                test___153.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__2501()
        {
            T::Test test___154 = new T::Test();
            try
            {
                ISafeIdentifier t___14680 = sid__709("users");
                ISafeIdentifier t___14681 = sid__709("id");
                Query q__1729 = S0::SrcGlobal.From(t___14680).WhereIn(t___14681, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___14686 = q__1729.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__14679()
                {
                    return "whereIn empty";
                }
                test___154.Assert(t___14686, (S1::Func<string>) fn__14679);
            }
            finally
            {
                test___154.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__2502()
        {
            T::Test test___155 = new T::Test();
            try
            {
                ISafeIdentifier t___14664 = sid__709("users");
                SqlBuilder t___14665 = new SqlBuilder();
                t___14665.AppendSafe("active = ");
                t___14665.AppendBoolean(true);
                SqlFragment t___14668 = t___14665.Accumulated;
                Query q__1731 = S0::SrcGlobal.From(t___14664).Where(t___14668).WhereIn(sid__709("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___14677 = q__1731.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__14663()
                {
                    return "whereIn chained";
                }
                test___155.Assert(t___14677, (S1::Func<string>) fn__14663);
            }
            finally
            {
                test___155.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__2504()
        {
            T::Test test___156 = new T::Test();
            try
            {
                ISafeIdentifier t___14654 = sid__709("users");
                ISafeIdentifier t___14655 = sid__709("id");
                SqlInt32 t___14656 = new SqlInt32(42);
                Query q__1733 = S0::SrcGlobal.From(t___14654).WhereIn(t___14655, C::Listed.CreateReadOnlyList<SqlInt32>(t___14656));
                bool t___14661 = q__1733.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__14653()
                {
                    return "whereIn single";
                }
                test___156.Assert(t___14661, (S1::Func<string>) fn__14653);
            }
            finally
            {
                test___156.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__2505()
        {
            T::Test test___157 = new T::Test();
            try
            {
                ISafeIdentifier t___14642 = sid__709("users");
                SqlBuilder t___14643 = new SqlBuilder();
                t___14643.AppendSafe("active = ");
                t___14643.AppendBoolean(true);
                SqlFragment t___14646 = t___14643.Accumulated;
                Query q__1735 = S0::SrcGlobal.From(t___14642).WhereNot(t___14646);
                bool t___14651 = q__1735.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__14641()
                {
                    return "whereNot";
                }
                test___157.Assert(t___14651, (S1::Func<string>) fn__14641);
            }
            finally
            {
                test___157.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__2507()
        {
            T::Test test___158 = new T::Test();
            try
            {
                ISafeIdentifier t___14625 = sid__709("users");
                SqlBuilder t___14626 = new SqlBuilder();
                t___14626.AppendSafe("age > ");
                t___14626.AppendInt32(18);
                SqlFragment t___14629 = t___14626.Accumulated;
                Query t___14630 = S0::SrcGlobal.From(t___14625).Where(t___14629);
                SqlBuilder t___14631 = new SqlBuilder();
                t___14631.AppendSafe("banned = ");
                t___14631.AppendBoolean(true);
                Query q__1737 = t___14630.WhereNot(t___14631.Accumulated);
                bool t___14639 = q__1737.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__14624()
                {
                    return "whereNot chained";
                }
                test___158.Assert(t___14639, (S1::Func<string>) fn__14624);
            }
            finally
            {
                test___158.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__2510()
        {
            T::Test test___159 = new T::Test();
            try
            {
                ISafeIdentifier t___14614 = sid__709("users");
                ISafeIdentifier t___14615 = sid__709("age");
                SqlInt32 t___14616 = new SqlInt32(18);
                SqlInt32 t___14617 = new SqlInt32(65);
                Query q__1739 = S0::SrcGlobal.From(t___14614).WhereBetween(t___14615, t___14616, t___14617);
                bool t___14622 = q__1739.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__14613()
                {
                    return "whereBetween ints";
                }
                test___159.Assert(t___14622, (S1::Func<string>) fn__14613);
            }
            finally
            {
                test___159.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__2511()
        {
            T::Test test___160 = new T::Test();
            try
            {
                ISafeIdentifier t___14598 = sid__709("users");
                SqlBuilder t___14599 = new SqlBuilder();
                t___14599.AppendSafe("active = ");
                t___14599.AppendBoolean(true);
                SqlFragment t___14602 = t___14599.Accumulated;
                Query q__1741 = S0::SrcGlobal.From(t___14598).Where(t___14602).WhereBetween(sid__709("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___14611 = q__1741.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__14597()
                {
                    return "whereBetween chained";
                }
                test___160.Assert(t___14611, (S1::Func<string>) fn__14597);
            }
            finally
            {
                test___160.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__2513()
        {
            T::Test test___161 = new T::Test();
            try
            {
                ISafeIdentifier t___14589 = sid__709("users");
                ISafeIdentifier t___14590 = sid__709("name");
                Query q__1743 = S0::SrcGlobal.From(t___14589).WhereLike(t___14590, "John%");
                bool t___14595 = q__1743.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__14588()
                {
                    return "whereLike";
                }
                test___161.Assert(t___14595, (S1::Func<string>) fn__14588);
            }
            finally
            {
                test___161.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__2514()
        {
            T::Test test___162 = new T::Test();
            try
            {
                ISafeIdentifier t___14580 = sid__709("users");
                ISafeIdentifier t___14581 = sid__709("email");
                Query q__1745 = S0::SrcGlobal.From(t___14580).WhereILike(t___14581, "%@gmail.com");
                bool t___14586 = q__1745.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__14579()
                {
                    return "whereILike";
                }
                test___162.Assert(t___14586, (S1::Func<string>) fn__14579);
            }
            finally
            {
                test___162.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__2515()
        {
            T::Test test___163 = new T::Test();
            try
            {
                ISafeIdentifier t___14566 = sid__709("users");
                ISafeIdentifier t___14567 = sid__709("name");
                Query q__1747 = S0::SrcGlobal.From(t___14566).WhereLike(t___14567, "'; DROP TABLE users; --");
                string s__1748 = q__1747.ToSql().ToString();
                bool t___14572 = s__1748.IndexOf("''") >= 0;
                string fn__14565()
                {
                    return "like injection escaped: " + s__1748;
                }
                test___163.Assert(t___14572, (S1::Func<string>) fn__14565);
                bool t___14576 = s__1748.IndexOf("LIKE") >= 0;
                string fn__14564()
                {
                    return "like structure intact: " + s__1748;
                }
                test___163.Assert(t___14576, (S1::Func<string>) fn__14564);
            }
            finally
            {
                test___163.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__2516()
        {
            T::Test test___164 = new T::Test();
            try
            {
                ISafeIdentifier t___14556 = sid__709("users");
                ISafeIdentifier t___14557 = sid__709("name");
                Query q__1750 = S0::SrcGlobal.From(t___14556).WhereLike(t___14557, "%son%");
                bool t___14562 = q__1750.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__14555()
                {
                    return "whereLike wildcard";
                }
                test___164.Assert(t___14562, (S1::Func<string>) fn__14555);
            }
            finally
            {
                test___164.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__2517()
        {
            T::Test test___165 = new T::Test();
            try
            {
                SqlFragment f__1752 = S0::SrcGlobal.CountAll();
                bool t___14553 = f__1752.ToString() == "COUNT(*)";
                string fn__14549()
                {
                    return "countAll";
                }
                test___165.Assert(t___14553, (S1::Func<string>) fn__14549);
            }
            finally
            {
                test___165.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__2518()
        {
            T::Test test___166 = new T::Test();
            try
            {
                SqlFragment f__1754 = S0::SrcGlobal.CountCol(sid__709("id"));
                bool t___14547 = f__1754.ToString() == "COUNT(id)";
                string fn__14542()
                {
                    return "countCol";
                }
                test___166.Assert(t___14547, (S1::Func<string>) fn__14542);
            }
            finally
            {
                test___166.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__2519()
        {
            T::Test test___167 = new T::Test();
            try
            {
                SqlFragment f__1756 = S0::SrcGlobal.SumCol(sid__709("amount"));
                bool t___14540 = f__1756.ToString() == "SUM(amount)";
                string fn__14535()
                {
                    return "sumCol";
                }
                test___167.Assert(t___14540, (S1::Func<string>) fn__14535);
            }
            finally
            {
                test___167.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__2520()
        {
            T::Test test___168 = new T::Test();
            try
            {
                SqlFragment f__1758 = S0::SrcGlobal.AvgCol(sid__709("price"));
                bool t___14533 = f__1758.ToString() == "AVG(price)";
                string fn__14528()
                {
                    return "avgCol";
                }
                test___168.Assert(t___14533, (S1::Func<string>) fn__14528);
            }
            finally
            {
                test___168.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__2521()
        {
            T::Test test___169 = new T::Test();
            try
            {
                SqlFragment f__1760 = S0::SrcGlobal.MinCol(sid__709("created_at"));
                bool t___14526 = f__1760.ToString() == "MIN(created_at)";
                string fn__14521()
                {
                    return "minCol";
                }
                test___169.Assert(t___14526, (S1::Func<string>) fn__14521);
            }
            finally
            {
                test___169.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__2522()
        {
            T::Test test___170 = new T::Test();
            try
            {
                SqlFragment f__1762 = S0::SrcGlobal.MaxCol(sid__709("score"));
                bool t___14519 = f__1762.ToString() == "MAX(score)";
                string fn__14514()
                {
                    return "maxCol";
                }
                test___170.Assert(t___14519, (S1::Func<string>) fn__14514);
            }
            finally
            {
                test___170.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__2523()
        {
            T::Test test___171 = new T::Test();
            try
            {
                ISafeIdentifier t___14506 = sid__709("orders");
                SqlFragment t___14507 = S0::SrcGlobal.CountAll();
                Query q__1764 = S0::SrcGlobal.From(t___14506).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___14507));
                bool t___14512 = q__1764.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__14505()
                {
                    return "selectExpr count";
                }
                test___171.Assert(t___14512, (S1::Func<string>) fn__14505);
            }
            finally
            {
                test___171.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__2524()
        {
            T::Test test___172 = new T::Test();
            try
            {
                SqlFragment nameFrag__1766 = S0::SrcGlobal.Col(sid__709("users"), sid__709("name"));
                ISafeIdentifier t___14497 = sid__709("users");
                SqlFragment t___14498 = S0::SrcGlobal.CountAll();
                Query q__1767 = S0::SrcGlobal.From(t___14497).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1766, t___14498));
                bool t___14503 = q__1767.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__14493()
                {
                    return "selectExpr multi";
                }
                test___172.Assert(t___14503, (S1::Func<string>) fn__14493);
            }
            finally
            {
                test___172.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__2525()
        {
            T::Test test___173 = new T::Test();
            try
            {
                ISafeIdentifier t___14482 = sid__709("users");
                ISafeIdentifier t___14483 = sid__709("id");
                ISafeIdentifier t___14484 = sid__709("name");
                Query q__1769 = S0::SrcGlobal.From(t___14482).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14483, t___14484)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___14491 = q__1769.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__14481()
                {
                    return "selectExpr overrides select";
                }
                test___173.Assert(t___14491, (S1::Func<string>) fn__14481);
            }
            finally
            {
                test___173.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__2526()
        {
            T::Test test___174 = new T::Test();
            try
            {
                ISafeIdentifier t___14468 = sid__709("orders");
                SqlFragment t___14471 = S0::SrcGlobal.Col(sid__709("orders"), sid__709("status"));
                SqlFragment t___14472 = S0::SrcGlobal.CountAll();
                Query q__1771 = S0::SrcGlobal.From(t___14468).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___14471, t___14472)).GroupBy(sid__709("status"));
                bool t___14479 = q__1771.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__14467()
                {
                    return "groupBy single";
                }
                test___174.Assert(t___14479, (S1::Func<string>) fn__14467);
            }
            finally
            {
                test___174.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__2527()
        {
            T::Test test___175 = new T::Test();
            try
            {
                ISafeIdentifier t___14457 = sid__709("orders");
                ISafeIdentifier t___14458 = sid__709("status");
                Query q__1773 = S0::SrcGlobal.From(t___14457).GroupBy(t___14458).GroupBy(sid__709("category"));
                bool t___14465 = q__1773.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__14456()
                {
                    return "groupBy multiple";
                }
                test___175.Assert(t___14465, (S1::Func<string>) fn__14456);
            }
            finally
            {
                test___175.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__2528()
        {
            T::Test test___176 = new T::Test();
            try
            {
                ISafeIdentifier t___14438 = sid__709("orders");
                SqlFragment t___14441 = S0::SrcGlobal.Col(sid__709("orders"), sid__709("status"));
                SqlFragment t___14442 = S0::SrcGlobal.CountAll();
                Query t___14445 = S0::SrcGlobal.From(t___14438).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___14441, t___14442)).GroupBy(sid__709("status"));
                SqlBuilder t___14446 = new SqlBuilder();
                t___14446.AppendSafe("COUNT(*) > ");
                t___14446.AppendInt32(5);
                Query q__1775 = t___14445.Having(t___14446.Accumulated);
                bool t___14454 = q__1775.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__14437()
                {
                    return "having basic";
                }
                test___176.Assert(t___14454, (S1::Func<string>) fn__14437);
            }
            finally
            {
                test___176.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__2530()
        {
            T::Test test___177 = new T::Test();
            try
            {
                ISafeIdentifier t___14419 = sid__709("orders");
                ISafeIdentifier t___14420 = sid__709("status");
                Query t___14421 = S0::SrcGlobal.From(t___14419).GroupBy(t___14420);
                SqlBuilder t___14422 = new SqlBuilder();
                t___14422.AppendSafe("COUNT(*) > ");
                t___14422.AppendInt32(5);
                Query t___14426 = t___14421.Having(t___14422.Accumulated);
                SqlBuilder t___14427 = new SqlBuilder();
                t___14427.AppendSafe("SUM(total) > ");
                t___14427.AppendInt32(1000);
                Query q__1777 = t___14426.OrHaving(t___14427.Accumulated);
                bool t___14435 = q__1777.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__14418()
                {
                    return "orHaving";
                }
                test___177.Assert(t___14435, (S1::Func<string>) fn__14418);
            }
            finally
            {
                test___177.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__2533()
        {
            T::Test test___178 = new T::Test();
            try
            {
                ISafeIdentifier t___14409 = sid__709("users");
                ISafeIdentifier t___14410 = sid__709("name");
                Query q__1779 = S0::SrcGlobal.From(t___14409).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14410)).Distinct();
                bool t___14416 = q__1779.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__14408()
                {
                    return "distinct";
                }
                test___178.Assert(t___14416, (S1::Func<string>) fn__14408);
            }
            finally
            {
                test___178.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__2534()
        {
            T::Test test___179 = new T::Test();
            try
            {
                ISafeIdentifier t___14394 = sid__709("users");
                ISafeIdentifier t___14395 = sid__709("email");
                Query t___14396 = S0::SrcGlobal.From(t___14394).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14395));
                SqlBuilder t___14397 = new SqlBuilder();
                t___14397.AppendSafe("active = ");
                t___14397.AppendBoolean(true);
                Query q__1781 = t___14396.Where(t___14397.Accumulated).Distinct();
                bool t___14406 = q__1781.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__14393()
                {
                    return "distinct with where";
                }
                test___179.Assert(t___14406, (S1::Func<string>) fn__14393);
            }
            finally
            {
                test___179.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__2536()
        {
            T::Test test___180 = new T::Test();
            try
            {
                Query q__1783 = S0::SrcGlobal.From(sid__709("users"));
                bool t___14391 = q__1783.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__14386()
                {
                    return "countSql bare";
                }
                test___180.Assert(t___14391, (S1::Func<string>) fn__14386);
            }
            finally
            {
                test___180.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__2537()
        {
            T::Test test___181 = new T::Test();
            try
            {
                ISafeIdentifier t___14375 = sid__709("users");
                SqlBuilder t___14376 = new SqlBuilder();
                t___14376.AppendSafe("active = ");
                t___14376.AppendBoolean(true);
                SqlFragment t___14379 = t___14376.Accumulated;
                Query q__1785 = S0::SrcGlobal.From(t___14375).Where(t___14379);
                bool t___14384 = q__1785.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__14374()
                {
                    return "countSql with where";
                }
                test___181.Assert(t___14384, (S1::Func<string>) fn__14374);
            }
            finally
            {
                test___181.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__2539()
        {
            T::Test test___182 = new T::Test();
            try
            {
                ISafeIdentifier t___14358 = sid__709("users");
                ISafeIdentifier t___14359 = sid__709("orders");
                SqlBuilder t___14360 = new SqlBuilder();
                t___14360.AppendSafe("users.id = orders.user_id");
                SqlFragment t___14362 = t___14360.Accumulated;
                Query t___14363 = S0::SrcGlobal.From(t___14358).InnerJoin(t___14359, t___14362);
                SqlBuilder t___14364 = new SqlBuilder();
                t___14364.AppendSafe("orders.total > ");
                t___14364.AppendInt32(100);
                Query q__1787 = t___14363.Where(t___14364.Accumulated);
                bool t___14372 = q__1787.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__14357()
                {
                    return "countSql with join";
                }
                test___182.Assert(t___14372, (S1::Func<string>) fn__14357);
            }
            finally
            {
                test___182.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__2542()
        {
            T::Test test___183 = new T::Test();
            try
            {
                ISafeIdentifier t___14344 = sid__709("users");
                SqlBuilder t___14345 = new SqlBuilder();
                t___14345.AppendSafe("active = ");
                t___14345.AppendBoolean(true);
                SqlFragment t___14348 = t___14345.Accumulated;
                Query t___7123;
                t___7123 = S0::SrcGlobal.From(t___14344).Where(t___14348).OrderBy(sid__709("name"), true).Limit(10);
                Query t___7124;
                t___7124 = t___7123.Offset(20);
                Query q__1789 = t___7124;
                string s__1790 = q__1789.CountSql().ToString();
                bool t___14355 = s__1790 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__14343()
                {
                    return "countSql drops extras: " + s__1790;
                }
                test___183.Assert(t___14355, (S1::Func<string>) fn__14343);
            }
            finally
            {
                test___183.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__2544()
        {
            T::Test test___184 = new T::Test();
            try
            {
                ISafeIdentifier t___14311 = sid__709("orders");
                SqlFragment t___14314 = S0::SrcGlobal.Col(sid__709("orders"), sid__709("status"));
                SqlFragment t___14315 = S0::SrcGlobal.CountAll();
                SqlFragment t___14317 = S0::SrcGlobal.SumCol(sid__709("total"));
                Query t___14318 = S0::SrcGlobal.From(t___14311).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___14314, t___14315, t___14317));
                ISafeIdentifier t___14319 = sid__709("users");
                SqlBuilder t___14320 = new SqlBuilder();
                t___14320.AppendSafe("orders.user_id = users.id");
                Query t___14323 = t___14318.InnerJoin(t___14319, t___14320.Accumulated);
                SqlBuilder t___14324 = new SqlBuilder();
                t___14324.AppendSafe("users.active = ");
                t___14324.AppendBoolean(true);
                Query t___14330 = t___14323.Where(t___14324.Accumulated).GroupBy(sid__709("status"));
                SqlBuilder t___14331 = new SqlBuilder();
                t___14331.AppendSafe("COUNT(*) > ");
                t___14331.AppendInt32(3);
                Query q__1792 = t___14330.Having(t___14331.Accumulated).OrderBy(sid__709("status"), true);
                string expected__1793 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___14341 = q__1792.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__14310()
                {
                    return "full aggregation";
                }
                test___184.Assert(t___14341, (S1::Func<string>) fn__14310);
            }
            finally
            {
                test___184.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionSql__2548()
        {
            T::Test test___185 = new T::Test();
            try
            {
                ISafeIdentifier t___14293 = sid__709("users");
                SqlBuilder t___14294 = new SqlBuilder();
                t___14294.AppendSafe("role = ");
                t___14294.AppendString("admin");
                SqlFragment t___14297 = t___14294.Accumulated;
                Query a__1795 = S0::SrcGlobal.From(t___14293).Where(t___14297);
                ISafeIdentifier t___14299 = sid__709("users");
                SqlBuilder t___14300 = new SqlBuilder();
                t___14300.AppendSafe("role = ");
                t___14300.AppendString("moderator");
                SqlFragment t___14303 = t___14300.Accumulated;
                Query b__1796 = S0::SrcGlobal.From(t___14299).Where(t___14303);
                string s__1797 = S0::SrcGlobal.UnionSql(a__1795, b__1796).ToString();
                bool t___14308 = s__1797 == "(SELECT * FROM users WHERE role = 'admin') UNION (SELECT * FROM users WHERE role = 'moderator')";
                string fn__14292()
                {
                    return "unionSql: " + s__1797;
                }
                test___185.Assert(t___14308, (S1::Func<string>) fn__14292);
            }
            finally
            {
                test___185.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionAllSql__2551()
        {
            T::Test test___186 = new T::Test();
            try
            {
                ISafeIdentifier t___14281 = sid__709("users");
                ISafeIdentifier t___14282 = sid__709("name");
                Query a__1799 = S0::SrcGlobal.From(t___14281).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14282));
                ISafeIdentifier t___14284 = sid__709("contacts");
                ISafeIdentifier t___14285 = sid__709("name");
                Query b__1800 = S0::SrcGlobal.From(t___14284).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14285));
                string s__1801 = S0::SrcGlobal.UnionAllSql(a__1799, b__1800).ToString();
                bool t___14290 = s__1801 == "(SELECT name FROM users) UNION ALL (SELECT name FROM contacts)";
                string fn__14280()
                {
                    return "unionAllSql: " + s__1801;
                }
                test___186.Assert(t___14290, (S1::Func<string>) fn__14280);
            }
            finally
            {
                test___186.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void intersectSql__2552()
        {
            T::Test test___187 = new T::Test();
            try
            {
                ISafeIdentifier t___14269 = sid__709("users");
                ISafeIdentifier t___14270 = sid__709("email");
                Query a__1803 = S0::SrcGlobal.From(t___14269).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14270));
                ISafeIdentifier t___14272 = sid__709("subscribers");
                ISafeIdentifier t___14273 = sid__709("email");
                Query b__1804 = S0::SrcGlobal.From(t___14272).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14273));
                string s__1805 = S0::SrcGlobal.IntersectSql(a__1803, b__1804).ToString();
                bool t___14278 = s__1805 == "(SELECT email FROM users) INTERSECT (SELECT email FROM subscribers)";
                string fn__14268()
                {
                    return "intersectSql: " + s__1805;
                }
                test___187.Assert(t___14278, (S1::Func<string>) fn__14268);
            }
            finally
            {
                test___187.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void exceptSql__2553()
        {
            T::Test test___188 = new T::Test();
            try
            {
                ISafeIdentifier t___14257 = sid__709("users");
                ISafeIdentifier t___14258 = sid__709("id");
                Query a__1807 = S0::SrcGlobal.From(t___14257).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14258));
                ISafeIdentifier t___14260 = sid__709("banned");
                ISafeIdentifier t___14261 = sid__709("id");
                Query b__1808 = S0::SrcGlobal.From(t___14260).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14261));
                string s__1809 = S0::SrcGlobal.ExceptSql(a__1807, b__1808).ToString();
                bool t___14266 = s__1809 == "(SELECT id FROM users) EXCEPT (SELECT id FROM banned)";
                string fn__14256()
                {
                    return "exceptSql: " + s__1809;
                }
                test___188.Assert(t___14266, (S1::Func<string>) fn__14256);
            }
            finally
            {
                test___188.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void subqueryWithAlias__2554()
        {
            T::Test test___189 = new T::Test();
            try
            {
                ISafeIdentifier t___14242 = sid__709("orders");
                ISafeIdentifier t___14243 = sid__709("user_id");
                Query t___14244 = S0::SrcGlobal.From(t___14242).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14243));
                SqlBuilder t___14245 = new SqlBuilder();
                t___14245.AppendSafe("total > ");
                t___14245.AppendInt32(100);
                Query inner__1811 = t___14244.Where(t___14245.Accumulated);
                string s__1812 = S0::SrcGlobal.Subquery(inner__1811, sid__709("big_orders")).ToString();
                bool t___14254 = s__1812 == "(SELECT user_id FROM orders WHERE total > 100) AS big_orders";
                string fn__14241()
                {
                    return "subquery: " + s__1812;
                }
                test___189.Assert(t___14254, (S1::Func<string>) fn__14241);
            }
            finally
            {
                test___189.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSql__2556()
        {
            T::Test test___190 = new T::Test();
            try
            {
                ISafeIdentifier t___14231 = sid__709("orders");
                SqlBuilder t___14232 = new SqlBuilder();
                t___14232.AppendSafe("orders.user_id = users.id");
                SqlFragment t___14234 = t___14232.Accumulated;
                Query inner__1814 = S0::SrcGlobal.From(t___14231).Where(t___14234);
                string s__1815 = S0::SrcGlobal.ExistsSql(inner__1814).ToString();
                bool t___14239 = s__1815 == "EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__14230()
                {
                    return "existsSql: " + s__1815;
                }
                test___190.Assert(t___14239, (S1::Func<string>) fn__14230);
            }
            finally
            {
                test___190.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubquery__2558()
        {
            T::Test test___191 = new T::Test();
            try
            {
                ISafeIdentifier t___14214 = sid__709("orders");
                ISafeIdentifier t___14215 = sid__709("user_id");
                Query t___14216 = S0::SrcGlobal.From(t___14214).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14215));
                SqlBuilder t___14217 = new SqlBuilder();
                t___14217.AppendSafe("total > ");
                t___14217.AppendInt32(1000);
                Query sub__1817 = t___14216.Where(t___14217.Accumulated);
                ISafeIdentifier t___14222 = sid__709("users");
                ISafeIdentifier t___14223 = sid__709("id");
                Query q__1818 = S0::SrcGlobal.From(t___14222).WhereInSubquery(t___14223, sub__1817);
                string s__1819 = q__1818.ToSql().ToString();
                bool t___14228 = s__1819 == "SELECT * FROM users WHERE id IN (SELECT user_id FROM orders WHERE total > 1000)";
                string fn__14213()
                {
                    return "whereInSubquery: " + s__1819;
                }
                test___191.Assert(t___14228, (S1::Func<string>) fn__14213);
            }
            finally
            {
                test___191.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void setOperationWithWhereOnEachSide__2560()
        {
            T::Test test___192 = new T::Test();
            try
            {
                ISafeIdentifier t___14191 = sid__709("users");
                SqlBuilder t___14192 = new SqlBuilder();
                t___14192.AppendSafe("age > ");
                t___14192.AppendInt32(18);
                SqlFragment t___14195 = t___14192.Accumulated;
                Query t___14196 = S0::SrcGlobal.From(t___14191).Where(t___14195);
                SqlBuilder t___14197 = new SqlBuilder();
                t___14197.AppendSafe("active = ");
                t___14197.AppendBoolean(true);
                Query a__1821 = t___14196.Where(t___14197.Accumulated);
                ISafeIdentifier t___14202 = sid__709("users");
                SqlBuilder t___14203 = new SqlBuilder();
                t___14203.AppendSafe("role = ");
                t___14203.AppendString("vip");
                SqlFragment t___14206 = t___14203.Accumulated;
                Query b__1822 = S0::SrcGlobal.From(t___14202).Where(t___14206);
                string s__1823 = S0::SrcGlobal.UnionSql(a__1821, b__1822).ToString();
                bool t___14211 = s__1823 == "(SELECT * FROM users WHERE age > 18 AND active = TRUE) UNION (SELECT * FROM users WHERE role = 'vip')";
                string fn__14190()
                {
                    return "union with where: " + s__1823;
                }
                test___192.Assert(t___14211, (S1::Func<string>) fn__14190);
            }
            finally
            {
                test___192.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubqueryChainedWithWhere__2564()
        {
            T::Test test___193 = new T::Test();
            try
            {
                ISafeIdentifier t___14174 = sid__709("orders");
                ISafeIdentifier t___14175 = sid__709("user_id");
                Query sub__1825 = S0::SrcGlobal.From(t___14174).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___14175));
                ISafeIdentifier t___14177 = sid__709("users");
                SqlBuilder t___14178 = new SqlBuilder();
                t___14178.AppendSafe("active = ");
                t___14178.AppendBoolean(true);
                SqlFragment t___14181 = t___14178.Accumulated;
                Query q__1826 = S0::SrcGlobal.From(t___14177).Where(t___14181).WhereInSubquery(sid__709("id"), sub__1825);
                string s__1827 = q__1826.ToSql().ToString();
                bool t___14188 = s__1827 == "SELECT * FROM users WHERE active = TRUE AND id IN (SELECT user_id FROM orders)";
                string fn__14173()
                {
                    return "whereInSubquery chained: " + s__1827;
                }
                test___193.Assert(t___14188, (S1::Func<string>) fn__14173);
            }
            finally
            {
                test___193.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSqlUsedInWhere__2566()
        {
            T::Test test___194 = new T::Test();
            try
            {
                ISafeIdentifier t___14160 = sid__709("orders");
                SqlBuilder t___14161 = new SqlBuilder();
                t___14161.AppendSafe("orders.user_id = users.id");
                SqlFragment t___14163 = t___14161.Accumulated;
                Query sub__1829 = S0::SrcGlobal.From(t___14160).Where(t___14163);
                ISafeIdentifier t___14165 = sid__709("users");
                SqlFragment t___14166 = S0::SrcGlobal.ExistsSql(sub__1829);
                Query q__1830 = S0::SrcGlobal.From(t___14165).Where(t___14166);
                string s__1831 = q__1830.ToSql().ToString();
                bool t___14171 = s__1831 == "SELECT * FROM users WHERE EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__14159()
                {
                    return "exists in where: " + s__1831;
                }
                test___194.Assert(t___14171, (S1::Func<string>) fn__14159);
            }
            finally
            {
                test___194.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBasic__2568()
        {
            T::Test test___195 = new T::Test();
            try
            {
                ISafeIdentifier t___14146 = sid__709("users");
                ISafeIdentifier t___14147 = sid__709("name");
                SqlString t___14148 = new SqlString("Alice");
                UpdateQuery t___14149 = S0::SrcGlobal.Update(t___14146).Set(t___14147, t___14148);
                SqlBuilder t___14150 = new SqlBuilder();
                t___14150.AppendSafe("id = ");
                t___14150.AppendInt32(1);
                SqlFragment t___6945;
                t___6945 = t___14149.Where(t___14150.Accumulated).ToSql();
                SqlFragment q__1833 = t___6945;
                bool t___14157 = q__1833.ToString() == "UPDATE users SET name = 'Alice' WHERE id = 1";
                string fn__14145()
                {
                    return "update basic";
                }
                test___195.Assert(t___14157, (S1::Func<string>) fn__14145);
            }
            finally
            {
                test___195.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleSet__2570()
        {
            T::Test test___196 = new T::Test();
            try
            {
                ISafeIdentifier t___14129 = sid__709("users");
                ISafeIdentifier t___14130 = sid__709("name");
                SqlString t___14131 = new SqlString("Bob");
                UpdateQuery t___14135 = S0::SrcGlobal.Update(t___14129).Set(t___14130, t___14131).Set(sid__709("age"), new SqlInt32(30));
                SqlBuilder t___14136 = new SqlBuilder();
                t___14136.AppendSafe("id = ");
                t___14136.AppendInt32(2);
                SqlFragment t___6930;
                t___6930 = t___14135.Where(t___14136.Accumulated).ToSql();
                SqlFragment q__1835 = t___6930;
                bool t___14143 = q__1835.ToString() == "UPDATE users SET name = 'Bob', age = 30 WHERE id = 2";
                string fn__14128()
                {
                    return "update multi set";
                }
                test___196.Assert(t___14143, (S1::Func<string>) fn__14128);
            }
            finally
            {
                test___196.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleWhere__2572()
        {
            T::Test test___197 = new T::Test();
            try
            {
                ISafeIdentifier t___14110 = sid__709("users");
                ISafeIdentifier t___14111 = sid__709("active");
                SqlBoolean t___14112 = new SqlBoolean(false);
                UpdateQuery t___14113 = S0::SrcGlobal.Update(t___14110).Set(t___14111, t___14112);
                SqlBuilder t___14114 = new SqlBuilder();
                t___14114.AppendSafe("age < ");
                t___14114.AppendInt32(18);
                UpdateQuery t___14118 = t___14113.Where(t___14114.Accumulated);
                SqlBuilder t___14119 = new SqlBuilder();
                t___14119.AppendSafe("role = ");
                t___14119.AppendString("guest");
                SqlFragment t___6912;
                t___6912 = t___14118.Where(t___14119.Accumulated).ToSql();
                SqlFragment q__1837 = t___6912;
                bool t___14126 = q__1837.ToString() == "UPDATE users SET active = FALSE WHERE age < 18 AND role = 'guest'";
                string fn__14109()
                {
                    return "update multi where";
                }
                test___197.Assert(t___14126, (S1::Func<string>) fn__14109);
            }
            finally
            {
                test___197.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryOrWhere__2575()
        {
            T::Test test___198 = new T::Test();
            try
            {
                ISafeIdentifier t___14091 = sid__709("users");
                ISafeIdentifier t___14092 = sid__709("status");
                SqlString t___14093 = new SqlString("banned");
                UpdateQuery t___14094 = S0::SrcGlobal.Update(t___14091).Set(t___14092, t___14093);
                SqlBuilder t___14095 = new SqlBuilder();
                t___14095.AppendSafe("spam_count > ");
                t___14095.AppendInt32(10);
                UpdateQuery t___14099 = t___14094.Where(t___14095.Accumulated);
                SqlBuilder t___14100 = new SqlBuilder();
                t___14100.AppendSafe("reported = ");
                t___14100.AppendBoolean(true);
                SqlFragment t___6891;
                t___6891 = t___14099.OrWhere(t___14100.Accumulated).ToSql();
                SqlFragment q__1839 = t___6891;
                bool t___14107 = q__1839.ToString() == "UPDATE users SET status = 'banned' WHERE spam_count > 10 OR reported = TRUE";
                string fn__14090()
                {
                    return "update orWhere";
                }
                test___198.Assert(t___14107, (S1::Func<string>) fn__14090);
            }
            finally
            {
                test___198.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutWhere__2578()
        {
            T::Test test___199 = new T::Test();
            try
            {
                ISafeIdentifier t___14084;
                ISafeIdentifier t___14085;
                SqlInt32 t___14086;
                bool didBubble__1841;
                try
                {
                    t___14084 = sid__709("users");
                    t___14085 = sid__709("x");
                    t___14086 = new SqlInt32(1);
                    S0::SrcGlobal.Update(t___14084).Set(t___14085, t___14086).ToSql();
                    didBubble__1841 = false;
                }
                catch
                {
                    didBubble__1841 = true;
                }
                string fn__14083()
                {
                    return "update without WHERE should bubble";
                }
                test___199.Assert(didBubble__1841, (S1::Func<string>) fn__14083);
            }
            finally
            {
                test___199.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutSet__2579()
        {
            T::Test test___200 = new T::Test();
            try
            {
                ISafeIdentifier t___14075;
                SqlBuilder t___14076;
                SqlFragment t___14079;
                bool didBubble__1843;
                try
                {
                    t___14075 = sid__709("users");
                    t___14076 = new SqlBuilder();
                    t___14076.AppendSafe("id = ");
                    t___14076.AppendInt32(1);
                    t___14079 = t___14076.Accumulated;
                    S0::SrcGlobal.Update(t___14075).Where(t___14079).ToSql();
                    didBubble__1843 = false;
                }
                catch
                {
                    didBubble__1843 = true;
                }
                string fn__14074()
                {
                    return "update without SET should bubble";
                }
                test___200.Assert(didBubble__1843, (S1::Func<string>) fn__14074);
            }
            finally
            {
                test___200.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryWithLimit__2581()
        {
            T::Test test___201 = new T::Test();
            try
            {
                ISafeIdentifier t___14061 = sid__709("users");
                ISafeIdentifier t___14062 = sid__709("active");
                SqlBoolean t___14063 = new SqlBoolean(false);
                UpdateQuery t___14064 = S0::SrcGlobal.Update(t___14061).Set(t___14062, t___14063);
                SqlBuilder t___14065 = new SqlBuilder();
                t___14065.AppendSafe("last_login < ");
                t___14065.AppendString("2024-01-01");
                UpdateQuery t___6854;
                t___6854 = t___14064.Where(t___14065.Accumulated).Limit(100);
                SqlFragment t___6855;
                t___6855 = t___6854.ToSql();
                SqlFragment q__1845 = t___6855;
                bool t___14072 = q__1845.ToString() == "UPDATE users SET active = FALSE WHERE last_login < '2024-01-01' LIMIT 100";
                string fn__14060()
                {
                    return "update limit";
                }
                test___201.Assert(t___14072, (S1::Func<string>) fn__14060);
            }
            finally
            {
                test___201.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryEscaping__2583()
        {
            T::Test test___202 = new T::Test();
            try
            {
                ISafeIdentifier t___14047 = sid__709("users");
                ISafeIdentifier t___14048 = sid__709("bio");
                SqlString t___14049 = new SqlString("It's a test");
                UpdateQuery t___14050 = S0::SrcGlobal.Update(t___14047).Set(t___14048, t___14049);
                SqlBuilder t___14051 = new SqlBuilder();
                t___14051.AppendSafe("id = ");
                t___14051.AppendInt32(1);
                SqlFragment t___6839;
                t___6839 = t___14050.Where(t___14051.Accumulated).ToSql();
                SqlFragment q__1847 = t___6839;
                bool t___14058 = q__1847.ToString() == "UPDATE users SET bio = 'It''s a test' WHERE id = 1";
                string fn__14046()
                {
                    return "update escaping";
                }
                test___202.Assert(t___14058, (S1::Func<string>) fn__14046);
            }
            finally
            {
                test___202.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBasic__2585()
        {
            T::Test test___203 = new T::Test();
            try
            {
                ISafeIdentifier t___14036 = sid__709("users");
                SqlBuilder t___14037 = new SqlBuilder();
                t___14037.AppendSafe("id = ");
                t___14037.AppendInt32(1);
                SqlFragment t___14040 = t___14037.Accumulated;
                SqlFragment t___6824;
                t___6824 = S0::SrcGlobal.DeleteFrom(t___14036).Where(t___14040).ToSql();
                SqlFragment q__1849 = t___6824;
                bool t___14044 = q__1849.ToString() == "DELETE FROM users WHERE id = 1";
                string fn__14035()
                {
                    return "delete basic";
                }
                test___203.Assert(t___14044, (S1::Func<string>) fn__14035);
            }
            finally
            {
                test___203.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryMultipleWhere__2587()
        {
            T::Test test___204 = new T::Test();
            try
            {
                ISafeIdentifier t___14020 = sid__709("logs");
                SqlBuilder t___14021 = new SqlBuilder();
                t___14021.AppendSafe("created_at < ");
                t___14021.AppendString("2024-01-01");
                SqlFragment t___14024 = t___14021.Accumulated;
                DeleteQuery t___14025 = S0::SrcGlobal.DeleteFrom(t___14020).Where(t___14024);
                SqlBuilder t___14026 = new SqlBuilder();
                t___14026.AppendSafe("level = ");
                t___14026.AppendString("debug");
                SqlFragment t___6812;
                t___6812 = t___14025.Where(t___14026.Accumulated).ToSql();
                SqlFragment q__1851 = t___6812;
                bool t___14033 = q__1851.ToString() == "DELETE FROM logs WHERE created_at < '2024-01-01' AND level = 'debug'";
                string fn__14019()
                {
                    return "delete multi where";
                }
                test___204.Assert(t___14033, (S1::Func<string>) fn__14019);
            }
            finally
            {
                test___204.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBubblesWithoutWhere__2590()
        {
            T::Test test___205 = new T::Test();
            try
            {
                bool didBubble__1853;
                try
                {
                    S0::SrcGlobal.DeleteFrom(sid__709("users")).ToSql();
                    didBubble__1853 = false;
                }
                catch
                {
                    didBubble__1853 = true;
                }
                string fn__14015()
                {
                    return "delete without WHERE should bubble";
                }
                test___205.Assert(didBubble__1853, (S1::Func<string>) fn__14015);
            }
            finally
            {
                test___205.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryOrWhere__2591()
        {
            T::Test test___206 = new T::Test();
            try
            {
                ISafeIdentifier t___14000 = sid__709("sessions");
                SqlBuilder t___14001 = new SqlBuilder();
                t___14001.AppendSafe("expired = ");
                t___14001.AppendBoolean(true);
                SqlFragment t___14004 = t___14001.Accumulated;
                DeleteQuery t___14005 = S0::SrcGlobal.DeleteFrom(t___14000).Where(t___14004);
                SqlBuilder t___14006 = new SqlBuilder();
                t___14006.AppendSafe("created_at < ");
                t___14006.AppendString("2023-01-01");
                SqlFragment t___6791;
                t___6791 = t___14005.OrWhere(t___14006.Accumulated).ToSql();
                SqlFragment q__1855 = t___6791;
                bool t___14013 = q__1855.ToString() == "DELETE FROM sessions WHERE expired = TRUE OR created_at < '2023-01-01'";
                string fn__13999()
                {
                    return "delete orWhere";
                }
                test___206.Assert(t___14013, (S1::Func<string>) fn__13999);
            }
            finally
            {
                test___206.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryWithLimit__2594()
        {
            T::Test test___207 = new T::Test();
            try
            {
                ISafeIdentifier t___13989 = sid__709("logs");
                SqlBuilder t___13990 = new SqlBuilder();
                t___13990.AppendSafe("level = ");
                t___13990.AppendString("debug");
                SqlFragment t___13993 = t___13990.Accumulated;
                DeleteQuery t___6772;
                t___6772 = S0::SrcGlobal.DeleteFrom(t___13989).Where(t___13993).Limit(1000);
                SqlFragment t___6773;
                t___6773 = t___6772.ToSql();
                SqlFragment q__1857 = t___6773;
                bool t___13997 = q__1857.ToString() == "DELETE FROM logs WHERE level = 'debug' LIMIT 1000";
                string fn__13988()
                {
                    return "delete limit";
                }
                test___207.Assert(t___13997, (S1::Func<string>) fn__13988);
            }
            finally
            {
                test___207.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsFirst__2596()
        {
            T::Test test___208 = new T::Test();
            try
            {
                ISafeIdentifier t___13979 = sid__709("users");
                ISafeIdentifier t___13980 = sid__709("email");
                NullsFirst t___13981 = new NullsFirst();
                Query q__1859 = S0::SrcGlobal.From(t___13979).OrderByNulls(t___13980, true, t___13981);
                bool t___13986 = q__1859.ToSql().ToString() == "SELECT * FROM users ORDER BY email ASC NULLS FIRST";
                string fn__13978()
                {
                    return "nulls first";
                }
                test___208.Assert(t___13986, (S1::Func<string>) fn__13978);
            }
            finally
            {
                test___208.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsLast__2597()
        {
            T::Test test___209 = new T::Test();
            try
            {
                ISafeIdentifier t___13969 = sid__709("users");
                ISafeIdentifier t___13970 = sid__709("score");
                NullsLast t___13971 = new NullsLast();
                Query q__1861 = S0::SrcGlobal.From(t___13969).OrderByNulls(t___13970, false, t___13971);
                bool t___13976 = q__1861.ToSql().ToString() == "SELECT * FROM users ORDER BY score DESC NULLS LAST";
                string fn__13968()
                {
                    return "nulls last";
                }
                test___209.Assert(t___13976, (S1::Func<string>) fn__13968);
            }
            finally
            {
                test___209.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedOrderByAndOrderByNulls__2598()
        {
            T::Test test___210 = new T::Test();
            try
            {
                ISafeIdentifier t___13957 = sid__709("users");
                ISafeIdentifier t___13958 = sid__709("name");
                Query q__1863 = S0::SrcGlobal.From(t___13957).OrderBy(t___13958, true).OrderByNulls(sid__709("email"), true, new NullsFirst());
                bool t___13966 = q__1863.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC, email ASC NULLS FIRST";
                string fn__13956()
                {
                    return "mixed order";
                }
                test___210.Assert(t___13966, (S1::Func<string>) fn__13956);
            }
            finally
            {
                test___210.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoin__2599()
        {
            T::Test test___211 = new T::Test();
            try
            {
                ISafeIdentifier t___13948 = sid__709("users");
                ISafeIdentifier t___13949 = sid__709("colors");
                Query q__1865 = S0::SrcGlobal.From(t___13948).CrossJoin(t___13949);
                bool t___13954 = q__1865.ToSql().ToString() == "SELECT * FROM users CROSS JOIN colors";
                string fn__13947()
                {
                    return "cross join";
                }
                test___211.Assert(t___13954, (S1::Func<string>) fn__13947);
            }
            finally
            {
                test___211.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoinCombinedWithOtherJoins__2600()
        {
            T::Test test___212 = new T::Test();
            try
            {
                ISafeIdentifier t___13934 = sid__709("users");
                ISafeIdentifier t___13935 = sid__709("orders");
                SqlBuilder t___13936 = new SqlBuilder();
                t___13936.AppendSafe("users.id = orders.user_id");
                SqlFragment t___13938 = t___13936.Accumulated;
                Query q__1867 = S0::SrcGlobal.From(t___13934).InnerJoin(t___13935, t___13938).CrossJoin(sid__709("colors"));
                bool t___13945 = q__1867.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id CROSS JOIN colors";
                string fn__13933()
                {
                    return "cross + inner join";
                }
                test___212.Assert(t___13945, (S1::Func<string>) fn__13933);
            }
            finally
            {
                test___212.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForUpdate__2602()
        {
            T::Test test___213 = new T::Test();
            try
            {
                ISafeIdentifier t___13920 = sid__709("users");
                SqlBuilder t___13921 = new SqlBuilder();
                t___13921.AppendSafe("id = ");
                t___13921.AppendInt32(1);
                SqlFragment t___13924 = t___13921.Accumulated;
                Query q__1869 = S0::SrcGlobal.From(t___13920).Where(t___13924).Lock(new ForUpdate());
                bool t___13931 = q__1869.ToSql().ToString() == "SELECT * FROM users WHERE id = 1 FOR UPDATE";
                string fn__13919()
                {
                    return "for update";
                }
                test___213.Assert(t___13931, (S1::Func<string>) fn__13919);
            }
            finally
            {
                test___213.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForShare__2604()
        {
            T::Test test___214 = new T::Test();
            try
            {
                ISafeIdentifier t___13909 = sid__709("users");
                ISafeIdentifier t___13910 = sid__709("name");
                Query q__1871 = S0::SrcGlobal.From(t___13909).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___13910)).Lock(new ForShare());
                bool t___13917 = q__1871.ToSql().ToString() == "SELECT name FROM users FOR SHARE";
                string fn__13908()
                {
                    return "for share";
                }
                test___214.Assert(t___13917, (S1::Func<string>) fn__13908);
            }
            finally
            {
                test___214.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockWithFullQuery__2605()
        {
            T::Test test___215 = new T::Test();
            try
            {
                ISafeIdentifier t___13895 = sid__709("accounts");
                SqlBuilder t___13896 = new SqlBuilder();
                t___13896.AppendSafe("id = ");
                t___13896.AppendInt32(42);
                SqlFragment t___13899 = t___13896.Accumulated;
                Query t___6696;
                t___6696 = S0::SrcGlobal.From(t___13895).Where(t___13899).Limit(1);
                Query t___13902 = t___6696.Lock(new ForUpdate());
                Query q__1873 = t___13902;
                bool t___13906 = q__1873.ToSql().ToString() == "SELECT * FROM accounts WHERE id = 42 LIMIT 1 FOR UPDATE";
                string fn__13894()
                {
                    return "lock full query";
                }
                test___215.Assert(t___13906, (S1::Func<string>) fn__13894);
            }
            finally
            {
                test___215.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void queryBuilderImmutabilityTwoQueriesFromSameBase__2607()
        {
            T::Test test___216 = new T::Test();
            try
            {
                ISafeIdentifier t___13878 = sid__709("users");
                SqlBuilder t___13879 = new SqlBuilder();
                t___13879.AppendSafe("active = ");
                t___13879.AppendBoolean(true);
                SqlFragment t___13882 = t___13879.Accumulated;
                Query base__1875 = S0::SrcGlobal.From(t___13878).Where(t___13882);
                Query t___6677;
                t___6677 = base__1875.Limit(10);
                Query q1__1876 = t___6677;
                Query t___6680;
                t___6680 = base__1875.Limit(20);
                Query q2__1877 = t___6680;
                bool t___13887 = q1__1876.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE LIMIT 10";
                string fn__13877()
                {
                    return "q1";
                }
                test___216.Assert(t___13887, (S1::Func<string>) fn__13877);
                bool t___13892 = q2__1877.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE LIMIT 20";
                string fn__13876()
                {
                    return "q2";
                }
                test___216.Assert(t___13892, (S1::Func<string>) fn__13876);
            }
            finally
            {
                test___216.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitZeroProducesLimit0__2609()
        {
            T::Test test___217 = new T::Test();
            try
            {
                Query t___6664;
                t___6664 = S0::SrcGlobal.From(sid__709("users")).Limit(0);
                Query q__1879 = t___6664;
                bool t___13874 = q__1879.ToSql().ToString() == "SELECT * FROM users LIMIT 0";
                string fn__13869()
                {
                    return "limit 0";
                }
                test___217.Assert(t___13874, (S1::Func<string>) fn__13869);
            }
            finally
            {
                test___217.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlWithZeroDefaultLimit__2610()
        {
            T::Test test___218 = new T::Test();
            try
            {
                Query q__1881 = S0::SrcGlobal.From(sid__709("users"));
                SqlFragment t___6658;
                t___6658 = q__1881.SafeToSql(0);
                SqlFragment s__1882 = t___6658;
                bool t___13867 = s__1882.ToString() == "SELECT * FROM users LIMIT 0";
                string fn__13863()
                {
                    return "safeToSql 0";
                }
                test___218.Assert(t___13867, (S1::Func<string>) fn__13863);
            }
            finally
            {
                test___218.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryLimitBubblesOnNegative__2611()
        {
            T::Test test___219 = new T::Test();
            try
            {
                ISafeIdentifier t___13852;
                ISafeIdentifier t___13853;
                SqlString t___13854;
                UpdateQuery t___13855;
                SqlBuilder t___13856;
                bool didBubble__1884;
                try
                {
                    t___13852 = sid__709("users");
                    t___13853 = sid__709("name");
                    t___13854 = new SqlString("x");
                    t___13855 = S0::SrcGlobal.Update(t___13852).Set(t___13853, t___13854);
                    t___13856 = new SqlBuilder();
                    t___13856.AppendSafe("id = ");
                    t___13856.AppendInt32(1);
                    t___13855.Where(t___13856.Accumulated).Limit(-1);
                    didBubble__1884 = false;
                }
                catch
                {
                    didBubble__1884 = true;
                }
                string fn__13851()
                {
                    return "UpdateQuery negative limit should bubble";
                }
                test___219.Assert(didBubble__1884, (S1::Func<string>) fn__13851);
            }
            finally
            {
                test___219.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryLimitBubblesOnNegative__2613()
        {
            T::Test test___220 = new T::Test();
            try
            {
                ISafeIdentifier t___13843;
                SqlBuilder t___13844;
                SqlFragment t___13847;
                bool didBubble__1886;
                try
                {
                    t___13843 = sid__709("users");
                    t___13844 = new SqlBuilder();
                    t___13844.AppendSafe("id = ");
                    t___13844.AppendInt32(1);
                    t___13847 = t___13844.Accumulated;
                    S0::SrcGlobal.DeleteFrom(t___13843).Where(t___13847).Limit(-1);
                    didBubble__1886 = false;
                }
                catch
                {
                    didBubble__1886 = true;
                }
                string fn__13842()
                {
                    return "DeleteQuery negative limit should bubble";
                }
                test___220.Assert(didBubble__1886, (S1::Func<string>) fn__13842);
            }
            finally
            {
                test___220.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryImmutabilityTwoFromSameBase__2615()
        {
            T::Test test___221 = new T::Test();
            try
            {
                ISafeIdentifier t___13812 = sid__709("users");
                ISafeIdentifier t___13813 = sid__709("name");
                SqlString t___13814 = new SqlString("Alice");
                UpdateQuery t___13815 = S0::SrcGlobal.Update(t___13812).Set(t___13813, t___13814);
                SqlBuilder t___13816 = new SqlBuilder();
                t___13816.AppendSafe("id = ");
                t___13816.AppendInt32(1);
                UpdateQuery base__1888 = t___13815.Where(t___13816.Accumulated);
                UpdateQuery q1__1889 = base__1888.Set(sid__709("age"), new SqlInt32(25));
                UpdateQuery q2__1890 = base__1888.Set(sid__709("age"), new SqlInt32(30));
                SqlFragment t___6618;
                t___6618 = q1__1889.ToSql();
                SqlFragment t___6619 = t___6618;
                string s1__1891 = t___6619.ToString();
                SqlFragment t___6621;
                t___6621 = q2__1890.ToSql();
                SqlFragment t___6622 = t___6621;
                string s2__1892 = t___6622.ToString();
                bool t___13830 = s1__1891.IndexOf("25") >= 0;
                string fn__13811()
                {
                    return "q1 should have 25: " + s1__1891;
                }
                test___221.Assert(t___13830, (S1::Func<string>) fn__13811);
                bool t___13834 = s2__1892.IndexOf("30") >= 0;
                string fn__13810()
                {
                    return "q2 should have 30: " + s2__1892;
                }
                test___221.Assert(t___13834, (S1::Func<string>) fn__13810);
                bool t___13840 = !(s1__1891.IndexOf("30") >= 0);
                string fn__13809()
                {
                    return "q1 should NOT have 30: " + s1__1891;
                }
                test___221.Assert(t___13840, (S1::Func<string>) fn__13809);
            }
            finally
            {
                test___221.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryImmutability__2617()
        {
            T::Test test___222 = new T::Test();
            try
            {
                ISafeIdentifier t___13778 = sid__709("users");
                SqlBuilder t___13779 = new SqlBuilder();
                t___13779.AppendSafe("active = ");
                t___13779.AppendBoolean(false);
                SqlFragment t___13782 = t___13779.Accumulated;
                DeleteQuery base__1894 = S0::SrcGlobal.DeleteFrom(t___13778).Where(t___13782);
                SqlBuilder t___13784 = new SqlBuilder();
                t___13784.AppendSafe("age < ");
                t___13784.AppendInt32(18);
                DeleteQuery q1__1895 = base__1894.Where(t___13784.Accumulated);
                SqlBuilder t___13789 = new SqlBuilder();
                t___13789.AppendSafe("age > ");
                t___13789.AppendInt32(65);
                DeleteQuery q2__1896 = base__1894.Where(t___13789.Accumulated);
                SqlFragment t___6584;
                t___6584 = q1__1895.ToSql();
                SqlFragment t___6585 = t___6584;
                string s1__1897 = t___6585.ToString();
                SqlFragment t___6587;
                t___6587 = q2__1896.ToSql();
                SqlFragment t___6588 = t___6587;
                string s2__1898 = t___6588.ToString();
                bool t___13797 = s1__1897.IndexOf("age < 18") >= 0;
                string fn__13777()
                {
                    return "q1: " + s1__1897;
                }
                test___222.Assert(t___13797, (S1::Func<string>) fn__13777);
                bool t___13801 = s2__1898.IndexOf("age > 65") >= 0;
                string fn__13776()
                {
                    return "q2: " + s2__1898;
                }
                test___222.Assert(t___13801, (S1::Func<string>) fn__13776);
                bool t___13807 = !(s1__1897.IndexOf("age > 65") >= 0);
                string fn__13775()
                {
                    return "q1 should not have q2 condition: " + s1__1897;
                }
                test___222.Assert(t___13807, (S1::Func<string>) fn__13775);
            }
            finally
            {
                test___222.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__2621()
        {
            T::Test test___229 = new T::Test();
            try
            {
                ISafeIdentifier t___6561;
                t___6561 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1946 = t___6561;
                bool t___13773 = id__1946.SqlValue == "user_name";
                string fn__13770()
                {
                    return "value should round-trip";
                }
                test___229.Assert(t___13773, (S1::Func<string>) fn__13770);
            }
            finally
            {
                test___229.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__2622()
        {
            T::Test test___230 = new T::Test();
            try
            {
                bool didBubble__1948;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1948 = false;
                }
                catch
                {
                    didBubble__1948 = true;
                }
                string fn__13767()
                {
                    return "empty string should bubble";
                }
                test___230.Assert(didBubble__1948, (S1::Func<string>) fn__13767);
            }
            finally
            {
                test___230.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__2623()
        {
            T::Test test___231 = new T::Test();
            try
            {
                bool didBubble__1950;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1950 = false;
                }
                catch
                {
                    didBubble__1950 = true;
                }
                string fn__13764()
                {
                    return "leading digit should bubble";
                }
                test___231.Assert(didBubble__1950, (S1::Func<string>) fn__13764);
            }
            finally
            {
                test___231.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__2624()
        {
            T::Test test___232 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1952 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__13761(string c__1953)
                {
                    bool didBubble__1954;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1953);
                        didBubble__1954 = false;
                    }
                    catch
                    {
                        didBubble__1954 = true;
                    }
                    string fn__13758()
                    {
                        return "should reject: " + c__1953;
                    }
                    test___232.Assert(didBubble__1954, (S1::Func<string>) fn__13758);
                }
                C::Listed.ForEach(cases__1952, (S1::Action<string>) fn__13761);
            }
            finally
            {
                test___232.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__2625()
        {
            T::Test test___233 = new T::Test();
            try
            {
                ISafeIdentifier t___6538;
                t___6538 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___6539 = t___6538;
                ISafeIdentifier t___6540;
                t___6540 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___6541 = t___6540;
                StringField t___13748 = new StringField();
                FieldDef t___13749 = new FieldDef(t___6541, t___13748, false, null, false);
                ISafeIdentifier t___6544;
                t___6544 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___6545 = t___6544;
                IntField t___13750 = new IntField();
                FieldDef t___13751 = new FieldDef(t___6545, t___13750, false, null, false);
                TableDef td__1956 = new TableDef(t___6539, C::Listed.CreateReadOnlyList<FieldDef>(t___13749, t___13751), null);
                FieldDef t___6549;
                t___6549 = td__1956.Field("age");
                FieldDef f__1957 = t___6549;
                bool t___13756 = f__1957.Name.SqlValue == "age";
                string fn__13747()
                {
                    return "should find age field";
                }
                test___233.Assert(t___13756, (S1::Func<string>) fn__13747);
            }
            finally
            {
                test___233.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__2626()
        {
            T::Test test___234 = new T::Test();
            try
            {
                ISafeIdentifier t___6529;
                t___6529 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___6530 = t___6529;
                ISafeIdentifier t___6531;
                t___6531 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___6532 = t___6531;
                StringField t___13742 = new StringField();
                FieldDef t___13743 = new FieldDef(t___6532, t___13742, false, null, false);
                TableDef td__1959 = new TableDef(t___6530, C::Listed.CreateReadOnlyList<FieldDef>(t___13743), null);
                bool didBubble__1960;
                try
                {
                    td__1959.Field("nonexistent");
                    didBubble__1960 = false;
                }
                catch
                {
                    didBubble__1960 = true;
                }
                string fn__13741()
                {
                    return "unknown field should bubble";
                }
                test___234.Assert(didBubble__1960, (S1::Func<string>) fn__13741);
            }
            finally
            {
                test___234.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__2627()
        {
            T::Test test___235 = new T::Test();
            try
            {
                ISafeIdentifier t___6517;
                t___6517 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___6518 = t___6517;
                StringField t___13730 = new StringField();
                FieldDef required__1962 = new FieldDef(t___6518, t___13730, false, null, false);
                ISafeIdentifier t___6521;
                t___6521 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___6522 = t___6521;
                StringField t___13732 = new StringField();
                FieldDef optional__1963 = new FieldDef(t___6522, t___13732, true, null, false);
                bool t___13736 = !required__1962.Nullable;
                string fn__13729()
                {
                    return "required field should not be nullable";
                }
                test___235.Assert(t___13736, (S1::Func<string>) fn__13729);
                bool t___13738 = optional__1963.Nullable;
                string fn__13728()
                {
                    return "optional field should be nullable";
                }
                test___235.Assert(t___13738, (S1::Func<string>) fn__13728);
            }
            finally
            {
                test___235.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void pkNameDefaultsToIdWhenPrimaryKeyIsNull__2628()
        {
            T::Test test___236 = new T::Test();
            try
            {
                ISafeIdentifier t___6508;
                t___6508 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___6509 = t___6508;
                ISafeIdentifier t___6510;
                t___6510 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___6511 = t___6510;
                StringField t___13721 = new StringField();
                FieldDef t___13722 = new FieldDef(t___6511, t___13721, false, null, false);
                TableDef td__1965 = new TableDef(t___6509, C::Listed.CreateReadOnlyList<FieldDef>(t___13722), null);
                bool t___13726 = td__1965.PkName() == "id";
                string fn__13720()
                {
                    return "default pk should be id";
                }
                test___236.Assert(t___13726, (S1::Func<string>) fn__13720);
            }
            finally
            {
                test___236.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void pkNameReturnsCustomPrimaryKey__2629()
        {
            T::Test test___237 = new T::Test();
            try
            {
                ISafeIdentifier t___6496;
                t___6496 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___6497 = t___6496;
                ISafeIdentifier t___6498;
                t___6498 = S0::SrcGlobal.SafeIdentifier("user_id");
                ISafeIdentifier t___6499 = t___6498;
                IntField t___13713 = new IntField();
                G::IReadOnlyList<FieldDef> t___6504 = C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(t___6499, t___13713, false, null, false));
                ISafeIdentifier t___6502;
                t___6502 = S0::SrcGlobal.SafeIdentifier("user_id");
                ISafeIdentifier t___6503 = t___6502;
                TableDef td__1967 = new TableDef(t___6497, t___6504, t___6503);
                bool t___13718 = td__1967.PkName() == "user_id";
                string fn__13712()
                {
                    return "custom pk should be user_id";
                }
                test___237.Assert(t___13718, (S1::Func<string>) fn__13712);
            }
            finally
            {
                test___237.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void timestampsReturnsTwoDateFieldDefs__2630()
        {
            T::Test test___238 = new T::Test();
            try
            {
                G::IReadOnlyList<FieldDef> t___6472;
                t___6472 = S0::SrcGlobal.Timestamps();
                G::IReadOnlyList<FieldDef> ts__1969 = t___6472;
                bool t___13680 = ts__1969.Count == 2;
                string fn__13677()
                {
                    return "should return 2 fields";
                }
                test___238.Assert(t___13680, (S1::Func<string>) fn__13677);
                bool t___13686 = ts__1969[0].Name.SqlValue == "inserted_at";
                string fn__13676()
                {
                    return "first should be inserted_at";
                }
                test___238.Assert(t___13686, (S1::Func<string>) fn__13676);
                bool t___13692 = ts__1969[1].Name.SqlValue == "updated_at";
                string fn__13675()
                {
                    return "second should be updated_at";
                }
                test___238.Assert(t___13692, (S1::Func<string>) fn__13675);
                bool t___13695 = ts__1969[0].Nullable;
                string fn__13674()
                {
                    return "inserted_at should be nullable";
                }
                test___238.Assert(t___13695, (S1::Func<string>) fn__13674);
                bool t___13699 = ts__1969[1].Nullable;
                string fn__13673()
                {
                    return "updated_at should be nullable";
                }
                test___238.Assert(t___13699, (S1::Func<string>) fn__13673);
                bool t___13705 = !(ts__1969[0].DefaultValue == null);
                string fn__13672()
                {
                    return "inserted_at should have default";
                }
                test___238.Assert(t___13705, (S1::Func<string>) fn__13672);
                bool t___13710 = !(ts__1969[1].DefaultValue == null);
                string fn__13671()
                {
                    return "updated_at should have default";
                }
                test___238.Assert(t___13710, (S1::Func<string>) fn__13671);
            }
            finally
            {
                test___238.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefDefaultValueField__2631()
        {
            T::Test test___239 = new T::Test();
            try
            {
                ISafeIdentifier t___6459;
                t___6459 = S0::SrcGlobal.SafeIdentifier("status");
                ISafeIdentifier t___6460 = t___6459;
                StringField t___13658 = new StringField();
                SqlDefault t___13659 = new SqlDefault();
                FieldDef withDefault__1971 = new FieldDef(t___6460, t___13658, false, t___13659, false);
                ISafeIdentifier t___6464;
                t___6464 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___6465 = t___6464;
                StringField t___13661 = new StringField();
                FieldDef withoutDefault__1972 = new FieldDef(t___6465, t___13661, false, null, false);
                bool t___13665 = !(withDefault__1971.DefaultValue == null);
                string fn__13657()
                {
                    return "should have default";
                }
                test___239.Assert(t___13665, (S1::Func<string>) fn__13657);
                bool t___13669 = withoutDefault__1972.DefaultValue == null;
                string fn__13656()
                {
                    return "should not have default";
                }
                test___239.Assert(t___13669, (S1::Func<string>) fn__13656);
            }
            finally
            {
                test___239.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefVirtualFlag__2632()
        {
            T::Test test___240 = new T::Test();
            try
            {
                ISafeIdentifier t___6447;
                t___6447 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___6448 = t___6447;
                StringField t___13645 = new StringField();
                FieldDef normal__1974 = new FieldDef(t___6448, t___13645, false, null, false);
                ISafeIdentifier t___6451;
                t___6451 = S0::SrcGlobal.SafeIdentifier("full_name");
                ISafeIdentifier t___6452 = t___6451;
                StringField t___13647 = new StringField();
                FieldDef virt__1975 = new FieldDef(t___6452, t___13647, true, null, true);
                bool t___13651 = !normal__1974.Virtual;
                string fn__13644()
                {
                    return "normal field should not be virtual";
                }
                test___240.Assert(t___13651, (S1::Func<string>) fn__13644);
                bool t___13653 = virt__1975.Virtual;
                string fn__13643()
                {
                    return "virtual field should be virtual";
                }
                test___240.Assert(t___13653, (S1::Func<string>) fn__13643);
            }
            finally
            {
                test___240.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsSingleCharacterNames__2633()
        {
            T::Test test___241 = new T::Test();
            try
            {
                ISafeIdentifier t___6439;
                t___6439 = S0::SrcGlobal.SafeIdentifier("a");
                ISafeIdentifier a__1977 = t___6439;
                bool t___13637 = a__1977.SqlValue == "a";
                string fn__13634()
                {
                    return "single letter should work";
                }
                test___241.Assert(t___13637, (S1::Func<string>) fn__13634);
                ISafeIdentifier t___6443;
                t___6443 = S0::SrcGlobal.SafeIdentifier("_");
                ISafeIdentifier u__1978 = t___6443;
                bool t___13641 = u__1978.SqlValue == "_";
                string fn__13633()
                {
                    return "single underscore should work";
                }
                test___241.Assert(t___13641, (S1::Func<string>) fn__13633);
            }
            finally
            {
                test___241.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsAllUnderscoreNames__2634()
        {
            T::Test test___242 = new T::Test();
            try
            {
                ISafeIdentifier t___6435;
                t___6435 = S0::SrcGlobal.SafeIdentifier("___");
                ISafeIdentifier id__1980 = t___6435;
                bool t___13631 = id__1980.SqlValue == "___";
                string fn__13628()
                {
                    return "all underscores should work";
                }
                test___242.Assert(t___13631, (S1::Func<string>) fn__13628);
            }
            finally
            {
                test___242.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefWithEmptyFieldList__2635()
        {
            T::Test test___243 = new T::Test();
            try
            {
                ISafeIdentifier t___6430;
                t___6430 = S0::SrcGlobal.SafeIdentifier("empty");
                ISafeIdentifier t___6431 = t___6430;
                TableDef tbl__1982 = new TableDef(t___6431, C::Listed.CreateReadOnlyList<FieldDef>(), null);
                bool didBubble__1983;
                try
                {
                    tbl__1982.Field("anything");
                    didBubble__1983 = false;
                }
                catch
                {
                    didBubble__1983 = true;
                }
                string fn__13624()
                {
                    return "field lookup on empty table should bubble";
                }
                test___243.Assert(didBubble__1983, (S1::Func<string>) fn__13624);
            }
            finally
            {
                test___243.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__2636()
        {
            T::Test test___247 = new T::Test();
            try
            {
                string build__2113(string name__2115)
                {
                    SqlBuilder t___13606 = new SqlBuilder();
                    t___13606.AppendSafe("select * from hi where name = ");
                    t___13606.AppendString(name__2115);
                    return t___13606.Accumulated.ToString();
                }
                string buildWrong__2114(string name__2117)
                {
                    return "select * from hi where name = '" + name__2117 + "'";
                }
                string actual___2638 = build__2113("world");
                bool t___13616 = actual___2638 == "select * from hi where name = 'world'";
                string fn__13613()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___2638 + ")";
                }
                test___247.Assert(t___13616, (S1::Func<string>) fn__13613);
                string bobbyTables__2119 = "Robert'); drop table hi;--";
                string actual___2640 = build__2113("Robert'); drop table hi;--");
                bool t___13620 = actual___2640 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__13612()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___2640 + ")";
                }
                test___247.Assert(t___13620, (S1::Func<string>) fn__13612);
                string fn__13611()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___247.Assert(true, (S1::Func<string>) fn__13611);
            }
            finally
            {
                test___247.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__2644()
        {
            T::Test test___248 = new T::Test();
            try
            {
                SqlBuilder t___13574 = new SqlBuilder();
                t___13574.AppendSafe("v = ");
                t___13574.AppendString("");
                string actual___2645 = t___13574.Accumulated.ToString();
                bool t___13580 = actual___2645 == "v = ''";
                string fn__13573()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___2645 + ")";
                }
                test___248.Assert(t___13580, (S1::Func<string>) fn__13573);
                SqlBuilder t___13582 = new SqlBuilder();
                t___13582.AppendSafe("v = ");
                t___13582.AppendString("a''b");
                string actual___2648 = t___13582.Accumulated.ToString();
                bool t___13588 = actual___2648 == "v = 'a''''b'";
                string fn__13572()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___2648 + ")";
                }
                test___248.Assert(t___13588, (S1::Func<string>) fn__13572);
                SqlBuilder t___13590 = new SqlBuilder();
                t___13590.AppendSafe("v = ");
                t___13590.AppendString("Hello 世界");
                string actual___2651 = t___13590.Accumulated.ToString();
                bool t___13596 = actual___2651 == "v = 'Hello 世界'";
                string fn__13571()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___2651 + ")";
                }
                test___248.Assert(t___13596, (S1::Func<string>) fn__13571);
                SqlBuilder t___13598 = new SqlBuilder();
                t___13598.AppendSafe("v = ");
                t___13598.AppendString("Line1\nLine2");
                string actual___2654 = t___13598.Accumulated.ToString();
                bool t___13604 = actual___2654 == "v = 'Line1\nLine2'";
                string fn__13570()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___2654 + ")";
                }
                test___248.Assert(t___13604, (S1::Func<string>) fn__13570);
            }
            finally
            {
                test___248.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__2657()
        {
            T::Test test___249 = new T::Test();
            try
            {
                SqlBuilder t___13545 = new SqlBuilder();
                t___13545.AppendSafe("select ");
                t___13545.AppendInt32(42);
                t___13545.AppendSafe(", ");
                t___13545.AppendInt64(43);
                t___13545.AppendSafe(", ");
                t___13545.AppendFloat64(19.99);
                t___13545.AppendSafe(", ");
                t___13545.AppendBoolean(true);
                t___13545.AppendSafe(", ");
                t___13545.AppendBoolean(false);
                string actual___2658 = t___13545.Accumulated.ToString();
                bool t___13559 = actual___2658 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__13544()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___2658 + ")";
                }
                test___249.Assert(t___13559, (S1::Func<string>) fn__13544);
                S1::DateTime t___6375;
                t___6375 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__2122 = t___6375;
                SqlBuilder t___13561 = new SqlBuilder();
                t___13561.AppendSafe("insert into t values (");
                t___13561.AppendDate(date__2122);
                t___13561.AppendSafe(")");
                string actual___2661 = t___13561.Accumulated.ToString();
                bool t___13568 = actual___2661 == "insert into t values ('2024-12-25')";
                string fn__13543()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___2661 + ")";
                }
                test___249.Assert(t___13568, (S1::Func<string>) fn__13543);
            }
            finally
            {
                test___249.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__2664()
        {
            T::Test test___250 = new T::Test();
            try
            {
                SqlBuilder t___13489 = new SqlBuilder();
                t___13489.AppendSafe("v IN (");
                t___13489.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___13489.AppendSafe(")");
                string actual___2665 = t___13489.Accumulated.ToString();
                bool t___13496 = actual___2665 == "v IN ('a', 'b', 'c''d')";
                string fn__13488()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___2665 + ")";
                }
                test___250.Assert(t___13496, (S1::Func<string>) fn__13488);
                SqlBuilder t___13498 = new SqlBuilder();
                t___13498.AppendSafe("v IN (");
                t___13498.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___13498.AppendSafe(")");
                string actual___2668 = t___13498.Accumulated.ToString();
                bool t___13505 = actual___2668 == "v IN (1, 2, 3)";
                string fn__13487()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___2668 + ")";
                }
                test___250.Assert(t___13505, (S1::Func<string>) fn__13487);
                SqlBuilder t___13507 = new SqlBuilder();
                t___13507.AppendSafe("v IN (");
                t___13507.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___13507.AppendSafe(")");
                string actual___2671 = t___13507.Accumulated.ToString();
                bool t___13514 = actual___2671 == "v IN (1, 2)";
                string fn__13486()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___2671 + ")";
                }
                test___250.Assert(t___13514, (S1::Func<string>) fn__13486);
                SqlBuilder t___13516 = new SqlBuilder();
                t___13516.AppendSafe("v IN (");
                t___13516.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___13516.AppendSafe(")");
                string actual___2674 = t___13516.Accumulated.ToString();
                bool t___13523 = actual___2674 == "v IN (1.0, 2.0)";
                string fn__13485()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___2674 + ")";
                }
                test___250.Assert(t___13523, (S1::Func<string>) fn__13485);
                SqlBuilder t___13525 = new SqlBuilder();
                t___13525.AppendSafe("v IN (");
                t___13525.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___13525.AppendSafe(")");
                string actual___2677 = t___13525.Accumulated.ToString();
                bool t___13532 = actual___2677 == "v IN (TRUE, FALSE)";
                string fn__13484()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___2677 + ")";
                }
                test___250.Assert(t___13532, (S1::Func<string>) fn__13484);
                S1::DateTime t___6347;
                t___6347 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___6348 = t___6347;
                S1::DateTime t___6349;
                t___6349 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___6350 = t___6349;
                G::IReadOnlyList<S1::DateTime> dates__2124 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___6348, t___6350);
                SqlBuilder t___13534 = new SqlBuilder();
                t___13534.AppendSafe("v IN (");
                t___13534.AppendDateList(dates__2124);
                t___13534.AppendSafe(")");
                string actual___2680 = t___13534.Accumulated.ToString();
                bool t___13541 = actual___2680 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__13483()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___2680 + ")";
                }
                test___250.Assert(t___13541, (S1::Func<string>) fn__13483);
            }
            finally
            {
                test___250.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__2683()
        {
            T::Test test___251 = new T::Test();
            try
            {
                double nan__2126;
                nan__2126 = 0.0 / 0.0;
                SqlBuilder t___13475 = new SqlBuilder();
                t___13475.AppendSafe("v = ");
                t___13475.AppendFloat64(nan__2126);
                string actual___2684 = t___13475.Accumulated.ToString();
                bool t___13481 = actual___2684 == "v = NULL";
                string fn__13474()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___2684 + ")";
                }
                test___251.Assert(t___13481, (S1::Func<string>) fn__13474);
            }
            finally
            {
                test___251.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__2687()
        {
            T::Test test___252 = new T::Test();
            try
            {
                double inf__2128;
                inf__2128 = 1.0 / 0.0;
                SqlBuilder t___13466 = new SqlBuilder();
                t___13466.AppendSafe("v = ");
                t___13466.AppendFloat64(inf__2128);
                string actual___2688 = t___13466.Accumulated.ToString();
                bool t___13472 = actual___2688 == "v = NULL";
                string fn__13465()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___2688 + ")";
                }
                test___252.Assert(t___13472, (S1::Func<string>) fn__13465);
            }
            finally
            {
                test___252.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__2691()
        {
            T::Test test___253 = new T::Test();
            try
            {
                double ninf__2130;
                ninf__2130 = -1.0 / 0.0;
                SqlBuilder t___13457 = new SqlBuilder();
                t___13457.AppendSafe("v = ");
                t___13457.AppendFloat64(ninf__2130);
                string actual___2692 = t___13457.Accumulated.ToString();
                bool t___13463 = actual___2692 == "v = NULL";
                string fn__13456()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___2692 + ")";
                }
                test___253.Assert(t___13463, (S1::Func<string>) fn__13456);
            }
            finally
            {
                test___253.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__2695()
        {
            T::Test test___254 = new T::Test();
            try
            {
                SqlBuilder t___13432 = new SqlBuilder();
                t___13432.AppendSafe("v = ");
                t___13432.AppendFloat64(3.14);
                string actual___2696 = t___13432.Accumulated.ToString();
                bool t___13438 = actual___2696 == "v = 3.14";
                string fn__13431()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___2696 + ")";
                }
                test___254.Assert(t___13438, (S1::Func<string>) fn__13431);
                SqlBuilder t___13440 = new SqlBuilder();
                t___13440.AppendSafe("v = ");
                t___13440.AppendFloat64(0.0);
                string actual___2699 = t___13440.Accumulated.ToString();
                bool t___13446 = actual___2699 == "v = 0.0";
                string fn__13430()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___2699 + ")";
                }
                test___254.Assert(t___13446, (S1::Func<string>) fn__13430);
                SqlBuilder t___13448 = new SqlBuilder();
                t___13448.AppendSafe("v = ");
                t___13448.AppendFloat64(-42.5);
                string actual___2702 = t___13448.Accumulated.ToString();
                bool t___13454 = actual___2702 == "v = -42.5";
                string fn__13429()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___2702 + ")";
                }
                test___254.Assert(t___13454, (S1::Func<string>) fn__13429);
            }
            finally
            {
                test___254.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__2705()
        {
            T::Test test___255 = new T::Test();
            try
            {
                S1::DateTime t___6243;
                t___6243 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__2133 = t___6243;
                SqlBuilder t___13421 = new SqlBuilder();
                t___13421.AppendSafe("v = ");
                t___13421.AppendDate(d__2133);
                string actual___2706 = t___13421.Accumulated.ToString();
                bool t___13427 = actual___2706 == "v = '2024-06-15'";
                string fn__13420()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___2706 + ")";
                }
                test___255.Assert(t___13427, (S1::Func<string>) fn__13420);
            }
            finally
            {
                test___255.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__2709()
        {
            T::Test test___256 = new T::Test();
            try
            {
                string name__2135 = "Someone";
                SqlBuilder t___13389 = new SqlBuilder();
                t___13389.AppendSafe("where p.last_name = ");
                t___13389.AppendString("Someone");
                SqlFragment condition__2136 = t___13389.Accumulated;
                SqlBuilder t___13393 = new SqlBuilder();
                t___13393.AppendSafe("select p.id from person p ");
                t___13393.AppendFragment(condition__2136);
                string actual___2711 = t___13393.Accumulated.ToString();
                bool t___13399 = actual___2711 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__13388()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2711 + ")";
                }
                test___256.Assert(t___13399, (S1::Func<string>) fn__13388);
                SqlBuilder t___13401 = new SqlBuilder();
                t___13401.AppendSafe("select p.id from person p ");
                t___13401.AppendPart(condition__2136.ToSource());
                string actual___2714 = t___13401.Accumulated.ToString();
                bool t___13408 = actual___2714 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__13387()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2714 + ")";
                }
                test___256.Assert(t___13408, (S1::Func<string>) fn__13387);
                G::IReadOnlyList<ISqlPart> parts__2137 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___13412 = new SqlBuilder();
                t___13412.AppendSafe("select ");
                t___13412.AppendPartList(parts__2137);
                string actual___2717 = t___13412.Accumulated.ToString();
                bool t___13418 = actual___2717 == "select 'a''b', 3";
                string fn__13386()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___2717 + ")";
                }
                test___256.Assert(t___13418, (S1::Func<string>) fn__13386);
            }
            finally
            {
                test___256.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlInt32_negativeAndZeroValues__2720()
        {
            T::Test test___257 = new T::Test();
            try
            {
                SqlBuilder t___13370 = new SqlBuilder();
                t___13370.AppendSafe("v = ");
                t___13370.AppendInt32(-42);
                bool t___13376 = t___13370.Accumulated.ToString() == "v = -42";
                string fn__13369()
                {
                    return "negative int";
                }
                test___257.Assert(t___13376, (S1::Func<string>) fn__13369);
                SqlBuilder t___13378 = new SqlBuilder();
                t___13378.AppendSafe("v = ");
                t___13378.AppendInt32(0);
                bool t___13384 = t___13378.Accumulated.ToString() == "v = 0";
                string fn__13368()
                {
                    return "zero int";
                }
                test___257.Assert(t___13384, (S1::Func<string>) fn__13368);
            }
            finally
            {
                test___257.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlInt64_negativeValue__2723()
        {
            T::Test test___258 = new T::Test();
            try
            {
                SqlBuilder t___13360 = new SqlBuilder();
                t___13360.AppendSafe("v = ");
                t___13360.AppendInt64(-99);
                bool t___13366 = t___13360.Accumulated.ToString() == "v = -99";
                string fn__13359()
                {
                    return "negative int64";
                }
                test___258.Assert(t___13366, (S1::Func<string>) fn__13359);
            }
            finally
            {
                test___258.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void singleElementListRendering__2725()
        {
            T::Test test___259 = new T::Test();
            try
            {
                SqlBuilder t___13341 = new SqlBuilder();
                t___13341.AppendSafe("v IN (");
                t___13341.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(42));
                t___13341.AppendSafe(")");
                bool t___13348 = t___13341.Accumulated.ToString() == "v IN (42)";
                string fn__13340()
                {
                    return "single int";
                }
                test___259.Assert(t___13348, (S1::Func<string>) fn__13340);
                SqlBuilder t___13350 = new SqlBuilder();
                t___13350.AppendSafe("v IN (");
                t___13350.AppendStringList(C::Listed.CreateReadOnlyList<string>("only"));
                t___13350.AppendSafe(")");
                bool t___13357 = t___13350.Accumulated.ToString() == "v IN ('only')";
                string fn__13339()
                {
                    return "single string";
                }
                test___259.Assert(t___13357, (S1::Func<string>) fn__13339);
            }
            finally
            {
                test___259.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDefaultRendersDefaultKeyword__2728()
        {
            T::Test test___260 = new T::Test();
            try
            {
                SqlBuilder b__2142 = new SqlBuilder();
                b__2142.AppendSafe("v = ");
                b__2142.AppendPart(new SqlDefault());
                bool t___13337 = b__2142.Accumulated.ToString() == "v = DEFAULT";
                string fn__13329()
                {
                    return "default keyword";
                }
                test___260.Assert(t___13337, (S1::Func<string>) fn__13329);
            }
            finally
            {
                test___260.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlStringWithBackslash__2729()
        {
            T::Test test___261 = new T::Test();
            try
            {
                SqlBuilder t___13321 = new SqlBuilder();
                t___13321.AppendSafe("v = ");
                t___13321.AppendString("a\\b");
                bool t___13327 = t___13321.Accumulated.ToString() == "v = 'a\\b'";
                string fn__13320()
                {
                    return "backslash passthrough";
                }
                test___261.Assert(t___13327, (S1::Func<string>) fn__13320);
            }
            finally
            {
                test___261.SoftFailToHard();
            }
        }
    }
}
