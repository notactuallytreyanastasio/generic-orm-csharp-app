using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__507;
        readonly G::IReadOnlyDictionary<string, string> _params__508;
        readonly G::IReadOnlyDictionary<string, string> _changes__509;
        readonly G::IReadOnlyList<ChangesetError> _errors__510;
        readonly bool _isValid__511;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__507;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__509;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__510;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__511;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__521)
        {
            G::IDictionary<string, string> mb__523 = new C::OrderedDictionary<string, string>();
            void fn__9583(ISafeIdentifier f__524)
            {
                string t___9581;
                string t___9578 = f__524.SqlValue;
                string val__525 = C::Mapped.GetOrDefault(this._params__508, t___9578, "");
                if (!string.IsNullOrEmpty(val__525))
                {
                    t___9581 = f__524.SqlValue;
                    mb__523[t___9581] = val__525;
                }
            }
            C::Listed.ForEach(allowedFields__521, (S::Action<ISafeIdentifier>) fn__9583);
            return new ChangesetImpl(this._tableDef__507, this._params__508, C::Mapped.ToMap(mb__523), this._errors__510, this._isValid__511);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__527)
        {
            IChangeset return__284;
            G::IReadOnlyList<ChangesetError> t___9576;
            TableDef t___5480;
            G::IReadOnlyDictionary<string, string> t___5481;
            G::IReadOnlyDictionary<string, string> t___5482;
            {
                {
                    if (!this._isValid__511)
                    {
                        return__284 = this;
                        goto fn__528;
                    }
                    G::IList<ChangesetError> eb__529 = L::Enumerable.ToList(this._errors__510);
                    bool valid__530 = true;
                    void fn__9572(ISafeIdentifier f__531)
                    {
                        ChangesetError t___9570;
                        string t___9567 = f__531.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__509, t___9567))
                        {
                            t___9570 = new ChangesetError(f__531.SqlValue, "is required");
                            C::Listed.Add(eb__529, t___9570);
                            valid__530 = false;
                        }
                    }
                    C::Listed.ForEach(fields__527, (S::Action<ISafeIdentifier>) fn__9572);
                    t___5480 = this._tableDef__507;
                    t___5481 = this._params__508;
                    t___5482 = this._changes__509;
                    t___9576 = C::Listed.ToReadOnlyList(eb__529);
                    return__284 = new ChangesetImpl(t___5480, t___5481, t___5482, t___9576, valid__530);
                }
                fn__528:
                {
                }
            }
            return return__284;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__533, int min__534, int max__535)
        {
            IChangeset return__285;
            string t___9554;
            G::IReadOnlyList<ChangesetError> t___9565;
            bool t___5463;
            TableDef t___5469;
            G::IReadOnlyDictionary<string, string> t___5470;
            G::IReadOnlyDictionary<string, string> t___5471;
            {
                {
                    if (!this._isValid__511)
                    {
                        return__285 = this;
                        goto fn__536;
                    }
                    t___9554 = field__533.SqlValue;
                    string val__537 = C::Mapped.GetOrDefault(this._changes__509, t___9554, "");
                    int len__538 = C::StringUtil.CountBetween(val__537, 0, val__537.Length);
                    if (len__538 < min__534)
                    {
                        t___5463 = true;
                    }
                    else
                    {
                        t___5463 = len__538 > max__535;
                    }
                    if (t___5463)
                    {
                        string msg__539 = "must be between " + S::Convert.ToString(min__534) + " and " + S::Convert.ToString(max__535) + " characters";
                        G::IList<ChangesetError> eb__540 = L::Enumerable.ToList(this._errors__510);
                        C::Listed.Add(eb__540, new ChangesetError(field__533.SqlValue, msg__539));
                        t___5469 = this._tableDef__507;
                        t___5470 = this._params__508;
                        t___5471 = this._changes__509;
                        t___9565 = C::Listed.ToReadOnlyList(eb__540);
                        return__285 = new ChangesetImpl(t___5469, t___5470, t___5471, t___9565, false);
                        goto fn__536;
                    }
                    return__285 = this;
                }
                fn__536:
                {
                }
            }
            return return__285;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__542)
        {
            IChangeset return__286;
            string t___9545;
            G::IReadOnlyList<ChangesetError> t___9552;
            TableDef t___5454;
            G::IReadOnlyDictionary<string, string> t___5455;
            G::IReadOnlyDictionary<string, string> t___5456;
            {
                {
                    if (!this._isValid__511)
                    {
                        return__286 = this;
                        goto fn__543;
                    }
                    t___9545 = field__542.SqlValue;
                    string val__544 = C::Mapped.GetOrDefault(this._changes__509, t___9545, "");
                    if (string.IsNullOrEmpty(val__544))
                    {
                        return__286 = this;
                        goto fn__543;
                    }
                    bool parseOk__545;
                    try
                    {
                        C::Core.ToInt(val__544);
                        parseOk__545 = true;
                    }
                    catch
                    {
                        parseOk__545 = false;
                    }
                    if (!parseOk__545)
                    {
                        G::IList<ChangesetError> eb__546 = L::Enumerable.ToList(this._errors__510);
                        C::Listed.Add(eb__546, new ChangesetError(field__542.SqlValue, "must be an integer"));
                        t___5454 = this._tableDef__507;
                        t___5455 = this._params__508;
                        t___5456 = this._changes__509;
                        t___9552 = C::Listed.ToReadOnlyList(eb__546);
                        return__286 = new ChangesetImpl(t___5454, t___5455, t___5456, t___9552, false);
                        goto fn__543;
                    }
                    return__286 = this;
                }
                fn__543:
                {
                }
            }
            return return__286;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__548)
        {
            IChangeset return__287;
            string t___9536;
            G::IReadOnlyList<ChangesetError> t___9543;
            TableDef t___5441;
            G::IReadOnlyDictionary<string, string> t___5442;
            G::IReadOnlyDictionary<string, string> t___5443;
            {
                {
                    if (!this._isValid__511)
                    {
                        return__287 = this;
                        goto fn__549;
                    }
                    t___9536 = field__548.SqlValue;
                    string val__550 = C::Mapped.GetOrDefault(this._changes__509, t___9536, "");
                    if (string.IsNullOrEmpty(val__550))
                    {
                        return__287 = this;
                        goto fn__549;
                    }
                    bool parseOk__551;
                    try
                    {
                        C::Core.ToInt64(val__550);
                        parseOk__551 = true;
                    }
                    catch
                    {
                        parseOk__551 = false;
                    }
                    if (!parseOk__551)
                    {
                        G::IList<ChangesetError> eb__552 = L::Enumerable.ToList(this._errors__510);
                        C::Listed.Add(eb__552, new ChangesetError(field__548.SqlValue, "must be a 64-bit integer"));
                        t___5441 = this._tableDef__507;
                        t___5442 = this._params__508;
                        t___5443 = this._changes__509;
                        t___9543 = C::Listed.ToReadOnlyList(eb__552);
                        return__287 = new ChangesetImpl(t___5441, t___5442, t___5443, t___9543, false);
                        goto fn__549;
                    }
                    return__287 = this;
                }
                fn__549:
                {
                }
            }
            return return__287;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__554)
        {
            IChangeset return__288;
            string t___9527;
            G::IReadOnlyList<ChangesetError> t___9534;
            TableDef t___5428;
            G::IReadOnlyDictionary<string, string> t___5429;
            G::IReadOnlyDictionary<string, string> t___5430;
            {
                {
                    if (!this._isValid__511)
                    {
                        return__288 = this;
                        goto fn__555;
                    }
                    t___9527 = field__554.SqlValue;
                    string val__556 = C::Mapped.GetOrDefault(this._changes__509, t___9527, "");
                    if (string.IsNullOrEmpty(val__556))
                    {
                        return__288 = this;
                        goto fn__555;
                    }
                    bool parseOk__557;
                    try
                    {
                        C::Float64.ToFloat64(val__556);
                        parseOk__557 = true;
                    }
                    catch
                    {
                        parseOk__557 = false;
                    }
                    if (!parseOk__557)
                    {
                        G::IList<ChangesetError> eb__558 = L::Enumerable.ToList(this._errors__510);
                        C::Listed.Add(eb__558, new ChangesetError(field__554.SqlValue, "must be a number"));
                        t___5428 = this._tableDef__507;
                        t___5429 = this._params__508;
                        t___5430 = this._changes__509;
                        t___9534 = C::Listed.ToReadOnlyList(eb__558);
                        return__288 = new ChangesetImpl(t___5428, t___5429, t___5430, t___9534, false);
                        goto fn__555;
                    }
                    return__288 = this;
                }
                fn__555:
                {
                }
            }
            return return__288;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__560)
        {
            IChangeset return__289;
            string t___9518;
            G::IReadOnlyList<ChangesetError> t___9525;
            bool t___5403;
            bool t___5404;
            bool t___5406;
            bool t___5407;
            bool t___5409;
            TableDef t___5415;
            G::IReadOnlyDictionary<string, string> t___5416;
            G::IReadOnlyDictionary<string, string> t___5417;
            {
                {
                    if (!this._isValid__511)
                    {
                        return__289 = this;
                        goto fn__561;
                    }
                    t___9518 = field__560.SqlValue;
                    string val__562 = C::Mapped.GetOrDefault(this._changes__509, t___9518, "");
                    if (string.IsNullOrEmpty(val__562))
                    {
                        return__289 = this;
                        goto fn__561;
                    }
                    bool isTrue__563;
                    if (val__562 == "true")
                    {
                        isTrue__563 = true;
                    }
                    else
                    {
                        if (val__562 == "1")
                        {
                            t___5404 = true;
                        }
                        else
                        {
                            if (val__562 == "yes")
                            {
                                t___5403 = true;
                            }
                            else
                            {
                                t___5403 = val__562 == "on";
                            }
                            t___5404 = t___5403;
                        }
                        isTrue__563 = t___5404;
                    }
                    bool isFalse__564;
                    if (val__562 == "false")
                    {
                        isFalse__564 = true;
                    }
                    else
                    {
                        if (val__562 == "0")
                        {
                            t___5407 = true;
                        }
                        else
                        {
                            if (val__562 == "no")
                            {
                                t___5406 = true;
                            }
                            else
                            {
                                t___5406 = val__562 == "off";
                            }
                            t___5407 = t___5406;
                        }
                        isFalse__564 = t___5407;
                    }
                    if (!isTrue__563)
                    {
                        t___5409 = !isFalse__564;
                    }
                    else
                    {
                        t___5409 = false;
                    }
                    if (t___5409)
                    {
                        G::IList<ChangesetError> eb__565 = L::Enumerable.ToList(this._errors__510);
                        C::Listed.Add(eb__565, new ChangesetError(field__560.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___5415 = this._tableDef__507;
                        t___5416 = this._params__508;
                        t___5417 = this._changes__509;
                        t___9525 = C::Listed.ToReadOnlyList(eb__565);
                        return__289 = new ChangesetImpl(t___5415, t___5416, t___5417, t___9525, false);
                        goto fn__561;
                    }
                    return__289 = this;
                }
                fn__561:
                {
                }
            }
            return return__289;
        }
        SqlBoolean ParseBoolSqlPart(string val__567)
        {
            SqlBoolean return__290;
            bool t___5392;
            bool t___5393;
            bool t___5394;
            bool t___5396;
            bool t___5397;
            bool t___5398;
            {
                {
                    if (val__567 == "true")
                    {
                        t___5394 = true;
                    }
                    else
                    {
                        if (val__567 == "1")
                        {
                            t___5393 = true;
                        }
                        else
                        {
                            if (val__567 == "yes")
                            {
                                t___5392 = true;
                            }
                            else
                            {
                                t___5392 = val__567 == "on";
                            }
                            t___5393 = t___5392;
                        }
                        t___5394 = t___5393;
                    }
                    if (t___5394)
                    {
                        return__290 = new SqlBoolean(true);
                        goto fn__568;
                    }
                    if (val__567 == "false")
                    {
                        t___5398 = true;
                    }
                    else
                    {
                        if (val__567 == "0")
                        {
                            t___5397 = true;
                        }
                        else
                        {
                            if (val__567 == "no")
                            {
                                t___5396 = true;
                            }
                            else
                            {
                                t___5396 = val__567 == "off";
                            }
                            t___5397 = t___5396;
                        }
                        t___5398 = t___5397;
                    }
                    if (t___5398)
                    {
                        return__290 = new SqlBoolean(false);
                        goto fn__568;
                    }
                    throw new S::Exception();
                }
                fn__568:
                {
                }
            }
            return return__290;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__570, string val__571)
        {
            ISqlPart return__291;
            int t___5379;
            long t___5382;
            double t___5385;
            S::DateTime t___5390;
            {
                {
                    IFieldType ft__573 = fieldDef__570.FieldType;
                    if (ft__573 is StringField)
                    {
                        return__291 = new SqlString(val__571);
                        goto fn__572;
                    }
                    if (ft__573 is IntField)
                    {
                        t___5379 = C::Core.ToInt(val__571);
                        return__291 = new SqlInt32(t___5379);
                        goto fn__572;
                    }
                    if (ft__573 is Int64_Field)
                    {
                        t___5382 = C::Core.ToInt64(val__571);
                        return__291 = new SqlInt64(t___5382);
                        goto fn__572;
                    }
                    if (ft__573 is FloatField)
                    {
                        t___5385 = C::Float64.ToFloat64(val__571);
                        return__291 = new SqlFloat64(t___5385);
                        goto fn__572;
                    }
                    if (ft__573 is BoolField)
                    {
                        return__291 = this.ParseBoolSqlPart(val__571);
                        goto fn__572;
                    }
                    if (ft__573 is DateField)
                    {
                        t___5390 = T::TemporalSupport.FromIsoString(val__571);
                        return__291 = new SqlDate(t___5390);
                        goto fn__572;
                    }
                    throw new S::Exception();
                }
                fn__572:
                {
                }
            }
            return return__291;
        }
        public SqlFragment ToInsertSql()
        {
            int t___9466;
            string t___9471;
            bool t___9472;
            int t___9477;
            string t___9479;
            string t___9483;
            int t___9498;
            bool t___5343;
            FieldDef t___5351;
            ISqlPart t___5356;
            if (!this._isValid__511) throw new S::Exception();
            int i__576 = 0;
            while (true)
            {
                t___9466 = this._tableDef__507.Fields.Count;
                if (!(i__576 < t___9466)) break;
                FieldDef f__577 = this._tableDef__507.Fields[i__576];
                if (!f__577.Nullable)
                {
                    t___9471 = f__577.Name.SqlValue;
                    t___9472 = C::Mapped.ContainsKey(this._changes__509, t___9471);
                    t___5343 = !t___9472;
                }
                else
                {
                    t___5343 = false;
                }
                if (t___5343) throw new S::Exception();
                i__576 = i__576 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__578 = C::Mapped.ToList(this._changes__509);
            if (pairs__578.Count == 0) throw new S::Exception();
            G::IList<string> colNames__579 = new G::List<string>();
            G::IList<ISqlPart> valParts__580 = new G::List<ISqlPart>();
            int i__581 = 0;
            while (true)
            {
                t___9477 = pairs__578.Count;
                if (!(i__581 < t___9477)) break;
                G::KeyValuePair<string, string> pair__582 = pairs__578[i__581];
                t___9479 = pair__582.Key;
                t___5351 = this._tableDef__507.Field(t___9479);
                FieldDef fd__583 = t___5351;
                C::Listed.Add(colNames__579, fd__583.Name.SqlValue);
                t___9483 = pair__582.Value;
                t___5356 = this.ValueToSqlPart(fd__583, t___9483);
                C::Listed.Add(valParts__580, t___5356);
                i__581 = i__581 + 1;
            }
            SqlBuilder b__584 = new SqlBuilder();
            b__584.AppendSafe("INSERT INTO ");
            b__584.AppendSafe(this._tableDef__507.TableName.SqlValue);
            b__584.AppendSafe(" (");
            G::IReadOnlyList<string> t___9491 = C::Listed.ToReadOnlyList(colNames__579);
            string fn__9464(string c__585)
            {
                return c__585;
            }
            b__584.AppendSafe(C::Listed.Join(t___9491, ", ", (S::Func<string, string>) fn__9464));
            b__584.AppendSafe(") VALUES (");
            b__584.AppendPart(valParts__580[0]);
            int j__586 = 1;
            while (true)
            {
                t___9498 = valParts__580.Count;
                if (!(j__586 < t___9498)) break;
                b__584.AppendSafe(", ");
                b__584.AppendPart(valParts__580[j__586]);
                j__586 = j__586 + 1;
            }
            b__584.AppendSafe(")");
            return b__584.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__588)
        {
            int t___9451;
            string t___9454;
            string t___9459;
            FieldDef t___5324;
            ISqlPart t___5330;
            if (!this._isValid__511) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__590 = C::Mapped.ToList(this._changes__509);
            if (pairs__590.Count == 0) throw new S::Exception();
            SqlBuilder b__591 = new SqlBuilder();
            b__591.AppendSafe("UPDATE ");
            b__591.AppendSafe(this._tableDef__507.TableName.SqlValue);
            b__591.AppendSafe(" SET ");
            int i__592 = 0;
            while (true)
            {
                t___9451 = pairs__590.Count;
                if (!(i__592 < t___9451)) break;
                if (i__592 > 0) b__591.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__593 = pairs__590[i__592];
                t___9454 = pair__593.Key;
                t___5324 = this._tableDef__507.Field(t___9454);
                FieldDef fd__594 = t___5324;
                b__591.AppendSafe(fd__594.Name.SqlValue);
                b__591.AppendSafe(" = ");
                t___9459 = pair__593.Value;
                t___5330 = this.ValueToSqlPart(fd__594, t___9459);
                b__591.AppendPart(t___5330);
                i__592 = i__592 + 1;
            }
            b__591.AppendSafe(" WHERE id = ");
            b__591.AppendInt32(id__588);
            return b__591.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__596, G::IReadOnlyDictionary<string, string> _params__597, G::IReadOnlyDictionary<string, string> _changes__598, G::IReadOnlyList<ChangesetError> _errors__599, bool _isValid__600)
        {
            this._tableDef__507 = _tableDef__596;
            this._params__508 = _params__597;
            this._changes__509 = _changes__598;
            this._errors__510 = _errors__599;
            this._isValid__511 = _isValid__600;
        }
    }
}
