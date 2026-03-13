using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__350;
        readonly G::IReadOnlyDictionary<string, string> _params__351;
        readonly G::IReadOnlyDictionary<string, string> _changes__352;
        readonly G::IReadOnlyList<ChangesetError> _errors__353;
        readonly bool _isValid__354;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__350;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__352;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__353;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__354;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__364)
        {
            G::IDictionary<string, string> mb__366 = new C::OrderedDictionary<string, string>();
            void fn__5014(ISafeIdentifier f__367)
            {
                string t___5012;
                string t___5009 = f__367.SqlValue;
                string val__368 = C::Mapped.GetOrDefault(this._params__351, t___5009, "");
                if (!string.IsNullOrEmpty(val__368))
                {
                    t___5012 = f__367.SqlValue;
                    mb__366[t___5012] = val__368;
                }
            }
            C::Listed.ForEach(allowedFields__364, (S::Action<ISafeIdentifier>) fn__5014);
            return new ChangesetImpl(this._tableDef__350, this._params__351, C::Mapped.ToMap(mb__366), this._errors__353, this._isValid__354);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__370)
        {
            IChangeset return__192;
            G::IReadOnlyList<ChangesetError> t___5007;
            TableDef t___2977;
            G::IReadOnlyDictionary<string, string> t___2978;
            G::IReadOnlyDictionary<string, string> t___2979;
            {
                {
                    if (!this._isValid__354)
                    {
                        return__192 = this;
                        goto fn__371;
                    }
                    G::IList<ChangesetError> eb__372 = L::Enumerable.ToList(this._errors__353);
                    bool valid__373 = true;
                    void fn__5003(ISafeIdentifier f__374)
                    {
                        ChangesetError t___5001;
                        string t___4998 = f__374.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__352, t___4998))
                        {
                            t___5001 = new ChangesetError(f__374.SqlValue, "is required");
                            C::Listed.Add(eb__372, t___5001);
                            valid__373 = false;
                        }
                    }
                    C::Listed.ForEach(fields__370, (S::Action<ISafeIdentifier>) fn__5003);
                    t___2977 = this._tableDef__350;
                    t___2978 = this._params__351;
                    t___2979 = this._changes__352;
                    t___5007 = C::Listed.ToReadOnlyList(eb__372);
                    return__192 = new ChangesetImpl(t___2977, t___2978, t___2979, t___5007, valid__373);
                }
                fn__371:
                {
                }
            }
            return return__192;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__376, int min__377, int max__378)
        {
            IChangeset return__193;
            string t___4985;
            G::IReadOnlyList<ChangesetError> t___4996;
            bool t___2960;
            TableDef t___2966;
            G::IReadOnlyDictionary<string, string> t___2967;
            G::IReadOnlyDictionary<string, string> t___2968;
            {
                {
                    if (!this._isValid__354)
                    {
                        return__193 = this;
                        goto fn__379;
                    }
                    t___4985 = field__376.SqlValue;
                    string val__380 = C::Mapped.GetOrDefault(this._changes__352, t___4985, "");
                    int len__381 = C::StringUtil.CountBetween(val__380, 0, val__380.Length);
                    if (len__381 < min__377)
                    {
                        t___2960 = true;
                    }
                    else
                    {
                        t___2960 = len__381 > max__378;
                    }
                    if (t___2960)
                    {
                        string msg__382 = "must be between " + S::Convert.ToString(min__377) + " and " + S::Convert.ToString(max__378) + " characters";
                        G::IList<ChangesetError> eb__383 = L::Enumerable.ToList(this._errors__353);
                        C::Listed.Add(eb__383, new ChangesetError(field__376.SqlValue, msg__382));
                        t___2966 = this._tableDef__350;
                        t___2967 = this._params__351;
                        t___2968 = this._changes__352;
                        t___4996 = C::Listed.ToReadOnlyList(eb__383);
                        return__193 = new ChangesetImpl(t___2966, t___2967, t___2968, t___4996, false);
                        goto fn__379;
                    }
                    return__193 = this;
                }
                fn__379:
                {
                }
            }
            return return__193;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__385)
        {
            IChangeset return__194;
            string t___4976;
            G::IReadOnlyList<ChangesetError> t___4983;
            TableDef t___2951;
            G::IReadOnlyDictionary<string, string> t___2952;
            G::IReadOnlyDictionary<string, string> t___2953;
            {
                {
                    if (!this._isValid__354)
                    {
                        return__194 = this;
                        goto fn__386;
                    }
                    t___4976 = field__385.SqlValue;
                    string val__387 = C::Mapped.GetOrDefault(this._changes__352, t___4976, "");
                    if (string.IsNullOrEmpty(val__387))
                    {
                        return__194 = this;
                        goto fn__386;
                    }
                    bool parseOk__388;
                    try
                    {
                        C::Core.ToInt(val__387);
                        parseOk__388 = true;
                    }
                    catch
                    {
                        parseOk__388 = false;
                    }
                    if (!parseOk__388)
                    {
                        G::IList<ChangesetError> eb__389 = L::Enumerable.ToList(this._errors__353);
                        C::Listed.Add(eb__389, new ChangesetError(field__385.SqlValue, "must be an integer"));
                        t___2951 = this._tableDef__350;
                        t___2952 = this._params__351;
                        t___2953 = this._changes__352;
                        t___4983 = C::Listed.ToReadOnlyList(eb__389);
                        return__194 = new ChangesetImpl(t___2951, t___2952, t___2953, t___4983, false);
                        goto fn__386;
                    }
                    return__194 = this;
                }
                fn__386:
                {
                }
            }
            return return__194;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__391)
        {
            IChangeset return__195;
            string t___4967;
            G::IReadOnlyList<ChangesetError> t___4974;
            TableDef t___2938;
            G::IReadOnlyDictionary<string, string> t___2939;
            G::IReadOnlyDictionary<string, string> t___2940;
            {
                {
                    if (!this._isValid__354)
                    {
                        return__195 = this;
                        goto fn__392;
                    }
                    t___4967 = field__391.SqlValue;
                    string val__393 = C::Mapped.GetOrDefault(this._changes__352, t___4967, "");
                    if (string.IsNullOrEmpty(val__393))
                    {
                        return__195 = this;
                        goto fn__392;
                    }
                    bool parseOk__394;
                    try
                    {
                        C::Core.ToInt64(val__393);
                        parseOk__394 = true;
                    }
                    catch
                    {
                        parseOk__394 = false;
                    }
                    if (!parseOk__394)
                    {
                        G::IList<ChangesetError> eb__395 = L::Enumerable.ToList(this._errors__353);
                        C::Listed.Add(eb__395, new ChangesetError(field__391.SqlValue, "must be a 64-bit integer"));
                        t___2938 = this._tableDef__350;
                        t___2939 = this._params__351;
                        t___2940 = this._changes__352;
                        t___4974 = C::Listed.ToReadOnlyList(eb__395);
                        return__195 = new ChangesetImpl(t___2938, t___2939, t___2940, t___4974, false);
                        goto fn__392;
                    }
                    return__195 = this;
                }
                fn__392:
                {
                }
            }
            return return__195;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__397)
        {
            IChangeset return__196;
            string t___4958;
            G::IReadOnlyList<ChangesetError> t___4965;
            TableDef t___2925;
            G::IReadOnlyDictionary<string, string> t___2926;
            G::IReadOnlyDictionary<string, string> t___2927;
            {
                {
                    if (!this._isValid__354)
                    {
                        return__196 = this;
                        goto fn__398;
                    }
                    t___4958 = field__397.SqlValue;
                    string val__399 = C::Mapped.GetOrDefault(this._changes__352, t___4958, "");
                    if (string.IsNullOrEmpty(val__399))
                    {
                        return__196 = this;
                        goto fn__398;
                    }
                    bool parseOk__400;
                    try
                    {
                        C::Float64.ToFloat64(val__399);
                        parseOk__400 = true;
                    }
                    catch
                    {
                        parseOk__400 = false;
                    }
                    if (!parseOk__400)
                    {
                        G::IList<ChangesetError> eb__401 = L::Enumerable.ToList(this._errors__353);
                        C::Listed.Add(eb__401, new ChangesetError(field__397.SqlValue, "must be a number"));
                        t___2925 = this._tableDef__350;
                        t___2926 = this._params__351;
                        t___2927 = this._changes__352;
                        t___4965 = C::Listed.ToReadOnlyList(eb__401);
                        return__196 = new ChangesetImpl(t___2925, t___2926, t___2927, t___4965, false);
                        goto fn__398;
                    }
                    return__196 = this;
                }
                fn__398:
                {
                }
            }
            return return__196;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__403)
        {
            IChangeset return__197;
            string t___4949;
            G::IReadOnlyList<ChangesetError> t___4956;
            bool t___2900;
            bool t___2901;
            bool t___2903;
            bool t___2904;
            bool t___2906;
            TableDef t___2912;
            G::IReadOnlyDictionary<string, string> t___2913;
            G::IReadOnlyDictionary<string, string> t___2914;
            {
                {
                    if (!this._isValid__354)
                    {
                        return__197 = this;
                        goto fn__404;
                    }
                    t___4949 = field__403.SqlValue;
                    string val__405 = C::Mapped.GetOrDefault(this._changes__352, t___4949, "");
                    if (string.IsNullOrEmpty(val__405))
                    {
                        return__197 = this;
                        goto fn__404;
                    }
                    bool isTrue__406;
                    if (val__405 == "true")
                    {
                        isTrue__406 = true;
                    }
                    else
                    {
                        if (val__405 == "1")
                        {
                            t___2901 = true;
                        }
                        else
                        {
                            if (val__405 == "yes")
                            {
                                t___2900 = true;
                            }
                            else
                            {
                                t___2900 = val__405 == "on";
                            }
                            t___2901 = t___2900;
                        }
                        isTrue__406 = t___2901;
                    }
                    bool isFalse__407;
                    if (val__405 == "false")
                    {
                        isFalse__407 = true;
                    }
                    else
                    {
                        if (val__405 == "0")
                        {
                            t___2904 = true;
                        }
                        else
                        {
                            if (val__405 == "no")
                            {
                                t___2903 = true;
                            }
                            else
                            {
                                t___2903 = val__405 == "off";
                            }
                            t___2904 = t___2903;
                        }
                        isFalse__407 = t___2904;
                    }
                    if (!isTrue__406)
                    {
                        t___2906 = !isFalse__407;
                    }
                    else
                    {
                        t___2906 = false;
                    }
                    if (t___2906)
                    {
                        G::IList<ChangesetError> eb__408 = L::Enumerable.ToList(this._errors__353);
                        C::Listed.Add(eb__408, new ChangesetError(field__403.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___2912 = this._tableDef__350;
                        t___2913 = this._params__351;
                        t___2914 = this._changes__352;
                        t___4956 = C::Listed.ToReadOnlyList(eb__408);
                        return__197 = new ChangesetImpl(t___2912, t___2913, t___2914, t___4956, false);
                        goto fn__404;
                    }
                    return__197 = this;
                }
                fn__404:
                {
                }
            }
            return return__197;
        }
        SqlBoolean ParseBoolSqlPart(string val__410)
        {
            SqlBoolean return__198;
            bool t___2889;
            bool t___2890;
            bool t___2891;
            bool t___2893;
            bool t___2894;
            bool t___2895;
            {
                {
                    if (val__410 == "true")
                    {
                        t___2891 = true;
                    }
                    else
                    {
                        if (val__410 == "1")
                        {
                            t___2890 = true;
                        }
                        else
                        {
                            if (val__410 == "yes")
                            {
                                t___2889 = true;
                            }
                            else
                            {
                                t___2889 = val__410 == "on";
                            }
                            t___2890 = t___2889;
                        }
                        t___2891 = t___2890;
                    }
                    if (t___2891)
                    {
                        return__198 = new SqlBoolean(true);
                        goto fn__411;
                    }
                    if (val__410 == "false")
                    {
                        t___2895 = true;
                    }
                    else
                    {
                        if (val__410 == "0")
                        {
                            t___2894 = true;
                        }
                        else
                        {
                            if (val__410 == "no")
                            {
                                t___2893 = true;
                            }
                            else
                            {
                                t___2893 = val__410 == "off";
                            }
                            t___2894 = t___2893;
                        }
                        t___2895 = t___2894;
                    }
                    if (t___2895)
                    {
                        return__198 = new SqlBoolean(false);
                        goto fn__411;
                    }
                    throw new S::Exception();
                }
                fn__411:
                {
                }
            }
            return return__198;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__413, string val__414)
        {
            ISqlPart return__199;
            int t___2876;
            long t___2879;
            double t___2882;
            S::DateTime t___2887;
            {
                {
                    IFieldType ft__416 = fieldDef__413.FieldType;
                    if (ft__416 is StringField)
                    {
                        return__199 = new SqlString(val__414);
                        goto fn__415;
                    }
                    if (ft__416 is IntField)
                    {
                        t___2876 = C::Core.ToInt(val__414);
                        return__199 = new SqlInt32(t___2876);
                        goto fn__415;
                    }
                    if (ft__416 is Int64_Field)
                    {
                        t___2879 = C::Core.ToInt64(val__414);
                        return__199 = new SqlInt64(t___2879);
                        goto fn__415;
                    }
                    if (ft__416 is FloatField)
                    {
                        t___2882 = C::Float64.ToFloat64(val__414);
                        return__199 = new SqlFloat64(t___2882);
                        goto fn__415;
                    }
                    if (ft__416 is BoolField)
                    {
                        return__199 = this.ParseBoolSqlPart(val__414);
                        goto fn__415;
                    }
                    if (ft__416 is DateField)
                    {
                        t___2887 = T::TemporalSupport.FromIsoString(val__414);
                        return__199 = new SqlDate(t___2887);
                        goto fn__415;
                    }
                    throw new S::Exception();
                }
                fn__415:
                {
                }
            }
            return return__199;
        }
        public SqlFragment ToInsertSql()
        {
            int t___4897;
            string t___4902;
            bool t___4903;
            int t___4908;
            string t___4910;
            string t___4914;
            int t___4929;
            bool t___2840;
            FieldDef t___2848;
            ISqlPart t___2853;
            if (!this._isValid__354) throw new S::Exception();
            int i__419 = 0;
            while (true)
            {
                t___4897 = this._tableDef__350.Fields.Count;
                if (!(i__419 < t___4897)) break;
                FieldDef f__420 = this._tableDef__350.Fields[i__419];
                if (!f__420.Nullable)
                {
                    t___4902 = f__420.Name.SqlValue;
                    t___4903 = C::Mapped.ContainsKey(this._changes__352, t___4902);
                    t___2840 = !t___4903;
                }
                else
                {
                    t___2840 = false;
                }
                if (t___2840) throw new S::Exception();
                i__419 = i__419 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__421 = C::Mapped.ToList(this._changes__352);
            if (pairs__421.Count == 0) throw new S::Exception();
            G::IList<string> colNames__422 = new G::List<string>();
            G::IList<ISqlPart> valParts__423 = new G::List<ISqlPart>();
            int i__424 = 0;
            while (true)
            {
                t___4908 = pairs__421.Count;
                if (!(i__424 < t___4908)) break;
                G::KeyValuePair<string, string> pair__425 = pairs__421[i__424];
                t___4910 = pair__425.Key;
                t___2848 = this._tableDef__350.Field(t___4910);
                FieldDef fd__426 = t___2848;
                C::Listed.Add(colNames__422, fd__426.Name.SqlValue);
                t___4914 = pair__425.Value;
                t___2853 = this.ValueToSqlPart(fd__426, t___4914);
                C::Listed.Add(valParts__423, t___2853);
                i__424 = i__424 + 1;
            }
            SqlBuilder b__427 = new SqlBuilder();
            b__427.AppendSafe("INSERT INTO ");
            b__427.AppendSafe(this._tableDef__350.TableName.SqlValue);
            b__427.AppendSafe(" (");
            G::IReadOnlyList<string> t___4922 = C::Listed.ToReadOnlyList(colNames__422);
            string fn__4895(string c__428)
            {
                return c__428;
            }
            b__427.AppendSafe(C::Listed.Join(t___4922, ", ", (S::Func<string, string>) fn__4895));
            b__427.AppendSafe(") VALUES (");
            b__427.AppendPart(valParts__423[0]);
            int j__429 = 1;
            while (true)
            {
                t___4929 = valParts__423.Count;
                if (!(j__429 < t___4929)) break;
                b__427.AppendSafe(", ");
                b__427.AppendPart(valParts__423[j__429]);
                j__429 = j__429 + 1;
            }
            b__427.AppendSafe(")");
            return b__427.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__431)
        {
            int t___4882;
            string t___4885;
            string t___4890;
            FieldDef t___2821;
            ISqlPart t___2827;
            if (!this._isValid__354) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__433 = C::Mapped.ToList(this._changes__352);
            if (pairs__433.Count == 0) throw new S::Exception();
            SqlBuilder b__434 = new SqlBuilder();
            b__434.AppendSafe("UPDATE ");
            b__434.AppendSafe(this._tableDef__350.TableName.SqlValue);
            b__434.AppendSafe(" SET ");
            int i__435 = 0;
            while (true)
            {
                t___4882 = pairs__433.Count;
                if (!(i__435 < t___4882)) break;
                if (i__435 > 0) b__434.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__436 = pairs__433[i__435];
                t___4885 = pair__436.Key;
                t___2821 = this._tableDef__350.Field(t___4885);
                FieldDef fd__437 = t___2821;
                b__434.AppendSafe(fd__437.Name.SqlValue);
                b__434.AppendSafe(" = ");
                t___4890 = pair__436.Value;
                t___2827 = this.ValueToSqlPart(fd__437, t___4890);
                b__434.AppendPart(t___2827);
                i__435 = i__435 + 1;
            }
            b__434.AppendSafe(" WHERE id = ");
            b__434.AppendInt32(id__431);
            return b__434.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__439, G::IReadOnlyDictionary<string, string> _params__440, G::IReadOnlyDictionary<string, string> _changes__441, G::IReadOnlyList<ChangesetError> _errors__442, bool _isValid__443)
        {
            this._tableDef__350 = _tableDef__439;
            this._params__351 = _params__440;
            this._changes__352 = _changes__441;
            this._errors__353 = _errors__442;
            this._isValid__354 = _isValid__443;
        }
    }
}
