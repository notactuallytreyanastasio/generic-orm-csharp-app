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
        internal static ISafeIdentifier csid__544(string name__689)
        {
            ISafeIdentifier t___6343;
            t___6343 = S0::SrcGlobal.SafeIdentifier(name__689);
            return t___6343;
        }
        internal static TableDef userTable__545()
        {
            return new TableDef(csid__544("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__544("name"), new StringField(), false), new FieldDef(csid__544("email"), new StringField(), false), new FieldDef(csid__544("age"), new IntField(), true), new FieldDef(csid__544("score"), new FloatField(), true), new FieldDef(csid__544("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__1645()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__693 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___11177 = userTable__545();
                ISafeIdentifier t___11178 = csid__544("name");
                ISafeIdentifier t___11179 = csid__544("email");
                IChangeset cs__694 = S0::SrcGlobal.Changeset(t___11177, params__693).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11178, t___11179));
                bool t___11182 = C::Mapped.ContainsKey(cs__694.Changes, "name");
                string fn__11172()
                {
                    return "name should be in changes";
                }
                test___24.Assert(t___11182, (S1::Func<string>) fn__11172);
                bool t___11186 = C::Mapped.ContainsKey(cs__694.Changes, "email");
                string fn__11171()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___11186, (S1::Func<string>) fn__11171);
                bool t___11192 = !C::Mapped.ContainsKey(cs__694.Changes, "admin");
                string fn__11170()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___24.Assert(t___11192, (S1::Func<string>) fn__11170);
                bool t___11194 = cs__694.IsValid;
                string fn__11169()
                {
                    return "should still be valid";
                }
                test___24.Assert(t___11194, (S1::Func<string>) fn__11169);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__1646()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__696 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___11155 = userTable__545();
                ISafeIdentifier t___11156 = csid__544("name");
                IChangeset cs__697 = S0::SrcGlobal.Changeset(t___11155, params__696).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11156)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("email")));
                bool t___11163 = !C::Mapped.ContainsKey(cs__697.Changes, "name");
                string fn__11151()
                {
                    return "name must be excluded by second cast";
                }
                test___25.Assert(t___11163, (S1::Func<string>) fn__11151);
                bool t___11166 = C::Mapped.ContainsKey(cs__697.Changes, "email");
                string fn__11150()
                {
                    return "email should be present";
                }
                test___25.Assert(t___11166, (S1::Func<string>) fn__11150);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__1647()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__699 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___11137 = userTable__545();
                ISafeIdentifier t___11138 = csid__544("name");
                ISafeIdentifier t___11139 = csid__544("email");
                IChangeset cs__700 = S0::SrcGlobal.Changeset(t___11137, params__699).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11138, t___11139));
                bool t___11144 = !C::Mapped.ContainsKey(cs__700.Changes, "name");
                string fn__11133()
                {
                    return "empty name should not be in changes";
                }
                test___26.Assert(t___11144, (S1::Func<string>) fn__11133);
                bool t___11147 = C::Mapped.ContainsKey(cs__700.Changes, "email");
                string fn__11132()
                {
                    return "email should be in changes";
                }
                test___26.Assert(t___11147, (S1::Func<string>) fn__11132);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__1648()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__702 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___11119 = userTable__545();
                ISafeIdentifier t___11120 = csid__544("name");
                IChangeset cs__703 = S0::SrcGlobal.Changeset(t___11119, params__702).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11120)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name")));
                bool t___11124 = cs__703.IsValid;
                string fn__11116()
                {
                    return "should be valid";
                }
                test___27.Assert(t___11124, (S1::Func<string>) fn__11116);
                bool t___11130 = cs__703.Errors.Count == 0;
                string fn__11115()
                {
                    return "no errors expected";
                }
                test___27.Assert(t___11130, (S1::Func<string>) fn__11115);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__1649()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__705 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___11095 = userTable__545();
                ISafeIdentifier t___11096 = csid__544("name");
                IChangeset cs__706 = S0::SrcGlobal.Changeset(t___11095, params__705).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11096)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name")));
                bool t___11102 = !cs__706.IsValid;
                string fn__11093()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___11102, (S1::Func<string>) fn__11093);
                bool t___11107 = cs__706.Errors.Count == 1;
                string fn__11092()
                {
                    return "should have one error";
                }
                test___28.Assert(t___11107, (S1::Func<string>) fn__11092);
                bool t___11113 = cs__706.Errors[0].Field == "name";
                string fn__11091()
                {
                    return "error should name the field";
                }
                test___28.Assert(t___11113, (S1::Func<string>) fn__11091);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__1650()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__708 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___11083 = userTable__545();
                ISafeIdentifier t___11084 = csid__544("name");
                IChangeset cs__709 = S0::SrcGlobal.Changeset(t___11083, params__708).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11084)).ValidateLength(csid__544("name"), 2, 50);
                bool t___11088 = cs__709.IsValid;
                string fn__11080()
                {
                    return "should be valid";
                }
                test___29.Assert(t___11088, (S1::Func<string>) fn__11080);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__1651()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__711 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___11071 = userTable__545();
                ISafeIdentifier t___11072 = csid__544("name");
                IChangeset cs__712 = S0::SrcGlobal.Changeset(t___11071, params__711).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11072)).ValidateLength(csid__544("name"), 2, 50);
                bool t___11078 = !cs__712.IsValid;
                string fn__11068()
                {
                    return "should be invalid";
                }
                test___30.Assert(t___11078, (S1::Func<string>) fn__11068);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__1652()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__714 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___11059 = userTable__545();
                ISafeIdentifier t___11060 = csid__544("name");
                IChangeset cs__715 = S0::SrcGlobal.Changeset(t___11059, params__714).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11060)).ValidateLength(csid__544("name"), 2, 10);
                bool t___11066 = !cs__715.IsValid;
                string fn__11056()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___11066, (S1::Func<string>) fn__11056);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__1653()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__717 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___11048 = userTable__545();
                ISafeIdentifier t___11049 = csid__544("age");
                IChangeset cs__718 = S0::SrcGlobal.Changeset(t___11048, params__717).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11049)).ValidateInt(csid__544("age"));
                bool t___11053 = cs__718.IsValid;
                string fn__11045()
                {
                    return "should be valid";
                }
                test___32.Assert(t___11053, (S1::Func<string>) fn__11045);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__1654()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__720 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___11036 = userTable__545();
                ISafeIdentifier t___11037 = csid__544("age");
                IChangeset cs__721 = S0::SrcGlobal.Changeset(t___11036, params__720).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11037)).ValidateInt(csid__544("age"));
                bool t___11043 = !cs__721.IsValid;
                string fn__11033()
                {
                    return "should be invalid";
                }
                test___33.Assert(t___11043, (S1::Func<string>) fn__11033);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__1655()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__723 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___11025 = userTable__545();
                ISafeIdentifier t___11026 = csid__544("score");
                IChangeset cs__724 = S0::SrcGlobal.Changeset(t___11025, params__723).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11026)).ValidateFloat(csid__544("score"));
                bool t___11030 = cs__724.IsValid;
                string fn__11022()
                {
                    return "should be valid";
                }
                test___34.Assert(t___11030, (S1::Func<string>) fn__11022);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__1656()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__726 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___11014 = userTable__545();
                ISafeIdentifier t___11015 = csid__544("age");
                IChangeset cs__727 = S0::SrcGlobal.Changeset(t___11014, params__726).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11015)).ValidateInt64(csid__544("age"));
                bool t___11019 = cs__727.IsValid;
                string fn__11011()
                {
                    return "should be valid";
                }
                test___35.Assert(t___11019, (S1::Func<string>) fn__11011);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__1657()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__729 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___11002 = userTable__545();
                ISafeIdentifier t___11003 = csid__544("age");
                IChangeset cs__730 = S0::SrcGlobal.Changeset(t___11002, params__729).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___11003)).ValidateInt64(csid__544("age"));
                bool t___11009 = !cs__730.IsValid;
                string fn__10999()
                {
                    return "should be invalid";
                }
                test___36.Assert(t___11009, (S1::Func<string>) fn__10999);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__1658()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__10996(string v__732)
                {
                    G::IReadOnlyDictionary<string, string> params__733 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__732)));
                    TableDef t___10988 = userTable__545();
                    ISafeIdentifier t___10989 = csid__544("active");
                    IChangeset cs__734 = S0::SrcGlobal.Changeset(t___10988, params__733).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10989)).ValidateBool(csid__544("active"));
                    bool t___10993 = cs__734.IsValid;
                    string fn__10985()
                    {
                        return "should accept: " + v__732;
                    }
                    test___37.Assert(t___10993, (S1::Func<string>) fn__10985);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__10996);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__1659()
        {
            T::Test test___38 = new T::Test();
            try
            {
                void fn__10982(string v__736)
                {
                    G::IReadOnlyDictionary<string, string> params__737 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__736)));
                    TableDef t___10974 = userTable__545();
                    ISafeIdentifier t___10975 = csid__544("active");
                    IChangeset cs__738 = S0::SrcGlobal.Changeset(t___10974, params__737).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10975)).ValidateBool(csid__544("active"));
                    bool t___10979 = cs__738.IsValid;
                    string fn__10971()
                    {
                        return "should accept: " + v__736;
                    }
                    test___38.Assert(t___10979, (S1::Func<string>) fn__10971);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__10982);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__1660()
        {
            T::Test test___39 = new T::Test();
            try
            {
                void fn__10968(string v__740)
                {
                    G::IReadOnlyDictionary<string, string> params__741 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__740)));
                    TableDef t___10959 = userTable__545();
                    ISafeIdentifier t___10960 = csid__544("active");
                    IChangeset cs__742 = S0::SrcGlobal.Changeset(t___10959, params__741).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10960)).ValidateBool(csid__544("active"));
                    bool t___10966 = !cs__742.IsValid;
                    string fn__10956()
                    {
                        return "should reject ambiguous: " + v__740;
                    }
                    test___39.Assert(t___10966, (S1::Func<string>) fn__10956);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__10968);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__1661()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__744 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___10944 = userTable__545();
                ISafeIdentifier t___10945 = csid__544("name");
                ISafeIdentifier t___10946 = csid__544("email");
                IChangeset cs__745 = S0::SrcGlobal.Changeset(t___10944, params__744).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10945, t___10946)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name"), csid__544("email")));
                SqlFragment t___6144;
                t___6144 = cs__745.ToInsertSql();
                SqlFragment sqlFrag__746 = t___6144;
                string s__747 = sqlFrag__746.ToString();
                bool t___10953 = s__747.IndexOf("''") >= 0;
                string fn__10940()
                {
                    return "single quote must be doubled: " + s__747;
                }
                test___40.Assert(t___10953, (S1::Func<string>) fn__10940);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__1662()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__749 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___10924 = userTable__545();
                ISafeIdentifier t___10925 = csid__544("name");
                ISafeIdentifier t___10926 = csid__544("email");
                IChangeset cs__750 = S0::SrcGlobal.Changeset(t___10924, params__749).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10925, t___10926)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name"), csid__544("email")));
                SqlFragment t___6123;
                t___6123 = cs__750.ToInsertSql();
                SqlFragment sqlFrag__751 = t___6123;
                string s__752 = sqlFrag__751.ToString();
                bool t___10933 = s__752.IndexOf("INSERT INTO users") >= 0;
                string fn__10920()
                {
                    return "has INSERT INTO: " + s__752;
                }
                test___41.Assert(t___10933, (S1::Func<string>) fn__10920);
                bool t___10937 = s__752.IndexOf("'Alice'") >= 0;
                string fn__10919()
                {
                    return "has quoted name: " + s__752;
                }
                test___41.Assert(t___10937, (S1::Func<string>) fn__10919);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__1663()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__754 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___10906 = userTable__545();
                ISafeIdentifier t___10907 = csid__544("name");
                ISafeIdentifier t___10908 = csid__544("email");
                ISafeIdentifier t___10909 = csid__544("age");
                IChangeset cs__755 = S0::SrcGlobal.Changeset(t___10906, params__754).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10907, t___10908, t___10909)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name"), csid__544("email")));
                SqlFragment t___6106;
                t___6106 = cs__755.ToInsertSql();
                SqlFragment sqlFrag__756 = t___6106;
                string s__757 = sqlFrag__756.ToString();
                bool t___10916 = s__757.IndexOf("25") >= 0;
                string fn__10901()
                {
                    return "age rendered unquoted: " + s__757;
                }
                test___42.Assert(t___10916, (S1::Func<string>) fn__10901);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__1664()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__759 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___10894 = userTable__545();
                ISafeIdentifier t___10895 = csid__544("name");
                IChangeset cs__760 = S0::SrcGlobal.Changeset(t___10894, params__759).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10895)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name")));
                bool didBubble__761;
                try
                {
                    cs__760.ToInsertSql();
                    didBubble__761 = false;
                }
                catch
                {
                    didBubble__761 = true;
                }
                string fn__10892()
                {
                    return "invalid changeset should bubble";
                }
                test___43.Assert(didBubble__761, (S1::Func<string>) fn__10892);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__1665()
        {
            T::Test test___44 = new T::Test();
            try
            {
                TableDef strictTable__763 = new TableDef(csid__544("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__544("title"), new StringField(), false), new FieldDef(csid__544("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__764 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___10885 = csid__544("body");
                IChangeset cs__765 = S0::SrcGlobal.Changeset(strictTable__763, params__764).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10885));
                bool t___10887 = cs__765.IsValid;
                string fn__10874()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___44.Assert(t___10887, (S1::Func<string>) fn__10874);
                bool didBubble__766;
                try
                {
                    cs__765.ToInsertSql();
                    didBubble__766 = false;
                }
                catch
                {
                    didBubble__766 = true;
                }
                string fn__10873()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___44.Assert(didBubble__766, (S1::Func<string>) fn__10873);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__1666()
        {
            T::Test test___45 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__768 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___10864 = userTable__545();
                ISafeIdentifier t___10865 = csid__544("name");
                IChangeset cs__769 = S0::SrcGlobal.Changeset(t___10864, params__768).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10865)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name")));
                SqlFragment t___6066;
                t___6066 = cs__769.ToUpdateSql(42);
                SqlFragment sqlFrag__770 = t___6066;
                string s__771 = sqlFrag__770.ToString();
                bool t___10871 = s__771 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__10861()
                {
                    return "got: " + s__771;
                }
                test___45.Assert(t___10871, (S1::Func<string>) fn__10861);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__1667()
        {
            T::Test test___46 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__773 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___10854 = userTable__545();
                ISafeIdentifier t___10855 = csid__544("name");
                IChangeset cs__774 = S0::SrcGlobal.Changeset(t___10854, params__773).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10855)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__544("name")));
                bool didBubble__775;
                try
                {
                    cs__774.ToUpdateSql(1);
                    didBubble__775 = false;
                }
                catch
                {
                    didBubble__775 = true;
                }
                string fn__10852()
                {
                    return "invalid changeset should bubble";
                }
                test___46.Assert(didBubble__775, (S1::Func<string>) fn__10852);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__546(string name__1120)
        {
            ISafeIdentifier t___5526;
            t___5526 = S0::SrcGlobal.SafeIdentifier(name__1120);
            return t___5526;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__1749()
        {
            T::Test test___47 = new T::Test();
            try
            {
                Query q__1123 = S0::SrcGlobal.From(sid__546("users"));
                bool t___10337 = q__1123.ToSql().ToString() == "SELECT * FROM users";
                string fn__10332()
                {
                    return "bare query";
                }
                test___47.Assert(t___10337, (S1::Func<string>) fn__10332);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__1750()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___10323 = sid__546("users");
                ISafeIdentifier t___10324 = sid__546("id");
                ISafeIdentifier t___10325 = sid__546("name");
                Query q__1125 = S0::SrcGlobal.From(t___10323).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10324, t___10325));
                bool t___10330 = q__1125.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__10322()
                {
                    return "select columns";
                }
                test___48.Assert(t___10330, (S1::Func<string>) fn__10322);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__1751()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___10311 = sid__546("users");
                SqlBuilder t___10312 = new SqlBuilder();
                t___10312.AppendSafe("age > ");
                t___10312.AppendInt32(18);
                SqlFragment t___10315 = t___10312.Accumulated;
                Query q__1127 = S0::SrcGlobal.From(t___10311).Where(t___10315);
                bool t___10320 = q__1127.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__10310()
                {
                    return "where int";
                }
                test___49.Assert(t___10320, (S1::Func<string>) fn__10310);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__1753()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___10299 = sid__546("users");
                SqlBuilder t___10300 = new SqlBuilder();
                t___10300.AppendSafe("active = ");
                t___10300.AppendBoolean(true);
                SqlFragment t___10303 = t___10300.Accumulated;
                Query q__1129 = S0::SrcGlobal.From(t___10299).Where(t___10303);
                bool t___10308 = q__1129.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__10298()
                {
                    return "where bool";
                }
                test___50.Assert(t___10308, (S1::Func<string>) fn__10298);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__1755()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___10282 = sid__546("users");
                SqlBuilder t___10283 = new SqlBuilder();
                t___10283.AppendSafe("age > ");
                t___10283.AppendInt32(18);
                SqlFragment t___10286 = t___10283.Accumulated;
                Query t___10287 = S0::SrcGlobal.From(t___10282).Where(t___10286);
                SqlBuilder t___10288 = new SqlBuilder();
                t___10288.AppendSafe("active = ");
                t___10288.AppendBoolean(true);
                Query q__1131 = t___10287.Where(t___10288.Accumulated);
                bool t___10296 = q__1131.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__10281()
                {
                    return "chained where";
                }
                test___51.Assert(t___10296, (S1::Func<string>) fn__10281);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__1758()
        {
            T::Test test___52 = new T::Test();
            try
            {
                ISafeIdentifier t___10273 = sid__546("users");
                ISafeIdentifier t___10274 = sid__546("name");
                Query q__1133 = S0::SrcGlobal.From(t___10273).OrderBy(t___10274, true);
                bool t___10279 = q__1133.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__10272()
                {
                    return "order asc";
                }
                test___52.Assert(t___10279, (S1::Func<string>) fn__10272);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__1759()
        {
            T::Test test___53 = new T::Test();
            try
            {
                ISafeIdentifier t___10264 = sid__546("users");
                ISafeIdentifier t___10265 = sid__546("created_at");
                Query q__1135 = S0::SrcGlobal.From(t___10264).OrderBy(t___10265, false);
                bool t___10270 = q__1135.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__10263()
                {
                    return "order desc";
                }
                test___53.Assert(t___10270, (S1::Func<string>) fn__10263);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__1760()
        {
            T::Test test___54 = new T::Test();
            try
            {
                Query t___5460;
                t___5460 = S0::SrcGlobal.From(sid__546("users")).Limit(10);
                Query t___5461;
                t___5461 = t___5460.Offset(20);
                Query q__1137 = t___5461;
                bool t___10261 = q__1137.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__10256()
                {
                    return "limit/offset";
                }
                test___54.Assert(t___10261, (S1::Func<string>) fn__10256);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__1761()
        {
            T::Test test___55 = new T::Test();
            try
            {
                bool didBubble__1139;
                try
                {
                    S0::SrcGlobal.From(sid__546("users")).Limit(-1);
                    didBubble__1139 = false;
                }
                catch
                {
                    didBubble__1139 = true;
                }
                string fn__10252()
                {
                    return "negative limit should bubble";
                }
                test___55.Assert(didBubble__1139, (S1::Func<string>) fn__10252);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__1762()
        {
            T::Test test___56 = new T::Test();
            try
            {
                bool didBubble__1141;
                try
                {
                    S0::SrcGlobal.From(sid__546("users")).Offset(-1);
                    didBubble__1141 = false;
                }
                catch
                {
                    didBubble__1141 = true;
                }
                string fn__10248()
                {
                    return "negative offset should bubble";
                }
                test___56.Assert(didBubble__1141, (S1::Func<string>) fn__10248);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__1763()
        {
            T::Test test___57 = new T::Test();
            try
            {
                int minAge__1143 = 21;
                ISafeIdentifier t___10226 = sid__546("users");
                ISafeIdentifier t___10227 = sid__546("id");
                ISafeIdentifier t___10228 = sid__546("name");
                ISafeIdentifier t___10229 = sid__546("email");
                Query t___10230 = S0::SrcGlobal.From(t___10226).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10227, t___10228, t___10229));
                SqlBuilder t___10231 = new SqlBuilder();
                t___10231.AppendSafe("age >= ");
                t___10231.AppendInt32(21);
                Query t___10235 = t___10230.Where(t___10231.Accumulated);
                SqlBuilder t___10236 = new SqlBuilder();
                t___10236.AppendSafe("active = ");
                t___10236.AppendBoolean(true);
                Query t___5446;
                t___5446 = t___10235.Where(t___10236.Accumulated).OrderBy(sid__546("name"), true).Limit(25);
                Query t___5447;
                t___5447 = t___5446.Offset(0);
                Query q__1144 = t___5447;
                bool t___10246 = q__1144.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__10225()
                {
                    return "complex query";
                }
                test___57.Assert(t___10246, (S1::Func<string>) fn__10225);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__1766()
        {
            T::Test test___58 = new T::Test();
            try
            {
                Query q__1146 = S0::SrcGlobal.From(sid__546("users"));
                SqlFragment t___5423;
                t___5423 = q__1146.SafeToSql(100);
                SqlFragment t___5424 = t___5423;
                string s__1147 = t___5424.ToString();
                bool t___10223 = s__1147 == "SELECT * FROM users LIMIT 100";
                string fn__10219()
                {
                    return "should have limit: " + s__1147;
                }
                test___58.Assert(t___10223, (S1::Func<string>) fn__10219);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__1767()
        {
            T::Test test___59 = new T::Test();
            try
            {
                Query t___5415;
                t___5415 = S0::SrcGlobal.From(sid__546("users")).Limit(5);
                Query q__1149 = t___5415;
                SqlFragment t___5418;
                t___5418 = q__1149.SafeToSql(100);
                SqlFragment t___5419 = t___5418;
                string s__1150 = t___5419.ToString();
                bool t___10217 = s__1150 == "SELECT * FROM users LIMIT 5";
                string fn__10213()
                {
                    return "explicit limit preserved: " + s__1150;
                }
                test___59.Assert(t___10217, (S1::Func<string>) fn__10213);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__1768()
        {
            T::Test test___60 = new T::Test();
            try
            {
                bool didBubble__1152;
                try
                {
                    S0::SrcGlobal.From(sid__546("users")).SafeToSql(-1);
                    didBubble__1152 = false;
                }
                catch
                {
                    didBubble__1152 = true;
                }
                string fn__10209()
                {
                    return "negative defaultLimit should bubble";
                }
                test___60.Assert(didBubble__1152, (S1::Func<string>) fn__10209);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__1769()
        {
            T::Test test___61 = new T::Test();
            try
            {
                string evil__1154 = "'; DROP TABLE users; --";
                ISafeIdentifier t___10193 = sid__546("users");
                SqlBuilder t___10194 = new SqlBuilder();
                t___10194.AppendSafe("name = ");
                t___10194.AppendString("'; DROP TABLE users; --");
                SqlFragment t___10197 = t___10194.Accumulated;
                Query q__1155 = S0::SrcGlobal.From(t___10193).Where(t___10197);
                string s__1156 = q__1155.ToSql().ToString();
                bool t___10202 = s__1156.IndexOf("''") >= 0;
                string fn__10192()
                {
                    return "quotes must be doubled: " + s__1156;
                }
                test___61.Assert(t___10202, (S1::Func<string>) fn__10192);
                bool t___10206 = s__1156.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__10191()
                {
                    return "structure intact: " + s__1156;
                }
                test___61.Assert(t___10206, (S1::Func<string>) fn__10191);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__1771()
        {
            T::Test test___62 = new T::Test();
            try
            {
                string attack__1158 = "users; DROP TABLE users; --";
                bool didBubble__1159;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__1159 = false;
                }
                catch
                {
                    didBubble__1159 = true;
                }
                string fn__10188()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___62.Assert(didBubble__1159, (S1::Func<string>) fn__10188);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__1772()
        {
            T::Test test___63 = new T::Test();
            try
            {
                ISafeIdentifier t___10177 = sid__546("users");
                ISafeIdentifier t___10178 = sid__546("orders");
                SqlBuilder t___10179 = new SqlBuilder();
                t___10179.AppendSafe("users.id = orders.user_id");
                SqlFragment t___10181 = t___10179.Accumulated;
                Query q__1161 = S0::SrcGlobal.From(t___10177).InnerJoin(t___10178, t___10181);
                bool t___10186 = q__1161.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__10176()
                {
                    return "inner join";
                }
                test___63.Assert(t___10186, (S1::Func<string>) fn__10176);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__1774()
        {
            T::Test test___64 = new T::Test();
            try
            {
                ISafeIdentifier t___10165 = sid__546("users");
                ISafeIdentifier t___10166 = sid__546("profiles");
                SqlBuilder t___10167 = new SqlBuilder();
                t___10167.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___10169 = t___10167.Accumulated;
                Query q__1163 = S0::SrcGlobal.From(t___10165).LeftJoin(t___10166, t___10169);
                bool t___10174 = q__1163.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__10164()
                {
                    return "left join";
                }
                test___64.Assert(t___10174, (S1::Func<string>) fn__10164);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__1776()
        {
            T::Test test___65 = new T::Test();
            try
            {
                ISafeIdentifier t___10153 = sid__546("orders");
                ISafeIdentifier t___10154 = sid__546("users");
                SqlBuilder t___10155 = new SqlBuilder();
                t___10155.AppendSafe("orders.user_id = users.id");
                SqlFragment t___10157 = t___10155.Accumulated;
                Query q__1165 = S0::SrcGlobal.From(t___10153).RightJoin(t___10154, t___10157);
                bool t___10162 = q__1165.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__10152()
                {
                    return "right join";
                }
                test___65.Assert(t___10162, (S1::Func<string>) fn__10152);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__1778()
        {
            T::Test test___66 = new T::Test();
            try
            {
                ISafeIdentifier t___10141 = sid__546("users");
                ISafeIdentifier t___10142 = sid__546("orders");
                SqlBuilder t___10143 = new SqlBuilder();
                t___10143.AppendSafe("users.id = orders.user_id");
                SqlFragment t___10145 = t___10143.Accumulated;
                Query q__1167 = S0::SrcGlobal.From(t___10141).FullJoin(t___10142, t___10145);
                bool t___10150 = q__1167.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__10140()
                {
                    return "full join";
                }
                test___66.Assert(t___10150, (S1::Func<string>) fn__10140);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__1780()
        {
            T::Test test___67 = new T::Test();
            try
            {
                ISafeIdentifier t___10124 = sid__546("users");
                ISafeIdentifier t___10125 = sid__546("orders");
                SqlBuilder t___10126 = new SqlBuilder();
                t___10126.AppendSafe("users.id = orders.user_id");
                SqlFragment t___10128 = t___10126.Accumulated;
                Query t___10129 = S0::SrcGlobal.From(t___10124).InnerJoin(t___10125, t___10128);
                ISafeIdentifier t___10130 = sid__546("profiles");
                SqlBuilder t___10131 = new SqlBuilder();
                t___10131.AppendSafe("users.id = profiles.user_id");
                Query q__1169 = t___10129.LeftJoin(t___10130, t___10131.Accumulated);
                bool t___10138 = q__1169.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__10123()
                {
                    return "chained joins";
                }
                test___67.Assert(t___10138, (S1::Func<string>) fn__10123);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__1783()
        {
            T::Test test___68 = new T::Test();
            try
            {
                ISafeIdentifier t___10105 = sid__546("users");
                ISafeIdentifier t___10106 = sid__546("orders");
                SqlBuilder t___10107 = new SqlBuilder();
                t___10107.AppendSafe("users.id = orders.user_id");
                SqlFragment t___10109 = t___10107.Accumulated;
                Query t___10110 = S0::SrcGlobal.From(t___10105).InnerJoin(t___10106, t___10109);
                SqlBuilder t___10111 = new SqlBuilder();
                t___10111.AppendSafe("orders.total > ");
                t___10111.AppendInt32(100);
                Query t___5330;
                t___5330 = t___10110.Where(t___10111.Accumulated).OrderBy(sid__546("name"), true).Limit(10);
                Query q__1171 = t___5330;
                bool t___10121 = q__1171.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__10104()
                {
                    return "join with where/order/limit";
                }
                test___68.Assert(t___10121, (S1::Func<string>) fn__10104);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__1786()
        {
            T::Test test___69 = new T::Test();
            try
            {
                SqlFragment c__1173 = S0::SrcGlobal.Col(sid__546("users"), sid__546("id"));
                bool t___10102 = c__1173.ToString() == "users.id";
                string fn__10096()
                {
                    return "col helper";
                }
                test___69.Assert(t___10102, (S1::Func<string>) fn__10096);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__1787()
        {
            T::Test test___70 = new T::Test();
            try
            {
                SqlFragment onCond__1175 = S0::SrcGlobal.Col(sid__546("users"), sid__546("id"));
                SqlBuilder b__1176 = new SqlBuilder();
                b__1176.AppendFragment(onCond__1175);
                b__1176.AppendSafe(" = ");
                b__1176.AppendFragment(S0::SrcGlobal.Col(sid__546("orders"), sid__546("user_id")));
                ISafeIdentifier t___10087 = sid__546("users");
                ISafeIdentifier t___10088 = sid__546("orders");
                SqlFragment t___10089 = b__1176.Accumulated;
                Query q__1177 = S0::SrcGlobal.From(t___10087).InnerJoin(t___10088, t___10089);
                bool t___10094 = q__1177.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__10076()
                {
                    return "join with col";
                }
                test___70.Assert(t___10094, (S1::Func<string>) fn__10076);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__1788()
        {
            T::Test test___71 = new T::Test();
            try
            {
                ISafeIdentifier t___10065 = sid__546("users");
                SqlBuilder t___10066 = new SqlBuilder();
                t___10066.AppendSafe("status = ");
                t___10066.AppendString("active");
                SqlFragment t___10069 = t___10066.Accumulated;
                Query q__1179 = S0::SrcGlobal.From(t___10065).OrWhere(t___10069);
                bool t___10074 = q__1179.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__10064()
                {
                    return "orWhere basic";
                }
                test___71.Assert(t___10074, (S1::Func<string>) fn__10064);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__1790()
        {
            T::Test test___72 = new T::Test();
            try
            {
                ISafeIdentifier t___10048 = sid__546("users");
                SqlBuilder t___10049 = new SqlBuilder();
                t___10049.AppendSafe("age > ");
                t___10049.AppendInt32(18);
                SqlFragment t___10052 = t___10049.Accumulated;
                Query t___10053 = S0::SrcGlobal.From(t___10048).Where(t___10052);
                SqlBuilder t___10054 = new SqlBuilder();
                t___10054.AppendSafe("vip = ");
                t___10054.AppendBoolean(true);
                Query q__1181 = t___10053.OrWhere(t___10054.Accumulated);
                bool t___10062 = q__1181.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__10047()
                {
                    return "where then orWhere";
                }
                test___72.Assert(t___10062, (S1::Func<string>) fn__10047);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__1793()
        {
            T::Test test___73 = new T::Test();
            try
            {
                ISafeIdentifier t___10026 = sid__546("users");
                SqlBuilder t___10027 = new SqlBuilder();
                t___10027.AppendSafe("active = ");
                t___10027.AppendBoolean(true);
                SqlFragment t___10030 = t___10027.Accumulated;
                Query t___10031 = S0::SrcGlobal.From(t___10026).Where(t___10030);
                SqlBuilder t___10032 = new SqlBuilder();
                t___10032.AppendSafe("role = ");
                t___10032.AppendString("admin");
                Query t___10036 = t___10031.OrWhere(t___10032.Accumulated);
                SqlBuilder t___10037 = new SqlBuilder();
                t___10037.AppendSafe("role = ");
                t___10037.AppendString("moderator");
                Query q__1183 = t___10036.OrWhere(t___10037.Accumulated);
                bool t___10045 = q__1183.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__10025()
                {
                    return "multiple orWhere";
                }
                test___73.Assert(t___10045, (S1::Func<string>) fn__10025);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__1797()
        {
            T::Test test___74 = new T::Test();
            try
            {
                ISafeIdentifier t___10004 = sid__546("users");
                SqlBuilder t___10005 = new SqlBuilder();
                t___10005.AppendSafe("age > ");
                t___10005.AppendInt32(18);
                SqlFragment t___10008 = t___10005.Accumulated;
                Query t___10009 = S0::SrcGlobal.From(t___10004).Where(t___10008);
                SqlBuilder t___10010 = new SqlBuilder();
                t___10010.AppendSafe("active = ");
                t___10010.AppendBoolean(true);
                Query t___10014 = t___10009.Where(t___10010.Accumulated);
                SqlBuilder t___10015 = new SqlBuilder();
                t___10015.AppendSafe("vip = ");
                t___10015.AppendBoolean(true);
                Query q__1185 = t___10014.OrWhere(t___10015.Accumulated);
                bool t___10023 = q__1185.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__10003()
                {
                    return "mixed where and orWhere";
                }
                test___74.Assert(t___10023, (S1::Func<string>) fn__10003);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__1801()
        {
            T::Test test___75 = new T::Test();
            try
            {
                ISafeIdentifier t___9995 = sid__546("users");
                ISafeIdentifier t___9996 = sid__546("deleted_at");
                Query q__1187 = S0::SrcGlobal.From(t___9995).WhereNull(t___9996);
                bool t___10001 = q__1187.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__9994()
                {
                    return "whereNull";
                }
                test___75.Assert(t___10001, (S1::Func<string>) fn__9994);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__1802()
        {
            T::Test test___76 = new T::Test();
            try
            {
                ISafeIdentifier t___9986 = sid__546("users");
                ISafeIdentifier t___9987 = sid__546("email");
                Query q__1189 = S0::SrcGlobal.From(t___9986).WhereNotNull(t___9987);
                bool t___9992 = q__1189.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__9985()
                {
                    return "whereNotNull";
                }
                test___76.Assert(t___9992, (S1::Func<string>) fn__9985);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__1803()
        {
            T::Test test___77 = new T::Test();
            try
            {
                ISafeIdentifier t___9972 = sid__546("users");
                SqlBuilder t___9973 = new SqlBuilder();
                t___9973.AppendSafe("active = ");
                t___9973.AppendBoolean(true);
                SqlFragment t___9976 = t___9973.Accumulated;
                Query q__1191 = S0::SrcGlobal.From(t___9972).Where(t___9976).WhereNull(sid__546("deleted_at"));
                bool t___9983 = q__1191.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__9971()
                {
                    return "whereNull chained";
                }
                test___77.Assert(t___9983, (S1::Func<string>) fn__9971);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__1805()
        {
            T::Test test___78 = new T::Test();
            try
            {
                ISafeIdentifier t___9958 = sid__546("users");
                ISafeIdentifier t___9959 = sid__546("deleted_at");
                Query t___9960 = S0::SrcGlobal.From(t___9958).WhereNull(t___9959);
                SqlBuilder t___9961 = new SqlBuilder();
                t___9961.AppendSafe("role = ");
                t___9961.AppendString("admin");
                Query q__1193 = t___9960.OrWhere(t___9961.Accumulated);
                bool t___9969 = q__1193.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__9957()
                {
                    return "whereNotNull with orWhere";
                }
                test___78.Assert(t___9969, (S1::Func<string>) fn__9957);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__1807()
        {
            T::Test test___79 = new T::Test();
            try
            {
                ISafeIdentifier t___9946 = sid__546("users");
                ISafeIdentifier t___9947 = sid__546("id");
                SqlInt32 t___9948 = new SqlInt32(1);
                SqlInt32 t___9949 = new SqlInt32(2);
                SqlInt32 t___9950 = new SqlInt32(3);
                Query q__1195 = S0::SrcGlobal.From(t___9946).WhereIn(t___9947, C::Listed.CreateReadOnlyList<SqlInt32>(t___9948, t___9949, t___9950));
                bool t___9955 = q__1195.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__9945()
                {
                    return "whereIn ints";
                }
                test___79.Assert(t___9955, (S1::Func<string>) fn__9945);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__1808()
        {
            T::Test test___80 = new T::Test();
            try
            {
                ISafeIdentifier t___9935 = sid__546("users");
                ISafeIdentifier t___9936 = sid__546("name");
                SqlString t___9937 = new SqlString("Alice");
                SqlString t___9938 = new SqlString("Bob's");
                Query q__1197 = S0::SrcGlobal.From(t___9935).WhereIn(t___9936, C::Listed.CreateReadOnlyList<SqlString>(t___9937, t___9938));
                bool t___9943 = q__1197.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__9934()
                {
                    return "whereIn strings";
                }
                test___80.Assert(t___9943, (S1::Func<string>) fn__9934);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__1809()
        {
            T::Test test___81 = new T::Test();
            try
            {
                ISafeIdentifier t___9926 = sid__546("users");
                ISafeIdentifier t___9927 = sid__546("id");
                Query q__1199 = S0::SrcGlobal.From(t___9926).WhereIn(t___9927, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___9932 = q__1199.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__9925()
                {
                    return "whereIn empty";
                }
                test___81.Assert(t___9932, (S1::Func<string>) fn__9925);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__1810()
        {
            T::Test test___82 = new T::Test();
            try
            {
                ISafeIdentifier t___9910 = sid__546("users");
                SqlBuilder t___9911 = new SqlBuilder();
                t___9911.AppendSafe("active = ");
                t___9911.AppendBoolean(true);
                SqlFragment t___9914 = t___9911.Accumulated;
                Query q__1201 = S0::SrcGlobal.From(t___9910).Where(t___9914).WhereIn(sid__546("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___9923 = q__1201.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__9909()
                {
                    return "whereIn chained";
                }
                test___82.Assert(t___9923, (S1::Func<string>) fn__9909);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__1812()
        {
            T::Test test___83 = new T::Test();
            try
            {
                ISafeIdentifier t___9900 = sid__546("users");
                ISafeIdentifier t___9901 = sid__546("id");
                SqlInt32 t___9902 = new SqlInt32(42);
                Query q__1203 = S0::SrcGlobal.From(t___9900).WhereIn(t___9901, C::Listed.CreateReadOnlyList<SqlInt32>(t___9902));
                bool t___9907 = q__1203.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__9899()
                {
                    return "whereIn single";
                }
                test___83.Assert(t___9907, (S1::Func<string>) fn__9899);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__1813()
        {
            T::Test test___84 = new T::Test();
            try
            {
                ISafeIdentifier t___9888 = sid__546("users");
                SqlBuilder t___9889 = new SqlBuilder();
                t___9889.AppendSafe("active = ");
                t___9889.AppendBoolean(true);
                SqlFragment t___9892 = t___9889.Accumulated;
                Query q__1205 = S0::SrcGlobal.From(t___9888).WhereNot(t___9892);
                bool t___9897 = q__1205.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__9887()
                {
                    return "whereNot";
                }
                test___84.Assert(t___9897, (S1::Func<string>) fn__9887);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__1815()
        {
            T::Test test___85 = new T::Test();
            try
            {
                ISafeIdentifier t___9871 = sid__546("users");
                SqlBuilder t___9872 = new SqlBuilder();
                t___9872.AppendSafe("age > ");
                t___9872.AppendInt32(18);
                SqlFragment t___9875 = t___9872.Accumulated;
                Query t___9876 = S0::SrcGlobal.From(t___9871).Where(t___9875);
                SqlBuilder t___9877 = new SqlBuilder();
                t___9877.AppendSafe("banned = ");
                t___9877.AppendBoolean(true);
                Query q__1207 = t___9876.WhereNot(t___9877.Accumulated);
                bool t___9885 = q__1207.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__9870()
                {
                    return "whereNot chained";
                }
                test___85.Assert(t___9885, (S1::Func<string>) fn__9870);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__1818()
        {
            T::Test test___86 = new T::Test();
            try
            {
                ISafeIdentifier t___9860 = sid__546("users");
                ISafeIdentifier t___9861 = sid__546("age");
                SqlInt32 t___9862 = new SqlInt32(18);
                SqlInt32 t___9863 = new SqlInt32(65);
                Query q__1209 = S0::SrcGlobal.From(t___9860).WhereBetween(t___9861, t___9862, t___9863);
                bool t___9868 = q__1209.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__9859()
                {
                    return "whereBetween ints";
                }
                test___86.Assert(t___9868, (S1::Func<string>) fn__9859);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__1819()
        {
            T::Test test___87 = new T::Test();
            try
            {
                ISafeIdentifier t___9844 = sid__546("users");
                SqlBuilder t___9845 = new SqlBuilder();
                t___9845.AppendSafe("active = ");
                t___9845.AppendBoolean(true);
                SqlFragment t___9848 = t___9845.Accumulated;
                Query q__1211 = S0::SrcGlobal.From(t___9844).Where(t___9848).WhereBetween(sid__546("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___9857 = q__1211.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__9843()
                {
                    return "whereBetween chained";
                }
                test___87.Assert(t___9857, (S1::Func<string>) fn__9843);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__1821()
        {
            T::Test test___88 = new T::Test();
            try
            {
                ISafeIdentifier t___9835 = sid__546("users");
                ISafeIdentifier t___9836 = sid__546("name");
                Query q__1213 = S0::SrcGlobal.From(t___9835).WhereLike(t___9836, "John%");
                bool t___9841 = q__1213.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__9834()
                {
                    return "whereLike";
                }
                test___88.Assert(t___9841, (S1::Func<string>) fn__9834);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__1822()
        {
            T::Test test___89 = new T::Test();
            try
            {
                ISafeIdentifier t___9826 = sid__546("users");
                ISafeIdentifier t___9827 = sid__546("email");
                Query q__1215 = S0::SrcGlobal.From(t___9826).WhereILike(t___9827, "%@gmail.com");
                bool t___9832 = q__1215.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__9825()
                {
                    return "whereILike";
                }
                test___89.Assert(t___9832, (S1::Func<string>) fn__9825);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__1823()
        {
            T::Test test___90 = new T::Test();
            try
            {
                ISafeIdentifier t___9812 = sid__546("users");
                ISafeIdentifier t___9813 = sid__546("name");
                Query q__1217 = S0::SrcGlobal.From(t___9812).WhereLike(t___9813, "'; DROP TABLE users; --");
                string s__1218 = q__1217.ToSql().ToString();
                bool t___9818 = s__1218.IndexOf("''") >= 0;
                string fn__9811()
                {
                    return "like injection escaped: " + s__1218;
                }
                test___90.Assert(t___9818, (S1::Func<string>) fn__9811);
                bool t___9822 = s__1218.IndexOf("LIKE") >= 0;
                string fn__9810()
                {
                    return "like structure intact: " + s__1218;
                }
                test___90.Assert(t___9822, (S1::Func<string>) fn__9810);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__1824()
        {
            T::Test test___91 = new T::Test();
            try
            {
                ISafeIdentifier t___9802 = sid__546("users");
                ISafeIdentifier t___9803 = sid__546("name");
                Query q__1220 = S0::SrcGlobal.From(t___9802).WhereLike(t___9803, "%son%");
                bool t___9808 = q__1220.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__9801()
                {
                    return "whereLike wildcard";
                }
                test___91.Assert(t___9808, (S1::Func<string>) fn__9801);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__1825()
        {
            T::Test test___92 = new T::Test();
            try
            {
                SqlFragment f__1222 = S0::SrcGlobal.CountAll();
                bool t___9799 = f__1222.ToString() == "COUNT(*)";
                string fn__9795()
                {
                    return "countAll";
                }
                test___92.Assert(t___9799, (S1::Func<string>) fn__9795);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__1826()
        {
            T::Test test___93 = new T::Test();
            try
            {
                SqlFragment f__1224 = S0::SrcGlobal.CountCol(sid__546("id"));
                bool t___9793 = f__1224.ToString() == "COUNT(id)";
                string fn__9788()
                {
                    return "countCol";
                }
                test___93.Assert(t___9793, (S1::Func<string>) fn__9788);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__1827()
        {
            T::Test test___94 = new T::Test();
            try
            {
                SqlFragment f__1226 = S0::SrcGlobal.SumCol(sid__546("amount"));
                bool t___9786 = f__1226.ToString() == "SUM(amount)";
                string fn__9781()
                {
                    return "sumCol";
                }
                test___94.Assert(t___9786, (S1::Func<string>) fn__9781);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__1828()
        {
            T::Test test___95 = new T::Test();
            try
            {
                SqlFragment f__1228 = S0::SrcGlobal.AvgCol(sid__546("price"));
                bool t___9779 = f__1228.ToString() == "AVG(price)";
                string fn__9774()
                {
                    return "avgCol";
                }
                test___95.Assert(t___9779, (S1::Func<string>) fn__9774);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__1829()
        {
            T::Test test___96 = new T::Test();
            try
            {
                SqlFragment f__1230 = S0::SrcGlobal.MinCol(sid__546("created_at"));
                bool t___9772 = f__1230.ToString() == "MIN(created_at)";
                string fn__9767()
                {
                    return "minCol";
                }
                test___96.Assert(t___9772, (S1::Func<string>) fn__9767);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__1830()
        {
            T::Test test___97 = new T::Test();
            try
            {
                SqlFragment f__1232 = S0::SrcGlobal.MaxCol(sid__546("score"));
                bool t___9765 = f__1232.ToString() == "MAX(score)";
                string fn__9760()
                {
                    return "maxCol";
                }
                test___97.Assert(t___9765, (S1::Func<string>) fn__9760);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__1831()
        {
            T::Test test___98 = new T::Test();
            try
            {
                ISafeIdentifier t___9752 = sid__546("orders");
                SqlFragment t___9753 = S0::SrcGlobal.CountAll();
                Query q__1234 = S0::SrcGlobal.From(t___9752).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9753));
                bool t___9758 = q__1234.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__9751()
                {
                    return "selectExpr count";
                }
                test___98.Assert(t___9758, (S1::Func<string>) fn__9751);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__1832()
        {
            T::Test test___99 = new T::Test();
            try
            {
                SqlFragment nameFrag__1236 = S0::SrcGlobal.Col(sid__546("users"), sid__546("name"));
                ISafeIdentifier t___9743 = sid__546("users");
                SqlFragment t___9744 = S0::SrcGlobal.CountAll();
                Query q__1237 = S0::SrcGlobal.From(t___9743).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1236, t___9744));
                bool t___9749 = q__1237.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__9739()
                {
                    return "selectExpr multi";
                }
                test___99.Assert(t___9749, (S1::Func<string>) fn__9739);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__1833()
        {
            T::Test test___100 = new T::Test();
            try
            {
                ISafeIdentifier t___9728 = sid__546("users");
                ISafeIdentifier t___9729 = sid__546("id");
                ISafeIdentifier t___9730 = sid__546("name");
                Query q__1239 = S0::SrcGlobal.From(t___9728).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9729, t___9730)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___9737 = q__1239.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__9727()
                {
                    return "selectExpr overrides select";
                }
                test___100.Assert(t___9737, (S1::Func<string>) fn__9727);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__1834()
        {
            T::Test test___101 = new T::Test();
            try
            {
                ISafeIdentifier t___9714 = sid__546("orders");
                SqlFragment t___9717 = S0::SrcGlobal.Col(sid__546("orders"), sid__546("status"));
                SqlFragment t___9718 = S0::SrcGlobal.CountAll();
                Query q__1241 = S0::SrcGlobal.From(t___9714).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9717, t___9718)).GroupBy(sid__546("status"));
                bool t___9725 = q__1241.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__9713()
                {
                    return "groupBy single";
                }
                test___101.Assert(t___9725, (S1::Func<string>) fn__9713);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__1835()
        {
            T::Test test___102 = new T::Test();
            try
            {
                ISafeIdentifier t___9703 = sid__546("orders");
                ISafeIdentifier t___9704 = sid__546("status");
                Query q__1243 = S0::SrcGlobal.From(t___9703).GroupBy(t___9704).GroupBy(sid__546("category"));
                bool t___9711 = q__1243.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__9702()
                {
                    return "groupBy multiple";
                }
                test___102.Assert(t___9711, (S1::Func<string>) fn__9702);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__1836()
        {
            T::Test test___103 = new T::Test();
            try
            {
                ISafeIdentifier t___9684 = sid__546("orders");
                SqlFragment t___9687 = S0::SrcGlobal.Col(sid__546("orders"), sid__546("status"));
                SqlFragment t___9688 = S0::SrcGlobal.CountAll();
                Query t___9691 = S0::SrcGlobal.From(t___9684).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9687, t___9688)).GroupBy(sid__546("status"));
                SqlBuilder t___9692 = new SqlBuilder();
                t___9692.AppendSafe("COUNT(*) > ");
                t___9692.AppendInt32(5);
                Query q__1245 = t___9691.Having(t___9692.Accumulated);
                bool t___9700 = q__1245.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__9683()
                {
                    return "having basic";
                }
                test___103.Assert(t___9700, (S1::Func<string>) fn__9683);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__1838()
        {
            T::Test test___104 = new T::Test();
            try
            {
                ISafeIdentifier t___9665 = sid__546("orders");
                ISafeIdentifier t___9666 = sid__546("status");
                Query t___9667 = S0::SrcGlobal.From(t___9665).GroupBy(t___9666);
                SqlBuilder t___9668 = new SqlBuilder();
                t___9668.AppendSafe("COUNT(*) > ");
                t___9668.AppendInt32(5);
                Query t___9672 = t___9667.Having(t___9668.Accumulated);
                SqlBuilder t___9673 = new SqlBuilder();
                t___9673.AppendSafe("SUM(total) > ");
                t___9673.AppendInt32(1000);
                Query q__1247 = t___9672.OrHaving(t___9673.Accumulated);
                bool t___9681 = q__1247.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__9664()
                {
                    return "orHaving";
                }
                test___104.Assert(t___9681, (S1::Func<string>) fn__9664);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__1841()
        {
            T::Test test___105 = new T::Test();
            try
            {
                ISafeIdentifier t___9655 = sid__546("users");
                ISafeIdentifier t___9656 = sid__546("name");
                Query q__1249 = S0::SrcGlobal.From(t___9655).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9656)).Distinct();
                bool t___9662 = q__1249.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__9654()
                {
                    return "distinct";
                }
                test___105.Assert(t___9662, (S1::Func<string>) fn__9654);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__1842()
        {
            T::Test test___106 = new T::Test();
            try
            {
                ISafeIdentifier t___9640 = sid__546("users");
                ISafeIdentifier t___9641 = sid__546("email");
                Query t___9642 = S0::SrcGlobal.From(t___9640).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9641));
                SqlBuilder t___9643 = new SqlBuilder();
                t___9643.AppendSafe("active = ");
                t___9643.AppendBoolean(true);
                Query q__1251 = t___9642.Where(t___9643.Accumulated).Distinct();
                bool t___9652 = q__1251.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__9639()
                {
                    return "distinct with where";
                }
                test___106.Assert(t___9652, (S1::Func<string>) fn__9639);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__1844()
        {
            T::Test test___107 = new T::Test();
            try
            {
                Query q__1253 = S0::SrcGlobal.From(sid__546("users"));
                bool t___9637 = q__1253.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__9632()
                {
                    return "countSql bare";
                }
                test___107.Assert(t___9637, (S1::Func<string>) fn__9632);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__1845()
        {
            T::Test test___108 = new T::Test();
            try
            {
                ISafeIdentifier t___9621 = sid__546("users");
                SqlBuilder t___9622 = new SqlBuilder();
                t___9622.AppendSafe("active = ");
                t___9622.AppendBoolean(true);
                SqlFragment t___9625 = t___9622.Accumulated;
                Query q__1255 = S0::SrcGlobal.From(t___9621).Where(t___9625);
                bool t___9630 = q__1255.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__9620()
                {
                    return "countSql with where";
                }
                test___108.Assert(t___9630, (S1::Func<string>) fn__9620);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__1847()
        {
            T::Test test___109 = new T::Test();
            try
            {
                ISafeIdentifier t___9604 = sid__546("users");
                ISafeIdentifier t___9605 = sid__546("orders");
                SqlBuilder t___9606 = new SqlBuilder();
                t___9606.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9608 = t___9606.Accumulated;
                Query t___9609 = S0::SrcGlobal.From(t___9604).InnerJoin(t___9605, t___9608);
                SqlBuilder t___9610 = new SqlBuilder();
                t___9610.AppendSafe("orders.total > ");
                t___9610.AppendInt32(100);
                Query q__1257 = t___9609.Where(t___9610.Accumulated);
                bool t___9618 = q__1257.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__9603()
                {
                    return "countSql with join";
                }
                test___109.Assert(t___9618, (S1::Func<string>) fn__9603);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__1850()
        {
            T::Test test___110 = new T::Test();
            try
            {
                ISafeIdentifier t___9590 = sid__546("users");
                SqlBuilder t___9591 = new SqlBuilder();
                t___9591.AppendSafe("active = ");
                t___9591.AppendBoolean(true);
                SqlFragment t___9594 = t___9591.Accumulated;
                Query t___4906;
                t___4906 = S0::SrcGlobal.From(t___9590).Where(t___9594).OrderBy(sid__546("name"), true).Limit(10);
                Query t___4907;
                t___4907 = t___4906.Offset(20);
                Query q__1259 = t___4907;
                string s__1260 = q__1259.CountSql().ToString();
                bool t___9601 = s__1260 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__9589()
                {
                    return "countSql drops extras: " + s__1260;
                }
                test___110.Assert(t___9601, (S1::Func<string>) fn__9589);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__1852()
        {
            T::Test test___111 = new T::Test();
            try
            {
                ISafeIdentifier t___9557 = sid__546("orders");
                SqlFragment t___9560 = S0::SrcGlobal.Col(sid__546("orders"), sid__546("status"));
                SqlFragment t___9561 = S0::SrcGlobal.CountAll();
                SqlFragment t___9563 = S0::SrcGlobal.SumCol(sid__546("total"));
                Query t___9564 = S0::SrcGlobal.From(t___9557).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9560, t___9561, t___9563));
                ISafeIdentifier t___9565 = sid__546("users");
                SqlBuilder t___9566 = new SqlBuilder();
                t___9566.AppendSafe("orders.user_id = users.id");
                Query t___9569 = t___9564.InnerJoin(t___9565, t___9566.Accumulated);
                SqlBuilder t___9570 = new SqlBuilder();
                t___9570.AppendSafe("users.active = ");
                t___9570.AppendBoolean(true);
                Query t___9576 = t___9569.Where(t___9570.Accumulated).GroupBy(sid__546("status"));
                SqlBuilder t___9577 = new SqlBuilder();
                t___9577.AppendSafe("COUNT(*) > ");
                t___9577.AppendInt32(3);
                Query q__1262 = t___9576.Having(t___9577.Accumulated).OrderBy(sid__546("status"), true);
                string expected__1263 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___9587 = q__1262.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__9556()
                {
                    return "full aggregation";
                }
                test___111.Assert(t___9587, (S1::Func<string>) fn__9556);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionSql__1856()
        {
            T::Test test___112 = new T::Test();
            try
            {
                ISafeIdentifier t___9539 = sid__546("users");
                SqlBuilder t___9540 = new SqlBuilder();
                t___9540.AppendSafe("role = ");
                t___9540.AppendString("admin");
                SqlFragment t___9543 = t___9540.Accumulated;
                Query a__1265 = S0::SrcGlobal.From(t___9539).Where(t___9543);
                ISafeIdentifier t___9545 = sid__546("users");
                SqlBuilder t___9546 = new SqlBuilder();
                t___9546.AppendSafe("role = ");
                t___9546.AppendString("moderator");
                SqlFragment t___9549 = t___9546.Accumulated;
                Query b__1266 = S0::SrcGlobal.From(t___9545).Where(t___9549);
                string s__1267 = S0::SrcGlobal.UnionSql(a__1265, b__1266).ToString();
                bool t___9554 = s__1267 == "(SELECT * FROM users WHERE role = 'admin') UNION (SELECT * FROM users WHERE role = 'moderator')";
                string fn__9538()
                {
                    return "unionSql: " + s__1267;
                }
                test___112.Assert(t___9554, (S1::Func<string>) fn__9538);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionAllSql__1859()
        {
            T::Test test___113 = new T::Test();
            try
            {
                ISafeIdentifier t___9527 = sid__546("users");
                ISafeIdentifier t___9528 = sid__546("name");
                Query a__1269 = S0::SrcGlobal.From(t___9527).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9528));
                ISafeIdentifier t___9530 = sid__546("contacts");
                ISafeIdentifier t___9531 = sid__546("name");
                Query b__1270 = S0::SrcGlobal.From(t___9530).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9531));
                string s__1271 = S0::SrcGlobal.UnionAllSql(a__1269, b__1270).ToString();
                bool t___9536 = s__1271 == "(SELECT name FROM users) UNION ALL (SELECT name FROM contacts)";
                string fn__9526()
                {
                    return "unionAllSql: " + s__1271;
                }
                test___113.Assert(t___9536, (S1::Func<string>) fn__9526);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void intersectSql__1860()
        {
            T::Test test___114 = new T::Test();
            try
            {
                ISafeIdentifier t___9515 = sid__546("users");
                ISafeIdentifier t___9516 = sid__546("email");
                Query a__1273 = S0::SrcGlobal.From(t___9515).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9516));
                ISafeIdentifier t___9518 = sid__546("subscribers");
                ISafeIdentifier t___9519 = sid__546("email");
                Query b__1274 = S0::SrcGlobal.From(t___9518).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9519));
                string s__1275 = S0::SrcGlobal.IntersectSql(a__1273, b__1274).ToString();
                bool t___9524 = s__1275 == "(SELECT email FROM users) INTERSECT (SELECT email FROM subscribers)";
                string fn__9514()
                {
                    return "intersectSql: " + s__1275;
                }
                test___114.Assert(t___9524, (S1::Func<string>) fn__9514);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void exceptSql__1861()
        {
            T::Test test___115 = new T::Test();
            try
            {
                ISafeIdentifier t___9503 = sid__546("users");
                ISafeIdentifier t___9504 = sid__546("id");
                Query a__1277 = S0::SrcGlobal.From(t___9503).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9504));
                ISafeIdentifier t___9506 = sid__546("banned");
                ISafeIdentifier t___9507 = sid__546("id");
                Query b__1278 = S0::SrcGlobal.From(t___9506).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9507));
                string s__1279 = S0::SrcGlobal.ExceptSql(a__1277, b__1278).ToString();
                bool t___9512 = s__1279 == "(SELECT id FROM users) EXCEPT (SELECT id FROM banned)";
                string fn__9502()
                {
                    return "exceptSql: " + s__1279;
                }
                test___115.Assert(t___9512, (S1::Func<string>) fn__9502);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void subqueryWithAlias__1862()
        {
            T::Test test___116 = new T::Test();
            try
            {
                ISafeIdentifier t___9488 = sid__546("orders");
                ISafeIdentifier t___9489 = sid__546("user_id");
                Query t___9490 = S0::SrcGlobal.From(t___9488).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9489));
                SqlBuilder t___9491 = new SqlBuilder();
                t___9491.AppendSafe("total > ");
                t___9491.AppendInt32(100);
                Query inner__1281 = t___9490.Where(t___9491.Accumulated);
                string s__1282 = S0::SrcGlobal.Subquery(inner__1281, sid__546("big_orders")).ToString();
                bool t___9500 = s__1282 == "(SELECT user_id FROM orders WHERE total > 100) AS big_orders";
                string fn__9487()
                {
                    return "subquery: " + s__1282;
                }
                test___116.Assert(t___9500, (S1::Func<string>) fn__9487);
            }
            finally
            {
                test___116.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSql__1864()
        {
            T::Test test___117 = new T::Test();
            try
            {
                ISafeIdentifier t___9477 = sid__546("orders");
                SqlBuilder t___9478 = new SqlBuilder();
                t___9478.AppendSafe("orders.user_id = users.id");
                SqlFragment t___9480 = t___9478.Accumulated;
                Query inner__1284 = S0::SrcGlobal.From(t___9477).Where(t___9480);
                string s__1285 = S0::SrcGlobal.ExistsSql(inner__1284).ToString();
                bool t___9485 = s__1285 == "EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__9476()
                {
                    return "existsSql: " + s__1285;
                }
                test___117.Assert(t___9485, (S1::Func<string>) fn__9476);
            }
            finally
            {
                test___117.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubquery__1866()
        {
            T::Test test___118 = new T::Test();
            try
            {
                ISafeIdentifier t___9460 = sid__546("orders");
                ISafeIdentifier t___9461 = sid__546("user_id");
                Query t___9462 = S0::SrcGlobal.From(t___9460).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9461));
                SqlBuilder t___9463 = new SqlBuilder();
                t___9463.AppendSafe("total > ");
                t___9463.AppendInt32(1000);
                Query sub__1287 = t___9462.Where(t___9463.Accumulated);
                ISafeIdentifier t___9468 = sid__546("users");
                ISafeIdentifier t___9469 = sid__546("id");
                Query q__1288 = S0::SrcGlobal.From(t___9468).WhereInSubquery(t___9469, sub__1287);
                string s__1289 = q__1288.ToSql().ToString();
                bool t___9474 = s__1289 == "SELECT * FROM users WHERE id IN (SELECT user_id FROM orders WHERE total > 1000)";
                string fn__9459()
                {
                    return "whereInSubquery: " + s__1289;
                }
                test___118.Assert(t___9474, (S1::Func<string>) fn__9459);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void setOperationWithWhereOnEachSide__1868()
        {
            T::Test test___119 = new T::Test();
            try
            {
                ISafeIdentifier t___9437 = sid__546("users");
                SqlBuilder t___9438 = new SqlBuilder();
                t___9438.AppendSafe("age > ");
                t___9438.AppendInt32(18);
                SqlFragment t___9441 = t___9438.Accumulated;
                Query t___9442 = S0::SrcGlobal.From(t___9437).Where(t___9441);
                SqlBuilder t___9443 = new SqlBuilder();
                t___9443.AppendSafe("active = ");
                t___9443.AppendBoolean(true);
                Query a__1291 = t___9442.Where(t___9443.Accumulated);
                ISafeIdentifier t___9448 = sid__546("users");
                SqlBuilder t___9449 = new SqlBuilder();
                t___9449.AppendSafe("role = ");
                t___9449.AppendString("vip");
                SqlFragment t___9452 = t___9449.Accumulated;
                Query b__1292 = S0::SrcGlobal.From(t___9448).Where(t___9452);
                string s__1293 = S0::SrcGlobal.UnionSql(a__1291, b__1292).ToString();
                bool t___9457 = s__1293 == "(SELECT * FROM users WHERE age > 18 AND active = TRUE) UNION (SELECT * FROM users WHERE role = 'vip')";
                string fn__9436()
                {
                    return "union with where: " + s__1293;
                }
                test___119.Assert(t___9457, (S1::Func<string>) fn__9436);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubqueryChainedWithWhere__1872()
        {
            T::Test test___120 = new T::Test();
            try
            {
                ISafeIdentifier t___9420 = sid__546("orders");
                ISafeIdentifier t___9421 = sid__546("user_id");
                Query sub__1295 = S0::SrcGlobal.From(t___9420).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9421));
                ISafeIdentifier t___9423 = sid__546("users");
                SqlBuilder t___9424 = new SqlBuilder();
                t___9424.AppendSafe("active = ");
                t___9424.AppendBoolean(true);
                SqlFragment t___9427 = t___9424.Accumulated;
                Query q__1296 = S0::SrcGlobal.From(t___9423).Where(t___9427).WhereInSubquery(sid__546("id"), sub__1295);
                string s__1297 = q__1296.ToSql().ToString();
                bool t___9434 = s__1297 == "SELECT * FROM users WHERE active = TRUE AND id IN (SELECT user_id FROM orders)";
                string fn__9419()
                {
                    return "whereInSubquery chained: " + s__1297;
                }
                test___120.Assert(t___9434, (S1::Func<string>) fn__9419);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSqlUsedInWhere__1874()
        {
            T::Test test___121 = new T::Test();
            try
            {
                ISafeIdentifier t___9406 = sid__546("orders");
                SqlBuilder t___9407 = new SqlBuilder();
                t___9407.AppendSafe("orders.user_id = users.id");
                SqlFragment t___9409 = t___9407.Accumulated;
                Query sub__1299 = S0::SrcGlobal.From(t___9406).Where(t___9409);
                ISafeIdentifier t___9411 = sid__546("users");
                SqlFragment t___9412 = S0::SrcGlobal.ExistsSql(sub__1299);
                Query q__1300 = S0::SrcGlobal.From(t___9411).Where(t___9412);
                string s__1301 = q__1300.ToSql().ToString();
                bool t___9417 = s__1301 == "SELECT * FROM users WHERE EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__9405()
                {
                    return "exists in where: " + s__1301;
                }
                test___121.Assert(t___9417, (S1::Func<string>) fn__9405);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBasic__1876()
        {
            T::Test test___122 = new T::Test();
            try
            {
                ISafeIdentifier t___9392 = sid__546("users");
                ISafeIdentifier t___9393 = sid__546("name");
                SqlString t___9394 = new SqlString("Alice");
                UpdateQuery t___9395 = S0::SrcGlobal.Update(t___9392).Set(t___9393, t___9394);
                SqlBuilder t___9396 = new SqlBuilder();
                t___9396.AppendSafe("id = ");
                t___9396.AppendInt32(1);
                SqlFragment t___4728;
                t___4728 = t___9395.Where(t___9396.Accumulated).ToSql();
                SqlFragment q__1303 = t___4728;
                bool t___9403 = q__1303.ToString() == "UPDATE users SET name = 'Alice' WHERE id = 1";
                string fn__9391()
                {
                    return "update basic";
                }
                test___122.Assert(t___9403, (S1::Func<string>) fn__9391);
            }
            finally
            {
                test___122.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleSet__1878()
        {
            T::Test test___123 = new T::Test();
            try
            {
                ISafeIdentifier t___9375 = sid__546("users");
                ISafeIdentifier t___9376 = sid__546("name");
                SqlString t___9377 = new SqlString("Bob");
                UpdateQuery t___9381 = S0::SrcGlobal.Update(t___9375).Set(t___9376, t___9377).Set(sid__546("age"), new SqlInt32(30));
                SqlBuilder t___9382 = new SqlBuilder();
                t___9382.AppendSafe("id = ");
                t___9382.AppendInt32(2);
                SqlFragment t___4713;
                t___4713 = t___9381.Where(t___9382.Accumulated).ToSql();
                SqlFragment q__1305 = t___4713;
                bool t___9389 = q__1305.ToString() == "UPDATE users SET name = 'Bob', age = 30 WHERE id = 2";
                string fn__9374()
                {
                    return "update multi set";
                }
                test___123.Assert(t___9389, (S1::Func<string>) fn__9374);
            }
            finally
            {
                test___123.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleWhere__1880()
        {
            T::Test test___124 = new T::Test();
            try
            {
                ISafeIdentifier t___9356 = sid__546("users");
                ISafeIdentifier t___9357 = sid__546("active");
                SqlBoolean t___9358 = new SqlBoolean(false);
                UpdateQuery t___9359 = S0::SrcGlobal.Update(t___9356).Set(t___9357, t___9358);
                SqlBuilder t___9360 = new SqlBuilder();
                t___9360.AppendSafe("age < ");
                t___9360.AppendInt32(18);
                UpdateQuery t___9364 = t___9359.Where(t___9360.Accumulated);
                SqlBuilder t___9365 = new SqlBuilder();
                t___9365.AppendSafe("role = ");
                t___9365.AppendString("guest");
                SqlFragment t___4695;
                t___4695 = t___9364.Where(t___9365.Accumulated).ToSql();
                SqlFragment q__1307 = t___4695;
                bool t___9372 = q__1307.ToString() == "UPDATE users SET active = FALSE WHERE age < 18 AND role = 'guest'";
                string fn__9355()
                {
                    return "update multi where";
                }
                test___124.Assert(t___9372, (S1::Func<string>) fn__9355);
            }
            finally
            {
                test___124.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryOrWhere__1883()
        {
            T::Test test___125 = new T::Test();
            try
            {
                ISafeIdentifier t___9337 = sid__546("users");
                ISafeIdentifier t___9338 = sid__546("status");
                SqlString t___9339 = new SqlString("banned");
                UpdateQuery t___9340 = S0::SrcGlobal.Update(t___9337).Set(t___9338, t___9339);
                SqlBuilder t___9341 = new SqlBuilder();
                t___9341.AppendSafe("spam_count > ");
                t___9341.AppendInt32(10);
                UpdateQuery t___9345 = t___9340.Where(t___9341.Accumulated);
                SqlBuilder t___9346 = new SqlBuilder();
                t___9346.AppendSafe("reported = ");
                t___9346.AppendBoolean(true);
                SqlFragment t___4674;
                t___4674 = t___9345.OrWhere(t___9346.Accumulated).ToSql();
                SqlFragment q__1309 = t___4674;
                bool t___9353 = q__1309.ToString() == "UPDATE users SET status = 'banned' WHERE spam_count > 10 OR reported = TRUE";
                string fn__9336()
                {
                    return "update orWhere";
                }
                test___125.Assert(t___9353, (S1::Func<string>) fn__9336);
            }
            finally
            {
                test___125.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutWhere__1886()
        {
            T::Test test___126 = new T::Test();
            try
            {
                ISafeIdentifier t___9330;
                ISafeIdentifier t___9331;
                SqlInt32 t___9332;
                bool didBubble__1311;
                try
                {
                    t___9330 = sid__546("users");
                    t___9331 = sid__546("x");
                    t___9332 = new SqlInt32(1);
                    S0::SrcGlobal.Update(t___9330).Set(t___9331, t___9332).ToSql();
                    didBubble__1311 = false;
                }
                catch
                {
                    didBubble__1311 = true;
                }
                string fn__9329()
                {
                    return "update without WHERE should bubble";
                }
                test___126.Assert(didBubble__1311, (S1::Func<string>) fn__9329);
            }
            finally
            {
                test___126.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutSet__1887()
        {
            T::Test test___127 = new T::Test();
            try
            {
                ISafeIdentifier t___9321;
                SqlBuilder t___9322;
                SqlFragment t___9325;
                bool didBubble__1313;
                try
                {
                    t___9321 = sid__546("users");
                    t___9322 = new SqlBuilder();
                    t___9322.AppendSafe("id = ");
                    t___9322.AppendInt32(1);
                    t___9325 = t___9322.Accumulated;
                    S0::SrcGlobal.Update(t___9321).Where(t___9325).ToSql();
                    didBubble__1313 = false;
                }
                catch
                {
                    didBubble__1313 = true;
                }
                string fn__9320()
                {
                    return "update without SET should bubble";
                }
                test___127.Assert(didBubble__1313, (S1::Func<string>) fn__9320);
            }
            finally
            {
                test___127.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryWithLimit__1889()
        {
            T::Test test___128 = new T::Test();
            try
            {
                ISafeIdentifier t___9307 = sid__546("users");
                ISafeIdentifier t___9308 = sid__546("active");
                SqlBoolean t___9309 = new SqlBoolean(false);
                UpdateQuery t___9310 = S0::SrcGlobal.Update(t___9307).Set(t___9308, t___9309);
                SqlBuilder t___9311 = new SqlBuilder();
                t___9311.AppendSafe("last_login < ");
                t___9311.AppendString("2024-01-01");
                UpdateQuery t___4637;
                t___4637 = t___9310.Where(t___9311.Accumulated).Limit(100);
                SqlFragment t___4638;
                t___4638 = t___4637.ToSql();
                SqlFragment q__1315 = t___4638;
                bool t___9318 = q__1315.ToString() == "UPDATE users SET active = FALSE WHERE last_login < '2024-01-01' LIMIT 100";
                string fn__9306()
                {
                    return "update limit";
                }
                test___128.Assert(t___9318, (S1::Func<string>) fn__9306);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryEscaping__1891()
        {
            T::Test test___129 = new T::Test();
            try
            {
                ISafeIdentifier t___9293 = sid__546("users");
                ISafeIdentifier t___9294 = sid__546("bio");
                SqlString t___9295 = new SqlString("It's a test");
                UpdateQuery t___9296 = S0::SrcGlobal.Update(t___9293).Set(t___9294, t___9295);
                SqlBuilder t___9297 = new SqlBuilder();
                t___9297.AppendSafe("id = ");
                t___9297.AppendInt32(1);
                SqlFragment t___4622;
                t___4622 = t___9296.Where(t___9297.Accumulated).ToSql();
                SqlFragment q__1317 = t___4622;
                bool t___9304 = q__1317.ToString() == "UPDATE users SET bio = 'It''s a test' WHERE id = 1";
                string fn__9292()
                {
                    return "update escaping";
                }
                test___129.Assert(t___9304, (S1::Func<string>) fn__9292);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBasic__1893()
        {
            T::Test test___130 = new T::Test();
            try
            {
                ISafeIdentifier t___9282 = sid__546("users");
                SqlBuilder t___9283 = new SqlBuilder();
                t___9283.AppendSafe("id = ");
                t___9283.AppendInt32(1);
                SqlFragment t___9286 = t___9283.Accumulated;
                SqlFragment t___4607;
                t___4607 = S0::SrcGlobal.DeleteFrom(t___9282).Where(t___9286).ToSql();
                SqlFragment q__1319 = t___4607;
                bool t___9290 = q__1319.ToString() == "DELETE FROM users WHERE id = 1";
                string fn__9281()
                {
                    return "delete basic";
                }
                test___130.Assert(t___9290, (S1::Func<string>) fn__9281);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryMultipleWhere__1895()
        {
            T::Test test___131 = new T::Test();
            try
            {
                ISafeIdentifier t___9266 = sid__546("logs");
                SqlBuilder t___9267 = new SqlBuilder();
                t___9267.AppendSafe("created_at < ");
                t___9267.AppendString("2024-01-01");
                SqlFragment t___9270 = t___9267.Accumulated;
                DeleteQuery t___9271 = S0::SrcGlobal.DeleteFrom(t___9266).Where(t___9270);
                SqlBuilder t___9272 = new SqlBuilder();
                t___9272.AppendSafe("level = ");
                t___9272.AppendString("debug");
                SqlFragment t___4595;
                t___4595 = t___9271.Where(t___9272.Accumulated).ToSql();
                SqlFragment q__1321 = t___4595;
                bool t___9279 = q__1321.ToString() == "DELETE FROM logs WHERE created_at < '2024-01-01' AND level = 'debug'";
                string fn__9265()
                {
                    return "delete multi where";
                }
                test___131.Assert(t___9279, (S1::Func<string>) fn__9265);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBubblesWithoutWhere__1898()
        {
            T::Test test___132 = new T::Test();
            try
            {
                bool didBubble__1323;
                try
                {
                    S0::SrcGlobal.DeleteFrom(sid__546("users")).ToSql();
                    didBubble__1323 = false;
                }
                catch
                {
                    didBubble__1323 = true;
                }
                string fn__9261()
                {
                    return "delete without WHERE should bubble";
                }
                test___132.Assert(didBubble__1323, (S1::Func<string>) fn__9261);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryOrWhere__1899()
        {
            T::Test test___133 = new T::Test();
            try
            {
                ISafeIdentifier t___9246 = sid__546("sessions");
                SqlBuilder t___9247 = new SqlBuilder();
                t___9247.AppendSafe("expired = ");
                t___9247.AppendBoolean(true);
                SqlFragment t___9250 = t___9247.Accumulated;
                DeleteQuery t___9251 = S0::SrcGlobal.DeleteFrom(t___9246).Where(t___9250);
                SqlBuilder t___9252 = new SqlBuilder();
                t___9252.AppendSafe("created_at < ");
                t___9252.AppendString("2023-01-01");
                SqlFragment t___4574;
                t___4574 = t___9251.OrWhere(t___9252.Accumulated).ToSql();
                SqlFragment q__1325 = t___4574;
                bool t___9259 = q__1325.ToString() == "DELETE FROM sessions WHERE expired = TRUE OR created_at < '2023-01-01'";
                string fn__9245()
                {
                    return "delete orWhere";
                }
                test___133.Assert(t___9259, (S1::Func<string>) fn__9245);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryWithLimit__1902()
        {
            T::Test test___134 = new T::Test();
            try
            {
                ISafeIdentifier t___9235 = sid__546("logs");
                SqlBuilder t___9236 = new SqlBuilder();
                t___9236.AppendSafe("level = ");
                t___9236.AppendString("debug");
                SqlFragment t___9239 = t___9236.Accumulated;
                DeleteQuery t___4555;
                t___4555 = S0::SrcGlobal.DeleteFrom(t___9235).Where(t___9239).Limit(1000);
                SqlFragment t___4556;
                t___4556 = t___4555.ToSql();
                SqlFragment q__1327 = t___4556;
                bool t___9243 = q__1327.ToString() == "DELETE FROM logs WHERE level = 'debug' LIMIT 1000";
                string fn__9234()
                {
                    return "delete limit";
                }
                test___134.Assert(t___9243, (S1::Func<string>) fn__9234);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsFirst__1904()
        {
            T::Test test___135 = new T::Test();
            try
            {
                ISafeIdentifier t___9225 = sid__546("users");
                ISafeIdentifier t___9226 = sid__546("email");
                NullsFirst t___9227 = new NullsFirst();
                Query q__1329 = S0::SrcGlobal.From(t___9225).OrderByNulls(t___9226, true, t___9227);
                bool t___9232 = q__1329.ToSql().ToString() == "SELECT * FROM users ORDER BY email ASC NULLS FIRST";
                string fn__9224()
                {
                    return "nulls first";
                }
                test___135.Assert(t___9232, (S1::Func<string>) fn__9224);
            }
            finally
            {
                test___135.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByNullsNullsLast__1905()
        {
            T::Test test___136 = new T::Test();
            try
            {
                ISafeIdentifier t___9215 = sid__546("users");
                ISafeIdentifier t___9216 = sid__546("score");
                NullsLast t___9217 = new NullsLast();
                Query q__1331 = S0::SrcGlobal.From(t___9215).OrderByNulls(t___9216, false, t___9217);
                bool t___9222 = q__1331.ToSql().ToString() == "SELECT * FROM users ORDER BY score DESC NULLS LAST";
                string fn__9214()
                {
                    return "nulls last";
                }
                test___136.Assert(t___9222, (S1::Func<string>) fn__9214);
            }
            finally
            {
                test___136.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedOrderByAndOrderByNulls__1906()
        {
            T::Test test___137 = new T::Test();
            try
            {
                ISafeIdentifier t___9203 = sid__546("users");
                ISafeIdentifier t___9204 = sid__546("name");
                Query q__1333 = S0::SrcGlobal.From(t___9203).OrderBy(t___9204, true).OrderByNulls(sid__546("email"), true, new NullsFirst());
                bool t___9212 = q__1333.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC, email ASC NULLS FIRST";
                string fn__9202()
                {
                    return "mixed order";
                }
                test___137.Assert(t___9212, (S1::Func<string>) fn__9202);
            }
            finally
            {
                test___137.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoin__1907()
        {
            T::Test test___138 = new T::Test();
            try
            {
                ISafeIdentifier t___9194 = sid__546("users");
                ISafeIdentifier t___9195 = sid__546("colors");
                Query q__1335 = S0::SrcGlobal.From(t___9194).CrossJoin(t___9195);
                bool t___9200 = q__1335.ToSql().ToString() == "SELECT * FROM users CROSS JOIN colors";
                string fn__9193()
                {
                    return "cross join";
                }
                test___138.Assert(t___9200, (S1::Func<string>) fn__9193);
            }
            finally
            {
                test___138.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void crossJoinCombinedWithOtherJoins__1908()
        {
            T::Test test___139 = new T::Test();
            try
            {
                ISafeIdentifier t___9180 = sid__546("users");
                ISafeIdentifier t___9181 = sid__546("orders");
                SqlBuilder t___9182 = new SqlBuilder();
                t___9182.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9184 = t___9182.Accumulated;
                Query q__1337 = S0::SrcGlobal.From(t___9180).InnerJoin(t___9181, t___9184).CrossJoin(sid__546("colors"));
                bool t___9191 = q__1337.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id CROSS JOIN colors";
                string fn__9179()
                {
                    return "cross + inner join";
                }
                test___139.Assert(t___9191, (S1::Func<string>) fn__9179);
            }
            finally
            {
                test___139.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForUpdate__1910()
        {
            T::Test test___140 = new T::Test();
            try
            {
                ISafeIdentifier t___9166 = sid__546("users");
                SqlBuilder t___9167 = new SqlBuilder();
                t___9167.AppendSafe("id = ");
                t___9167.AppendInt32(1);
                SqlFragment t___9170 = t___9167.Accumulated;
                Query q__1339 = S0::SrcGlobal.From(t___9166).Where(t___9170).Lock(new ForUpdate());
                bool t___9177 = q__1339.ToSql().ToString() == "SELECT * FROM users WHERE id = 1 FOR UPDATE";
                string fn__9165()
                {
                    return "for update";
                }
                test___140.Assert(t___9177, (S1::Func<string>) fn__9165);
            }
            finally
            {
                test___140.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockForShare__1912()
        {
            T::Test test___141 = new T::Test();
            try
            {
                ISafeIdentifier t___9155 = sid__546("users");
                ISafeIdentifier t___9156 = sid__546("name");
                Query q__1341 = S0::SrcGlobal.From(t___9155).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9156)).Lock(new ForShare());
                bool t___9163 = q__1341.ToSql().ToString() == "SELECT name FROM users FOR SHARE";
                string fn__9154()
                {
                    return "for share";
                }
                test___141.Assert(t___9163, (S1::Func<string>) fn__9154);
            }
            finally
            {
                test___141.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lockWithFullQuery__1913()
        {
            T::Test test___142 = new T::Test();
            try
            {
                ISafeIdentifier t___9141 = sid__546("accounts");
                SqlBuilder t___9142 = new SqlBuilder();
                t___9142.AppendSafe("id = ");
                t___9142.AppendInt32(42);
                SqlFragment t___9145 = t___9142.Accumulated;
                Query t___4479;
                t___4479 = S0::SrcGlobal.From(t___9141).Where(t___9145).Limit(1);
                Query t___9148 = t___4479.Lock(new ForUpdate());
                Query q__1343 = t___9148;
                bool t___9152 = q__1343.ToSql().ToString() == "SELECT * FROM accounts WHERE id = 42 LIMIT 1 FOR UPDATE";
                string fn__9140()
                {
                    return "lock full query";
                }
                test___142.Assert(t___9152, (S1::Func<string>) fn__9140);
            }
            finally
            {
                test___142.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__1915()
        {
            T::Test test___149 = new T::Test();
            try
            {
                ISafeIdentifier t___4468;
                t___4468 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1381 = t___4468;
                bool t___9138 = id__1381.SqlValue == "user_name";
                string fn__9135()
                {
                    return "value should round-trip";
                }
                test___149.Assert(t___9138, (S1::Func<string>) fn__9135);
            }
            finally
            {
                test___149.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__1916()
        {
            T::Test test___150 = new T::Test();
            try
            {
                bool didBubble__1383;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1383 = false;
                }
                catch
                {
                    didBubble__1383 = true;
                }
                string fn__9132()
                {
                    return "empty string should bubble";
                }
                test___150.Assert(didBubble__1383, (S1::Func<string>) fn__9132);
            }
            finally
            {
                test___150.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__1917()
        {
            T::Test test___151 = new T::Test();
            try
            {
                bool didBubble__1385;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1385 = false;
                }
                catch
                {
                    didBubble__1385 = true;
                }
                string fn__9129()
                {
                    return "leading digit should bubble";
                }
                test___151.Assert(didBubble__1385, (S1::Func<string>) fn__9129);
            }
            finally
            {
                test___151.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__1918()
        {
            T::Test test___152 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1387 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__9126(string c__1388)
                {
                    bool didBubble__1389;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1388);
                        didBubble__1389 = false;
                    }
                    catch
                    {
                        didBubble__1389 = true;
                    }
                    string fn__9123()
                    {
                        return "should reject: " + c__1388;
                    }
                    test___152.Assert(didBubble__1389, (S1::Func<string>) fn__9123);
                }
                C::Listed.ForEach(cases__1387, (S1::Action<string>) fn__9126);
            }
            finally
            {
                test___152.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__1919()
        {
            T::Test test___153 = new T::Test();
            try
            {
                ISafeIdentifier t___4445;
                t___4445 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___4446 = t___4445;
                ISafeIdentifier t___4447;
                t___4447 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___4448 = t___4447;
                StringField t___9113 = new StringField();
                FieldDef t___9114 = new FieldDef(t___4448, t___9113, false);
                ISafeIdentifier t___4451;
                t___4451 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___4452 = t___4451;
                IntField t___9115 = new IntField();
                FieldDef t___9116 = new FieldDef(t___4452, t___9115, false);
                TableDef td__1391 = new TableDef(t___4446, C::Listed.CreateReadOnlyList<FieldDef>(t___9114, t___9116));
                FieldDef t___4456;
                t___4456 = td__1391.Field("age");
                FieldDef f__1392 = t___4456;
                bool t___9121 = f__1392.Name.SqlValue == "age";
                string fn__9112()
                {
                    return "should find age field";
                }
                test___153.Assert(t___9121, (S1::Func<string>) fn__9112);
            }
            finally
            {
                test___153.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__1920()
        {
            T::Test test___154 = new T::Test();
            try
            {
                ISafeIdentifier t___4436;
                t___4436 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___4437 = t___4436;
                ISafeIdentifier t___4438;
                t___4438 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___4439 = t___4438;
                StringField t___9107 = new StringField();
                FieldDef t___9108 = new FieldDef(t___4439, t___9107, false);
                TableDef td__1394 = new TableDef(t___4437, C::Listed.CreateReadOnlyList<FieldDef>(t___9108));
                bool didBubble__1395;
                try
                {
                    td__1394.Field("nonexistent");
                    didBubble__1395 = false;
                }
                catch
                {
                    didBubble__1395 = true;
                }
                string fn__9106()
                {
                    return "unknown field should bubble";
                }
                test___154.Assert(didBubble__1395, (S1::Func<string>) fn__9106);
            }
            finally
            {
                test___154.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__1921()
        {
            T::Test test___155 = new T::Test();
            try
            {
                ISafeIdentifier t___4424;
                t___4424 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___4425 = t___4424;
                StringField t___9095 = new StringField();
                FieldDef required__1397 = new FieldDef(t___4425, t___9095, false);
                ISafeIdentifier t___4428;
                t___4428 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___4429 = t___4428;
                StringField t___9097 = new StringField();
                FieldDef optional__1398 = new FieldDef(t___4429, t___9097, true);
                bool t___9101 = !required__1397.Nullable;
                string fn__9094()
                {
                    return "required field should not be nullable";
                }
                test___155.Assert(t___9101, (S1::Func<string>) fn__9094);
                bool t___9103 = optional__1398.Nullable;
                string fn__9093()
                {
                    return "optional field should be nullable";
                }
                test___155.Assert(t___9103, (S1::Func<string>) fn__9093);
            }
            finally
            {
                test___155.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__1922()
        {
            T::Test test___159 = new T::Test();
            try
            {
                string build__1524(string name__1526)
                {
                    SqlBuilder t___9075 = new SqlBuilder();
                    t___9075.AppendSafe("select * from hi where name = ");
                    t___9075.AppendString(name__1526);
                    return t___9075.Accumulated.ToString();
                }
                string buildWrong__1525(string name__1528)
                {
                    return "select * from hi where name = '" + name__1528 + "'";
                }
                string actual___1924 = build__1524("world");
                bool t___9085 = actual___1924 == "select * from hi where name = 'world'";
                string fn__9082()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___1924 + ")";
                }
                test___159.Assert(t___9085, (S1::Func<string>) fn__9082);
                string bobbyTables__1530 = "Robert'); drop table hi;--";
                string actual___1926 = build__1524("Robert'); drop table hi;--");
                bool t___9089 = actual___1926 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__9081()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___1926 + ")";
                }
                test___159.Assert(t___9089, (S1::Func<string>) fn__9081);
                string fn__9080()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___159.Assert(true, (S1::Func<string>) fn__9080);
            }
            finally
            {
                test___159.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__1930()
        {
            T::Test test___160 = new T::Test();
            try
            {
                SqlBuilder t___9043 = new SqlBuilder();
                t___9043.AppendSafe("v = ");
                t___9043.AppendString("");
                string actual___1931 = t___9043.Accumulated.ToString();
                bool t___9049 = actual___1931 == "v = ''";
                string fn__9042()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___1931 + ")";
                }
                test___160.Assert(t___9049, (S1::Func<string>) fn__9042);
                SqlBuilder t___9051 = new SqlBuilder();
                t___9051.AppendSafe("v = ");
                t___9051.AppendString("a''b");
                string actual___1934 = t___9051.Accumulated.ToString();
                bool t___9057 = actual___1934 == "v = 'a''''b'";
                string fn__9041()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___1934 + ")";
                }
                test___160.Assert(t___9057, (S1::Func<string>) fn__9041);
                SqlBuilder t___9059 = new SqlBuilder();
                t___9059.AppendSafe("v = ");
                t___9059.AppendString("Hello 世界");
                string actual___1937 = t___9059.Accumulated.ToString();
                bool t___9065 = actual___1937 == "v = 'Hello 世界'";
                string fn__9040()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1937 + ")";
                }
                test___160.Assert(t___9065, (S1::Func<string>) fn__9040);
                SqlBuilder t___9067 = new SqlBuilder();
                t___9067.AppendSafe("v = ");
                t___9067.AppendString("Line1\nLine2");
                string actual___1940 = t___9067.Accumulated.ToString();
                bool t___9073 = actual___1940 == "v = 'Line1\nLine2'";
                string fn__9039()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1940 + ")";
                }
                test___160.Assert(t___9073, (S1::Func<string>) fn__9039);
            }
            finally
            {
                test___160.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1943()
        {
            T::Test test___161 = new T::Test();
            try
            {
                SqlBuilder t___9014 = new SqlBuilder();
                t___9014.AppendSafe("select ");
                t___9014.AppendInt32(42);
                t___9014.AppendSafe(", ");
                t___9014.AppendInt64(43);
                t___9014.AppendSafe(", ");
                t___9014.AppendFloat64(19.99);
                t___9014.AppendSafe(", ");
                t___9014.AppendBoolean(true);
                t___9014.AppendSafe(", ");
                t___9014.AppendBoolean(false);
                string actual___1944 = t___9014.Accumulated.ToString();
                bool t___9028 = actual___1944 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__9013()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1944 + ")";
                }
                test___161.Assert(t___9028, (S1::Func<string>) fn__9013);
                S1::DateTime t___4369;
                t___4369 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1533 = t___4369;
                SqlBuilder t___9030 = new SqlBuilder();
                t___9030.AppendSafe("insert into t values (");
                t___9030.AppendDate(date__1533);
                t___9030.AppendSafe(")");
                string actual___1947 = t___9030.Accumulated.ToString();
                bool t___9037 = actual___1947 == "insert into t values ('2024-12-25')";
                string fn__9012()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1947 + ")";
                }
                test___161.Assert(t___9037, (S1::Func<string>) fn__9012);
            }
            finally
            {
                test___161.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1950()
        {
            T::Test test___162 = new T::Test();
            try
            {
                SqlBuilder t___8958 = new SqlBuilder();
                t___8958.AppendSafe("v IN (");
                t___8958.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___8958.AppendSafe(")");
                string actual___1951 = t___8958.Accumulated.ToString();
                bool t___8965 = actual___1951 == "v IN ('a', 'b', 'c''d')";
                string fn__8957()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1951 + ")";
                }
                test___162.Assert(t___8965, (S1::Func<string>) fn__8957);
                SqlBuilder t___8967 = new SqlBuilder();
                t___8967.AppendSafe("v IN (");
                t___8967.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___8967.AppendSafe(")");
                string actual___1954 = t___8967.Accumulated.ToString();
                bool t___8974 = actual___1954 == "v IN (1, 2, 3)";
                string fn__8956()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1954 + ")";
                }
                test___162.Assert(t___8974, (S1::Func<string>) fn__8956);
                SqlBuilder t___8976 = new SqlBuilder();
                t___8976.AppendSafe("v IN (");
                t___8976.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___8976.AppendSafe(")");
                string actual___1957 = t___8976.Accumulated.ToString();
                bool t___8983 = actual___1957 == "v IN (1, 2)";
                string fn__8955()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1957 + ")";
                }
                test___162.Assert(t___8983, (S1::Func<string>) fn__8955);
                SqlBuilder t___8985 = new SqlBuilder();
                t___8985.AppendSafe("v IN (");
                t___8985.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___8985.AppendSafe(")");
                string actual___1960 = t___8985.Accumulated.ToString();
                bool t___8992 = actual___1960 == "v IN (1.0, 2.0)";
                string fn__8954()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1960 + ")";
                }
                test___162.Assert(t___8992, (S1::Func<string>) fn__8954);
                SqlBuilder t___8994 = new SqlBuilder();
                t___8994.AppendSafe("v IN (");
                t___8994.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___8994.AppendSafe(")");
                string actual___1963 = t___8994.Accumulated.ToString();
                bool t___9001 = actual___1963 == "v IN (TRUE, FALSE)";
                string fn__8953()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1963 + ")";
                }
                test___162.Assert(t___9001, (S1::Func<string>) fn__8953);
                S1::DateTime t___4341;
                t___4341 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___4342 = t___4341;
                S1::DateTime t___4343;
                t___4343 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___4344 = t___4343;
                G::IReadOnlyList<S1::DateTime> dates__1535 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___4342, t___4344);
                SqlBuilder t___9003 = new SqlBuilder();
                t___9003.AppendSafe("v IN (");
                t___9003.AppendDateList(dates__1535);
                t___9003.AppendSafe(")");
                string actual___1966 = t___9003.Accumulated.ToString();
                bool t___9010 = actual___1966 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__8952()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1966 + ")";
                }
                test___162.Assert(t___9010, (S1::Func<string>) fn__8952);
            }
            finally
            {
                test___162.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1969()
        {
            T::Test test___163 = new T::Test();
            try
            {
                double nan__1537;
                nan__1537 = 0.0 / 0.0;
                SqlBuilder t___8944 = new SqlBuilder();
                t___8944.AppendSafe("v = ");
                t___8944.AppendFloat64(nan__1537);
                string actual___1970 = t___8944.Accumulated.ToString();
                bool t___8950 = actual___1970 == "v = NULL";
                string fn__8943()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1970 + ")";
                }
                test___163.Assert(t___8950, (S1::Func<string>) fn__8943);
            }
            finally
            {
                test___163.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1973()
        {
            T::Test test___164 = new T::Test();
            try
            {
                double inf__1539;
                inf__1539 = 1.0 / 0.0;
                SqlBuilder t___8935 = new SqlBuilder();
                t___8935.AppendSafe("v = ");
                t___8935.AppendFloat64(inf__1539);
                string actual___1974 = t___8935.Accumulated.ToString();
                bool t___8941 = actual___1974 == "v = NULL";
                string fn__8934()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1974 + ")";
                }
                test___164.Assert(t___8941, (S1::Func<string>) fn__8934);
            }
            finally
            {
                test___164.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1977()
        {
            T::Test test___165 = new T::Test();
            try
            {
                double ninf__1541;
                ninf__1541 = -1.0 / 0.0;
                SqlBuilder t___8926 = new SqlBuilder();
                t___8926.AppendSafe("v = ");
                t___8926.AppendFloat64(ninf__1541);
                string actual___1978 = t___8926.Accumulated.ToString();
                bool t___8932 = actual___1978 == "v = NULL";
                string fn__8925()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1978 + ")";
                }
                test___165.Assert(t___8932, (S1::Func<string>) fn__8925);
            }
            finally
            {
                test___165.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1981()
        {
            T::Test test___166 = new T::Test();
            try
            {
                SqlBuilder t___8901 = new SqlBuilder();
                t___8901.AppendSafe("v = ");
                t___8901.AppendFloat64(3.14);
                string actual___1982 = t___8901.Accumulated.ToString();
                bool t___8907 = actual___1982 == "v = 3.14";
                string fn__8900()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1982 + ")";
                }
                test___166.Assert(t___8907, (S1::Func<string>) fn__8900);
                SqlBuilder t___8909 = new SqlBuilder();
                t___8909.AppendSafe("v = ");
                t___8909.AppendFloat64(0.0);
                string actual___1985 = t___8909.Accumulated.ToString();
                bool t___8915 = actual___1985 == "v = 0.0";
                string fn__8899()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1985 + ")";
                }
                test___166.Assert(t___8915, (S1::Func<string>) fn__8899);
                SqlBuilder t___8917 = new SqlBuilder();
                t___8917.AppendSafe("v = ");
                t___8917.AppendFloat64(-42.5);
                string actual___1988 = t___8917.Accumulated.ToString();
                bool t___8923 = actual___1988 == "v = -42.5";
                string fn__8898()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1988 + ")";
                }
                test___166.Assert(t___8923, (S1::Func<string>) fn__8898);
            }
            finally
            {
                test___166.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1991()
        {
            T::Test test___167 = new T::Test();
            try
            {
                S1::DateTime t___4237;
                t___4237 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1544 = t___4237;
                SqlBuilder t___8890 = new SqlBuilder();
                t___8890.AppendSafe("v = ");
                t___8890.AppendDate(d__1544);
                string actual___1992 = t___8890.Accumulated.ToString();
                bool t___8896 = actual___1992 == "v = '2024-06-15'";
                string fn__8889()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1992 + ")";
                }
                test___167.Assert(t___8896, (S1::Func<string>) fn__8889);
            }
            finally
            {
                test___167.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1995()
        {
            T::Test test___168 = new T::Test();
            try
            {
                string name__1546 = "Someone";
                SqlBuilder t___8858 = new SqlBuilder();
                t___8858.AppendSafe("where p.last_name = ");
                t___8858.AppendString("Someone");
                SqlFragment condition__1547 = t___8858.Accumulated;
                SqlBuilder t___8862 = new SqlBuilder();
                t___8862.AppendSafe("select p.id from person p ");
                t___8862.AppendFragment(condition__1547);
                string actual___1997 = t___8862.Accumulated.ToString();
                bool t___8868 = actual___1997 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__8857()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1997 + ")";
                }
                test___168.Assert(t___8868, (S1::Func<string>) fn__8857);
                SqlBuilder t___8870 = new SqlBuilder();
                t___8870.AppendSafe("select p.id from person p ");
                t___8870.AppendPart(condition__1547.ToSource());
                string actual___2000 = t___8870.Accumulated.ToString();
                bool t___8877 = actual___2000 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__8856()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___2000 + ")";
                }
                test___168.Assert(t___8877, (S1::Func<string>) fn__8856);
                G::IReadOnlyList<ISqlPart> parts__1548 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___8881 = new SqlBuilder();
                t___8881.AppendSafe("select ");
                t___8881.AppendPartList(parts__1548);
                string actual___2003 = t___8881.Accumulated.ToString();
                bool t___8887 = actual___2003 == "select 'a''b', 3";
                string fn__8855()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___2003 + ")";
                }
                test___168.Assert(t___8887, (S1::Func<string>) fn__8855);
            }
            finally
            {
                test___168.SoftFailToHard();
            }
        }
    }
}
