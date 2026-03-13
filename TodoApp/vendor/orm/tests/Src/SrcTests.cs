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
        internal static ISafeIdentifier csid__441(string name__586)
        {
            ISafeIdentifier t___4812;
            t___4812 = S0::SrcGlobal.SafeIdentifier(name__586);
            return t___4812;
        }
        internal static TableDef userTable__442()
        {
            return new TableDef(csid__441("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__441("name"), new StringField(), false), new FieldDef(csid__441("email"), new StringField(), false), new FieldDef(csid__441("age"), new IntField(), true), new FieldDef(csid__441("score"), new FloatField(), true), new FieldDef(csid__441("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__1320()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__590 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___8523 = userTable__442();
                ISafeIdentifier t___8524 = csid__441("name");
                ISafeIdentifier t___8525 = csid__441("email");
                IChangeset cs__591 = S0::SrcGlobal.Changeset(t___8523, params__590).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8524, t___8525));
                bool t___8528 = C::Mapped.ContainsKey(cs__591.Changes, "name");
                string fn__8518()
                {
                    return "name should be in changes";
                }
                test___24.Assert(t___8528, (S1::Func<string>) fn__8518);
                bool t___8532 = C::Mapped.ContainsKey(cs__591.Changes, "email");
                string fn__8517()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___8532, (S1::Func<string>) fn__8517);
                bool t___8538 = !C::Mapped.ContainsKey(cs__591.Changes, "admin");
                string fn__8516()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___24.Assert(t___8538, (S1::Func<string>) fn__8516);
                bool t___8540 = cs__591.IsValid;
                string fn__8515()
                {
                    return "should still be valid";
                }
                test___24.Assert(t___8540, (S1::Func<string>) fn__8515);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__1321()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__593 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___8501 = userTable__442();
                ISafeIdentifier t___8502 = csid__441("name");
                IChangeset cs__594 = S0::SrcGlobal.Changeset(t___8501, params__593).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8502)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("email")));
                bool t___8509 = !C::Mapped.ContainsKey(cs__594.Changes, "name");
                string fn__8497()
                {
                    return "name must be excluded by second cast";
                }
                test___25.Assert(t___8509, (S1::Func<string>) fn__8497);
                bool t___8512 = C::Mapped.ContainsKey(cs__594.Changes, "email");
                string fn__8496()
                {
                    return "email should be present";
                }
                test___25.Assert(t___8512, (S1::Func<string>) fn__8496);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__1322()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__596 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___8483 = userTable__442();
                ISafeIdentifier t___8484 = csid__441("name");
                ISafeIdentifier t___8485 = csid__441("email");
                IChangeset cs__597 = S0::SrcGlobal.Changeset(t___8483, params__596).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8484, t___8485));
                bool t___8490 = !C::Mapped.ContainsKey(cs__597.Changes, "name");
                string fn__8479()
                {
                    return "empty name should not be in changes";
                }
                test___26.Assert(t___8490, (S1::Func<string>) fn__8479);
                bool t___8493 = C::Mapped.ContainsKey(cs__597.Changes, "email");
                string fn__8478()
                {
                    return "email should be in changes";
                }
                test___26.Assert(t___8493, (S1::Func<string>) fn__8478);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__1323()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__599 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___8465 = userTable__442();
                ISafeIdentifier t___8466 = csid__441("name");
                IChangeset cs__600 = S0::SrcGlobal.Changeset(t___8465, params__599).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8466)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name")));
                bool t___8470 = cs__600.IsValid;
                string fn__8462()
                {
                    return "should be valid";
                }
                test___27.Assert(t___8470, (S1::Func<string>) fn__8462);
                bool t___8476 = cs__600.Errors.Count == 0;
                string fn__8461()
                {
                    return "no errors expected";
                }
                test___27.Assert(t___8476, (S1::Func<string>) fn__8461);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__1324()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__602 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___8441 = userTable__442();
                ISafeIdentifier t___8442 = csid__441("name");
                IChangeset cs__603 = S0::SrcGlobal.Changeset(t___8441, params__602).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8442)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name")));
                bool t___8448 = !cs__603.IsValid;
                string fn__8439()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___8448, (S1::Func<string>) fn__8439);
                bool t___8453 = cs__603.Errors.Count == 1;
                string fn__8438()
                {
                    return "should have one error";
                }
                test___28.Assert(t___8453, (S1::Func<string>) fn__8438);
                bool t___8459 = cs__603.Errors[0].Field == "name";
                string fn__8437()
                {
                    return "error should name the field";
                }
                test___28.Assert(t___8459, (S1::Func<string>) fn__8437);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__1325()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__605 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___8429 = userTable__442();
                ISafeIdentifier t___8430 = csid__441("name");
                IChangeset cs__606 = S0::SrcGlobal.Changeset(t___8429, params__605).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8430)).ValidateLength(csid__441("name"), 2, 50);
                bool t___8434 = cs__606.IsValid;
                string fn__8426()
                {
                    return "should be valid";
                }
                test___29.Assert(t___8434, (S1::Func<string>) fn__8426);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__1326()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__608 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___8417 = userTable__442();
                ISafeIdentifier t___8418 = csid__441("name");
                IChangeset cs__609 = S0::SrcGlobal.Changeset(t___8417, params__608).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8418)).ValidateLength(csid__441("name"), 2, 50);
                bool t___8424 = !cs__609.IsValid;
                string fn__8414()
                {
                    return "should be invalid";
                }
                test___30.Assert(t___8424, (S1::Func<string>) fn__8414);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__1327()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__611 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___8405 = userTable__442();
                ISafeIdentifier t___8406 = csid__441("name");
                IChangeset cs__612 = S0::SrcGlobal.Changeset(t___8405, params__611).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8406)).ValidateLength(csid__441("name"), 2, 10);
                bool t___8412 = !cs__612.IsValid;
                string fn__8402()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___8412, (S1::Func<string>) fn__8402);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__1328()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__614 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___8394 = userTable__442();
                ISafeIdentifier t___8395 = csid__441("age");
                IChangeset cs__615 = S0::SrcGlobal.Changeset(t___8394, params__614).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8395)).ValidateInt(csid__441("age"));
                bool t___8399 = cs__615.IsValid;
                string fn__8391()
                {
                    return "should be valid";
                }
                test___32.Assert(t___8399, (S1::Func<string>) fn__8391);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__1329()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__617 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___8382 = userTable__442();
                ISafeIdentifier t___8383 = csid__441("age");
                IChangeset cs__618 = S0::SrcGlobal.Changeset(t___8382, params__617).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8383)).ValidateInt(csid__441("age"));
                bool t___8389 = !cs__618.IsValid;
                string fn__8379()
                {
                    return "should be invalid";
                }
                test___33.Assert(t___8389, (S1::Func<string>) fn__8379);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__1330()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__620 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___8371 = userTable__442();
                ISafeIdentifier t___8372 = csid__441("score");
                IChangeset cs__621 = S0::SrcGlobal.Changeset(t___8371, params__620).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8372)).ValidateFloat(csid__441("score"));
                bool t___8376 = cs__621.IsValid;
                string fn__8368()
                {
                    return "should be valid";
                }
                test___34.Assert(t___8376, (S1::Func<string>) fn__8368);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__1331()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__623 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___8360 = userTable__442();
                ISafeIdentifier t___8361 = csid__441("age");
                IChangeset cs__624 = S0::SrcGlobal.Changeset(t___8360, params__623).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8361)).ValidateInt64(csid__441("age"));
                bool t___8365 = cs__624.IsValid;
                string fn__8357()
                {
                    return "should be valid";
                }
                test___35.Assert(t___8365, (S1::Func<string>) fn__8357);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__1332()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__626 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___8348 = userTable__442();
                ISafeIdentifier t___8349 = csid__441("age");
                IChangeset cs__627 = S0::SrcGlobal.Changeset(t___8348, params__626).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8349)).ValidateInt64(csid__441("age"));
                bool t___8355 = !cs__627.IsValid;
                string fn__8345()
                {
                    return "should be invalid";
                }
                test___36.Assert(t___8355, (S1::Func<string>) fn__8345);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__1333()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__8342(string v__629)
                {
                    G::IReadOnlyDictionary<string, string> params__630 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__629)));
                    TableDef t___8334 = userTable__442();
                    ISafeIdentifier t___8335 = csid__441("active");
                    IChangeset cs__631 = S0::SrcGlobal.Changeset(t___8334, params__630).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8335)).ValidateBool(csid__441("active"));
                    bool t___8339 = cs__631.IsValid;
                    string fn__8331()
                    {
                        return "should accept: " + v__629;
                    }
                    test___37.Assert(t___8339, (S1::Func<string>) fn__8331);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__8342);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__1334()
        {
            T::Test test___38 = new T::Test();
            try
            {
                void fn__8328(string v__633)
                {
                    G::IReadOnlyDictionary<string, string> params__634 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__633)));
                    TableDef t___8320 = userTable__442();
                    ISafeIdentifier t___8321 = csid__441("active");
                    IChangeset cs__635 = S0::SrcGlobal.Changeset(t___8320, params__634).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8321)).ValidateBool(csid__441("active"));
                    bool t___8325 = cs__635.IsValid;
                    string fn__8317()
                    {
                        return "should accept: " + v__633;
                    }
                    test___38.Assert(t___8325, (S1::Func<string>) fn__8317);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__8328);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__1335()
        {
            T::Test test___39 = new T::Test();
            try
            {
                void fn__8314(string v__637)
                {
                    G::IReadOnlyDictionary<string, string> params__638 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__637)));
                    TableDef t___8305 = userTable__442();
                    ISafeIdentifier t___8306 = csid__441("active");
                    IChangeset cs__639 = S0::SrcGlobal.Changeset(t___8305, params__638).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8306)).ValidateBool(csid__441("active"));
                    bool t___8312 = !cs__639.IsValid;
                    string fn__8302()
                    {
                        return "should reject ambiguous: " + v__637;
                    }
                    test___39.Assert(t___8312, (S1::Func<string>) fn__8302);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__8314);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__1336()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__641 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___8290 = userTable__442();
                ISafeIdentifier t___8291 = csid__441("name");
                ISafeIdentifier t___8292 = csid__441("email");
                IChangeset cs__642 = S0::SrcGlobal.Changeset(t___8290, params__641).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8291, t___8292)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name"), csid__441("email")));
                SqlFragment t___4613;
                t___4613 = cs__642.ToInsertSql();
                SqlFragment sqlFrag__643 = t___4613;
                string s__644 = sqlFrag__643.ToString();
                bool t___8299 = s__644.IndexOf("''") >= 0;
                string fn__8286()
                {
                    return "single quote must be doubled: " + s__644;
                }
                test___40.Assert(t___8299, (S1::Func<string>) fn__8286);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__1337()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__646 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___8270 = userTable__442();
                ISafeIdentifier t___8271 = csid__441("name");
                ISafeIdentifier t___8272 = csid__441("email");
                IChangeset cs__647 = S0::SrcGlobal.Changeset(t___8270, params__646).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8271, t___8272)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name"), csid__441("email")));
                SqlFragment t___4592;
                t___4592 = cs__647.ToInsertSql();
                SqlFragment sqlFrag__648 = t___4592;
                string s__649 = sqlFrag__648.ToString();
                bool t___8279 = s__649.IndexOf("INSERT INTO users") >= 0;
                string fn__8266()
                {
                    return "has INSERT INTO: " + s__649;
                }
                test___41.Assert(t___8279, (S1::Func<string>) fn__8266);
                bool t___8283 = s__649.IndexOf("'Alice'") >= 0;
                string fn__8265()
                {
                    return "has quoted name: " + s__649;
                }
                test___41.Assert(t___8283, (S1::Func<string>) fn__8265);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__1338()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__651 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___8252 = userTable__442();
                ISafeIdentifier t___8253 = csid__441("name");
                ISafeIdentifier t___8254 = csid__441("email");
                ISafeIdentifier t___8255 = csid__441("age");
                IChangeset cs__652 = S0::SrcGlobal.Changeset(t___8252, params__651).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8253, t___8254, t___8255)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name"), csid__441("email")));
                SqlFragment t___4575;
                t___4575 = cs__652.ToInsertSql();
                SqlFragment sqlFrag__653 = t___4575;
                string s__654 = sqlFrag__653.ToString();
                bool t___8262 = s__654.IndexOf("25") >= 0;
                string fn__8247()
                {
                    return "age rendered unquoted: " + s__654;
                }
                test___42.Assert(t___8262, (S1::Func<string>) fn__8247);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__1339()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__656 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___8240 = userTable__442();
                ISafeIdentifier t___8241 = csid__441("name");
                IChangeset cs__657 = S0::SrcGlobal.Changeset(t___8240, params__656).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8241)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name")));
                bool didBubble__658;
                try
                {
                    cs__657.ToInsertSql();
                    didBubble__658 = false;
                }
                catch
                {
                    didBubble__658 = true;
                }
                string fn__8238()
                {
                    return "invalid changeset should bubble";
                }
                test___43.Assert(didBubble__658, (S1::Func<string>) fn__8238);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__1340()
        {
            T::Test test___44 = new T::Test();
            try
            {
                TableDef strictTable__660 = new TableDef(csid__441("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__441("title"), new StringField(), false), new FieldDef(csid__441("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__661 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___8231 = csid__441("body");
                IChangeset cs__662 = S0::SrcGlobal.Changeset(strictTable__660, params__661).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8231));
                bool t___8233 = cs__662.IsValid;
                string fn__8220()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___44.Assert(t___8233, (S1::Func<string>) fn__8220);
                bool didBubble__663;
                try
                {
                    cs__662.ToInsertSql();
                    didBubble__663 = false;
                }
                catch
                {
                    didBubble__663 = true;
                }
                string fn__8219()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___44.Assert(didBubble__663, (S1::Func<string>) fn__8219);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__1341()
        {
            T::Test test___45 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__665 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___8210 = userTable__442();
                ISafeIdentifier t___8211 = csid__441("name");
                IChangeset cs__666 = S0::SrcGlobal.Changeset(t___8210, params__665).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8211)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name")));
                SqlFragment t___4535;
                t___4535 = cs__666.ToUpdateSql(42);
                SqlFragment sqlFrag__667 = t___4535;
                string s__668 = sqlFrag__667.ToString();
                bool t___8217 = s__668 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__8207()
                {
                    return "got: " + s__668;
                }
                test___45.Assert(t___8217, (S1::Func<string>) fn__8207);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__1342()
        {
            T::Test test___46 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__670 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___8200 = userTable__442();
                ISafeIdentifier t___8201 = csid__441("name");
                IChangeset cs__671 = S0::SrcGlobal.Changeset(t___8200, params__670).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8201)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__441("name")));
                bool didBubble__672;
                try
                {
                    cs__671.ToUpdateSql(1);
                    didBubble__672 = false;
                }
                catch
                {
                    didBubble__672 = true;
                }
                string fn__8198()
                {
                    return "invalid changeset should bubble";
                }
                test___46.Assert(didBubble__672, (S1::Func<string>) fn__8198);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__443(string name__886)
        {
            ISafeIdentifier t___4188;
            t___4188 = S0::SrcGlobal.SafeIdentifier(name__886);
            return t___4188;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__1391()
        {
            T::Test test___47 = new T::Test();
            try
            {
                Query q__889 = S0::SrcGlobal.From(sid__443("users"));
                bool t___7866 = q__889.ToSql().ToString() == "SELECT * FROM users";
                string fn__7861()
                {
                    return "bare query";
                }
                test___47.Assert(t___7866, (S1::Func<string>) fn__7861);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__1392()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___7852 = sid__443("users");
                ISafeIdentifier t___7853 = sid__443("id");
                ISafeIdentifier t___7854 = sid__443("name");
                Query q__891 = S0::SrcGlobal.From(t___7852).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7853, t___7854));
                bool t___7859 = q__891.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__7851()
                {
                    return "select columns";
                }
                test___48.Assert(t___7859, (S1::Func<string>) fn__7851);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__1393()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___7840 = sid__443("users");
                SqlBuilder t___7841 = new SqlBuilder();
                t___7841.AppendSafe("age > ");
                t___7841.AppendInt32(18);
                SqlFragment t___7844 = t___7841.Accumulated;
                Query q__893 = S0::SrcGlobal.From(t___7840).Where(t___7844);
                bool t___7849 = q__893.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__7839()
                {
                    return "where int";
                }
                test___49.Assert(t___7849, (S1::Func<string>) fn__7839);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__1395()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___7828 = sid__443("users");
                SqlBuilder t___7829 = new SqlBuilder();
                t___7829.AppendSafe("active = ");
                t___7829.AppendBoolean(true);
                SqlFragment t___7832 = t___7829.Accumulated;
                Query q__895 = S0::SrcGlobal.From(t___7828).Where(t___7832);
                bool t___7837 = q__895.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__7827()
                {
                    return "where bool";
                }
                test___50.Assert(t___7837, (S1::Func<string>) fn__7827);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__1397()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___7811 = sid__443("users");
                SqlBuilder t___7812 = new SqlBuilder();
                t___7812.AppendSafe("age > ");
                t___7812.AppendInt32(18);
                SqlFragment t___7815 = t___7812.Accumulated;
                Query t___7816 = S0::SrcGlobal.From(t___7811).Where(t___7815);
                SqlBuilder t___7817 = new SqlBuilder();
                t___7817.AppendSafe("active = ");
                t___7817.AppendBoolean(true);
                Query q__897 = t___7816.Where(t___7817.Accumulated);
                bool t___7825 = q__897.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__7810()
                {
                    return "chained where";
                }
                test___51.Assert(t___7825, (S1::Func<string>) fn__7810);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__1400()
        {
            T::Test test___52 = new T::Test();
            try
            {
                ISafeIdentifier t___7802 = sid__443("users");
                ISafeIdentifier t___7803 = sid__443("name");
                Query q__899 = S0::SrcGlobal.From(t___7802).OrderBy(t___7803, true);
                bool t___7808 = q__899.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__7801()
                {
                    return "order asc";
                }
                test___52.Assert(t___7808, (S1::Func<string>) fn__7801);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__1401()
        {
            T::Test test___53 = new T::Test();
            try
            {
                ISafeIdentifier t___7793 = sid__443("users");
                ISafeIdentifier t___7794 = sid__443("created_at");
                Query q__901 = S0::SrcGlobal.From(t___7793).OrderBy(t___7794, false);
                bool t___7799 = q__901.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__7792()
                {
                    return "order desc";
                }
                test___53.Assert(t___7799, (S1::Func<string>) fn__7792);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__1402()
        {
            T::Test test___54 = new T::Test();
            try
            {
                Query t___4122;
                t___4122 = S0::SrcGlobal.From(sid__443("users")).Limit(10);
                Query t___4123;
                t___4123 = t___4122.Offset(20);
                Query q__903 = t___4123;
                bool t___7790 = q__903.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__7785()
                {
                    return "limit/offset";
                }
                test___54.Assert(t___7790, (S1::Func<string>) fn__7785);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__1403()
        {
            T::Test test___55 = new T::Test();
            try
            {
                bool didBubble__905;
                try
                {
                    S0::SrcGlobal.From(sid__443("users")).Limit(-1);
                    didBubble__905 = false;
                }
                catch
                {
                    didBubble__905 = true;
                }
                string fn__7781()
                {
                    return "negative limit should bubble";
                }
                test___55.Assert(didBubble__905, (S1::Func<string>) fn__7781);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__1404()
        {
            T::Test test___56 = new T::Test();
            try
            {
                bool didBubble__907;
                try
                {
                    S0::SrcGlobal.From(sid__443("users")).Offset(-1);
                    didBubble__907 = false;
                }
                catch
                {
                    didBubble__907 = true;
                }
                string fn__7777()
                {
                    return "negative offset should bubble";
                }
                test___56.Assert(didBubble__907, (S1::Func<string>) fn__7777);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__1405()
        {
            T::Test test___57 = new T::Test();
            try
            {
                int minAge__909 = 21;
                ISafeIdentifier t___7755 = sid__443("users");
                ISafeIdentifier t___7756 = sid__443("id");
                ISafeIdentifier t___7757 = sid__443("name");
                ISafeIdentifier t___7758 = sid__443("email");
                Query t___7759 = S0::SrcGlobal.From(t___7755).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7756, t___7757, t___7758));
                SqlBuilder t___7760 = new SqlBuilder();
                t___7760.AppendSafe("age >= ");
                t___7760.AppendInt32(21);
                Query t___7764 = t___7759.Where(t___7760.Accumulated);
                SqlBuilder t___7765 = new SqlBuilder();
                t___7765.AppendSafe("active = ");
                t___7765.AppendBoolean(true);
                Query t___4108;
                t___4108 = t___7764.Where(t___7765.Accumulated).OrderBy(sid__443("name"), true).Limit(25);
                Query t___4109;
                t___4109 = t___4108.Offset(0);
                Query q__910 = t___4109;
                bool t___7775 = q__910.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__7754()
                {
                    return "complex query";
                }
                test___57.Assert(t___7775, (S1::Func<string>) fn__7754);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__1408()
        {
            T::Test test___58 = new T::Test();
            try
            {
                Query q__912 = S0::SrcGlobal.From(sid__443("users"));
                SqlFragment t___4085;
                t___4085 = q__912.SafeToSql(100);
                SqlFragment t___4086 = t___4085;
                string s__913 = t___4086.ToString();
                bool t___7752 = s__913 == "SELECT * FROM users LIMIT 100";
                string fn__7748()
                {
                    return "should have limit: " + s__913;
                }
                test___58.Assert(t___7752, (S1::Func<string>) fn__7748);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__1409()
        {
            T::Test test___59 = new T::Test();
            try
            {
                Query t___4077;
                t___4077 = S0::SrcGlobal.From(sid__443("users")).Limit(5);
                Query q__915 = t___4077;
                SqlFragment t___4080;
                t___4080 = q__915.SafeToSql(100);
                SqlFragment t___4081 = t___4080;
                string s__916 = t___4081.ToString();
                bool t___7746 = s__916 == "SELECT * FROM users LIMIT 5";
                string fn__7742()
                {
                    return "explicit limit preserved: " + s__916;
                }
                test___59.Assert(t___7746, (S1::Func<string>) fn__7742);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__1410()
        {
            T::Test test___60 = new T::Test();
            try
            {
                bool didBubble__918;
                try
                {
                    S0::SrcGlobal.From(sid__443("users")).SafeToSql(-1);
                    didBubble__918 = false;
                }
                catch
                {
                    didBubble__918 = true;
                }
                string fn__7738()
                {
                    return "negative defaultLimit should bubble";
                }
                test___60.Assert(didBubble__918, (S1::Func<string>) fn__7738);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__1411()
        {
            T::Test test___61 = new T::Test();
            try
            {
                string evil__920 = "'; DROP TABLE users; --";
                ISafeIdentifier t___7722 = sid__443("users");
                SqlBuilder t___7723 = new SqlBuilder();
                t___7723.AppendSafe("name = ");
                t___7723.AppendString("'; DROP TABLE users; --");
                SqlFragment t___7726 = t___7723.Accumulated;
                Query q__921 = S0::SrcGlobal.From(t___7722).Where(t___7726);
                string s__922 = q__921.ToSql().ToString();
                bool t___7731 = s__922.IndexOf("''") >= 0;
                string fn__7721()
                {
                    return "quotes must be doubled: " + s__922;
                }
                test___61.Assert(t___7731, (S1::Func<string>) fn__7721);
                bool t___7735 = s__922.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__7720()
                {
                    return "structure intact: " + s__922;
                }
                test___61.Assert(t___7735, (S1::Func<string>) fn__7720);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__1413()
        {
            T::Test test___62 = new T::Test();
            try
            {
                string attack__924 = "users; DROP TABLE users; --";
                bool didBubble__925;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__925 = false;
                }
                catch
                {
                    didBubble__925 = true;
                }
                string fn__7717()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___62.Assert(didBubble__925, (S1::Func<string>) fn__7717);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__1414()
        {
            T::Test test___63 = new T::Test();
            try
            {
                ISafeIdentifier t___7706 = sid__443("users");
                ISafeIdentifier t___7707 = sid__443("orders");
                SqlBuilder t___7708 = new SqlBuilder();
                t___7708.AppendSafe("users.id = orders.user_id");
                SqlFragment t___7710 = t___7708.Accumulated;
                Query q__927 = S0::SrcGlobal.From(t___7706).InnerJoin(t___7707, t___7710);
                bool t___7715 = q__927.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__7705()
                {
                    return "inner join";
                }
                test___63.Assert(t___7715, (S1::Func<string>) fn__7705);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__1416()
        {
            T::Test test___64 = new T::Test();
            try
            {
                ISafeIdentifier t___7694 = sid__443("users");
                ISafeIdentifier t___7695 = sid__443("profiles");
                SqlBuilder t___7696 = new SqlBuilder();
                t___7696.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___7698 = t___7696.Accumulated;
                Query q__929 = S0::SrcGlobal.From(t___7694).LeftJoin(t___7695, t___7698);
                bool t___7703 = q__929.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__7693()
                {
                    return "left join";
                }
                test___64.Assert(t___7703, (S1::Func<string>) fn__7693);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__1418()
        {
            T::Test test___65 = new T::Test();
            try
            {
                ISafeIdentifier t___7682 = sid__443("orders");
                ISafeIdentifier t___7683 = sid__443("users");
                SqlBuilder t___7684 = new SqlBuilder();
                t___7684.AppendSafe("orders.user_id = users.id");
                SqlFragment t___7686 = t___7684.Accumulated;
                Query q__931 = S0::SrcGlobal.From(t___7682).RightJoin(t___7683, t___7686);
                bool t___7691 = q__931.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__7681()
                {
                    return "right join";
                }
                test___65.Assert(t___7691, (S1::Func<string>) fn__7681);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__1420()
        {
            T::Test test___66 = new T::Test();
            try
            {
                ISafeIdentifier t___7670 = sid__443("users");
                ISafeIdentifier t___7671 = sid__443("orders");
                SqlBuilder t___7672 = new SqlBuilder();
                t___7672.AppendSafe("users.id = orders.user_id");
                SqlFragment t___7674 = t___7672.Accumulated;
                Query q__933 = S0::SrcGlobal.From(t___7670).FullJoin(t___7671, t___7674);
                bool t___7679 = q__933.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__7669()
                {
                    return "full join";
                }
                test___66.Assert(t___7679, (S1::Func<string>) fn__7669);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__1422()
        {
            T::Test test___67 = new T::Test();
            try
            {
                ISafeIdentifier t___7653 = sid__443("users");
                ISafeIdentifier t___7654 = sid__443("orders");
                SqlBuilder t___7655 = new SqlBuilder();
                t___7655.AppendSafe("users.id = orders.user_id");
                SqlFragment t___7657 = t___7655.Accumulated;
                Query t___7658 = S0::SrcGlobal.From(t___7653).InnerJoin(t___7654, t___7657);
                ISafeIdentifier t___7659 = sid__443("profiles");
                SqlBuilder t___7660 = new SqlBuilder();
                t___7660.AppendSafe("users.id = profiles.user_id");
                Query q__935 = t___7658.LeftJoin(t___7659, t___7660.Accumulated);
                bool t___7667 = q__935.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__7652()
                {
                    return "chained joins";
                }
                test___67.Assert(t___7667, (S1::Func<string>) fn__7652);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__1425()
        {
            T::Test test___68 = new T::Test();
            try
            {
                ISafeIdentifier t___7634 = sid__443("users");
                ISafeIdentifier t___7635 = sid__443("orders");
                SqlBuilder t___7636 = new SqlBuilder();
                t___7636.AppendSafe("users.id = orders.user_id");
                SqlFragment t___7638 = t___7636.Accumulated;
                Query t___7639 = S0::SrcGlobal.From(t___7634).InnerJoin(t___7635, t___7638);
                SqlBuilder t___7640 = new SqlBuilder();
                t___7640.AppendSafe("orders.total > ");
                t___7640.AppendInt32(100);
                Query t___3992;
                t___3992 = t___7639.Where(t___7640.Accumulated).OrderBy(sid__443("name"), true).Limit(10);
                Query q__937 = t___3992;
                bool t___7650 = q__937.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__7633()
                {
                    return "join with where/order/limit";
                }
                test___68.Assert(t___7650, (S1::Func<string>) fn__7633);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__1428()
        {
            T::Test test___69 = new T::Test();
            try
            {
                SqlFragment c__939 = S0::SrcGlobal.Col(sid__443("users"), sid__443("id"));
                bool t___7631 = c__939.ToString() == "users.id";
                string fn__7625()
                {
                    return "col helper";
                }
                test___69.Assert(t___7631, (S1::Func<string>) fn__7625);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__1429()
        {
            T::Test test___70 = new T::Test();
            try
            {
                SqlFragment onCond__941 = S0::SrcGlobal.Col(sid__443("users"), sid__443("id"));
                SqlBuilder b__942 = new SqlBuilder();
                b__942.AppendFragment(onCond__941);
                b__942.AppendSafe(" = ");
                b__942.AppendFragment(S0::SrcGlobal.Col(sid__443("orders"), sid__443("user_id")));
                ISafeIdentifier t___7616 = sid__443("users");
                ISafeIdentifier t___7617 = sid__443("orders");
                SqlFragment t___7618 = b__942.Accumulated;
                Query q__943 = S0::SrcGlobal.From(t___7616).InnerJoin(t___7617, t___7618);
                bool t___7623 = q__943.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__7605()
                {
                    return "join with col";
                }
                test___70.Assert(t___7623, (S1::Func<string>) fn__7605);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__1430()
        {
            T::Test test___71 = new T::Test();
            try
            {
                ISafeIdentifier t___7594 = sid__443("users");
                SqlBuilder t___7595 = new SqlBuilder();
                t___7595.AppendSafe("status = ");
                t___7595.AppendString("active");
                SqlFragment t___7598 = t___7595.Accumulated;
                Query q__945 = S0::SrcGlobal.From(t___7594).OrWhere(t___7598);
                bool t___7603 = q__945.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__7593()
                {
                    return "orWhere basic";
                }
                test___71.Assert(t___7603, (S1::Func<string>) fn__7593);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__1432()
        {
            T::Test test___72 = new T::Test();
            try
            {
                ISafeIdentifier t___7577 = sid__443("users");
                SqlBuilder t___7578 = new SqlBuilder();
                t___7578.AppendSafe("age > ");
                t___7578.AppendInt32(18);
                SqlFragment t___7581 = t___7578.Accumulated;
                Query t___7582 = S0::SrcGlobal.From(t___7577).Where(t___7581);
                SqlBuilder t___7583 = new SqlBuilder();
                t___7583.AppendSafe("vip = ");
                t___7583.AppendBoolean(true);
                Query q__947 = t___7582.OrWhere(t___7583.Accumulated);
                bool t___7591 = q__947.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__7576()
                {
                    return "where then orWhere";
                }
                test___72.Assert(t___7591, (S1::Func<string>) fn__7576);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__1435()
        {
            T::Test test___73 = new T::Test();
            try
            {
                ISafeIdentifier t___7555 = sid__443("users");
                SqlBuilder t___7556 = new SqlBuilder();
                t___7556.AppendSafe("active = ");
                t___7556.AppendBoolean(true);
                SqlFragment t___7559 = t___7556.Accumulated;
                Query t___7560 = S0::SrcGlobal.From(t___7555).Where(t___7559);
                SqlBuilder t___7561 = new SqlBuilder();
                t___7561.AppendSafe("role = ");
                t___7561.AppendString("admin");
                Query t___7565 = t___7560.OrWhere(t___7561.Accumulated);
                SqlBuilder t___7566 = new SqlBuilder();
                t___7566.AppendSafe("role = ");
                t___7566.AppendString("moderator");
                Query q__949 = t___7565.OrWhere(t___7566.Accumulated);
                bool t___7574 = q__949.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__7554()
                {
                    return "multiple orWhere";
                }
                test___73.Assert(t___7574, (S1::Func<string>) fn__7554);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__1439()
        {
            T::Test test___74 = new T::Test();
            try
            {
                ISafeIdentifier t___7533 = sid__443("users");
                SqlBuilder t___7534 = new SqlBuilder();
                t___7534.AppendSafe("age > ");
                t___7534.AppendInt32(18);
                SqlFragment t___7537 = t___7534.Accumulated;
                Query t___7538 = S0::SrcGlobal.From(t___7533).Where(t___7537);
                SqlBuilder t___7539 = new SqlBuilder();
                t___7539.AppendSafe("active = ");
                t___7539.AppendBoolean(true);
                Query t___7543 = t___7538.Where(t___7539.Accumulated);
                SqlBuilder t___7544 = new SqlBuilder();
                t___7544.AppendSafe("vip = ");
                t___7544.AppendBoolean(true);
                Query q__951 = t___7543.OrWhere(t___7544.Accumulated);
                bool t___7552 = q__951.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__7532()
                {
                    return "mixed where and orWhere";
                }
                test___74.Assert(t___7552, (S1::Func<string>) fn__7532);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__1443()
        {
            T::Test test___75 = new T::Test();
            try
            {
                ISafeIdentifier t___7524 = sid__443("users");
                ISafeIdentifier t___7525 = sid__443("deleted_at");
                Query q__953 = S0::SrcGlobal.From(t___7524).WhereNull(t___7525);
                bool t___7530 = q__953.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__7523()
                {
                    return "whereNull";
                }
                test___75.Assert(t___7530, (S1::Func<string>) fn__7523);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__1444()
        {
            T::Test test___76 = new T::Test();
            try
            {
                ISafeIdentifier t___7515 = sid__443("users");
                ISafeIdentifier t___7516 = sid__443("email");
                Query q__955 = S0::SrcGlobal.From(t___7515).WhereNotNull(t___7516);
                bool t___7521 = q__955.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__7514()
                {
                    return "whereNotNull";
                }
                test___76.Assert(t___7521, (S1::Func<string>) fn__7514);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__1445()
        {
            T::Test test___77 = new T::Test();
            try
            {
                ISafeIdentifier t___7501 = sid__443("users");
                SqlBuilder t___7502 = new SqlBuilder();
                t___7502.AppendSafe("active = ");
                t___7502.AppendBoolean(true);
                SqlFragment t___7505 = t___7502.Accumulated;
                Query q__957 = S0::SrcGlobal.From(t___7501).Where(t___7505).WhereNull(sid__443("deleted_at"));
                bool t___7512 = q__957.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__7500()
                {
                    return "whereNull chained";
                }
                test___77.Assert(t___7512, (S1::Func<string>) fn__7500);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__1447()
        {
            T::Test test___78 = new T::Test();
            try
            {
                ISafeIdentifier t___7487 = sid__443("users");
                ISafeIdentifier t___7488 = sid__443("deleted_at");
                Query t___7489 = S0::SrcGlobal.From(t___7487).WhereNull(t___7488);
                SqlBuilder t___7490 = new SqlBuilder();
                t___7490.AppendSafe("role = ");
                t___7490.AppendString("admin");
                Query q__959 = t___7489.OrWhere(t___7490.Accumulated);
                bool t___7498 = q__959.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__7486()
                {
                    return "whereNotNull with orWhere";
                }
                test___78.Assert(t___7498, (S1::Func<string>) fn__7486);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__1449()
        {
            T::Test test___79 = new T::Test();
            try
            {
                ISafeIdentifier t___7475 = sid__443("users");
                ISafeIdentifier t___7476 = sid__443("id");
                SqlInt32 t___7477 = new SqlInt32(1);
                SqlInt32 t___7478 = new SqlInt32(2);
                SqlInt32 t___7479 = new SqlInt32(3);
                Query q__961 = S0::SrcGlobal.From(t___7475).WhereIn(t___7476, C::Listed.CreateReadOnlyList<SqlInt32>(t___7477, t___7478, t___7479));
                bool t___7484 = q__961.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__7474()
                {
                    return "whereIn ints";
                }
                test___79.Assert(t___7484, (S1::Func<string>) fn__7474);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__1450()
        {
            T::Test test___80 = new T::Test();
            try
            {
                ISafeIdentifier t___7464 = sid__443("users");
                ISafeIdentifier t___7465 = sid__443("name");
                SqlString t___7466 = new SqlString("Alice");
                SqlString t___7467 = new SqlString("Bob's");
                Query q__963 = S0::SrcGlobal.From(t___7464).WhereIn(t___7465, C::Listed.CreateReadOnlyList<SqlString>(t___7466, t___7467));
                bool t___7472 = q__963.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__7463()
                {
                    return "whereIn strings";
                }
                test___80.Assert(t___7472, (S1::Func<string>) fn__7463);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__1451()
        {
            T::Test test___81 = new T::Test();
            try
            {
                ISafeIdentifier t___7455 = sid__443("users");
                ISafeIdentifier t___7456 = sid__443("id");
                Query q__965 = S0::SrcGlobal.From(t___7455).WhereIn(t___7456, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___7461 = q__965.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__7454()
                {
                    return "whereIn empty";
                }
                test___81.Assert(t___7461, (S1::Func<string>) fn__7454);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__1452()
        {
            T::Test test___82 = new T::Test();
            try
            {
                ISafeIdentifier t___7439 = sid__443("users");
                SqlBuilder t___7440 = new SqlBuilder();
                t___7440.AppendSafe("active = ");
                t___7440.AppendBoolean(true);
                SqlFragment t___7443 = t___7440.Accumulated;
                Query q__967 = S0::SrcGlobal.From(t___7439).Where(t___7443).WhereIn(sid__443("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___7452 = q__967.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__7438()
                {
                    return "whereIn chained";
                }
                test___82.Assert(t___7452, (S1::Func<string>) fn__7438);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__1454()
        {
            T::Test test___83 = new T::Test();
            try
            {
                ISafeIdentifier t___7429 = sid__443("users");
                ISafeIdentifier t___7430 = sid__443("id");
                SqlInt32 t___7431 = new SqlInt32(42);
                Query q__969 = S0::SrcGlobal.From(t___7429).WhereIn(t___7430, C::Listed.CreateReadOnlyList<SqlInt32>(t___7431));
                bool t___7436 = q__969.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__7428()
                {
                    return "whereIn single";
                }
                test___83.Assert(t___7436, (S1::Func<string>) fn__7428);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__1455()
        {
            T::Test test___84 = new T::Test();
            try
            {
                ISafeIdentifier t___7417 = sid__443("users");
                SqlBuilder t___7418 = new SqlBuilder();
                t___7418.AppendSafe("active = ");
                t___7418.AppendBoolean(true);
                SqlFragment t___7421 = t___7418.Accumulated;
                Query q__971 = S0::SrcGlobal.From(t___7417).WhereNot(t___7421);
                bool t___7426 = q__971.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__7416()
                {
                    return "whereNot";
                }
                test___84.Assert(t___7426, (S1::Func<string>) fn__7416);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__1457()
        {
            T::Test test___85 = new T::Test();
            try
            {
                ISafeIdentifier t___7400 = sid__443("users");
                SqlBuilder t___7401 = new SqlBuilder();
                t___7401.AppendSafe("age > ");
                t___7401.AppendInt32(18);
                SqlFragment t___7404 = t___7401.Accumulated;
                Query t___7405 = S0::SrcGlobal.From(t___7400).Where(t___7404);
                SqlBuilder t___7406 = new SqlBuilder();
                t___7406.AppendSafe("banned = ");
                t___7406.AppendBoolean(true);
                Query q__973 = t___7405.WhereNot(t___7406.Accumulated);
                bool t___7414 = q__973.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__7399()
                {
                    return "whereNot chained";
                }
                test___85.Assert(t___7414, (S1::Func<string>) fn__7399);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__1460()
        {
            T::Test test___86 = new T::Test();
            try
            {
                ISafeIdentifier t___7389 = sid__443("users");
                ISafeIdentifier t___7390 = sid__443("age");
                SqlInt32 t___7391 = new SqlInt32(18);
                SqlInt32 t___7392 = new SqlInt32(65);
                Query q__975 = S0::SrcGlobal.From(t___7389).WhereBetween(t___7390, t___7391, t___7392);
                bool t___7397 = q__975.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__7388()
                {
                    return "whereBetween ints";
                }
                test___86.Assert(t___7397, (S1::Func<string>) fn__7388);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__1461()
        {
            T::Test test___87 = new T::Test();
            try
            {
                ISafeIdentifier t___7373 = sid__443("users");
                SqlBuilder t___7374 = new SqlBuilder();
                t___7374.AppendSafe("active = ");
                t___7374.AppendBoolean(true);
                SqlFragment t___7377 = t___7374.Accumulated;
                Query q__977 = S0::SrcGlobal.From(t___7373).Where(t___7377).WhereBetween(sid__443("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___7386 = q__977.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__7372()
                {
                    return "whereBetween chained";
                }
                test___87.Assert(t___7386, (S1::Func<string>) fn__7372);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__1463()
        {
            T::Test test___88 = new T::Test();
            try
            {
                ISafeIdentifier t___7364 = sid__443("users");
                ISafeIdentifier t___7365 = sid__443("name");
                Query q__979 = S0::SrcGlobal.From(t___7364).WhereLike(t___7365, "John%");
                bool t___7370 = q__979.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__7363()
                {
                    return "whereLike";
                }
                test___88.Assert(t___7370, (S1::Func<string>) fn__7363);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__1464()
        {
            T::Test test___89 = new T::Test();
            try
            {
                ISafeIdentifier t___7355 = sid__443("users");
                ISafeIdentifier t___7356 = sid__443("email");
                Query q__981 = S0::SrcGlobal.From(t___7355).WhereILike(t___7356, "%@gmail.com");
                bool t___7361 = q__981.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__7354()
                {
                    return "whereILike";
                }
                test___89.Assert(t___7361, (S1::Func<string>) fn__7354);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__1465()
        {
            T::Test test___90 = new T::Test();
            try
            {
                ISafeIdentifier t___7341 = sid__443("users");
                ISafeIdentifier t___7342 = sid__443("name");
                Query q__983 = S0::SrcGlobal.From(t___7341).WhereLike(t___7342, "'; DROP TABLE users; --");
                string s__984 = q__983.ToSql().ToString();
                bool t___7347 = s__984.IndexOf("''") >= 0;
                string fn__7340()
                {
                    return "like injection escaped: " + s__984;
                }
                test___90.Assert(t___7347, (S1::Func<string>) fn__7340);
                bool t___7351 = s__984.IndexOf("LIKE") >= 0;
                string fn__7339()
                {
                    return "like structure intact: " + s__984;
                }
                test___90.Assert(t___7351, (S1::Func<string>) fn__7339);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__1466()
        {
            T::Test test___91 = new T::Test();
            try
            {
                ISafeIdentifier t___7331 = sid__443("users");
                ISafeIdentifier t___7332 = sid__443("name");
                Query q__986 = S0::SrcGlobal.From(t___7331).WhereLike(t___7332, "%son%");
                bool t___7337 = q__986.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__7330()
                {
                    return "whereLike wildcard";
                }
                test___91.Assert(t___7337, (S1::Func<string>) fn__7330);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__1467()
        {
            T::Test test___92 = new T::Test();
            try
            {
                SqlFragment f__988 = S0::SrcGlobal.CountAll();
                bool t___7328 = f__988.ToString() == "COUNT(*)";
                string fn__7324()
                {
                    return "countAll";
                }
                test___92.Assert(t___7328, (S1::Func<string>) fn__7324);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__1468()
        {
            T::Test test___93 = new T::Test();
            try
            {
                SqlFragment f__990 = S0::SrcGlobal.CountCol(sid__443("id"));
                bool t___7322 = f__990.ToString() == "COUNT(id)";
                string fn__7317()
                {
                    return "countCol";
                }
                test___93.Assert(t___7322, (S1::Func<string>) fn__7317);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__1469()
        {
            T::Test test___94 = new T::Test();
            try
            {
                SqlFragment f__992 = S0::SrcGlobal.SumCol(sid__443("amount"));
                bool t___7315 = f__992.ToString() == "SUM(amount)";
                string fn__7310()
                {
                    return "sumCol";
                }
                test___94.Assert(t___7315, (S1::Func<string>) fn__7310);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__1470()
        {
            T::Test test___95 = new T::Test();
            try
            {
                SqlFragment f__994 = S0::SrcGlobal.AvgCol(sid__443("price"));
                bool t___7308 = f__994.ToString() == "AVG(price)";
                string fn__7303()
                {
                    return "avgCol";
                }
                test___95.Assert(t___7308, (S1::Func<string>) fn__7303);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__1471()
        {
            T::Test test___96 = new T::Test();
            try
            {
                SqlFragment f__996 = S0::SrcGlobal.MinCol(sid__443("created_at"));
                bool t___7301 = f__996.ToString() == "MIN(created_at)";
                string fn__7296()
                {
                    return "minCol";
                }
                test___96.Assert(t___7301, (S1::Func<string>) fn__7296);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__1472()
        {
            T::Test test___97 = new T::Test();
            try
            {
                SqlFragment f__998 = S0::SrcGlobal.MaxCol(sid__443("score"));
                bool t___7294 = f__998.ToString() == "MAX(score)";
                string fn__7289()
                {
                    return "maxCol";
                }
                test___97.Assert(t___7294, (S1::Func<string>) fn__7289);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__1473()
        {
            T::Test test___98 = new T::Test();
            try
            {
                ISafeIdentifier t___7281 = sid__443("orders");
                SqlFragment t___7282 = S0::SrcGlobal.CountAll();
                Query q__1000 = S0::SrcGlobal.From(t___7281).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___7282));
                bool t___7287 = q__1000.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__7280()
                {
                    return "selectExpr count";
                }
                test___98.Assert(t___7287, (S1::Func<string>) fn__7280);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__1474()
        {
            T::Test test___99 = new T::Test();
            try
            {
                SqlFragment nameFrag__1002 = S0::SrcGlobal.Col(sid__443("users"), sid__443("name"));
                ISafeIdentifier t___7272 = sid__443("users");
                SqlFragment t___7273 = S0::SrcGlobal.CountAll();
                Query q__1003 = S0::SrcGlobal.From(t___7272).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1002, t___7273));
                bool t___7278 = q__1003.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__7268()
                {
                    return "selectExpr multi";
                }
                test___99.Assert(t___7278, (S1::Func<string>) fn__7268);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__1475()
        {
            T::Test test___100 = new T::Test();
            try
            {
                ISafeIdentifier t___7257 = sid__443("users");
                ISafeIdentifier t___7258 = sid__443("id");
                ISafeIdentifier t___7259 = sid__443("name");
                Query q__1005 = S0::SrcGlobal.From(t___7257).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7258, t___7259)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___7266 = q__1005.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__7256()
                {
                    return "selectExpr overrides select";
                }
                test___100.Assert(t___7266, (S1::Func<string>) fn__7256);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__1476()
        {
            T::Test test___101 = new T::Test();
            try
            {
                ISafeIdentifier t___7243 = sid__443("orders");
                SqlFragment t___7246 = S0::SrcGlobal.Col(sid__443("orders"), sid__443("status"));
                SqlFragment t___7247 = S0::SrcGlobal.CountAll();
                Query q__1007 = S0::SrcGlobal.From(t___7243).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___7246, t___7247)).GroupBy(sid__443("status"));
                bool t___7254 = q__1007.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__7242()
                {
                    return "groupBy single";
                }
                test___101.Assert(t___7254, (S1::Func<string>) fn__7242);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__1477()
        {
            T::Test test___102 = new T::Test();
            try
            {
                ISafeIdentifier t___7232 = sid__443("orders");
                ISafeIdentifier t___7233 = sid__443("status");
                Query q__1009 = S0::SrcGlobal.From(t___7232).GroupBy(t___7233).GroupBy(sid__443("category"));
                bool t___7240 = q__1009.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__7231()
                {
                    return "groupBy multiple";
                }
                test___102.Assert(t___7240, (S1::Func<string>) fn__7231);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__1478()
        {
            T::Test test___103 = new T::Test();
            try
            {
                ISafeIdentifier t___7213 = sid__443("orders");
                SqlFragment t___7216 = S0::SrcGlobal.Col(sid__443("orders"), sid__443("status"));
                SqlFragment t___7217 = S0::SrcGlobal.CountAll();
                Query t___7220 = S0::SrcGlobal.From(t___7213).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___7216, t___7217)).GroupBy(sid__443("status"));
                SqlBuilder t___7221 = new SqlBuilder();
                t___7221.AppendSafe("COUNT(*) > ");
                t___7221.AppendInt32(5);
                Query q__1011 = t___7220.Having(t___7221.Accumulated);
                bool t___7229 = q__1011.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__7212()
                {
                    return "having basic";
                }
                test___103.Assert(t___7229, (S1::Func<string>) fn__7212);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__1480()
        {
            T::Test test___104 = new T::Test();
            try
            {
                ISafeIdentifier t___7194 = sid__443("orders");
                ISafeIdentifier t___7195 = sid__443("status");
                Query t___7196 = S0::SrcGlobal.From(t___7194).GroupBy(t___7195);
                SqlBuilder t___7197 = new SqlBuilder();
                t___7197.AppendSafe("COUNT(*) > ");
                t___7197.AppendInt32(5);
                Query t___7201 = t___7196.Having(t___7197.Accumulated);
                SqlBuilder t___7202 = new SqlBuilder();
                t___7202.AppendSafe("SUM(total) > ");
                t___7202.AppendInt32(1000);
                Query q__1013 = t___7201.OrHaving(t___7202.Accumulated);
                bool t___7210 = q__1013.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__7193()
                {
                    return "orHaving";
                }
                test___104.Assert(t___7210, (S1::Func<string>) fn__7193);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__1483()
        {
            T::Test test___105 = new T::Test();
            try
            {
                ISafeIdentifier t___7184 = sid__443("users");
                ISafeIdentifier t___7185 = sid__443("name");
                Query q__1015 = S0::SrcGlobal.From(t___7184).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7185)).Distinct();
                bool t___7191 = q__1015.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__7183()
                {
                    return "distinct";
                }
                test___105.Assert(t___7191, (S1::Func<string>) fn__7183);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__1484()
        {
            T::Test test___106 = new T::Test();
            try
            {
                ISafeIdentifier t___7169 = sid__443("users");
                ISafeIdentifier t___7170 = sid__443("email");
                Query t___7171 = S0::SrcGlobal.From(t___7169).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7170));
                SqlBuilder t___7172 = new SqlBuilder();
                t___7172.AppendSafe("active = ");
                t___7172.AppendBoolean(true);
                Query q__1017 = t___7171.Where(t___7172.Accumulated).Distinct();
                bool t___7181 = q__1017.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__7168()
                {
                    return "distinct with where";
                }
                test___106.Assert(t___7181, (S1::Func<string>) fn__7168);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__1486()
        {
            T::Test test___107 = new T::Test();
            try
            {
                Query q__1019 = S0::SrcGlobal.From(sid__443("users"));
                bool t___7166 = q__1019.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__7161()
                {
                    return "countSql bare";
                }
                test___107.Assert(t___7166, (S1::Func<string>) fn__7161);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__1487()
        {
            T::Test test___108 = new T::Test();
            try
            {
                ISafeIdentifier t___7150 = sid__443("users");
                SqlBuilder t___7151 = new SqlBuilder();
                t___7151.AppendSafe("active = ");
                t___7151.AppendBoolean(true);
                SqlFragment t___7154 = t___7151.Accumulated;
                Query q__1021 = S0::SrcGlobal.From(t___7150).Where(t___7154);
                bool t___7159 = q__1021.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__7149()
                {
                    return "countSql with where";
                }
                test___108.Assert(t___7159, (S1::Func<string>) fn__7149);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__1489()
        {
            T::Test test___109 = new T::Test();
            try
            {
                ISafeIdentifier t___7133 = sid__443("users");
                ISafeIdentifier t___7134 = sid__443("orders");
                SqlBuilder t___7135 = new SqlBuilder();
                t___7135.AppendSafe("users.id = orders.user_id");
                SqlFragment t___7137 = t___7135.Accumulated;
                Query t___7138 = S0::SrcGlobal.From(t___7133).InnerJoin(t___7134, t___7137);
                SqlBuilder t___7139 = new SqlBuilder();
                t___7139.AppendSafe("orders.total > ");
                t___7139.AppendInt32(100);
                Query q__1023 = t___7138.Where(t___7139.Accumulated);
                bool t___7147 = q__1023.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__7132()
                {
                    return "countSql with join";
                }
                test___109.Assert(t___7147, (S1::Func<string>) fn__7132);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__1492()
        {
            T::Test test___110 = new T::Test();
            try
            {
                ISafeIdentifier t___7119 = sid__443("users");
                SqlBuilder t___7120 = new SqlBuilder();
                t___7120.AppendSafe("active = ");
                t___7120.AppendBoolean(true);
                SqlFragment t___7123 = t___7120.Accumulated;
                Query t___3568;
                t___3568 = S0::SrcGlobal.From(t___7119).Where(t___7123).OrderBy(sid__443("name"), true).Limit(10);
                Query t___3569;
                t___3569 = t___3568.Offset(20);
                Query q__1025 = t___3569;
                string s__1026 = q__1025.CountSql().ToString();
                bool t___7130 = s__1026 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__7118()
                {
                    return "countSql drops extras: " + s__1026;
                }
                test___110.Assert(t___7130, (S1::Func<string>) fn__7118);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__1494()
        {
            T::Test test___111 = new T::Test();
            try
            {
                ISafeIdentifier t___7086 = sid__443("orders");
                SqlFragment t___7089 = S0::SrcGlobal.Col(sid__443("orders"), sid__443("status"));
                SqlFragment t___7090 = S0::SrcGlobal.CountAll();
                SqlFragment t___7092 = S0::SrcGlobal.SumCol(sid__443("total"));
                Query t___7093 = S0::SrcGlobal.From(t___7086).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___7089, t___7090, t___7092));
                ISafeIdentifier t___7094 = sid__443("users");
                SqlBuilder t___7095 = new SqlBuilder();
                t___7095.AppendSafe("orders.user_id = users.id");
                Query t___7098 = t___7093.InnerJoin(t___7094, t___7095.Accumulated);
                SqlBuilder t___7099 = new SqlBuilder();
                t___7099.AppendSafe("users.active = ");
                t___7099.AppendBoolean(true);
                Query t___7105 = t___7098.Where(t___7099.Accumulated).GroupBy(sid__443("status"));
                SqlBuilder t___7106 = new SqlBuilder();
                t___7106.AppendSafe("COUNT(*) > ");
                t___7106.AppendInt32(3);
                Query q__1028 = t___7105.Having(t___7106.Accumulated).OrderBy(sid__443("status"), true);
                string expected__1029 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___7116 = q__1028.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__7085()
                {
                    return "full aggregation";
                }
                test___111.Assert(t___7116, (S1::Func<string>) fn__7085);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__1498()
        {
            T::Test test___118 = new T::Test();
            try
            {
                ISafeIdentifier t___3522;
                t___3522 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1067 = t___3522;
                bool t___7083 = id__1067.SqlValue == "user_name";
                string fn__7080()
                {
                    return "value should round-trip";
                }
                test___118.Assert(t___7083, (S1::Func<string>) fn__7080);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__1499()
        {
            T::Test test___119 = new T::Test();
            try
            {
                bool didBubble__1069;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1069 = false;
                }
                catch
                {
                    didBubble__1069 = true;
                }
                string fn__7077()
                {
                    return "empty string should bubble";
                }
                test___119.Assert(didBubble__1069, (S1::Func<string>) fn__7077);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__1500()
        {
            T::Test test___120 = new T::Test();
            try
            {
                bool didBubble__1071;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1071 = false;
                }
                catch
                {
                    didBubble__1071 = true;
                }
                string fn__7074()
                {
                    return "leading digit should bubble";
                }
                test___120.Assert(didBubble__1071, (S1::Func<string>) fn__7074);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__1501()
        {
            T::Test test___121 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1073 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__7071(string c__1074)
                {
                    bool didBubble__1075;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1074);
                        didBubble__1075 = false;
                    }
                    catch
                    {
                        didBubble__1075 = true;
                    }
                    string fn__7068()
                    {
                        return "should reject: " + c__1074;
                    }
                    test___121.Assert(didBubble__1075, (S1::Func<string>) fn__7068);
                }
                C::Listed.ForEach(cases__1073, (S1::Action<string>) fn__7071);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__1502()
        {
            T::Test test___122 = new T::Test();
            try
            {
                ISafeIdentifier t___3499;
                t___3499 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___3500 = t___3499;
                ISafeIdentifier t___3501;
                t___3501 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___3502 = t___3501;
                StringField t___7058 = new StringField();
                FieldDef t___7059 = new FieldDef(t___3502, t___7058, false);
                ISafeIdentifier t___3505;
                t___3505 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___3506 = t___3505;
                IntField t___7060 = new IntField();
                FieldDef t___7061 = new FieldDef(t___3506, t___7060, false);
                TableDef td__1077 = new TableDef(t___3500, C::Listed.CreateReadOnlyList<FieldDef>(t___7059, t___7061));
                FieldDef t___3510;
                t___3510 = td__1077.Field("age");
                FieldDef f__1078 = t___3510;
                bool t___7066 = f__1078.Name.SqlValue == "age";
                string fn__7057()
                {
                    return "should find age field";
                }
                test___122.Assert(t___7066, (S1::Func<string>) fn__7057);
            }
            finally
            {
                test___122.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__1503()
        {
            T::Test test___123 = new T::Test();
            try
            {
                ISafeIdentifier t___3490;
                t___3490 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___3491 = t___3490;
                ISafeIdentifier t___3492;
                t___3492 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___3493 = t___3492;
                StringField t___7052 = new StringField();
                FieldDef t___7053 = new FieldDef(t___3493, t___7052, false);
                TableDef td__1080 = new TableDef(t___3491, C::Listed.CreateReadOnlyList<FieldDef>(t___7053));
                bool didBubble__1081;
                try
                {
                    td__1080.Field("nonexistent");
                    didBubble__1081 = false;
                }
                catch
                {
                    didBubble__1081 = true;
                }
                string fn__7051()
                {
                    return "unknown field should bubble";
                }
                test___123.Assert(didBubble__1081, (S1::Func<string>) fn__7051);
            }
            finally
            {
                test___123.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__1504()
        {
            T::Test test___124 = new T::Test();
            try
            {
                ISafeIdentifier t___3478;
                t___3478 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___3479 = t___3478;
                StringField t___7040 = new StringField();
                FieldDef required__1083 = new FieldDef(t___3479, t___7040, false);
                ISafeIdentifier t___3482;
                t___3482 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___3483 = t___3482;
                StringField t___7042 = new StringField();
                FieldDef optional__1084 = new FieldDef(t___3483, t___7042, true);
                bool t___7046 = !required__1083.Nullable;
                string fn__7039()
                {
                    return "required field should not be nullable";
                }
                test___124.Assert(t___7046, (S1::Func<string>) fn__7039);
                bool t___7048 = optional__1084.Nullable;
                string fn__7038()
                {
                    return "optional field should be nullable";
                }
                test___124.Assert(t___7048, (S1::Func<string>) fn__7038);
            }
            finally
            {
                test___124.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__1505()
        {
            T::Test test___128 = new T::Test();
            try
            {
                string build__1210(string name__1212)
                {
                    SqlBuilder t___7020 = new SqlBuilder();
                    t___7020.AppendSafe("select * from hi where name = ");
                    t___7020.AppendString(name__1212);
                    return t___7020.Accumulated.ToString();
                }
                string buildWrong__1211(string name__1214)
                {
                    return "select * from hi where name = '" + name__1214 + "'";
                }
                string actual___1507 = build__1210("world");
                bool t___7030 = actual___1507 == "select * from hi where name = 'world'";
                string fn__7027()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___1507 + ")";
                }
                test___128.Assert(t___7030, (S1::Func<string>) fn__7027);
                string bobbyTables__1216 = "Robert'); drop table hi;--";
                string actual___1509 = build__1210("Robert'); drop table hi;--");
                bool t___7034 = actual___1509 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__7026()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___1509 + ")";
                }
                test___128.Assert(t___7034, (S1::Func<string>) fn__7026);
                string fn__7025()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___128.Assert(true, (S1::Func<string>) fn__7025);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__1513()
        {
            T::Test test___129 = new T::Test();
            try
            {
                SqlBuilder t___6988 = new SqlBuilder();
                t___6988.AppendSafe("v = ");
                t___6988.AppendString("");
                string actual___1514 = t___6988.Accumulated.ToString();
                bool t___6994 = actual___1514 == "v = ''";
                string fn__6987()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___1514 + ")";
                }
                test___129.Assert(t___6994, (S1::Func<string>) fn__6987);
                SqlBuilder t___6996 = new SqlBuilder();
                t___6996.AppendSafe("v = ");
                t___6996.AppendString("a''b");
                string actual___1517 = t___6996.Accumulated.ToString();
                bool t___7002 = actual___1517 == "v = 'a''''b'";
                string fn__6986()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___1517 + ")";
                }
                test___129.Assert(t___7002, (S1::Func<string>) fn__6986);
                SqlBuilder t___7004 = new SqlBuilder();
                t___7004.AppendSafe("v = ");
                t___7004.AppendString("Hello 世界");
                string actual___1520 = t___7004.Accumulated.ToString();
                bool t___7010 = actual___1520 == "v = 'Hello 世界'";
                string fn__6985()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1520 + ")";
                }
                test___129.Assert(t___7010, (S1::Func<string>) fn__6985);
                SqlBuilder t___7012 = new SqlBuilder();
                t___7012.AppendSafe("v = ");
                t___7012.AppendString("Line1\nLine2");
                string actual___1523 = t___7012.Accumulated.ToString();
                bool t___7018 = actual___1523 == "v = 'Line1\nLine2'";
                string fn__6984()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1523 + ")";
                }
                test___129.Assert(t___7018, (S1::Func<string>) fn__6984);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1526()
        {
            T::Test test___130 = new T::Test();
            try
            {
                SqlBuilder t___6959 = new SqlBuilder();
                t___6959.AppendSafe("select ");
                t___6959.AppendInt32(42);
                t___6959.AppendSafe(", ");
                t___6959.AppendInt64(43);
                t___6959.AppendSafe(", ");
                t___6959.AppendFloat64(19.99);
                t___6959.AppendSafe(", ");
                t___6959.AppendBoolean(true);
                t___6959.AppendSafe(", ");
                t___6959.AppendBoolean(false);
                string actual___1527 = t___6959.Accumulated.ToString();
                bool t___6973 = actual___1527 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__6958()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1527 + ")";
                }
                test___130.Assert(t___6973, (S1::Func<string>) fn__6958);
                S1::DateTime t___3423;
                t___3423 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1219 = t___3423;
                SqlBuilder t___6975 = new SqlBuilder();
                t___6975.AppendSafe("insert into t values (");
                t___6975.AppendDate(date__1219);
                t___6975.AppendSafe(")");
                string actual___1530 = t___6975.Accumulated.ToString();
                bool t___6982 = actual___1530 == "insert into t values ('2024-12-25')";
                string fn__6957()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1530 + ")";
                }
                test___130.Assert(t___6982, (S1::Func<string>) fn__6957);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1533()
        {
            T::Test test___131 = new T::Test();
            try
            {
                SqlBuilder t___6903 = new SqlBuilder();
                t___6903.AppendSafe("v IN (");
                t___6903.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___6903.AppendSafe(")");
                string actual___1534 = t___6903.Accumulated.ToString();
                bool t___6910 = actual___1534 == "v IN ('a', 'b', 'c''d')";
                string fn__6902()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1534 + ")";
                }
                test___131.Assert(t___6910, (S1::Func<string>) fn__6902);
                SqlBuilder t___6912 = new SqlBuilder();
                t___6912.AppendSafe("v IN (");
                t___6912.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___6912.AppendSafe(")");
                string actual___1537 = t___6912.Accumulated.ToString();
                bool t___6919 = actual___1537 == "v IN (1, 2, 3)";
                string fn__6901()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1537 + ")";
                }
                test___131.Assert(t___6919, (S1::Func<string>) fn__6901);
                SqlBuilder t___6921 = new SqlBuilder();
                t___6921.AppendSafe("v IN (");
                t___6921.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___6921.AppendSafe(")");
                string actual___1540 = t___6921.Accumulated.ToString();
                bool t___6928 = actual___1540 == "v IN (1, 2)";
                string fn__6900()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1540 + ")";
                }
                test___131.Assert(t___6928, (S1::Func<string>) fn__6900);
                SqlBuilder t___6930 = new SqlBuilder();
                t___6930.AppendSafe("v IN (");
                t___6930.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___6930.AppendSafe(")");
                string actual___1543 = t___6930.Accumulated.ToString();
                bool t___6937 = actual___1543 == "v IN (1.0, 2.0)";
                string fn__6899()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1543 + ")";
                }
                test___131.Assert(t___6937, (S1::Func<string>) fn__6899);
                SqlBuilder t___6939 = new SqlBuilder();
                t___6939.AppendSafe("v IN (");
                t___6939.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___6939.AppendSafe(")");
                string actual___1546 = t___6939.Accumulated.ToString();
                bool t___6946 = actual___1546 == "v IN (TRUE, FALSE)";
                string fn__6898()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1546 + ")";
                }
                test___131.Assert(t___6946, (S1::Func<string>) fn__6898);
                S1::DateTime t___3395;
                t___3395 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___3396 = t___3395;
                S1::DateTime t___3397;
                t___3397 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___3398 = t___3397;
                G::IReadOnlyList<S1::DateTime> dates__1221 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___3396, t___3398);
                SqlBuilder t___6948 = new SqlBuilder();
                t___6948.AppendSafe("v IN (");
                t___6948.AppendDateList(dates__1221);
                t___6948.AppendSafe(")");
                string actual___1549 = t___6948.Accumulated.ToString();
                bool t___6955 = actual___1549 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__6897()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1549 + ")";
                }
                test___131.Assert(t___6955, (S1::Func<string>) fn__6897);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1552()
        {
            T::Test test___132 = new T::Test();
            try
            {
                double nan__1223;
                nan__1223 = 0.0 / 0.0;
                SqlBuilder t___6889 = new SqlBuilder();
                t___6889.AppendSafe("v = ");
                t___6889.AppendFloat64(nan__1223);
                string actual___1553 = t___6889.Accumulated.ToString();
                bool t___6895 = actual___1553 == "v = NULL";
                string fn__6888()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1553 + ")";
                }
                test___132.Assert(t___6895, (S1::Func<string>) fn__6888);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1556()
        {
            T::Test test___133 = new T::Test();
            try
            {
                double inf__1225;
                inf__1225 = 1.0 / 0.0;
                SqlBuilder t___6880 = new SqlBuilder();
                t___6880.AppendSafe("v = ");
                t___6880.AppendFloat64(inf__1225);
                string actual___1557 = t___6880.Accumulated.ToString();
                bool t___6886 = actual___1557 == "v = NULL";
                string fn__6879()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1557 + ")";
                }
                test___133.Assert(t___6886, (S1::Func<string>) fn__6879);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1560()
        {
            T::Test test___134 = new T::Test();
            try
            {
                double ninf__1227;
                ninf__1227 = -1.0 / 0.0;
                SqlBuilder t___6871 = new SqlBuilder();
                t___6871.AppendSafe("v = ");
                t___6871.AppendFloat64(ninf__1227);
                string actual___1561 = t___6871.Accumulated.ToString();
                bool t___6877 = actual___1561 == "v = NULL";
                string fn__6870()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1561 + ")";
                }
                test___134.Assert(t___6877, (S1::Func<string>) fn__6870);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1564()
        {
            T::Test test___135 = new T::Test();
            try
            {
                SqlBuilder t___6846 = new SqlBuilder();
                t___6846.AppendSafe("v = ");
                t___6846.AppendFloat64(3.14);
                string actual___1565 = t___6846.Accumulated.ToString();
                bool t___6852 = actual___1565 == "v = 3.14";
                string fn__6845()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1565 + ")";
                }
                test___135.Assert(t___6852, (S1::Func<string>) fn__6845);
                SqlBuilder t___6854 = new SqlBuilder();
                t___6854.AppendSafe("v = ");
                t___6854.AppendFloat64(0.0);
                string actual___1568 = t___6854.Accumulated.ToString();
                bool t___6860 = actual___1568 == "v = 0.0";
                string fn__6844()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1568 + ")";
                }
                test___135.Assert(t___6860, (S1::Func<string>) fn__6844);
                SqlBuilder t___6862 = new SqlBuilder();
                t___6862.AppendSafe("v = ");
                t___6862.AppendFloat64(-42.5);
                string actual___1571 = t___6862.Accumulated.ToString();
                bool t___6868 = actual___1571 == "v = -42.5";
                string fn__6843()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1571 + ")";
                }
                test___135.Assert(t___6868, (S1::Func<string>) fn__6843);
            }
            finally
            {
                test___135.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1574()
        {
            T::Test test___136 = new T::Test();
            try
            {
                S1::DateTime t___3291;
                t___3291 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1230 = t___3291;
                SqlBuilder t___6835 = new SqlBuilder();
                t___6835.AppendSafe("v = ");
                t___6835.AppendDate(d__1230);
                string actual___1575 = t___6835.Accumulated.ToString();
                bool t___6841 = actual___1575 == "v = '2024-06-15'";
                string fn__6834()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1575 + ")";
                }
                test___136.Assert(t___6841, (S1::Func<string>) fn__6834);
            }
            finally
            {
                test___136.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1578()
        {
            T::Test test___137 = new T::Test();
            try
            {
                string name__1232 = "Someone";
                SqlBuilder t___6803 = new SqlBuilder();
                t___6803.AppendSafe("where p.last_name = ");
                t___6803.AppendString("Someone");
                SqlFragment condition__1233 = t___6803.Accumulated;
                SqlBuilder t___6807 = new SqlBuilder();
                t___6807.AppendSafe("select p.id from person p ");
                t___6807.AppendFragment(condition__1233);
                string actual___1580 = t___6807.Accumulated.ToString();
                bool t___6813 = actual___1580 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__6802()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1580 + ")";
                }
                test___137.Assert(t___6813, (S1::Func<string>) fn__6802);
                SqlBuilder t___6815 = new SqlBuilder();
                t___6815.AppendSafe("select p.id from person p ");
                t___6815.AppendPart(condition__1233.ToSource());
                string actual___1583 = t___6815.Accumulated.ToString();
                bool t___6822 = actual___1583 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__6801()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1583 + ")";
                }
                test___137.Assert(t___6822, (S1::Func<string>) fn__6801);
                G::IReadOnlyList<ISqlPart> parts__1234 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___6826 = new SqlBuilder();
                t___6826.AppendSafe("select ");
                t___6826.AppendPartList(parts__1234);
                string actual___1586 = t___6826.Accumulated.ToString();
                bool t___6832 = actual___1586 == "select 'a''b', 3";
                string fn__6800()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___1586 + ")";
                }
                test___137.Assert(t___6832, (S1::Func<string>) fn__6800);
            }
            finally
            {
                test___137.SoftFailToHard();
            }
        }
    }
}
