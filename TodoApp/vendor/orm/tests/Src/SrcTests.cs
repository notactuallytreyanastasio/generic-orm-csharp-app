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
        internal static ISafeIdentifier csid__302(string name__447)
        {
            ISafeIdentifier t___2783;
            t___2783 = S0::SrcGlobal.SafeIdentifier(name__447);
            return t___2783;
        }
        internal static TableDef userTable__303()
        {
            return new TableDef(csid__302("users"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__302("name"), new StringField(), false), new FieldDef(csid__302("email"), new StringField(), false), new FieldDef(csid__302("age"), new IntField(), true), new FieldDef(csid__302("score"), new FloatField(), true), new FieldDef(csid__302("active"), new BoolField(), true)));
        }
        [U::TestMethod]
        public void castWhitelistsAllowedFields__908()
        {
            T::Test test___22 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__451 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com"), new G::KeyValuePair<string, string>("admin", "true")));
                TableDef t___4828 = userTable__303();
                ISafeIdentifier t___4829 = csid__302("name");
                ISafeIdentifier t___4830 = csid__302("email");
                IChangeset cs__452 = S0::SrcGlobal.Changeset(t___4828, params__451).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4829, t___4830));
                bool t___4833 = C::Mapped.ContainsKey(cs__452.Changes, "name");
                string fn__4823()
                {
                    return "name should be in changes";
                }
                test___22.Assert(t___4833, (S1::Func<string>) fn__4823);
                bool t___4837 = C::Mapped.ContainsKey(cs__452.Changes, "email");
                string fn__4822()
                {
                    return "email should be in changes";
                }
                test___22.Assert(t___4837, (S1::Func<string>) fn__4822);
                bool t___4843 = !C::Mapped.ContainsKey(cs__452.Changes, "admin");
                string fn__4821()
                {
                    return "admin must be dropped (not in whitelist)";
                }
                test___22.Assert(t___4843, (S1::Func<string>) fn__4821);
                bool t___4845 = cs__452.IsValid;
                string fn__4820()
                {
                    return "should still be valid";
                }
                test___22.Assert(t___4845, (S1::Func<string>) fn__4820);
            }
            finally
            {
                test___22.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIsReplacingNotAdditiveSecondCallResetsWhitelist__909()
        {
            T::Test test___23 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__454 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "alice@example.com")));
                TableDef t___4806 = userTable__303();
                ISafeIdentifier t___4807 = csid__302("name");
                IChangeset cs__455 = S0::SrcGlobal.Changeset(t___4806, params__454).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4807)).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("email")));
                bool t___4814 = !C::Mapped.ContainsKey(cs__455.Changes, "name");
                string fn__4802()
                {
                    return "name must be excluded by second cast";
                }
                test___23.Assert(t___4814, (S1::Func<string>) fn__4802);
                bool t___4817 = C::Mapped.ContainsKey(cs__455.Changes, "email");
                string fn__4801()
                {
                    return "email should be present";
                }
                test___23.Assert(t___4817, (S1::Func<string>) fn__4801);
            }
            finally
            {
                test___23.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void castIgnoresEmptyStringValues__910()
        {
            T::Test test___24 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__457 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", ""), new G::KeyValuePair<string, string>("email", "bob@example.com")));
                TableDef t___4788 = userTable__303();
                ISafeIdentifier t___4789 = csid__302("name");
                ISafeIdentifier t___4790 = csid__302("email");
                IChangeset cs__458 = S0::SrcGlobal.Changeset(t___4788, params__457).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4789, t___4790));
                bool t___4795 = !C::Mapped.ContainsKey(cs__458.Changes, "name");
                string fn__4784()
                {
                    return "empty name should not be in changes";
                }
                test___24.Assert(t___4795, (S1::Func<string>) fn__4784);
                bool t___4798 = C::Mapped.ContainsKey(cs__458.Changes, "email");
                string fn__4783()
                {
                    return "email should be in changes";
                }
                test___24.Assert(t___4798, (S1::Func<string>) fn__4783);
            }
            finally
            {
                test___24.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredPassesWhenFieldPresent__911()
        {
            T::Test test___25 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__460 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___4770 = userTable__303();
                ISafeIdentifier t___4771 = csid__302("name");
                IChangeset cs__461 = S0::SrcGlobal.Changeset(t___4770, params__460).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4771)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name")));
                bool t___4775 = cs__461.IsValid;
                string fn__4767()
                {
                    return "should be valid";
                }
                test___25.Assert(t___4775, (S1::Func<string>) fn__4767);
                bool t___4781 = cs__461.Errors.Count == 0;
                string fn__4766()
                {
                    return "no errors expected";
                }
                test___25.Assert(t___4781, (S1::Func<string>) fn__4766);
            }
            finally
            {
                test___25.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateRequiredFailsWhenFieldMissing__912()
        {
            T::Test test___26 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__463 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___4746 = userTable__303();
                ISafeIdentifier t___4747 = csid__302("name");
                IChangeset cs__464 = S0::SrcGlobal.Changeset(t___4746, params__463).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4747)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name")));
                bool t___4753 = !cs__464.IsValid;
                string fn__4744()
                {
                    return "should be invalid";
                }
                test___26.Assert(t___4753, (S1::Func<string>) fn__4744);
                bool t___4758 = cs__464.Errors.Count == 1;
                string fn__4743()
                {
                    return "should have one error";
                }
                test___26.Assert(t___4758, (S1::Func<string>) fn__4743);
                bool t___4764 = cs__464.Errors[0].Field == "name";
                string fn__4742()
                {
                    return "error should name the field";
                }
                test___26.Assert(t___4764, (S1::Func<string>) fn__4742);
            }
            finally
            {
                test___26.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthPassesWithinRange__913()
        {
            T::Test test___27 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__466 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice")));
                TableDef t___4734 = userTable__303();
                ISafeIdentifier t___4735 = csid__302("name");
                IChangeset cs__467 = S0::SrcGlobal.Changeset(t___4734, params__466).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4735)).ValidateLength(csid__302("name"), 2, 50);
                bool t___4739 = cs__467.IsValid;
                string fn__4731()
                {
                    return "should be valid";
                }
                test___27.Assert(t___4739, (S1::Func<string>) fn__4731);
            }
            finally
            {
                test___27.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooShort__914()
        {
            T::Test test___28 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__469 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "A")));
                TableDef t___4722 = userTable__303();
                ISafeIdentifier t___4723 = csid__302("name");
                IChangeset cs__470 = S0::SrcGlobal.Changeset(t___4722, params__469).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4723)).ValidateLength(csid__302("name"), 2, 50);
                bool t___4729 = !cs__470.IsValid;
                string fn__4719()
                {
                    return "should be invalid";
                }
                test___28.Assert(t___4729, (S1::Func<string>) fn__4719);
            }
            finally
            {
                test___28.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateLengthFailsWhenTooLong__915()
        {
            T::Test test___29 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__472 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")));
                TableDef t___4710 = userTable__303();
                ISafeIdentifier t___4711 = csid__302("name");
                IChangeset cs__473 = S0::SrcGlobal.Changeset(t___4710, params__472).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4711)).ValidateLength(csid__302("name"), 2, 10);
                bool t___4717 = !cs__473.IsValid;
                string fn__4707()
                {
                    return "should be invalid";
                }
                test___29.Assert(t___4717, (S1::Func<string>) fn__4707);
            }
            finally
            {
                test___29.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntPassesForValidInteger__916()
        {
            T::Test test___30 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__475 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "30")));
                TableDef t___4699 = userTable__303();
                ISafeIdentifier t___4700 = csid__302("age");
                IChangeset cs__476 = S0::SrcGlobal.Changeset(t___4699, params__475).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4700)).ValidateInt(csid__302("age"));
                bool t___4704 = cs__476.IsValid;
                string fn__4696()
                {
                    return "should be valid";
                }
                test___30.Assert(t___4704, (S1::Func<string>) fn__4696);
            }
            finally
            {
                test___30.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateIntFailsForNonInteger__917()
        {
            T::Test test___31 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__478 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___4687 = userTable__303();
                ISafeIdentifier t___4688 = csid__302("age");
                IChangeset cs__479 = S0::SrcGlobal.Changeset(t___4687, params__478).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4688)).ValidateInt(csid__302("age"));
                bool t___4694 = !cs__479.IsValid;
                string fn__4684()
                {
                    return "should be invalid";
                }
                test___31.Assert(t___4694, (S1::Func<string>) fn__4684);
            }
            finally
            {
                test___31.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateFloatPassesForValidFloat__918()
        {
            T::Test test___32 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__481 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("score", "9.5")));
                TableDef t___4676 = userTable__303();
                ISafeIdentifier t___4677 = csid__302("score");
                IChangeset cs__482 = S0::SrcGlobal.Changeset(t___4676, params__481).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4677)).ValidateFloat(csid__302("score"));
                bool t___4681 = cs__482.IsValid;
                string fn__4673()
                {
                    return "should be valid";
                }
                test___32.Assert(t___4681, (S1::Func<string>) fn__4673);
            }
            finally
            {
                test___32.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_passesForValid64_bitInteger__919()
        {
            T::Test test___33 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__484 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "9999999999")));
                TableDef t___4665 = userTable__303();
                ISafeIdentifier t___4666 = csid__302("age");
                IChangeset cs__485 = S0::SrcGlobal.Changeset(t___4665, params__484).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4666)).ValidateInt64(csid__302("age"));
                bool t___4670 = cs__485.IsValid;
                string fn__4662()
                {
                    return "should be valid";
                }
                test___33.Assert(t___4670, (S1::Func<string>) fn__4662);
            }
            finally
            {
                test___33.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateInt64_failsForNonInteger__920()
        {
            T::Test test___34 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__487 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("age", "not-a-number")));
                TableDef t___4653 = userTable__303();
                ISafeIdentifier t___4654 = csid__302("age");
                IChangeset cs__488 = S0::SrcGlobal.Changeset(t___4653, params__487).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4654)).ValidateInt64(csid__302("age"));
                bool t___4660 = !cs__488.IsValid;
                string fn__4650()
                {
                    return "should be invalid";
                }
                test___34.Assert(t___4660, (S1::Func<string>) fn__4650);
            }
            finally
            {
                test___34.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsTrue1_yesOn__921()
        {
            T::Test test___35 = new T::Test();
            try
            {
                void fn__4647(string v__490)
                {
                    G::IReadOnlyDictionary<string, string> params__491 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__490)));
                    TableDef t___4639 = userTable__303();
                    ISafeIdentifier t___4640 = csid__302("active");
                    IChangeset cs__492 = S0::SrcGlobal.Changeset(t___4639, params__491).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4640)).ValidateBool(csid__302("active"));
                    bool t___4644 = cs__492.IsValid;
                    string fn__4636()
                    {
                        return "should accept: " + v__490;
                    }
                    test___35.Assert(t___4644, (S1::Func<string>) fn__4636);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("true", "1", "yes", "on"), (S1::Action<string>) fn__4647);
            }
            finally
            {
                test___35.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolAcceptsFalse0_noOff__922()
        {
            T::Test test___36 = new T::Test();
            try
            {
                void fn__4633(string v__494)
                {
                    G::IReadOnlyDictionary<string, string> params__495 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__494)));
                    TableDef t___4625 = userTable__303();
                    ISafeIdentifier t___4626 = csid__302("active");
                    IChangeset cs__496 = S0::SrcGlobal.Changeset(t___4625, params__495).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4626)).ValidateBool(csid__302("active"));
                    bool t___4630 = cs__496.IsValid;
                    string fn__4622()
                    {
                        return "should accept: " + v__494;
                    }
                    test___36.Assert(t___4630, (S1::Func<string>) fn__4622);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("false", "0", "no", "off"), (S1::Action<string>) fn__4633);
            }
            finally
            {
                test___36.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void validateBoolRejectsAmbiguousValues__923()
        {
            T::Test test___37 = new T::Test();
            try
            {
                void fn__4619(string v__498)
                {
                    G::IReadOnlyDictionary<string, string> params__499 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("active", v__498)));
                    TableDef t___4610 = userTable__303();
                    ISafeIdentifier t___4611 = csid__302("active");
                    IChangeset cs__500 = S0::SrcGlobal.Changeset(t___4610, params__499).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4611)).ValidateBool(csid__302("active"));
                    bool t___4617 = !cs__500.IsValid;
                    string fn__4607()
                    {
                        return "should reject ambiguous: " + v__498;
                    }
                    test___37.Assert(t___4617, (S1::Func<string>) fn__4607);
                }
                C::Listed.ForEach(C::Listed.CreateReadOnlyList<string>("TRUE", "Yes", "maybe", "2", "enabled"), (S1::Action<string>) fn__4619);
            }
            finally
            {
                test___37.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEscapesBobbyTables__924()
        {
            T::Test test___38 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__502 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Robert'); DROP TABLE users;--"), new G::KeyValuePair<string, string>("email", "bobby@evil.com")));
                TableDef t___4595 = userTable__303();
                ISafeIdentifier t___4596 = csid__302("name");
                ISafeIdentifier t___4597 = csid__302("email");
                IChangeset cs__503 = S0::SrcGlobal.Changeset(t___4595, params__502).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4596, t___4597)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name"), csid__302("email")));
                SqlFragment t___2584;
                t___2584 = cs__503.ToInsertSql();
                SqlFragment sqlFrag__504 = t___2584;
                string s__505 = sqlFrag__504.ToString();
                bool t___4604 = s__505.IndexOf("''") >= 0;
                string fn__4591()
                {
                    return "single quote must be doubled: " + s__505;
                }
                test___38.Assert(t___4604, (S1::Func<string>) fn__4591);
            }
            finally
            {
                test___38.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForStringField__925()
        {
            T::Test test___39 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__507 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Alice"), new G::KeyValuePair<string, string>("email", "a@example.com")));
                TableDef t___4575 = userTable__303();
                ISafeIdentifier t___4576 = csid__302("name");
                ISafeIdentifier t___4577 = csid__302("email");
                IChangeset cs__508 = S0::SrcGlobal.Changeset(t___4575, params__507).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4576, t___4577)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name"), csid__302("email")));
                SqlFragment t___2563;
                t___2563 = cs__508.ToInsertSql();
                SqlFragment sqlFrag__509 = t___2563;
                string s__510 = sqlFrag__509.ToString();
                bool t___4584 = s__510.IndexOf("INSERT INTO users") >= 0;
                string fn__4571()
                {
                    return "has INSERT INTO: " + s__510;
                }
                test___39.Assert(t___4584, (S1::Func<string>) fn__4571);
                bool t___4588 = s__510.IndexOf("'Alice'") >= 0;
                string fn__4570()
                {
                    return "has quoted name: " + s__510;
                }
                test___39.Assert(t___4588, (S1::Func<string>) fn__4570);
            }
            finally
            {
                test___39.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlProducesCorrectSqlForIntField__926()
        {
            T::Test test___40 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__512 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob"), new G::KeyValuePair<string, string>("email", "b@example.com"), new G::KeyValuePair<string, string>("age", "25")));
                TableDef t___4557 = userTable__303();
                ISafeIdentifier t___4558 = csid__302("name");
                ISafeIdentifier t___4559 = csid__302("email");
                ISafeIdentifier t___4560 = csid__302("age");
                IChangeset cs__513 = S0::SrcGlobal.Changeset(t___4557, params__512).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4558, t___4559, t___4560)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name"), csid__302("email")));
                SqlFragment t___2546;
                t___2546 = cs__513.ToInsertSql();
                SqlFragment sqlFrag__514 = t___2546;
                string s__515 = sqlFrag__514.ToString();
                bool t___4567 = s__515.IndexOf("25") >= 0;
                string fn__4552()
                {
                    return "age rendered unquoted: " + s__515;
                }
                test___40.Assert(t___4567, (S1::Func<string>) fn__4552);
            }
            finally
            {
                test___40.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlBubblesOnInvalidChangeset__927()
        {
            T::Test test___41 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__517 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___4545 = userTable__303();
                ISafeIdentifier t___4546 = csid__302("name");
                IChangeset cs__518 = S0::SrcGlobal.Changeset(t___4545, params__517).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4546)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name")));
                bool didBubble__519;
                try
                {
                    cs__518.ToInsertSql();
                    didBubble__519 = false;
                }
                catch
                {
                    didBubble__519 = true;
                }
                string fn__4543()
                {
                    return "invalid changeset should bubble";
                }
                test___41.Assert(didBubble__519, (S1::Func<string>) fn__4543);
            }
            finally
            {
                test___41.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toInsertSqlEnforcesNonNullableFieldsIndependentlyOfIsValid__928()
        {
            T::Test test___42 = new T::Test();
            try
            {
                TableDef strictTable__521 = new TableDef(csid__302("posts"), C::Listed.CreateReadOnlyList<FieldDef>(new FieldDef(csid__302("title"), new StringField(), false), new FieldDef(csid__302("body"), new StringField(), true)));
                G::IReadOnlyDictionary<string, string> params__522 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("body", "hello")));
                ISafeIdentifier t___4536 = csid__302("body");
                IChangeset cs__523 = S0::SrcGlobal.Changeset(strictTable__521, params__522).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4536));
                bool t___4538 = cs__523.IsValid;
                string fn__4525()
                {
                    return "changeset should appear valid (no explicit validation run)";
                }
                test___42.Assert(t___4538, (S1::Func<string>) fn__4525);
                bool didBubble__524;
                try
                {
                    cs__523.ToInsertSql();
                    didBubble__524 = false;
                }
                catch
                {
                    didBubble__524 = true;
                }
                string fn__4524()
                {
                    return "toInsertSql should enforce nullable regardless of isValid";
                }
                test___42.Assert(didBubble__524, (S1::Func<string>) fn__4524);
            }
            finally
            {
                test___42.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlProducesCorrectSql__929()
        {
            T::Test test___43 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__526 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>(new G::KeyValuePair<string, string>("name", "Bob")));
                TableDef t___4515 = userTable__303();
                ISafeIdentifier t___4516 = csid__302("name");
                IChangeset cs__527 = S0::SrcGlobal.Changeset(t___4515, params__526).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4516)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name")));
                SqlFragment t___2506;
                t___2506 = cs__527.ToUpdateSql(42);
                SqlFragment sqlFrag__528 = t___2506;
                string s__529 = sqlFrag__528.ToString();
                bool t___4522 = s__529 == "UPDATE users SET name = 'Bob' WHERE id = 42";
                string fn__4512()
                {
                    return "got: " + s__529;
                }
                test___43.Assert(t___4522, (S1::Func<string>) fn__4512);
            }
            finally
            {
                test___43.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void toUpdateSqlBubblesOnInvalidChangeset__930()
        {
            T::Test test___44 = new T::Test();
            try
            {
                G::IReadOnlyDictionary<string, string> params__531 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
                TableDef t___4505 = userTable__303();
                ISafeIdentifier t___4506 = csid__302("name");
                IChangeset cs__532 = S0::SrcGlobal.Changeset(t___4505, params__531).Cast(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4506)).ValidateRequired(C::Listed.CreateReadOnlyList<ISafeIdentifier>(csid__302("name")));
                bool didBubble__533;
                try
                {
                    cs__532.ToUpdateSql(1);
                    didBubble__533 = false;
                }
                catch
                {
                    didBubble__533 = true;
                }
                string fn__4503()
                {
                    return "invalid changeset should bubble";
                }
                test___44.Assert(didBubble__533, (S1::Func<string>) fn__4503);
            }
            finally
            {
                test___44.SoftFailToHard();
            }
        }
        internal static ISafeIdentifier sid__304(string name__588)
        {
            ISafeIdentifier t___2420;
            t___2420 = S0::SrcGlobal.SafeIdentifier(name__588);
            return t___2420;
        }
        [U::TestMethod]
        public void bareFromProducesSelect__955()
        {
            T::Test test___45 = new T::Test();
            try
            {
                Query q__591 = S0::SrcGlobal.From(sid__304("users"));
                bool t___4438 = q__591.ToSql().ToString() == "SELECT * FROM users";
                string fn__4433()
                {
                    return "bare query";
                }
                test___45.Assert(t___4438, (S1::Func<string>) fn__4433);
            }
            finally
            {
                test___45.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void selectRestrictsColumns__956()
        {
            T::Test test___46 = new T::Test();
            try
            {
                ISafeIdentifier t___4424 = sid__304("users");
                ISafeIdentifier t___4425 = sid__304("id");
                ISafeIdentifier t___4426 = sid__304("name");
                Query q__593 = S0::SrcGlobal.From(t___4424).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4425, t___4426));
                bool t___4431 = q__593.ToSql().ToString() == "SELECT id, name FROM users";
                string fn__4423()
                {
                    return "select columns";
                }
                test___46.Assert(t___4431, (S1::Func<string>) fn__4423);
            }
            finally
            {
                test___46.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithIntValue__957()
        {
            T::Test test___47 = new T::Test();
            try
            {
                ISafeIdentifier t___4412 = sid__304("users");
                SqlBuilder t___4413 = new SqlBuilder();
                t___4413.AppendSafe("age > ");
                t___4413.AppendInt32(18);
                SqlFragment t___4416 = t___4413.Accumulated;
                Query q__595 = S0::SrcGlobal.From(t___4412).Where(t___4416);
                bool t___4421 = q__595.ToSql().ToString() == "SELECT * FROM users WHERE age > 18";
                string fn__4411()
                {
                    return "where int";
                }
                test___47.Assert(t___4421, (S1::Func<string>) fn__4411);
            }
            finally
            {
                test___47.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereAddsConditionWithBoolValue__959()
        {
            T::Test test___48 = new T::Test();
            try
            {
                ISafeIdentifier t___4400 = sid__304("users");
                SqlBuilder t___4401 = new SqlBuilder();
                t___4401.AppendSafe("active = ");
                t___4401.AppendBoolean(true);
                SqlFragment t___4404 = t___4401.Accumulated;
                Query q__597 = S0::SrcGlobal.From(t___4400).Where(t___4404);
                bool t___4409 = q__597.ToSql().ToString() == "SELECT * FROM users WHERE active = TRUE";
                string fn__4399()
                {
                    return "where bool";
                }
                test___48.Assert(t___4409, (S1::Func<string>) fn__4399);
            }
            finally
            {
                test___48.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void chainedWhereUsesAnd__961()
        {
            T::Test test___49 = new T::Test();
            try
            {
                ISafeIdentifier t___4383 = sid__304("users");
                SqlBuilder t___4384 = new SqlBuilder();
                t___4384.AppendSafe("age > ");
                t___4384.AppendInt32(18);
                SqlFragment t___4387 = t___4384.Accumulated;
                Query t___4388 = S0::SrcGlobal.From(t___4383).Where(t___4387);
                SqlBuilder t___4389 = new SqlBuilder();
                t___4389.AppendSafe("active = ");
                t___4389.AppendBoolean(true);
                Query q__599 = t___4388.Where(t___4389.Accumulated);
                bool t___4397 = q__599.ToSql().ToString() == "SELECT * FROM users WHERE age > 18 AND active = TRUE";
                string fn__4382()
                {
                    return "chained where";
                }
                test___49.Assert(t___4397, (S1::Func<string>) fn__4382);
            }
            finally
            {
                test___49.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByAsc__964()
        {
            T::Test test___50 = new T::Test();
            try
            {
                ISafeIdentifier t___4374 = sid__304("users");
                ISafeIdentifier t___4375 = sid__304("name");
                Query q__601 = S0::SrcGlobal.From(t___4374).OrderBy(t___4375, true);
                bool t___4380 = q__601.ToSql().ToString() == "SELECT * FROM users ORDER BY name ASC";
                string fn__4373()
                {
                    return "order asc";
                }
                test___50.Assert(t___4380, (S1::Func<string>) fn__4373);
            }
            finally
            {
                test___50.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void orderByDesc__965()
        {
            T::Test test___51 = new T::Test();
            try
            {
                ISafeIdentifier t___4365 = sid__304("users");
                ISafeIdentifier t___4366 = sid__304("created_at");
                Query q__603 = S0::SrcGlobal.From(t___4365).OrderBy(t___4366, false);
                bool t___4371 = q__603.ToSql().ToString() == "SELECT * FROM users ORDER BY created_at DESC";
                string fn__4364()
                {
                    return "order desc";
                }
                test___51.Assert(t___4371, (S1::Func<string>) fn__4364);
            }
            finally
            {
                test___51.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitAndOffset__966()
        {
            T::Test test___52 = new T::Test();
            try
            {
                Query t___2354;
                t___2354 = S0::SrcGlobal.From(sid__304("users")).Limit(10);
                Query t___2355;
                t___2355 = t___2354.Offset(20);
                Query q__605 = t___2355;
                bool t___4362 = q__605.ToSql().ToString() == "SELECT * FROM users LIMIT 10 OFFSET 20";
                string fn__4357()
                {
                    return "limit/offset";
                }
                test___52.Assert(t___4362, (S1::Func<string>) fn__4357);
            }
            finally
            {
                test___52.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void limitBubblesOnNegative__967()
        {
            T::Test test___53 = new T::Test();
            try
            {
                bool didBubble__607;
                try
                {
                    S0::SrcGlobal.From(sid__304("users")).Limit(-1);
                    didBubble__607 = false;
                }
                catch
                {
                    didBubble__607 = true;
                }
                string fn__4353()
                {
                    return "negative limit should bubble";
                }
                test___53.Assert(didBubble__607, (S1::Func<string>) fn__4353);
            }
            finally
            {
                test___53.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void offsetBubblesOnNegative__968()
        {
            T::Test test___54 = new T::Test();
            try
            {
                bool didBubble__609;
                try
                {
                    S0::SrcGlobal.From(sid__304("users")).Offset(-1);
                    didBubble__609 = false;
                }
                catch
                {
                    didBubble__609 = true;
                }
                string fn__4349()
                {
                    return "negative offset should bubble";
                }
                test___54.Assert(didBubble__609, (S1::Func<string>) fn__4349);
            }
            finally
            {
                test___54.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void complexComposedQuery__969()
        {
            T::Test test___55 = new T::Test();
            try
            {
                int minAge__611 = 21;
                ISafeIdentifier t___4327 = sid__304("users");
                ISafeIdentifier t___4328 = sid__304("id");
                ISafeIdentifier t___4329 = sid__304("name");
                ISafeIdentifier t___4330 = sid__304("email");
                Query t___4331 = S0::SrcGlobal.From(t___4327).Select(C::Listed.CreateReadOnlyList<ISafeIdentifier>(t___4328, t___4329, t___4330));
                SqlBuilder t___4332 = new SqlBuilder();
                t___4332.AppendSafe("age >= ");
                t___4332.AppendInt32(21);
                Query t___4336 = t___4331.Where(t___4332.Accumulated);
                SqlBuilder t___4337 = new SqlBuilder();
                t___4337.AppendSafe("active = ");
                t___4337.AppendBoolean(true);
                Query t___2340;
                t___2340 = t___4336.Where(t___4337.Accumulated).OrderBy(sid__304("name"), true).Limit(25);
                Query t___2341;
                t___2341 = t___2340.Offset(0);
                Query q__612 = t___2341;
                bool t___4347 = q__612.ToSql().ToString() == "SELECT id, name, email FROM users WHERE age >= 21 AND active = TRUE ORDER BY name ASC LIMIT 25 OFFSET 0";
                string fn__4326()
                {
                    return "complex query";
                }
                test___55.Assert(t___4347, (S1::Func<string>) fn__4326);
            }
            finally
            {
                test___55.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlAppliesDefaultLimitWhenNoneSet__972()
        {
            T::Test test___56 = new T::Test();
            try
            {
                Query q__614 = S0::SrcGlobal.From(sid__304("users"));
                SqlFragment t___2317;
                t___2317 = q__614.SafeToSql(100);
                SqlFragment t___2318 = t___2317;
                string s__615 = t___2318.ToString();
                bool t___4324 = s__615 == "SELECT * FROM users LIMIT 100";
                string fn__4320()
                {
                    return "should have limit: " + s__615;
                }
                test___56.Assert(t___4324, (S1::Func<string>) fn__4320);
            }
            finally
            {
                test___56.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlRespectsExplicitLimit__973()
        {
            T::Test test___57 = new T::Test();
            try
            {
                Query t___2309;
                t___2309 = S0::SrcGlobal.From(sid__304("users")).Limit(5);
                Query q__617 = t___2309;
                SqlFragment t___2312;
                t___2312 = q__617.SafeToSql(100);
                SqlFragment t___2313 = t___2312;
                string s__618 = t___2313.ToString();
                bool t___4318 = s__618 == "SELECT * FROM users LIMIT 5";
                string fn__4314()
                {
                    return "explicit limit preserved: " + s__618;
                }
                test___57.Assert(t___4318, (S1::Func<string>) fn__4314);
            }
            finally
            {
                test___57.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeToSqlBubblesOnNegativeDefaultLimit__974()
        {
            T::Test test___58 = new T::Test();
            try
            {
                bool didBubble__620;
                try
                {
                    S0::SrcGlobal.From(sid__304("users")).SafeToSql(-1);
                    didBubble__620 = false;
                }
                catch
                {
                    didBubble__620 = true;
                }
                string fn__4310()
                {
                    return "negative defaultLimit should bubble";
                }
                test___58.Assert(didBubble__620, (S1::Func<string>) fn__4310);
            }
            finally
            {
                test___58.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void whereWithInjectionAttemptInStringValueIsEscaped__975()
        {
            T::Test test___59 = new T::Test();
            try
            {
                string evil__622 = "'; DROP TABLE users; --";
                ISafeIdentifier t___4294 = sid__304("users");
                SqlBuilder t___4295 = new SqlBuilder();
                t___4295.AppendSafe("name = ");
                t___4295.AppendString("'; DROP TABLE users; --");
                SqlFragment t___4298 = t___4295.Accumulated;
                Query q__623 = S0::SrcGlobal.From(t___4294).Where(t___4298);
                string s__624 = q__623.ToSql().ToString();
                bool t___4303 = s__624.IndexOf("''") >= 0;
                string fn__4293()
                {
                    return "quotes must be doubled: " + s__624;
                }
                test___59.Assert(t___4303, (S1::Func<string>) fn__4293);
                bool t___4307 = s__624.IndexOf("SELECT * FROM users WHERE name =") >= 0;
                string fn__4292()
                {
                    return "structure intact: " + s__624;
                }
                test___59.Assert(t___4307, (S1::Func<string>) fn__4292);
            }
            finally
            {
                test___59.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsUserSuppliedTableNameWithMetacharacters__977()
        {
            T::Test test___60 = new T::Test();
            try
            {
                string attack__626 = "users; DROP TABLE users; --";
                bool didBubble__627;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("users; DROP TABLE users; --");
                    didBubble__627 = false;
                }
                catch
                {
                    didBubble__627 = true;
                }
                string fn__4289()
                {
                    return "metacharacter-containing name must be rejected at construction";
                }
                test___60.Assert(didBubble__627, (S1::Func<string>) fn__4289);
            }
            finally
            {
                test___60.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierAcceptsValidNames__978()
        {
            T::Test test___67 = new T::Test();
            try
            {
                ISafeIdentifier t___2282;
                t___2282 = S0::SrcGlobal.SafeIdentifier("user_name");
                ISafeIdentifier id__665 = t___2282;
                bool t___4287 = id__665.SqlValue == "user_name";
                string fn__4284()
                {
                    return "value should round-trip";
                }
                test___67.Assert(t___4287, (S1::Func<string>) fn__4284);
            }
            finally
            {
                test___67.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsEmptyString__979()
        {
            T::Test test___68 = new T::Test();
            try
            {
                bool didBubble__667;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("");
                    didBubble__667 = false;
                }
                catch
                {
                    didBubble__667 = true;
                }
                string fn__4281()
                {
                    return "empty string should bubble";
                }
                test___68.Assert(didBubble__667, (S1::Func<string>) fn__4281);
            }
            finally
            {
                test___68.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsLeadingDigit__980()
        {
            T::Test test___69 = new T::Test();
            try
            {
                bool didBubble__669;
                try
                {
                    S0::SrcGlobal.SafeIdentifier("1col");
                    didBubble__669 = false;
                }
                catch
                {
                    didBubble__669 = true;
                }
                string fn__4278()
                {
                    return "leading digit should bubble";
                }
                test___69.Assert(didBubble__669, (S1::Func<string>) fn__4278);
            }
            finally
            {
                test___69.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void safeIdentifierRejectsSqlMetacharacters__981()
        {
            T::Test test___70 = new T::Test();
            try
            {
                G::IReadOnlyList<string> cases__671 = C::Listed.CreateReadOnlyList<string>("name); DROP TABLE", "col'", "a b", "a-b", "a.b", "a;b");
                void fn__4275(string c__672)
                {
                    bool didBubble__673;
                    try
                    {
                        S0::SrcGlobal.SafeIdentifier(c__672);
                        didBubble__673 = false;
                    }
                    catch
                    {
                        didBubble__673 = true;
                    }
                    string fn__4272()
                    {
                        return "should reject: " + c__672;
                    }
                    test___70.Assert(didBubble__673, (S1::Func<string>) fn__4272);
                }
                C::Listed.ForEach(cases__671, (S1::Action<string>) fn__4275);
            }
            finally
            {
                test___70.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupFound__982()
        {
            T::Test test___71 = new T::Test();
            try
            {
                ISafeIdentifier t___2259;
                t___2259 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___2260 = t___2259;
                ISafeIdentifier t___2261;
                t___2261 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___2262 = t___2261;
                StringField t___4262 = new StringField();
                FieldDef t___4263 = new FieldDef(t___2262, t___4262, false);
                ISafeIdentifier t___2265;
                t___2265 = S0::SrcGlobal.SafeIdentifier("age");
                ISafeIdentifier t___2266 = t___2265;
                IntField t___4264 = new IntField();
                FieldDef t___4265 = new FieldDef(t___2266, t___4264, false);
                TableDef td__675 = new TableDef(t___2260, C::Listed.CreateReadOnlyList<FieldDef>(t___4263, t___4265));
                FieldDef t___2270;
                t___2270 = td__675.Field("age");
                FieldDef f__676 = t___2270;
                bool t___4270 = f__676.Name.SqlValue == "age";
                string fn__4261()
                {
                    return "should find age field";
                }
                test___71.Assert(t___4270, (S1::Func<string>) fn__4261);
            }
            finally
            {
                test___71.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void tableDefFieldLookupNotFoundBubbles__983()
        {
            T::Test test___72 = new T::Test();
            try
            {
                ISafeIdentifier t___2250;
                t___2250 = S0::SrcGlobal.SafeIdentifier("users");
                ISafeIdentifier t___2251 = t___2250;
                ISafeIdentifier t___2252;
                t___2252 = S0::SrcGlobal.SafeIdentifier("name");
                ISafeIdentifier t___2253 = t___2252;
                StringField t___4256 = new StringField();
                FieldDef t___4257 = new FieldDef(t___2253, t___4256, false);
                TableDef td__678 = new TableDef(t___2251, C::Listed.CreateReadOnlyList<FieldDef>(t___4257));
                bool didBubble__679;
                try
                {
                    td__678.Field("nonexistent");
                    didBubble__679 = false;
                }
                catch
                {
                    didBubble__679 = true;
                }
                string fn__4255()
                {
                    return "unknown field should bubble";
                }
                test___72.Assert(didBubble__679, (S1::Func<string>) fn__4255);
            }
            finally
            {
                test___72.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void fieldDefNullableFlag__984()
        {
            T::Test test___73 = new T::Test();
            try
            {
                ISafeIdentifier t___2238;
                t___2238 = S0::SrcGlobal.SafeIdentifier("email");
                ISafeIdentifier t___2239 = t___2238;
                StringField t___4244 = new StringField();
                FieldDef required__681 = new FieldDef(t___2239, t___4244, false);
                ISafeIdentifier t___2242;
                t___2242 = S0::SrcGlobal.SafeIdentifier("bio");
                ISafeIdentifier t___2243 = t___2242;
                StringField t___4246 = new StringField();
                FieldDef optional__682 = new FieldDef(t___2243, t___4246, true);
                bool t___4250 = !required__681.Nullable;
                string fn__4243()
                {
                    return "required field should not be nullable";
                }
                test___73.Assert(t___4250, (S1::Func<string>) fn__4243);
                bool t___4252 = optional__682.Nullable;
                string fn__4242()
                {
                    return "optional field should be nullable";
                }
                test___73.Assert(t___4252, (S1::Func<string>) fn__4242);
            }
            finally
            {
                test___73.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEscaping__985()
        {
            T::Test test___77 = new T::Test();
            try
            {
                string build__808(string name__810)
                {
                    SqlBuilder t___4224 = new SqlBuilder();
                    t___4224.AppendSafe("select * from hi where name = ");
                    t___4224.AppendString(name__810);
                    return t___4224.Accumulated.ToString();
                }
                string buildWrong__809(string name__812)
                {
                    return "select * from hi where name = '" + name__812 + "'";
                }
                string actual___987 = build__808("world");
                bool t___4234 = actual___987 == "select * from hi where name = 'world'";
                string fn__4231()
                {
                    return "expected build(\u0022world\u0022) == (" + "select * from hi where name = 'world'" + ") not (" + actual___987 + ")";
                }
                test___77.Assert(t___4234, (S1::Func<string>) fn__4231);
                string bobbyTables__814 = "Robert'); drop table hi;--";
                string actual___989 = build__808("Robert'); drop table hi;--");
                bool t___4238 = actual___989 == "select * from hi where name = 'Robert''); drop table hi;--'";
                string fn__4230()
                {
                    return "expected build(bobbyTables) == (" + "select * from hi where name = 'Robert''); drop table hi;--'" + ") not (" + actual___989 + ")";
                }
                test___77.Assert(t___4238, (S1::Func<string>) fn__4230);
                string fn__4229()
                {
                    return "expected buildWrong(bobbyTables) == (select * from hi where name = 'Robert'); drop table hi;--') not (select * from hi where name = 'Robert'); drop table hi;--')";
                }
                test___77.Assert(true, (S1::Func<string>) fn__4229);
            }
            finally
            {
                test___77.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void stringEdgeCases__993()
        {
            T::Test test___78 = new T::Test();
            try
            {
                SqlBuilder t___4192 = new SqlBuilder();
                t___4192.AppendSafe("v = ");
                t___4192.AppendString("");
                string actual___994 = t___4192.Accumulated.ToString();
                bool t___4198 = actual___994 == "v = ''";
                string fn__4191()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022\u0022).toString() == (" + "v = ''" + ") not (" + actual___994 + ")";
                }
                test___78.Assert(t___4198, (S1::Func<string>) fn__4191);
                SqlBuilder t___4200 = new SqlBuilder();
                t___4200.AppendSafe("v = ");
                t___4200.AppendString("a''b");
                string actual___997 = t___4200.Accumulated.ToString();
                bool t___4206 = actual___997 == "v = 'a''''b'";
                string fn__4190()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022a''b\u0022).toString() == (" + "v = 'a''''b'" + ") not (" + actual___997 + ")";
                }
                test___78.Assert(t___4206, (S1::Func<string>) fn__4190);
                SqlBuilder t___4208 = new SqlBuilder();
                t___4208.AppendSafe("v = ");
                t___4208.AppendString("Hello 世界");
                string actual___1000 = t___4208.Accumulated.ToString();
                bool t___4214 = actual___1000 == "v = 'Hello 世界'";
                string fn__4189()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Hello 世界\u0022).toString() == (" + "v = 'Hello 世界'" + ") not (" + actual___1000 + ")";
                }
                test___78.Assert(t___4214, (S1::Func<string>) fn__4189);
                SqlBuilder t___4216 = new SqlBuilder();
                t___4216.AppendSafe("v = ");
                t___4216.AppendString("Line1\nLine2");
                string actual___1003 = t___4216.Accumulated.ToString();
                bool t___4222 = actual___1003 == "v = 'Line1\nLine2'";
                string fn__4188()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, \u0022Line1\\nLine2\u0022).toString() == (" + "v = 'Line1\nLine2'" + ") not (" + actual___1003 + ")";
                }
                test___78.Assert(t___4222, (S1::Func<string>) fn__4188);
            }
            finally
            {
                test___78.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void numbersAndBooleans__1006()
        {
            T::Test test___79 = new T::Test();
            try
            {
                SqlBuilder t___4163 = new SqlBuilder();
                t___4163.AppendSafe("select ");
                t___4163.AppendInt32(42);
                t___4163.AppendSafe(", ");
                t___4163.AppendInt64(43);
                t___4163.AppendSafe(", ");
                t___4163.AppendFloat64(19.99);
                t___4163.AppendSafe(", ");
                t___4163.AppendBoolean(true);
                t___4163.AppendSafe(", ");
                t___4163.AppendBoolean(false);
                string actual___1007 = t___4163.Accumulated.ToString();
                bool t___4177 = actual___1007 == "select 42, 43, 19.99, TRUE, FALSE";
                string fn__4162()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, 42, \u0022, \u0022, \\interpolate, 43, \u0022, \u0022, \\interpolate, 19.99, \u0022, \u0022, \\interpolate, true, \u0022, \u0022, \\interpolate, false).toString() == (" + "select 42, 43, 19.99, TRUE, FALSE" + ") not (" + actual___1007 + ")";
                }
                test___79.Assert(t___4177, (S1::Func<string>) fn__4162);
                S1::DateTime t___2183;
                t___2183 = new S1::DateTime(2024, 12, 25);
                S1::DateTime date__817 = t___2183;
                SqlBuilder t___4179 = new SqlBuilder();
                t___4179.AppendSafe("insert into t values (");
                t___4179.AppendDate(date__817);
                t___4179.AppendSafe(")");
                string actual___1010 = t___4179.Accumulated.ToString();
                bool t___4186 = actual___1010 == "insert into t values ('2024-12-25')";
                string fn__4161()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022insert into t values (\u0022, \\interpolate, date, \u0022)\u0022).toString() == (" + "insert into t values ('2024-12-25')" + ") not (" + actual___1010 + ")";
                }
                test___79.Assert(t___4186, (S1::Func<string>) fn__4161);
            }
            finally
            {
                test___79.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void lists__1013()
        {
            T::Test test___80 = new T::Test();
            try
            {
                SqlBuilder t___4107 = new SqlBuilder();
                t___4107.AppendSafe("v IN (");
                t___4107.AppendStringList(C::Listed.CreateReadOnlyList<string>("a", "b", "c'd"));
                t___4107.AppendSafe(")");
                string actual___1014 = t___4107.Accumulated.ToString();
                bool t___4114 = actual___1014 == "v IN ('a', 'b', 'c''d')";
                string fn__4106()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(\u0022a\u0022, \u0022b\u0022, \u0022c'd\u0022), \u0022)\u0022).toString() == (" + "v IN ('a', 'b', 'c''d')" + ") not (" + actual___1014 + ")";
                }
                test___80.Assert(t___4114, (S1::Func<string>) fn__4106);
                SqlBuilder t___4116 = new SqlBuilder();
                t___4116.AppendSafe("v IN (");
                t___4116.AppendInt32_List(C::Listed.CreateReadOnlyList<int>(1, 2, 3));
                t___4116.AppendSafe(")");
                string actual___1017 = t___4116.Accumulated.ToString();
                bool t___4123 = actual___1017 == "v IN (1, 2, 3)";
                string fn__4105()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2, 3), \u0022)\u0022).toString() == (" + "v IN (1, 2, 3)" + ") not (" + actual___1017 + ")";
                }
                test___80.Assert(t___4123, (S1::Func<string>) fn__4105);
                SqlBuilder t___4125 = new SqlBuilder();
                t___4125.AppendSafe("v IN (");
                t___4125.AppendInt64_List(C::Listed.CreateReadOnlyList<long>(1, 2));
                t___4125.AppendSafe(")");
                string actual___1020 = t___4125.Accumulated.ToString();
                bool t___4132 = actual___1020 == "v IN (1, 2)";
                string fn__4104()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1, 2), \u0022)\u0022).toString() == (" + "v IN (1, 2)" + ") not (" + actual___1020 + ")";
                }
                test___80.Assert(t___4132, (S1::Func<string>) fn__4104);
                SqlBuilder t___4134 = new SqlBuilder();
                t___4134.AppendSafe("v IN (");
                t___4134.AppendFloat64_List(C::Listed.CreateReadOnlyList<double>(1.0, 2.0));
                t___4134.AppendSafe(")");
                string actual___1023 = t___4134.Accumulated.ToString();
                bool t___4141 = actual___1023 == "v IN (1.0, 2.0)";
                string fn__4103()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(1.0, 2.0), \u0022)\u0022).toString() == (" + "v IN (1.0, 2.0)" + ") not (" + actual___1023 + ")";
                }
                test___80.Assert(t___4141, (S1::Func<string>) fn__4103);
                SqlBuilder t___4143 = new SqlBuilder();
                t___4143.AppendSafe("v IN (");
                t___4143.AppendBooleanList(C::Listed.CreateReadOnlyList<bool>(true, false));
                t___4143.AppendSafe(")");
                string actual___1026 = t___4143.Accumulated.ToString();
                bool t___4150 = actual___1026 == "v IN (TRUE, FALSE)";
                string fn__4102()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, list(true, false), \u0022)\u0022).toString() == (" + "v IN (TRUE, FALSE)" + ") not (" + actual___1026 + ")";
                }
                test___80.Assert(t___4150, (S1::Func<string>) fn__4102);
                S1::DateTime t___2155;
                t___2155 = new S1::DateTime(2024, 1, 1);
                S1::DateTime t___2156 = t___2155;
                S1::DateTime t___2157;
                t___2157 = new S1::DateTime(2024, 12, 25);
                S1::DateTime t___2158 = t___2157;
                G::IReadOnlyList<S1::DateTime> dates__819 = C::Listed.CreateReadOnlyList<S1::DateTime>(t___2156, t___2158);
                SqlBuilder t___4152 = new SqlBuilder();
                t___4152.AppendSafe("v IN (");
                t___4152.AppendDateList(dates__819);
                t___4152.AppendSafe(")");
                string actual___1029 = t___4152.Accumulated.ToString();
                bool t___4159 = actual___1029 == "v IN ('2024-01-01', '2024-12-25')";
                string fn__4101()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v IN (\u0022, \\interpolate, dates, \u0022)\u0022).toString() == (" + "v IN ('2024-01-01', '2024-12-25')" + ") not (" + actual___1029 + ")";
                }
                test___80.Assert(t___4159, (S1::Func<string>) fn__4101);
            }
            finally
            {
                test___80.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_naNRendersAsNull__1032()
        {
            T::Test test___81 = new T::Test();
            try
            {
                double nan__821;
                nan__821 = 0.0 / 0.0;
                SqlBuilder t___4093 = new SqlBuilder();
                t___4093.AppendSafe("v = ");
                t___4093.AppendFloat64(nan__821);
                string actual___1033 = t___4093.Accumulated.ToString();
                bool t___4099 = actual___1033 == "v = NULL";
                string fn__4092()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, nan).toString() == (" + "v = NULL" + ") not (" + actual___1033 + ")";
                }
                test___81.Assert(t___4099, (S1::Func<string>) fn__4092);
            }
            finally
            {
                test___81.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_infinityRendersAsNull__1036()
        {
            T::Test test___82 = new T::Test();
            try
            {
                double inf__823;
                inf__823 = 1.0 / 0.0;
                SqlBuilder t___4084 = new SqlBuilder();
                t___4084.AppendSafe("v = ");
                t___4084.AppendFloat64(inf__823);
                string actual___1037 = t___4084.Accumulated.ToString();
                bool t___4090 = actual___1037 == "v = NULL";
                string fn__4083()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, inf).toString() == (" + "v = NULL" + ") not (" + actual___1037 + ")";
                }
                test___82.Assert(t___4090, (S1::Func<string>) fn__4083);
            }
            finally
            {
                test___82.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_negativeInfinityRendersAsNull__1040()
        {
            T::Test test___83 = new T::Test();
            try
            {
                double ninf__825;
                ninf__825 = -1.0 / 0.0;
                SqlBuilder t___4075 = new SqlBuilder();
                t___4075.AppendSafe("v = ");
                t___4075.AppendFloat64(ninf__825);
                string actual___1041 = t___4075.Accumulated.ToString();
                bool t___4081 = actual___1041 == "v = NULL";
                string fn__4074()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, ninf).toString() == (" + "v = NULL" + ") not (" + actual___1041 + ")";
                }
                test___83.Assert(t___4081, (S1::Func<string>) fn__4074);
            }
            finally
            {
                test___83.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlFloat64_normalValuesStillWork__1044()
        {
            T::Test test___84 = new T::Test();
            try
            {
                SqlBuilder t___4050 = new SqlBuilder();
                t___4050.AppendSafe("v = ");
                t___4050.AppendFloat64(3.14);
                string actual___1045 = t___4050.Accumulated.ToString();
                bool t___4056 = actual___1045 == "v = 3.14";
                string fn__4049()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 3.14).toString() == (" + "v = 3.14" + ") not (" + actual___1045 + ")";
                }
                test___84.Assert(t___4056, (S1::Func<string>) fn__4049);
                SqlBuilder t___4058 = new SqlBuilder();
                t___4058.AppendSafe("v = ");
                t___4058.AppendFloat64(0.0);
                string actual___1048 = t___4058.Accumulated.ToString();
                bool t___4064 = actual___1048 == "v = 0.0";
                string fn__4048()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, 0.0).toString() == (" + "v = 0.0" + ") not (" + actual___1048 + ")";
                }
                test___84.Assert(t___4064, (S1::Func<string>) fn__4048);
                SqlBuilder t___4066 = new SqlBuilder();
                t___4066.AppendSafe("v = ");
                t___4066.AppendFloat64(-42.5);
                string actual___1051 = t___4066.Accumulated.ToString();
                bool t___4072 = actual___1051 == "v = -42.5";
                string fn__4047()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, -42.5).toString() == (" + "v = -42.5" + ") not (" + actual___1051 + ")";
                }
                test___84.Assert(t___4072, (S1::Func<string>) fn__4047);
            }
            finally
            {
                test___84.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void sqlDateRendersWithQuotes__1054()
        {
            T::Test test___85 = new T::Test();
            try
            {
                S1::DateTime t___2051;
                t___2051 = new S1::DateTime(2024, 6, 15);
                S1::DateTime d__828 = t___2051;
                SqlBuilder t___4039 = new SqlBuilder();
                t___4039.AppendSafe("v = ");
                t___4039.AppendDate(d__828);
                string actual___1055 = t___4039.Accumulated.ToString();
                bool t___4045 = actual___1055 == "v = '2024-06-15'";
                string fn__4038()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022v = \u0022, \\interpolate, d).toString() == (" + "v = '2024-06-15'" + ") not (" + actual___1055 + ")";
                }
                test___85.Assert(t___4045, (S1::Func<string>) fn__4038);
            }
            finally
            {
                test___85.SoftFailToHard();
            }
        }
        [U::TestMethod]
        public void nesting__1058()
        {
            T::Test test___86 = new T::Test();
            try
            {
                string name__830 = "Someone";
                SqlBuilder t___4007 = new SqlBuilder();
                t___4007.AppendSafe("where p.last_name = ");
                t___4007.AppendString("Someone");
                SqlFragment condition__831 = t___4007.Accumulated;
                SqlBuilder t___4011 = new SqlBuilder();
                t___4011.AppendSafe("select p.id from person p ");
                t___4011.AppendFragment(condition__831);
                string actual___1060 = t___4011.Accumulated.ToString();
                bool t___4017 = actual___1060 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__4006()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1060 + ")";
                }
                test___86.Assert(t___4017, (S1::Func<string>) fn__4006);
                SqlBuilder t___4019 = new SqlBuilder();
                t___4019.AppendSafe("select p.id from person p ");
                t___4019.AppendPart(condition__831.ToSource());
                string actual___1063 = t___4019.Accumulated.ToString();
                bool t___4026 = actual___1063 == "select p.id from person p where p.last_name = 'Someone'";
                string fn__4005()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select p.id from person p \u0022, \\interpolate, condition.toSource()).toString() == (" + "select p.id from person p where p.last_name = 'Someone'" + ") not (" + actual___1063 + ")";
                }
                test___86.Assert(t___4026, (S1::Func<string>) fn__4005);
                G::IReadOnlyList<ISqlPart> parts__832 = C::Listed.CreateReadOnlyList<ISqlPart>(new SqlString("a'b"), new SqlInt32(3));
                SqlBuilder t___4030 = new SqlBuilder();
                t___4030.AppendSafe("select ");
                t___4030.AppendPartList(parts__832);
                string actual___1066 = t___4030.Accumulated.ToString();
                bool t___4036 = actual___1066 == "select 'a''b', 3";
                string fn__4004()
                {
                    return "expected stringExpr(`-work//src/`.sql, true, \u0022select \u0022, \\interpolate, parts).toString() == (" + "select 'a''b', 3" + ") not (" + actual___1066 + ")";
                }
                test___86.Assert(t___4036, (S1::Func<string>) fn__4004);
            }
            finally
            {
                test___86.SoftFailToHard();
            }
        }
    }
}
