using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__390;
        readonly G::IReadOnlyDictionary<string, string> _params__391;
        readonly G::IReadOnlyDictionary<string, string> _changes__392;
        readonly G::IReadOnlyList<ChangesetError> _errors__393;
        readonly bool _isValid__394;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__390;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__392;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__393;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__394;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__404)
        {
            G::IDictionary<string, string> mb__406 = new C::OrderedDictionary<string, string>();
            void fn__5700(ISafeIdentifier f__407)
            {
                string t___5698;
                string t___5695 = f__407.SqlValue;
                string val__408 = C::Mapped.GetOrDefault(this._params__391, t___5695, "");
                if (!string.IsNullOrEmpty(val__408))
                {
                    t___5698 = f__407.SqlValue;
                    mb__406[t___5698] = val__408;
                }
            }
            C::Listed.ForEach(allowedFields__404, (S::Action<ISafeIdentifier>) fn__5700);
            return new ChangesetImpl(this._tableDef__390, this._params__391, C::Mapped.ToMap(mb__406), this._errors__393, this._isValid__394);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__410)
        {
            IChangeset return__210;
            G::IReadOnlyList<ChangesetError> t___5693;
            TableDef t___3382;
            G::IReadOnlyDictionary<string, string> t___3383;
            G::IReadOnlyDictionary<string, string> t___3384;
            {
                {
                    if (!this._isValid__394)
                    {
                        return__210 = this;
                        goto fn__411;
                    }
                    G::IList<ChangesetError> eb__412 = L::Enumerable.ToList(this._errors__393);
                    bool valid__413 = true;
                    void fn__5689(ISafeIdentifier f__414)
                    {
                        ChangesetError t___5687;
                        string t___5684 = f__414.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__392, t___5684))
                        {
                            t___5687 = new ChangesetError(f__414.SqlValue, "is required");
                            C::Listed.Add(eb__412, t___5687);
                            valid__413 = false;
                        }
                    }
                    C::Listed.ForEach(fields__410, (S::Action<ISafeIdentifier>) fn__5689);
                    t___3382 = this._tableDef__390;
                    t___3383 = this._params__391;
                    t___3384 = this._changes__392;
                    t___5693 = C::Listed.ToReadOnlyList(eb__412);
                    return__210 = new ChangesetImpl(t___3382, t___3383, t___3384, t___5693, valid__413);
                }
                fn__411:
                {
                }
            }
            return return__210;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__416, int min__417, int max__418)
        {
            IChangeset return__211;
            string t___5671;
            G::IReadOnlyList<ChangesetError> t___5682;
            bool t___3365;
            TableDef t___3371;
            G::IReadOnlyDictionary<string, string> t___3372;
            G::IReadOnlyDictionary<string, string> t___3373;
            {
                {
                    if (!this._isValid__394)
                    {
                        return__211 = this;
                        goto fn__419;
                    }
                    t___5671 = field__416.SqlValue;
                    string val__420 = C::Mapped.GetOrDefault(this._changes__392, t___5671, "");
                    int len__421 = C::StringUtil.CountBetween(val__420, 0, val__420.Length);
                    if (len__421 < min__417)
                    {
                        t___3365 = true;
                    }
                    else
                    {
                        t___3365 = len__421 > max__418;
                    }
                    if (t___3365)
                    {
                        string msg__422 = "must be between " + S::Convert.ToString(min__417) + " and " + S::Convert.ToString(max__418) + " characters";
                        G::IList<ChangesetError> eb__423 = L::Enumerable.ToList(this._errors__393);
                        C::Listed.Add(eb__423, new ChangesetError(field__416.SqlValue, msg__422));
                        t___3371 = this._tableDef__390;
                        t___3372 = this._params__391;
                        t___3373 = this._changes__392;
                        t___5682 = C::Listed.ToReadOnlyList(eb__423);
                        return__211 = new ChangesetImpl(t___3371, t___3372, t___3373, t___5682, false);
                        goto fn__419;
                    }
                    return__211 = this;
                }
                fn__419:
                {
                }
            }
            return return__211;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__425)
        {
            IChangeset return__212;
            string t___5662;
            G::IReadOnlyList<ChangesetError> t___5669;
            TableDef t___3356;
            G::IReadOnlyDictionary<string, string> t___3357;
            G::IReadOnlyDictionary<string, string> t___3358;
            {
                {
                    if (!this._isValid__394)
                    {
                        return__212 = this;
                        goto fn__426;
                    }
                    t___5662 = field__425.SqlValue;
                    string val__427 = C::Mapped.GetOrDefault(this._changes__392, t___5662, "");
                    if (string.IsNullOrEmpty(val__427))
                    {
                        return__212 = this;
                        goto fn__426;
                    }
                    bool parseOk__428;
                    try
                    {
                        C::Core.ToInt(val__427);
                        parseOk__428 = true;
                    }
                    catch
                    {
                        parseOk__428 = false;
                    }
                    if (!parseOk__428)
                    {
                        G::IList<ChangesetError> eb__429 = L::Enumerable.ToList(this._errors__393);
                        C::Listed.Add(eb__429, new ChangesetError(field__425.SqlValue, "must be an integer"));
                        t___3356 = this._tableDef__390;
                        t___3357 = this._params__391;
                        t___3358 = this._changes__392;
                        t___5669 = C::Listed.ToReadOnlyList(eb__429);
                        return__212 = new ChangesetImpl(t___3356, t___3357, t___3358, t___5669, false);
                        goto fn__426;
                    }
                    return__212 = this;
                }
                fn__426:
                {
                }
            }
            return return__212;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__431)
        {
            IChangeset return__213;
            string t___5653;
            G::IReadOnlyList<ChangesetError> t___5660;
            TableDef t___3343;
            G::IReadOnlyDictionary<string, string> t___3344;
            G::IReadOnlyDictionary<string, string> t___3345;
            {
                {
                    if (!this._isValid__394)
                    {
                        return__213 = this;
                        goto fn__432;
                    }
                    t___5653 = field__431.SqlValue;
                    string val__433 = C::Mapped.GetOrDefault(this._changes__392, t___5653, "");
                    if (string.IsNullOrEmpty(val__433))
                    {
                        return__213 = this;
                        goto fn__432;
                    }
                    bool parseOk__434;
                    try
                    {
                        C::Core.ToInt64(val__433);
                        parseOk__434 = true;
                    }
                    catch
                    {
                        parseOk__434 = false;
                    }
                    if (!parseOk__434)
                    {
                        G::IList<ChangesetError> eb__435 = L::Enumerable.ToList(this._errors__393);
                        C::Listed.Add(eb__435, new ChangesetError(field__431.SqlValue, "must be a 64-bit integer"));
                        t___3343 = this._tableDef__390;
                        t___3344 = this._params__391;
                        t___3345 = this._changes__392;
                        t___5660 = C::Listed.ToReadOnlyList(eb__435);
                        return__213 = new ChangesetImpl(t___3343, t___3344, t___3345, t___5660, false);
                        goto fn__432;
                    }
                    return__213 = this;
                }
                fn__432:
                {
                }
            }
            return return__213;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__437)
        {
            IChangeset return__214;
            string t___5644;
            G::IReadOnlyList<ChangesetError> t___5651;
            TableDef t___3330;
            G::IReadOnlyDictionary<string, string> t___3331;
            G::IReadOnlyDictionary<string, string> t___3332;
            {
                {
                    if (!this._isValid__394)
                    {
                        return__214 = this;
                        goto fn__438;
                    }
                    t___5644 = field__437.SqlValue;
                    string val__439 = C::Mapped.GetOrDefault(this._changes__392, t___5644, "");
                    if (string.IsNullOrEmpty(val__439))
                    {
                        return__214 = this;
                        goto fn__438;
                    }
                    bool parseOk__440;
                    try
                    {
                        C::Float64.ToFloat64(val__439);
                        parseOk__440 = true;
                    }
                    catch
                    {
                        parseOk__440 = false;
                    }
                    if (!parseOk__440)
                    {
                        G::IList<ChangesetError> eb__441 = L::Enumerable.ToList(this._errors__393);
                        C::Listed.Add(eb__441, new ChangesetError(field__437.SqlValue, "must be a number"));
                        t___3330 = this._tableDef__390;
                        t___3331 = this._params__391;
                        t___3332 = this._changes__392;
                        t___5651 = C::Listed.ToReadOnlyList(eb__441);
                        return__214 = new ChangesetImpl(t___3330, t___3331, t___3332, t___5651, false);
                        goto fn__438;
                    }
                    return__214 = this;
                }
                fn__438:
                {
                }
            }
            return return__214;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__443)
        {
            IChangeset return__215;
            string t___5635;
            G::IReadOnlyList<ChangesetError> t___5642;
            bool t___3305;
            bool t___3306;
            bool t___3308;
            bool t___3309;
            bool t___3311;
            TableDef t___3317;
            G::IReadOnlyDictionary<string, string> t___3318;
            G::IReadOnlyDictionary<string, string> t___3319;
            {
                {
                    if (!this._isValid__394)
                    {
                        return__215 = this;
                        goto fn__444;
                    }
                    t___5635 = field__443.SqlValue;
                    string val__445 = C::Mapped.GetOrDefault(this._changes__392, t___5635, "");
                    if (string.IsNullOrEmpty(val__445))
                    {
                        return__215 = this;
                        goto fn__444;
                    }
                    bool isTrue__446;
                    if (val__445 == "true")
                    {
                        isTrue__446 = true;
                    }
                    else
                    {
                        if (val__445 == "1")
                        {
                            t___3306 = true;
                        }
                        else
                        {
                            if (val__445 == "yes")
                            {
                                t___3305 = true;
                            }
                            else
                            {
                                t___3305 = val__445 == "on";
                            }
                            t___3306 = t___3305;
                        }
                        isTrue__446 = t___3306;
                    }
                    bool isFalse__447;
                    if (val__445 == "false")
                    {
                        isFalse__447 = true;
                    }
                    else
                    {
                        if (val__445 == "0")
                        {
                            t___3309 = true;
                        }
                        else
                        {
                            if (val__445 == "no")
                            {
                                t___3308 = true;
                            }
                            else
                            {
                                t___3308 = val__445 == "off";
                            }
                            t___3309 = t___3308;
                        }
                        isFalse__447 = t___3309;
                    }
                    if (!isTrue__446)
                    {
                        t___3311 = !isFalse__447;
                    }
                    else
                    {
                        t___3311 = false;
                    }
                    if (t___3311)
                    {
                        G::IList<ChangesetError> eb__448 = L::Enumerable.ToList(this._errors__393);
                        C::Listed.Add(eb__448, new ChangesetError(field__443.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___3317 = this._tableDef__390;
                        t___3318 = this._params__391;
                        t___3319 = this._changes__392;
                        t___5642 = C::Listed.ToReadOnlyList(eb__448);
                        return__215 = new ChangesetImpl(t___3317, t___3318, t___3319, t___5642, false);
                        goto fn__444;
                    }
                    return__215 = this;
                }
                fn__444:
                {
                }
            }
            return return__215;
        }
        SqlBoolean ParseBoolSqlPart(string val__450)
        {
            SqlBoolean return__216;
            bool t___3294;
            bool t___3295;
            bool t___3296;
            bool t___3298;
            bool t___3299;
            bool t___3300;
            {
                {
                    if (val__450 == "true")
                    {
                        t___3296 = true;
                    }
                    else
                    {
                        if (val__450 == "1")
                        {
                            t___3295 = true;
                        }
                        else
                        {
                            if (val__450 == "yes")
                            {
                                t___3294 = true;
                            }
                            else
                            {
                                t___3294 = val__450 == "on";
                            }
                            t___3295 = t___3294;
                        }
                        t___3296 = t___3295;
                    }
                    if (t___3296)
                    {
                        return__216 = new SqlBoolean(true);
                        goto fn__451;
                    }
                    if (val__450 == "false")
                    {
                        t___3300 = true;
                    }
                    else
                    {
                        if (val__450 == "0")
                        {
                            t___3299 = true;
                        }
                        else
                        {
                            if (val__450 == "no")
                            {
                                t___3298 = true;
                            }
                            else
                            {
                                t___3298 = val__450 == "off";
                            }
                            t___3299 = t___3298;
                        }
                        t___3300 = t___3299;
                    }
                    if (t___3300)
                    {
                        return__216 = new SqlBoolean(false);
                        goto fn__451;
                    }
                    throw new S::Exception();
                }
                fn__451:
                {
                }
            }
            return return__216;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__453, string val__454)
        {
            ISqlPart return__217;
            int t___3281;
            long t___3284;
            double t___3287;
            S::DateTime t___3292;
            {
                {
                    IFieldType ft__456 = fieldDef__453.FieldType;
                    if (ft__456 is StringField)
                    {
                        return__217 = new SqlString(val__454);
                        goto fn__455;
                    }
                    if (ft__456 is IntField)
                    {
                        t___3281 = C::Core.ToInt(val__454);
                        return__217 = new SqlInt32(t___3281);
                        goto fn__455;
                    }
                    if (ft__456 is Int64_Field)
                    {
                        t___3284 = C::Core.ToInt64(val__454);
                        return__217 = new SqlInt64(t___3284);
                        goto fn__455;
                    }
                    if (ft__456 is FloatField)
                    {
                        t___3287 = C::Float64.ToFloat64(val__454);
                        return__217 = new SqlFloat64(t___3287);
                        goto fn__455;
                    }
                    if (ft__456 is BoolField)
                    {
                        return__217 = this.ParseBoolSqlPart(val__454);
                        goto fn__455;
                    }
                    if (ft__456 is DateField)
                    {
                        t___3292 = T::TemporalSupport.FromIsoString(val__454);
                        return__217 = new SqlDate(t___3292);
                        goto fn__455;
                    }
                    throw new S::Exception();
                }
                fn__455:
                {
                }
            }
            return return__217;
        }
        public SqlFragment ToInsertSql()
        {
            int t___5583;
            string t___5588;
            bool t___5589;
            int t___5594;
            string t___5596;
            string t___5600;
            int t___5615;
            bool t___3245;
            FieldDef t___3253;
            ISqlPart t___3258;
            if (!this._isValid__394) throw new S::Exception();
            int i__459 = 0;
            while (true)
            {
                t___5583 = this._tableDef__390.Fields.Count;
                if (!(i__459 < t___5583)) break;
                FieldDef f__460 = this._tableDef__390.Fields[i__459];
                if (!f__460.Nullable)
                {
                    t___5588 = f__460.Name.SqlValue;
                    t___5589 = C::Mapped.ContainsKey(this._changes__392, t___5588);
                    t___3245 = !t___5589;
                }
                else
                {
                    t___3245 = false;
                }
                if (t___3245) throw new S::Exception();
                i__459 = i__459 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__461 = C::Mapped.ToList(this._changes__392);
            if (pairs__461.Count == 0) throw new S::Exception();
            G::IList<string> colNames__462 = new G::List<string>();
            G::IList<ISqlPart> valParts__463 = new G::List<ISqlPart>();
            int i__464 = 0;
            while (true)
            {
                t___5594 = pairs__461.Count;
                if (!(i__464 < t___5594)) break;
                G::KeyValuePair<string, string> pair__465 = pairs__461[i__464];
                t___5596 = pair__465.Key;
                t___3253 = this._tableDef__390.Field(t___5596);
                FieldDef fd__466 = t___3253;
                C::Listed.Add(colNames__462, fd__466.Name.SqlValue);
                t___5600 = pair__465.Value;
                t___3258 = this.ValueToSqlPart(fd__466, t___5600);
                C::Listed.Add(valParts__463, t___3258);
                i__464 = i__464 + 1;
            }
            SqlBuilder b__467 = new SqlBuilder();
            b__467.AppendSafe("INSERT INTO ");
            b__467.AppendSafe(this._tableDef__390.TableName.SqlValue);
            b__467.AppendSafe(" (");
            G::IReadOnlyList<string> t___5608 = C::Listed.ToReadOnlyList(colNames__462);
            string fn__5581(string c__468)
            {
                return c__468;
            }
            b__467.AppendSafe(C::Listed.Join(t___5608, ", ", (S::Func<string, string>) fn__5581));
            b__467.AppendSafe(") VALUES (");
            b__467.AppendPart(valParts__463[0]);
            int j__469 = 1;
            while (true)
            {
                t___5615 = valParts__463.Count;
                if (!(j__469 < t___5615)) break;
                b__467.AppendSafe(", ");
                b__467.AppendPart(valParts__463[j__469]);
                j__469 = j__469 + 1;
            }
            b__467.AppendSafe(")");
            return b__467.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__471)
        {
            int t___5568;
            string t___5571;
            string t___5576;
            FieldDef t___3226;
            ISqlPart t___3232;
            if (!this._isValid__394) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__473 = C::Mapped.ToList(this._changes__392);
            if (pairs__473.Count == 0) throw new S::Exception();
            SqlBuilder b__474 = new SqlBuilder();
            b__474.AppendSafe("UPDATE ");
            b__474.AppendSafe(this._tableDef__390.TableName.SqlValue);
            b__474.AppendSafe(" SET ");
            int i__475 = 0;
            while (true)
            {
                t___5568 = pairs__473.Count;
                if (!(i__475 < t___5568)) break;
                if (i__475 > 0) b__474.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__476 = pairs__473[i__475];
                t___5571 = pair__476.Key;
                t___3226 = this._tableDef__390.Field(t___5571);
                FieldDef fd__477 = t___3226;
                b__474.AppendSafe(fd__477.Name.SqlValue);
                b__474.AppendSafe(" = ");
                t___5576 = pair__476.Value;
                t___3232 = this.ValueToSqlPart(fd__477, t___5576);
                b__474.AppendPart(t___3232);
                i__475 = i__475 + 1;
            }
            b__474.AppendSafe(" WHERE id = ");
            b__474.AppendInt32(id__471);
            return b__474.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__479, G::IReadOnlyDictionary<string, string> _params__480, G::IReadOnlyDictionary<string, string> _changes__481, G::IReadOnlyList<ChangesetError> _errors__482, bool _isValid__483)
        {
            this._tableDef__390 = _tableDef__479;
            this._params__391 = _params__480;
            this._changes__392 = _changes__481;
            this._errors__393 = _errors__482;
            this._isValid__394 = _isValid__483;
        }
    }
}
