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
        internal static ISafeIdentifier csid__503(string name__648)
        {
            ISafeIdentifier t___6032;
            t___6032 = S0::SrcGlobal.SafeIdentifier(name__648);
            return t___6032;
        }
        internal static TableDef userTable__504()
        {
            return new TableDef(csid__503("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__503("name"), new StringField(), false), new FieldDef(csid__503("email"), new StringField(), false), new FieldDef(csid__503("age"), new IntField(), true), new FieldDef(csid__503("score"), new FloatField(), true), new FieldDef(csid__503("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__1546()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__652 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___10651 = userTable__504();
                ISafeIdentifier t___10652 = csid__503("name");
                ISafeIdentifier t___10653 = csid__503("email");
                IChangeset cs__653 = S0::SrcGlobal.Changeset(t___10651, params__652).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10652, t___10653));
                bool t___10656 = C::Mapped.ContainsKey(cs__653.Changes, "name");
                string fn__10646()
                {
                    return "name should be in changes";
                }
                test___24.Assert(t___10656, (S1::Func<string>) fn__10646);
                bool t___10660 = C::Mapped.ContainsKey(cs__653.Changes, "email");
                string fn__10645()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___10660, (S1::Func<string>) fn__10645);
                bool t___10666 = !C::Mapped.ContainsKey(cs__653.Changes, "admin");
                string fn__10644()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___24.Assert(t___10666, (S1::Func<string>) fn__10644);
                bool t___10668 = cs__653.IsValid;
                string fn__10643()
                {
                    return "should still be valid";
                }
                test___24.Assert(t___10668, (S1::Func<string>) fn__10643);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__1547()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__655 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___10629 = userTable__504();
                ISafeIdentifier t___10630 = csid__503("name");
                IChangeset cs__656 = S0::SrcGlobal.Changeset(t___10629, params__655).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10630)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("email")));
                bool t___10637 = !C::Mapped.ContainsKey(cs__656.Changes, "name");
                string fn__10625()
                {
                    return "name must be excluded by second cast";
                }
                test___25.Assert(t___10637, (S1::Func<string>) fn__10625);
                bool t___10640 = C::Mapped.ContainsKey(cs__656.Changes, "email");
                string fn__10624()
                {
                    return "email should be present";
                }
                test___25.Assert(t___10640, (S1::Func<string>) fn__10624);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__1548()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__658 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___10611 = userTable__504();
                ISafeIdentifier t___10612 = csid__503("name");
                ISafeIdentifier t___10613 = csid__503("email");
                IChangeset cs__659 = S0::SrcGlobal.Changeset(t___10611, params__658).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10612, t___10613));
                bool t___10618 = !C::Mapped.ContainsKey(cs__659.Changes, "name");
                string fn__10607()
                {
                    return "empty name should not be in changes";
                }
                test___26.Assert(t___10618, (S1::Func<string>) fn__10607);
                bool t___10621 = C::Mapped.ContainsKey(cs__659.Changes, "email");
                string fn__10606()
                {
                    return "email should be in changes";
                }
                test___26.Assert(t___10621, (S1::Func<string>) fn__10606);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__1549()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__661 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___10593 = userTable__504();
                ISafeIdentifier t___10594 = csid__503("name");
                IChangeset cs__662 = S0::SrcGlobal.Changeset(t___10593, params__661).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10594)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name")));
                bool t___10598 = cs__662.IsValid;
                string fn__10590()
                {
                    return "should be valid";
                }
                test___27.Assert(t___10598, (S1::Func<string>) fn__10590);
                bool t___10604 = cs__662.Errors.Count == 0;
                string fn__10589()
                {
                    return "no errors expected";
                }
                test___27.Assert(t___10604, (S1::Func<string>) fn__10589);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__1550()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__664 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___10569 = userTable__504();
                ISafeIdentifier t___10570 = csid__503("name");
                IChangeset cs__665 = S0::SrcGlobal.Changeset(t___10569, params__664).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10570)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name")));
                bool t___10576 = !cs__665.IsValid;
                string fn__10567()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___10576, (S1::Func<string>) fn__10567);
                bool t___10581 = cs__665.Errors.Count == 1;
                string fn__10566()
                {
                    return "should have one error";
                }
                test___28.Assert(t___10581, (S1::Func<string>) fn__10566);
                bool t___10587 = cs__665.Errors[0].Field == "name";
                string fn__10565()
                {
                    return "error should name the field";
                }
                test___28.Assert(t___10587, (S1::Func<string>) fn__10565);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__1551()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__667 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___10557 = userTable__504();
                ISafeIdentifier t___10558 = csid__503("name");
                IChangeset cs__668 = S0::SrcGlobal.Changeset(t___10557, params__667).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10558)).ValidateLength(csid__503("name"), 2, 50);
                bool t___10562 = cs__668.IsValid;
                string fn__10554()
                {
                    return "should be valid";
                }
                test___29.Assert(t___10562, (S1::Func<string>) fn__10554);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__1552()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__670 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___10545 = userTable__504();
                ISafeIdentifier t___10546 = csid__503("name");
                IChangeset cs__671 = S0::SrcGlobal.Changeset(t___10545, params__670).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10546)).ValidateLength(csid__503("name"), 2, 50);
                bool t___10552 = !cs__671.IsValid;
                string fn__10542()
                {
                    return "should be invalid";
                }
                test___30.Assert(t___10552, (S1::Func<string>) fn__10542);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__1553()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__673 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___10533 = userTable__504();
                ISafeIdentifier t___10534 = csid__503("name");
                IChangeset cs__674 = S0::SrcGlobal.Changeset(t___10533, params__673).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10534)).ValidateLength(csid__503("name"), 2, 10);
                bool t___10540 = !cs__674.IsValid;
                string fn__10530()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___10540, (S1::Func<string>) fn__10530);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__1554()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__676 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___10522 = userTable__504();
                ISafeIdentifier t___10523 = csid__503("age");
                IChangeset cs__677 = S0::SrcGlobal.Changeset(t___10522, params__676).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10523)).ValidateInt(csid__503("age"));
                bool t___10527 = cs__677.IsValid;
                string fn__10519()
                {
                    return "should be valid";
                }
                test___32.Assert(t___10527, (S1::Func<string>) fn__10519);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__1555()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__679 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___10510 = userTable__504();
                ISafeIdentifier t___10511 = csid__503("age");
                IChangeset cs__680 = S0::SrcGlobal.Changeset(t___10510, params__679).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10511)).ValidateInt(csid__503("age"));
                bool t___10517 = !cs__680.IsValid;
                string fn__10507()
                {
                    return "should be invalid";
                }
                test___33.Assert(t___10517, (S1::Func<string>) fn__10507);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__1556()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__682 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___10499 = userTable__504();
                ISafeIdentifier t___10500 = csid__503("score");
                IChangeset cs__683 = S0::SrcGlobal.Changeset(t___10499, params__682).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10500)).ValidateFloat(csid__503("score"));
                bool t___10504 = cs__683.IsValid;
                string fn__10496()
                {
                    return "should be valid";
                }
                test___34.Assert(t___10504, (S1::Func<string>) fn__10496);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__1557()
        {
            T::Test test___35 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__685 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___10488 = userTable__504();
                ISafeIdentifier t___10489 = csid__503("age");
                IChangeset cs__686 = S0::SrcGlobal.Changeset(t___10488, params__685).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10489)).ValidateInt64(csid__503("age"));
                bool t___10493 = cs__686.IsValid;
                string fn__10485()
                {
                    return "should be valid";
                }
                test___35.Assert(t___10493, (S1::Func<string>) fn__10485);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__1558()
        {
            T::Test test___36 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__688 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___10476 = userTable__504();
                ISafeIdentifier t___10477 = csid__503("age");
                IChangeset cs__689 = S0::SrcGlobal.Changeset(t___10476, params__688).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10477)).ValidateInt64(csid__503("age"));
                bool t___10483 = !cs__689.IsValid;
                string fn__10473()
                {
                    return "should be invalid";
                }
                test___36.Assert(t___10483, (S1::Func<string>) fn__10473);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__1559()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__10470(string v__691)
                {
                    G::IReadOnlyDictionary<string, string> params__692 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__691)));
                    TableDef t___10462 = userTable__504();
                    ISafeIdentifier t___10463 = csid__503("active");
                    IChangeset cs__693 = S0::SrcGlobal.Changeset(t___10462, params__692).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10463)).ValidateBool(csid__503("active"));
                    bool t___10467 = cs__693.IsValid;
                    string fn__10459()
                    {
                        return "should accept: " + v__691;
                    }
                    test___37.Assert(t___10467, (S1::Func<string>) fn__10459);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__10470);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__1560()
        {
            T::Test test___38 = new T::Test();
            try
            {
                void fn__10456(string v__695)
                {
                    G::IReadOnlyDictionary<string, string> params__696 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__695)));
                    TableDef t___10448 = userTable__504();
                    ISafeIdentifier t___10449 = csid__503("active");
                    IChangeset cs__697 = S0::SrcGlobal.Changeset(t___10448, params__696).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10449)).ValidateBool(csid__503("active"));
                    bool t___10453 = cs__697.IsValid;
                    string fn__10445()
                    {
                        return "should accept: " + v__695;
                    }
                    test___38.Assert(t___10453, (S1::Func<string>) fn__10445);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__10456);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__1561()
        {
            T::Test test___39 = new T::Test();
            try
            {
                void fn__10442(string v__699)
                {
                    G::IReadOnlyDictionary<string, string> params__700 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__699)));
                    TableDef t___10433 = userTable__504();
                    ISafeIdentifier t___10434 = csid__503("active");
                    IChangeset cs__701 = S0::SrcGlobal.Changeset(t___10433, params__700).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10434)).ValidateBool(csid__503("active"));
                    bool t___10440 = !cs__701.IsValid;
                    string fn__10430()
                    {
                        return "should reject ambiguous: " + v__699;
                    }
                    test___39.Assert(t___10440, (S1::Func<string>) fn__10430);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__10442);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__1562()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__703 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___10418 = userTable__504();
                ISafeIdentifier t___10419 = csid__503("name");
                ISafeIdentifier t___10420 = csid__503("email");
                IChangeset cs__704 = S0::SrcGlobal.Changeset(t___10418, params__703).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10419, t___10420)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name"), csid__503("email")));
                SqlFragment t___5833;
                t___5833 = cs__704.ToInsertSql();
                SqlFragment sqlFrag__705 = t___5833;
                string s__706 = sqlFrag__705.ToString();
                bool t___10427 = s__706.IndexOf("''") >= 0;
                string fn__10414()
                {
                    return "single quote must be doubled: " + s__706;
                }
                test___40.Assert(t___10427, (S1::Func<string>) fn__10414);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__1563()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__708 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___10398 = userTable__504();
                ISafeIdentifier t___10399 = csid__503("name");
                ISafeIdentifier t___10400 = csid__503("email");
                IChangeset cs__709 = S0::SrcGlobal.Changeset(t___10398, params__708).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10399, t___10400)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name"), csid__503("email")));
                SqlFragment t___5812;
                t___5812 = cs__709.ToInsertSql();
                SqlFragment sqlFrag__710 = t___5812;
                string s__711 = sqlFrag__710.ToString();
                bool t___10407 = s__711.IndexOf("INSERT INTO users") >= 0;
                string fn__10394()
                {
                    return "has INSERT INTO: " + s__711;
                }
                test___41.Assert(t___10407, (S1::Func<string>) fn__10394);
                bool t___10411 = s__711.IndexOf("'Alice'") >= 0;
                string fn__10393()
                {
                    return "has quoted name: " + s__711;
                }
                test___41.Assert(t___10411, (S1::Func<string>) fn__10393);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__1564()
        {
            T::Test test___42 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__713 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___10380 = userTable__504();
                ISafeIdentifier t___10381 = csid__503("name");
                ISafeIdentifier t___10382 = csid__503("email");
                ISafeIdentifier t___10383 = csid__503("age");
                IChangeset cs__714 = S0::SrcGlobal.Changeset(t___10380, params__713).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10381, t___10382, t___10383)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name"), csid__503("email")));
                SqlFragment t___5795;
                t___5795 = cs__714.ToInsertSql();
                SqlFragment sqlFrag__715 = t___5795;
                string s__716 = sqlFrag__715.ToString();
                bool t___10390 = s__716.IndexOf("25") >= 0;
                string fn__10375()
                {
                    return "age rendered unquoted: " + s__716;
                }
                test___42.Assert(t___10390, (S1::Func<string>) fn__10375);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__1565()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__718 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___10368 = userTable__504();
                ISafeIdentifier t___10369 = csid__503("name");
                IChangeset cs__719 = S0::SrcGlobal.Changeset(t___10368, params__718).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10369)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name")));
                bool didBubble__720;
                try
                {
                    cs__719.ToInsertSql();
                    didBubble__720 = false;
                }
                catch
                {
                    didBubble__720 = true;
                }
                string fn__10366()
                {
                    return "invalid changeset should bubble";
                }
                test___43.Assert(didBubble__720, (S1::Func<string>) fn__10366);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__1566()
        {
            T::Test test___44 = new T::Test();
            try
            {
                TableDef strictTable__722 = new TableDef(csid__503("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__503("title"), new StringField(), false), new FieldDef(csid__503("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__723 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___10359 = csid__503("body");
                IChangeset cs__724 = S0::SrcGlobal.Changeset(strictTable__722, params__723).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10359));
                bool t___10361 = cs__724.IsValid;
                string fn__10348()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___44.Assert(t___10361, (S1::Func<string>) fn__10348);
                bool didBubble__725;
                try
                {
                    cs__724.ToInsertSql();
                    didBubble__725 = false;
                }
                catch
                {
                    didBubble__725 = true;
                }
                string fn__10347()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___44.Assert(didBubble__725, (S1::Func<string>) fn__10347);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__1567()
        {
            T::Test test___45 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__727 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___10338 = userTable__504();
                ISafeIdentifier t___10339 = csid__503("name");
                IChangeset cs__728 = S0::SrcGlobal.Changeset(t___10338, params__727).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10339)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name")));
                SqlFragment t___5755;
                t___5755 = cs__728.ToUpdateSql(42);
                SqlFragment sqlFrag__729 = t___5755;
                string s__730 = sqlFrag__729.ToString();
                bool t___10345 = s__730 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__10335()
                {
                    return "got: " + s__730;
                }
                test___45.Assert(t___10345, (S1::Func<string>) fn__10335);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__1568()
        {
            T::Test test___46 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__732 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___10328 = userTable__504();
                ISafeIdentifier t___10329 = csid__503("name");
                IChangeset cs__733 = S0::SrcGlobal.Changeset(t___10328, params__732).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___10329)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__503("name")));
                bool didBubble__734;
                try
                {
                    cs__733.ToUpdateSql(1);
                    didBubble__734 = false;
                }
                catch
                {
                    didBubble__734 = true;
                }
                string fn__10326()
                {
                    return "invalid changeset should bubble";
                }
                test___46.Assert(didBubble__734, (S1::Func<string>) fn__10326);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__505(string name__1039)
        {
            ISafeIdentifier t___5241;
            t___5241 = S0::SrcGlobal.SafeIdentifier(name__1039);
            return t___5241;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__1644()
        {
            T::Test test___47 = new T::Test();
            try
            {
                Query q__1042 = S0::SrcGlobal.From(sid__505("users"));
                bool t___9837 = q__1042.ToSql().ToString() == "SELECT * FROM users";
                string fn__9832()
                {
                    return "bare query";
                }
                test___47.Assert(t___9837, (S1::Func<string>) fn__9832);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__1645()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___9823 = sid__505("users");
                ISafeIdentifier t___9824 = sid__505("id");
                ISafeIdentifier t___9825 = sid__505("name");
                Query q__1044 = S0::SrcGlobal.From(t___9823).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9824, t___9825));
                bool t___9830 = q__1044.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__9822()
                {
                    return "select columns";
                }
                test___48.Assert(t___9830, (S1::Func<string>) fn__9822);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__1646()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___9811 = sid__505("users");
                SqlBuilder t___9812 = new SqlBuilder();
                t___9812.AppendSafe("age > ");
                t___9812.AppendInt32(18);
                SqlFragment t___9815 = t___9812.Accumulated;
                Query q__1046 = S0::SrcGlobal.From(t___9811).Where(t___9815);
                bool t___9820 = q__1046.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__9810()
                {
                    return "where int";
                }
                test___49.Assert(t___9820, (S1::Func<string>) fn__9810);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__1648()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___9799 = sid__505("users");
                SqlBuilder t___9800 = new SqlBuilder();
                t___9800.AppendSafe("active = ");
                t___9800.AppendBoolean(true);
                SqlFragment t___9803 = t___9800.Accumulated;
                Query q__1048 = S0::SrcGlobal.From(t___9799).Where(t___9803);
                bool t___9808 = q__1048.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__9798()
                {
                    return "where bool";
                }
                test___50.Assert(t___9808, (S1::Func<string>) fn__9798);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__1650()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___9782 = sid__505("users");
                SqlBuilder t___9783 = new SqlBuilder();
                t___9783.AppendSafe("age > ");
                t___9783.AppendInt32(18);
                SqlFragment t___9786 = t___9783.Accumulated;
                Query t___9787 = S0::SrcGlobal.From(t___9782).Where(t___9786);
                SqlBuilder t___9788 = new SqlBuilder();
                t___9788.AppendSafe("active = ");
                t___9788.AppendBoolean(true);
                Query q__1050 = t___9787.Where(t___9788.Accumulated);
                bool t___9796 = q__1050.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__9781()
                {
                    return "chained where";
                }
                test___51.Assert(t___9796, (S1::Func<string>) fn__9781);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__1653()
        {
            T::Test test___52 = new T::Test();
            try
            {
                ISafeIdentifier t___9773 = sid__505("users");
                ISafeIdentifier t___9774 = sid__505("name");
                Query q__1052 = S0::SrcGlobal.From(t___9773).OrderBy(t___9774, true);
                bool t___9779 = q__1052.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__9772()
                {
                    return "order asc";
                }
                test___52.Assert(t___9779, (S1::Func<string>) fn__9772);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__1654()
        {
            T::Test test___53 = new T::Test();
            try
            {
                ISafeIdentifier t___9764 = sid__505("users");
                ISafeIdentifier t___9765 = sid__505("created_at");
                Query q__1054 = S0::SrcGlobal.From(t___9764).OrderBy(t___9765, false);
                bool t___9770 = q__1054.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__9763()
                {
                    return "order desc";
                }
                test___53.Assert(t___9770, (S1::Func<string>) fn__9763);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__1655()
        {
            T::Test test___54 = new T::Test();
            try
            {
                Query t___5175;
                t___5175 = S0::SrcGlobal.From(sid__505("users")).Limit(10);
                Query t___5176;
                t___5176 = t___5175.Offset(20);
                Query q__1056 = t___5176;
                bool t___9761 = q__1056.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__9756()
                {
                    return "limit/offset";
                }
                test___54.Assert(t___9761, (S1::Func<string>) fn__9756);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__1656()
        {
            T::Test test___55 = new T::Test();
            try
            {
                bool didBubble__1058;
                try
                {
                    S0::SrcGlobal.From(sid__505("users")).Limit(-1);
                    didBubble__1058 = false;
                }
                catch
                {
                    didBubble__1058 = true;
                }
                string fn__9752()
                {
                    return "negative limit should bubble";
                }
                test___55.Assert(didBubble__1058, (S1::Func<string>) fn__9752);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__1657()
        {
            T::Test test___56 = new T::Test();
            try
            {
                bool didBubble__1060;
                try
                {
                    S0::SrcGlobal.From(sid__505("users")).Offset(-1);
                    didBubble__1060 = false;
                }
                catch
                {
                    didBubble__1060 = true;
                }
                string fn__9748()
                {
                    return "negative offset should bubble";
                }
                test___56.Assert(didBubble__1060, (S1::Func<string>) fn__9748);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__1658()
        {
            T::Test test___57 = new T::Test();
            try
            {
                int minAge__1062 = 21;
                ISafeIdentifier t___9726 = sid__505("users");
                ISafeIdentifier t___9727 = sid__505("id");
                ISafeIdentifier t___9728 = sid__505("name");
                ISafeIdentifier t___9729 = sid__505("email");
                Query t___9730 = S0::SrcGlobal.From(t___9726).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9727, t___9728, t___9729));
                SqlBuilder t___9731 = new SqlBuilder();
                t___9731.AppendSafe("age >= ");
                t___9731.AppendInt32(21);
                Query t___9735 = t___9730.Where(t___9731.Accumulated);
                SqlBuilder t___9736 = new SqlBuilder();
                t___9736.AppendSafe("active = ");
                t___9736.AppendBoolean(true);
                Query t___5161;
                t___5161 = t___9735.Where(t___9736.Accumulated).OrderBy(sid__505("name"), true).Limit(25);
                Query t___5162;
                t___5162 = t___5161.Offset(0);
                Query q__1063 = t___5162;
                bool t___9746 = q__1063.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__9725()
                {
                    return "complex query";
                }
                test___57.Assert(t___9746, (S1::Func<string>) fn__9725);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__1661()
        {
            T::Test test___58 = new T::Test();
            try
            {
                Query q__1065 = S0::SrcGlobal.From(sid__505("users"));
                SqlFragment t___5138;
                t___5138 = q__1065.SafeToSql(100);
                SqlFragment t___5139 = t___5138;
                string s__1066 = t___5139.ToString();
                bool t___9723 = s__1066 == "SELECT * FROM users LIMIT 100";
                string fn__9719()
                {
                    return "should have limit: " + s__1066;
                }
                test___58.Assert(t___9723, (S1::Func<string>) fn__9719);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__1662()
        {
            T::Test test___59 = new T::Test();
            try
            {
                Query t___5130;
                t___5130 = S0::SrcGlobal.From(sid__505("users")).Limit(5);
                Query q__1068 = t___5130;
                SqlFragment t___5133;
                t___5133 = q__1068.SafeToSql(100);
                SqlFragment t___5134 = t___5133;
                string s__1069 = t___5134.ToString();
                bool t___9717 = s__1069 == "SELECT * FROM users LIMIT 5";
                string fn__9713()
                {
                    return "explicit limit preserved: " + s__1069;
                }
                test___59.Assert(t___9717, (S1::Func<string>) fn__9713);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__1663()
        {
            T::Test test___60 = new T::Test();
            try
            {
                bool didBubble__1071;
                try
                {
                    S0::SrcGlobal.From(sid__505("users")).SafeToSql(-1);
                    didBubble__1071 = false;
                }
                catch
                {
                    didBubble__1071 = true;
                }
                string fn__9709()
                {
                    return "negative defaultLimit should bubble";
                }
                test___60.Assert(didBubble__1071, (S1::Func<string>) fn__9709);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__1664()
        {
            T::Test test___61 = new T::Test();
            try
            {
                string evil__1073 = "'; DROP TABLE users; --";
                ISafeIdentifier t___9693 = sid__505("users");
                SqlBuilder t___9694 = new SqlBuilder();
                t___9694.AppendSafe("name = ");
                t___9694.AppendString("'; DROP TABLE users; --");
                SqlFragment t___9697 = t___9694.Accumulated;
                Query q__1074 = S0::SrcGlobal.From(t___9693).Where(t___9697);
                string s__1075 = q__1074.ToSql().ToString();
                bool t___9702 = s__1075.IndexOf("''") >= 0;
                string fn__9692()
                {
                    return "quotes must be doubled: " + s__1075;
                }
                test___61.Assert(t___9702, (S1::Func<string>) fn__9692);
                bool t___9706 = s__1075.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__9691()
                {
                    return "structure intact: " + s__1075;
                }
                test___61.Assert(t___9706, (S1::Func<string>) fn__9691);
            }
            finally
            {
                test___61.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__1666()
        {
            T::Test test___62 = new T::Test();
            try
            {
                string attack__1077 = "users; DROP TABLE users; --";
                bool didBubble__1078;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__1078 = false;
                }
                catch
                {
                    didBubble__1078 = true;
                }
                string fn__9688()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___62.Assert(didBubble__1078, (S1::Func<string>) fn__9688);
            }
            finally
            {
                test___62.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void innerJoinProducesInnerJoin__1667()
        {
            T::Test test___63 = new T::Test();
            try
            {
                ISafeIdentifier t___9677 = sid__505("users");
                ISafeIdentifier t___9678 = sid__505("orders");
                SqlBuilder t___9679 = new SqlBuilder();
                t___9679.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9681 = t___9679.Accumulated;
                Query q__1080 = S0::SrcGlobal.From(t___9677).InnerJoin(t___9678, t___9681);
                bool t___9686 = q__1080.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__9676()
                {
                    return "inner join";
                }
                test___63.Assert(t___9686, (S1::Func<string>) fn__9676);
            }
            finally
            {
                test___63.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void leftJoinProducesLeftJoin__1669()
        {
            T::Test test___64 = new T::Test();
            try
            {
                ISafeIdentifier t___9665 = sid__505("users");
                ISafeIdentifier t___9666 = sid__505("profiles");
                SqlBuilder t___9667 = new SqlBuilder();
                t___9667.AppendSafe("users.id = profiles.user_id");
                SqlFragment t___9669 = t___9667.Accumulated;
                Query q__1082 = S0::SrcGlobal.From(t___9665).LeftJoin(t___9666, t___9669);
                bool t___9674 = q__1082.ToSql().ToString() == "SELECT * FROM users LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__9664()
                {
                    return "left join";
                }
                test___64.Assert(t___9674, (S1::Func<string>) fn__9664);
            }
            finally
            {
                test___64.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void rightJoinProducesRightJoin__1671()
        {
            T::Test test___65 = new T::Test();
            try
            {
                ISafeIdentifier t___9653 = sid__505("orders");
                ISafeIdentifier t___9654 = sid__505("users");
                SqlBuilder t___9655 = new SqlBuilder();
                t___9655.AppendSafe("orders.user_id = users.id");
                SqlFragment t___9657 = t___9655.Accumulated;
                Query q__1084 = S0::SrcGlobal.From(t___9653).RightJoin(t___9654, t___9657);
                bool t___9662 = q__1084.ToSql().ToString() == "SELECT * FROM orders RIGHT JOIN users ON orders.user_id = users.id";
                string fn__9652()
                {
                    return "right join";
                }
                test___65.Assert(t___9662, (S1::Func<string>) fn__9652);
            }
            finally
            {
                test___65.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullJoinProducesFullOuterJoin__1673()
        {
            T::Test test___66 = new T::Test();
            try
            {
                ISafeIdentifier t___9641 = sid__505("users");
                ISafeIdentifier t___9642 = sid__505("orders");
                SqlBuilder t___9643 = new SqlBuilder();
                t___9643.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9645 = t___9643.Accumulated;
                Query q__1086 = S0::SrcGlobal.From(t___9641).FullJoin(t___9642, t___9645);
                bool t___9650 = q__1086.ToSql().ToString() == "SELECT * FROM users FULL OUTER JOIN orders ON users.id = orders.user_id";
                string fn__9640()
                {
                    return "full join";
                }
                test___66.Assert(t___9650, (S1::Func<string>) fn__9640);
            }
            finally
            {
                test___66.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedJoins__1675()
        {
            T::Test test___67 = new T::Test();
            try
            {
                ISafeIdentifier t___9624 = sid__505("users");
                ISafeIdentifier t___9625 = sid__505("orders");
                SqlBuilder t___9626 = new SqlBuilder();
                t___9626.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9628 = t___9626.Accumulated;
                Query t___9629 = S0::SrcGlobal.From(t___9624).InnerJoin(t___9625, t___9628);
                ISafeIdentifier t___9630 = sid__505("profiles");
                SqlBuilder t___9631 = new SqlBuilder();
                t___9631.AppendSafe("users.id = profiles.user_id");
                Query q__1088 = t___9629.LeftJoin(t___9630, t___9631.Accumulated);
                bool t___9638 = q__1088.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id LEFT JOIN profiles ON users.id = profiles.user_id";
                string fn__9623()
                {
                    return "chained joins";
                }
                test___67.Assert(t___9638, (S1::Func<string>) fn__9623);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithWhereAndOrderBy__1678()
        {
            T::Test test___68 = new T::Test();
            try
            {
                ISafeIdentifier t___9605 = sid__505("users");
                ISafeIdentifier t___9606 = sid__505("orders");
                SqlBuilder t___9607 = new SqlBuilder();
                t___9607.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9609 = t___9607.Accumulated;
                Query t___9610 = S0::SrcGlobal.From(t___9605).InnerJoin(t___9606, t___9609);
                SqlBuilder t___9611 = new SqlBuilder();
                t___9611.AppendSafe("orders.total > ");
                t___9611.AppendInt32(100);
                Query t___5045;
                t___5045 = t___9610.Where(t___9611.Accumulated).OrderBy(sid__505("name"), true).Limit(10);
                Query q__1090 = t___5045;
                bool t___9621 = q__1090.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100 ORDER BY name ASC LIMIT 10";
                string fn__9604()
                {
                    return "join with where/order/limit";
                }
                test___68.Assert(t___9621, (S1::Func<string>) fn__9604);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void colHelperProducesQualifiedReference__1681()
        {
            T::Test test___69 = new T::Test();
            try
            {
                SqlFragment c__1092 = S0::SrcGlobal.Col(sid__505("users"), sid__505("id"));
                bool t___9602 = c__1092.ToString() == "users.id";
                string fn__9596()
                {
                    return "col helper";
                }
                test___69.Assert(t___9602, (S1::Func<string>) fn__9596);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void joinWithColHelper__1682()
        {
            T::Test test___70 = new T::Test();
            try
            {
                SqlFragment onCond__1094 = S0::SrcGlobal.Col(sid__505("users"), sid__505("id"));
                SqlBuilder b__1095 = new SqlBuilder();
                b__1095.AppendFragment(onCond__1094);
                b__1095.AppendSafe(" = ");
                b__1095.AppendFragment(S0::SrcGlobal.Col(sid__505("orders"), sid__505("user_id")));
                ISafeIdentifier t___9587 = sid__505("users");
                ISafeIdentifier t___9588 = sid__505("orders");
                SqlFragment t___9589 = b__1095.Accumulated;
                Query q__1096 = S0::SrcGlobal.From(t___9587).InnerJoin(t___9588, t___9589);
                bool t___9594 = q__1096.ToSql().ToString() == "SELECT * FROM users INNER JOIN orders ON users.id = orders.user_id";
                string fn__9576()
                {
                    return "join with col";
                }
                test___70.Assert(t___9594, (S1::Func<string>) fn__9576);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orWhereBasic__1683()
        {
            T::Test test___71 = new T::Test();
            try
            {
                ISafeIdentifier t___9565 = sid__505("users");
                SqlBuilder t___9566 = new SqlBuilder();
                t___9566.AppendSafe("status = ");
                t___9566.AppendString("active");
                SqlFragment t___9569 = t___9566.Accumulated;
                Query q__1098 = S0::SrcGlobal.From(t___9565).OrWhere(t___9569);
                bool t___9574 = q__1098.ToSql().ToString() == "SELECT * FROM users WHERE status = 'active'";
                string fn__9564()
                {
                    return "orWhere basic";
                }
                test___71.Assert(t___9574, (S1::Func<string>) fn__9564);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereThenOrWhere__1685()
        {
            T::Test test___72 = new T::Test();
            try
            {
                ISafeIdentifier t___9548 = sid__505("users");
                SqlBuilder t___9549 = new SqlBuilder();
                t___9549.AppendSafe("age > ");
                t___9549.AppendInt32(18);
                SqlFragment t___9552 = t___9549.Accumulated;
                Query t___9553 = S0::SrcGlobal.From(t___9548).Where(t___9552);
                SqlBuilder t___9554 = new SqlBuilder();
                t___9554.AppendSafe("vip = ");
                t___9554.AppendBoolean(true);
                Query q__1100 = t___9553.OrWhere(t___9554.Accumulated);
                bool t___9562 = q__1100.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 OR vip = TRUE";
                string fn__9547()
                {
                    return "where then orWhere";
                }
                test___72.Assert(t___9562, (S1::Func<string>) fn__9547);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void multipleOrWhere__1688()
        {
            T::Test test___73 = new T::Test();
            try
            {
                ISafeIdentifier t___9526 = sid__505("users");
                SqlBuilder t___9527 = new SqlBuilder();
                t___9527.AppendSafe("active = ");
                t___9527.AppendBoolean(true);
                SqlFragment t___9530 = t___9527.Accumulated;
                Query t___9531 = S0::SrcGlobal.From(t___9526).Where(t___9530);
                SqlBuilder t___9532 = new SqlBuilder();
                t___9532.AppendSafe("role = ");
                t___9532.AppendString("admin");
                Query t___9536 = t___9531.OrWhere(t___9532.Accumulated);
                SqlBuilder t___9537 = new SqlBuilder();
                t___9537.AppendSafe("role = ");
                t___9537.AppendString("moderator");
                Query q__1102 = t___9536.OrWhere(t___9537.Accumulated);
                bool t___9545 = q__1102.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE OR role = 'admin' OR role = 'moderator'";
                string fn__9525()
                {
                    return "multiple orWhere";
                }
                test___73.Assert(t___9545, (S1::Func<string>) fn__9525);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void mixedWhereAndOrWhere__1692()
        {
            T::Test test___74 = new T::Test();
            try
            {
                ISafeIdentifier t___9504 = sid__505("users");
                SqlBuilder t___9505 = new SqlBuilder();
                t___9505.AppendSafe("age > ");
                t___9505.AppendInt32(18);
                SqlFragment t___9508 = t___9505.Accumulated;
                Query t___9509 = S0::SrcGlobal.From(t___9504).Where(t___9508);
                SqlBuilder t___9510 = new SqlBuilder();
                t___9510.AppendSafe("active = ");
                t___9510.AppendBoolean(true);
                Query t___9514 = t___9509.Where(t___9510.Accumulated);
                SqlBuilder t___9515 = new SqlBuilder();
                t___9515.AppendSafe("vip = ");
                t___9515.AppendBoolean(true);
                Query q__1104 = t___9514.OrWhere(t___9515.Accumulated);
                bool t___9523 = q__1104.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE OR vip = TRUE";
                string fn__9503()
                {
                    return "mixed where and orWhere";
                }
                test___74.Assert(t___9523, (S1::Func<string>) fn__9503);
            }
            finally
            {
                test___74.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNull__1696()
        {
            T::Test test___75 = new T::Test();
            try
            {
                ISafeIdentifier t___9495 = sid__505("users");
                ISafeIdentifier t___9496 = sid__505("deleted_at");
                Query q__1106 = S0::SrcGlobal.From(t___9495).WhereNull(t___9496);
                bool t___9501 = q__1106.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL";
                string fn__9494()
                {
                    return "whereNull";
                }
                test___75.Assert(t___9501, (S1::Func<string>) fn__9494);
            }
            finally
            {
                test___75.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNull__1697()
        {
            T::Test test___76 = new T::Test();
            try
            {
                ISafeIdentifier t___9486 = sid__505("users");
                ISafeIdentifier t___9487 = sid__505("email");
                Query q__1108 = S0::SrcGlobal.From(t___9486).WhereNotNull(t___9487);
                bool t___9492 = q__1108.ToSql().ToString() == "SELECT * FROM users WHERE email IS NOT NULL";
                string fn__9485()
                {
                    return "whereNotNull";
                }
                test___76.Assert(t___9492, (S1::Func<string>) fn__9485);
            }
            finally
            {
                test___76.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNullChainedWithWhere__1698()
        {
            T::Test test___77 = new T::Test();
            try
            {
                ISafeIdentifier t___9472 = sid__505("users");
                SqlBuilder t___9473 = new SqlBuilder();
                t___9473.AppendSafe("active = ");
                t___9473.AppendBoolean(true);
                SqlFragment t___9476 = t___9473.Accumulated;
                Query q__1110 = S0::SrcGlobal.From(t___9472).Where(t___9476).WhereNull(sid__505("deleted_at"));
                bool t___9483 = q__1110.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND deleted_at IS NULL";
                string fn__9471()
                {
                    return "whereNull chained";
                }
                test___77.Assert(t___9483, (S1::Func<string>) fn__9471);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotNullChainedWithOrWhere__1700()
        {
            T::Test test___78 = new T::Test();
            try
            {
                ISafeIdentifier t___9458 = sid__505("users");
                ISafeIdentifier t___9459 = sid__505("deleted_at");
                Query t___9460 = S0::SrcGlobal.From(t___9458).WhereNull(t___9459);
                SqlBuilder t___9461 = new SqlBuilder();
                t___9461.AppendSafe("role = ");
                t___9461.AppendString("admin");
                Query q__1112 = t___9460.OrWhere(t___9461.Accumulated);
                bool t___9469 = q__1112.ToSql().ToString() == "SELECT * FROM users WHERE deleted_at IS NULL OR role = 'admin'";
                string fn__9457()
                {
                    return "whereNotNull with orWhere";
                }
                test___78.Assert(t___9469, (S1::Func<string>) fn__9457);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithIntValues__1702()
        {
            T::Test test___79 = new T::Test();
            try
            {
                ISafeIdentifier t___9446 = sid__505("users");
                ISafeIdentifier t___9447 = sid__505("id");
                SqlInt32 t___9448 = new SqlInt32(1);
                SqlInt32 t___9449 = new SqlInt32(2);
                SqlInt32 t___9450 = new SqlInt32(3);
                Query q__1114 = S0::SrcGlobal.From(t___9446).WhereIn(t___9447, C::Listed.CreateReadOnlyList<SqlInt32>(t___9448, t___9449, t___9450));
                bool t___9455 = q__1114.ToSql().ToString() == "SELECT * FROM users WHERE id IN (1, 2, 3)";
                string fn__9445()
                {
                    return "whereIn ints";
                }
                test___79.Assert(t___9455, (S1::Func<string>) fn__9445);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithStringValuesEscaping__1703()
        {
            T::Test test___80 = new T::Test();
            try
            {
                ISafeIdentifier t___9435 = sid__505("users");
                ISafeIdentifier t___9436 = sid__505("name");
                SqlString t___9437 = new SqlString("Alice");
                SqlString t___9438 = new SqlString("Bob's");
                Query q__1116 = S0::SrcGlobal.From(t___9435).WhereIn(t___9436, C::Listed.CreateReadOnlyList<SqlString>(t___9437, t___9438));
                bool t___9443 = q__1116.ToSql().ToString() == "SELECT * FROM users WHERE name IN ('Alice', 'Bob''s')";
                string fn__9434()
                {
                    return "whereIn strings";
                }
                test___80.Assert(t___9443, (S1::Func<string>) fn__9434);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInWithEmptyListProduces1_0__1704()
        {
            T::Test test___81 = new T::Test();
            try
            {
                ISafeIdentifier t___9426 = sid__505("users");
                ISafeIdentifier t___9427 = sid__505("id");
                Query q__1118 = S0::SrcGlobal.From(t___9426).WhereIn(t___9427, C::Listed.CreateReadOnlyList<ISqlPart>());
                bool t___9432 = q__1118.ToSql().ToString() == "SELECT * FROM users WHERE 1 = 0";
                string fn__9425()
                {
                    return "whereIn empty";
                }
                test___81.Assert(t___9432, (S1::Func<string>) fn__9425);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInChained__1705()
        {
            T::Test test___82 = new T::Test();
            try
            {
                ISafeIdentifier t___9410 = sid__505("users");
                SqlBuilder t___9411 = new SqlBuilder();
                t___9411.AppendSafe("active = ");
                t___9411.AppendBoolean(true);
                SqlFragment t___9414 = t___9411.Accumulated;
                Query q__1120 = S0::SrcGlobal.From(t___9410).Where(t___9414).WhereIn(sid__505("role"), C::Listed.CreateReadOnlyList<SqlString>(new SqlString("admin"), new SqlString("user")));
                bool t___9423 = q__1120.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND role IN ('admin', 'user')";
                string fn__9409()
                {
                    return "whereIn chained";
                }
                test___82.Assert(t___9423, (S1::Func<string>) fn__9409);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSingleElement__1707()
        {
            T::Test test___83 = new T::Test();
            try
            {
                ISafeIdentifier t___9400 = sid__505("users");
                ISafeIdentifier t___9401 = sid__505("id");
                SqlInt32 t___9402 = new SqlInt32(42);
                Query q__1122 = S0::SrcGlobal.From(t___9400).WhereIn(t___9401, C::Listed.CreateReadOnlyList<SqlInt32>(t___9402));
                bool t___9407 = q__1122.ToSql().ToString() == "SELECT * FROM users WHERE id IN (42)";
                string fn__9399()
                {
                    return "whereIn single";
                }
                test___83.Assert(t___9407, (S1::Func<string>) fn__9399);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotBasic__1708()
        {
            T::Test test___84 = new T::Test();
            try
            {
                ISafeIdentifier t___9388 = sid__505("users");
                SqlBuilder t___9389 = new SqlBuilder();
                t___9389.AppendSafe("active = ");
                t___9389.AppendBoolean(true);
                SqlFragment t___9392 = t___9389.Accumulated;
                Query q__1124 = S0::SrcGlobal.From(t___9388).WhereNot(t___9392);
                bool t___9397 = q__1124.ToSql().ToString() == "SELECT * FROM users WHERE NOT (active = TRUE)";
                string fn__9387()
                {
                    return "whereNot";
                }
                test___84.Assert(t___9397, (S1::Func<string>) fn__9387);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereNotChained__1710()
        {
            T::Test test___85 = new T::Test();
            try
            {
                ISafeIdentifier t___9371 = sid__505("users");
                SqlBuilder t___9372 = new SqlBuilder();
                t___9372.AppendSafe("age > ");
                t___9372.AppendInt32(18);
                SqlFragment t___9375 = t___9372.Accumulated;
                Query t___9376 = S0::SrcGlobal.From(t___9371).Where(t___9375);
                SqlBuilder t___9377 = new SqlBuilder();
                t___9377.AppendSafe("banned = ");
                t___9377.AppendBoolean(true);
                Query q__1126 = t___9376.WhereNot(t___9377.Accumulated);
                bool t___9385 = q__1126.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND NOT (banned = TRUE)";
                string fn__9370()
                {
                    return "whereNot chained";
                }
                test___85.Assert(t___9385, (S1::Func<string>) fn__9370);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenIntegers__1713()
        {
            T::Test test___86 = new T::Test();
            try
            {
                ISafeIdentifier t___9360 = sid__505("users");
                ISafeIdentifier t___9361 = sid__505("age");
                SqlInt32 t___9362 = new SqlInt32(18);
                SqlInt32 t___9363 = new SqlInt32(65);
                Query q__1128 = S0::SrcGlobal.From(t___9360).WhereBetween(t___9361, t___9362, t___9363);
                bool t___9368 = q__1128.ToSql().ToString() == "SELECT * FROM users WHERE age BETWEEN 18 AND 65";
                string fn__9359()
                {
                    return "whereBetween ints";
                }
                test___86.Assert(t___9368, (S1::Func<string>) fn__9359);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereBetweenChained__1714()
        {
            T::Test test___87 = new T::Test();
            try
            {
                ISafeIdentifier t___9344 = sid__505("users");
                SqlBuilder t___9345 = new SqlBuilder();
                t___9345.AppendSafe("active = ");
                t___9345.AppendBoolean(true);
                SqlFragment t___9348 = t___9345.Accumulated;
                Query q__1130 = S0::SrcGlobal.From(t___9344).Where(t___9348).WhereBetween(sid__505("age"), new SqlInt32(21), new SqlInt32(30));
                bool t___9357 = q__1130.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE AND age BETWEEN 21 AND 30";
                string fn__9343()
                {
                    return "whereBetween chained";
                }
                test___87.Assert(t___9357, (S1::Func<string>) fn__9343);
            }
            finally
            {
                test___87.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeBasic__1716()
        {
            T::Test test___88 = new T::Test();
            try
            {
                ISafeIdentifier t___9335 = sid__505("users");
                ISafeIdentifier t___9336 = sid__505("name");
                Query q__1132 = S0::SrcGlobal.From(t___9335).WhereLike(t___9336, "John%");
                bool t___9341 = q__1132.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE 'John%'";
                string fn__9334()
                {
                    return "whereLike";
                }
                test___88.Assert(t___9341, (S1::Func<string>) fn__9334);
            }
            finally
            {
                test___88.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereIlikeBasic__1717()
        {
            T::Test test___89 = new T::Test();
            try
            {
                ISafeIdentifier t___9326 = sid__505("users");
                ISafeIdentifier t___9327 = sid__505("email");
                Query q__1134 = S0::SrcGlobal.From(t___9326).WhereILike(t___9327, "%@gmail.com");
                bool t___9332 = q__1134.ToSql().ToString() == "SELECT * FROM users WHERE email ILIKE '%@gmail.com'";
                string fn__9325()
                {
                    return "whereILike";
                }
                test___89.Assert(t___9332, (S1::Func<string>) fn__9325);
            }
            finally
            {
                test___89.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWithInjectionAttempt__1718()
        {
            T::Test test___90 = new T::Test();
            try
            {
                ISafeIdentifier t___9312 = sid__505("users");
                ISafeIdentifier t___9313 = sid__505("name");
                Query q__1136 = S0::SrcGlobal.From(t___9312).WhereLike(t___9313, "'; DROP TABLE users; --");
                string s__1137 = q__1136.ToSql().ToString();
                bool t___9318 = s__1137.IndexOf("''") >= 0;
                string fn__9311()
                {
                    return "like injection escaped: " + s__1137;
                }
                test___90.Assert(t___9318, (S1::Func<string>) fn__9311);
                bool t___9322 = s__1137.IndexOf("LIKE") >= 0;
                string fn__9310()
                {
                    return "like structure intact: " + s__1137;
                }
                test___90.Assert(t___9322, (S1::Func<string>) fn__9310);
            }
            finally
            {
                test___90.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereLikeWildcardPatterns__1719()
        {
            T::Test test___91 = new T::Test();
            try
            {
                ISafeIdentifier t___9302 = sid__505("users");
                ISafeIdentifier t___9303 = sid__505("name");
                Query q__1139 = S0::SrcGlobal.From(t___9302).WhereLike(t___9303, "%son%");
                bool t___9308 = q__1139.ToSql().ToString() == "SELECT * FROM users WHERE name LIKE '%son%'";
                string fn__9301()
                {
                    return "whereLike wildcard";
                }
                test___91.Assert(t___9308, (S1::Func<string>) fn__9301);
            }
            finally
            {
                test___91.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countAllProducesCount__1720()
        {
            T::Test test___92 = new T::Test();
            try
            {
                SqlFragment f__1141 = S0::SrcGlobal.CountAll();
                bool t___9299 = f__1141.ToString() == "COUNT(*)";
                string fn__9295()
                {
                    return "countAll";
                }
                test___92.Assert(t___9299, (S1::Func<string>) fn__9295);
            }
            finally
            {
                test___92.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countColProducesCountField__1721()
        {
            T::Test test___93 = new T::Test();
            try
            {
                SqlFragment f__1143 = S0::SrcGlobal.CountCol(sid__505("id"));
                bool t___9293 = f__1143.ToString() == "COUNT(id)";
                string fn__9288()
                {
                    return "countCol";
                }
                test___93.Assert(t___9293, (S1::Func<string>) fn__9288);
            }
            finally
            {
                test___93.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sumColProducesSumField__1722()
        {
            T::Test test___94 = new T::Test();
            try
            {
                SqlFragment f__1145 = S0::SrcGlobal.SumCol(sid__505("amount"));
                bool t___9286 = f__1145.ToString() == "SUM(amount)";
                string fn__9281()
                {
                    return "sumCol";
                }
                test___94.Assert(t___9286, (S1::Func<string>) fn__9281);
            }
            finally
            {
                test___94.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void avgColProducesAvgField__1723()
        {
            T::Test test___95 = new T::Test();
            try
            {
                SqlFragment f__1147 = S0::SrcGlobal.AvgCol(sid__505("price"));
                bool t___9279 = f__1147.ToString() == "AVG(price)";
                string fn__9274()
                {
                    return "avgCol";
                }
                test___95.Assert(t___9279, (S1::Func<string>) fn__9274);
            }
            finally
            {
                test___95.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void minColProducesMinField__1724()
        {
            T::Test test___96 = new T::Test();
            try
            {
                SqlFragment f__1149 = S0::SrcGlobal.MinCol(sid__505("created_at"));
                bool t___9272 = f__1149.ToString() == "MIN(created_at)";
                string fn__9267()
                {
                    return "minCol";
                }
                test___96.Assert(t___9272, (S1::Func<string>) fn__9267);
            }
            finally
            {
                test___96.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void maxColProducesMaxField__1725()
        {
            T::Test test___97 = new T::Test();
            try
            {
                SqlFragment f__1151 = S0::SrcGlobal.MaxCol(sid__505("score"));
                bool t___9265 = f__1151.ToString() == "MAX(score)";
                string fn__9260()
                {
                    return "maxCol";
                }
                test___97.Assert(t___9265, (S1::Func<string>) fn__9260);
            }
            finally
            {
                test___97.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithAggregate__1726()
        {
            T::Test test___98 = new T::Test();
            try
            {
                ISafeIdentifier t___9252 = sid__505("orders");
                SqlFragment t___9253 = S0::SrcGlobal.CountAll();
                Query q__1153 = S0::SrcGlobal.From(t___9252).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9253));
                bool t___9258 = q__1153.ToSql().ToString() == "SELECT COUNT(*) FROM orders";
                string fn__9251()
                {
                    return "selectExpr count";
                }
                test___98.Assert(t___9258, (S1::Func<string>) fn__9251);
            }
            finally
            {
                test___98.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprWithMultipleExpressions__1727()
        {
            T::Test test___99 = new T::Test();
            try
            {
                SqlFragment nameFrag__1155 = S0::SrcGlobal.Col(sid__505("users"), sid__505("name"));
                ISafeIdentifier t___9243 = sid__505("users");
                SqlFragment t___9244 = S0::SrcGlobal.CountAll();
                Query q__1156 = S0::SrcGlobal.From(t___9243).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(nameFrag__1155, t___9244));
                bool t___9249 = q__1156.ToSql().ToString() == "SELECT users.name, COUNT(*) FROM users";
                string fn__9239()
                {
                    return "selectExpr multi";
                }
                test___99.Assert(t___9249, (S1::Func<string>) fn__9239);
            }
            finally
            {
                test___99.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectExprOverridesSelectedFields__1728()
        {
            T::Test test___100 = new T::Test();
            try
            {
                ISafeIdentifier t___9228 = sid__505("users");
                ISafeIdentifier t___9229 = sid__505("id");
                ISafeIdentifier t___9230 = sid__505("name");
                Query q__1158 = S0::SrcGlobal.From(t___9228).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9229, t___9230)).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(S0::SrcGlobal.CountAll()));
                bool t___9237 = q__1158.ToSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__9227()
                {
                    return "selectExpr overrides select";
                }
                test___100.Assert(t___9237, (S1::Func<string>) fn__9227);
            }
            finally
            {
                test___100.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupBySingleField__1729()
        {
            T::Test test___101 = new T::Test();
            try
            {
                ISafeIdentifier t___9214 = sid__505("orders");
                SqlFragment t___9217 = S0::SrcGlobal.Col(sid__505("orders"), sid__505("status"));
                SqlFragment t___9218 = S0::SrcGlobal.CountAll();
                Query q__1160 = S0::SrcGlobal.From(t___9214).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9217, t___9218)).GroupBy(sid__505("status"));
                bool t___9225 = q__1160.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status";
                string fn__9213()
                {
                    return "groupBy single";
                }
                test___101.Assert(t___9225, (S1::Func<string>) fn__9213);
            }
            finally
            {
                test___101.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void groupByMultipleFields__1730()
        {
            T::Test test___102 = new T::Test();
            try
            {
                ISafeIdentifier t___9203 = sid__505("orders");
                ISafeIdentifier t___9204 = sid__505("status");
                Query q__1162 = S0::SrcGlobal.From(t___9203).GroupBy(t___9204).GroupBy(sid__505("category"));
                bool t___9211 = q__1162.ToSql().ToString() == "SELECT * FROM orders GROUP BY status, category";
                string fn__9202()
                {
                    return "groupBy multiple";
                }
                test___102.Assert(t___9211, (S1::Func<string>) fn__9202);
            }
            finally
            {
                test___102.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void havingBasic__1731()
        {
            T::Test test___103 = new T::Test();
            try
            {
                ISafeIdentifier t___9184 = sid__505("orders");
                SqlFragment t___9187 = S0::SrcGlobal.Col(sid__505("orders"), sid__505("status"));
                SqlFragment t___9188 = S0::SrcGlobal.CountAll();
                Query t___9191 = S0::SrcGlobal.From(t___9184).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9187, t___9188)).GroupBy(sid__505("status"));
                SqlBuilder t___9192 = new SqlBuilder();
                t___9192.AppendSafe("COUNT(*) > ");
                t___9192.AppendInt32(5);
                Query q__1164 = t___9191.Having(t___9192.Accumulated);
                bool t___9200 = q__1164.ToSql().ToString() == "SELECT orders.status, COUNT(*) FROM orders GROUP BY status HAVING COUNT(*) > 5";
                string fn__9183()
                {
                    return "having basic";
                }
                test___103.Assert(t___9200, (S1::Func<string>) fn__9183);
            }
            finally
            {
                test___103.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orHaving__1733()
        {
            T::Test test___104 = new T::Test();
            try
            {
                ISafeIdentifier t___9165 = sid__505("orders");
                ISafeIdentifier t___9166 = sid__505("status");
                Query t___9167 = S0::SrcGlobal.From(t___9165).GroupBy(t___9166);
                SqlBuilder t___9168 = new SqlBuilder();
                t___9168.AppendSafe("COUNT(*) > ");
                t___9168.AppendInt32(5);
                Query t___9172 = t___9167.Having(t___9168.Accumulated);
                SqlBuilder t___9173 = new SqlBuilder();
                t___9173.AppendSafe("SUM(total) > ");
                t___9173.AppendInt32(1000);
                Query q__1166 = t___9172.OrHaving(t___9173.Accumulated);
                bool t___9181 = q__1166.ToSql().ToString() == "SELECT * FROM orders GROUP BY status HAVING COUNT(*) > 5 OR SUM(total) > 1000";
                string fn__9164()
                {
                    return "orHaving";
                }
                test___104.Assert(t___9181, (S1::Func<string>) fn__9164);
            }
            finally
            {
                test___104.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctBasic__1736()
        {
            T::Test test___105 = new T::Test();
            try
            {
                ISafeIdentifier t___9155 = sid__505("users");
                ISafeIdentifier t___9156 = sid__505("name");
                Query q__1168 = S0::SrcGlobal.From(t___9155).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9156)).Distinct();
                bool t___9162 = q__1168.ToSql().ToString() == "SELECT DISTINCT name FROM users";
                string fn__9154()
                {
                    return "distinct";
                }
                test___105.Assert(t___9162, (S1::Func<string>) fn__9154);
            }
            finally
            {
                test___105.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void distinctWithWhere__1737()
        {
            T::Test test___106 = new T::Test();
            try
            {
                ISafeIdentifier t___9140 = sid__505("users");
                ISafeIdentifier t___9141 = sid__505("email");
                Query t___9142 = S0::SrcGlobal.From(t___9140).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9141));
                SqlBuilder t___9143 = new SqlBuilder();
                t___9143.AppendSafe("active = ");
                t___9143.AppendBoolean(true);
                Query q__1170 = t___9142.Where(t___9143.Accumulated).Distinct();
                bool t___9152 = q__1170.ToSql().ToString() == "SELECT DISTINCT email FROM users WHERE active = TRUE";
                string fn__9139()
                {
                    return "distinct with where";
                }
                test___106.Assert(t___9152, (S1::Func<string>) fn__9139);
            }
            finally
            {
                test___106.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlBare__1739()
        {
            T::Test test___107 = new T::Test();
            try
            {
                Query q__1172 = S0::SrcGlobal.From(sid__505("users"));
                bool t___9137 = q__1172.CountSql().ToString() == "SELECT COUNT(*) FROM users";
                string fn__9132()
                {
                    return "countSql bare";
                }
                test___107.Assert(t___9137, (S1::Func<string>) fn__9132);
            }
            finally
            {
                test___107.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithWhere__1740()
        {
            T::Test test___108 = new T::Test();
            try
            {
                ISafeIdentifier t___9121 = sid__505("users");
                SqlBuilder t___9122 = new SqlBuilder();
                t___9122.AppendSafe("active = ");
                t___9122.AppendBoolean(true);
                SqlFragment t___9125 = t___9122.Accumulated;
                Query q__1174 = S0::SrcGlobal.From(t___9121).Where(t___9125);
                bool t___9130 = q__1174.CountSql().ToString() == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__9120()
                {
                    return "countSql with where";
                }
                test___108.Assert(t___9130, (S1::Func<string>) fn__9120);
            }
            finally
            {
                test___108.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlWithJoin__1742()
        {
            T::Test test___109 = new T::Test();
            try
            {
                ISafeIdentifier t___9104 = sid__505("users");
                ISafeIdentifier t___9105 = sid__505("orders");
                SqlBuilder t___9106 = new SqlBuilder();
                t___9106.AppendSafe("users.id = orders.user_id");
                SqlFragment t___9108 = t___9106.Accumulated;
                Query t___9109 = S0::SrcGlobal.From(t___9104).InnerJoin(t___9105, t___9108);
                SqlBuilder t___9110 = new SqlBuilder();
                t___9110.AppendSafe("orders.total > ");
                t___9110.AppendInt32(100);
                Query q__1176 = t___9109.Where(t___9110.Accumulated);
                bool t___9118 = q__1176.CountSql().ToString() == "SELECT COUNT(*) FROM users INNER JOIN orders ON users.id = orders.user_id WHERE orders.total > 100";
                string fn__9103()
                {
                    return "countSql with join";
                }
                test___109.Assert(t___9118, (S1::Func<string>) fn__9103);
            }
            finally
            {
                test___109.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void countSqlDropsOrderByLimitOffset__1745()
        {
            T::Test test___110 = new T::Test();
            try
            {
                ISafeIdentifier t___9090 = sid__505("users");
                SqlBuilder t___9091 = new SqlBuilder();
                t___9091.AppendSafe("active = ");
                t___9091.AppendBoolean(true);
                SqlFragment t___9094 = t___9091.Accumulated;
                Query t___4621;
                t___4621 = S0::SrcGlobal.From(t___9090).Where(t___9094).OrderBy(sid__505("name"), true).Limit(10);
                Query t___4622;
                t___4622 = t___4621.Offset(20);
                Query q__1178 = t___4622;
                string s__1179 = q__1178.CountSql().ToString();
                bool t___9101 = s__1179 == "SELECT COUNT(*) FROM users WHERE active = TRUE";
                string fn__9089()
                {
                    return "countSql drops extras: " + s__1179;
                }
                test___110.Assert(t___9101, (S1::Func<string>) fn__9089);
            }
            finally
            {
                test___110.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fullAggregationQuery__1747()
        {
            T::Test test___111 = new T::Test();
            try
            {
                ISafeIdentifier t___9057 = sid__505("orders");
                SqlFragment t___9060 = S0::SrcGlobal.Col(sid__505("orders"), sid__505("status"));
                SqlFragment t___9061 = S0::SrcGlobal.CountAll();
                SqlFragment t___9063 = S0::SrcGlobal.SumCol(sid__505("total"));
                Query t___9064 = S0::SrcGlobal.From(t___9057).SelectExpr(C::Listed.CreateReadOnlyList<SqlFragment>(t___9060, t___9061, t___9063));
                ISafeIdentifier t___9065 = sid__505("users");
                SqlBuilder t___9066 = new SqlBuilder();
                t___9066.AppendSafe("orders.user_id = users.id");
                Query t___9069 = t___9064.InnerJoin(t___9065, t___9066.Accumulated);
                SqlBuilder t___9070 = new SqlBuilder();
                t___9070.AppendSafe("users.active = ");
                t___9070.AppendBoolean(true);
                Query t___9076 = t___9069.Where(t___9070.Accumulated).GroupBy(sid__505("status"));
                SqlBuilder t___9077 = new SqlBuilder();
                t___9077.AppendSafe("COUNT(*) > ");
                t___9077.AppendInt32(3);
                Query q__1181 = t___9076.Having(t___9077.Accumulated).OrderBy(sid__505("status"), true);
                string expected__1182 = "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                bool t___9087 = q__1181.ToSql().ToString() == "SELECT orders.status, COUNT(*), SUM(total) FROM orders INNER JOIN users ON orders.user_id = users.id WHERE users.active = TRUE GROUP BY status HAVING COUNT(*) > 3 ORDER BY status ASC";
                string fn__9056()
                {
                    return "full aggregation";
                }
                test___111.Assert(t___9087, (S1::Func<string>) fn__9056);
            }
            finally
            {
                test___111.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionSql__1751()
        {
            T::Test test___112 = new T::Test();
            try
            {
                ISafeIdentifier t___9039 = sid__505("users");
                SqlBuilder t___9040 = new SqlBuilder();
                t___9040.AppendSafe("role = ");
                t___9040.AppendString("admin");
                SqlFragment t___9043 = t___9040.Accumulated;
                Query a__1184 = S0::SrcGlobal.From(t___9039).Where(t___9043);
                ISafeIdentifier t___9045 = sid__505("users");
                SqlBuilder t___9046 = new SqlBuilder();
                t___9046.AppendSafe("role = ");
                t___9046.AppendString("moderator");
                SqlFragment t___9049 = t___9046.Accumulated;
                Query b__1185 = S0::SrcGlobal.From(t___9045).Where(t___9049);
                string s__1186 = S0::SrcGlobal.UnionSql(a__1184, b__1185).ToString();
                bool t___9054 = s__1186 == "(SELECT * FROM users WHERE role = 'admin') UNION (SELECT * FROM users WHERE role = 'moderator')";
                string fn__9038()
                {
                    return "unionSql: " + s__1186;
                }
                test___112.Assert(t___9054, (S1::Func<string>) fn__9038);
            }
            finally
            {
                test___112.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void unionAllSql__1754()
        {
            T::Test test___113 = new T::Test();
            try
            {
                ISafeIdentifier t___9027 = sid__505("users");
                ISafeIdentifier t___9028 = sid__505("name");
                Query a__1188 = S0::SrcGlobal.From(t___9027).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9028));
                ISafeIdentifier t___9030 = sid__505("contacts");
                ISafeIdentifier t___9031 = sid__505("name");
                Query b__1189 = S0::SrcGlobal.From(t___9030).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9031));
                string s__1190 = S0::SrcGlobal.UnionAllSql(a__1188, b__1189).ToString();
                bool t___9036 = s__1190 == "(SELECT name FROM users) UNION ALL (SELECT name FROM contacts)";
                string fn__9026()
                {
                    return "unionAllSql: " + s__1190;
                }
                test___113.Assert(t___9036, (S1::Func<string>) fn__9026);
            }
            finally
            {
                test___113.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void intersectSql__1755()
        {
            T::Test test___114 = new T::Test();
            try
            {
                ISafeIdentifier t___9015 = sid__505("users");
                ISafeIdentifier t___9016 = sid__505("email");
                Query a__1192 = S0::SrcGlobal.From(t___9015).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9016));
                ISafeIdentifier t___9018 = sid__505("subscribers");
                ISafeIdentifier t___9019 = sid__505("email");
                Query b__1193 = S0::SrcGlobal.From(t___9018).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9019));
                string s__1194 = S0::SrcGlobal.IntersectSql(a__1192, b__1193).ToString();
                bool t___9024 = s__1194 == "(SELECT email FROM users) INTERSECT (SELECT email FROM subscribers)";
                string fn__9014()
                {
                    return "intersectSql: " + s__1194;
                }
                test___114.Assert(t___9024, (S1::Func<string>) fn__9014);
            }
            finally
            {
                test___114.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void exceptSql__1756()
        {
            T::Test test___115 = new T::Test();
            try
            {
                ISafeIdentifier t___9003 = sid__505("users");
                ISafeIdentifier t___9004 = sid__505("id");
                Query a__1196 = S0::SrcGlobal.From(t___9003).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9004));
                ISafeIdentifier t___9006 = sid__505("banned");
                ISafeIdentifier t___9007 = sid__505("id");
                Query b__1197 = S0::SrcGlobal.From(t___9006).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___9007));
                string s__1198 = S0::SrcGlobal.ExceptSql(a__1196, b__1197).ToString();
                bool t___9012 = s__1198 == "(SELECT id FROM users) EXCEPT (SELECT id FROM banned)";
                string fn__9002()
                {
                    return "exceptSql: " + s__1198;
                }
                test___115.Assert(t___9012, (S1::Func<string>) fn__9002);
            }
            finally
            {
                test___115.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void subqueryWithAlias__1757()
        {
            T::Test test___116 = new T::Test();
            try
            {
                ISafeIdentifier t___8988 = sid__505("orders");
                ISafeIdentifier t___8989 = sid__505("user_id");
                Query t___8990 = S0::SrcGlobal.From(t___8988).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8989));
                SqlBuilder t___8991 = new SqlBuilder();
                t___8991.AppendSafe("total > ");
                t___8991.AppendInt32(100);
                Query inner__1200 = t___8990.Where(t___8991.Accumulated);
                string s__1201 = S0::SrcGlobal.Subquery(inner__1200, sid__505("big_orders")).ToString();
                bool t___9000 = s__1201 == "(SELECT user_id FROM orders WHERE total > 100) AS big_orders";
                string fn__8987()
                {
                    return "subquery: " + s__1201;
                }
                test___116.Assert(t___9000, (S1::Func<string>) fn__8987);
            }
            finally
            {
                test___116.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSql__1759()
        {
            T::Test test___117 = new T::Test();
            try
            {
                ISafeIdentifier t___8977 = sid__505("orders");
                SqlBuilder t___8978 = new SqlBuilder();
                t___8978.AppendSafe("orders.user_id = users.id");
                SqlFragment t___8980 = t___8978.Accumulated;
                Query inner__1203 = S0::SrcGlobal.From(t___8977).Where(t___8980);
                string s__1204 = S0::SrcGlobal.ExistsSql(inner__1203).ToString();
                bool t___8985 = s__1204 == "EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__8976()
                {
                    return "existsSql: " + s__1204;
                }
                test___117.Assert(t___8985, (S1::Func<string>) fn__8976);
            }
            finally
            {
                test___117.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubquery__1761()
        {
            T::Test test___118 = new T::Test();
            try
            {
                ISafeIdentifier t___8960 = sid__505("orders");
                ISafeIdentifier t___8961 = sid__505("user_id");
                Query t___8962 = S0::SrcGlobal.From(t___8960).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8961));
                SqlBuilder t___8963 = new SqlBuilder();
                t___8963.AppendSafe("total > ");
                t___8963.AppendInt32(1000);
                Query sub__1206 = t___8962.Where(t___8963.Accumulated);
                ISafeIdentifier t___8968 = sid__505("users");
                ISafeIdentifier t___8969 = sid__505("id");
                Query q__1207 = S0::SrcGlobal.From(t___8968).WhereInSubquery(t___8969, sub__1206);
                string s__1208 = q__1207.ToSql().ToString();
                bool t___8974 = s__1208 == "SELECT * FROM users WHERE id IN (SELECT user_id FROM orders WHERE total > 1000)";
                string fn__8959()
                {
                    return "whereInSubquery: " + s__1208;
                }
                test___118.Assert(t___8974, (S1::Func<string>) fn__8959);
            }
            finally
            {
                test___118.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void setOperationWithWhereOnEachSide__1763()
        {
            T::Test test___119 = new T::Test();
            try
            {
                ISafeIdentifier t___8937 = sid__505("users");
                SqlBuilder t___8938 = new SqlBuilder();
                t___8938.AppendSafe("age > ");
                t___8938.AppendInt32(18);
                SqlFragment t___8941 = t___8938.Accumulated;
                Query t___8942 = S0::SrcGlobal.From(t___8937).Where(t___8941);
                SqlBuilder t___8943 = new SqlBuilder();
                t___8943.AppendSafe("active = ");
                t___8943.AppendBoolean(true);
                Query a__1210 = t___8942.Where(t___8943.Accumulated);
                ISafeIdentifier t___8948 = sid__505("users");
                SqlBuilder t___8949 = new SqlBuilder();
                t___8949.AppendSafe("role = ");
                t___8949.AppendString("vip");
                SqlFragment t___8952 = t___8949.Accumulated;
                Query b__1211 = S0::SrcGlobal.From(t___8948).Where(t___8952);
                string s__1212 = S0::SrcGlobal.UnionSql(a__1210, b__1211).ToString();
                bool t___8957 = s__1212 == "(SELECT * FROM users WHERE age > 18 AND active = TRUE) UNION (SELECT * FROM users WHERE role = 'vip')";
                string fn__8936()
                {
                    return "union with where: " + s__1212;
                }
                test___119.Assert(t___8957, (S1::Func<string>) fn__8936);
            }
            finally
            {
                test___119.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereInSubqueryChainedWithWhere__1767()
        {
            T::Test test___120 = new T::Test();
            try
            {
                ISafeIdentifier t___8920 = sid__505("orders");
                ISafeIdentifier t___8921 = sid__505("user_id");
                Query sub__1214 = S0::SrcGlobal.From(t___8920).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___8921));
                ISafeIdentifier t___8923 = sid__505("users");
                SqlBuilder t___8924 = new SqlBuilder();
                t___8924.AppendSafe("active = ");
                t___8924.AppendBoolean(true);
                SqlFragment t___8927 = t___8924.Accumulated;
                Query q__1215 = S0::SrcGlobal.From(t___8923).Where(t___8927).WhereInSubquery(sid__505("id"), sub__1214);
                string s__1216 = q__1215.ToSql().ToString();
                bool t___8934 = s__1216 == "SELECT * FROM users WHERE active = TRUE AND id IN (SELECT user_id FROM orders)";
                string fn__8919()
                {
                    return "whereInSubquery chained: " + s__1216;
                }
                test___120.Assert(t___8934, (S1::Func<string>) fn__8919);
            }
            finally
            {
                test___120.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void existsSqlUsedInWhere__1769()
        {
            T::Test test___121 = new T::Test();
            try
            {
                ISafeIdentifier t___8906 = sid__505("orders");
                SqlBuilder t___8907 = new SqlBuilder();
                t___8907.AppendSafe("orders.user_id = users.id");
                SqlFragment t___8909 = t___8907.Accumulated;
                Query sub__1218 = S0::SrcGlobal.From(t___8906).Where(t___8909);
                ISafeIdentifier t___8911 = sid__505("users");
                SqlFragment t___8912 = S0::SrcGlobal.ExistsSql(sub__1218);
                Query q__1219 = S0::SrcGlobal.From(t___8911).Where(t___8912);
                string s__1220 = q__1219.ToSql().ToString();
                bool t___8917 = s__1220 == "SELECT * FROM users WHERE EXISTS (SELECT * FROM orders WHERE orders.user_id = users.id)";
                string fn__8905()
                {
                    return "exists in where: " + s__1220;
                }
                test___121.Assert(t___8917, (S1::Func<string>) fn__8905);
            }
            finally
            {
                test___121.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBasic__1771()
        {
            T::Test test___122 = new T::Test();
            try
            {
                ISafeIdentifier t___8892 = sid__505("users");
                ISafeIdentifier t___8893 = sid__505("name");
                SqlString t___8894 = new SqlString("Alice");
                UpdateQuery t___8895 = S0::SrcGlobal.Update(t___8892).Set(t___8893, t___8894);
                SqlBuilder t___8896 = new SqlBuilder();
                t___8896.AppendSafe("id = ");
                t___8896.AppendInt32(1);
                SqlFragment t___4443;
                t___4443 = t___8895.Where(t___8896.Accumulated).ToSql();
                SqlFragment q__1222 = t___4443;
                bool t___8903 = q__1222.ToString() == "UPDATE users SET name = 'Alice' WHERE id = 1";
                string fn__8891()
                {
                    return "update basic";
                }
                test___122.Assert(t___8903, (S1::Func<string>) fn__8891);
            }
            finally
            {
                test___122.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleSet__1773()
        {
            T::Test test___123 = new T::Test();
            try
            {
                ISafeIdentifier t___8875 = sid__505("users");
                ISafeIdentifier t___8876 = sid__505("name");
                SqlString t___8877 = new SqlString("Bob");
                UpdateQuery t___8881 = S0::SrcGlobal.Update(t___8875).Set(t___8876, t___8877).Set(sid__505("age"), new SqlInt32(30));
                SqlBuilder t___8882 = new SqlBuilder();
                t___8882.AppendSafe("id = ");
                t___8882.AppendInt32(2);
                SqlFragment t___4428;
                t___4428 = t___8881.Where(t___8882.Accumulated).ToSql();
                SqlFragment q__1224 = t___4428;
                bool t___8889 = q__1224.ToString() == "UPDATE users SET name = 'Bob', age = 30 WHERE id = 2";
                string fn__8874()
                {
                    return "update multi set";
                }
                test___123.Assert(t___8889, (S1::Func<string>) fn__8874);
            }
            finally
            {
                test___123.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryMultipleWhere__1775()
        {
            T::Test test___124 = new T::Test();
            try
            {
                ISafeIdentifier t___8856 = sid__505("users");
                ISafeIdentifier t___8857 = sid__505("active");
                SqlBoolean t___8858 = new SqlBoolean(false);
                UpdateQuery t___8859 = S0::SrcGlobal.Update(t___8856).Set(t___8857, t___8858);
                SqlBuilder t___8860 = new SqlBuilder();
                t___8860.AppendSafe("age < ");
                t___8860.AppendInt32(18);
                UpdateQuery t___8864 = t___8859.Where(t___8860.Accumulated);
                SqlBuilder t___8865 = new SqlBuilder();
                t___8865.AppendSafe("role = ");
                t___8865.AppendString("guest");
                SqlFragment t___4410;
                t___4410 = t___8864.Where(t___8865.Accumulated).ToSql();
                SqlFragment q__1226 = t___4410;
                bool t___8872 = q__1226.ToString() == "UPDATE users SET active = FALSE WHERE age < 18 AND role = 'guest'";
                string fn__8855()
                {
                    return "update multi where";
                }
                test___124.Assert(t___8872, (S1::Func<string>) fn__8855);
            }
            finally
            {
                test___124.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryOrWhere__1778()
        {
            T::Test test___125 = new T::Test();
            try
            {
                ISafeIdentifier t___8837 = sid__505("users");
                ISafeIdentifier t___8838 = sid__505("status");
                SqlString t___8839 = new SqlString("banned");
                UpdateQuery t___8840 = S0::SrcGlobal.Update(t___8837).Set(t___8838, t___8839);
                SqlBuilder t___8841 = new SqlBuilder();
                t___8841.AppendSafe("spam_count > ");
                t___8841.AppendInt32(10);
                UpdateQuery t___8845 = t___8840.Where(t___8841.Accumulated);
                SqlBuilder t___8846 = new SqlBuilder();
                t___8846.AppendSafe("reported = ");
                t___8846.AppendBoolean(true);
                SqlFragment t___4389;
                t___4389 = t___8845.OrWhere(t___8846.Accumulated).ToSql();
                SqlFragment q__1228 = t___4389;
                bool t___8853 = q__1228.ToString() == "UPDATE users SET status = 'banned' WHERE spam_count > 10 OR reported = TRUE";
                string fn__8836()
                {
                    return "update orWhere";
                }
                test___125.Assert(t___8853, (S1::Func<string>) fn__8836);
            }
            finally
            {
                test___125.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutWhere__1781()
        {
            T::Test test___126 = new T::Test();
            try
            {
                ISafeIdentifier t___8830;
                ISafeIdentifier t___8831;
                SqlInt32 t___8832;
                bool didBubble__1230;
                try
                {
                    t___8830 = sid__505("users");
                    t___8831 = sid__505("x");
                    t___8832 = new SqlInt32(1);
                    S0::SrcGlobal.Update(t___8830).Set(t___8831, t___8832).ToSql();
                    didBubble__1230 = false;
                }
                catch
                {
                    didBubble__1230 = true;
                }
                string fn__8829()
                {
                    return "update without WHERE should bubble";
                }
                test___126.Assert(didBubble__1230, (S1::Func<string>) fn__8829);
            }
            finally
            {
                test___126.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryBubblesWithoutSet__1782()
        {
            T::Test test___127 = new T::Test();
            try
            {
                ISafeIdentifier t___8821;
                SqlBuilder t___8822;
                SqlFragment t___8825;
                bool didBubble__1232;
                try
                {
                    t___8821 = sid__505("users");
                    t___8822 = new SqlBuilder();
                    t___8822.AppendSafe("id = ");
                    t___8822.AppendInt32(1);
                    t___8825 = t___8822.Accumulated;
                    S0::SrcGlobal.Update(t___8821).Where(t___8825).ToSql();
                    didBubble__1232 = false;
                }
                catch
                {
                    didBubble__1232 = true;
                }
                string fn__8820()
                {
                    return "update without SET should bubble";
                }
                test___127.Assert(didBubble__1232, (S1::Func<string>) fn__8820);
            }
            finally
            {
                test___127.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryWithLimit__1784()
        {
            T::Test test___128 = new T::Test();
            try
            {
                ISafeIdentifier t___8807 = sid__505("users");
                ISafeIdentifier t___8808 = sid__505("active");
                SqlBoolean t___8809 = new SqlBoolean(false);
                UpdateQuery t___8810 = S0::SrcGlobal.Update(t___8807).Set(t___8808, t___8809);
                SqlBuilder t___8811 = new SqlBuilder();
                t___8811.AppendSafe("last_login < ");
                t___8811.AppendString("2024-01-01");
                UpdateQuery t___4352;
                t___4352 = t___8810.Where(t___8811.Accumulated).Limit(100);
                SqlFragment t___4353;
                t___4353 = t___4352.ToSql();
                SqlFragment q__1234 = t___4353;
                bool t___8818 = q__1234.ToString() == "UPDATE users SET active = FALSE WHERE last_login < '2024-01-01' LIMIT 100";
                string fn__8806()
                {
                    return "update limit";
                }
                test___128.Assert(t___8818, (S1::Func<string>) fn__8806);
            }
            finally
            {
                test___128.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void updateQueryEscaping__1786()
        {
            T::Test test___129 = new T::Test();
            try
            {
                ISafeIdentifier t___8793 = sid__505("users");
                ISafeIdentifier t___8794 = sid__505("bio");
                SqlString t___8795 = new SqlString("It's a test");
                UpdateQuery t___8796 = S0::SrcGlobal.Update(t___8793).Set(t___8794, t___8795);
                SqlBuilder t___8797 = new SqlBuilder();
                t___8797.AppendSafe("id = ");
                t___8797.AppendInt32(1);
                SqlFragment t___4337;
                t___4337 = t___8796.Where(t___8797.Accumulated).ToSql();
                SqlFragment q__1236 = t___4337;
                bool t___8804 = q__1236.ToString() == "UPDATE users SET bio = 'It''s a test' WHERE id = 1";
                string fn__8792()
                {
                    return "update escaping";
                }
                test___129.Assert(t___8804, (S1::Func<string>) fn__8792);
            }
            finally
            {
                test___129.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBasic__1788()
        {
            T::Test test___130 = new T::Test();
            try
            {
                ISafeIdentifier t___8782 = sid__505("users");
                SqlBuilder t___8783 = new SqlBuilder();
                t___8783.AppendSafe("id = ");
                t___8783.AppendInt32(1);
                SqlFragment t___8786 = t___8783.Accumulated;
                SqlFragment t___4322;
                t___4322 = S0::SrcGlobal.DeleteFrom(t___8782).Where(t___8786).ToSql();
                SqlFragment q__1238 = t___4322;
                bool t___8790 = q__1238.ToString() == "DELETE FROM users WHERE id = 1";
                string fn__8781()
                {
                    return "delete basic";
                }
                test___130.Assert(t___8790, (S1::Func<string>) fn__8781);
            }
            finally
            {
                test___130.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryMultipleWhere__1790()
        {
            T::Test test___131 = new T::Test();
            try
            {
                ISafeIdentifier t___8766 = sid__505("logs");
                SqlBuilder t___8767 = new SqlBuilder();
                t___8767.AppendSafe("created_at < ");
                t___8767.AppendString("2024-01-01");
                SqlFragment t___8770 = t___8767.Accumulated;
                DeleteQuery t___8771 = S0::SrcGlobal.DeleteFrom(t___8766).Where(t___8770);
                SqlBuilder t___8772 = new SqlBuilder();
                t___8772.AppendSafe("level = ");
                t___8772.AppendString("debug");
                SqlFragment t___4310;
                t___4310 = t___8771.Where(t___8772.Accumulated).ToSql();
                SqlFragment q__1240 = t___4310;
                bool t___8779 = q__1240.ToString() == "DELETE FROM logs WHERE created_at < '2024-01-01' AND level = 'debug'";
                string fn__8765()
                {
                    return "delete multi where";
                }
                test___131.Assert(t___8779, (S1::Func<string>) fn__8765);
            }
            finally
            {
                test___131.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryBubblesWithoutWhere__1793()
        {
            T::Test test___132 = new T::Test();
            try
            {
                bool didBubble__1242;
                try
                {
                    S0::SrcGlobal.DeleteFrom(sid__505("users")).ToSql();
                    didBubble__1242 = false;
                }
                catch
                {
                    didBubble__1242 = true;
                }
                string fn__8761()
                {
                    return "delete without WHERE should bubble";
                }
                test___132.Assert(didBubble__1242, (S1::Func<string>) fn__8761);
            }
            finally
            {
                test___132.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryOrWhere__1794()
        {
            T::Test test___133 = new T::Test();
            try
            {
                ISafeIdentifier t___8746 = sid__505("sessions");
                SqlBuilder t___8747 = new SqlBuilder();
                t___8747.AppendSafe("expired = ");
                t___8747.AppendBoolean(true);
                SqlFragment t___8750 = t___8747.Accumulated;
                DeleteQuery t___8751 = S0::SrcGlobal.DeleteFrom(t___8746).Where(t___8750);
                SqlBuilder t___8752 = new SqlBuilder();
                t___8752.AppendSafe("created_at < ");
                t___8752.AppendString("2023-01-01");
                SqlFragment t___4289;
                t___4289 = t___8751.OrWhere(t___8752.Accumulated).ToSql();
                SqlFragment q__1244 = t___4289;
                bool t___8759 = q__1244.ToString() == "DELETE FROM sessions WHERE expired = TRUE OR created_at < '2023-01-01'";
                string fn__8745()
                {
                    return "delete orWhere";
                }
                test___133.Assert(t___8759, (S1::Func<string>) fn__8745);
            }
            finally
            {
                test___133.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void deleteQueryWithLimit__1797()
        {
            T::Test test___134 = new T::Test();
            try
            {
                ISafeIdentifier t___8735 = sid__505("logs");
                SqlBuilder t___8736 = new SqlBuilder();
                t___8736.AppendSafe("level = ");
                t___8736.AppendString("debug");
                SqlFragment t___8739 = t___8736.Accumulated;
                DeleteQuery t___4270;
                t___4270 = S0::SrcGlobal.DeleteFrom(t___8735).Where(t___8739).Limit(1000);
                SqlFragment t___4271;
                t___4271 = t___4270.ToSql();
                SqlFragment q__1246 = t___4271;
                bool t___8743 = q__1246.ToString() == "DELETE FROM logs WHERE level = 'debug' LIMIT 1000";
                string fn__8734()
                {
                    return "delete limit";
                }
                test___134.Assert(t___8743, (S1::Func<string>) fn__8734);
            }
            finally
            {
                test___134.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__1799()
        {
            T::Test test___141 = new T::Test();
            try
            {
                ISafeIdentifier t___4259;
                t___4259 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__1284 = t___4259;
                bool t___8732 = id__1284.SqlValue == "user_name";
                string fn__8729()
                {
                    return "value should round-trip";
                }
                test___141.Assert(t___8732, (S1::Func<string>) fn__8729);
            }
            finally
            {
                test___141.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__1800()
        {
            T::Test test___142 = new T::Test();
            try
            {
                bool didBubble__1286;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__1286 = false;
                }
                catch
                {
                    didBubble__1286 = true;
                }
                string fn__8726()
                {
                    return "empty string should bubble";
                }
                test___142.Assert(didBubble__1286, (S1::Func<string>) fn__8726);
            }
            finally
            {
                test___142.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__1801()
        {
            T::Test test___143 = new T::Test();
            try
            {
                bool didBubble__1288;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__1288 = false;
                }
                catch
                {
                    didBubble__1288 = true;
                }
                string fn__8723()
                {
                    return "leading digit should bubble";
                }
                test___143.Assert(didBubble__1288, (S1::Func<string>) fn__8723);
            }
            finally
            {
                test___143.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__1802()
        {
            T::Test test___144 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__1290 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__8720(string c__1291)
                {
                    bool didBubble__1292;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__1291);
                        didBubble__1292 = false;
                    }
                    catch
                    {
                        didBubble__1292 = true;
                    }
                    string fn__8717()
                    {
                        return "should reject: " + c__1291;
                    }
                    test___144.Assert(didBubble__1292, (S1::Func<string>) fn__8717);
                }
                C::Listed.ForEach(cases__1290, (S1::Action<string>) fn__8720);
            }
            finally
            {
                test___144.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__1803()
        {
            T::Test test___145 = new T::Test();
            try
            {
                ISafeIdentifier t___4236;
                t___4236 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___4237 = t___4236;
                ISafeIdentifier t___4238;
                t___4238 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___4239 = t___4238;
                StringField t___8707 = new StringField();
                FieldDef t___8708 = new FieldDef(t___4239, t___8707, false);
                ISafeIdentifier t___4242;
                t___4242 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___4243 = t___4242;
                IntField t___8709 = new IntField();
                FieldDef t___8710 = new FieldDef(t___4243, t___8709, false);
                TableDef td__1294 = new TableDef(t___4237, C::Listed.CreateReadOnlyList<FieldDef>(t___8708, t___8710));
                FieldDef t___4247;
                t___4247 = td__1294.Field("age");
                FieldDef f__1295 = t___4247;
                bool t___8715 = f__1295.Name.SqlValue == "age";
                string fn__8706()
                {
                    return "should find age field";
                }
                test___145.Assert(t___8715, (S1::Func<string>) fn__8706);
            }
            finally
            {
                test___145.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__1804()
        {
            T::Test test___146 = new T::Test();
            try
            {
                ISafeIdentifier t___4227;
                t___4227 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___4228 = t___4227;
                ISafeIdentifier t___4229;
                t___4229 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___4230 = t___4229;
                StringField t___8701 = new StringField();
                FieldDef t___8702 = new FieldDef(t___4230, t___8701, false);
                TableDef td__1297 = new TableDef(t___4228, C::Listed.CreateReadOnlyList<FieldDef>(t___8702));
                bool didBubble__1298;
                try
                {
                    td__1297.Field("nonexistent");
                    didBubble__1298 = false;
                }
                catch
                {
                    didBubble__1298 = true;
                }
                string fn__8700()
                {
                    return "unknown field should bubble";
                }
                test___146.Assert(didBubble__1298, (S1::Func<string>) fn__8700);
            }
            finally
            {
                test___146.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__1805()
        {
            T::Test test___147 = new T::Test();
            try
            {
                ISafeIdentifier t___4215;
                t___4215 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___4216 = t___4215;
                StringField t___8689 = new StringField();
                FieldDef required__1300 = new FieldDef(t___4216, t___8689, false);
                ISafeIdentifier t___4219;
                t___4219 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___4220 = t___4219;
                StringField t___8691 = new StringField();
                FieldDef optional__1301 = new FieldDef(t___4220, t___8691, true);
                bool t___8695 = !required__1300.Nullable;
                string fn__8688()
                {
                    return "required field should not be nullable";
                }
                test___147.Assert(t___8695, (S1::Func<string>) fn__8688);
                bool t___8697 = optional__1301.Nullable;
                string fn__8687()
                {
                    return "optional field should be nullable";
                }
                test___147.Assert(t___8697, (S1::Func<string>) fn__8687);
            }
            finally
            {
                test___147.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__1806()
        {
            T::Test test___151 = new T::Test();
            try
            {
                string build__1427(string name__1429)
                {
                    SqlBuilder t___8669 = new SqlBuilder();
                    t___8669.AppendSafe("select * from hi where name = ");
                    t___8669.AppendString(name__1429);
                    return t___8669.Accumulated.ToString();
                }
                string buildWrong__1428(string name__1431)
                {
                    return "select * from hi where name = '" + name__1431 + "'";
                }
                string actual___1808 = build__1427("world");
                bool t___8679 = actual___1808 == "select * from hi where name = 'world'";
                string fn__8676()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___1808 + ")";
                }
                test___151.Assert(t___8679, (S1::Func<string>) fn__8676);
                string bobbyTables__1433 = "Robert'); drop table hi;--";
                string actual___1810 = build__1427("Robert'); drop table hi;--");
                bool t___8683 = actual___1810 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__8675()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___1810 + ")";
                }
                test___151.Assert(t___8683, (S1::Func<string>) fn__8675);
                string fn__8674()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___151.Assert(true, (S1::Func<string>) fn__8674);
            }
            finally
            {
                test___151.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__1814()
        {
            T::Test test___152 = new T::Test();
            try
            {
                SqlBuilder t___8637 = new SqlBuilder();
                t___8637.AppendSafe("v = ");
                t___8637.AppendString("");
                string actual___1815 = t___8637.Accumulated.ToString();
                bool t___8643 = actual___1815 == "v = ''";
                string fn__8636()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___1815 + ")";
                }
                test___152.Assert(t___8643, (S1::Func<string>) fn__8636);
                SqlBuilder t___8645 = new SqlBuilder();
                t___8645.AppendSafe("v = ");
                t___8645.AppendString("a''b");
                string actual___1818 = t___8645.Accumulated.ToString();
                bool t___8651 = actual___1818 == "v = 'a''''b'";
                string fn__8635()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___1818 + ")";
                }
                test___152.Assert(t___8651, (S1::Func<string>) fn__8635);
                SqlBuilder t___8653 = new SqlBuilder();
                t___8653.AppendSafe("v = ");
                t___8653.AppendString("Hello 世界");
                string actual___1821 = t___8653.Accumulated.ToString();
                bool t___8659 = actual___1821 == "v = 'Hello 世界'";
                string fn__8634()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1821 + ")";
                }
                test___152.Assert(t___8659, (S1::Func<string>) fn__8634);
                SqlBuilder t___8661 = new SqlBuilder();
                t___8661.AppendSafe("v = ");
                t___8661.AppendString("Line1\nLine2");
                string actual___1824 = t___8661.Accumulated.ToString();
                bool t___8667 = actual___1824 == "v = 'Line1\nLine2'";
                string fn__8633()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1824 + ")";
                }
                test___152.Assert(t___8667, (S1::Func<string>) fn__8633);
            }
            finally
            {
                test___152.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1827()
        {
            T::Test test___153 = new T::Test();
            try
            {
                SqlBuilder t___8608 = new SqlBuilder();
                t___8608.AppendSafe("select ");
                t___8608.AppendInt32(42);
                t___8608.AppendSafe(", ");
                t___8608.AppendInt64(43);
                t___8608.AppendSafe(", ");
                t___8608.AppendFloat64(19.99);
                t___8608.AppendSafe(", ");
                t___8608.AppendBoolean(true);
                t___8608.AppendSafe(", ");
                t___8608.AppendBoolean(false);
                string actual___1828 = t___8608.Accumulated.ToString();
                bool t___8622 = actual___1828 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__8607()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1828 + ")";
                }
                test___153.Assert(t___8622, (S1::Func<string>) fn__8607);
                S1::DateTime t___4160;
                t___4160 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__1436 = t___4160;
                SqlBuilder t___8624 = new SqlBuilder();
                t___8624.AppendSafe("insert into t values (");
                t___8624.AppendDate(date__1436);
                t___8624.AppendSafe(")");
                string actual___1831 = t___8624.Accumulated.ToString();
                bool t___8631 = actual___1831 == "insert into t values ('2024-12-25')";
                string fn__8606()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1831 + ")";
                }
                test___153.Assert(t___8631, (S1::Func<string>) fn__8606);
            }
            finally
            {
                test___153.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1834()
        {
            T::Test test___154 = new T::Test();
            try
            {
                SqlBuilder t___8552 = new SqlBuilder();
                t___8552.AppendSafe("v IN (");
                t___8552.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___8552.AppendSafe(")");
                string actual___1835 = t___8552.Accumulated.ToString();
                bool t___8559 = actual___1835 == "v IN ('a', 'b', 'c''d')";
                string fn__8551()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1835 + ")";
                }
                test___154.Assert(t___8559, (S1::Func<string>) fn__8551);
                SqlBuilder t___8561 = new SqlBuilder();
                t___8561.AppendSafe("v IN (");
                t___8561.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___8561.AppendSafe(")");
                string actual___1838 = t___8561.Accumulated.ToString();
                bool t___8568 = actual___1838 == "v IN (1, 2, 3)";
                string fn__8550()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1838 + ")";
                }
                test___154.Assert(t___8568, (S1::Func<string>) fn__8550);
                SqlBuilder t___8570 = new SqlBuilder();
                t___8570.AppendSafe("v IN (");
                t___8570.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___8570.AppendSafe(")");
                string actual___1841 = t___8570.Accumulated.ToString();
                bool t___8577 = actual___1841 == "v IN (1, 2)";
                string fn__8549()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1841 + ")";
                }
                test___154.Assert(t___8577, (S1::Func<string>) fn__8549);
                SqlBuilder t___8579 = new SqlBuilder();
                t___8579.AppendSafe("v IN (");
                t___8579.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___8579.AppendSafe(")");
                string actual___1844 = t___8579.Accumulated.ToString();
                bool t___8586 = actual___1844 == "v IN (1.0, 2.0)";
                string fn__8548()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1844 + ")";
                }
                test___154.Assert(t___8586, (S1::Func<string>) fn__8548);
                SqlBuilder t___8588 = new SqlBuilder();
                t___8588.AppendSafe("v IN (");
                t___8588.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___8588.AppendSafe(")");
                string actual___1847 = t___8588.Accumulated.ToString();
                bool t___8595 = actual___1847 == "v IN (TRUE, FALSE)";
                string fn__8547()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1847 + ")";
                }
                test___154.Assert(t___8595, (S1::Func<string>) fn__8547);
                S1::DateTime t___4132;
                t___4132 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___4133 = t___4132;
                S1::DateTime t___4134;
                t___4134 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___4135 = t___4134;
                G::IReadOnlyList<S1::DateTime> dates__1438 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___4133, t___4135);
                SqlBuilder t___8597 = new SqlBuilder();
                t___8597.AppendSafe("v IN (");
                t___8597.AppendDateList(dates__1438);
                t___8597.AppendSafe(")");
                string actual___1850 = t___8597.Accumulated.ToString();
                bool t___8604 = actual___1850 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__8546()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1850 + ")";
                }
                test___154.Assert(t___8604, (S1::Func<string>) fn__8546);
            }
            finally
            {
                test___154.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1853()
        {
            T::Test test___155 = new T::Test();
            try
            {
                double nan__1440;
                nan__1440 = 0.0 / 0.0;
                SqlBuilder t___8538 = new SqlBuilder();
                t___8538.AppendSafe("v = ");
                t___8538.AppendFloat64(nan__1440);
                string actual___1854 = t___8538.Accumulated.ToString();
                bool t___8544 = actual___1854 == "v = NULL";
                string fn__8537()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1854 + ")";
                }
                test___155.Assert(t___8544, (S1::Func<string>) fn__8537);
            }
            finally
            {
                test___155.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1857()
        {
            T::Test test___156 = new T::Test();
            try
            {
                double inf__1442;
                inf__1442 = 1.0 / 0.0;
                SqlBuilder t___8529 = new SqlBuilder();
                t___8529.AppendSafe("v = ");
                t___8529.AppendFloat64(inf__1442);
                string actual___1858 = t___8529.Accumulated.ToString();
                bool t___8535 = actual___1858 == "v = NULL";
                string fn__8528()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1858 + ")";
                }
                test___156.Assert(t___8535, (S1::Func<string>) fn__8528);
            }
            finally
            {
                test___156.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1861()
        {
            T::Test test___157 = new T::Test();
            try
            {
                double ninf__1444;
                ninf__1444 = -1.0 / 0.0;
                SqlBuilder t___8520 = new SqlBuilder();
                t___8520.AppendSafe("v = ");
                t___8520.AppendFloat64(ninf__1444);
                string actual___1862 = t___8520.Accumulated.ToString();
                bool t___8526 = actual___1862 == "v = NULL";
                string fn__8519()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1862 + ")";
                }
                test___157.Assert(t___8526, (S1::Func<string>) fn__8519);
            }
            finally
            {
                test___157.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1865()
        {
            T::Test test___158 = new T::Test();
            try
            {
                SqlBuilder t___8495 = new SqlBuilder();
                t___8495.AppendSafe("v = ");
                t___8495.AppendFloat64(3.14);
                string actual___1866 = t___8495.Accumulated.ToString();
                bool t___8501 = actual___1866 == "v = 3.14";
                string fn__8494()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1866 + ")";
                }
                test___158.Assert(t___8501, (S1::Func<string>) fn__8494);
                SqlBuilder t___8503 = new SqlBuilder();
                t___8503.AppendSafe("v = ");
                t___8503.AppendFloat64(0.0);
                string actual___1869 = t___8503.Accumulated.ToString();
                bool t___8509 = actual___1869 == "v = 0.0";
                string fn__8493()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1869 + ")";
                }
                test___158.Assert(t___8509, (S1::Func<string>) fn__8493);
                SqlBuilder t___8511 = new SqlBuilder();
                t___8511.AppendSafe("v = ");
                t___8511.AppendFloat64(-42.5);
                string actual___1872 = t___8511.Accumulated.ToString();
                bool t___8517 = actual___1872 == "v = -42.5";
                string fn__8492()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1872 + ")";
                }
                test___158.Assert(t___8517, (S1::Func<string>) fn__8492);
            }
            finally
            {
                test___158.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1875()
        {
            T::Test test___159 = new T::Test();
            try
            {
                S1::DateTime t___4028;
                t___4028 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__1447 = t___4028;
                SqlBuilder t___8484 = new SqlBuilder();
                t___8484.AppendSafe("v = ");
                t___8484.AppendDate(d__1447);
                string actual___1876 = t___8484.Accumulated.ToString();
                bool t___8490 = actual___1876 == "v = '2024-06-15'";
                string fn__8483()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1876 + ")";
                }
                test___159.Assert(t___8490, (S1::Func<string>) fn__8483);
            }
            finally
            {
                test___159.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1879()
        {
            T::Test test___160 = new T::Test();
            try
            {
                string name__1449 = "Someone";
                SqlBuilder t___8452 = new SqlBuilder();
                t___8452.AppendSafe("where p.last_name = ");
                t___8452.AppendString("Someone");
                SqlFragment condition__1450 = t___8452.Accumulated;
                SqlBuilder t___8456 = new SqlBuilder();
                t___8456.AppendSafe("select p.id from person p ");
                t___8456.AppendFragment(condition__1450);
                string actual___1881 = t___8456.Accumulated.ToString();
                bool t___8462 = actual___1881 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__8451()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1881 + ")";
                }
                test___160.Assert(t___8462, (S1::Func<string>) fn__8451);
                SqlBuilder t___8464 = new SqlBuilder();
                t___8464.AppendSafe("select p.id from person p ");
                t___8464.AppendPart(condition__1450.ToSource());
                string actual___1884 = t___8464.Accumulated.ToString();
                bool t___8471 = actual___1884 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__8450()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1884 + ")";
                }
                test___160.Assert(t___8471, (S1::Func<string>) fn__8450);
                G::IReadOnlyList<ISqlPart> parts__1451 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___8475 = new SqlBuilder();
                t___8475.AppendSafe("select ");
                t___8475.AppendPartList(parts__1451);
                string actual___1887 = t___8475.Accumulated.ToString();
                bool t___8481 = actual___1887 == "select 'a''b', 3";
                string fn__8449()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___1887 + ")";
                }
                test___160.Assert(t___8481, (S1::Func<string>) fn__8449);
            }
            finally
            {
                test___160.SoftFailToHard();
            }
        }
    }
}
