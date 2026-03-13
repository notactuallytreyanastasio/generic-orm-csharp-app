using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__489;
        readonly G::IReadOnlyDictionary<string, string> _params__490;
        readonly G::IReadOnlyDictionary<string, string> _changes__491;
        readonly G::IReadOnlyList<ChangesetError> _errors__492;
        readonly bool _isValid__493;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__489;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__491;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__492;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__493;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__503)
        {
            G::IDictionary<string, string> mb__505 = new C::OrderedDictionary<string, string>();
            void fn__8709(ISafeIdentifier f__506)
            {
                string t___8707;
                string t___8704 = f__506.SqlValue;
                string val__507 = C::Mapped.GetOrDefault(this._params__490, t___8704, "");
                if (!string.IsNullOrEmpty(val__507))
                {
                    t___8707 = f__506.SqlValue;
                    mb__505[t___8707] = val__507;
                }
            }
            C::Listed.ForEach(allowedFields__503, (S::Action<ISafeIdentifier>) fn__8709);
            return new ChangesetImpl(this._tableDef__489, this._params__490, C::Mapped.ToMap(mb__505), this._errors__492, this._isValid__493);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__509)
        {
            IChangeset return__273;
            G::IReadOnlyList<ChangesetError> t___8702;
            TableDef t___5006;
            G::IReadOnlyDictionary<string, string> t___5007;
            G::IReadOnlyDictionary<string, string> t___5008;
            {
                {
                    if (!this._isValid__493)
                    {
                        return__273 = this;
                        goto fn__510;
                    }
                    G::IList<ChangesetError> eb__511 = L::Enumerable.ToList(this._errors__492);
                    bool valid__512 = true;
                    void fn__8698(ISafeIdentifier f__513)
                    {
                        ChangesetError t___8696;
                        string t___8693 = f__513.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__491, t___8693))
                        {
                            t___8696 = new ChangesetError(f__513.SqlValue, "is required");
                            C::Listed.Add(eb__511, t___8696);
                            valid__512 = false;
                        }
                    }
                    C::Listed.ForEach(fields__509, (S::Action<ISafeIdentifier>) fn__8698);
                    t___5006 = this._tableDef__489;
                    t___5007 = this._params__490;
                    t___5008 = this._changes__491;
                    t___8702 = C::Listed.ToReadOnlyList(eb__511);
                    return__273 = new ChangesetImpl(t___5006, t___5007, t___5008, t___8702, valid__512);
                }
                fn__510:
                {
                }
            }
            return return__273;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__515, int min__516, int max__517)
        {
            IChangeset return__274;
            string t___8680;
            G::IReadOnlyList<ChangesetError> t___8691;
            bool t___4989;
            TableDef t___4995;
            G::IReadOnlyDictionary<string, string> t___4996;
            G::IReadOnlyDictionary<string, string> t___4997;
            {
                {
                    if (!this._isValid__493)
                    {
                        return__274 = this;
                        goto fn__518;
                    }
                    t___8680 = field__515.SqlValue;
                    string val__519 = C::Mapped.GetOrDefault(this._changes__491, t___8680, "");
                    int len__520 = C::StringUtil.CountBetween(val__519, 0, val__519.Length);
                    if (len__520 < min__516)
                    {
                        t___4989 = true;
                    }
                    else
                    {
                        t___4989 = len__520 > max__517;
                    }
                    if (t___4989)
                    {
                        string msg__521 = "must be between " + S::Convert.ToString(min__516) + " and " + S::Convert.ToString(max__517) + " characters";
                        G::IList<ChangesetError> eb__522 = L::Enumerable.ToList(this._errors__492);
                        C::Listed.Add(eb__522, new ChangesetError(field__515.SqlValue, msg__521));
                        t___4995 = this._tableDef__489;
                        t___4996 = this._params__490;
                        t___4997 = this._changes__491;
                        t___8691 = C::Listed.ToReadOnlyList(eb__522);
                        return__274 = new ChangesetImpl(t___4995, t___4996, t___4997, t___8691, false);
                        goto fn__518;
                    }
                    return__274 = this;
                }
                fn__518:
                {
                }
            }
            return return__274;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__524)
        {
            IChangeset return__275;
            string t___8671;
            G::IReadOnlyList<ChangesetError> t___8678;
            TableDef t___4980;
            G::IReadOnlyDictionary<string, string> t___4981;
            G::IReadOnlyDictionary<string, string> t___4982;
            {
                {
                    if (!this._isValid__493)
                    {
                        return__275 = this;
                        goto fn__525;
                    }
                    t___8671 = field__524.SqlValue;
                    string val__526 = C::Mapped.GetOrDefault(this._changes__491, t___8671, "");
                    if (string.IsNullOrEmpty(val__526))
                    {
                        return__275 = this;
                        goto fn__525;
                    }
                    bool parseOk__527;
                    try
                    {
                        C::Core.ToInt(val__526);
                        parseOk__527 = true;
                    }
                    catch
                    {
                        parseOk__527 = false;
                    }
                    if (!parseOk__527)
                    {
                        G::IList<ChangesetError> eb__528 = L::Enumerable.ToList(this._errors__492);
                        C::Listed.Add(eb__528, new ChangesetError(field__524.SqlValue, "must be an integer"));
                        t___4980 = this._tableDef__489;
                        t___4981 = this._params__490;
                        t___4982 = this._changes__491;
                        t___8678 = C::Listed.ToReadOnlyList(eb__528);
                        return__275 = new ChangesetImpl(t___4980, t___4981, t___4982, t___8678, false);
                        goto fn__525;
                    }
                    return__275 = this;
                }
                fn__525:
                {
                }
            }
            return return__275;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__530)
        {
            IChangeset return__276;
            string t___8662;
            G::IReadOnlyList<ChangesetError> t___8669;
            TableDef t___4967;
            G::IReadOnlyDictionary<string, string> t___4968;
            G::IReadOnlyDictionary<string, string> t___4969;
            {
                {
                    if (!this._isValid__493)
                    {
                        return__276 = this;
                        goto fn__531;
                    }
                    t___8662 = field__530.SqlValue;
                    string val__532 = C::Mapped.GetOrDefault(this._changes__491, t___8662, "");
                    if (string.IsNullOrEmpty(val__532))
                    {
                        return__276 = this;
                        goto fn__531;
                    }
                    bool parseOk__533;
                    try
                    {
                        C::Core.ToInt64(val__532);
                        parseOk__533 = true;
                    }
                    catch
                    {
                        parseOk__533 = false;
                    }
                    if (!parseOk__533)
                    {
                        G::IList<ChangesetError> eb__534 = L::Enumerable.ToList(this._errors__492);
                        C::Listed.Add(eb__534, new ChangesetError(field__530.SqlValue, "must be a 64-bit integer"));
                        t___4967 = this._tableDef__489;
                        t___4968 = this._params__490;
                        t___4969 = this._changes__491;
                        t___8669 = C::Listed.ToReadOnlyList(eb__534);
                        return__276 = new ChangesetImpl(t___4967, t___4968, t___4969, t___8669, false);
                        goto fn__531;
                    }
                    return__276 = this;
                }
                fn__531:
                {
                }
            }
            return return__276;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__536)
        {
            IChangeset return__277;
            string t___8653;
            G::IReadOnlyList<ChangesetError> t___8660;
            TableDef t___4954;
            G::IReadOnlyDictionary<string, string> t___4955;
            G::IReadOnlyDictionary<string, string> t___4956;
            {
                {
                    if (!this._isValid__493)
                    {
                        return__277 = this;
                        goto fn__537;
                    }
                    t___8653 = field__536.SqlValue;
                    string val__538 = C::Mapped.GetOrDefault(this._changes__491, t___8653, "");
                    if (string.IsNullOrEmpty(val__538))
                    {
                        return__277 = this;
                        goto fn__537;
                    }
                    bool parseOk__539;
                    try
                    {
                        C::Float64.ToFloat64(val__538);
                        parseOk__539 = true;
                    }
                    catch
                    {
                        parseOk__539 = false;
                    }
                    if (!parseOk__539)
                    {
                        G::IList<ChangesetError> eb__540 = L::Enumerable.ToList(this._errors__492);
                        C::Listed.Add(eb__540, new ChangesetError(field__536.SqlValue, "must be a number"));
                        t___4954 = this._tableDef__489;
                        t___4955 = this._params__490;
                        t___4956 = this._changes__491;
                        t___8660 = C::Listed.ToReadOnlyList(eb__540);
                        return__277 = new ChangesetImpl(t___4954, t___4955, t___4956, t___8660, false);
                        goto fn__537;
                    }
                    return__277 = this;
                }
                fn__537:
                {
                }
            }
            return return__277;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__542)
        {
            IChangeset return__278;
            string t___8644;
            G::IReadOnlyList<ChangesetError> t___8651;
            bool t___4929;
            bool t___4930;
            bool t___4932;
            bool t___4933;
            bool t___4935;
            TableDef t___4941;
            G::IReadOnlyDictionary<string, string> t___4942;
            G::IReadOnlyDictionary<string, string> t___4943;
            {
                {
                    if (!this._isValid__493)
                    {
                        return__278 = this;
                        goto fn__543;
                    }
                    t___8644 = field__542.SqlValue;
                    string val__544 = C::Mapped.GetOrDefault(this._changes__491, t___8644, "");
                    if (string.IsNullOrEmpty(val__544))
                    {
                        return__278 = this;
                        goto fn__543;
                    }
                    bool isTrue__545;
                    if (val__544 == "true")
                    {
                        isTrue__545 = true;
                    }
                    else
                    {
                        if (val__544 == "1")
                        {
                            t___4930 = true;
                        }
                        else
                        {
                            if (val__544 == "yes")
                            {
                                t___4929 = true;
                            }
                            else
                            {
                                t___4929 = val__544 == "on";
                            }
                            t___4930 = t___4929;
                        }
                        isTrue__545 = t___4930;
                    }
                    bool isFalse__546;
                    if (val__544 == "false")
                    {
                        isFalse__546 = true;
                    }
                    else
                    {
                        if (val__544 == "0")
                        {
                            t___4933 = true;
                        }
                        else
                        {
                            if (val__544 == "no")
                            {
                                t___4932 = true;
                            }
                            else
                            {
                                t___4932 = val__544 == "off";
                            }
                            t___4933 = t___4932;
                        }
                        isFalse__546 = t___4933;
                    }
                    if (!isTrue__545)
                    {
                        t___4935 = !isFalse__546;
                    }
                    else
                    {
                        t___4935 = false;
                    }
                    if (t___4935)
                    {
                        G::IList<ChangesetError> eb__547 = L::Enumerable.ToList(this._errors__492);
                        C::Listed.Add(eb__547, new ChangesetError(field__542.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___4941 = this._tableDef__489;
                        t___4942 = this._params__490;
                        t___4943 = this._changes__491;
                        t___8651 = C::Listed.ToReadOnlyList(eb__547);
                        return__278 = new ChangesetImpl(t___4941, t___4942, t___4943, t___8651, false);
                        goto fn__543;
                    }
                    return__278 = this;
                }
                fn__543:
                {
                }
            }
            return return__278;
        }
        SqlBoolean ParseBoolSqlPart(string val__549)
        {
            SqlBoolean return__279;
            bool t___4918;
            bool t___4919;
            bool t___4920;
            bool t___4922;
            bool t___4923;
            bool t___4924;
            {
                {
                    if (val__549 == "true")
                    {
                        t___4920 = true;
                    }
                    else
                    {
                        if (val__549 == "1")
                        {
                            t___4919 = true;
                        }
                        else
                        {
                            if (val__549 == "yes")
                            {
                                t___4918 = true;
                            }
                            else
                            {
                                t___4918 = val__549 == "on";
                            }
                            t___4919 = t___4918;
                        }
                        t___4920 = t___4919;
                    }
                    if (t___4920)
                    {
                        return__279 = new SqlBoolean(true);
                        goto fn__550;
                    }
                    if (val__549 == "false")
                    {
                        t___4924 = true;
                    }
                    else
                    {
                        if (val__549 == "0")
                        {
                            t___4923 = true;
                        }
                        else
                        {
                            if (val__549 == "no")
                            {
                                t___4922 = true;
                            }
                            else
                            {
                                t___4922 = val__549 == "off";
                            }
                            t___4923 = t___4922;
                        }
                        t___4924 = t___4923;
                    }
                    if (t___4924)
                    {
                        return__279 = new SqlBoolean(false);
                        goto fn__550;
                    }
                    throw new S::Exception();
                }
                fn__550:
                {
                }
            }
            return return__279;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__552, string val__553)
        {
            ISqlPart return__280;
            int t___4905;
            long t___4908;
            double t___4911;
            S::DateTime t___4916;
            {
                {
                    IFieldType ft__555 = fieldDef__552.FieldType;
                    if (ft__555 is StringField)
                    {
                        return__280 = new SqlString(val__553);
                        goto fn__554;
                    }
                    if (ft__555 is IntField)
                    {
                        t___4905 = C::Core.ToInt(val__553);
                        return__280 = new SqlInt32(t___4905);
                        goto fn__554;
                    }
                    if (ft__555 is Int64_Field)
                    {
                        t___4908 = C::Core.ToInt64(val__553);
                        return__280 = new SqlInt64(t___4908);
                        goto fn__554;
                    }
                    if (ft__555 is FloatField)
                    {
                        t___4911 = C::Float64.ToFloat64(val__553);
                        return__280 = new SqlFloat64(t___4911);
                        goto fn__554;
                    }
                    if (ft__555 is BoolField)
                    {
                        return__280 = this.ParseBoolSqlPart(val__553);
                        goto fn__554;
                    }
                    if (ft__555 is DateField)
                    {
                        t___4916 = T::TemporalSupport.FromIsoString(val__553);
                        return__280 = new SqlDate(t___4916);
                        goto fn__554;
                    }
                    throw new S::Exception();
                }
                fn__554:
                {
                }
            }
            return return__280;
        }
        public SqlFragment ToInsertSql()
        {
            int t___8592;
            string t___8597;
            bool t___8598;
            int t___8603;
            string t___8605;
            string t___8609;
            int t___8624;
            bool t___4869;
            FieldDef t___4877;
            ISqlPart t___4882;
            if (!this._isValid__493) throw new S::Exception();
            int i__558 = 0;
            while (true)
            {
                t___8592 = this._tableDef__489.Fields.Count;
                if (!(i__558 < t___8592)) break;
                FieldDef f__559 = this._tableDef__489.Fields[i__558];
                if (!f__559.Nullable)
                {
                    t___8597 = f__559.Name.SqlValue;
                    t___8598 = C::Mapped.ContainsKey(this._changes__491, t___8597);
                    t___4869 = !t___8598;
                }
                else
                {
                    t___4869 = false;
                }
                if (t___4869) throw new S::Exception();
                i__558 = i__558 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__560 = C::Mapped.ToList(this._changes__491);
            if (pairs__560.Count == 0) throw new S::Exception();
            G::IList<string> colNames__561 = new G::List<string>();
            G::IList<ISqlPart> valParts__562 = new G::List<ISqlPart>();
            int i__563 = 0;
            while (true)
            {
                t___8603 = pairs__560.Count;
                if (!(i__563 < t___8603)) break;
                G::KeyValuePair<string, string> pair__564 = pairs__560[i__563];
                t___8605 = pair__564.Key;
                t___4877 = this._tableDef__489.Field(t___8605);
                FieldDef fd__565 = t___4877;
                C::Listed.Add(colNames__561, fd__565.Name.SqlValue);
                t___8609 = pair__564.Value;
                t___4882 = this.ValueToSqlPart(fd__565, t___8609);
                C::Listed.Add(valParts__562, t___4882);
                i__563 = i__563 + 1;
            }
            SqlBuilder b__566 = new SqlBuilder();
            b__566.AppendSafe("INSERT INTO ");
            b__566.AppendSafe(this._tableDef__489.TableName.SqlValue);
            b__566.AppendSafe(" (");
            G::IReadOnlyList<string> t___8617 = C::Listed.ToReadOnlyList(colNames__561);
            string fn__8590(string c__567)
            {
                return c__567;
            }
            b__566.AppendSafe(C::Listed.Join(t___8617, ", ", (S::Func<string, string>) fn__8590));
            b__566.AppendSafe(") VALUES (");
            b__566.AppendPart(valParts__562[0]);
            int j__568 = 1;
            while (true)
            {
                t___8624 = valParts__562.Count;
                if (!(j__568 < t___8624)) break;
                b__566.AppendSafe(", ");
                b__566.AppendPart(valParts__562[j__568]);
                j__568 = j__568 + 1;
            }
            b__566.AppendSafe(")");
            return b__566.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__570)
        {
            int t___8577;
            string t___8580;
            string t___8585;
            FieldDef t___4850;
            ISqlPart t___4856;
            if (!this._isValid__493) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__572 = C::Mapped.ToList(this._changes__491);
            if (pairs__572.Count == 0) throw new S::Exception();
            SqlBuilder b__573 = new SqlBuilder();
            b__573.AppendSafe("UPDATE ");
            b__573.AppendSafe(this._tableDef__489.TableName.SqlValue);
            b__573.AppendSafe(" SET ");
            int i__574 = 0;
            while (true)
            {
                t___8577 = pairs__572.Count;
                if (!(i__574 < t___8577)) break;
                if (i__574 > 0) b__573.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__575 = pairs__572[i__574];
                t___8580 = pair__575.Key;
                t___4850 = this._tableDef__489.Field(t___8580);
                FieldDef fd__576 = t___4850;
                b__573.AppendSafe(fd__576.Name.SqlValue);
                b__573.AppendSafe(" = ");
                t___8585 = pair__575.Value;
                t___4856 = this.ValueToSqlPart(fd__576, t___8585);
                b__573.AppendPart(t___4856);
                i__574 = i__574 + 1;
            }
            b__573.AppendSafe(" WHERE id = ");
            b__573.AppendInt32(id__570);
            return b__573.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__578, G::IReadOnlyDictionary<string, string> _params__579, G::IReadOnlyDictionary<string, string> _changes__580, G::IReadOnlyList<ChangesetError> _errors__581, bool _isValid__582)
        {
            this._tableDef__489 = _tableDef__578;
            this._params__490 = _params__579;
            this._changes__491 = _changes__580;
            this._errors__492 = _errors__581;
            this._isValid__493 = _isValid__582;
        }
    }
}
