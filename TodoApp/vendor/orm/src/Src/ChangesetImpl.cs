using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__551;
        readonly G::IReadOnlyDictionary<string, string> _params__552;
        readonly G::IReadOnlyDictionary<string, string> _changes__553;
        readonly G::IReadOnlyList<ChangesetError> _errors__554;
        readonly bool _isValid__555;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__551;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__553;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__554;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__555;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__565)
        {
            G::IDictionary<string, string> mb__567 = new C::OrderedDictionary<string, string>();
            void fn__10837(ISafeIdentifier f__568)
            {
                string t___10835;
                string t___10832 = f__568.SqlValue;
                string val__569 = C::Mapped.GetOrDefault(this._params__552, t___10832, "");
                if (!string.IsNullOrEmpty(val__569))
                {
                    t___10835 = f__568.SqlValue;
                    mb__567[t___10835] = val__569;
                }
            }
            C::Listed.ForEach(allowedFields__565, (S::Action<ISafeIdentifier>) fn__10837);
            return new ChangesetImpl(this._tableDef__551, this._params__552, C::Mapped.ToMap(mb__567), this._errors__554, this._isValid__555);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__571)
        {
            IChangeset return__306;
            G::IReadOnlyList<ChangesetError> t___10830;
            TableDef t___6226;
            G::IReadOnlyDictionary<string, string> t___6227;
            G::IReadOnlyDictionary<string, string> t___6228;
            {
                {
                    if (!this._isValid__555)
                    {
                        return__306 = this;
                        goto fn__572;
                    }
                    G::IList<ChangesetError> eb__573 = L::Enumerable.ToList(this._errors__554);
                    bool valid__574 = true;
                    void fn__10826(ISafeIdentifier f__575)
                    {
                        ChangesetError t___10824;
                        string t___10821 = f__575.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__553, t___10821))
                        {
                            t___10824 = new ChangesetError(f__575.SqlValue, "is required");
                            C::Listed.Add(eb__573, t___10824);
                            valid__574 = false;
                        }
                    }
                    C::Listed.ForEach(fields__571, (S::Action<ISafeIdentifier>) fn__10826);
                    t___6226 = this._tableDef__551;
                    t___6227 = this._params__552;
                    t___6228 = this._changes__553;
                    t___10830 = C::Listed.ToReadOnlyList(eb__573);
                    return__306 = new ChangesetImpl(t___6226, t___6227, t___6228, t___10830, valid__574);
                }
                fn__572:
                {
                }
            }
            return return__306;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__577, int min__578, int max__579)
        {
            IChangeset return__307;
            string t___10808;
            G::IReadOnlyList<ChangesetError> t___10819;
            bool t___6209;
            TableDef t___6215;
            G::IReadOnlyDictionary<string, string> t___6216;
            G::IReadOnlyDictionary<string, string> t___6217;
            {
                {
                    if (!this._isValid__555)
                    {
                        return__307 = this;
                        goto fn__580;
                    }
                    t___10808 = field__577.SqlValue;
                    string val__581 = C::Mapped.GetOrDefault(this._changes__553, t___10808, "");
                    int len__582 = C::StringUtil.CountBetween(val__581, 0, val__581.Length);
                    if (len__582 < min__578)
                    {
                        t___6209 = true;
                    }
                    else
                    {
                        t___6209 = len__582 > max__579;
                    }
                    if (t___6209)
                    {
                        string msg__583 = "must be between " + S::Convert.ToString(min__578) + " and " + S::Convert.ToString(max__579) + " characters";
                        G::IList<ChangesetError> eb__584 = L::Enumerable.ToList(this._errors__554);
                        C::Listed.Add(eb__584, new ChangesetError(field__577.SqlValue, msg__583));
                        t___6215 = this._tableDef__551;
                        t___6216 = this._params__552;
                        t___6217 = this._changes__553;
                        t___10819 = C::Listed.ToReadOnlyList(eb__584);
                        return__307 = new ChangesetImpl(t___6215, t___6216, t___6217, t___10819, false);
                        goto fn__580;
                    }
                    return__307 = this;
                }
                fn__580:
                {
                }
            }
            return return__307;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__586)
        {
            IChangeset return__308;
            string t___10799;
            G::IReadOnlyList<ChangesetError> t___10806;
            TableDef t___6200;
            G::IReadOnlyDictionary<string, string> t___6201;
            G::IReadOnlyDictionary<string, string> t___6202;
            {
                {
                    if (!this._isValid__555)
                    {
                        return__308 = this;
                        goto fn__587;
                    }
                    t___10799 = field__586.SqlValue;
                    string val__588 = C::Mapped.GetOrDefault(this._changes__553, t___10799, "");
                    if (string.IsNullOrEmpty(val__588))
                    {
                        return__308 = this;
                        goto fn__587;
                    }
                    bool parseOk__589;
                    try
                    {
                        C::Core.ToInt(val__588);
                        parseOk__589 = true;
                    }
                    catch
                    {
                        parseOk__589 = false;
                    }
                    if (!parseOk__589)
                    {
                        G::IList<ChangesetError> eb__590 = L::Enumerable.ToList(this._errors__554);
                        C::Listed.Add(eb__590, new ChangesetError(field__586.SqlValue, "must be an integer"));
                        t___6200 = this._tableDef__551;
                        t___6201 = this._params__552;
                        t___6202 = this._changes__553;
                        t___10806 = C::Listed.ToReadOnlyList(eb__590);
                        return__308 = new ChangesetImpl(t___6200, t___6201, t___6202, t___10806, false);
                        goto fn__587;
                    }
                    return__308 = this;
                }
                fn__587:
                {
                }
            }
            return return__308;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__592)
        {
            IChangeset return__309;
            string t___10790;
            G::IReadOnlyList<ChangesetError> t___10797;
            TableDef t___6187;
            G::IReadOnlyDictionary<string, string> t___6188;
            G::IReadOnlyDictionary<string, string> t___6189;
            {
                {
                    if (!this._isValid__555)
                    {
                        return__309 = this;
                        goto fn__593;
                    }
                    t___10790 = field__592.SqlValue;
                    string val__594 = C::Mapped.GetOrDefault(this._changes__553, t___10790, "");
                    if (string.IsNullOrEmpty(val__594))
                    {
                        return__309 = this;
                        goto fn__593;
                    }
                    bool parseOk__595;
                    try
                    {
                        C::Core.ToInt64(val__594);
                        parseOk__595 = true;
                    }
                    catch
                    {
                        parseOk__595 = false;
                    }
                    if (!parseOk__595)
                    {
                        G::IList<ChangesetError> eb__596 = L::Enumerable.ToList(this._errors__554);
                        C::Listed.Add(eb__596, new ChangesetError(field__592.SqlValue, "must be a 64-bit integer"));
                        t___6187 = this._tableDef__551;
                        t___6188 = this._params__552;
                        t___6189 = this._changes__553;
                        t___10797 = C::Listed.ToReadOnlyList(eb__596);
                        return__309 = new ChangesetImpl(t___6187, t___6188, t___6189, t___10797, false);
                        goto fn__593;
                    }
                    return__309 = this;
                }
                fn__593:
                {
                }
            }
            return return__309;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__598)
        {
            IChangeset return__310;
            string t___10781;
            G::IReadOnlyList<ChangesetError> t___10788;
            TableDef t___6174;
            G::IReadOnlyDictionary<string, string> t___6175;
            G::IReadOnlyDictionary<string, string> t___6176;
            {
                {
                    if (!this._isValid__555)
                    {
                        return__310 = this;
                        goto fn__599;
                    }
                    t___10781 = field__598.SqlValue;
                    string val__600 = C::Mapped.GetOrDefault(this._changes__553, t___10781, "");
                    if (string.IsNullOrEmpty(val__600))
                    {
                        return__310 = this;
                        goto fn__599;
                    }
                    bool parseOk__601;
                    try
                    {
                        C::Float64.ToFloat64(val__600);
                        parseOk__601 = true;
                    }
                    catch
                    {
                        parseOk__601 = false;
                    }
                    if (!parseOk__601)
                    {
                        G::IList<ChangesetError> eb__602 = L::Enumerable.ToList(this._errors__554);
                        C::Listed.Add(eb__602, new ChangesetError(field__598.SqlValue, "must be a number"));
                        t___6174 = this._tableDef__551;
                        t___6175 = this._params__552;
                        t___6176 = this._changes__553;
                        t___10788 = C::Listed.ToReadOnlyList(eb__602);
                        return__310 = new ChangesetImpl(t___6174, t___6175, t___6176, t___10788, false);
                        goto fn__599;
                    }
                    return__310 = this;
                }
                fn__599:
                {
                }
            }
            return return__310;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__604)
        {
            IChangeset return__311;
            string t___10772;
            G::IReadOnlyList<ChangesetError> t___10779;
            bool t___6149;
            bool t___6150;
            bool t___6152;
            bool t___6153;
            bool t___6155;
            TableDef t___6161;
            G::IReadOnlyDictionary<string, string> t___6162;
            G::IReadOnlyDictionary<string, string> t___6163;
            {
                {
                    if (!this._isValid__555)
                    {
                        return__311 = this;
                        goto fn__605;
                    }
                    t___10772 = field__604.SqlValue;
                    string val__606 = C::Mapped.GetOrDefault(this._changes__553, t___10772, "");
                    if (string.IsNullOrEmpty(val__606))
                    {
                        return__311 = this;
                        goto fn__605;
                    }
                    bool isTrue__607;
                    if (val__606 == "true")
                    {
                        isTrue__607 = true;
                    }
                    else
                    {
                        if (val__606 == "1")
                        {
                            t___6150 = true;
                        }
                        else
                        {
                            if (val__606 == "yes")
                            {
                                t___6149 = true;
                            }
                            else
                            {
                                t___6149 = val__606 == "on";
                            }
                            t___6150 = t___6149;
                        }
                        isTrue__607 = t___6150;
                    }
                    bool isFalse__608;
                    if (val__606 == "false")
                    {
                        isFalse__608 = true;
                    }
                    else
                    {
                        if (val__606 == "0")
                        {
                            t___6153 = true;
                        }
                        else
                        {
                            if (val__606 == "no")
                            {
                                t___6152 = true;
                            }
                            else
                            {
                                t___6152 = val__606 == "off";
                            }
                            t___6153 = t___6152;
                        }
                        isFalse__608 = t___6153;
                    }
                    if (!isTrue__607)
                    {
                        t___6155 = !isFalse__608;
                    }
                    else
                    {
                        t___6155 = false;
                    }
                    if (t___6155)
                    {
                        G::IList<ChangesetError> eb__609 = L::Enumerable.ToList(this._errors__554);
                        C::Listed.Add(eb__609, new ChangesetError(field__604.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___6161 = this._tableDef__551;
                        t___6162 = this._params__552;
                        t___6163 = this._changes__553;
                        t___10779 = C::Listed.ToReadOnlyList(eb__609);
                        return__311 = new ChangesetImpl(t___6161, t___6162, t___6163, t___10779, false);
                        goto fn__605;
                    }
                    return__311 = this;
                }
                fn__605:
                {
                }
            }
            return return__311;
        }
        SqlBoolean ParseBoolSqlPart(string val__611)
        {
            SqlBoolean return__312;
            bool t___6138;
            bool t___6139;
            bool t___6140;
            bool t___6142;
            bool t___6143;
            bool t___6144;
            {
                {
                    if (val__611 == "true")
                    {
                        t___6140 = true;
                    }
                    else
                    {
                        if (val__611 == "1")
                        {
                            t___6139 = true;
                        }
                        else
                        {
                            if (val__611 == "yes")
                            {
                                t___6138 = true;
                            }
                            else
                            {
                                t___6138 = val__611 == "on";
                            }
                            t___6139 = t___6138;
                        }
                        t___6140 = t___6139;
                    }
                    if (t___6140)
                    {
                        return__312 = new SqlBoolean(true);
                        goto fn__612;
                    }
                    if (val__611 == "false")
                    {
                        t___6144 = true;
                    }
                    else
                    {
                        if (val__611 == "0")
                        {
                            t___6143 = true;
                        }
                        else
                        {
                            if (val__611 == "no")
                            {
                                t___6142 = true;
                            }
                            else
                            {
                                t___6142 = val__611 == "off";
                            }
                            t___6143 = t___6142;
                        }
                        t___6144 = t___6143;
                    }
                    if (t___6144)
                    {
                        return__312 = new SqlBoolean(false);
                        goto fn__612;
                    }
                    throw new S::Exception();
                }
                fn__612:
                {
                }
            }
            return return__312;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__614, string val__615)
        {
            ISqlPart return__313;
            int t___6125;
            long t___6128;
            double t___6131;
            S::DateTime t___6136;
            {
                {
                    IFieldType ft__617 = fieldDef__614.FieldType;
                    if (ft__617 is StringField)
                    {
                        return__313 = new SqlString(val__615);
                        goto fn__616;
                    }
                    if (ft__617 is IntField)
                    {
                        t___6125 = C::Core.ToInt(val__615);
                        return__313 = new SqlInt32(t___6125);
                        goto fn__616;
                    }
                    if (ft__617 is Int64_Field)
                    {
                        t___6128 = C::Core.ToInt64(val__615);
                        return__313 = new SqlInt64(t___6128);
                        goto fn__616;
                    }
                    if (ft__617 is FloatField)
                    {
                        t___6131 = C::Float64.ToFloat64(val__615);
                        return__313 = new SqlFloat64(t___6131);
                        goto fn__616;
                    }
                    if (ft__617 is BoolField)
                    {
                        return__313 = this.ParseBoolSqlPart(val__615);
                        goto fn__616;
                    }
                    if (ft__617 is DateField)
                    {
                        t___6136 = T::TemporalSupport.FromIsoString(val__615);
                        return__313 = new SqlDate(t___6136);
                        goto fn__616;
                    }
                    throw new S::Exception();
                }
                fn__616:
                {
                }
            }
            return return__313;
        }
        public SqlFragment ToInsertSql()
        {
            int t___10720;
            string t___10725;
            bool t___10726;
            int t___10731;
            string t___10733;
            string t___10737;
            int t___10752;
            bool t___6089;
            FieldDef t___6097;
            ISqlPart t___6102;
            if (!this._isValid__555) throw new S::Exception();
            int i__620 = 0;
            while (true)
            {
                t___10720 = this._tableDef__551.Fields.Count;
                if (!(i__620 < t___10720)) break;
                FieldDef f__621 = this._tableDef__551.Fields[i__620];
                if (!f__621.Nullable)
                {
                    t___10725 = f__621.Name.SqlValue;
                    t___10726 = C::Mapped.ContainsKey(this._changes__553, t___10725);
                    t___6089 = !t___10726;
                }
                else
                {
                    t___6089 = false;
                }
                if (t___6089) throw new S::Exception();
                i__620 = i__620 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__622 = C::Mapped.ToList(this._changes__553);
            if (pairs__622.Count == 0) throw new S::Exception();
            G::IList<string> colNames__623 = new G::List<string>();
            G::IList<ISqlPart> valParts__624 = new G::List<ISqlPart>();
            int i__625 = 0;
            while (true)
            {
                t___10731 = pairs__622.Count;
                if (!(i__625 < t___10731)) break;
                G::KeyValuePair<string, string> pair__626 = pairs__622[i__625];
                t___10733 = pair__626.Key;
                t___6097 = this._tableDef__551.Field(t___10733);
                FieldDef fd__627 = t___6097;
                C::Listed.Add(colNames__623, fd__627.Name.SqlValue);
                t___10737 = pair__626.Value;
                t___6102 = this.ValueToSqlPart(fd__627, t___10737);
                C::Listed.Add(valParts__624, t___6102);
                i__625 = i__625 + 1;
            }
            SqlBuilder b__628 = new SqlBuilder();
            b__628.AppendSafe("INSERT INTO ");
            b__628.AppendSafe(this._tableDef__551.TableName.SqlValue);
            b__628.AppendSafe(" (");
            G::IReadOnlyList<string> t___10745 = C::Listed.ToReadOnlyList(colNames__623);
            string fn__10718(string c__629)
            {
                return c__629;
            }
            b__628.AppendSafe(C::Listed.Join(t___10745, ", ", (S::Func<string, string>) fn__10718));
            b__628.AppendSafe(") VALUES (");
            b__628.AppendPart(valParts__624[0]);
            int j__630 = 1;
            while (true)
            {
                t___10752 = valParts__624.Count;
                if (!(j__630 < t___10752)) break;
                b__628.AppendSafe(", ");
                b__628.AppendPart(valParts__624[j__630]);
                j__630 = j__630 + 1;
            }
            b__628.AppendSafe(")");
            return b__628.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__632)
        {
            int t___10705;
            string t___10708;
            string t___10713;
            FieldDef t___6070;
            ISqlPart t___6076;
            if (!this._isValid__555) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__634 = C::Mapped.ToList(this._changes__553);
            if (pairs__634.Count == 0) throw new S::Exception();
            SqlBuilder b__635 = new SqlBuilder();
            b__635.AppendSafe("UPDATE ");
            b__635.AppendSafe(this._tableDef__551.TableName.SqlValue);
            b__635.AppendSafe(" SET ");
            int i__636 = 0;
            while (true)
            {
                t___10705 = pairs__634.Count;
                if (!(i__636 < t___10705)) break;
                if (i__636 > 0) b__635.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__637 = pairs__634[i__636];
                t___10708 = pair__637.Key;
                t___6070 = this._tableDef__551.Field(t___10708);
                FieldDef fd__638 = t___6070;
                b__635.AppendSafe(fd__638.Name.SqlValue);
                b__635.AppendSafe(" = ");
                t___10713 = pair__637.Value;
                t___6076 = this.ValueToSqlPart(fd__638, t___10713);
                b__635.AppendPart(t___6076);
                i__636 = i__636 + 1;
            }
            b__635.AppendSafe(" WHERE id = ");
            b__635.AppendInt32(id__632);
            return b__635.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__640, G::IReadOnlyDictionary<string, string> _params__641, G::IReadOnlyDictionary<string, string> _changes__642, G::IReadOnlyList<ChangesetError> _errors__643, bool _isValid__644)
        {
            this._tableDef__551 = _tableDef__640;
            this._params__552 = _params__641;
            this._changes__553 = _changes__642;
            this._errors__554 = _errors__643;
            this._isValid__555 = _isValid__644;
        }
    }
}
