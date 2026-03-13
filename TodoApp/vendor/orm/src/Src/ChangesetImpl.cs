using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__446;
        readonly G::IReadOnlyDictionary<string, string> _params__447;
        readonly G::IReadOnlyDictionary<string, string> _changes__448;
        readonly G::IReadOnlyList<ChangesetError> _errors__449;
        readonly bool _isValid__450;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__446;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__448;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__449;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__450;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__460)
        {
            G::IDictionary<string, string> mb__462 = new C::OrderedDictionary<string, string>();
            void fn__7132(ISafeIdentifier f__463)
            {
                string t___7130;
                string t___7127 = f__463.SqlValue;
                string val__464 = C::Mapped.GetOrDefault(this._params__447, t___7127, "");
                if (!string.IsNullOrEmpty(val__464))
                {
                    t___7130 = f__463.SqlValue;
                    mb__462[t___7130] = val__464;
                }
            }
            C::Listed.ForEach(allowedFields__460, (S::Action<ISafeIdentifier>) fn__7132);
            return new ChangesetImpl(this._tableDef__446, this._params__447, C::Mapped.ToMap(mb__462), this._errors__449, this._isValid__450);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__466)
        {
            IChangeset return__245;
            G::IReadOnlyList<ChangesetError> t___7125;
            TableDef t___4163;
            G::IReadOnlyDictionary<string, string> t___4164;
            G::IReadOnlyDictionary<string, string> t___4165;
            {
                {
                    if (!this._isValid__450)
                    {
                        return__245 = this;
                        goto fn__467;
                    }
                    G::IList<ChangesetError> eb__468 = L::Enumerable.ToList(this._errors__449);
                    bool valid__469 = true;
                    void fn__7121(ISafeIdentifier f__470)
                    {
                        ChangesetError t___7119;
                        string t___7116 = f__470.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__448, t___7116))
                        {
                            t___7119 = new ChangesetError(f__470.SqlValue, "is required");
                            C::Listed.Add(eb__468, t___7119);
                            valid__469 = false;
                        }
                    }
                    C::Listed.ForEach(fields__466, (S::Action<ISafeIdentifier>) fn__7121);
                    t___4163 = this._tableDef__446;
                    t___4164 = this._params__447;
                    t___4165 = this._changes__448;
                    t___7125 = C::Listed.ToReadOnlyList(eb__468);
                    return__245 = new ChangesetImpl(t___4163, t___4164, t___4165, t___7125, valid__469);
                }
                fn__467:
                {
                }
            }
            return return__245;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__472, int min__473, int max__474)
        {
            IChangeset return__246;
            string t___7103;
            G::IReadOnlyList<ChangesetError> t___7114;
            bool t___4146;
            TableDef t___4152;
            G::IReadOnlyDictionary<string, string> t___4153;
            G::IReadOnlyDictionary<string, string> t___4154;
            {
                {
                    if (!this._isValid__450)
                    {
                        return__246 = this;
                        goto fn__475;
                    }
                    t___7103 = field__472.SqlValue;
                    string val__476 = C::Mapped.GetOrDefault(this._changes__448, t___7103, "");
                    int len__477 = C::StringUtil.CountBetween(val__476, 0, val__476.Length);
                    if (len__477 < min__473)
                    {
                        t___4146 = true;
                    }
                    else
                    {
                        t___4146 = len__477 > max__474;
                    }
                    if (t___4146)
                    {
                        string msg__478 = "must be between " + S::Convert.ToString(min__473) + " and " + S::Convert.ToString(max__474) + " characters";
                        G::IList<ChangesetError> eb__479 = L::Enumerable.ToList(this._errors__449);
                        C::Listed.Add(eb__479, new ChangesetError(field__472.SqlValue, msg__478));
                        t___4152 = this._tableDef__446;
                        t___4153 = this._params__447;
                        t___4154 = this._changes__448;
                        t___7114 = C::Listed.ToReadOnlyList(eb__479);
                        return__246 = new ChangesetImpl(t___4152, t___4153, t___4154, t___7114, false);
                        goto fn__475;
                    }
                    return__246 = this;
                }
                fn__475:
                {
                }
            }
            return return__246;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__481)
        {
            IChangeset return__247;
            string t___7094;
            G::IReadOnlyList<ChangesetError> t___7101;
            TableDef t___4137;
            G::IReadOnlyDictionary<string, string> t___4138;
            G::IReadOnlyDictionary<string, string> t___4139;
            {
                {
                    if (!this._isValid__450)
                    {
                        return__247 = this;
                        goto fn__482;
                    }
                    t___7094 = field__481.SqlValue;
                    string val__483 = C::Mapped.GetOrDefault(this._changes__448, t___7094, "");
                    if (string.IsNullOrEmpty(val__483))
                    {
                        return__247 = this;
                        goto fn__482;
                    }
                    bool parseOk__484;
                    try
                    {
                        C::Core.ToInt(val__483);
                        parseOk__484 = true;
                    }
                    catch
                    {
                        parseOk__484 = false;
                    }
                    if (!parseOk__484)
                    {
                        G::IList<ChangesetError> eb__485 = L::Enumerable.ToList(this._errors__449);
                        C::Listed.Add(eb__485, new ChangesetError(field__481.SqlValue, "must be an integer"));
                        t___4137 = this._tableDef__446;
                        t___4138 = this._params__447;
                        t___4139 = this._changes__448;
                        t___7101 = C::Listed.ToReadOnlyList(eb__485);
                        return__247 = new ChangesetImpl(t___4137, t___4138, t___4139, t___7101, false);
                        goto fn__482;
                    }
                    return__247 = this;
                }
                fn__482:
                {
                }
            }
            return return__247;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__487)
        {
            IChangeset return__248;
            string t___7085;
            G::IReadOnlyList<ChangesetError> t___7092;
            TableDef t___4124;
            G::IReadOnlyDictionary<string, string> t___4125;
            G::IReadOnlyDictionary<string, string> t___4126;
            {
                {
                    if (!this._isValid__450)
                    {
                        return__248 = this;
                        goto fn__488;
                    }
                    t___7085 = field__487.SqlValue;
                    string val__489 = C::Mapped.GetOrDefault(this._changes__448, t___7085, "");
                    if (string.IsNullOrEmpty(val__489))
                    {
                        return__248 = this;
                        goto fn__488;
                    }
                    bool parseOk__490;
                    try
                    {
                        C::Core.ToInt64(val__489);
                        parseOk__490 = true;
                    }
                    catch
                    {
                        parseOk__490 = false;
                    }
                    if (!parseOk__490)
                    {
                        G::IList<ChangesetError> eb__491 = L::Enumerable.ToList(this._errors__449);
                        C::Listed.Add(eb__491, new ChangesetError(field__487.SqlValue, "must be a 64-bit integer"));
                        t___4124 = this._tableDef__446;
                        t___4125 = this._params__447;
                        t___4126 = this._changes__448;
                        t___7092 = C::Listed.ToReadOnlyList(eb__491);
                        return__248 = new ChangesetImpl(t___4124, t___4125, t___4126, t___7092, false);
                        goto fn__488;
                    }
                    return__248 = this;
                }
                fn__488:
                {
                }
            }
            return return__248;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__493)
        {
            IChangeset return__249;
            string t___7076;
            G::IReadOnlyList<ChangesetError> t___7083;
            TableDef t___4111;
            G::IReadOnlyDictionary<string, string> t___4112;
            G::IReadOnlyDictionary<string, string> t___4113;
            {
                {
                    if (!this._isValid__450)
                    {
                        return__249 = this;
                        goto fn__494;
                    }
                    t___7076 = field__493.SqlValue;
                    string val__495 = C::Mapped.GetOrDefault(this._changes__448, t___7076, "");
                    if (string.IsNullOrEmpty(val__495))
                    {
                        return__249 = this;
                        goto fn__494;
                    }
                    bool parseOk__496;
                    try
                    {
                        C::Float64.ToFloat64(val__495);
                        parseOk__496 = true;
                    }
                    catch
                    {
                        parseOk__496 = false;
                    }
                    if (!parseOk__496)
                    {
                        G::IList<ChangesetError> eb__497 = L::Enumerable.ToList(this._errors__449);
                        C::Listed.Add(eb__497, new ChangesetError(field__493.SqlValue, "must be a number"));
                        t___4111 = this._tableDef__446;
                        t___4112 = this._params__447;
                        t___4113 = this._changes__448;
                        t___7083 = C::Listed.ToReadOnlyList(eb__497);
                        return__249 = new ChangesetImpl(t___4111, t___4112, t___4113, t___7083, false);
                        goto fn__494;
                    }
                    return__249 = this;
                }
                fn__494:
                {
                }
            }
            return return__249;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__499)
        {
            IChangeset return__250;
            string t___7067;
            G::IReadOnlyList<ChangesetError> t___7074;
            bool t___4086;
            bool t___4087;
            bool t___4089;
            bool t___4090;
            bool t___4092;
            TableDef t___4098;
            G::IReadOnlyDictionary<string, string> t___4099;
            G::IReadOnlyDictionary<string, string> t___4100;
            {
                {
                    if (!this._isValid__450)
                    {
                        return__250 = this;
                        goto fn__500;
                    }
                    t___7067 = field__499.SqlValue;
                    string val__501 = C::Mapped.GetOrDefault(this._changes__448, t___7067, "");
                    if (string.IsNullOrEmpty(val__501))
                    {
                        return__250 = this;
                        goto fn__500;
                    }
                    bool isTrue__502;
                    if (val__501 == "true")
                    {
                        isTrue__502 = true;
                    }
                    else
                    {
                        if (val__501 == "1")
                        {
                            t___4087 = true;
                        }
                        else
                        {
                            if (val__501 == "yes")
                            {
                                t___4086 = true;
                            }
                            else
                            {
                                t___4086 = val__501 == "on";
                            }
                            t___4087 = t___4086;
                        }
                        isTrue__502 = t___4087;
                    }
                    bool isFalse__503;
                    if (val__501 == "false")
                    {
                        isFalse__503 = true;
                    }
                    else
                    {
                        if (val__501 == "0")
                        {
                            t___4090 = true;
                        }
                        else
                        {
                            if (val__501 == "no")
                            {
                                t___4089 = true;
                            }
                            else
                            {
                                t___4089 = val__501 == "off";
                            }
                            t___4090 = t___4089;
                        }
                        isFalse__503 = t___4090;
                    }
                    if (!isTrue__502)
                    {
                        t___4092 = !isFalse__503;
                    }
                    else
                    {
                        t___4092 = false;
                    }
                    if (t___4092)
                    {
                        G::IList<ChangesetError> eb__504 = L::Enumerable.ToList(this._errors__449);
                        C::Listed.Add(eb__504, new ChangesetError(field__499.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___4098 = this._tableDef__446;
                        t___4099 = this._params__447;
                        t___4100 = this._changes__448;
                        t___7074 = C::Listed.ToReadOnlyList(eb__504);
                        return__250 = new ChangesetImpl(t___4098, t___4099, t___4100, t___7074, false);
                        goto fn__500;
                    }
                    return__250 = this;
                }
                fn__500:
                {
                }
            }
            return return__250;
        }
        SqlBoolean ParseBoolSqlPart(string val__506)
        {
            SqlBoolean return__251;
            bool t___4075;
            bool t___4076;
            bool t___4077;
            bool t___4079;
            bool t___4080;
            bool t___4081;
            {
                {
                    if (val__506 == "true")
                    {
                        t___4077 = true;
                    }
                    else
                    {
                        if (val__506 == "1")
                        {
                            t___4076 = true;
                        }
                        else
                        {
                            if (val__506 == "yes")
                            {
                                t___4075 = true;
                            }
                            else
                            {
                                t___4075 = val__506 == "on";
                            }
                            t___4076 = t___4075;
                        }
                        t___4077 = t___4076;
                    }
                    if (t___4077)
                    {
                        return__251 = new SqlBoolean(true);
                        goto fn__507;
                    }
                    if (val__506 == "false")
                    {
                        t___4081 = true;
                    }
                    else
                    {
                        if (val__506 == "0")
                        {
                            t___4080 = true;
                        }
                        else
                        {
                            if (val__506 == "no")
                            {
                                t___4079 = true;
                            }
                            else
                            {
                                t___4079 = val__506 == "off";
                            }
                            t___4080 = t___4079;
                        }
                        t___4081 = t___4080;
                    }
                    if (t___4081)
                    {
                        return__251 = new SqlBoolean(false);
                        goto fn__507;
                    }
                    throw new S::Exception();
                }
                fn__507:
                {
                }
            }
            return return__251;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__509, string val__510)
        {
            ISqlPart return__252;
            int t___4062;
            long t___4065;
            double t___4068;
            S::DateTime t___4073;
            {
                {
                    IFieldType ft__512 = fieldDef__509.FieldType;
                    if (ft__512 is StringField)
                    {
                        return__252 = new SqlString(val__510);
                        goto fn__511;
                    }
                    if (ft__512 is IntField)
                    {
                        t___4062 = C::Core.ToInt(val__510);
                        return__252 = new SqlInt32(t___4062);
                        goto fn__511;
                    }
                    if (ft__512 is Int64_Field)
                    {
                        t___4065 = C::Core.ToInt64(val__510);
                        return__252 = new SqlInt64(t___4065);
                        goto fn__511;
                    }
                    if (ft__512 is FloatField)
                    {
                        t___4068 = C::Float64.ToFloat64(val__510);
                        return__252 = new SqlFloat64(t___4068);
                        goto fn__511;
                    }
                    if (ft__512 is BoolField)
                    {
                        return__252 = this.ParseBoolSqlPart(val__510);
                        goto fn__511;
                    }
                    if (ft__512 is DateField)
                    {
                        t___4073 = T::TemporalSupport.FromIsoString(val__510);
                        return__252 = new SqlDate(t___4073);
                        goto fn__511;
                    }
                    throw new S::Exception();
                }
                fn__511:
                {
                }
            }
            return return__252;
        }
        public SqlFragment ToInsertSql()
        {
            int t___7015;
            string t___7020;
            bool t___7021;
            int t___7026;
            string t___7028;
            string t___7032;
            int t___7047;
            bool t___4026;
            FieldDef t___4034;
            ISqlPart t___4039;
            if (!this._isValid__450) throw new S::Exception();
            int i__515 = 0;
            while (true)
            {
                t___7015 = this._tableDef__446.Fields.Count;
                if (!(i__515 < t___7015)) break;
                FieldDef f__516 = this._tableDef__446.Fields[i__515];
                if (!f__516.Nullable)
                {
                    t___7020 = f__516.Name.SqlValue;
                    t___7021 = C::Mapped.ContainsKey(this._changes__448, t___7020);
                    t___4026 = !t___7021;
                }
                else
                {
                    t___4026 = false;
                }
                if (t___4026) throw new S::Exception();
                i__515 = i__515 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__517 = C::Mapped.ToList(this._changes__448);
            if (pairs__517.Count == 0) throw new S::Exception();
            G::IList<string> colNames__518 = new G::List<string>();
            G::IList<ISqlPart> valParts__519 = new G::List<ISqlPart>();
            int i__520 = 0;
            while (true)
            {
                t___7026 = pairs__517.Count;
                if (!(i__520 < t___7026)) break;
                G::KeyValuePair<string, string> pair__521 = pairs__517[i__520];
                t___7028 = pair__521.Key;
                t___4034 = this._tableDef__446.Field(t___7028);
                FieldDef fd__522 = t___4034;
                C::Listed.Add(colNames__518, fd__522.Name.SqlValue);
                t___7032 = pair__521.Value;
                t___4039 = this.ValueToSqlPart(fd__522, t___7032);
                C::Listed.Add(valParts__519, t___4039);
                i__520 = i__520 + 1;
            }
            SqlBuilder b__523 = new SqlBuilder();
            b__523.AppendSafe("INSERT INTO ");
            b__523.AppendSafe(this._tableDef__446.TableName.SqlValue);
            b__523.AppendSafe(" (");
            G::IReadOnlyList<string> t___7040 = C::Listed.ToReadOnlyList(colNames__518);
            string fn__7013(string c__524)
            {
                return c__524;
            }
            b__523.AppendSafe(C::Listed.Join(t___7040, ", ", (S::Func<string, string>) fn__7013));
            b__523.AppendSafe(") VALUES (");
            b__523.AppendPart(valParts__519[0]);
            int j__525 = 1;
            while (true)
            {
                t___7047 = valParts__519.Count;
                if (!(j__525 < t___7047)) break;
                b__523.AppendSafe(", ");
                b__523.AppendPart(valParts__519[j__525]);
                j__525 = j__525 + 1;
            }
            b__523.AppendSafe(")");
            return b__523.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__527)
        {
            int t___7000;
            string t___7003;
            string t___7008;
            FieldDef t___4007;
            ISqlPart t___4013;
            if (!this._isValid__450) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__529 = C::Mapped.ToList(this._changes__448);
            if (pairs__529.Count == 0) throw new S::Exception();
            SqlBuilder b__530 = new SqlBuilder();
            b__530.AppendSafe("UPDATE ");
            b__530.AppendSafe(this._tableDef__446.TableName.SqlValue);
            b__530.AppendSafe(" SET ");
            int i__531 = 0;
            while (true)
            {
                t___7000 = pairs__529.Count;
                if (!(i__531 < t___7000)) break;
                if (i__531 > 0) b__530.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__532 = pairs__529[i__531];
                t___7003 = pair__532.Key;
                t___4007 = this._tableDef__446.Field(t___7003);
                FieldDef fd__533 = t___4007;
                b__530.AppendSafe(fd__533.Name.SqlValue);
                b__530.AppendSafe(" = ");
                t___7008 = pair__532.Value;
                t___4013 = this.ValueToSqlPart(fd__533, t___7008);
                b__530.AppendPart(t___4013);
                i__531 = i__531 + 1;
            }
            b__530.AppendSafe(" WHERE id = ");
            b__530.AppendInt32(id__527);
            return b__530.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__535, G::IReadOnlyDictionary<string, string> _params__536, G::IReadOnlyDictionary<string, string> _changes__537, G::IReadOnlyList<ChangesetError> _errors__538, bool _isValid__539)
        {
            this._tableDef__446 = _tableDef__535;
            this._params__447 = _params__536;
            this._changes__448 = _changes__537;
            this._errors__449 = _errors__538;
            this._isValid__450 = _isValid__539;
        }
    }
}
