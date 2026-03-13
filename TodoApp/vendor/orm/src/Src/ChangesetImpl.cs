using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__592;
        readonly G::IReadOnlyDictionary<string, string> _params__593;
        readonly G::IReadOnlyDictionary<string, string> _changes__594;
        readonly G::IReadOnlyList<ChangesetError> _errors__595;
        readonly bool _isValid__596;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__592;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__594;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__595;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__596;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__606)
        {
            G::IDictionary<string, string> mb__608 = new C::OrderedDictionary<string, string>();
            void fn__11363(ISafeIdentifier f__609)
            {
                string t___11361;
                string t___11358 = f__609.SqlValue;
                string val__610 = C::Mapped.GetOrDefault(this._params__593, t___11358, "");
                if (!string.IsNullOrEmpty(val__610))
                {
                    t___11361 = f__609.SqlValue;
                    mb__608[t___11361] = val__610;
                }
            }
            C::Listed.ForEach(allowedFields__606, (S::Action<ISafeIdentifier>) fn__11363);
            return new ChangesetImpl(this._tableDef__592, this._params__593, C::Mapped.ToMap(mb__608), this._errors__595, this._isValid__596);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__612)
        {
            IChangeset return__324;
            G::IReadOnlyList<ChangesetError> t___11356;
            TableDef t___6537;
            G::IReadOnlyDictionary<string, string> t___6538;
            G::IReadOnlyDictionary<string, string> t___6539;
            {
                {
                    if (!this._isValid__596)
                    {
                        return__324 = this;
                        goto fn__613;
                    }
                    G::IList<ChangesetError> eb__614 = L::Enumerable.ToList(this._errors__595);
                    bool valid__615 = true;
                    void fn__11352(ISafeIdentifier f__616)
                    {
                        ChangesetError t___11350;
                        string t___11347 = f__616.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__594, t___11347))
                        {
                            t___11350 = new ChangesetError(f__616.SqlValue, "is required");
                            C::Listed.Add(eb__614, t___11350);
                            valid__615 = false;
                        }
                    }
                    C::Listed.ForEach(fields__612, (S::Action<ISafeIdentifier>) fn__11352);
                    t___6537 = this._tableDef__592;
                    t___6538 = this._params__593;
                    t___6539 = this._changes__594;
                    t___11356 = C::Listed.ToReadOnlyList(eb__614);
                    return__324 = new ChangesetImpl(t___6537, t___6538, t___6539, t___11356, valid__615);
                }
                fn__613:
                {
                }
            }
            return return__324;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__618, int min__619, int max__620)
        {
            IChangeset return__325;
            string t___11334;
            G::IReadOnlyList<ChangesetError> t___11345;
            bool t___6520;
            TableDef t___6526;
            G::IReadOnlyDictionary<string, string> t___6527;
            G::IReadOnlyDictionary<string, string> t___6528;
            {
                {
                    if (!this._isValid__596)
                    {
                        return__325 = this;
                        goto fn__621;
                    }
                    t___11334 = field__618.SqlValue;
                    string val__622 = C::Mapped.GetOrDefault(this._changes__594, t___11334, "");
                    int len__623 = C::StringUtil.CountBetween(val__622, 0, val__622.Length);
                    if (len__623 < min__619)
                    {
                        t___6520 = true;
                    }
                    else
                    {
                        t___6520 = len__623 > max__620;
                    }
                    if (t___6520)
                    {
                        string msg__624 = "must be between " + S::Convert.ToString(min__619) + " and " + S::Convert.ToString(max__620) + " characters";
                        G::IList<ChangesetError> eb__625 = L::Enumerable.ToList(this._errors__595);
                        C::Listed.Add(eb__625, new ChangesetError(field__618.SqlValue, msg__624));
                        t___6526 = this._tableDef__592;
                        t___6527 = this._params__593;
                        t___6528 = this._changes__594;
                        t___11345 = C::Listed.ToReadOnlyList(eb__625);
                        return__325 = new ChangesetImpl(t___6526, t___6527, t___6528, t___11345, false);
                        goto fn__621;
                    }
                    return__325 = this;
                }
                fn__621:
                {
                }
            }
            return return__325;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__627)
        {
            IChangeset return__326;
            string t___11325;
            G::IReadOnlyList<ChangesetError> t___11332;
            TableDef t___6511;
            G::IReadOnlyDictionary<string, string> t___6512;
            G::IReadOnlyDictionary<string, string> t___6513;
            {
                {
                    if (!this._isValid__596)
                    {
                        return__326 = this;
                        goto fn__628;
                    }
                    t___11325 = field__627.SqlValue;
                    string val__629 = C::Mapped.GetOrDefault(this._changes__594, t___11325, "");
                    if (string.IsNullOrEmpty(val__629))
                    {
                        return__326 = this;
                        goto fn__628;
                    }
                    bool parseOk__630;
                    try
                    {
                        C::Core.ToInt(val__629);
                        parseOk__630 = true;
                    }
                    catch
                    {
                        parseOk__630 = false;
                    }
                    if (!parseOk__630)
                    {
                        G::IList<ChangesetError> eb__631 = L::Enumerable.ToList(this._errors__595);
                        C::Listed.Add(eb__631, new ChangesetError(field__627.SqlValue, "must be an integer"));
                        t___6511 = this._tableDef__592;
                        t___6512 = this._params__593;
                        t___6513 = this._changes__594;
                        t___11332 = C::Listed.ToReadOnlyList(eb__631);
                        return__326 = new ChangesetImpl(t___6511, t___6512, t___6513, t___11332, false);
                        goto fn__628;
                    }
                    return__326 = this;
                }
                fn__628:
                {
                }
            }
            return return__326;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__633)
        {
            IChangeset return__327;
            string t___11316;
            G::IReadOnlyList<ChangesetError> t___11323;
            TableDef t___6498;
            G::IReadOnlyDictionary<string, string> t___6499;
            G::IReadOnlyDictionary<string, string> t___6500;
            {
                {
                    if (!this._isValid__596)
                    {
                        return__327 = this;
                        goto fn__634;
                    }
                    t___11316 = field__633.SqlValue;
                    string val__635 = C::Mapped.GetOrDefault(this._changes__594, t___11316, "");
                    if (string.IsNullOrEmpty(val__635))
                    {
                        return__327 = this;
                        goto fn__634;
                    }
                    bool parseOk__636;
                    try
                    {
                        C::Core.ToInt64(val__635);
                        parseOk__636 = true;
                    }
                    catch
                    {
                        parseOk__636 = false;
                    }
                    if (!parseOk__636)
                    {
                        G::IList<ChangesetError> eb__637 = L::Enumerable.ToList(this._errors__595);
                        C::Listed.Add(eb__637, new ChangesetError(field__633.SqlValue, "must be a 64-bit integer"));
                        t___6498 = this._tableDef__592;
                        t___6499 = this._params__593;
                        t___6500 = this._changes__594;
                        t___11323 = C::Listed.ToReadOnlyList(eb__637);
                        return__327 = new ChangesetImpl(t___6498, t___6499, t___6500, t___11323, false);
                        goto fn__634;
                    }
                    return__327 = this;
                }
                fn__634:
                {
                }
            }
            return return__327;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__639)
        {
            IChangeset return__328;
            string t___11307;
            G::IReadOnlyList<ChangesetError> t___11314;
            TableDef t___6485;
            G::IReadOnlyDictionary<string, string> t___6486;
            G::IReadOnlyDictionary<string, string> t___6487;
            {
                {
                    if (!this._isValid__596)
                    {
                        return__328 = this;
                        goto fn__640;
                    }
                    t___11307 = field__639.SqlValue;
                    string val__641 = C::Mapped.GetOrDefault(this._changes__594, t___11307, "");
                    if (string.IsNullOrEmpty(val__641))
                    {
                        return__328 = this;
                        goto fn__640;
                    }
                    bool parseOk__642;
                    try
                    {
                        C::Float64.ToFloat64(val__641);
                        parseOk__642 = true;
                    }
                    catch
                    {
                        parseOk__642 = false;
                    }
                    if (!parseOk__642)
                    {
                        G::IList<ChangesetError> eb__643 = L::Enumerable.ToList(this._errors__595);
                        C::Listed.Add(eb__643, new ChangesetError(field__639.SqlValue, "must be a number"));
                        t___6485 = this._tableDef__592;
                        t___6486 = this._params__593;
                        t___6487 = this._changes__594;
                        t___11314 = C::Listed.ToReadOnlyList(eb__643);
                        return__328 = new ChangesetImpl(t___6485, t___6486, t___6487, t___11314, false);
                        goto fn__640;
                    }
                    return__328 = this;
                }
                fn__640:
                {
                }
            }
            return return__328;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__645)
        {
            IChangeset return__329;
            string t___11298;
            G::IReadOnlyList<ChangesetError> t___11305;
            bool t___6460;
            bool t___6461;
            bool t___6463;
            bool t___6464;
            bool t___6466;
            TableDef t___6472;
            G::IReadOnlyDictionary<string, string> t___6473;
            G::IReadOnlyDictionary<string, string> t___6474;
            {
                {
                    if (!this._isValid__596)
                    {
                        return__329 = this;
                        goto fn__646;
                    }
                    t___11298 = field__645.SqlValue;
                    string val__647 = C::Mapped.GetOrDefault(this._changes__594, t___11298, "");
                    if (string.IsNullOrEmpty(val__647))
                    {
                        return__329 = this;
                        goto fn__646;
                    }
                    bool isTrue__648;
                    if (val__647 == "true")
                    {
                        isTrue__648 = true;
                    }
                    else
                    {
                        if (val__647 == "1")
                        {
                            t___6461 = true;
                        }
                        else
                        {
                            if (val__647 == "yes")
                            {
                                t___6460 = true;
                            }
                            else
                            {
                                t___6460 = val__647 == "on";
                            }
                            t___6461 = t___6460;
                        }
                        isTrue__648 = t___6461;
                    }
                    bool isFalse__649;
                    if (val__647 == "false")
                    {
                        isFalse__649 = true;
                    }
                    else
                    {
                        if (val__647 == "0")
                        {
                            t___6464 = true;
                        }
                        else
                        {
                            if (val__647 == "no")
                            {
                                t___6463 = true;
                            }
                            else
                            {
                                t___6463 = val__647 == "off";
                            }
                            t___6464 = t___6463;
                        }
                        isFalse__649 = t___6464;
                    }
                    if (!isTrue__648)
                    {
                        t___6466 = !isFalse__649;
                    }
                    else
                    {
                        t___6466 = false;
                    }
                    if (t___6466)
                    {
                        G::IList<ChangesetError> eb__650 = L::Enumerable.ToList(this._errors__595);
                        C::Listed.Add(eb__650, new ChangesetError(field__645.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___6472 = this._tableDef__592;
                        t___6473 = this._params__593;
                        t___6474 = this._changes__594;
                        t___11305 = C::Listed.ToReadOnlyList(eb__650);
                        return__329 = new ChangesetImpl(t___6472, t___6473, t___6474, t___11305, false);
                        goto fn__646;
                    }
                    return__329 = this;
                }
                fn__646:
                {
                }
            }
            return return__329;
        }
        SqlBoolean ParseBoolSqlPart(string val__652)
        {
            SqlBoolean return__330;
            bool t___6449;
            bool t___6450;
            bool t___6451;
            bool t___6453;
            bool t___6454;
            bool t___6455;
            {
                {
                    if (val__652 == "true")
                    {
                        t___6451 = true;
                    }
                    else
                    {
                        if (val__652 == "1")
                        {
                            t___6450 = true;
                        }
                        else
                        {
                            if (val__652 == "yes")
                            {
                                t___6449 = true;
                            }
                            else
                            {
                                t___6449 = val__652 == "on";
                            }
                            t___6450 = t___6449;
                        }
                        t___6451 = t___6450;
                    }
                    if (t___6451)
                    {
                        return__330 = new SqlBoolean(true);
                        goto fn__653;
                    }
                    if (val__652 == "false")
                    {
                        t___6455 = true;
                    }
                    else
                    {
                        if (val__652 == "0")
                        {
                            t___6454 = true;
                        }
                        else
                        {
                            if (val__652 == "no")
                            {
                                t___6453 = true;
                            }
                            else
                            {
                                t___6453 = val__652 == "off";
                            }
                            t___6454 = t___6453;
                        }
                        t___6455 = t___6454;
                    }
                    if (t___6455)
                    {
                        return__330 = new SqlBoolean(false);
                        goto fn__653;
                    }
                    throw new S::Exception();
                }
                fn__653:
                {
                }
            }
            return return__330;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__655, string val__656)
        {
            ISqlPart return__331;
            int t___6436;
            long t___6439;
            double t___6442;
            S::DateTime t___6447;
            {
                {
                    IFieldType ft__658 = fieldDef__655.FieldType;
                    if (ft__658 is StringField)
                    {
                        return__331 = new SqlString(val__656);
                        goto fn__657;
                    }
                    if (ft__658 is IntField)
                    {
                        t___6436 = C::Core.ToInt(val__656);
                        return__331 = new SqlInt32(t___6436);
                        goto fn__657;
                    }
                    if (ft__658 is Int64_Field)
                    {
                        t___6439 = C::Core.ToInt64(val__656);
                        return__331 = new SqlInt64(t___6439);
                        goto fn__657;
                    }
                    if (ft__658 is FloatField)
                    {
                        t___6442 = C::Float64.ToFloat64(val__656);
                        return__331 = new SqlFloat64(t___6442);
                        goto fn__657;
                    }
                    if (ft__658 is BoolField)
                    {
                        return__331 = this.ParseBoolSqlPart(val__656);
                        goto fn__657;
                    }
                    if (ft__658 is DateField)
                    {
                        t___6447 = T::TemporalSupport.FromIsoString(val__656);
                        return__331 = new SqlDate(t___6447);
                        goto fn__657;
                    }
                    throw new S::Exception();
                }
                fn__657:
                {
                }
            }
            return return__331;
        }
        public SqlFragment ToInsertSql()
        {
            int t___11246;
            string t___11251;
            bool t___11252;
            int t___11257;
            string t___11259;
            string t___11263;
            int t___11278;
            bool t___6400;
            FieldDef t___6408;
            ISqlPart t___6413;
            if (!this._isValid__596) throw new S::Exception();
            int i__661 = 0;
            while (true)
            {
                t___11246 = this._tableDef__592.Fields.Count;
                if (!(i__661 < t___11246)) break;
                FieldDef f__662 = this._tableDef__592.Fields[i__661];
                if (!f__662.Nullable)
                {
                    t___11251 = f__662.Name.SqlValue;
                    t___11252 = C::Mapped.ContainsKey(this._changes__594, t___11251);
                    t___6400 = !t___11252;
                }
                else
                {
                    t___6400 = false;
                }
                if (t___6400) throw new S::Exception();
                i__661 = i__661 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__663 = C::Mapped.ToList(this._changes__594);
            if (pairs__663.Count == 0) throw new S::Exception();
            G::IList<string> colNames__664 = new G::List<string>();
            G::IList<ISqlPart> valParts__665 = new G::List<ISqlPart>();
            int i__666 = 0;
            while (true)
            {
                t___11257 = pairs__663.Count;
                if (!(i__666 < t___11257)) break;
                G::KeyValuePair<string, string> pair__667 = pairs__663[i__666];
                t___11259 = pair__667.Key;
                t___6408 = this._tableDef__592.Field(t___11259);
                FieldDef fd__668 = t___6408;
                C::Listed.Add(colNames__664, fd__668.Name.SqlValue);
                t___11263 = pair__667.Value;
                t___6413 = this.ValueToSqlPart(fd__668, t___11263);
                C::Listed.Add(valParts__665, t___6413);
                i__666 = i__666 + 1;
            }
            SqlBuilder b__669 = new SqlBuilder();
            b__669.AppendSafe("INSERT INTO ");
            b__669.AppendSafe(this._tableDef__592.TableName.SqlValue);
            b__669.AppendSafe(" (");
            G::IReadOnlyList<string> t___11271 = C::Listed.ToReadOnlyList(colNames__664);
            string fn__11244(string c__670)
            {
                return c__670;
            }
            b__669.AppendSafe(C::Listed.Join(t___11271, ", ", (S::Func<string, string>) fn__11244));
            b__669.AppendSafe(") VALUES (");
            b__669.AppendPart(valParts__665[0]);
            int j__671 = 1;
            while (true)
            {
                t___11278 = valParts__665.Count;
                if (!(j__671 < t___11278)) break;
                b__669.AppendSafe(", ");
                b__669.AppendPart(valParts__665[j__671]);
                j__671 = j__671 + 1;
            }
            b__669.AppendSafe(")");
            return b__669.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__673)
        {
            int t___11231;
            string t___11234;
            string t___11239;
            FieldDef t___6381;
            ISqlPart t___6387;
            if (!this._isValid__596) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__675 = C::Mapped.ToList(this._changes__594);
            if (pairs__675.Count == 0) throw new S::Exception();
            SqlBuilder b__676 = new SqlBuilder();
            b__676.AppendSafe("UPDATE ");
            b__676.AppendSafe(this._tableDef__592.TableName.SqlValue);
            b__676.AppendSafe(" SET ");
            int i__677 = 0;
            while (true)
            {
                t___11231 = pairs__675.Count;
                if (!(i__677 < t___11231)) break;
                if (i__677 > 0) b__676.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__678 = pairs__675[i__677];
                t___11234 = pair__678.Key;
                t___6381 = this._tableDef__592.Field(t___11234);
                FieldDef fd__679 = t___6381;
                b__676.AppendSafe(fd__679.Name.SqlValue);
                b__676.AppendSafe(" = ");
                t___11239 = pair__678.Value;
                t___6387 = this.ValueToSqlPart(fd__679, t___11239);
                b__676.AppendPart(t___6387);
                i__677 = i__677 + 1;
            }
            b__676.AppendSafe(" WHERE id = ");
            b__676.AppendInt32(id__673);
            return b__676.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__681, G::IReadOnlyDictionary<string, string> _params__682, G::IReadOnlyDictionary<string, string> _changes__683, G::IReadOnlyList<ChangesetError> _errors__684, bool _isValid__685)
        {
            this._tableDef__592 = _tableDef__681;
            this._params__593 = _params__682;
            this._changes__594 = _changes__683;
            this._errors__595 = _errors__684;
            this._isValid__596 = _isValid__685;
        }
    }
}
