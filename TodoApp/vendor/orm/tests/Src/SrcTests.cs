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
        internal static ISafeIdentifier csid__342(string name__487)
        {
            ISafeIdentifier t___3188;
            t___3188 = S0::SrcGlobal.SafeIdentifier(name__487);
            return t___3188;
        }
        internal static TableDef userTable__343()
        {
            return new TableDef(csid__342("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__342("name"), new StringField(), false), new FieldDef(csid__342("email"), new StringField(), false), new FieldDef(csid__342("age"), new IntField(), true), new FieldDef(csid__342("score"), new FloatField(), true), new FieldDef(csid__342("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__1020()
        {
            T::Test test___22 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__491 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___5514 = userTable__343();
                ISafeIdentifier t___5515 = csid__342("name");
                ISafeIdentifier t___5516 = csid__342("email");
                IChangeset cs__492 = S0::SrcGlobal.Changeset(t___5514, params__491).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5515, t___5516));
                bool t___5519 = C::Mapped.ContainsKey(cs__492.Changes, "name");
                string fn__5509()
                {
                    return "name should be in changes";
                }
                test___22.Assert(t___5519, (S1::Func<string>) fn__5509);
                bool t___5523 = C::Mapped.ContainsKey(cs__492.Changes, "email");
                string fn__5508()
                {
                    return "email should be in changes";
                }
                test___22.Assert(t___5523, (S1::Func<string>) fn__5508);
                bool t___5529 = !C::Mapped.ContainsKey(cs__492.Changes, "admin");
                string fn__5507()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___22.Assert(t___5529, (S1::Func<string>) fn__5507);
                bool t___5531 = cs__492.IsValid;
                string fn__5506()
                {
                    return "should still be valid";
                }
                test___22.Assert(t___5531, (S1::Func<string>) fn__5506);
            }
            finally
            {
                test___22.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__1021()
        {
            T::Test test___23 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__494 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___5492 = userTable__343();
                ISafeIdentifier t___5493 = csid__342("name");
                IChangeset cs__495 = S0::SrcGlobal.Changeset(t___5492, params__494).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5493)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("email")));
                bool t___5500 = !C::Mapped.ContainsKey(cs__495.Changes, "name");
                string fn__5488()
                {
                    return "name must be excluded by second cast";
                }
                test___23.Assert(t___5500, (S1::Func<string>) fn__5488);
                bool t___5503 = C::Mapped.ContainsKey(cs__495.Changes, "email");
                string fn__5487()
                {
                    return "email should be present";
                }
                test___23.Assert(t___5503, (S1::Func<string>) fn__5487);
            }
            finally
            {
                test___23.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__1022()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__497 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___5474 = userTable__343();
                ISafeIdentifier t___5475 = csid__342("name");
                ISafeIdentifier t___5476 = csid__342("email");
                IChangeset cs__498 = S0::SrcGlobal.Changeset(t___5474, params__497).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5475, t___5476));
                bool t___5481 = !C::Mapped.ContainsKey(cs__498.Changes, "name");
                string fn__5470()
                {
                    return "empty name should not be in changes";
                }
                test___24.Assert(t___5481, (S1::Func<string>) fn__5470);
                bool t___5484 = C::Mapped.ContainsKey(cs__498.Changes, "email");
                string fn__5469()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___5484, (S1::Func<string>) fn__5469);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__1023()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__500 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___5456 = userTable__343();
                ISafeIdentifier t___5457 = csid__342("name");
                IChangeset cs__501 = S0::SrcGlobal.Changeset(t___5456, params__500).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5457)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name")));
                bool t___5461 = cs__501.IsValid;
                string fn__5453()
                {
                    return "should be valid";
                }
                test___25.Assert(t___5461, (S1::Func<string>) fn__5453);
                bool t___5467 = cs__501.Errors.Count == 0;
                string fn__5452()
                {
                    return "no errors expected";
                }
                test___25.Assert(t___5467, (S1::Func<string>) fn__5452);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__1024()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__503 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___5432 = userTable__343();
                ISafeIdentifier t___5433 = csid__342("name");
                IChangeset cs__504 = S0::SrcGlobal.Changeset(t___5432, params__503).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5433)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name")));
                bool t___5439 = !cs__504.IsValid;
                string fn__5430()
                {
                    return "should be invalid";
                }
                test___26.Assert(t___5439, (S1::Func<string>) fn__5430);
                bool t___5444 = cs__504.Errors.Count == 1;
                string fn__5429()
                {
                    return "should have one error";
                }
                test___26.Assert(t___5444, (S1::Func<string>) fn__5429);
                bool t___5450 = cs__504.Errors[0].Field == "name";
                string fn__5428()
                {
                    return "error should name the field";
                }
                test___26.Assert(t___5450, (S1::Func<string>) fn__5428);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__1025()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__506 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___5420 = userTable__343();
                ISafeIdentifier t___5421 = csid__342("name");
                IChangeset cs__507 = S0::SrcGlobal.Changeset(t___5420, params__506).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5421)).ValidateLength(csid__342("name"), 2, 50);
                bool t___5425 = cs__507.IsValid;
                string fn__5417()
                {
                    return "should be valid";
                }
                test___27.Assert(t___5425, (S1::Func<string>) fn__5417);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__1026()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__509 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___5408 = userTable__343();
                ISafeIdentifier t___5409 = csid__342("name");
                IChangeset cs__510 = S0::SrcGlobal.Changeset(t___5408, params__509).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5409)).ValidateLength(csid__342("name"), 2, 50);
                bool t___5415 = !cs__510.IsValid;
                string fn__5405()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___5415, (S1::Func<string>) fn__5405);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__1027()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__512 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___5396 = userTable__343();
                ISafeIdentifier t___5397 = csid__342("name");
                IChangeset cs__513 = S0::SrcGlobal.Changeset(t___5396, params__512).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5397)).ValidateLength(csid__342("name"), 2, 10);
                bool t___5403 = !cs__513.IsValid;
                string fn__5393()
                {
                    return "should be invalid";
                }
                test___29.Assert(t___5403, (S1::Func<string>) fn__5393);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__1028()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__515 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___5385 = userTable__343();
                ISafeIdentifier t___5386 = csid__342("age");
                IChangeset cs__516 = S0::SrcGlobal.Changeset(t___5385, params__515).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5386)).ValidateInt(csid__342("age"));
                bool t___5390 = cs__516.IsValid;
                string fn__5382()
                {
                    return "should be valid";
                }
                test___30.Assert(t___5390, (S1::Func<string>) fn__5382);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__1029()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__518 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___5373 = userTable__343();
                ISafeIdentifier t___5374 = csid__342("age");
                IChangeset cs__519 = S0::SrcGlobal.Changeset(t___5373, params__518).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5374)).ValidateInt(csid__342("age"));
                bool t___5380 = !cs__519.IsValid;
                string fn__5370()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___5380, (S1::Func<string>) fn__5370);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__1030()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__521 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___5362 = userTable__343();
                ISafeIdentifier t___5363 = csid__342("score");
                IChangeset cs__522 = S0::SrcGlobal.Changeset(t___5362, params__521).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5363)).ValidateFloat(csid__342("score"));
                bool t___5367 = cs__522.IsValid;
                string fn__5359()
                {
                    return "should be valid";
                }
                test___32.Assert(t___5367, (S1::Func<string>) fn__5359);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__1031()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__524 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___5351 = userTable__343();
                ISafeIdentifier t___5352 = csid__342("age");
                IChangeset cs__525 = S0::SrcGlobal.Changeset(t___5351, params__524).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5352)).ValidateInt64(csid__342("age"));
                bool t___5356 = cs__525.IsValid;
                string fn__5348()
                {
                    return "should be valid";
                }
                test___33.Assert(t___5356, (S1::Func<string>) fn__5348);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__1032()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__527 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___5339 = userTable__343();
                ISafeIdentifier t___5340 = csid__342("age");
                IChangeset cs__528 = S0::SrcGlobal.Changeset(t___5339, params__527).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5340)).ValidateInt64(csid__342("age"));
                bool t___5346 = !cs__528.IsValid;
                string fn__5336()
                {
                    return "should be invalid";
                }
                test___34.Assert(t___5346, (S1::Func<string>) fn__5336);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__1033()
        {
            T::Test test___35 = new T::Test();
            try
            {
                void fn__5333(string v__530)
                {
                    G::IReadOnlyDictionary<string, string> params__531 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__530)));
                    TableDef t___5325 = userTable__343();
                    ISafeIdentifier t___5326 = csid__342("active");
                    IChangeset cs__532 = S0::SrcGlobal.Changeset(t___5325, params__531).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5326)).ValidateBool(csid__342("active"));
                    bool t___5330 = cs__532.IsValid;
                    string fn__5322()
                    {
                        return "should accept: " + v__530;
                    }
                    test___35.Assert(t___5330, (S1::Func<string>) fn__5322);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__5333);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__1034()
        {
            T::Test test___36 = new T::Test();
            try
            {
                void fn__5319(string v__534)
                {
                    G::IReadOnlyDictionary<string, string> params__535 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__534)));
                    TableDef t___5311 = userTable__343();
                    ISafeIdentifier t___5312 = csid__342("active");
                    IChangeset cs__536 = S0::SrcGlobal.Changeset(t___5311, params__535).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5312)).ValidateBool(csid__342("active"));
                    bool t___5316 = cs__536.IsValid;
                    string fn__5308()
                    {
                        return "should accept: " + v__534;
                    }
                    test___36.Assert(t___5316, (S1::Func<string>) fn__5308);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__5319);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__1035()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__5305(string v__538)
                {
                    G::IReadOnlyDictionary<string, string> params__539 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__538)));
                    TableDef t___5296 = userTable__343();
                    ISafeIdentifier t___5297 = csid__342("active");
                    IChangeset cs__540 = S0::SrcGlobal.Changeset(t___5296, params__539).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5297)).ValidateBool(csid__342("active"));
                    bool t___5303 = !cs__540.IsValid;
                    string fn__5293()
                    {
                        return "should reject ambiguous: " + v__538;
                    }
                    test___37.Assert(t___5303, (S1::Func<string>) fn__5293);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__5305);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__1036()
        {
            T::Test test___38 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__542 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___5281 = userTable__343();
                ISafeIdentifier t___5282 = csid__342("name");
                ISafeIdentifier t___5283 = csid__342("email");
                IChangeset cs__543 = S0::SrcGlobal.Changeset(t___5281, params__542).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5282, t___5283)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name"), csid__342("email")));
                SqlFragment t___2989;
                t___2989 = cs__543.ToInsertSql();
                SqlFragment sqlFrag__544 = t___2989;
                string s__545 = sqlFrag__544.ToString();
                bool t___5290 = s__545.IndexOf("''") >= 0;
                string fn__5277()
                {
                    return "single quote must be doubled: " + s__545;
                }
                test___38.Assert(t___5290, (S1::Func<string>) fn__5277);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__1037()
        {
            T::Test test___39 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__547 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___5261 = userTable__343();
                ISafeIdentifier t___5262 = csid__342("name");
                ISafeIdentifier t___5263 = csid__342("email");
                IChangeset cs__548 = S0::SrcGlobal.Changeset(t___5261, params__547).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5262, t___5263)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name"), csid__342("email")));
                SqlFragment t___2968;
                t___2968 = cs__548.ToInsertSql();
                SqlFragment sqlFrag__549 = t___2968;
                string s__550 = sqlFrag__549.ToString();
                bool t___5270 = s__550.IndexOf("INSERT INTO users") >= 0;
                string fn__5257()
                {
                    return "has INSERT INTO: " + s__550;
                }
                test___39.Assert(t___5270, (S1::Func<string>) fn__5257);
                bool t___5274 = s__550.IndexOf("'Alice'") >= 0;
                string fn__5256()
                {
                    return "has quoted name: " + s__550;
                }
                test___39.Assert(t___5274, (S1::Func<string>) fn__5256);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__1038()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__552 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___5243 = userTable__343();
                ISafeIdentifier t___5244 = csid__342("name");
                ISafeIdentifier t___5245 = csid__342("email");
                ISafeIdentifier t___5246 = csid__342("age");
                IChangeset cs__553 = S0::SrcGlobal.Changeset(t___5243, params__552).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5244, t___5245, t___5246)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name"), csid__342("email")));
                SqlFragment t___2951;
                t___2951 = cs__553.ToInsertSql();
                SqlFragment sqlFrag__554 = t___2951;
                string s__555 = sqlFrag__554.ToString();
                bool t___5253 = s__555.IndexOf("25") >= 0;
                string fn__5238()
                {
                    return "age rendered unquoted: " + s__555;
                }
                test___40.Assert(t___5253, (S1::Func<string>) fn__5238);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__1039()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__557 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___5231 = userTable__343();
                ISafeIdentifier t___5232 = csid__342("name");
                IChangeset cs__558 = S0::SrcGlobal.Changeset(t___5231, params__557).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5232)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name")));
                bool didBubble__559;
                try
                {
                    cs__558.ToInsertSql();
                    didBubble__559 = false;
                }
                catch
                {
                    didBubble__559 = true;
                }
                string fn__5229()
                {
                    return "invalid changeset should bubble";
                }
                test___41.Assert(didBubble__559, (S1::Func<string>) fn__5229);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__1040()
        {
            T::Test test___42 = new T::Test();
            try
            {
                TableDef strictTable__561 = new TableDef(csid__342("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__342("title"), new StringField(), false), new FieldDef(csid__342("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__562 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___5222 = csid__342("body");
                IChangeset cs__563 = S0::SrcGlobal.Changeset(strictTable__561, params__562).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5222));
                bool t___5224 = cs__563.IsValid;
                string fn__5211()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___42.Assert(t___5224, (S1::Func<string>) fn__5211);
                bool didBubble__564;
                try
                {
                    cs__563.ToInsertSql();
                    didBubble__564 = false;
                }
                catch
                {
                    didBubble__564 = true;
                }
                string fn__5210()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___42.Assert(didBubble__564, (S1::Func<string>) fn__5210);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__1041()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__566 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___5201 = userTable__343();
                ISafeIdentifier t___5202 = csid__342("name");
                IChangeset cs__567 = S0::SrcGlobal.Changeset(t___5201, params__566).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5202)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name")));
                SqlFragment t___2911;
                t___2911 = cs__567.ToUpdateSql(42);
                SqlFragment sqlFrag__568 = t___2911;
                string s__569 = sqlFrag__568.ToString();
                bool t___5208 = s__569 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__5198()
                {
                    return "got: " + s__569;
                }
                test___43.Assert(t___5208, (S1::Func<string>) fn__5198);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__1042()
        {
            T::Test test___44 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__571 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___5191 = userTable__343();
                ISafeIdentifier t___5192 = csid__342("name");
                IChangeset cs__572 = S0::SrcGlobal.Changeset(t___5191, params__571).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5192)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__342("name")));
                bool didBubble__573;
                try
                {
                    cs__572.ToUpdateSql(1);
                    didBubble__573 = false;
                }
                catch
                {
                    didBubble__573 = true;
                }
                string fn__5189()
                {
                    return "invalid changeset should bubble";
                }
                test___44.Assert(didBubble__573, (S1::Func<string>) fn__5189);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__344(string name__678)
        {
            ISafeIdentifier t___2781;
            t___2781 = S0::SrcGlobal.SafeIdentifier(name__678);
            return t___2781;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__1079()
        {
            T::Test test___45 = new T::Test();
            try
            {
                Query q__681 = S0::SrcGlobal.From(sid__344("users"));
                bool t___5084 = q__681.ToSql().ToString() == "SELECT * FROM users";
                string fn__5079()
                {
                    return "bare query";
                }
                test___45.Assert(t___5084, (S1::Func<string>) fn__5079);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__1080()
        {
            T::Test test___46 = new T::Test();
            try
            {
                ISafeIdentifier t___5070 = sid__344("users");
                ISafeIdentifier t___5071 = sid__344("id");
                ISafeIdentifier t___5072 = sid__344("name");
                Query q__683 = S0::SrcGlobal.From(t___5070).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___5071, t___5072));
                bool t___5077 = q__683.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__5069()
                {
                    return "select columns";
                }
                test___46.Assert(t___5077, (S1::Func<string>) fn__5069);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__1081()
        {
            T::Test test___47 = new T::Test();
            try
            {
                ISafeIdentifier t___5058 = sid__344("users");
                SqlBuilder t___5059 = new SqlBuilder();
                t___5059.AppendSafe("age > ");
                t___5059.AppendInt32(18);
                SqlFragment t___5062 = t___5059.Accumulated;
                Query q__685 = S0::SrcGlobal.From(t___5058).Where(t___5062);
                bool t___5067 = q__685.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__5057()
                {
                    return "where int";
                }
                test___47.Assert(t___5067, (S1::Func<string>) fn__5057);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__1083()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___5046 = sid__344("users");
                SqlBuilder t___5047 = new SqlBuilder();
                t___5047.AppendSafe("active = ");
                t___5047.AppendBoolean(true);
                SqlFragment t___5050 = t___5047.Accumulated;
                Query q__687 = S0::SrcGlobal.From(t___5046).Where(t___5050);
                bool t___5055 = q__687.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__5045()
                {
                    return "where bool";
                }
                test___48.Assert(t___5055, (S1::Func<string>) fn__5045);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__1085()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___5029 = sid__344("users");
                SqlBuilder t___5030 = new SqlBuilder();
                t___5030.AppendSafe("age > ");
                t___5030.AppendInt32(18);
                SqlFragment t___5033 = t___5030.Accumulated;
                Query t___5034 = S0::SrcGlobal.From(t___5029).Where(t___5033);
                SqlBuilder t___5035 = new SqlBuilder();
                t___5035.AppendSafe("active = ");
                t___5035.AppendBoolean(true);
                Query q__689 = t___5034.Where(t___5035.Accumulated);
                bool t___5043 = q__689.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__5028()
                {
                    return "chained where";
                }
                test___49.Assert(t___5043, (S1::Func<string>) fn__5028);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__1088()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___5020 = sid__344("users");
                ISafeIdentifier t___5021 = sid__344("name");
                Query q__691 = S0::SrcGlobal.From(t___5020).OrderBy(t___5021, true);
                bool t___5026 = q__691.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__5019()
                {
                    return "order asc";
                }
                test___50.Assert(t___5026, (S1::Func<string>) fn__5019);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__1089()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___5011 = sid__344("users");
                ISafeIdentifier t___5012 = sid__344("created_at");
                Query q__693 = S0::SrcGlobal.From(t___5011).OrderBy(t___5012, false);
                bool t___5017 = q__693.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__5010()
                {
                    return "order desc";
                }
                test___51.Assert(t___5017, (S1::Func<string>) fn__5010);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__1090()
        {
            T::Test test___52 = new T::Test();
            try
            {
                Query t___2715;
                t___2715 = S0::SrcGlobal.From(sid__344("users")).Limit(10);
                Query t___2716;
                t___2716 = t___2715.Offset(20);
                Query q__695 = t___2716;
                bool t___5008 = q__695.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__5003()
                {
                    return "limit/offset";
                }
                test___52.Assert(t___5008, (S1::Func<string>) fn__5003);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__1091()
        {
            T::Test test___53 = new T::Test();
            try
            {
                bool didBubble__697;
                try
                {
                    S0::SrcGlobal.From(sid__344("users")).Limit(-1);
                    didBubble__697 = false;
                }
                catch
                {
                    didBubble__697 = true;
                }
                string fn__4999()
                {
                    return "negative limit should bubble";
                }
                test___53.Assert(didBubble__697, (S1::Func<string>) fn__4999);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__1092()
        {
            T::Test test___54 = new T::Test();
            try
            {
                bool didBubble__699;
                try
                {
                    S0::SrcGlobal.From(sid__344("users")).Offset(-1);
                    didBubble__699 = false;
                }
                catch
                {
                    didBubble__699 = true;
                }
                string fn__4995()
                {
                    return "negative offset should bubble";
                }
                test___54.Assert(didBubble__699, (S1::Func<string>) fn__4995);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__1093()
        {
            T::Test test___55 = new T::Test();
            try
            {
                int minAge__701 = 21;
                ISafeIdentifier t___4973 = sid__344("users");
                ISafeIdentifier t___4974 = sid__344("id");
                ISafeIdentifier t___4975 = sid__344("name");
                ISafeIdentifier t___4976 = sid__344("email");
                Query t___4977 = S0::SrcGlobal.From(t___4973).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4974, t___4975, t___4976));
                SqlBuilder t___4978 = new SqlBuilder();
                t___4978.AppendSafe("age >= ");
                t___4978.AppendInt32(21);
                Query t___4982 = t___4977.Where(t___4978.Accumulated);
                SqlBuilder t___4983 = new SqlBuilder();
                t___4983.AppendSafe("active = ");
                t___4983.AppendBoolean(true);
                Query t___2701;
                t___2701 = t___4982.Where(t___4983.Accumulated).OrderBy(sid__344("name"), true).Limit(25);
                Query t___2702;
                t___2702 = t___2701.Offset(0);
                Query q__702 = t___2702;
                bool t___4993 = q__702.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__4972()
                {
                    return "complex query";
                }
                test___55.Assert(t___4993, (S1::Func<string>) fn__4972);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__1096()
        {
            T::Test test___56 = new T::Test();
            try
            {
                Query q__704 = S0::SrcGlobal.From(sid__344("users"));
                SqlFragment t___2678;
                t___2678 = q__704.SafeToSql(100);
                SqlFragment t___2679 = t___2678;
                string s__705 = t___2679.ToString();
                bool t___4970 = s__705 == "SELECT * FROM users LIMIT 100";
                string fn__4966()
                {
                    return "should have limit: " + s__705;
                }
                test___56.Assert(t___4970, (S1::Func<string>) fn__4966);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__1097()
        {
            T::Test test___57 = new T::Test();
            try
            {
                Query t___2670;
                t___2670 = S0::SrcGlobal.From(sid__344("users")).Limit(5);
                Query q__707 = t___2670;
                SqlFragment t___2673;
                t___2673 = q__707.SafeToSql(100);
                SqlFragment t___2674 = t___2673;
                string s__708 = t___2674.ToString();
                bool t___4964 = s__708 == "SELECT * FROM users LIMIT 5";
                string fn__4960()
                {
                    return "explicit limit preserved: " + s__708;
                }
                test___57.Assert(t___4964, (S1::Func<string>) fn__4960);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__1098()
        {
            T::Test test___58 = new T::Test();
            try
            {
                bool didBubble__710;
                try
                {
                    S0::SrcGlobal.From(sid__344("users")).SafeToSql(-1);
                    didBubble__710 = false;
                }
                catch
                {
                    didBubble__710 = true;
                }
                string fn__4956()
                {
                    return "negative defaultLimit should bubble";
                }
                test___58.Assert(didBubble__710, (S1::Func<string>) fn__4956);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__1099()
        {
            T::Test test___59 = new T::Test();
            try
            {
                string evil__712 = "'; DROP TABLE users; --";
                ISafeIdentifier t___4940 = sid__344("users");
                SqlBuilder t___4941 = new SqlBuilder();
                t___4941.AppendSafe("name = ");
                t___4941.AppendString("'; DROP TABLE users; --");
                SqlFragment t___4944 = t___4941.Accumulated;
                Query q__713 = S0::SrcGlobal.From(t___4940).Where(t___4944);
                string s__714 = q__713.ToSql().ToString();
                bool t___4949 = s__714.IndexOf("''") >= 0;
                string fn__4939()
                {
                    return "quotes must be doubled: " + s__714;
                }
                test___59.Assert(t___4949, (S1::Func<string>) fn__4939);
                bool t___4953 = s__714.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__4938()
                {
                    return "structure intact: " + s__714;
                }
                test___59.Assert(t___4953, (S1::Func<string>) fn__4938);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__1101()
        {
            T::Test test___60 = new T::Test();
            try
            {
                string attack__716 = "users; DROP TABLE users; --";
                bool didBubble__717;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__717 = false;
                }
                catch
                {
                    didBubble__717 = true;
                }
                string fn__4935()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___60.Assert(didBubble__717, (S1::Func<string>) fn__4935);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__1102()
        {
            T::Test test___61 = new T::Test();
            try
            {
                ISafeIdentifier t___4924 = sid__344("users");
                ISafeIdentifier t___4925 = sid__344("orders");
                SqlBuilder t___4926 = new SqlBuilder();
                t___4926.AppendSafe("users.id = orders.user_id");
                SqlFragment t___4928 = t___4926.Accumulated;
                Query q__719 = S0::SrcGlobal.From(t___4924).InnerJoin(t___4925, t___4928);
                bool t___4933 = q__719.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__4923()
                {
                    return "inner join";
                }
                test___61.Assert(t___4933, (S1::Func<string>) fn__4923);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__1104()
        {
            T::Test test___62 = new T::Test();
            try
            {
                ISafeIdentifier t___4912 = sid__344("users");
                ISafeIdentifier t___4913 = sid__344("profiles");
                SqlBuilder t___4914 = new SqlBuilder();
                t___4914.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___4916 = t___4914.Accumulated;
                Query q__721 = S0::SrcGlobal.From(t___4912).LeftJoin(t___4913, t___4916);
                bool t___4921 = q__721.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__4911()
                {
                    return "left join";
                }
                test___62.Assert(t___4921, (S1::Func<string>) fn__4911);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__1106()
        {
            T::Test test___63 = new T::Test();
            try
            {
                ISafeIdentifier t___4900 = sid__344("orders");
                ISafeIdentifier t___4901 = sid__344("users");
                SqlBuilder t___4902 = new SqlBuilder();
                t___4902.AppendSafe("orders.user_id = users.id");
                SqlFragment t___4904 = t___4902.Accumulated;
                Query q__723 = S0::SrcGlobal.From(t___4900).RightJoin(t___4901, t___4904);
                bool t___4909 = q__723.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__4899()
                {
                    return "right join";
                }
                test___63.Assert(t___4909, (S1::Func<string>) fn__4899);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__1108()
        {
            T::Test test___64 = new T::Test();
            try
            {
                ISafeIdentifier t___4888 = sid__344("users");
                ISafeIdentifier t___4889 = sid__344("orders");
                SqlBuilder t___4890 = new SqlBuilder();
                t___4890.AppendSafe("users.id = orders.user_id");
                SqlFragment t___4892 = t___4890.Accumulated;
                Query q__725 = S0::SrcGlobal.From(t___4888).FullJoin(t___4889, t___4892);
                bool t___4897 = q__725.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__4887()
                {
                    return "full join";
                }
                test___64.Assert(t___4897, (S1::Func<string>) fn__4887);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__1110()
        {
            T::Test test___65 = new T::Test();
            try
            {
                ISafeIdentifier t___4871 = sid__344("users");
                ISafeIdentifier t___4872 = sid__344("orders");
                SqlBuilder t___4873 = new SqlBuilder();
                t___4873.AppendSafe("users.id = orders.user_id");
                SqlFragment t___4875 = t___4873.Accumulated;
                Query t___4876 = S0::SrcGlobal.From(t___4871).InnerJoin(t___4872, t___4875);
                ISafeIdentifier t___4877 = sid__344("profiles");
                SqlBuilder t___4878 = new SqlBuilder();
                t___4878.AppendSafe("users.id = profiles.user_id");
                Query q__727 = t___4876.LeftJoin(t___4877, t___4878.Accumulated);
                bool t___4885 = q__727.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__4870()
                {
                    return "chained joins";
                }
                test___65.Assert(t___4885, (S1::Func<string>) fn__4870);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__1113()
        {
            T::Test test___66 = new T::Test();
            try
            {
                ISafeIdentifier t___4852 = sid__344("users");
                ISafeIdentifier t___4853 = sid__344("orders");
                SqlBuilder t___4854 = new SqlBuilder();
                t___4854.AppendSafe("users.id = orders.user_id");
                SqlFragment t___4856 = t___4854.Accumulated;
                Query t___4857 = S0::SrcGlobal.From(t___4852).InnerJoin(t___4853, t___4856);
                SqlBuilder t___4858 = new SqlBuilder();
                t___4858.AppendSafe("orders.total > ");
                t___4858.AppendInt32(100);
                Query t___2585;
                t___2585 = t___4857.Where(t___4858.Accumulated).OrderBy(sid__344("name"), true).Limit(10);
                Query q__729 = t___2585;
                bool t___4868 = q__729.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__4851()
                {
                    return "join with where/order/limit";
                }
                test___66.Assert(t___4868, (S1::Func<string>) fn__4851);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__1116()
        {
            T::Test test___67 = new T::Test();
            try
            {
                SqlFragment c__731 = S0::SrcGlobal.Col(sid__344("users"), sid__344("id"));
                bool t___4849 = c__731.ToString() == "users.id";
                string fn__4843()
                {
                    return "col helper";
                }
                test___67.Assert(t___4849, (S1::Func<string>) fn__4843);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__1117()
        {
            T::Test test___68 = new T::Test();
            try
            {
                SqlFragment onCond__733 = S0::SrcGlobal.Col(sid__344("users"), sid__344("id"));
                SqlBuilder b__734 = new SqlBuilder();
                b__734.AppendFragment(onCond__733);
                b__734.AppendSafe(" = ");
                b__734.AppendFragment(S0::SrcGlobal.Col(sid__344("orders"), sid__344("user_id")));
                ISafeIdentifier t___4834 = sid__344("users");
                ISafeIdentifier t___4835 = sid__344("orders");
                SqlFragment t___4836 = b__734.Accumulated;
                Query q__735 = S0::SrcGlobal.From(t___4834).InnerJoin(t___4835, t___4836);
                bool t___4841 = q__735.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__4823()
                {
                    return "join with col";
                }
                test___68.Assert(t___4841, (S1::Func<string>) fn__4823);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__1118()
        {
            T::Test test___75 = new T::Test();
            try
            {
                ISafeIdentifier t___2544;
                t___2544 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__773 = t___2544;
                bool t___4821 = id__773.SqlValue == "user_name";
                string fn__4818()
                {
                    return "value should round-trip";
                }
                test___75.Assert(t___4821, (S1::Func<string>) fn__4818);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__1119()
        {
            T::Test test___76 = new T::Test();
            try
            {
                bool didBubble__775;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__775 = false;
                }
                catch
                {
                    didBubble__775 = true;
                }
                string fn__4815()
                {
                    return "empty string should bubble";
                }
                test___76.Assert(didBubble__775, (S1::Func<string>) fn__4815);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__1120()
        {
            T::Test test___77 = new T::Test();
            try
            {
                bool didBubble__777;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__777 = false;
                }
                catch
                {
                    didBubble__777 = true;
                }
                string fn__4812()
                {
                    return "leading digit should bubble";
                }
                test___77.Assert(didBubble__777, (S1::Func<string>) fn__4812);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__1121()
        {
            T::Test test___78 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__779 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__4809(string c__780)
                {
                    bool didBubble__781;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__780);
                        didBubble__781 = false;
                    }
                    catch
                    {
                        didBubble__781 = true;
                    }
                    string fn__4806()
                    {
                        return "should reject: " + c__780;
                    }
                    test___78.Assert(didBubble__781, (S1::Func<string>) fn__4806);
                }
                C::Listed.ForEach(cases__779, (S1::Action<string>) fn__4809);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__1122()
        {
            T::Test test___79 = new T::Test();
            try
            {
                ISafeIdentifier t___2521;
                t___2521 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___2522 = t___2521;
                ISafeIdentifier t___2523;
                t___2523 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___2524 = t___2523;
                StringField t___4796 = new StringField();
                FieldDef t___4797 = new FieldDef(t___2524, t___4796, false);
                ISafeIdentifier t___2527;
                t___2527 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___2528 = t___2527;
                IntField t___4798 = new IntField();
                FieldDef t___4799 = new FieldDef(t___2528, t___4798, false);
                TableDef td__783 = new TableDef(t___2522, C::Listed.CreateReadOnlyList<FieldDef>(t___4797, t___4799));
                FieldDef t___2532;
                t___2532 = td__783.Field("age");
                FieldDef f__784 = t___2532;
                bool t___4804 = f__784.Name.SqlValue == "age";
                string fn__4795()
                {
                    return "should find age field";
                }
                test___79.Assert(t___4804, (S1::Func<string>) fn__4795);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__1123()
        {
            T::Test test___80 = new T::Test();
            try
            {
                ISafeIdentifier t___2512;
                t___2512 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___2513 = t___2512;
                ISafeIdentifier t___2514;
                t___2514 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___2515 = t___2514;
                StringField t___4790 = new StringField();
                FieldDef t___4791 = new FieldDef(t___2515, t___4790, false);
                TableDef td__786 = new TableDef(t___2513, C::Listed.CreateReadOnlyList<FieldDef>(t___4791));
                bool didBubble__787;
                try
                {
                    td__786.Field("nonexistent");
                    didBubble__787 = false;
                }
                catch
                {
                    didBubble__787 = true;
                }
                string fn__4789()
                {
                    return "unknown field should bubble";
                }
                test___80.Assert(didBubble__787, (S1::Func<string>) fn__4789);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__1124()
        {
            T::Test test___81 = new T::Test();
            try
            {
                ISafeIdentifier t___2500;
                t___2500 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___2501 = t___2500;
                StringField t___4778 = new StringField();
                FieldDef required__789 = new FieldDef(t___2501, t___4778, false);
                ISafeIdentifier t___2504;
                t___2504 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___2505 = t___2504;
                StringField t___4780 = new StringField();
                FieldDef optional__790 = new FieldDef(t___2505, t___4780, true);
                bool t___4784 = !required__789.Nullable;
                string fn__4777()
                {
                    return "required field should not be nullable";
                }
                test___81.Assert(t___4784, (S1::Func<string>) fn__4777);
                bool t___4786 = optional__790.Nullable;
                string fn__4776()
                {
                    return "optional field should be nullable";
                }
                test___81.Assert(t___4786, (S1::Func<string>) fn__4776);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__1125()
        {
            T::Test test___85 = new T::Test();
            try
            {
                string build__916(string name__918)
                {
                    SqlBuilder t___4758 = new SqlBuilder();
                    t___4758.AppendSafe("select * from hi where name = ");
                    t___4758.AppendString(name__918);
                    return t___4758.Accumulated.ToString();
                }
                string buildWrong__917(string name__920)
                {
                    return "select * from hi where name = '" + name__920 + "'";
                }
                string actual___1127 = build__916("world");
                bool t___4768 = actual___1127 == "select * from hi where name = 'world'";
                string fn__4765()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___1127 + ")";
                }
                test___85.Assert(t___4768, (S1::Func<string>) fn__4765);
                string bobbyTables__922 = "Robert'); drop table hi;--";
                string actual___1129 = build__916("Robert'); drop table hi;--");
                bool t___4772 = actual___1129 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__4764()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___1129 + ")";
                }
                test___85.Assert(t___4772, (S1::Func<string>) fn__4764);
                string fn__4763()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___85.Assert(true, (S1::Func<string>) fn__4763);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__1133()
        {
            T::Test test___86 = new T::Test();
            try
            {
                SqlBuilder t___4726 = new SqlBuilder();
                t___4726.AppendSafe("v = ");
                t___4726.AppendString("");
                string actual___1134 = t___4726.Accumulated.ToString();
                bool t___4732 = actual___1134 == "v = ''";
                string fn__4725()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___1134 + ")";
                }
                test___86.Assert(t___4732, (S1::Func<string>) fn__4725);
                SqlBuilder t___4734 = new SqlBuilder();
                t___4734.AppendSafe("v = ");
                t___4734.AppendString("a''b");
                string actual___1137 = t___4734.Accumulated.ToString();
                bool t___4740 = actual___1137 == "v = 'a''''b'";
                string fn__4724()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___1137 + ")";
                }
                test___86.Assert(t___4740, (S1::Func<string>) fn__4724);
                SqlBuilder t___4742 = new SqlBuilder();
                t___4742.AppendSafe("v = ");
                t___4742.AppendString("Hello 世界");
                string actual___1140 = t___4742.Accumulated.ToString();
                bool t___4748 = actual___1140 == "v = 'Hello 世界'";
                string fn__4723()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1140 + ")";
                }
                test___86.Assert(t___4748, (S1::Func<string>) fn__4723);
                SqlBuilder t___4750 = new SqlBuilder();
                t___4750.AppendSafe("v = ");
                t___4750.AppendString("Line1\nLine2");
                string actual___1143 = t___4750.Accumulated.ToString();
                bool t___4756 = actual___1143 == "v = 'Line1\nLine2'";
                string fn__4722()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1143 + ")";
                }
                test___86.Assert(t___4756, (S1::Func<string>) fn__4722);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1146()
        {
            T::Test test___87 = new T::Test();
            try
            {
                SqlBuilder t___4697 = new SqlBuilder();
                t___4697.AppendSafe("select ");
                t___4697.AppendInt32(42);
                t___4697.AppendSafe(", ");
                t___4697.AppendInt64(43);
                t___4697.AppendSafe(", ");
                t___4697.AppendFloat64(19.99);
                t___4697.AppendSafe(", ");
                t___4697.AppendBoolean(true);
                t___4697.AppendSafe(", ");
                t___4697.AppendBoolean(false);
                string actual___1147 = t___4697.Accumulated.ToString();
                bool t___4711 = actual___1147 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__4696()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1147 + ")";
                }
                test___87.Assert(t___4711, (S1::Func<string>) fn__4696);
                S1::DateTime t___2445;
                t___2445 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__925 = t___2445;
                SqlBuilder t___4713 = new SqlBuilder();
                t___4713.AppendSafe("insert into t values (");
                t___4713.AppendDate(date__925);
                t___4713.AppendSafe(")");
                string actual___1150 = t___4713.Accumulated.ToString();
                bool t___4720 = actual___1150 == "insert into t values ('2024-12-25')";
                string fn__4695()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1150 + ")";
                }
                test___87.Assert(t___4720, (S1::Func<string>) fn__4695);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1153()
        {
            T::Test test___88 = new T::Test();
            try
            {
                SqlBuilder t___4641 = new SqlBuilder();
                t___4641.AppendSafe("v IN (");
                t___4641.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___4641.AppendSafe(")");
                string actual___1154 = t___4641.Accumulated.ToString();
                bool t___4648 = actual___1154 == "v IN ('a', 'b', 'c''d')";
                string fn__4640()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1154 + ")";
                }
                test___88.Assert(t___4648, (S1::Func<string>) fn__4640);
                SqlBuilder t___4650 = new SqlBuilder();
                t___4650.AppendSafe("v IN (");
                t___4650.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___4650.AppendSafe(")");
                string actual___1157 = t___4650.Accumulated.ToString();
                bool t___4657 = actual___1157 == "v IN (1, 2, 3)";
                string fn__4639()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1157 + ")";
                }
                test___88.Assert(t___4657, (S1::Func<string>) fn__4639);
                SqlBuilder t___4659 = new SqlBuilder();
                t___4659.AppendSafe("v IN (");
                t___4659.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___4659.AppendSafe(")");
                string actual___1160 = t___4659.Accumulated.ToString();
                bool t___4666 = actual___1160 == "v IN (1, 2)";
                string fn__4638()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1160 + ")";
                }
                test___88.Assert(t___4666, (S1::Func<string>) fn__4638);
                SqlBuilder t___4668 = new SqlBuilder();
                t___4668.AppendSafe("v IN (");
                t___4668.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___4668.AppendSafe(")");
                string actual___1163 = t___4668.Accumulated.ToString();
                bool t___4675 = actual___1163 == "v IN (1.0, 2.0)";
                string fn__4637()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1163 + ")";
                }
                test___88.Assert(t___4675, (S1::Func<string>) fn__4637);
                SqlBuilder t___4677 = new SqlBuilder();
                t___4677.AppendSafe("v IN (");
                t___4677.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___4677.AppendSafe(")");
                string actual___1166 = t___4677.Accumulated.ToString();
                bool t___4684 = actual___1166 == "v IN (TRUE, FALSE)";
                string fn__4636()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1166 + ")";
                }
                test___88.Assert(t___4684, (S1::Func<string>) fn__4636);
                S1::DateTime t___2417;
                t___2417 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___2418 = t___2417;
                S1::DateTime t___2419;
                t___2419 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___2420 = t___2419;
                G::IReadOnlyList<S1::DateTime> dates__927 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___2418, t___2420);
                SqlBuilder t___4686 = new SqlBuilder();
                t___4686.AppendSafe("v IN (");
                t___4686.AppendDateList(dates__927);
                t___4686.AppendSafe(")");
                string actual___1169 = t___4686.Accumulated.ToString();
                bool t___4693 = actual___1169 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__4635()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1169 + ")";
                }
                test___88.Assert(t___4693, (S1::Func<string>) fn__4635);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1172()
        {
            T::Test test___89 = new T::Test();
            try
            {
                double nan__929;
                nan__929 = 0.0 / 0.0;
                SqlBuilder t___4627 = new SqlBuilder();
                t___4627.AppendSafe("v = ");
                t___4627.AppendFloat64(nan__929);
                string actual___1173 = t___4627.Accumulated.ToString();
                bool t___4633 = actual___1173 == "v = NULL";
                string fn__4626()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1173 + ")";
                }
                test___89.Assert(t___4633, (S1::Func<string>) fn__4626);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1176()
        {
            T::Test test___90 = new T::Test();
            try
            {
                double inf__931;
                inf__931 = 1.0 / 0.0;
                SqlBuilder t___4618 = new SqlBuilder();
                t___4618.AppendSafe("v = ");
                t___4618.AppendFloat64(inf__931);
                string actual___1177 = t___4618.Accumulated.ToString();
                bool t___4624 = actual___1177 == "v = NULL";
                string fn__4617()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1177 + ")";
                }
                test___90.Assert(t___4624, (S1::Func<string>) fn__4617);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1180()
        {
            T::Test test___91 = new T::Test();
            try
            {
                double ninf__933;
                ninf__933 = -1.0 / 0.0;
                SqlBuilder t___4609 = new SqlBuilder();
                t___4609.AppendSafe("v = ");
                t___4609.AppendFloat64(ninf__933);
                string actual___1181 = t___4609.Accumulated.ToString();
                bool t___4615 = actual___1181 == "v = NULL";
                string fn__4608()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1181 + ")";
                }
                test___91.Assert(t___4615, (S1::Func<string>) fn__4608);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1184()
        {
            T::Test test___92 = new T::Test();
            try
            {
                SqlBuilder t___4584 = new SqlBuilder();
                t___4584.AppendSafe("v = ");
                t___4584.AppendFloat64(3.14);
                string actual___1185 = t___4584.Accumulated.ToString();
                bool t___4590 = actual___1185 == "v = 3.14";
                string fn__4583()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1185 + ")";
                }
                test___92.Assert(t___4590, (S1::Func<string>) fn__4583);
                SqlBuilder t___4592 = new SqlBuilder();
                t___4592.AppendSafe("v = ");
                t___4592.AppendFloat64(0.0);
                string actual___1188 = t___4592.Accumulated.ToString();
                bool t___4598 = actual___1188 == "v = 0.0";
                string fn__4582()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1188 + ")";
                }
                test___92.Assert(t___4598, (S1::Func<string>) fn__4582);
                SqlBuilder t___4600 = new SqlBuilder();
                t___4600.AppendSafe("v = ");
                t___4600.AppendFloat64(-42.5);
                string actual___1191 = t___4600.Accumulated.ToString();
                bool t___4606 = actual___1191 == "v = -42.5";
                string fn__4581()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1191 + ")";
                }
                test___92.Assert(t___4606, (S1::Func<string>) fn__4581);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1194()
        {
            T::Test test___93 = new T::Test();
            try
            {
                S1::DateTime t___2313;
                t___2313 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__936 = t___2313;
                SqlBuilder t___4573 = new SqlBuilder();
                t___4573.AppendSafe("v = ");
                t___4573.AppendDate(d__936);
                string actual___1195 = t___4573.Accumulated.ToString();
                bool t___4579 = actual___1195 == "v = '2024-06-15'";
                string fn__4572()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1195 + ")";
                }
                test___93.Assert(t___4579, (S1::Func<string>) fn__4572);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1198()
        {
            T::Test test___94 = new T::Test();
            try
            {
                string name__938 = "Someone";
                SqlBuilder t___4541 = new SqlBuilder();
                t___4541.AppendSafe("where p.last_name = ");
                t___4541.AppendString("Someone");
                SqlFragment condition__939 = t___4541.Accumulated;
                SqlBuilder t___4545 = new SqlBuilder();
                t___4545.AppendSafe("select p.id from person p ");
                t___4545.AppendFragment(condition__939);
                string actual___1200 = t___4545.Accumulated.ToString();
                bool t___4551 = actual___1200 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__4540()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1200 + ")";
                }
                test___94.Assert(t___4551, (S1::Func<string>) fn__4540);
                SqlBuilder t___4553 = new SqlBuilder();
                t___4553.AppendSafe("select p.id from person p ");
                t___4553.AppendPart(condition__939.ToSource());
                string actual___1203 = t___4553.Accumulated.ToString();
                bool t___4560 = actual___1203 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__4539()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1203 + ")";
                }
                test___94.Assert(t___4560, (S1::Func<string>) fn__4539);
                G::IReadOnlyList<ISqlPart> parts__940 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___4564 = new SqlBuilder();
                t___4564.AppendSafe("select ");
                t___4564.AppendPartList(parts__940);
                string actual___1206 = t___4564.Accumulated.ToString();
                bool t___4570 = actual___1206 == "select 'a''b', 3";
                string fn__4538()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___1206 + ")";
                }
                test___94.Assert(t___4570, (S1::Func<string>) fn__4538);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
    }
}
