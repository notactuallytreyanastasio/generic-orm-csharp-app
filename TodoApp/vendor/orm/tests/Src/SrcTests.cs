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
        internal static ISafeIdentifier csid__459(string name__604)
        {
            ISafeIdentifier t___5286;
            t___5286 = S0::SrcGlobal.SafeIdentifier(name__604);
            return t___5286;
        }
        internal static TableDef userTable__460()
        {
            return new TableDef(csid__459("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__459("name"), new StringField(), false), new FieldDef(csid__459("email"), new StringField(), false), new FieldDef(csid__459("age"), new IntField(), true), new FieldDef(csid__459("score"), new FloatField(), true), new FieldDef(csid__459("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__1404()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__608 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___9397 = userTable__460();
                ISafeIdentifier t___9398 = csid__459("name");
                ISafeIdentifier t___9399 = csid__459("email");
                IChangeset cs__609 = S0::SrcGlobal.Changeset(t___9397, params__608).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9398, t___9399));
                bool t___9402 = C::Mapped.ContainsKey(cs__609.Changes, "name");
                string fn__9392()
                {
                    return "name should be in changes";
                }
                test___24.Assert(t___9402, (S1::Func<string>) fn__9392);
                bool t___9406 = C::Mapped.ContainsKey(cs__609.Changes, "email");
                string fn__9391()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___9406, (S1::Func<string>) fn__9391);
                bool t___9412 = !C::Mapped.ContainsKey(cs__609.Changes, "admin");
                string fn__9390()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___24.Assert(t___9412, (S1::Func<string>) fn__9390);
                bool t___9414 = cs__609.IsValid;
                string fn__9389()
                {
                    return "should still be valid";
                }
                test___24.Assert(t___9414, (S1::Func<string>) fn__9389);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__1405()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__611 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___9375 = userTable__460();
                ISafeIdentifier t___9376 = csid__459("name");
                IChangeset cs__612 = S0::SrcGlobal.Changeset(t___9375, params__611).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9376)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("email")));
                bool t___9383 = !C::Mapped.ContainsKey(cs__612.Changes, "name");
                string fn__9371()
                {
                    return "name must be excluded by second cast";
                }
                test___25.Assert(t___9383, (S1::Func<string>) fn__9371);
                bool t___9386 = C::Mapped.ContainsKey(cs__612.Changes, "email");
                string fn__9370()
                {
                    return "email should be present";
                }
                test___25.Assert(t___9386, (S1::Func<string>) fn__9370);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__1406()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__614 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___9357 = userTable__460();
                ISafeIdentifier t___9358 = csid__459("name");
                ISafeIdentifier t___9359 = csid__459("email");
                IChangeset cs__615 = S0::SrcGlobal.Changeset(t___9357, params__614).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9358, t___9359));
                bool t___9364 = !C::Mapped.ContainsKey(cs__615.Changes, "name");
                string fn__9353()
                {
                    return "empty name should not be in changes";
                }
                test___26.Assert(t___9364, (S1::Func<string>) fn__9353);
                bool t___9367 = C::Mapped.ContainsKey(cs__615.Changes, "email");
                string fn__9352()
                {
                    return "email should be in changes";
                }
                test___26.Assert(t___9367, (S1::Func<string>) fn__9352);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__1407()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__617 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___9339 = userTable__460();
                ISafeIdentifier t___9340 = csid__459("name");
                IChangeset cs__618 = S0::SrcGlobal.Changeset(t___9339, params__617).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9340)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name")));
                bool t___9344 = cs__618.IsValid;
                string fn__9336()
                {
                    return "should be valid";
                }
                test___27.Assert(t___9344, (S1::Func<string>) fn__9336);
                bool t___9350 = cs__618.Errors.Count == 0;
                string fn__9335()
                {
                    return "no errors expected";
                }
                test___27.Assert(t___9350, (S1::Func<string>) fn__9335);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__1408()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__620 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___9315 = userTable__460();
                ISafeIdentifier t___9316 = csid__459("name");
                IChangeset cs__621 = S0::SrcGlobal.Changeset(t___9315, params__620).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9316)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name")));
                bool t___9322 = !cs__621.IsValid;
                string fn__9313()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___9322, (S1::Func<string>) fn__9313);
                bool t___9327 = cs__621.Errors.Count == 1;
                string fn__9312()
                {
                    return "should have one error";
                }
                test___28.Assert(t___9327, (S1::Func<string>) fn__9312);
                bool t___9333 = cs__621.Errors[0].Field == "name";
                string fn__9311()
                {
                    return "error should name the field";
                }
                test___28.Assert(t___9333, (S1::Func<string>) fn__9311);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__1409()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__623 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___9303 = userTable__460();
                ISafeIdentifier t___9304 = csid__459("name");
                IChangeset cs__624 = S0::SrcGlobal.Changeset(t___9303, params__623).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9304)).ValidateLength(csid__459("name"), 2, 50);
                bool t___9308 = cs__624.IsValid;
                string fn__9300()
                {
                    return "should be valid";
                }
                test___29.Assert(t___9308, (S1::Func<string>) fn__9300);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__1410()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__626 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___9291 = userTable__460();
                ISafeIdentifier t___9292 = csid__459("name");
                IChangeset cs__627 = S0::SrcGlobal.Changeset(t___9291, params__626).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9292)).ValidateLength(csid__459("name"), 2, 50);
                bool t___9298 = !cs__627.IsValid;
                string fn__9288()
                {
                    return "should be invalid";
                }
                test___30.Assert(t___9298, (S1::Func<string>) fn__9288);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__1411()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__629 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___9279 = userTable__460();
                ISafeIdentifier t___9280 = csid__459("name");
                IChangeset cs__630 = S0::SrcGlobal.Changeset(t___9279, params__629).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9280)).ValidateLength(csid__459("name"), 2, 10);
                bool t___9286 = !cs__630.IsValid;
                string fn__9276()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___9286, (S1::Func<string>) fn__9276);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__1412()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__632 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___9268 = userTable__460();
                ISafeIdentifier t___9269 = csid__459("age");
                IChangeset cs__633 = S0::SrcGlobal.Changeset(t___9268, params__632).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9269)).ValidateInt(csid__459("age"));
                bool t___9273 = cs__633.IsValid;
                string fn__9265()
                {
                    return "should be valid";
                }
                test___32.Assert(t___9273, (S1::Func<string>) fn__9265);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__1413()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__635 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___9256 = userTable__460();
                ISafeIdentifier t___9257 = csid__459("age");
                IChangeset cs__636 = S0::SrcGlobal.Changeset(t___9256, params__635).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9257)).ValidateInt(csid__459("age"));
                bool t___9263 = !cs__636.IsValid;
                string fn__9253()
                {
                    return "should be invalid";
                }
                test___33.Assert(t___9263, (S1::Func<string>) fn__9253);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__1414()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__638 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___9245 = userTable__460();
                ISafeIdentifier t___9246 = csid__459("score");
                IChangeset cs__639 = S0::SrcGlobal.Changeset(t___9245, params__638).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9246)).ValidateFloat(csid__459("score"));
                bool t___9250 = cs__639.IsValid;
                string fn__9242()
                {
                    return "should be valid";
                }
                test___34.Assert(t___9250, (S1::Func<string>) fn__9242);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__1415()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__641 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___9234 = userTable__460();
                ISafeIdentifier t___9235 = csid__459("age");
                IChangeset cs__642 = S0::SrcGlobal.Changeset(t___9234, params__641).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9235)).ValidateInt64(csid__459("age"));
                bool t___9239 = cs__642.IsValid;
                string fn__9231()
                {
                    return "should be valid";
                }
                test___35.Assert(t___9239, (S1::Func<string>) fn__9231);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__1416()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__644 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___9222 = userTable__460();
                ISafeIdentifier t___9223 = csid__459("age");
                IChangeset cs__645 = S0::SrcGlobal.Changeset(t___9222, params__644).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9223)).ValidateInt64(csid__459("age"));
                bool t___9229 = !cs__645.IsValid;
                string fn__9219()
                {
                    return "should be invalid";
                }
                test___36.Assert(t___9229, (S1::Func<string>) fn__9219);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__1417()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__9216(string v__647)
                {
                    G::IReadOnlyDictionary<string, string> params__648 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__647)));
                    TableDef t___9208 = userTable__460();
                    ISafeIdentifier t___9209 = csid__459("active");
                    IChangeset cs__649 = S0::SrcGlobal.Changeset(t___9208, params__648).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9209)).ValidateBool(csid__459("active"));
                    bool t___9213 = cs__649.IsValid;
                    string fn__9205()
                    {
                        return "should accept: " + v__647;
                    }
                    test___37.Assert(t___9213, (S1::Func<string>) fn__9205);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__9216);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__1418()
        {
            T::Test test___38 = new T::Test();
            try
            {
                void fn__9202(string v__651)
                {
                    G::IReadOnlyDictionary<string, string> params__652 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__651)));
                    TableDef t___9194 = userTable__460();
                    ISafeIdentifier t___9195 = csid__459("active");
                    IChangeset cs__653 = S0::SrcGlobal.Changeset(t___9194, params__652).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9195)).ValidateBool(csid__459("active"));
                    bool t___9199 = cs__653.IsValid;
                    string fn__9191()
                    {
                        return "should accept: " + v__651;
                    }
                    test___38.Assert(t___9199, (S1::Func<string>) fn__9191);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__9202);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__1419()
        {
            T::Test test___39 = new T::Test();
            try
            {
                void fn__9188(string v__655)
                {
                    G::IReadOnlyDictionary<string, string> params__656 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__655)));
                    TableDef t___9179 = userTable__460();
                    ISafeIdentifier t___9180 = csid__459("active");
                    IChangeset cs__657 = S0::SrcGlobal.Changeset(t___9179, params__656).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9180)).ValidateBool(csid__459("active"));
                    bool t___9186 = !cs__657.IsValid;
                    string fn__9176()
                    {
                        return "should reject ambiguous: " + v__655;
                    }
                    test___39.Assert(t___9186, (S1::Func<string>) fn__9176);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__9188);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__1420()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__659 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___9164 = userTable__460();
                ISafeIdentifier t___9165 = csid__459("name");
                ISafeIdentifier t___9166 = csid__459("email");
                IChangeset cs__660 = S0::SrcGlobal.Changeset(t___9164, params__659).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9165, t___9166)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name"), csid__459("email")));
                SqlFragment t___5087;
                t___5087 = cs__660.ToInsertSql();
                SqlFragment sqlFrag__661 = t___5087;
                string s__662 = sqlFrag__661.ToString();
                bool t___9173 = s__662.IndexOf("''") >= 0;
                string fn__9160()
                {
                    return "single quote must be doubled: " + s__662;
                }
                test___40.Assert(t___9173, (S1::Func<string>) fn__9160);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__1421()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__664 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___9144 = userTable__460();
                ISafeIdentifier t___9145 = csid__459("name");
                ISafeIdentifier t___9146 = csid__459("email");
                IChangeset cs__665 = S0::SrcGlobal.Changeset(t___9144, params__664).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9145, t___9146)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name"), csid__459("email")));
                SqlFragment t___5066;
                t___5066 = cs__665.ToInsertSql();
                SqlFragment sqlFrag__666 = t___5066;
                string s__667 = sqlFrag__666.ToString();
                bool t___9153 = s__667.IndexOf("INSERT INTO users") >= 0;
                string fn__9140()
                {
                    return "has INSERT INTO: " + s__667;
                }
                test___41.Assert(t___9153, (S1::Func<string>) fn__9140);
                bool t___9157 = s__667.IndexOf("'Alice'") >= 0;
                string fn__9139()
                {
                    return "has quoted name: " + s__667;
                }
                test___41.Assert(t___9157, (S1::Func<string>) fn__9139);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__1422()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__669 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___9126 = userTable__460();
                ISafeIdentifier t___9127 = csid__459("name");
                ISafeIdentifier t___9128 = csid__459("email");
                ISafeIdentifier t___9129 = csid__459("age");
                IChangeset cs__670 = S0::SrcGlobal.Changeset(t___9126, params__669).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9127, t___9128, t___9129)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name"), csid__459("email")));
                SqlFragment t___5049;
                t___5049 = cs__670.ToInsertSql();
                SqlFragment sqlFrag__671 = t___5049;
                string s__672 = sqlFrag__671.ToString();
                bool t___9136 = s__672.IndexOf("25") >= 0;
                string fn__9121()
                {
                    return "age rendered unquoted: " + s__672;
                }
                test___42.Assert(t___9136, (S1::Func<string>) fn__9121);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__1423()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__674 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___9114 = userTable__460();
                ISafeIdentifier t___9115 = csid__459("name");
                IChangeset cs__675 = S0::SrcGlobal.Changeset(t___9114, params__674).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9115)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name")));
                bool didBubble__676;
                try
                {
                    cs__675.ToInsertSql();
                    didBubble__676 = false;
                }
                catch
                {
                    didBubble__676 = true;
                }
                string fn__9112()
                {
                    return "invalid changeset should bubble";
                }
                test___43.Assert(didBubble__676, (S1::Func<string>) fn__9112);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__1424()
        {
            T::Test test___44 = new T::Test();
            try
            {
                TableDef strictTable__678 = new TableDef(csid__459("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__459("title"), new StringField(), false), new FieldDef(csid__459("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__679 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___9105 = csid__459("body");
                IChangeset cs__680 = S0::SrcGlobal.Changeset(strictTable__678, params__679).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9105));
                bool t___9107 = cs__680.IsValid;
                string fn__9094()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___44.Assert(t___9107, (S1::Func<string>) fn__9094);
                bool didBubble__681;
                try
                {
                    cs__680.ToInsertSql();
                    didBubble__681 = false;
                }
                catch
                {
                    didBubble__681 = true;
                }
                string fn__9093()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___44.Assert(didBubble__681, (S1::Func<string>) fn__9093);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__1425()
        {
            T::Test test___45 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__683 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___9084 = userTable__460();
                ISafeIdentifier t___9085 = csid__459("name");
                IChangeset cs__684 = S0::SrcGlobal.Changeset(t___9084, params__683).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9085)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name")));
                SqlFragment t___5009;
                t___5009 = cs__684.ToUpdateSql(42);
                SqlFragment sqlFrag__685 = t___5009;
                string s__686 = sqlFrag__685.ToString();
                bool t___9091 = s__686 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__9081()
                {
                    return "got: " + s__686;
                }
                test___45.Assert(t___9091, (S1::Func<string>) fn__9081);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__1426()
        {
            T::Test test___46 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__688 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___9074 = userTable__460();
                ISafeIdentifier t___9075 = csid__459("name");
                IChangeset cs__689 = S0::SrcGlobal.Changeset(t___9074, params__688).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9075)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__459("name")));
                bool didBubble__690;
                try
                {
                    cs__689.ToUpdateSql(1);
                    didBubble__690 = false;
                }
                catch
                {
                    didBubble__690 = true;
                }
                string fn__9072()
                {
                    return "invalid changeset should bubble";
                }
                test___46.Assert(didBubble__690, (S1::Func<string>) fn__9072);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__461(string name__932)
        {
            ISafeIdentifier t___4603;
            t___4603 = S0::SrcGlobal.SafeIdentifier(name__932);
            return t___4603;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__1475()
        {
            T::Test test___47 = new T::Test();
            try
            {
                Query q__935 = S0::SrcGlobal.From(sid__461("users"));
                bool t___8681 = q__935.ToSql().ToString() == "SELECT * FROM users";
                string fn__8676()
                {
                    return "bare query";
                }
                test___47.Assert(t___8681, (S1::Func<string>) fn__8676);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__1476()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___8667 = sid__461("users");
                ISafeIdentifier t___8668 = sid__461("id");
                ISafeIdentifier t___8669 = sid__461("name");
                Query q__937 = S0::SrcGlobal.From(t___8667).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8668, t___8669));
                bool t___8674 = q__937.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__8666()
                {
                    return "select columns";
                }
                test___48.Assert(t___8674, (S1::Func<string>) fn__8666);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__1477()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___8655 = sid__461("users");
                SqlBuilder t___8656 = new SqlBuilder();
                t___8656.AppendSafe("age > ");
                t___8656.AppendInt32(18);
                SqlFragment t___8659 = t___8656.Accumulated;
                Query q__939 = S0::SrcGlobal.From(t___8655).Where(t___8659);
                bool t___8664 = q__939.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__8654()
                {
                    return "where int";
                }
                test___49.Assert(t___8664, (S1::Func<string>) fn__8654);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__1479()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___8643 = sid__461("users");
                SqlBuilder t___8644 = new SqlBuilder();
                t___8644.AppendSafe("active = ");
                t___8644.AppendBoolean(true);
                SqlFragment t___8647 = t___8644.Accumulated;
                Query q__941 = S0::SrcGlobal.From(t___8643).Where(t___8647);
                bool t___8652 = q__941.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__8642()
                {
                    return "where bool";
                }
                test___50.Assert(t___8652, (S1::Func<string>) fn__8642);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__1481()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___8626 = sid__461("users");
                SqlBuilder t___8627 = new SqlBuilder();
                t___8627.AppendSafe("age > ");
                t___8627.AppendInt32(18);
                SqlFragment t___8630 = t___8627.Accumulated;
                Query t___8631 = S0::SrcGlobal.From(t___8626).Where(t___8630);
                SqlBuilder t___8632 = new SqlBuilder();
                t___8632.AppendSafe("active = ");
                t___8632.AppendBoolean(true);
                Query q__943 = t___8631.Where(t___8632.Accumulated);
                bool t___8640 = q__943.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__8625()
                {
                    return "chained where";
                }
                test___51.Assert(t___8640, (S1::Func<string>) fn__8625);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__1484()
        {
            T::Test test___52 = new T::Test();
            try
            {
                ISafeIdentifier t___8617 = sid__461("users");
                ISafeIdentifier t___8618 = sid__461("name");
                Query q__945 = S0::SrcGlobal.From(t___8617).OrderBy(t___8618, true);
                bool t___8623 = q__945.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__8616()
                {
                    return "order asc";
                }
                test___52.Assert(t___8623, (S1::Func<string>) fn__8616);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__1485()
        {
            T::Test test___53 = new T::Test();
            try
            {
                ISafeIdentifier t___8608 = sid__461("users");
                ISafeIdentifier t___8609 = sid__461("created_at");
                Query q__947 = S0::SrcGlobal.From(t___8608).OrderBy(t___8609, false);
                bool t___8614 = q__947.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__8607()
                {
                    return "order desc";
                }
                test___53.Assert(t___8614, (S1::Func<string>) fn__8607);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__1486()
        {
            T::Test test___54 = new T::Test();
            try
            {
                Query t___4537;
                t___4537 = S0::SrcGlobal.From(sid__461("users")).Limit(10);
                Query t___4538;
                t___4538 = t___4537.Offset(20);
                Query q__949 = t___4538;
                bool t___8605 = q__949.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__8600()
                {
                    return "limit/offset";
                }
                test___54.Assert(t___8605, (S1::Func<string>) fn__8600);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__1487()
        {
            T::Test test___55 = new T::Test();
            try
            {
                bool didBubble__951;
                try
                {
                    S0::SrcGlobal.From(sid__461("users")).Limit(-1);
                    didBubble__951 = false;
                }
                catch
                {
                    didBubble__951 = true;
                }
                string fn__8596()
                {
                    return "negative limit should bubble";
                }
                test___55.Assert(didBubble__951, (S1::Func<string>) fn__8596);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__1488()
        {
            T::Test test___56 = new T::Test();
            try
            {
                bool didBubble__953;
                try
                {
                    S0::SrcGlobal.From(sid__461("users")).Offset(-1);
                    didBubble__953 = false;
                }
                catch
                {
                    didBubble__953 = true;
                }
                string fn__8592()
                {
                    return "negative offset should bubble";
                }
                test___56.Assert(didBubble__953, (S1::Func<string>) fn__8592);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__1489()
        {
            T::Test test___57 = new T::Test();
            try
            {
                int minAge__955 = 21;
                ISafeIdentifier t___8570 = sid__461("users");
                ISafeIdentifier t___8571 = sid__461("id");
                ISafeIdentifier t___8572 = sid__461("name");
                ISafeIdentifier t___8573 = sid__461("email");
                Query t___8574 = S0::SrcGlobal.From(t___8570).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8571, t___8572, t___8573));
                SqlBuilder t___8575 = new SqlBuilder();
                t___8575.AppendSafe("age >= ");
                t___8575.AppendInt32(21);
                Query t___8579 = t___8574.Where(t___8575.Accumulated);
                SqlBuilder t___8580 = new SqlBuilder();
                t___8580.AppendSafe("active = ");
                t___8580.AppendBoolean(true);
                Query t___4523;
                t___4523 = t___8579.Where(t___8580.Accumulated).OrderBy(sid__461("name"), true).Limit(25);
                Query t___4524;
                t___4524 = t___4523.Offset(0);
                Query q__956 = t___4524;
                bool t___8590 = q__956.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__8569()
                {
                    return "complex query";
                }
                test___57.Assert(t___8590, (S1::Func<string>) fn__8569);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__1492()
        {
            T::Test test___58 = new T::Test();
            try
            {
                Query q__958 = S0::SrcGlobal.From(sid__461("users"));
                SqlFragment t___4500;
                t___4500 = q__958.SafeToSql(100);
                SqlFragment t___4501 = t___4500;
                string s__959 = t___4501.ToString();
                bool t___8567 = s__959 == "SELECT * FROM users LIMIT 100";
                string fn__8563()
                {
                    return "should have limit: " + s__959;
                }
                test___58.Assert(t___8567, (S1::Func<string>) fn__8563);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__1493()
        {
            T::Test test___59 = new T::Test();
            try
            {
                Query t___4492;
                t___4492 = S0::SrcGlobal.From(sid__461("users")).Limit(5);
                Query q__961 = t___4492;
                SqlFragment t___4495;
                t___4495 = q__961.SafeToSql(100);
                SqlFragment t___4496 = t___4495;
                string s__962 = t___4496.ToString();
                bool t___8561 = s__962 == "SELECT * FROM users LIMIT 5";
                string fn__8557()
                {
                    return "explicit limit preserved: " + s__962;
                }
                test___59.Assert(t___8561, (S1::Func<string>) fn__8557);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__1494()
        {
            T::Test test___60 = new T::Test();
            try
            {
                bool didBubble__964;
                try
                {
                    S0::SrcGlobal.From(sid__461("users")).SafeToSql(-1);
                    didBubble__964 = false;
                }
                catch
                {
                    didBubble__964 = true;
                }
                string fn__8553()
                {
                    return "negative defaultLimit should bubble";
                }
                test___60.Assert(didBubble__964, (S1::Func<string>) fn__8553);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__1495()
        {
            T::Test test___61 = new T::Test();
            try
            {
                string evil__966 = "'; DROP TABLE users; --";
                ISafeIdentifier t___8537 = sid__461("users");
                SqlBuilder t___8538 = new SqlBuilder();
                t___8538.AppendSafe("name = ");
                t___8538.AppendString("'; DROP TABLE users; --");
                SqlFragment t___8541 = t___8538.Accumulated;
                Query q__967 = S0::SrcGlobal.From(t___8537).Where(t___8541);
                string s__968 = q__967.ToSql().ToString();
                bool t___8546 = s__968.IndexOf("''") >= 0;
                string fn__8536()
                {
                    return "quotes must be doubled: " + s__968;
                }
                test___61.Assert(t___8546, (S1::Func<string>) fn__8536);
                bool t___8550 = s__968.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__8535()
                {
                    return "structure intact: " + s__968;
                }
                test___61.Assert(t___8550, (S1::Func<string>) fn__8535);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__1497()
        {
            T::Test test___62 = new T::Test();
            try
            {
                string attack__970 = "users; DROP TABLE users; --";
                bool didBubble__971;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__971 = false;
                }
                catch
                {
                    didBubble__971 = true;
                }
                string fn__8532()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___62.Assert(didBubble__971, (S1::Func<string>) fn__8532);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__1498()
        {
            T::Test test___63 = new T::Test();
            try
            {
                ISafeIdentifier t___8521 = sid__461("users");
                ISafeIdentifier t___8522 = sid__461("orders");
                SqlBuilder t___8523 = new SqlBuilder();
                t___8523.AppendSafe("users.id = orders.user_id");
                SqlFragment t___8525 = t___8523.Accumulated;
                Query q__973 = S0::SrcGlobal.From(t___8521).InnerJoin(t___8522, t___8525);
                bool t___8530 = q__973.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__8520()
                {
                    return "inner join";
                }
                test___63.Assert(t___8530, (S1::Func<string>) fn__8520);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__1500()
        {
            T::Test test___64 = new T::Test();
            try
            {
                ISafeIdentifier t___8509 = sid__461("users");
                ISafeIdentifier t___8510 = sid__461("profiles");
                SqlBuilder t___8511 = new SqlBuilder();
                t___8511.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___8513 = t___8511.Accumulated;
                Query q__975 = S0::SrcGlobal.From(t___8509).LeftJoin(t___8510, t___8513);
                bool t___8518 = q__975.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__8508()
                {
                    return "left join";
                }
                test___64.Assert(t___8518, (S1::Func<string>) fn__8508);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__1502()
        {
            T::Test test___65 = new T::Test();
            try
            {
                ISafeIdentifier t___8497 = sid__461("orders");
                ISafeIdentifier t___8498 = sid__461("users");
                SqlBuilder t___8499 = new SqlBuilder();
                t___8499.AppendSafe("orders.user_id = users.id");
                SqlFragment t___8501 = t___8499.Accumulated;
                Query q__977 = S0::SrcGlobal.From(t___8497).RightJoin(t___8498, t___8501);
                bool t___8506 = q__977.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__8496()
                {
                    return "right join";
                }
                test___65.Assert(t___8506, (S1::Func<string>) fn__8496);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__1504()
        {
            T::Test test___66 = new T::Test();
            try
            {
                ISafeIdentifier t___8485 = sid__461("users");
                ISafeIdentifier t___8486 = sid__461("orders");
                SqlBuilder t___8487 = new SqlBuilder();
                t___8487.AppendSafe("users.id = orders.user_id");
                SqlFragment t___8489 = t___8487.Accumulated;
                Query q__979 = S0::SrcGlobal.From(t___8485).FullJoin(t___8486, t___8489);
                bool t___8494 = q__979.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__8484()
                {
                    return "full join";
                }
                test___66.Assert(t___8494, (S1::Func<string>) fn__8484);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__1506()
        {
            T::Test test___67 = new T::Test();
            try
            {
                ISafeIdentifier t___8468 = sid__461("users");
                ISafeIdentifier t___8469 = sid__461("orders");
                SqlBuilder t___8470 = new SqlBuilder();
                t___8470.AppendSafe("users.id = orders.user_id");
                SqlFragment t___8472 = t___8470.Accumulated;
                Query t___8473 = S0::SrcGlobal.From(t___8468).InnerJoin(t___8469, t___8472);
                ISafeIdentifier t___8474 = sid__461("profiles");
                SqlBuilder t___8475 = new SqlBuilder();
                t___8475.AppendSafe("users.id = profiles.user_id");
                Query q__981 = t___8473.LeftJoin(t___8474, t___8475.Accumulated);
                bool t___8482 = q__981.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__8467()
                {
                    return "chained joins";
                }
                test___67.Assert(t___8482, (S1::Func<string>) fn__8467);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__1509()
        {
            T::Test test___68 = new T::Test();
            try
            {
                ISafeIdentifier t___8449 = sid__461("users");
                ISafeIdentifier t___8450 = sid__461("orders");
                SqlBuilder t___8451 = new SqlBuilder();
                t___8451.AppendSafe("users.id = orders.user_id");
                SqlFragment t___8453 = t___8451.Accumulated;
                Query t___8454 = S0::SrcGlobal.From(t___8449).InnerJoin(t___8450, t___8453);
                SqlBuilder t___8455 = new SqlBuilder();
                t___8455.AppendSafe("orders.total > ");
                t___8455.AppendInt32(100);
                Query t___4407;
                t___4407 = t___8454.Where(t___8455.Accumulated).OrderBy(sid__461("name"), true).Limit(10);
                Query q__983 = t___4407;
                bool t___8465 = q__983.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__8448()
                {
                    return "join with where/order/limit";
                }
                test___68.Assert(t___8465, (S1::Func<string>) fn__8448);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__1512()
        {
            T::Test test___69 = new T::Test();
            try
            {
                SqlFragment c__985 = S0::SrcGlobal.Col(sid__461("users"), sid__461("id"));
                bool t___8446 = c__985.ToString() == "users.id";
                string fn__8440()
                {
                    return "col helper";
                }
                test___69.Assert(t___8446, (S1::Func<string>) fn__8440);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__1513()
        {
            T::Test test___70 = new T::Test();
            try
            {
                SqlFragment onCond__987 = S0::SrcGlobal.Col(sid__461("users"), sid__461("id"));
                SqlBuilder b__988 = new SqlBuilder();
                b__988.AppendFragment(onCond__987);
                b__988.AppendSafe(" = ");
                b__988.AppendFragment(S0::SrcGlobal.Col(sid__461("orders"), sid__461("user_id")));
                ISafeIdentifier t___8431 = sid__461("users");
                ISafeIdentifier t___8432 = sid__461("orders");
                SqlFragment t___8433 = b__988.Accumulated;
                Query q__989 = S0::SrcGlobal.From(t___8431).InnerJoin(t___8432, t___8433);
                bool t___8438 = q__989.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__8420()
                {
                    return "join with col";
                }
                test___70.Assert(t___8438, (S1::Func<string>) fn__8420);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__1514()
        {
            T::Test test___71 = new T::Test();
            try
            {
                ISafeIdentifier t___8409 = sid__461("users");
                SqlBuilder t___8410 = new SqlBuilder();
                t___8410.AppendSafe("status = ");
                t___8410.AppendString("active");
                SqlFragment t___8413 = t___8410.Accumulated;
                Query q__991 = S0::SrcGlobal.From(t___8409).OrWhere(t___8413);
                bool t___8418 = q__991.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__8408()
                {
                    return "orWhere basic";
                }
                test___71.Assert(t___8418, (S1::Func<string>) fn__8408);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__1516()
        {
            T::Test test___72 = new T::Test();
            try
            {
                ISafeIdentifier t___8392 = sid__461("users");
                SqlBuilder t___8393 = new SqlBuilder();
                t___8393.AppendSafe("age > ");
                t___8393.AppendInt32(18);
                SqlFragment t___8396 = t___8393.Accumulated;
                Query t___8397 = S0::SrcGlobal.From(t___8392).Where(t___8396);
                SqlBuilder t___8398 = new SqlBuilder();
                t___8398.AppendSafe("vip = ");
                t___8398.AppendBoolean(true);
                Query q__993 = t___8397.OrWhere(t___8398.Accumulated);
                bool t___8406 = q__993.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__8391()
                {
                    return "where then orWhere";
                }
                test___72.Assert(t___8406, (S1::Func<string>) fn__8391);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__1519()
        {
            T::Test test___73 = new T::Test();
            try
            {
                ISafeIdentifier t___8370 = sid__461("users");
                SqlBuilder t___8371 = new SqlBuilder();
                t___8371.AppendSafe("active = ");
                t___8371.AppendBoolean(true);
                SqlFragment t___8374 = t___8371.Accumulated;
                Query t___8375 = S0::SrcGlobal.From(t___8370).Where(t___8374);
                SqlBuilder t___8376 = new SqlBuilder();
                t___8376.AppendSafe("role = ");
                t___8376.AppendString("admin");
                Query t___8380 = t___8375.OrWhere(t___8376.Accumulated);
                SqlBuilder t___8381 = new SqlBuilder();
                t___8381.AppendSafe("role = ");
                t___8381.AppendString("moderator");
                Query q__995 = t___8380.OrWhere(t___8381.Accumulated);
                bool t___8389 = q__995.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__8369()
                {
                    return "multiple orWhere";
                }
                test___73.Assert(t___8389, (S1::Func<string>) fn__8369);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__1523()
        {
            T::Test test___74 = new T::Test();
            try
            {
                ISafeIdentifier t___8348 = sid__461("users");
                SqlBuilder t___8349 = new SqlBuilder();
                t___8349.AppendSafe("age > ");
                t___8349.AppendInt32(18);
                SqlFragment t___8352 = t___8349.Accumulated;
                Query t___8353 = S0::SrcGlobal.From(t___8348).Where(t___8352);
                SqlBuilder t___8354 = new SqlBuilder();
                t___8354.AppendSafe("active = ");
                t___8354.AppendBoolean(true);
                Query t___8358 = t___8353.Where(t___8354.Accumulated);
                SqlBuilder t___8359 = new SqlBuilder();
                t___8359.AppendSafe("vip = ");
                t___8359.AppendBoolean(true);
                Query q__997 = t___8358.OrWhere(t___8359.Accumulated);
                bool t___8367 = q__997.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__8347()
                {
                    return "mixed where and orWhere";
                }
                test___74.Assert(t___8367, (S1::Func<string>) fn__8347);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__1527()
        {
            T::Test test___75 = new T::Test();
            try
            {
                ISafeIdentifier t___8339 = sid__461("users");
                ISafeIdentifier t___8340 = sid__461("deleted_at");
                Query q__999 = S0::SrcGlobal.From(t___8339).WhereNull(t___8340);
                bool t___8345 = q__999.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__8338()
                {
                    return "whereNull";
                }
                test___75.Assert(t___8345, (S1::Func<string>) fn__8338);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__1528()
        {
            T::Test test___76 = new T::Test();
            try
            {
                ISafeIdentifier t___8330 = sid__461("users");
                ISafeIdentifier t___8331 = sid__461("email");
                Query q__1001 = S0::SrcGlobal.From(t___8330).WhereNotNull(t___8331);
                bool t___8336 = q__1001.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__8329()
                {
                    return "whereNotNull";
                }
                test___76.Assert(t___8336, (S1::Func<string>) fn__8329);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__1529()
        {
            T::Test test___77 = new T::Test();
            try
            {
                ISafeIdentifier t___8316 = sid__461("users");
                SqlBuilder t___8317 = new SqlBuilder();
                t___8317.AppendSafe("active = ");
                t___8317.AppendBoolean(true);
                SqlFragment t___8320 = t___8317.Accumulated;
                Query q__1003 = S0::SrcGlobal.From(t___8316).Where(t___8320).WhereNull(sid__461("deleted_at"));
                bool t___8327 = q__1003.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__8315()
                {
                    return "whereNull chained";
                }
                test___77.Assert(t___8327, (S1::Func<string>) fn__8315);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__1531()
        {
            T::Test test___78 = new T::Test();
            try
            {
                ISafeIdentifier t___8302 = sid__461("users");
                ISafeIdentifier t___8303 = sid__461("deleted_at");
                Query t___8304 = S0::SrcGlobal.From(t___8302).WhereNull(t___8303);
                SqlBuilder t___8305 = new SqlBuilder();
                t___8305.AppendSafe("role = ");
                t___8305.AppendString("admin");
                Query q__1005 = t___8304.OrWhere(t___8305.Accumulated);
                bool t___8313 = q__1005.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__8301()
                {
                    return "whereNotNull with orWhere";
                }
                test___78.Assert(t___8313, (S1::Func<string>) fn__8301);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__1533()
        {
            T::Test test___79 = new T::Test();
            try
            {
                ISafeIdentifier t___8290 = sid__461("users");
                ISafeIdentifier t___8291 = sid__461("id");
                SqlInt32 t___8292 = new SqlInt32(1);
                SqlInt32 t___8293 = new SqlInt32(2);
                SqlInt32 t___8294 = new SqlInt32(3);
                Query q__1007 = S0::SrcGlobal.From(t___8290).WhereIn(t___8291, C::Listed.CreateReadOnlyList<SqlInt32>(t___8292, t___8293, t___8294));
                bool t___8299 = q__1007.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__8289()
                {
                    return "whereIn ints";
                }
                test___79.Assert(t___8299, (S1::Func<string>) fn__8289);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__1534()
        {
            T::Test test___80 = new T::Test();
            try
            {
                ISafeIdentifier t___8279 = sid__461("users");
                ISafeIdentifier t___8280 = sid__461("name");
                SqlString t___8281 = new SqlString("Alice");
                SqlString t___8282 = new SqlString("Bob's");
                Query q__1009 = S0::SrcGlobal.From(t___8279).WhereIn(t___8280, C::Listed.CreateReadOnlyList<SqlString>(t___8281, t___8282));
                bool t___8287 = q__1009.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__8278()
                {
                    return "whereIn strings";
                }
                test___80.Assert(t___8287, (S1::Func<string>) fn__8278);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__1535()
        {
            T::Test test___81 = new T::Test();
            try
            {
                ISafeIdentifier t___8270 = sid__461("users");
                ISafeIdentifier t___8271 = sid__461("id");
                Query q__1011 = S0::SrcGlobal.From(t___8270).WhereIn(t___8271, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___8276 = q__1011.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__8269()
                {
                    return "whereIn empty";
                }
                test___81.Assert(t___8276, (S1::Func<string>) fn__8269);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__1536()
        {
            T::Test test___82 = new T::Test();
            try
            {
                ISafeIdentifier t___8254 = sid__461("users");
                SqlBuilder t___8255 = new SqlBuilder();
                t___8255.AppendSafe("active = ");
                t___8255.AppendBoolean(true);
                SqlFragment t___8258 = t___8255.Accumulated;
                Query q__1013 = S0::SrcGlobal.From(t___8254).Where(t___8258).WhereIn(sid__461("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___8267 = q__1013.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__8253()
                {
                    return "whereIn chained";
                }
                test___82.Assert(t___8267, (S1::Func<string>) fn__8253);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__1538()
        {
            T::Test test___83 = new T::Test();
            try
            {
                ISafeIdentifier t___8244 = sid__461("users");
                ISafeIdentifier t___8245 = sid__461("id");
                SqlInt32 t___8246 = new SqlInt32(42);
                Query q__1015 = S0::SrcGlobal.From(t___8244).WhereIn(t___8245, C::Listed.CreateReadOnlyList<SqlInt32>(t___8246));
                bool t___8251 = q__1015.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__8243()
                {
                    return "whereIn single";
                }
                test___83.Assert(t___8251, (S1::Func<string>) fn__8243);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__1539()
        {
            T::Test test___84 = new T::Test();
            try
            {
                ISafeIdentifier t___8232 = sid__461("users");
                SqlBuilder t___8233 = new SqlBuilder();
                t___8233.AppendSafe("active = ");
                t___8233.AppendBoolean(true);
                SqlFragment t___8236 = t___8233.Accumulated;
                Query q__1017 = S0::SrcGlobal.From(t___8232).WhereNot(t___8236);
                bool t___8241 = q__1017.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__8231()
                {
                    return "whereNot";
                }
                test___84.Assert(t___8241, (S1::Func<string>) fn__8231);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__1541()
        {
            T::Test test___85 = new T::Test();
            try
            {
                ISafeIdentifier t___8215 = sid__461("users");
                SqlBuilder t___8216 = new SqlBuilder();
                t___8216.AppendSafe("age > ");
                t___8216.AppendInt32(18);
                SqlFragment t___8219 = t___8216.Accumulated;
                Query t___8220 = S0::SrcGlobal.From(t___8215).Where(t___8219);
                SqlBuilder t___8221 = new SqlBuilder();
                t___8221.AppendSafe("banned = ");
                t___8221.AppendBoolean(true);
                Query q__1019 = t___8220.WhereNot(t___8221.Accumulated);
                bool t___8229 = q__1019.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__8214()
                {
                    return "whereNot chained";
                }
                test___85.Assert(t___8229, (S1::Func<string>) fn__8214);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__1544()
        {
            T::Test test___86 = new T::Test();
            try
            {
                ISafeIdentifier t___8204 = sid__461("users");
                ISafeIdentifier t___8205 = sid__461("age");
                SqlInt32 t___8206 = new SqlInt32(18);
                SqlInt32 t___8207 = new SqlInt32(65);
                Query q__1021 = S0::SrcGlobal.From(t___8204).WhereBetween(t___8205, t___8206, t___8207);
                bool t___8212 = q__1021.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__8203()
                {
                    return "whereBetween ints";
                }
                test___86.Assert(t___8212, (S1::Func<string>) fn__8203);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__1545()
        {
            T::Test test___87 = new T::Test();
            try
            {
                ISafeIdentifier t___8188 = sid__461("users");
                SqlBuilder t___8189 = new SqlBuilder();
                t___8189.AppendSafe("active = ");
                t___8189.AppendBoolean(true);
                SqlFragment t___8192 = t___8189.Accumulated;
                Query q__1023 = S0::SrcGlobal.From(t___8188).Where(t___8192).WhereBetween(sid__461("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___8201 = q__1023.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__8187()
                {
                    return "whereBetween chained";
                }
                test___87.Assert(t___8201, (S1::Func<string>) fn__8187);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__1547()
        {
            T::Test test___88 = new T::Test();
            try
            {
                ISafeIdentifier t___8179 = sid__461("users");
                ISafeIdentifier t___8180 = sid__461("name");
                Query q__1025 = S0::SrcGlobal.From(t___8179).WhereLike(t___8180, "John%");
                bool t___8185 = q__1025.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__8178()
                {
                    return "whereLike";
                }
                test___88.Assert(t___8185, (S1::Func<string>) fn__8178);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__1548()
        {
            T::Test test___89 = new T::Test();
            try
            {
                ISafeIdentifier t___8170 = sid__461("users");
                ISafeIdentifier t___8171 = sid__461("email");
                Query q__1027 = S0::SrcGlobal.From(t___8170).WhereILike(t___8171, "%@gmail.com");
                bool t___8176 = q__1027.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__8169()
                {
                    return "whereILike";
                }
                test___89.Assert(t___8176, (S1::Func<string>) fn__8169);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__1549()
        {
            T::Test test___90 = new T::Test();
            try
            {
                ISafeIdentifier t___8156 = sid__461("users");
                ISafeIdentifier t___8157 = sid__461("name");
                Query q__1029 = S0::SrcGlobal.From(t___8156).WhereLike(t___8157, "'; DROP TABLE users; --");
                string s__1030 = q__1029.ToSql().ToString();
                bool t___8162 = s__1030.IndexOf("''") >= 0;
                string fn__8155()
                {
                    return "like injection escaped: " + s__1030;
                }
                test___90.Assert(t___8162, (S1::Func<string>) fn__8155);
                bool t___8166 = s__1030.IndexOf("LIKE") >= 0;
                string fn__8154()
                {
                    return "like structure intact: " + s__1030;
                }
                test___90.Assert(t___8166, (S1::Func<string>) fn__8154);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__1550()
        {
            T::Test test___91 = new T::Test();
            try
            {
                ISafeIdentifier t___8146 = sid__461("users");
                ISafeIdentifier t___8147 = sid__461("name");
                Query q__1032 = S0::SrcGlobal.From(t___8146).WhereLike(t___8147, "%son%");
                bool t___8152 = q__1032.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__8145()
                {
                    return "whereLike wildcard";
                }
                test___91.Assert(t___8152, (S1::Func<string>) fn__8145);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__1551()
        {
            T::Test test___92 = new T::Test();
            try
            {
                SqlFragment f__1034 = S0::SrcGlobal.CountAll();
                bool t___8143 = f__1034.ToString() == "COUNT(*)";
                string fn__8139()
                {
                    return "countAll";
                }
                test___92.Assert(t___8143, (S1::Func<string>) fn__8139);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__1552()
        {
            T::Test test___93 = new T::Test();
            try
            {
                SqlFragment f__1036 = S0::SrcGlobal.CountCol(sid__461("id"));
                bool t___8137 = f__1036.ToString() == "COUNT(id)";
                string fn__8132()
                {
                    return "countCol";
                }
                test___93.Assert(t___8137, (S1::Func<string>) fn__8132);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__1553()
        {
            T::Test test___94 = new T::Test();
            try
            {
                SqlFragment f__1038 = S0::SrcGlobal.SumCol(sid__461("amount"));
                bool t___8130 = f__1038.ToString() == "SUM(amount)";
                string fn__8125()
                {
                    return "sumCol";
                }
                test___94.Assert(t___8130, (S1::Func<string>) fn__8125);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__1554()
        {
            T::Test test___95 = new T::Test();
            try
            {
                SqlFragment f__1040 = S0::SrcGlobal.AvgCol(sid__461("price"));
                bool t___8123 = f__1040.ToString() == "AVG(price)";
                string fn__8118()
                {
                    return "avgCol";
                }
                test___95.Assert(t___8123, (S1::Func<string>) fn__8118);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__1555()
        {
            T::Test test___96 = new T::Test();
            try
            {
                SqlFragment f__1042 = S0::SrcGlobal.MinCol(sid__461("created_at"));
                bool t___8116 = f__1042.ToString() == "MIN(created_at)";
                string fn__8111()
                {
                    return "minCol";
                }
                test___96.Assert(t___8116, (S1::Func<string>) fn__8111);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__1556()
        {
            T::Test test___97 = new T::Test();
            try
            {
                SqlFragment f__1044 = S0::SrcGlobal.MaxCol(sid__461("score"));
                bool t___8109 = f__1044.ToString() == "MAX(score)";
                string fn__8104()
                {
                    return "maxCol";
                }
                test___97.Assert(t___8109, (S1::Func<string>) fn__8104);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__1557()
        {
            T::Test test___98 = new T::Test();
            try
            {
                ISafeIdentifier t___8096 = sid__461("orders");
                SqlFragment t___8097 = S0::SrcGlobal.CountAll();
                Query q__1046 = S0::SrcGlobal.From(t___8096).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___8097));
                bool t___8102 = q__1046.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__8095()
                {
                    return "selectExpr count";
                }
                test___98.Assert(t___8102, (S1::Func<string>) fn__8095);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__1558()
        {
            T::Test test___99 = new T::Test();
            try
            {
                SqlFragment nameFrag__1048 = S0::SrcGlobal.Col(sid__461("users"), sid__461("name"));
                ISafeIdentifier t___8087 = sid__461("users");
                SqlFragment t___8088 = S0::SrcGlobal.CountAll();
                Query q__1049 = S0::SrcGlobal.From(t___8087).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1048, t___8088));
                bool t___8093 = q__1049.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__8083()
                {
                    return "selectExpr multi";
                }
                test___99.Assert(t___8093, (S1::Func<string>) fn__8083);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__1559()
        {
            T::Test test___100 = new T::Test();
            try
            {
                ISafeIdentifier t___8072 = sid__461("users");
                ISafeIdentifier t___8073 = sid__461("id");
                ISafeIdentifier t___8074 = sid__461("name");
                Query q__1051 = S0::SrcGlobal.From(t___8072).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8073, t___8074)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___8081 = q__1051.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__8071()
                {
                    return "selectExpr overrides select";
                }
                test___100.Assert(t___8081, (S1::Func<string>) fn__8071);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__1560()
        {
            T::Test test___101 = new T::Test();
            try
            {
                ISafeIdentifier t___8058 = sid__461("orders");
                SqlFragment t___8061 = S0::SrcGlobal.Col(sid__461("orders"), sid__461("status"));
                SqlFragment t___8062 = S0::SrcGlobal.CountAll();
                Query q__1053 = S0::SrcGlobal.From(t___8058).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___8061, t___8062)).GroupBy(sid__461("status"));
                bool t___8069 = q__1053.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__8057()
                {
                    return "groupBy single";
                }
                test___101.Assert(t___8069, (S1::Func<string>) fn__8057);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__1561()
        {
            T::Test test___102 = new T::Test();
            try
            {
                ISafeIdentifier t___8047 = sid__461("orders");
                ISafeIdentifier t___8048 = sid__461("status");
                Query q__1055 = S0::SrcGlobal.From(t___8047).GroupBy(t___8048).GroupBy(sid__461("category"));
                bool t___8055 = q__1055.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__8046()
                {
                    return "groupBy multiple";
                }
                test___102.Assert(t___8055, (S1::Func<string>) fn__8046);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__1562()
        {
            T::Test test___103 = new T::Test();
            try
            {
                ISafeIdentifier t___8028 = sid__461("orders");
                SqlFragment t___8031 = S0::SrcGlobal.Col(sid__461("orders"), sid__461("status"));
                SqlFragment t___8032 = S0::SrcGlobal.CountAll();
                Query t___8035 = S0::SrcGlobal.From(t___8028).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___8031, t___8032)).GroupBy(sid__461("status"));
                SqlBuilder t___8036 = new SqlBuilder();
                t___8036.AppendSafe("COUNT(*) > ");
                t___8036.AppendInt32(5);
                Query q__1057 = t___8035.Having(t___8036.Accumulated);
                bool t___8044 = q__1057.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__8027()
                {
                    return "having basic";
                }
                test___103.Assert(t___8044, (S1::Func<string>) fn__8027);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__1564()
        {
            T::Test test___104 = new T::Test();
            try
            {
                ISafeIdentifier t___8009 = sid__461("orders");
                ISafeIdentifier t___8010 = sid__461("status");
                Query t___8011 = S0::SrcGlobal.From(t___8009).GroupBy(t___8010);
                SqlBuilder t___8012 = new SqlBuilder();
                t___8012.AppendSafe("COUNT(*) > ");
                t___8012.AppendInt32(5);
                Query t___8016 = t___8011.Having(t___8012.Accumulated);
                SqlBuilder t___8017 = new SqlBuilder();
                t___8017.AppendSafe("SUM(total) > ");
                t___8017.AppendInt32(1000);
                Query q__1059 = t___8016.OrHaving(t___8017.Accumulated);
                bool t___8025 = q__1059.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__8008()
                {
                    return "orHaving";
                }
                test___104.Assert(t___8025, (S1::Func<string>) fn__8008);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__1567()
        {
            T::Test test___105 = new T::Test();
            try
            {
                ISafeIdentifier t___7999 = sid__461("users");
                ISafeIdentifier t___8000 = sid__461("name");
                Query q__1061 = S0::SrcGlobal.From(t___7999).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8000)).Distinct();
                bool t___8006 = q__1061.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__7998()
                {
                    return "distinct";
                }
                test___105.Assert(t___8006, (S1::Func<string>) fn__7998);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__1568()
        {
            T::Test test___106 = new T::Test();
            try
            {
                ISafeIdentifier t___7984 = sid__461("users");
                ISafeIdentifier t___7985 = sid__461("email");
                Query t___7986 = S0::SrcGlobal.From(t___7984).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7985));
                SqlBuilder t___7987 = new SqlBuilder();
                t___7987.AppendSafe("active = ");
                t___7987.AppendBoolean(true);
                Query q__1063 = t___7986.Where(t___7987.Accumulated).Distinct();
                bool t___7996 = q__1063.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__7983()
                {
                    return "distinct with where";
                }
                test___106.Assert(t___7996, (S1::Func<string>) fn__7983);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__1570()
        {
            T::Test test___107 = new T::Test();
            try
            {
                Query q__1065 = S0::SrcGlobal.From(sid__461("users"));
                bool t___7981 = q__1065.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__7976()
                {
                    return "countSql bare";
                }
                test___107.Assert(t___7981, (S1::Func<string>) fn__7976);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__1571()
        {
            T::Test test___108 = new T::Test();
            try
            {
                ISafeIdentifier t___7965 = sid__461("users");
                SqlBuilder t___7966 = new SqlBuilder();
                t___7966.AppendSafe("active = ");
                t___7966.AppendBoolean(true);
                SqlFragment t___7969 = t___7966.Accumulated;
                Query q__1067 = S0::SrcGlobal.From(t___7965).Where(t___7969);
                bool t___7974 = q__1067.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__7964()
                {
                    return "countSql with where";
                }
                test___108.Assert(t___7974, (S1::Func<string>) fn__7964);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__1573()
        {
            T::Test test___109 = new T::Test();
            try
            {
                ISafeIdentifier t___7948 = sid__461("users");
                ISafeIdentifier t___7949 = sid__461("orders");
                SqlBuilder t___7950 = new SqlBuilder();
                t___7950.AppendSafe("users.id = orders.user_id");
                SqlFragment t___7952 = t___7950.Accumulated;
                Query t___7953 = S0::SrcGlobal.From(t___7948).InnerJoin(t___7949, t___7952);
                SqlBuilder t___7954 = new SqlBuilder();
                t___7954.AppendSafe("orders.total > ");
                t___7954.AppendInt32(100);
                Query q__1069 = t___7953.Where(t___7954.Accumulated);
                bool t___7962 = q__1069.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__7947()
                {
                    return "countSql with join";
                }
                test___109.Assert(t___7962, (S1::Func<string>) fn__7947);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__1576()
        {
            T::Test test___110 = new T::Test();
            try
            {
                ISafeIdentifier t___7934 = sid__461("users");
                SqlBuilder t___7935 = new SqlBuilder();
                t___7935.AppendSafe("active = ");
                t___7935.AppendBoolean(true);
                SqlFragment t___7938 = t___7935.Accumulated;
                Query t___3983;
                t___3983 = S0::SrcGlobal.From(t___7934).Where(t___7938).OrderBy(sid__461("name"), true).Limit(10);
                Query t___3984;
                t___3984 = t___3983.Offset(20);
                Query q__1071 = t___3984;
                string s__1072 = q__1071.CountSql().ToString();
                bool t___7945 = s__1072 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__7933()
                {
                    return "countSql drops extras: " + s__1072;
                }
                test___110.Assert(t___7945, (S1::Func<string>) fn__7933);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__1578()
        {
            T::Test test___111 = new T::Test();
            try
            {
                ISafeIdentifier t___7901 = sid__461("orders");
                SqlFragment t___7904 = S0::SrcGlobal.Col(sid__461("orders"), sid__461("status"));
                SqlFragment t___7905 = S0::SrcGlobal.CountAll();
                SqlFragment t___7907 = S0::SrcGlobal.SumCol(sid__461("total"));
                Query t___7908 = S0::SrcGlobal.From(t___7901).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___7904, t___7905, t___7907));
                ISafeIdentifier t___7909 = sid__461("users");
                SqlBuilder t___7910 = new SqlBuilder();
                t___7910.AppendSafe("orders.user_id = users.id");
                Query t___7913 = t___7908.InnerJoin(t___7909, t___7910.Accumulated);
                SqlBuilder t___7914 = new SqlBuilder();
                t___7914.AppendSafe("users.active = ");
                t___7914.AppendBoolean(true);
                Query t___7920 = t___7913.Where(t___7914.Accumulated).GroupBy(sid__461("status"));
                SqlBuilder t___7921 = new SqlBuilder();
                t___7921.AppendSafe("COUNT(*) > ");
                t___7921.AppendInt32(3);
                Query q__1074 = t___7920.Having(t___7921.Accumulated).OrderBy(sid__461("status"), true);
                string expected__1075 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___7931 = q__1074.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__7900()
                {
                    return "full aggregation";
                }
                test___111.Assert(t___7931, (S1::Func<string>) fn__7900);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionSql__1582()
        {
            T::Test test___112 = new T::Test();
            try
            {
                ISafeIdentifier t___7883 = sid__461("users");
                SqlBuilder t___7884 = new SqlBuilder();
                t___7884.AppendSafe("role = ");
                t___7884.AppendString("admin");
                SqlFragment t___7887 = t___7884.Accumulated;
                Query a__1077 = S0::SrcGlobal.From(t___7883).Where(t___7887);
                ISafeIdentifier t___7889 = sid__461("users");
                SqlBuilder t___7890 = new SqlBuilder();
                t___7890.AppendSafe("role = ");
                t___7890.AppendString("moderator");
                SqlFragment t___7893 = t___7890.Accumulated;
                Query b__1078 = S0::SrcGlobal.From(t___7889).Where(t___7893);
                string s__1079 = S0::SrcGlobal.UnionSql(a__1077, b__1078).ToString();
                bool t___7898 = s__1079 == "(SELECT * FROM users WHERE role = 'admin') UNION (SELECT * FROM users WHERE role = 'moderator')";
                string fn__7882()
                {
                    return "unionSql: " + s__1079;
                }
                test___112.Assert(t___7898, (S1::Func<string>) fn__7882);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionAllSql__1585()
        {
            T::Test test___113 = new T::Test();
            try
            {
                ISafeIdentifier t___7871 = sid__461("users");
                ISafeIdentifier t___7872 = sid__461("name");
                Query a__1081 = S0::SrcGlobal.From(t___7871).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7872));
                ISafeIdentifier t___7874 = sid__461("contacts");
                ISafeIdentifier t___7875 = sid__461("name");
                Query b__1082 = S0::SrcGlobal.From(t___7874).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7875));
                string s__1083 = S0::SrcGlobal.UnionAllSql(a__1081, b__1082).ToString();
                bool t___7880 = s__1083 == "(SELECT name FROM users) UNION ALL (SELECT name FROM contacts)";
                string fn__7870()
                {
                    return "unionAllSql: " + s__1083;
                }
                test___113.Assert(t___7880, (S1::Func<string>) fn__7870);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void intersectSql__1586()
        {
            T::Test test___114 = new T::Test();
            try
            {
                ISafeIdentifier t___7859 = sid__461("users");
                ISafeIdentifier t___7860 = sid__461("email");
                Query a__1085 = S0::SrcGlobal.From(t___7859).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7860));
                ISafeIdentifier t___7862 = sid__461("subscribers");
                ISafeIdentifier t___7863 = sid__461("email");
                Query b__1086 = S0::SrcGlobal.From(t___7862).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7863));
                string s__1087 = S0::SrcGlobal.IntersectSql(a__1085, b__1086).ToString();
                bool t___7868 = s__1087 == "(SELECT email FROM users) INTERSECT (SELECT email FROM subscribers)";
                string fn__7858()
                {
                    return "intersectSql: " + s__1087;
                }
                test___114.Assert(t___7868, (S1::Func<string>) fn__7858);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void exceptSql__1587()
        {
            T::Test test___115 = new T::Test();
            try
            {
                ISafeIdentifier t___7847 = sid__461("users");
                ISafeIdentifier t___7848 = sid__461("id");
                Query a__1089 = S0::SrcGlobal.From(t___7847).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7848));
                ISafeIdentifier t___7850 = sid__461("banned");
                ISafeIdentifier t___7851 = sid__461("id");
                Query b__1090 = S0::SrcGlobal.From(t___7850).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7851));
                string s__1091 = S0::SrcGlobal.ExceptSql(a__1089, b__1090).ToString();
                bool t___7856 = s__1091 == "(SELECT id FROM users) EXCEPT (SELECT id FROM banned)";
                string fn__7846()
                {
                    return "exceptSql: " + s__1091;
                }
                test___115.Assert(t___7856, (S1::Func<string>) fn__7846);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void subqueryWithAlias__1588()
        {
            T::Test test___116 = new T::Test();
            try
            {
                ISafeIdentifier t___7832 = sid__461("orders");
                ISafeIdentifier t___7833 = sid__461("user_id");
                Query t___7834 = S0::SrcGlobal.From(t___7832).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7833));
                SqlBuilder t___7835 = new SqlBuilder();
                t___7835.AppendSafe("total > ");
                t___7835.AppendInt32(100);
                Query inner__1093 = t___7834.Where(t___7835.Accumulated);
                string s__1094 = S0::SrcGlobal.Subquery(inner__1093, sid__461("big_orders")).ToString();
                bool t___7844 = s__1094 == "(SELECT user_id FROM orders WHERE total > 100) AS big_orders";
                string fn__7831()
                {
                    return "subquery: " + s__1094;
                }
                test___116.Assert(t___7844, (S1::Func<string>) fn__7831);
            }
            finally
            {
                test___116.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSql__1590()
        {
            T::Test test___117 = new T::Test();
            try
            {
                ISafeIdentifier t___7821 = sid__461("orders");
                SqlBuilder t___7822 = new SqlBuilder();
                t___7822.AppendSafe("orders.user_id = users.id");
                SqlFragment t___7824 = t___7822.Accumulated;
                Query inner__1096 = S0::SrcGlobal.From(t___7821).Where(t___7824);
                string s__1097 = S0::SrcGlobal.ExistsSql(inner__1096).ToString();
                bool t___7829 = s__1097 == "EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__7820()
                {
                    return "existsSql: " + s__1097;
                }
                test___117.Assert(t___7829, (S1::Func<string>) fn__7820);
            }
            finally
            {
                test___117.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubquery__1592()
        {
            T::Test test___118 = new T::Test();
            try
            {
                ISafeIdentifier t___7804 = sid__461("orders");
                ISafeIdentifier t___7805 = sid__461("user_id");
                Query t___7806 = S0::SrcGlobal.From(t___7804).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7805));
                SqlBuilder t___7807 = new SqlBuilder();
                t___7807.AppendSafe("total > ");
                t___7807.AppendInt32(1000);
                Query sub__1099 = t___7806.Where(t___7807.Accumulated);
                ISafeIdentifier t___7812 = sid__461("users");
                ISafeIdentifier t___7813 = sid__461("id");
                Query q__1100 = S0::SrcGlobal.From(t___7812).WhereInSubquery(t___7813, sub__1099);
                string s__1101 = q__1100.ToSql().ToString();
                bool t___7818 = s__1101 == "SELECT * FROM users WHERE id IN (SELECT user_id FROM orders WHERE total > 1000)";
                string fn__7803()
                {
                    return "whereInSubquery: " + s__1101;
                }
                test___118.Assert(t___7818, (S1::Func<string>) fn__7803);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void setOperationWithWhereOnEachSide__1594()
        {
            T::Test test___119 = new T::Test();
            try
            {
                ISafeIdentifier t___7781 = sid__461("users");
                SqlBuilder t___7782 = new SqlBuilder();
                t___7782.AppendSafe("age > ");
                t___7782.AppendInt32(18);
                SqlFragment t___7785 = t___7782.Accumulated;
                Query t___7786 = S0::SrcGlobal.From(t___7781).Where(t___7785);
                SqlBuilder t___7787 = new SqlBuilder();
                t___7787.AppendSafe("active = ");
                t___7787.AppendBoolean(true);
                Query a__1103 = t___7786.Where(t___7787.Accumulated);
                ISafeIdentifier t___7792 = sid__461("users");
                SqlBuilder t___7793 = new SqlBuilder();
                t___7793.AppendSafe("role = ");
                t___7793.AppendString("vip");
                SqlFragment t___7796 = t___7793.Accumulated;
                Query b__1104 = S0::SrcGlobal.From(t___7792).Where(t___7796);
                string s__1105 = S0::SrcGlobal.UnionSql(a__1103, b__1104).ToString();
                bool t___7801 = s__1105 == "(SELECT * FROM users WHERE age > 18 AND active = TRUE) UNION (SELECT * FROM users WHERE role = 'vip')";
                string fn__7780()
                {
                    return "union with where: " + s__1105;
                }
                test___119.Assert(t___7801, (S1::Func<string>) fn__7780);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubqueryChainedWithWhere__1598()
        {
            T::Test test___120 = new T::Test();
            try
            {
                ISafeIdentifier t___7764 = sid__461("orders");
                ISafeIdentifier t___7765 = sid__461("user_id");
                Query sub__1107 = S0::SrcGlobal.From(t___7764).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___7765));
                ISafeIdentifier t___7767 = sid__461("users");
                SqlBuilder t___7768 = new SqlBuilder();
                t___7768.AppendSafe("active = ");
                t___7768.AppendBoolean(true);
                SqlFragment t___7771 = t___7768.Accumulated;
                Query q__1108 = S0::SrcGlobal.From(t___7767).Where(t___7771).WhereInSubquery(sid__461("id"), sub__1107);
                string s__1109 = q__1108.ToSql().ToString();
                bool t___7778 = s__1109 == "SELECT * FROM users WHERE active = TRUE AND id IN (SELECT user_id FROM orders)";
                string fn__7763()
                {
                    return "whereInSubquery chained: " + s__1109;
                }
                test___120.Assert(t___7778, (S1::Func<string>) fn__7763);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSqlUsedInWhere__1600()
        {
            T::Test test___121 = new T::Test();
            try
            {
                ISafeIdentifier t___7750 = sid__461("orders");
                SqlBuilder t___7751 = new SqlBuilder();
                t___7751.AppendSafe("orders.user_id = users.id");
                SqlFragment t___7753 = t___7751.Accumulated;
                Query sub__1111 = S0::SrcGlobal.From(t___7750).Where(t___7753);
                ISafeIdentifier t___7755 = sid__461("users");
                SqlFragment t___7756 = S0::SrcGlobal.ExistsSql(sub__1111);
                Query q__1112 = S0::SrcGlobal.From(t___7755).Where(t___7756);
                string s__1113 = q__1112.ToSql().ToString();
                bool t___7761 = s__1113 == "SELECT * FROM users WHERE EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__7749()
                {
                    return "exists in where: " + s__1113;
                }
                test___121.Assert(t___7761, (S1::Func<string>) fn__7749);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__1602()
        {
            T::Test test___128 = new T::Test();
            try
            {
                ISafeIdentifier t___3806;
                t___3806 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1151 = t___3806;
                bool t___7747 = id__1151.SqlValue == "user_name";
                string fn__7744()
                {
                    return "value should round-trip";
                }
                test___128.Assert(t___7747, (S1::Func<string>) fn__7744);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__1603()
        {
            T::Test test___129 = new T::Test();
            try
            {
                bool didBubble__1153;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1153 = false;
                }
                catch
                {
                    didBubble__1153 = true;
                }
                string fn__7741()
                {
                    return "empty string should bubble";
                }
                test___129.Assert(didBubble__1153, (S1::Func<string>) fn__7741);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__1604()
        {
            T::Test test___130 = new T::Test();
            try
            {
                bool didBubble__1155;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1155 = false;
                }
                catch
                {
                    didBubble__1155 = true;
                }
                string fn__7738()
                {
                    return "leading digit should bubble";
                }
                test___130.Assert(didBubble__1155, (S1::Func<string>) fn__7738);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__1605()
        {
            T::Test test___131 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1157 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__7735(string c__1158)
                {
                    bool didBubble__1159;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1158);
                        didBubble__1159 = false;
                    }
                    catch
                    {
                        didBubble__1159 = true;
                    }
                    string fn__7732()
                    {
                        return "should reject: " + c__1158;
                    }
                    test___131.Assert(didBubble__1159, (S1::Func<string>) fn__7732);
                }
                C::Listed.ForEach(cases__1157, (S1::Action<string>) fn__7735);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__1606()
        {
            T::Test test___132 = new T::Test();
            try
            {
                ISafeIdentifier t___3783;
                t___3783 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___3784 = t___3783;
                ISafeIdentifier t___3785;
                t___3785 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___3786 = t___3785;
                StringField t___7722 = new StringField();
                FieldDef t___7723 = new FieldDef(t___3786, t___7722, false);
                ISafeIdentifier t___3789;
                t___3789 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___3790 = t___3789;
                IntField t___7724 = new IntField();
                FieldDef t___7725 = new FieldDef(t___3790, t___7724, false);
                TableDef td__1161 = new TableDef(t___3784, C::Listed.CreateReadOnlyList<FieldDef>(t___7723, t___7725));
                FieldDef t___3794;
                t___3794 = td__1161.Field("age");
                FieldDef f__1162 = t___3794;
                bool t___7730 = f__1162.Name.SqlValue == "age";
                string fn__7721()
                {
                    return "should find age field";
                }
                test___132.Assert(t___7730, (S1::Func<string>) fn__7721);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__1607()
        {
            T::Test test___133 = new T::Test();
            try
            {
                ISafeIdentifier t___3774;
                t___3774 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___3775 = t___3774;
                ISafeIdentifier t___3776;
                t___3776 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___3777 = t___3776;
                StringField t___7716 = new StringField();
                FieldDef t___7717 = new FieldDef(t___3777, t___7716, false);
                TableDef td__1164 = new TableDef(t___3775, C::Listed.CreateReadOnlyList<FieldDef>(t___7717));
                bool didBubble__1165;
                try
                {
                    td__1164.Field("nonexistent");
                    didBubble__1165 = false;
                }
                catch
                {
                    didBubble__1165 = true;
                }
                string fn__7715()
                {
                    return "unknown field should bubble";
                }
                test___133.Assert(didBubble__1165, (S1::Func<string>) fn__7715);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__1608()
        {
            T::Test test___134 = new T::Test();
            try
            {
                ISafeIdentifier t___3762;
                t___3762 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___3763 = t___3762;
                StringField t___7704 = new StringField();
                FieldDef required__1167 = new FieldDef(t___3763, t___7704, false);
                ISafeIdentifier t___3766;
                t___3766 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___3767 = t___3766;
                StringField t___7706 = new StringField();
                FieldDef optional__1168 = new FieldDef(t___3767, t___7706, true);
                bool t___7710 = !required__1167.Nullable;
                string fn__7703()
                {
                    return "required field should not be nullable";
                }
                test___134.Assert(t___7710, (S1::Func<string>) fn__7703);
                bool t___7712 = optional__1168.Nullable;
                string fn__7702()
                {
                    return "optional field should be nullable";
                }
                test___134.Assert(t___7712, (S1::Func<string>) fn__7702);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__1609()
        {
            T::Test test___138 = new T::Test();
            try
            {
                string build__1294(string name__1296)
                {
                    SqlBuilder t___7684 = new SqlBuilder();
                    t___7684.AppendSafe("select * from hi where name = ");
                    t___7684.AppendString(name__1296);
                    return t___7684.Accumulated.ToString();
                }
                string buildWrong__1295(string name__1298)
                {
                    return "select * from hi where name = '" + name__1298 + "'";
                }
                string actual___1611 = build__1294("world");
                bool t___7694 = actual___1611 == "select * from hi where name = 'world'";
                string fn__7691()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___1611 + ")";
                }
                test___138.Assert(t___7694, (S1::Func<string>) fn__7691);
                string bobbyTables__1300 = "Robert'); drop table hi;--";
                string actual___1613 = build__1294("Robert'); drop table hi;--");
                bool t___7698 = actual___1613 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__7690()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___1613 + ")";
                }
                test___138.Assert(t___7698, (S1::Func<string>) fn__7690);
                string fn__7689()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___138.Assert(true, (S1::Func<string>) fn__7689);
            }
            finally
            {
                test___138.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__1617()
        {
            T::Test test___139 = new T::Test();
            try
            {
                SqlBuilder t___7652 = new SqlBuilder();
                t___7652.AppendSafe("v = ");
                t___7652.AppendString("");
                string actual___1618 = t___7652.Accumulated.ToString();
                bool t___7658 = actual___1618 == "v = ''";
                string fn__7651()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___1618 + ")";
                }
                test___139.Assert(t___7658, (S1::Func<string>) fn__7651);
                SqlBuilder t___7660 = new SqlBuilder();
                t___7660.AppendSafe("v = ");
                t___7660.AppendString("a''b");
                string actual___1621 = t___7660.Accumulated.ToString();
                bool t___7666 = actual___1621 == "v = 'a''''b'";
                string fn__7650()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___1621 + ")";
                }
                test___139.Assert(t___7666, (S1::Func<string>) fn__7650);
                SqlBuilder t___7668 = new SqlBuilder();
                t___7668.AppendSafe("v = ");
                t___7668.AppendString("Hello 世界");
                string actual___1624 = t___7668.Accumulated.ToString();
                bool t___7674 = actual___1624 == "v = 'Hello 世界'";
                string fn__7649()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1624 + ")";
                }
                test___139.Assert(t___7674, (S1::Func<string>) fn__7649);
                SqlBuilder t___7676 = new SqlBuilder();
                t___7676.AppendSafe("v = ");
                t___7676.AppendString("Line1\nLine2");
                string actual___1627 = t___7676.Accumulated.ToString();
                bool t___7682 = actual___1627 == "v = 'Line1\nLine2'";
                string fn__7648()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1627 + ")";
                }
                test___139.Assert(t___7682, (S1::Func<string>) fn__7648);
            }
            finally
            {
                test___139.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1630()
        {
            T::Test test___140 = new T::Test();
            try
            {
                SqlBuilder t___7623 = new SqlBuilder();
                t___7623.AppendSafe("select ");
                t___7623.AppendInt32(42);
                t___7623.AppendSafe(", ");
                t___7623.AppendInt64(43);
                t___7623.AppendSafe(", ");
                t___7623.AppendFloat64(19.99);
                t___7623.AppendSafe(", ");
                t___7623.AppendBoolean(true);
                t___7623.AppendSafe(", ");
                t___7623.AppendBoolean(false);
                string actual___1631 = t___7623.Accumulated.ToString();
                bool t___7637 = actual___1631 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__7622()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1631 + ")";
                }
                test___140.Assert(t___7637, (S1::Func<string>) fn__7622);
                S1::DateTime t___3707;
                t___3707 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1303 = t___3707;
                SqlBuilder t___7639 = new SqlBuilder();
                t___7639.AppendSafe("insert into t values (");
                t___7639.AppendDate(date__1303);
                t___7639.AppendSafe(")");
                string actual___1634 = t___7639.Accumulated.ToString();
                bool t___7646 = actual___1634 == "insert into t values ('2024-12-25')";
                string fn__7621()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1634 + ")";
                }
                test___140.Assert(t___7646, (S1::Func<string>) fn__7621);
            }
            finally
            {
                test___140.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1637()
        {
            T::Test test___141 = new T::Test();
            try
            {
                SqlBuilder t___7567 = new SqlBuilder();
                t___7567.AppendSafe("v IN (");
                t___7567.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___7567.AppendSafe(")");
                string actual___1638 = t___7567.Accumulated.ToString();
                bool t___7574 = actual___1638 == "v IN ('a', 'b', 'c''d')";
                string fn__7566()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1638 + ")";
                }
                test___141.Assert(t___7574, (S1::Func<string>) fn__7566);
                SqlBuilder t___7576 = new SqlBuilder();
                t___7576.AppendSafe("v IN (");
                t___7576.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___7576.AppendSafe(")");
                string actual___1641 = t___7576.Accumulated.ToString();
                bool t___7583 = actual___1641 == "v IN (1, 2, 3)";
                string fn__7565()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1641 + ")";
                }
                test___141.Assert(t___7583, (S1::Func<string>) fn__7565);
                SqlBuilder t___7585 = new SqlBuilder();
                t___7585.AppendSafe("v IN (");
                t___7585.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___7585.AppendSafe(")");
                string actual___1644 = t___7585.Accumulated.ToString();
                bool t___7592 = actual___1644 == "v IN (1, 2)";
                string fn__7564()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1644 + ")";
                }
                test___141.Assert(t___7592, (S1::Func<string>) fn__7564);
                SqlBuilder t___7594 = new SqlBuilder();
                t___7594.AppendSafe("v IN (");
                t___7594.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___7594.AppendSafe(")");
                string actual___1647 = t___7594.Accumulated.ToString();
                bool t___7601 = actual___1647 == "v IN (1.0, 2.0)";
                string fn__7563()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1647 + ")";
                }
                test___141.Assert(t___7601, (S1::Func<string>) fn__7563);
                SqlBuilder t___7603 = new SqlBuilder();
                t___7603.AppendSafe("v IN (");
                t___7603.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___7603.AppendSafe(")");
                string actual___1650 = t___7603.Accumulated.ToString();
                bool t___7610 = actual___1650 == "v IN (TRUE, FALSE)";
                string fn__7562()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1650 + ")";
                }
                test___141.Assert(t___7610, (S1::Func<string>) fn__7562);
                S1::DateTime t___3679;
                t___3679 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___3680 = t___3679;
                S1::DateTime t___3681;
                t___3681 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___3682 = t___3681;
                G::IReadOnlyList<S1::DateTime> dates__1305 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___3680, t___3682);
                SqlBuilder t___7612 = new SqlBuilder();
                t___7612.AppendSafe("v IN (");
                t___7612.AppendDateList(dates__1305);
                t___7612.AppendSafe(")");
                string actual___1653 = t___7612.Accumulated.ToString();
                bool t___7619 = actual___1653 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__7561()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1653 + ")";
                }
                test___141.Assert(t___7619, (S1::Func<string>) fn__7561);
            }
            finally
            {
                test___141.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1656()
        {
            T::Test test___142 = new T::Test();
            try
            {
                double nan__1307;
                nan__1307 = 0.0 / 0.0;
                SqlBuilder t___7553 = new SqlBuilder();
                t___7553.AppendSafe("v = ");
                t___7553.AppendFloat64(nan__1307);
                string actual___1657 = t___7553.Accumulated.ToString();
                bool t___7559 = actual___1657 == "v = NULL";
                string fn__7552()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1657 + ")";
                }
                test___142.Assert(t___7559, (S1::Func<string>) fn__7552);
            }
            finally
            {
                test___142.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1660()
        {
            T::Test test___143 = new T::Test();
            try
            {
                double inf__1309;
                inf__1309 = 1.0 / 0.0;
                SqlBuilder t___7544 = new SqlBuilder();
                t___7544.AppendSafe("v = ");
                t___7544.AppendFloat64(inf__1309);
                string actual___1661 = t___7544.Accumulated.ToString();
                bool t___7550 = actual___1661 == "v = NULL";
                string fn__7543()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1661 + ")";
                }
                test___143.Assert(t___7550, (S1::Func<string>) fn__7543);
            }
            finally
            {
                test___143.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1664()
        {
            T::Test test___144 = new T::Test();
            try
            {
                double ninf__1311;
                ninf__1311 = -1.0 / 0.0;
                SqlBuilder t___7535 = new SqlBuilder();
                t___7535.AppendSafe("v = ");
                t___7535.AppendFloat64(ninf__1311);
                string actual___1665 = t___7535.Accumulated.ToString();
                bool t___7541 = actual___1665 == "v = NULL";
                string fn__7534()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1665 + ")";
                }
                test___144.Assert(t___7541, (S1::Func<string>) fn__7534);
            }
            finally
            {
                test___144.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1668()
        {
            T::Test test___145 = new T::Test();
            try
            {
                SqlBuilder t___7510 = new SqlBuilder();
                t___7510.AppendSafe("v = ");
                t___7510.AppendFloat64(3.14);
                string actual___1669 = t___7510.Accumulated.ToString();
                bool t___7516 = actual___1669 == "v = 3.14";
                string fn__7509()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1669 + ")";
                }
                test___145.Assert(t___7516, (S1::Func<string>) fn__7509);
                SqlBuilder t___7518 = new SqlBuilder();
                t___7518.AppendSafe("v = ");
                t___7518.AppendFloat64(0.0);
                string actual___1672 = t___7518.Accumulated.ToString();
                bool t___7524 = actual___1672 == "v = 0.0";
                string fn__7508()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1672 + ")";
                }
                test___145.Assert(t___7524, (S1::Func<string>) fn__7508);
                SqlBuilder t___7526 = new SqlBuilder();
                t___7526.AppendSafe("v = ");
                t___7526.AppendFloat64(-42.5);
                string actual___1675 = t___7526.Accumulated.ToString();
                bool t___7532 = actual___1675 == "v = -42.5";
                string fn__7507()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1675 + ")";
                }
                test___145.Assert(t___7532, (S1::Func<string>) fn__7507);
            }
            finally
            {
                test___145.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1678()
        {
            T::Test test___146 = new T::Test();
            try
            {
                S1::DateTime t___3575;
                t___3575 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1314 = t___3575;
                SqlBuilder t___7499 = new SqlBuilder();
                t___7499.AppendSafe("v = ");
                t___7499.AppendDate(d__1314);
                string actual___1679 = t___7499.Accumulated.ToString();
                bool t___7505 = actual___1679 == "v = '2024-06-15'";
                string fn__7498()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1679 + ")";
                }
                test___146.Assert(t___7505, (S1::Func<string>) fn__7498);
            }
            finally
            {
                test___146.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1682()
        {
            T::Test test___147 = new T::Test();
            try
            {
                string name__1316 = "Someone";
                SqlBuilder t___7467 = new SqlBuilder();
                t___7467.AppendSafe("where p.last_name = ");
                t___7467.AppendString("Someone");
                SqlFragment condition__1317 = t___7467.Accumulated;
                SqlBuilder t___7471 = new SqlBuilder();
                t___7471.AppendSafe("select p.id from person p ");
                t___7471.AppendFragment(condition__1317);
                string actual___1684 = t___7471.Accumulated.ToString();
                bool t___7477 = actual___1684 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__7466()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1684 + ")";
                }
                test___147.Assert(t___7477, (S1::Func<string>) fn__7466);
                SqlBuilder t___7479 = new SqlBuilder();
                t___7479.AppendSafe("select p.id from person p ");
                t___7479.AppendPart(condition__1317.ToSource());
                string actual___1687 = t___7479.Accumulated.ToString();
                bool t___7486 = actual___1687 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__7465()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1687 + ")";
                }
                test___147.Assert(t___7486, (S1::Func<string>) fn__7465);
                G::IReadOnlyList<ISqlPart> parts__1318 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___7490 = new SqlBuilder();
                t___7490.AppendSafe("select ");
                t___7490.AppendPartList(parts__1318);
                string actual___1690 = t___7490.Accumulated.ToString();
                bool t___7496 = actual___1690 == "select 'a''b', 3";
                string fn__7464()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___1690 + ")";
                }
                test___147.Assert(t___7496, (S1::Func<string>) fn__7464);
            }
            finally
            {
                test___147.SoftFailToHard();
            }
        }
    }
}
