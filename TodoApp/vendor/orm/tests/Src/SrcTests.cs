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
        internal static ISafeIdentifier csid__398(string name__543)
        {
            ISafeIdentifier t___3969;
            t___3969 = S0::SrcGlobal.SafeIdentifier(name__543);
            return t___3969;
        }
        internal static TableDef userTable__399()
        {
            return new TableDef(csid__398("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__398("name"), new StringField(), false), new FieldDef(csid__398("email"), new StringField(), false), new FieldDef(csid__398("age"), new IntField(), true), new FieldDef(csid__398("score"), new FloatField(), true), new FieldDef(csid__398("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__1178()
        {
            T::Test test___22 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__547 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___6946 = userTable__399();
                ISafeIdentifier t___6947 = csid__398("name");
                ISafeIdentifier t___6948 = csid__398("email");
                IChangeset cs__548 = S0::SrcGlobal.Changeset(t___6946, params__547).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6947, t___6948));
                bool t___6951 = C::Mapped.ContainsKey(cs__548.Changes, "name");
                string fn__6941()
                {
                    return "name should be in changes";
                }
                test___22.Assert(t___6951, (S1::Func<string>) fn__6941);
                bool t___6955 = C::Mapped.ContainsKey(cs__548.Changes, "email");
                string fn__6940()
                {
                    return "email should be in changes";
                }
                test___22.Assert(t___6955, (S1::Func<string>) fn__6940);
                bool t___6961 = !C::Mapped.ContainsKey(cs__548.Changes, "admin");
                string fn__6939()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___22.Assert(t___6961, (S1::Func<string>) fn__6939);
                bool t___6963 = cs__548.IsValid;
                string fn__6938()
                {
                    return "should still be valid";
                }
                test___22.Assert(t___6963, (S1::Func<string>) fn__6938);
            }
            finally
            {
                test___22.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__1179()
        {
            T::Test test___23 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__550 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___6924 = userTable__399();
                ISafeIdentifier t___6925 = csid__398("name");
                IChangeset cs__551 = S0::SrcGlobal.Changeset(t___6924, params__550).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6925)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("email")));
                bool t___6932 = !C::Mapped.ContainsKey(cs__551.Changes, "name");
                string fn__6920()
                {
                    return "name must be excluded by second cast";
                }
                test___23.Assert(t___6932, (S1::Func<string>) fn__6920);
                bool t___6935 = C::Mapped.ContainsKey(cs__551.Changes, "email");
                string fn__6919()
                {
                    return "email should be present";
                }
                test___23.Assert(t___6935, (S1::Func<string>) fn__6919);
            }
            finally
            {
                test___23.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__1180()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__553 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___6906 = userTable__399();
                ISafeIdentifier t___6907 = csid__398("name");
                ISafeIdentifier t___6908 = csid__398("email");
                IChangeset cs__554 = S0::SrcGlobal.Changeset(t___6906, params__553).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6907, t___6908));
                bool t___6913 = !C::Mapped.ContainsKey(cs__554.Changes, "name");
                string fn__6902()
                {
                    return "empty name should not be in changes";
                }
                test___24.Assert(t___6913, (S1::Func<string>) fn__6902);
                bool t___6916 = C::Mapped.ContainsKey(cs__554.Changes, "email");
                string fn__6901()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___6916, (S1::Func<string>) fn__6901);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__1181()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__556 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___6888 = userTable__399();
                ISafeIdentifier t___6889 = csid__398("name");
                IChangeset cs__557 = S0::SrcGlobal.Changeset(t___6888, params__556).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6889)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name")));
                bool t___6893 = cs__557.IsValid;
                string fn__6885()
                {
                    return "should be valid";
                }
                test___25.Assert(t___6893, (S1::Func<string>) fn__6885);
                bool t___6899 = cs__557.Errors.Count == 0;
                string fn__6884()
                {
                    return "no errors expected";
                }
                test___25.Assert(t___6899, (S1::Func<string>) fn__6884);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__1182()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__559 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___6864 = userTable__399();
                ISafeIdentifier t___6865 = csid__398("name");
                IChangeset cs__560 = S0::SrcGlobal.Changeset(t___6864, params__559).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6865)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name")));
                bool t___6871 = !cs__560.IsValid;
                string fn__6862()
                {
                    return "should be invalid";
                }
                test___26.Assert(t___6871, (S1::Func<string>) fn__6862);
                bool t___6876 = cs__560.Errors.Count == 1;
                string fn__6861()
                {
                    return "should have one error";
                }
                test___26.Assert(t___6876, (S1::Func<string>) fn__6861);
                bool t___6882 = cs__560.Errors[0].Field == "name";
                string fn__6860()
                {
                    return "error should name the field";
                }
                test___26.Assert(t___6882, (S1::Func<string>) fn__6860);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__1183()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__562 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___6852 = userTable__399();
                ISafeIdentifier t___6853 = csid__398("name");
                IChangeset cs__563 = S0::SrcGlobal.Changeset(t___6852, params__562).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6853)).ValidateLength(csid__398("name"), 2, 50);
                bool t___6857 = cs__563.IsValid;
                string fn__6849()
                {
                    return "should be valid";
                }
                test___27.Assert(t___6857, (S1::Func<string>) fn__6849);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__1184()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__565 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___6840 = userTable__399();
                ISafeIdentifier t___6841 = csid__398("name");
                IChangeset cs__566 = S0::SrcGlobal.Changeset(t___6840, params__565).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6841)).ValidateLength(csid__398("name"), 2, 50);
                bool t___6847 = !cs__566.IsValid;
                string fn__6837()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___6847, (S1::Func<string>) fn__6837);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__1185()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__568 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___6828 = userTable__399();
                ISafeIdentifier t___6829 = csid__398("name");
                IChangeset cs__569 = S0::SrcGlobal.Changeset(t___6828, params__568).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6829)).ValidateLength(csid__398("name"), 2, 10);
                bool t___6835 = !cs__569.IsValid;
                string fn__6825()
                {
                    return "should be invalid";
                }
                test___29.Assert(t___6835, (S1::Func<string>) fn__6825);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__1186()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__571 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___6817 = userTable__399();
                ISafeIdentifier t___6818 = csid__398("age");
                IChangeset cs__572 = S0::SrcGlobal.Changeset(t___6817, params__571).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6818)).ValidateInt(csid__398("age"));
                bool t___6822 = cs__572.IsValid;
                string fn__6814()
                {
                    return "should be valid";
                }
                test___30.Assert(t___6822, (S1::Func<string>) fn__6814);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__1187()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__574 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___6805 = userTable__399();
                ISafeIdentifier t___6806 = csid__398("age");
                IChangeset cs__575 = S0::SrcGlobal.Changeset(t___6805, params__574).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6806)).ValidateInt(csid__398("age"));
                bool t___6812 = !cs__575.IsValid;
                string fn__6802()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___6812, (S1::Func<string>) fn__6802);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__1188()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__577 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___6794 = userTable__399();
                ISafeIdentifier t___6795 = csid__398("score");
                IChangeset cs__578 = S0::SrcGlobal.Changeset(t___6794, params__577).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6795)).ValidateFloat(csid__398("score"));
                bool t___6799 = cs__578.IsValid;
                string fn__6791()
                {
                    return "should be valid";
                }
                test___32.Assert(t___6799, (S1::Func<string>) fn__6791);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__1189()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__580 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___6783 = userTable__399();
                ISafeIdentifier t___6784 = csid__398("age");
                IChangeset cs__581 = S0::SrcGlobal.Changeset(t___6783, params__580).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6784)).ValidateInt64(csid__398("age"));
                bool t___6788 = cs__581.IsValid;
                string fn__6780()
                {
                    return "should be valid";
                }
                test___33.Assert(t___6788, (S1::Func<string>) fn__6780);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__1190()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__583 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___6771 = userTable__399();
                ISafeIdentifier t___6772 = csid__398("age");
                IChangeset cs__584 = S0::SrcGlobal.Changeset(t___6771, params__583).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6772)).ValidateInt64(csid__398("age"));
                bool t___6778 = !cs__584.IsValid;
                string fn__6768()
                {
                    return "should be invalid";
                }
                test___34.Assert(t___6778, (S1::Func<string>) fn__6768);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__1191()
        {
            T::Test test___35 = new T::Test();
            try
            {
                void fn__6765(string v__586)
                {
                    G::IReadOnlyDictionary<string, string> params__587 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__586)));
                    TableDef t___6757 = userTable__399();
                    ISafeIdentifier t___6758 = csid__398("active");
                    IChangeset cs__588 = S0::SrcGlobal.Changeset(t___6757, params__587).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6758)).ValidateBool(csid__398("active"));
                    bool t___6762 = cs__588.IsValid;
                    string fn__6754()
                    {
                        return "should accept: " + v__586;
                    }
                    test___35.Assert(t___6762, (S1::Func<string>) fn__6754);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__6765);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__1192()
        {
            T::Test test___36 = new T::Test();
            try
            {
                void fn__6751(string v__590)
                {
                    G::IReadOnlyDictionary<string, string> params__591 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__590)));
                    TableDef t___6743 = userTable__399();
                    ISafeIdentifier t___6744 = csid__398("active");
                    IChangeset cs__592 = S0::SrcGlobal.Changeset(t___6743, params__591).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6744)).ValidateBool(csid__398("active"));
                    bool t___6748 = cs__592.IsValid;
                    string fn__6740()
                    {
                        return "should accept: " + v__590;
                    }
                    test___36.Assert(t___6748, (S1::Func<string>) fn__6740);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__6751);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__1193()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__6737(string v__594)
                {
                    G::IReadOnlyDictionary<string, string> params__595 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__594)));
                    TableDef t___6728 = userTable__399();
                    ISafeIdentifier t___6729 = csid__398("active");
                    IChangeset cs__596 = S0::SrcGlobal.Changeset(t___6728, params__595).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6729)).ValidateBool(csid__398("active"));
                    bool t___6735 = !cs__596.IsValid;
                    string fn__6725()
                    {
                        return "should reject ambiguous: " + v__594;
                    }
                    test___37.Assert(t___6735, (S1::Func<string>) fn__6725);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__6737);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__1194()
        {
            T::Test test___38 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__598 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___6713 = userTable__399();
                ISafeIdentifier t___6714 = csid__398("name");
                ISafeIdentifier t___6715 = csid__398("email");
                IChangeset cs__599 = S0::SrcGlobal.Changeset(t___6713, params__598).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6714, t___6715)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name"), csid__398("email")));
                SqlFragment t___3770;
                t___3770 = cs__599.ToInsertSql();
                SqlFragment sqlFrag__600 = t___3770;
                string s__601 = sqlFrag__600.ToString();
                bool t___6722 = s__601.IndexOf("''") >= 0;
                string fn__6709()
                {
                    return "single quote must be doubled: " + s__601;
                }
                test___38.Assert(t___6722, (S1::Func<string>) fn__6709);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__1195()
        {
            T::Test test___39 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__603 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___6693 = userTable__399();
                ISafeIdentifier t___6694 = csid__398("name");
                ISafeIdentifier t___6695 = csid__398("email");
                IChangeset cs__604 = S0::SrcGlobal.Changeset(t___6693, params__603).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6694, t___6695)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name"), csid__398("email")));
                SqlFragment t___3749;
                t___3749 = cs__604.ToInsertSql();
                SqlFragment sqlFrag__605 = t___3749;
                string s__606 = sqlFrag__605.ToString();
                bool t___6702 = s__606.IndexOf("INSERT INTO users") >= 0;
                string fn__6689()
                {
                    return "has INSERT INTO: " + s__606;
                }
                test___39.Assert(t___6702, (S1::Func<string>) fn__6689);
                bool t___6706 = s__606.IndexOf("'Alice'") >= 0;
                string fn__6688()
                {
                    return "has quoted name: " + s__606;
                }
                test___39.Assert(t___6706, (S1::Func<string>) fn__6688);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__1196()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__608 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___6675 = userTable__399();
                ISafeIdentifier t___6676 = csid__398("name");
                ISafeIdentifier t___6677 = csid__398("email");
                ISafeIdentifier t___6678 = csid__398("age");
                IChangeset cs__609 = S0::SrcGlobal.Changeset(t___6675, params__608).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6676, t___6677, t___6678)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name"), csid__398("email")));
                SqlFragment t___3732;
                t___3732 = cs__609.ToInsertSql();
                SqlFragment sqlFrag__610 = t___3732;
                string s__611 = sqlFrag__610.ToString();
                bool t___6685 = s__611.IndexOf("25") >= 0;
                string fn__6670()
                {
                    return "age rendered unquoted: " + s__611;
                }
                test___40.Assert(t___6685, (S1::Func<string>) fn__6670);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__1197()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__613 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___6663 = userTable__399();
                ISafeIdentifier t___6664 = csid__398("name");
                IChangeset cs__614 = S0::SrcGlobal.Changeset(t___6663, params__613).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6664)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name")));
                bool didBubble__615;
                try
                {
                    cs__614.ToInsertSql();
                    didBubble__615 = false;
                }
                catch
                {
                    didBubble__615 = true;
                }
                string fn__6661()
                {
                    return "invalid changeset should bubble";
                }
                test___41.Assert(didBubble__615, (S1::Func<string>) fn__6661);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__1198()
        {
            T::Test test___42 = new T::Test();
            try
            {
                TableDef strictTable__617 = new TableDef(csid__398("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__398("title"), new StringField(), false), new FieldDef(csid__398("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__618 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___6654 = csid__398("body");
                IChangeset cs__619 = S0::SrcGlobal.Changeset(strictTable__617, params__618).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6654));
                bool t___6656 = cs__619.IsValid;
                string fn__6643()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___42.Assert(t___6656, (S1::Func<string>) fn__6643);
                bool didBubble__620;
                try
                {
                    cs__619.ToInsertSql();
                    didBubble__620 = false;
                }
                catch
                {
                    didBubble__620 = true;
                }
                string fn__6642()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___42.Assert(didBubble__620, (S1::Func<string>) fn__6642);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__1199()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__622 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___6633 = userTable__399();
                ISafeIdentifier t___6634 = csid__398("name");
                IChangeset cs__623 = S0::SrcGlobal.Changeset(t___6633, params__622).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6634)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name")));
                SqlFragment t___3692;
                t___3692 = cs__623.ToUpdateSql(42);
                SqlFragment sqlFrag__624 = t___3692;
                string s__625 = sqlFrag__624.ToString();
                bool t___6640 = s__625 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__6630()
                {
                    return "got: " + s__625;
                }
                test___43.Assert(t___6640, (S1::Func<string>) fn__6630);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__1200()
        {
            T::Test test___44 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__627 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___6623 = userTable__399();
                ISafeIdentifier t___6624 = csid__398("name");
                IChangeset cs__628 = S0::SrcGlobal.Changeset(t___6623, params__627).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6624)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__398("name")));
                bool didBubble__629;
                try
                {
                    cs__628.ToUpdateSql(1);
                    didBubble__629 = false;
                }
                catch
                {
                    didBubble__629 = true;
                }
                string fn__6621()
                {
                    return "invalid changeset should bubble";
                }
                test___44.Assert(didBubble__629, (S1::Func<string>) fn__6621);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__400(string name__791)
        {
            ISafeIdentifier t___3489;
            t___3489 = S0::SrcGlobal.SafeIdentifier(name__791);
            return t___3489;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__1237()
        {
            T::Test test___45 = new T::Test();
            try
            {
                Query q__794 = S0::SrcGlobal.From(sid__400("users"));
                bool t___6444 = q__794.ToSql().ToString() == "SELECT * FROM users";
                string fn__6439()
                {
                    return "bare query";
                }
                test___45.Assert(t___6444, (S1::Func<string>) fn__6439);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__1238()
        {
            T::Test test___46 = new T::Test();
            try
            {
                ISafeIdentifier t___6430 = sid__400("users");
                ISafeIdentifier t___6431 = sid__400("id");
                ISafeIdentifier t___6432 = sid__400("name");
                Query q__796 = S0::SrcGlobal.From(t___6430).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6431, t___6432));
                bool t___6437 = q__796.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__6429()
                {
                    return "select columns";
                }
                test___46.Assert(t___6437, (S1::Func<string>) fn__6429);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__1239()
        {
            T::Test test___47 = new T::Test();
            try
            {
                ISafeIdentifier t___6418 = sid__400("users");
                SqlBuilder t___6419 = new SqlBuilder();
                t___6419.AppendSafe("age > ");
                t___6419.AppendInt32(18);
                SqlFragment t___6422 = t___6419.Accumulated;
                Query q__798 = S0::SrcGlobal.From(t___6418).Where(t___6422);
                bool t___6427 = q__798.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__6417()
                {
                    return "where int";
                }
                test___47.Assert(t___6427, (S1::Func<string>) fn__6417);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__1241()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___6406 = sid__400("users");
                SqlBuilder t___6407 = new SqlBuilder();
                t___6407.AppendSafe("active = ");
                t___6407.AppendBoolean(true);
                SqlFragment t___6410 = t___6407.Accumulated;
                Query q__800 = S0::SrcGlobal.From(t___6406).Where(t___6410);
                bool t___6415 = q__800.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__6405()
                {
                    return "where bool";
                }
                test___48.Assert(t___6415, (S1::Func<string>) fn__6405);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__1243()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___6389 = sid__400("users");
                SqlBuilder t___6390 = new SqlBuilder();
                t___6390.AppendSafe("age > ");
                t___6390.AppendInt32(18);
                SqlFragment t___6393 = t___6390.Accumulated;
                Query t___6394 = S0::SrcGlobal.From(t___6389).Where(t___6393);
                SqlBuilder t___6395 = new SqlBuilder();
                t___6395.AppendSafe("active = ");
                t___6395.AppendBoolean(true);
                Query q__802 = t___6394.Where(t___6395.Accumulated);
                bool t___6403 = q__802.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__6388()
                {
                    return "chained where";
                }
                test___49.Assert(t___6403, (S1::Func<string>) fn__6388);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__1246()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___6380 = sid__400("users");
                ISafeIdentifier t___6381 = sid__400("name");
                Query q__804 = S0::SrcGlobal.From(t___6380).OrderBy(t___6381, true);
                bool t___6386 = q__804.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__6379()
                {
                    return "order asc";
                }
                test___50.Assert(t___6386, (S1::Func<string>) fn__6379);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__1247()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___6371 = sid__400("users");
                ISafeIdentifier t___6372 = sid__400("created_at");
                Query q__806 = S0::SrcGlobal.From(t___6371).OrderBy(t___6372, false);
                bool t___6377 = q__806.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__6370()
                {
                    return "order desc";
                }
                test___51.Assert(t___6377, (S1::Func<string>) fn__6370);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__1248()
        {
            T::Test test___52 = new T::Test();
            try
            {
                Query t___3423;
                t___3423 = S0::SrcGlobal.From(sid__400("users")).Limit(10);
                Query t___3424;
                t___3424 = t___3423.Offset(20);
                Query q__808 = t___3424;
                bool t___6368 = q__808.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__6363()
                {
                    return "limit/offset";
                }
                test___52.Assert(t___6368, (S1::Func<string>) fn__6363);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__1249()
        {
            T::Test test___53 = new T::Test();
            try
            {
                bool didBubble__810;
                try
                {
                    S0::SrcGlobal.From(sid__400("users")).Limit(-1);
                    didBubble__810 = false;
                }
                catch
                {
                    didBubble__810 = true;
                }
                string fn__6359()
                {
                    return "negative limit should bubble";
                }
                test___53.Assert(didBubble__810, (S1::Func<string>) fn__6359);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__1250()
        {
            T::Test test___54 = new T::Test();
            try
            {
                bool didBubble__812;
                try
                {
                    S0::SrcGlobal.From(sid__400("users")).Offset(-1);
                    didBubble__812 = false;
                }
                catch
                {
                    didBubble__812 = true;
                }
                string fn__6355()
                {
                    return "negative offset should bubble";
                }
                test___54.Assert(didBubble__812, (S1::Func<string>) fn__6355);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__1251()
        {
            T::Test test___55 = new T::Test();
            try
            {
                int minAge__814 = 21;
                ISafeIdentifier t___6333 = sid__400("users");
                ISafeIdentifier t___6334 = sid__400("id");
                ISafeIdentifier t___6335 = sid__400("name");
                ISafeIdentifier t___6336 = sid__400("email");
                Query t___6337 = S0::SrcGlobal.From(t___6333).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___6334, t___6335, t___6336));
                SqlBuilder t___6338 = new SqlBuilder();
                t___6338.AppendSafe("age >= ");
                t___6338.AppendInt32(21);
                Query t___6342 = t___6337.Where(t___6338.Accumulated);
                SqlBuilder t___6343 = new SqlBuilder();
                t___6343.AppendSafe("active = ");
                t___6343.AppendBoolean(true);
                Query t___3409;
                t___3409 = t___6342.Where(t___6343.Accumulated).OrderBy(sid__400("name"), true).Limit(25);
                Query t___3410;
                t___3410 = t___3409.Offset(0);
                Query q__815 = t___3410;
                bool t___6353 = q__815.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__6332()
                {
                    return "complex query";
                }
                test___55.Assert(t___6353, (S1::Func<string>) fn__6332);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__1254()
        {
            T::Test test___56 = new T::Test();
            try
            {
                Query q__817 = S0::SrcGlobal.From(sid__400("users"));
                SqlFragment t___3386;
                t___3386 = q__817.SafeToSql(100);
                SqlFragment t___3387 = t___3386;
                string s__818 = t___3387.ToString();
                bool t___6330 = s__818 == "SELECT * FROM users LIMIT 100";
                string fn__6326()
                {
                    return "should have limit: " + s__818;
                }
                test___56.Assert(t___6330, (S1::Func<string>) fn__6326);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__1255()
        {
            T::Test test___57 = new T::Test();
            try
            {
                Query t___3378;
                t___3378 = S0::SrcGlobal.From(sid__400("users")).Limit(5);
                Query q__820 = t___3378;
                SqlFragment t___3381;
                t___3381 = q__820.SafeToSql(100);
                SqlFragment t___3382 = t___3381;
                string s__821 = t___3382.ToString();
                bool t___6324 = s__821 == "SELECT * FROM users LIMIT 5";
                string fn__6320()
                {
                    return "explicit limit preserved: " + s__821;
                }
                test___57.Assert(t___6324, (S1::Func<string>) fn__6320);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__1256()
        {
            T::Test test___58 = new T::Test();
            try
            {
                bool didBubble__823;
                try
                {
                    S0::SrcGlobal.From(sid__400("users")).SafeToSql(-1);
                    didBubble__823 = false;
                }
                catch
                {
                    didBubble__823 = true;
                }
                string fn__6316()
                {
                    return "negative defaultLimit should bubble";
                }
                test___58.Assert(didBubble__823, (S1::Func<string>) fn__6316);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__1257()
        {
            T::Test test___59 = new T::Test();
            try
            {
                string evil__825 = "'; DROP TABLE users; --";
                ISafeIdentifier t___6300 = sid__400("users");
                SqlBuilder t___6301 = new SqlBuilder();
                t___6301.AppendSafe("name = ");
                t___6301.AppendString("'; DROP TABLE users; --");
                SqlFragment t___6304 = t___6301.Accumulated;
                Query q__826 = S0::SrcGlobal.From(t___6300).Where(t___6304);
                string s__827 = q__826.ToSql().ToString();
                bool t___6309 = s__827.IndexOf("''") >= 0;
                string fn__6299()
                {
                    return "quotes must be doubled: " + s__827;
                }
                test___59.Assert(t___6309, (S1::Func<string>) fn__6299);
                bool t___6313 = s__827.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__6298()
                {
                    return "structure intact: " + s__827;
                }
                test___59.Assert(t___6313, (S1::Func<string>) fn__6298);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__1259()
        {
            T::Test test___60 = new T::Test();
            try
            {
                string attack__829 = "users; DROP TABLE users; --";
                bool didBubble__830;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__830 = false;
                }
                catch
                {
                    didBubble__830 = true;
                }
                string fn__6295()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___60.Assert(didBubble__830, (S1::Func<string>) fn__6295);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__1260()
        {
            T::Test test___61 = new T::Test();
            try
            {
                ISafeIdentifier t___6284 = sid__400("users");
                ISafeIdentifier t___6285 = sid__400("orders");
                SqlBuilder t___6286 = new SqlBuilder();
                t___6286.AppendSafe("users.id = orders.user_id");
                SqlFragment t___6288 = t___6286.Accumulated;
                Query q__832 = S0::SrcGlobal.From(t___6284).InnerJoin(t___6285, t___6288);
                bool t___6293 = q__832.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__6283()
                {
                    return "inner join";
                }
                test___61.Assert(t___6293, (S1::Func<string>) fn__6283);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__1262()
        {
            T::Test test___62 = new T::Test();
            try
            {
                ISafeIdentifier t___6272 = sid__400("users");
                ISafeIdentifier t___6273 = sid__400("profiles");
                SqlBuilder t___6274 = new SqlBuilder();
                t___6274.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___6276 = t___6274.Accumulated;
                Query q__834 = S0::SrcGlobal.From(t___6272).LeftJoin(t___6273, t___6276);
                bool t___6281 = q__834.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__6271()
                {
                    return "left join";
                }
                test___62.Assert(t___6281, (S1::Func<string>) fn__6271);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__1264()
        {
            T::Test test___63 = new T::Test();
            try
            {
                ISafeIdentifier t___6260 = sid__400("orders");
                ISafeIdentifier t___6261 = sid__400("users");
                SqlBuilder t___6262 = new SqlBuilder();
                t___6262.AppendSafe("orders.user_id = users.id");
                SqlFragment t___6264 = t___6262.Accumulated;
                Query q__836 = S0::SrcGlobal.From(t___6260).RightJoin(t___6261, t___6264);
                bool t___6269 = q__836.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__6259()
                {
                    return "right join";
                }
                test___63.Assert(t___6269, (S1::Func<string>) fn__6259);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__1266()
        {
            T::Test test___64 = new T::Test();
            try
            {
                ISafeIdentifier t___6248 = sid__400("users");
                ISafeIdentifier t___6249 = sid__400("orders");
                SqlBuilder t___6250 = new SqlBuilder();
                t___6250.AppendSafe("users.id = orders.user_id");
                SqlFragment t___6252 = t___6250.Accumulated;
                Query q__838 = S0::SrcGlobal.From(t___6248).FullJoin(t___6249, t___6252);
                bool t___6257 = q__838.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__6247()
                {
                    return "full join";
                }
                test___64.Assert(t___6257, (S1::Func<string>) fn__6247);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__1268()
        {
            T::Test test___65 = new T::Test();
            try
            {
                ISafeIdentifier t___6231 = sid__400("users");
                ISafeIdentifier t___6232 = sid__400("orders");
                SqlBuilder t___6233 = new SqlBuilder();
                t___6233.AppendSafe("users.id = orders.user_id");
                SqlFragment t___6235 = t___6233.Accumulated;
                Query t___6236 = S0::SrcGlobal.From(t___6231).InnerJoin(t___6232, t___6235);
                ISafeIdentifier t___6237 = sid__400("profiles");
                SqlBuilder t___6238 = new SqlBuilder();
                t___6238.AppendSafe("users.id = profiles.user_id");
                Query q__840 = t___6236.LeftJoin(t___6237, t___6238.Accumulated);
                bool t___6245 = q__840.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__6230()
                {
                    return "chained joins";
                }
                test___65.Assert(t___6245, (S1::Func<string>) fn__6230);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__1271()
        {
            T::Test test___66 = new T::Test();
            try
            {
                ISafeIdentifier t___6212 = sid__400("users");
                ISafeIdentifier t___6213 = sid__400("orders");
                SqlBuilder t___6214 = new SqlBuilder();
                t___6214.AppendSafe("users.id = orders.user_id");
                SqlFragment t___6216 = t___6214.Accumulated;
                Query t___6217 = S0::SrcGlobal.From(t___6212).InnerJoin(t___6213, t___6216);
                SqlBuilder t___6218 = new SqlBuilder();
                t___6218.AppendSafe("orders.total > ");
                t___6218.AppendInt32(100);
                Query t___3293;
                t___3293 = t___6217.Where(t___6218.Accumulated).OrderBy(sid__400("name"), true).Limit(10);
                Query q__842 = t___3293;
                bool t___6228 = q__842.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__6211()
                {
                    return "join with where/order/limit";
                }
                test___66.Assert(t___6228, (S1::Func<string>) fn__6211);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__1274()
        {
            T::Test test___67 = new T::Test();
            try
            {
                SqlFragment c__844 = S0::SrcGlobal.Col(sid__400("users"), sid__400("id"));
                bool t___6209 = c__844.ToString() == "users.id";
                string fn__6203()
                {
                    return "col helper";
                }
                test___67.Assert(t___6209, (S1::Func<string>) fn__6203);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__1275()
        {
            T::Test test___68 = new T::Test();
            try
            {
                SqlFragment onCond__846 = S0::SrcGlobal.Col(sid__400("users"), sid__400("id"));
                SqlBuilder b__847 = new SqlBuilder();
                b__847.AppendFragment(onCond__846);
                b__847.AppendSafe(" = ");
                b__847.AppendFragment(S0::SrcGlobal.Col(sid__400("orders"), sid__400("user_id")));
                ISafeIdentifier t___6194 = sid__400("users");
                ISafeIdentifier t___6195 = sid__400("orders");
                SqlFragment t___6196 = b__847.Accumulated;
                Query q__848 = S0::SrcGlobal.From(t___6194).InnerJoin(t___6195, t___6196);
                bool t___6201 = q__848.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__6183()
                {
                    return "join with col";
                }
                test___68.Assert(t___6201, (S1::Func<string>) fn__6183);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__1276()
        {
            T::Test test___69 = new T::Test();
            try
            {
                ISafeIdentifier t___6172 = sid__400("users");
                SqlBuilder t___6173 = new SqlBuilder();
                t___6173.AppendSafe("status = ");
                t___6173.AppendString("active");
                SqlFragment t___6176 = t___6173.Accumulated;
                Query q__850 = S0::SrcGlobal.From(t___6172).OrWhere(t___6176);
                bool t___6181 = q__850.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__6171()
                {
                    return "orWhere basic";
                }
                test___69.Assert(t___6181, (S1::Func<string>) fn__6171);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__1278()
        {
            T::Test test___70 = new T::Test();
            try
            {
                ISafeIdentifier t___6155 = sid__400("users");
                SqlBuilder t___6156 = new SqlBuilder();
                t___6156.AppendSafe("age > ");
                t___6156.AppendInt32(18);
                SqlFragment t___6159 = t___6156.Accumulated;
                Query t___6160 = S0::SrcGlobal.From(t___6155).Where(t___6159);
                SqlBuilder t___6161 = new SqlBuilder();
                t___6161.AppendSafe("vip = ");
                t___6161.AppendBoolean(true);
                Query q__852 = t___6160.OrWhere(t___6161.Accumulated);
                bool t___6169 = q__852.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__6154()
                {
                    return "where then orWhere";
                }
                test___70.Assert(t___6169, (S1::Func<string>) fn__6154);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__1281()
        {
            T::Test test___71 = new T::Test();
            try
            {
                ISafeIdentifier t___6133 = sid__400("users");
                SqlBuilder t___6134 = new SqlBuilder();
                t___6134.AppendSafe("active = ");
                t___6134.AppendBoolean(true);
                SqlFragment t___6137 = t___6134.Accumulated;
                Query t___6138 = S0::SrcGlobal.From(t___6133).Where(t___6137);
                SqlBuilder t___6139 = new SqlBuilder();
                t___6139.AppendSafe("role = ");
                t___6139.AppendString("admin");
                Query t___6143 = t___6138.OrWhere(t___6139.Accumulated);
                SqlBuilder t___6144 = new SqlBuilder();
                t___6144.AppendSafe("role = ");
                t___6144.AppendString("moderator");
                Query q__854 = t___6143.OrWhere(t___6144.Accumulated);
                bool t___6152 = q__854.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__6132()
                {
                    return "multiple orWhere";
                }
                test___71.Assert(t___6152, (S1::Func<string>) fn__6132);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__1285()
        {
            T::Test test___72 = new T::Test();
            try
            {
                ISafeIdentifier t___6111 = sid__400("users");
                SqlBuilder t___6112 = new SqlBuilder();
                t___6112.AppendSafe("age > ");
                t___6112.AppendInt32(18);
                SqlFragment t___6115 = t___6112.Accumulated;
                Query t___6116 = S0::SrcGlobal.From(t___6111).Where(t___6115);
                SqlBuilder t___6117 = new SqlBuilder();
                t___6117.AppendSafe("active = ");
                t___6117.AppendBoolean(true);
                Query t___6121 = t___6116.Where(t___6117.Accumulated);
                SqlBuilder t___6122 = new SqlBuilder();
                t___6122.AppendSafe("vip = ");
                t___6122.AppendBoolean(true);
                Query q__856 = t___6121.OrWhere(t___6122.Accumulated);
                bool t___6130 = q__856.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__6110()
                {
                    return "mixed where and orWhere";
                }
                test___72.Assert(t___6130, (S1::Func<string>) fn__6110);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__1289()
        {
            T::Test test___73 = new T::Test();
            try
            {
                ISafeIdentifier t___6102 = sid__400("users");
                ISafeIdentifier t___6103 = sid__400("deleted_at");
                Query q__858 = S0::SrcGlobal.From(t___6102).WhereNull(t___6103);
                bool t___6108 = q__858.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__6101()
                {
                    return "whereNull";
                }
                test___73.Assert(t___6108, (S1::Func<string>) fn__6101);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__1290()
        {
            T::Test test___74 = new T::Test();
            try
            {
                ISafeIdentifier t___6093 = sid__400("users");
                ISafeIdentifier t___6094 = sid__400("email");
                Query q__860 = S0::SrcGlobal.From(t___6093).WhereNotNull(t___6094);
                bool t___6099 = q__860.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__6092()
                {
                    return "whereNotNull";
                }
                test___74.Assert(t___6099, (S1::Func<string>) fn__6092);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__1291()
        {
            T::Test test___75 = new T::Test();
            try
            {
                ISafeIdentifier t___6079 = sid__400("users");
                SqlBuilder t___6080 = new SqlBuilder();
                t___6080.AppendSafe("active = ");
                t___6080.AppendBoolean(true);
                SqlFragment t___6083 = t___6080.Accumulated;
                Query q__862 = S0::SrcGlobal.From(t___6079).Where(t___6083).WhereNull(sid__400("deleted_at"));
                bool t___6090 = q__862.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__6078()
                {
                    return "whereNull chained";
                }
                test___75.Assert(t___6090, (S1::Func<string>) fn__6078);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__1293()
        {
            T::Test test___76 = new T::Test();
            try
            {
                ISafeIdentifier t___6065 = sid__400("users");
                ISafeIdentifier t___6066 = sid__400("deleted_at");
                Query t___6067 = S0::SrcGlobal.From(t___6065).WhereNull(t___6066);
                SqlBuilder t___6068 = new SqlBuilder();
                t___6068.AppendSafe("role = ");
                t___6068.AppendString("admin");
                Query q__864 = t___6067.OrWhere(t___6068.Accumulated);
                bool t___6076 = q__864.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__6064()
                {
                    return "whereNotNull with orWhere";
                }
                test___76.Assert(t___6076, (S1::Func<string>) fn__6064);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__1295()
        {
            T::Test test___77 = new T::Test();
            try
            {
                ISafeIdentifier t___6053 = sid__400("users");
                ISafeIdentifier t___6054 = sid__400("id");
                SqlInt32 t___6055 = new SqlInt32(1);
                SqlInt32 t___6056 = new SqlInt32(2);
                SqlInt32 t___6057 = new SqlInt32(3);
                Query q__866 = S0::SrcGlobal.From(t___6053).WhereIn(t___6054, C::Listed.CreateReadOnlyList<SqlInt32>(t___6055, t___6056, t___6057));
                bool t___6062 = q__866.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__6052()
                {
                    return "whereIn ints";
                }
                test___77.Assert(t___6062, (S1::Func<string>) fn__6052);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__1296()
        {
            T::Test test___78 = new T::Test();
            try
            {
                ISafeIdentifier t___6042 = sid__400("users");
                ISafeIdentifier t___6043 = sid__400("name");
                SqlString t___6044 = new SqlString("Alice");
                SqlString t___6045 = new SqlString("Bob's");
                Query q__868 = S0::SrcGlobal.From(t___6042).WhereIn(t___6043, C::Listed.CreateReadOnlyList<SqlString>(t___6044, t___6045));
                bool t___6050 = q__868.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__6041()
                {
                    return "whereIn strings";
                }
                test___78.Assert(t___6050, (S1::Func<string>) fn__6041);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__1297()
        {
            T::Test test___79 = new T::Test();
            try
            {
                ISafeIdentifier t___6033 = sid__400("users");
                ISafeIdentifier t___6034 = sid__400("id");
                Query q__870 = S0::SrcGlobal.From(t___6033).WhereIn(t___6034, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___6039 = q__870.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__6032()
                {
                    return "whereIn empty";
                }
                test___79.Assert(t___6039, (S1::Func<string>) fn__6032);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__1298()
        {
            T::Test test___80 = new T::Test();
            try
            {
                ISafeIdentifier t___6017 = sid__400("users");
                SqlBuilder t___6018 = new SqlBuilder();
                t___6018.AppendSafe("active = ");
                t___6018.AppendBoolean(true);
                SqlFragment t___6021 = t___6018.Accumulated;
                Query q__872 = S0::SrcGlobal.From(t___6017).Where(t___6021).WhereIn(sid__400("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___6030 = q__872.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__6016()
                {
                    return "whereIn chained";
                }
                test___80.Assert(t___6030, (S1::Func<string>) fn__6016);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__1300()
        {
            T::Test test___81 = new T::Test();
            try
            {
                ISafeIdentifier t___6007 = sid__400("users");
                ISafeIdentifier t___6008 = sid__400("id");
                SqlInt32 t___6009 = new SqlInt32(42);
                Query q__874 = S0::SrcGlobal.From(t___6007).WhereIn(t___6008, C::Listed.CreateReadOnlyList<SqlInt32>(t___6009));
                bool t___6014 = q__874.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__6006()
                {
                    return "whereIn single";
                }
                test___81.Assert(t___6014, (S1::Func<string>) fn__6006);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__1301()
        {
            T::Test test___82 = new T::Test();
            try
            {
                ISafeIdentifier t___5995 = sid__400("users");
                SqlBuilder t___5996 = new SqlBuilder();
                t___5996.AppendSafe("active = ");
                t___5996.AppendBoolean(true);
                SqlFragment t___5999 = t___5996.Accumulated;
                Query q__876 = S0::SrcGlobal.From(t___5995).WhereNot(t___5999);
                bool t___6004 = q__876.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__5994()
                {
                    return "whereNot";
                }
                test___82.Assert(t___6004, (S1::Func<string>) fn__5994);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__1303()
        {
            T::Test test___83 = new T::Test();
            try
            {
                ISafeIdentifier t___5978 = sid__400("users");
                SqlBuilder t___5979 = new SqlBuilder();
                t___5979.AppendSafe("age > ");
                t___5979.AppendInt32(18);
                SqlFragment t___5982 = t___5979.Accumulated;
                Query t___5983 = S0::SrcGlobal.From(t___5978).Where(t___5982);
                SqlBuilder t___5984 = new SqlBuilder();
                t___5984.AppendSafe("banned = ");
                t___5984.AppendBoolean(true);
                Query q__878 = t___5983.WhereNot(t___5984.Accumulated);
                bool t___5992 = q__878.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__5977()
                {
                    return "whereNot chained";
                }
                test___83.Assert(t___5992, (S1::Func<string>) fn__5977);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__1306()
        {
            T::Test test___84 = new T::Test();
            try
            {
                ISafeIdentifier t___5967 = sid__400("users");
                ISafeIdentifier t___5968 = sid__400("age");
                SqlInt32 t___5969 = new SqlInt32(18);
                SqlInt32 t___5970 = new SqlInt32(65);
                Query q__880 = S0::SrcGlobal.From(t___5967).WhereBetween(t___5968, t___5969, t___5970);
                bool t___5975 = q__880.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__5966()
                {
                    return "whereBetween ints";
                }
                test___84.Assert(t___5975, (S1::Func<string>) fn__5966);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__1307()
        {
            T::Test test___85 = new T::Test();
            try
            {
                ISafeIdentifier t___5951 = sid__400("users");
                SqlBuilder t___5952 = new SqlBuilder();
                t___5952.AppendSafe("active = ");
                t___5952.AppendBoolean(true);
                SqlFragment t___5955 = t___5952.Accumulated;
                Query q__882 = S0::SrcGlobal.From(t___5951).Where(t___5955).WhereBetween(sid__400("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___5964 = q__882.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__5950()
                {
                    return "whereBetween chained";
                }
                test___85.Assert(t___5964, (S1::Func<string>) fn__5950);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__1309()
        {
            T::Test test___86 = new T::Test();
            try
            {
                ISafeIdentifier t___5942 = sid__400("users");
                ISafeIdentifier t___5943 = sid__400("name");
                Query q__884 = S0::SrcGlobal.From(t___5942).WhereLike(t___5943, "John%");
                bool t___5948 = q__884.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__5941()
                {
                    return "whereLike";
                }
                test___86.Assert(t___5948, (S1::Func<string>) fn__5941);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__1310()
        {
            T::Test test___87 = new T::Test();
            try
            {
                ISafeIdentifier t___5933 = sid__400("users");
                ISafeIdentifier t___5934 = sid__400("email");
                Query q__886 = S0::SrcGlobal.From(t___5933).WhereILike(t___5934, "%@gmail.com");
                bool t___5939 = q__886.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__5932()
                {
                    return "whereILike";
                }
                test___87.Assert(t___5939, (S1::Func<string>) fn__5932);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__1311()
        {
            T::Test test___88 = new T::Test();
            try
            {
                ISafeIdentifier t___5919 = sid__400("users");
                ISafeIdentifier t___5920 = sid__400("name");
                Query q__888 = S0::SrcGlobal.From(t___5919).WhereLike(t___5920, "'; DROP TABLE users; --");
                string s__889 = q__888.ToSql().ToString();
                bool t___5925 = s__889.IndexOf("''") >= 0;
                string fn__5918()
                {
                    return "like injection escaped: " + s__889;
                }
                test___88.Assert(t___5925, (S1::Func<string>) fn__5918);
                bool t___5929 = s__889.IndexOf("LIKE") >= 0;
                string fn__5917()
                {
                    return "like structure intact: " + s__889;
                }
                test___88.Assert(t___5929, (S1::Func<string>) fn__5917);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__1312()
        {
            T::Test test___89 = new T::Test();
            try
            {
                ISafeIdentifier t___5909 = sid__400("users");
                ISafeIdentifier t___5910 = sid__400("name");
                Query q__891 = S0::SrcGlobal.From(t___5909).WhereLike(t___5910, "%son%");
                bool t___5915 = q__891.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__5908()
                {
                    return "whereLike wildcard";
                }
                test___89.Assert(t___5915, (S1::Func<string>) fn__5908);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__1313()
        {
            T::Test test___96 = new T::Test();
            try
            {
                ISafeIdentifier t___3023;
                t___3023 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__929 = t___3023;
                bool t___5906 = id__929.SqlValue == "user_name";
                string fn__5903()
                {
                    return "value should round-trip";
                }
                test___96.Assert(t___5906, (S1::Func<string>) fn__5903);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__1314()
        {
            T::Test test___97 = new T::Test();
            try
            {
                bool didBubble__931;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__931 = false;
                }
                catch
                {
                    didBubble__931 = true;
                }
                string fn__5900()
                {
                    return "empty string should bubble";
                }
                test___97.Assert(didBubble__931, (S1::Func<string>) fn__5900);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__1315()
        {
            T::Test test___98 = new T::Test();
            try
            {
                bool didBubble__933;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__933 = false;
                }
                catch
                {
                    didBubble__933 = true;
                }
                string fn__5897()
                {
                    return "leading digit should bubble";
                }
                test___98.Assert(didBubble__933, (S1::Func<string>) fn__5897);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__1316()
        {
            T::Test test___99 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__935 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__5894(string c__936)
                {
                    bool didBubble__937;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__936);
                        didBubble__937 = false;
                    }
                    catch
                    {
                        didBubble__937 = true;
                    }
                    string fn__5891()
                    {
                        return "should reject: " + c__936;
                    }
                    test___99.Assert(didBubble__937, (S1::Func<string>) fn__5891);
                }
                C::Listed.ForEach(cases__935, (S1::Action<string>) fn__5894);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__1317()
        {
            T::Test test___100 = new T::Test();
            try
            {
                ISafeIdentifier t___3000;
                t___3000 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___3001 = t___3000;
                ISafeIdentifier t___3002;
                t___3002 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___3003 = t___3002;
                StringField t___5881 = new StringField();
                FieldDef t___5882 = new FieldDef(t___3003, t___5881, false);
                ISafeIdentifier t___3006;
                t___3006 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___3007 = t___3006;
                IntField t___5883 = new IntField();
                FieldDef t___5884 = new FieldDef(t___3007, t___5883, false);
                TableDef td__939 = new TableDef(t___3001, C::Listed.CreateReadOnlyList<FieldDef>(t___5882, t___5884));
                FieldDef t___3011;
                t___3011 = td__939.Field("age");
                FieldDef f__940 = t___3011;
                bool t___5889 = f__940.Name.SqlValue == "age";
                string fn__5880()
                {
                    return "should find age field";
                }
                test___100.Assert(t___5889, (S1::Func<string>) fn__5880);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__1318()
        {
            T::Test test___101 = new T::Test();
            try
            {
                ISafeIdentifier t___2991;
                t___2991 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___2992 = t___2991;
                ISafeIdentifier t___2993;
                t___2993 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___2994 = t___2993;
                StringField t___5875 = new StringField();
                FieldDef t___5876 = new FieldDef(t___2994, t___5875, false);
                TableDef td__942 = new TableDef(t___2992, C::Listed.CreateReadOnlyList<FieldDef>(t___5876));
                bool didBubble__943;
                try
                {
                    td__942.Field("nonexistent");
                    didBubble__943 = false;
                }
                catch
                {
                    didBubble__943 = true;
                }
                string fn__5874()
                {
                    return "unknown field should bubble";
                }
                test___101.Assert(didBubble__943, (S1::Func<string>) fn__5874);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__1319()
        {
            T::Test test___102 = new T::Test();
            try
            {
                ISafeIdentifier t___2979;
                t___2979 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___2980 = t___2979;
                StringField t___5863 = new StringField();
                FieldDef required__945 = new FieldDef(t___2980, t___5863, false);
                ISafeIdentifier t___2983;
                t___2983 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___2984 = t___2983;
                StringField t___5865 = new StringField();
                FieldDef optional__946 = new FieldDef(t___2984, t___5865, true);
                bool t___5869 = !required__945.Nullable;
                string fn__5862()
                {
                    return "required field should not be nullable";
                }
                test___102.Assert(t___5869, (S1::Func<string>) fn__5862);
                bool t___5871 = optional__946.Nullable;
                string fn__5861()
                {
                    return "optional field should be nullable";
                }
                test___102.Assert(t___5871, (S1::Func<string>) fn__5861);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__1320()
        {
            T::Test test___106 = new T::Test();
            try
            {
                string build__1072(string name__1074)
                {
                    SqlBuilder t___5843 = new SqlBuilder();
                    t___5843.AppendSafe("select * from hi where name = ");
                    t___5843.AppendString(name__1074);
                    return t___5843.Accumulated.ToString();
                }
                string buildWrong__1073(string name__1076)
                {
                    return "select * from hi where name = '" + name__1076 + "'";
                }
                string actual___1322 = build__1072("world");
                bool t___5853 = actual___1322 == "select * from hi where name = 'world'";
                string fn__5850()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___1322 + ")";
                }
                test___106.Assert(t___5853, (S1::Func<string>) fn__5850);
                string bobbyTables__1078 = "Robert'); drop table hi;--";
                string actual___1324 = build__1072("Robert'); drop table hi;--");
                bool t___5857 = actual___1324 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__5849()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___1324 + ")";
                }
                test___106.Assert(t___5857, (S1::Func<string>) fn__5849);
                string fn__5848()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___106.Assert(true, (S1::Func<string>) fn__5848);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__1328()
        {
            T::Test test___107 = new T::Test();
            try
            {
                SqlBuilder t___5811 = new SqlBuilder();
                t___5811.AppendSafe("v = ");
                t___5811.AppendString("");
                string actual___1329 = t___5811.Accumulated.ToString();
                bool t___5817 = actual___1329 == "v = ''";
                string fn__5810()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___1329 + ")";
                }
                test___107.Assert(t___5817, (S1::Func<string>) fn__5810);
                SqlBuilder t___5819 = new SqlBuilder();
                t___5819.AppendSafe("v = ");
                t___5819.AppendString("a''b");
                string actual___1332 = t___5819.Accumulated.ToString();
                bool t___5825 = actual___1332 == "v = 'a''''b'";
                string fn__5809()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___1332 + ")";
                }
                test___107.Assert(t___5825, (S1::Func<string>) fn__5809);
                SqlBuilder t___5827 = new SqlBuilder();
                t___5827.AppendSafe("v = ");
                t___5827.AppendString("Hello 世界");
                string actual___1335 = t___5827.Accumulated.ToString();
                bool t___5833 = actual___1335 == "v = 'Hello 世界'";
                string fn__5808()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1335 + ")";
                }
                test___107.Assert(t___5833, (S1::Func<string>) fn__5808);
                SqlBuilder t___5835 = new SqlBuilder();
                t___5835.AppendSafe("v = ");
                t___5835.AppendString("Line1\nLine2");
                string actual___1338 = t___5835.Accumulated.ToString();
                bool t___5841 = actual___1338 == "v = 'Line1\nLine2'";
                string fn__5807()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1338 + ")";
                }
                test___107.Assert(t___5841, (S1::Func<string>) fn__5807);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1341()
        {
            T::Test test___108 = new T::Test();
            try
            {
                SqlBuilder t___5782 = new SqlBuilder();
                t___5782.AppendSafe("select ");
                t___5782.AppendInt32(42);
                t___5782.AppendSafe(", ");
                t___5782.AppendInt64(43);
                t___5782.AppendSafe(", ");
                t___5782.AppendFloat64(19.99);
                t___5782.AppendSafe(", ");
                t___5782.AppendBoolean(true);
                t___5782.AppendSafe(", ");
                t___5782.AppendBoolean(false);
                string actual___1342 = t___5782.Accumulated.ToString();
                bool t___5796 = actual___1342 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__5781()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1342 + ")";
                }
                test___108.Assert(t___5796, (S1::Func<string>) fn__5781);
                S1::DateTime t___2924;
                t___2924 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1081 = t___2924;
                SqlBuilder t___5798 = new SqlBuilder();
                t___5798.AppendSafe("insert into t values (");
                t___5798.AppendDate(date__1081);
                t___5798.AppendSafe(")");
                string actual___1345 = t___5798.Accumulated.ToString();
                bool t___5805 = actual___1345 == "insert into t values ('2024-12-25')";
                string fn__5780()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1345 + ")";
                }
                test___108.Assert(t___5805, (S1::Func<string>) fn__5780);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1348()
        {
            T::Test test___109 = new T::Test();
            try
            {
                SqlBuilder t___5726 = new SqlBuilder();
                t___5726.AppendSafe("v IN (");
                t___5726.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___5726.AppendSafe(")");
                string actual___1349 = t___5726.Accumulated.ToString();
                bool t___5733 = actual___1349 == "v IN ('a', 'b', 'c''d')";
                string fn__5725()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1349 + ")";
                }
                test___109.Assert(t___5733, (S1::Func<string>) fn__5725);
                SqlBuilder t___5735 = new SqlBuilder();
                t___5735.AppendSafe("v IN (");
                t___5735.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___5735.AppendSafe(")");
                string actual___1352 = t___5735.Accumulated.ToString();
                bool t___5742 = actual___1352 == "v IN (1, 2, 3)";
                string fn__5724()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1352 + ")";
                }
                test___109.Assert(t___5742, (S1::Func<string>) fn__5724);
                SqlBuilder t___5744 = new SqlBuilder();
                t___5744.AppendSafe("v IN (");
                t___5744.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___5744.AppendSafe(")");
                string actual___1355 = t___5744.Accumulated.ToString();
                bool t___5751 = actual___1355 == "v IN (1, 2)";
                string fn__5723()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1355 + ")";
                }
                test___109.Assert(t___5751, (S1::Func<string>) fn__5723);
                SqlBuilder t___5753 = new SqlBuilder();
                t___5753.AppendSafe("v IN (");
                t___5753.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___5753.AppendSafe(")");
                string actual___1358 = t___5753.Accumulated.ToString();
                bool t___5760 = actual___1358 == "v IN (1.0, 2.0)";
                string fn__5722()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1358 + ")";
                }
                test___109.Assert(t___5760, (S1::Func<string>) fn__5722);
                SqlBuilder t___5762 = new SqlBuilder();
                t___5762.AppendSafe("v IN (");
                t___5762.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___5762.AppendSafe(")");
                string actual___1361 = t___5762.Accumulated.ToString();
                bool t___5769 = actual___1361 == "v IN (TRUE, FALSE)";
                string fn__5721()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1361 + ")";
                }
                test___109.Assert(t___5769, (S1::Func<string>) fn__5721);
                S1::DateTime t___2896;
                t___2896 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___2897 = t___2896;
                S1::DateTime t___2898;
                t___2898 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___2899 = t___2898;
                G::IReadOnlyList<S1::DateTime> dates__1083 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___2897, t___2899);
                SqlBuilder t___5771 = new SqlBuilder();
                t___5771.AppendSafe("v IN (");
                t___5771.AppendDateList(dates__1083);
                t___5771.AppendSafe(")");
                string actual___1364 = t___5771.Accumulated.ToString();
                bool t___5778 = actual___1364 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__5720()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1364 + ")";
                }
                test___109.Assert(t___5778, (S1::Func<string>) fn__5720);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1367()
        {
            T::Test test___110 = new T::Test();
            try
            {
                double nan__1085;
                nan__1085 = 0.0 / 0.0;
                SqlBuilder t___5712 = new SqlBuilder();
                t___5712.AppendSafe("v = ");
                t___5712.AppendFloat64(nan__1085);
                string actual___1368 = t___5712.Accumulated.ToString();
                bool t___5718 = actual___1368 == "v = NULL";
                string fn__5711()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1368 + ")";
                }
                test___110.Assert(t___5718, (S1::Func<string>) fn__5711);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1371()
        {
            T::Test test___111 = new T::Test();
            try
            {
                double inf__1087;
                inf__1087 = 1.0 / 0.0;
                SqlBuilder t___5703 = new SqlBuilder();
                t___5703.AppendSafe("v = ");
                t___5703.AppendFloat64(inf__1087);
                string actual___1372 = t___5703.Accumulated.ToString();
                bool t___5709 = actual___1372 == "v = NULL";
                string fn__5702()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1372 + ")";
                }
                test___111.Assert(t___5709, (S1::Func<string>) fn__5702);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1375()
        {
            T::Test test___112 = new T::Test();
            try
            {
                double ninf__1089;
                ninf__1089 = -1.0 / 0.0;
                SqlBuilder t___5694 = new SqlBuilder();
                t___5694.AppendSafe("v = ");
                t___5694.AppendFloat64(ninf__1089);
                string actual___1376 = t___5694.Accumulated.ToString();
                bool t___5700 = actual___1376 == "v = NULL";
                string fn__5693()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1376 + ")";
                }
                test___112.Assert(t___5700, (S1::Func<string>) fn__5693);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1379()
        {
            T::Test test___113 = new T::Test();
            try
            {
                SqlBuilder t___5669 = new SqlBuilder();
                t___5669.AppendSafe("v = ");
                t___5669.AppendFloat64(3.14);
                string actual___1380 = t___5669.Accumulated.ToString();
                bool t___5675 = actual___1380 == "v = 3.14";
                string fn__5668()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1380 + ")";
                }
                test___113.Assert(t___5675, (S1::Func<string>) fn__5668);
                SqlBuilder t___5677 = new SqlBuilder();
                t___5677.AppendSafe("v = ");
                t___5677.AppendFloat64(0.0);
                string actual___1383 = t___5677.Accumulated.ToString();
                bool t___5683 = actual___1383 == "v = 0.0";
                string fn__5667()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1383 + ")";
                }
                test___113.Assert(t___5683, (S1::Func<string>) fn__5667);
                SqlBuilder t___5685 = new SqlBuilder();
                t___5685.AppendSafe("v = ");
                t___5685.AppendFloat64(-42.5);
                string actual___1386 = t___5685.Accumulated.ToString();
                bool t___5691 = actual___1386 == "v = -42.5";
                string fn__5666()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1386 + ")";
                }
                test___113.Assert(t___5691, (S1::Func<string>) fn__5666);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1389()
        {
            T::Test test___114 = new T::Test();
            try
            {
                S1::DateTime t___2792;
                t___2792 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1092 = t___2792;
                SqlBuilder t___5658 = new SqlBuilder();
                t___5658.AppendSafe("v = ");
                t___5658.AppendDate(d__1092);
                string actual___1390 = t___5658.Accumulated.ToString();
                bool t___5664 = actual___1390 == "v = '2024-06-15'";
                string fn__5657()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1390 + ")";
                }
                test___114.Assert(t___5664, (S1::Func<string>) fn__5657);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1393()
        {
            T::Test test___115 = new T::Test();
            try
            {
                string name__1094 = "Someone";
                SqlBuilder t___5626 = new SqlBuilder();
                t___5626.AppendSafe("where p.last_name = ");
                t___5626.AppendString("Someone");
                SqlFragment condition__1095 = t___5626.Accumulated;
                SqlBuilder t___5630 = new SqlBuilder();
                t___5630.AppendSafe("select p.id from person p ");
                t___5630.AppendFragment(condition__1095);
                string actual___1395 = t___5630.Accumulated.ToString();
                bool t___5636 = actual___1395 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__5625()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1395 + ")";
                }
                test___115.Assert(t___5636, (S1::Func<string>) fn__5625);
                SqlBuilder t___5638 = new SqlBuilder();
                t___5638.AppendSafe("select p.id from person p ");
                t___5638.AppendPart(condition__1095.ToSource());
                string actual___1398 = t___5638.Accumulated.ToString();
                bool t___5645 = actual___1398 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__5624()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1398 + ")";
                }
                test___115.Assert(t___5645, (S1::Func<string>) fn__5624);
                G::IReadOnlyList<ISqlPart> parts__1096 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___5649 = new SqlBuilder();
                t___5649.AppendSafe("select ");
                t___5649.AppendPartList(parts__1096);
                string actual___1401 = t___5649.Accumulated.ToString();
                bool t___5655 = actual___1401 == "select 'a''b', 3";
                string fn__5623()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___1401 + ")";
                }
                test___115.Assert(t___5655, (S1::Func<string>) fn__5623);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
    }
}
