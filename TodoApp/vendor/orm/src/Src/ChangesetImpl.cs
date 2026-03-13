using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__807;
        readonly G::IReadOnlyDictionary<string, string> _params__808;
        readonly G::IReadOnlyDictionary<string, string> _changes__809;
        readonly G::IReadOnlyList<ChangesetError> _errors__810;
        readonly bool _isValid__811;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__807;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__809;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__810;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__811;
            }
        }
        IChangeset AddError(string field__821, string message__822)
        {
            G::IList<ChangesetError> eb__824 = L::Enumerable.ToList(this._errors__810);
            C::Listed.Add(eb__824, new ChangesetError(field__821, message__822));
            return new ChangesetImpl(this._tableDef__807, this._params__808, this._changes__809, C::Listed.ToReadOnlyList(eb__824), false);
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__826)
        {
            G::IDictionary<string, string> mb__828 = new C::OrderedDictionary<string, string>();
            void fn__17273(ISafeIdentifier f__829)
            {
                string t___17271;
                string t___17268 = f__829.SqlValue;
                string val__830 = C::Mapped.GetOrDefault(this._params__808, t___17268, "");
                if (!string.IsNullOrEmpty(val__830))
                {
                    t___17271 = f__829.SqlValue;
                    mb__828[t___17271] = val__830;
                }
            }
            C::Listed.ForEach(allowedFields__826, (S::Action<ISafeIdentifier>) fn__17273);
            return new ChangesetImpl(this._tableDef__807, this._params__808, C::Mapped.ToMap(mb__828), this._errors__810, this._isValid__811);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__832)
        {
            IChangeset return__461;
            G::IReadOnlyList<ChangesetError> t___17266;
            TableDef t___9729;
            G::IReadOnlyDictionary<string, string> t___9730;
            G::IReadOnlyDictionary<string, string> t___9731;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__461 = this;
                        goto fn__833;
                    }
                    G::IList<ChangesetError> eb__834 = L::Enumerable.ToList(this._errors__810);
                    bool valid__835 = true;
                    void fn__17262(ISafeIdentifier f__836)
                    {
                        ChangesetError t___17260;
                        string t___17257 = f__836.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__809, t___17257))
                        {
                            t___17260 = new ChangesetError(f__836.SqlValue, "is required");
                            C::Listed.Add(eb__834, t___17260);
                            valid__835 = false;
                        }
                    }
                    C::Listed.ForEach(fields__832, (S::Action<ISafeIdentifier>) fn__17262);
                    t___9729 = this._tableDef__807;
                    t___9730 = this._params__808;
                    t___9731 = this._changes__809;
                    t___17266 = C::Listed.ToReadOnlyList(eb__834);
                    return__461 = new ChangesetImpl(t___9729, t___9730, t___9731, t___17266, valid__835);
                }
                fn__833:
                {
                }
            }
            return return__461;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__838, int min__839, int max__840)
        {
            IChangeset return__462;
            string t___17248;
            string t___17253;
            string t___17254;
            string t___17255;
            bool t___9719;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__462 = this;
                        goto fn__841;
                    }
                    t___17248 = field__838.SqlValue;
                    string val__842 = C::Mapped.GetOrDefault(this._changes__809, t___17248, "");
                    int len__843 = C::StringUtil.CountBetween(val__842, 0, val__842.Length);
                    if (len__843 < min__839)
                    {
                        t___9719 = true;
                    }
                    else
                    {
                        t___9719 = len__843 > max__840;
                    }
                    if (t___9719)
                    {
                        t___17253 = field__838.SqlValue;
                        t___17254 = S::Convert.ToString(min__839);
                        t___17255 = S::Convert.ToString(max__840);
                        return__462 = this.AddError(t___17253, "must be between " + t___17254 + " and " + t___17255 + " characters");
                        goto fn__841;
                    }
                    return__462 = this;
                }
                fn__841:
                {
                }
            }
            return return__462;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__845)
        {
            IChangeset return__463;
            string t___17243;
            string t___17246;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__463 = this;
                        goto fn__846;
                    }
                    t___17243 = field__845.SqlValue;
                    string val__847 = C::Mapped.GetOrDefault(this._changes__809, t___17243, "");
                    if (string.IsNullOrEmpty(val__847))
                    {
                        return__463 = this;
                        goto fn__846;
                    }
                    bool parseOk__848;
                    try
                    {
                        C::Core.ToInt(val__847);
                        parseOk__848 = true;
                    }
                    catch
                    {
                        parseOk__848 = false;
                    }
                    if (!parseOk__848)
                    {
                        t___17246 = field__845.SqlValue;
                        return__463 = this.AddError(t___17246, "must be an integer");
                        goto fn__846;
                    }
                    return__463 = this;
                }
                fn__846:
                {
                }
            }
            return return__463;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__850)
        {
            IChangeset return__464;
            string t___17238;
            string t___17241;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__464 = this;
                        goto fn__851;
                    }
                    t___17238 = field__850.SqlValue;
                    string val__852 = C::Mapped.GetOrDefault(this._changes__809, t___17238, "");
                    if (string.IsNullOrEmpty(val__852))
                    {
                        return__464 = this;
                        goto fn__851;
                    }
                    bool parseOk__853;
                    try
                    {
                        C::Core.ToInt64(val__852);
                        parseOk__853 = true;
                    }
                    catch
                    {
                        parseOk__853 = false;
                    }
                    if (!parseOk__853)
                    {
                        t___17241 = field__850.SqlValue;
                        return__464 = this.AddError(t___17241, "must be a 64-bit integer");
                        goto fn__851;
                    }
                    return__464 = this;
                }
                fn__851:
                {
                }
            }
            return return__464;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__855)
        {
            IChangeset return__465;
            string t___17233;
            string t___17236;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__465 = this;
                        goto fn__856;
                    }
                    t___17233 = field__855.SqlValue;
                    string val__857 = C::Mapped.GetOrDefault(this._changes__809, t___17233, "");
                    if (string.IsNullOrEmpty(val__857))
                    {
                        return__465 = this;
                        goto fn__856;
                    }
                    bool parseOk__858;
                    try
                    {
                        C::Float64.ToFloat64(val__857);
                        parseOk__858 = true;
                    }
                    catch
                    {
                        parseOk__858 = false;
                    }
                    if (!parseOk__858)
                    {
                        t___17236 = field__855.SqlValue;
                        return__465 = this.AddError(t___17236, "must be a number");
                        goto fn__856;
                    }
                    return__465 = this;
                }
                fn__856:
                {
                }
            }
            return return__465;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__860)
        {
            IChangeset return__466;
            string t___17228;
            string t___17231;
            bool t___9687;
            bool t___9688;
            bool t___9690;
            bool t___9691;
            bool t___9693;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__466 = this;
                        goto fn__861;
                    }
                    t___17228 = field__860.SqlValue;
                    string val__862 = C::Mapped.GetOrDefault(this._changes__809, t___17228, "");
                    if (string.IsNullOrEmpty(val__862))
                    {
                        return__466 = this;
                        goto fn__861;
                    }
                    bool isTrue__863;
                    if (val__862 == "true")
                    {
                        isTrue__863 = true;
                    }
                    else
                    {
                        if (val__862 == "1")
                        {
                            t___9688 = true;
                        }
                        else
                        {
                            if (val__862 == "yes")
                            {
                                t___9687 = true;
                            }
                            else
                            {
                                t___9687 = val__862 == "on";
                            }
                            t___9688 = t___9687;
                        }
                        isTrue__863 = t___9688;
                    }
                    bool isFalse__864;
                    if (val__862 == "false")
                    {
                        isFalse__864 = true;
                    }
                    else
                    {
                        if (val__862 == "0")
                        {
                            t___9691 = true;
                        }
                        else
                        {
                            if (val__862 == "no")
                            {
                                t___9690 = true;
                            }
                            else
                            {
                                t___9690 = val__862 == "off";
                            }
                            t___9691 = t___9690;
                        }
                        isFalse__864 = t___9691;
                    }
                    if (!isTrue__863)
                    {
                        t___9693 = !isFalse__864;
                    }
                    else
                    {
                        t___9693 = false;
                    }
                    if (t___9693)
                    {
                        t___17231 = field__860.SqlValue;
                        return__466 = this.AddError(t___17231, "must be a boolean (true/false/1/0/yes/no/on/off)");
                        goto fn__861;
                    }
                    return__466 = this;
                }
                fn__861:
                {
                }
            }
            return return__466;
        }
        public IChangeset PutChange(ISafeIdentifier field__866, string value__867)
        {
            int t___17216;
            G::IDictionary<string, string> mb__869 = new C::OrderedDictionary<string, string>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__870 = C::Mapped.ToList(this._changes__809);
            int i__871 = 0;
            while (true)
            {
                t___17216 = pairs__870.Count;
                if (!(i__871 < t___17216)) break;
                mb__869[pairs__870[i__871].Key] = pairs__870[i__871].Value;
                i__871 = i__871 + 1;
            }
            mb__869[field__866.SqlValue] = value__867;
            return new ChangesetImpl(this._tableDef__807, this._params__808, C::Mapped.ToMap(mb__869), this._errors__810, this._isValid__811);
        }
        public string GetChange(ISafeIdentifier field__873)
        {
            string t___17210 = field__873.SqlValue;
            if (!C::Mapped.ContainsKey(this._changes__809, t___17210)) throw new S::Exception();
            string t___17212 = field__873.SqlValue;
            return C::Mapped.GetOrDefault(this._changes__809, t___17212, "");
        }
        public IChangeset DeleteChange(ISafeIdentifier field__876)
        {
            int t___17197;
            G::IDictionary<string, string> mb__878 = new C::OrderedDictionary<string, string>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__879 = C::Mapped.ToList(this._changes__809);
            int i__880 = 0;
            while (true)
            {
                t___17197 = pairs__879.Count;
                if (!(i__880 < t___17197)) break;
                if (pairs__879[i__880].Key != field__876.SqlValue) mb__878[pairs__879[i__880].Key] = pairs__879[i__880].Value;
                i__880 = i__880 + 1;
            }
            return new ChangesetImpl(this._tableDef__807, this._params__808, C::Mapped.ToMap(mb__878), this._errors__810, this._isValid__811);
        }
        public IChangeset ValidateInclusion(ISafeIdentifier field__882, G::IReadOnlyList<string> allowed__883)
        {
            IChangeset return__470;
            string t___17187;
            string t___17189;
            string t___17193;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__470 = this;
                        goto fn__884;
                    }
                    t___17187 = field__882.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17187))
                    {
                        return__470 = this;
                        goto fn__884;
                    }
                    t___17189 = field__882.SqlValue;
                    string val__885 = C::Mapped.GetOrDefault(this._changes__809, t___17189, "");
                    bool found__886 = false;
                    void fn__17186(string a__887)
                    {
                        if (a__887 == val__885)
                        {
                            found__886 = true;
                        }
                    }
                    C::Listed.ForEach(allowed__883, (S::Action<string>) fn__17186);
                    if (!found__886)
                    {
                        t___17193 = field__882.SqlValue;
                        return__470 = this.AddError(t___17193, "is not included in the list");
                        goto fn__884;
                    }
                    return__470 = this;
                }
                fn__884:
                {
                }
            }
            return return__470;
        }
        public IChangeset ValidateExclusion(ISafeIdentifier field__889, G::IReadOnlyList<string> disallowed__890)
        {
            IChangeset return__471;
            string t___17178;
            string t___17180;
            string t___17184;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__471 = this;
                        goto fn__891;
                    }
                    t___17178 = field__889.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17178))
                    {
                        return__471 = this;
                        goto fn__891;
                    }
                    t___17180 = field__889.SqlValue;
                    string val__892 = C::Mapped.GetOrDefault(this._changes__809, t___17180, "");
                    bool found__893 = false;
                    void fn__17177(string d__894)
                    {
                        if (d__894 == val__892)
                        {
                            found__893 = true;
                        }
                    }
                    C::Listed.ForEach(disallowed__890, (S::Action<string>) fn__17177);
                    if (found__893)
                    {
                        t___17184 = field__889.SqlValue;
                        return__471 = this.AddError(t___17184, "is reserved");
                        goto fn__891;
                    }
                    return__471 = this;
                }
                fn__891:
                {
                }
            }
            return return__471;
        }
        public IChangeset ValidateNumber(ISafeIdentifier field__896, NumberValidationOpts opts__897)
        {
            IChangeset return__472;
            string t___17151;
            string t___17153;
            string t___17155;
            string t___17158;
            string t___17159;
            string t___17162;
            string t___17163;
            string t___17166;
            string t___17167;
            string t___17170;
            string t___17171;
            string t___17174;
            string t___17175;
            double t___9621;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__472 = this;
                        goto fn__898;
                    }
                    t___17151 = field__896.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17151))
                    {
                        return__472 = this;
                        goto fn__898;
                    }
                    t___17153 = field__896.SqlValue;
                    string val__899 = C::Mapped.GetOrDefault(this._changes__809, t___17153, "");
                    bool parseOk__900;
                    try
                    {
                        C::Float64.ToFloat64(val__899);
                        parseOk__900 = true;
                    }
                    catch
                    {
                        parseOk__900 = false;
                    }
                    if (!parseOk__900)
                    {
                        t___17155 = field__896.SqlValue;
                        return__472 = this.AddError(t___17155, "must be a number");
                        goto fn__898;
                    }
                    double num__901;
                    try
                    {
                        t___9621 = C::Float64.ToFloat64(val__899);
                        num__901 = t___9621;
                    }
                    catch
                    {
                        num__901 = 0.0;
                    }
                    double ? gt__902 = opts__897.GreaterThan;
                    if (!(gt__902 == null))
                    {
                        double gt___2831 = gt__902.Value;
                        if (!(C::Float64.Compare(num__901, gt___2831) > 0.0))
                        {
                            t___17158 = field__896.SqlValue;
                            t___17159 = C::Float64.Format(gt___2831);
                            return__472 = this.AddError(t___17158, "must be greater than " + t___17159);
                            goto fn__898;
                        }
                    }
                    double ? lt__903 = opts__897.LessThan;
                    if (!(lt__903 == null))
                    {
                        double lt___2832 = lt__903.Value;
                        if (!(C::Float64.Compare(num__901, lt___2832) < 0.0))
                        {
                            t___17162 = field__896.SqlValue;
                            t___17163 = C::Float64.Format(lt___2832);
                            return__472 = this.AddError(t___17162, "must be less than " + t___17163);
                            goto fn__898;
                        }
                    }
                    double ? gte__904 = opts__897.GreaterThanOrEqual;
                    if (!(gte__904 == null))
                    {
                        double gte___2833 = gte__904.Value;
                        if (!(C::Float64.Compare(num__901, gte___2833) >= 0.0))
                        {
                            t___17166 = field__896.SqlValue;
                            t___17167 = C::Float64.Format(gte___2833);
                            return__472 = this.AddError(t___17166, "must be greater than or equal to " + t___17167);
                            goto fn__898;
                        }
                    }
                    double ? lte__905 = opts__897.LessThanOrEqual;
                    if (!(lte__905 == null))
                    {
                        double lte___2834 = lte__905.Value;
                        if (!(C::Float64.Compare(num__901, lte___2834) <= 0.0))
                        {
                            t___17170 = field__896.SqlValue;
                            t___17171 = C::Float64.Format(lte___2834);
                            return__472 = this.AddError(t___17170, "must be less than or equal to " + t___17171);
                            goto fn__898;
                        }
                    }
                    double ? eq__906 = opts__897.EqualTo;
                    if (!(eq__906 == null))
                    {
                        double eq___2835 = eq__906.Value;
                        if (!(C::Float64.Compare(num__901, eq___2835) == 0.0))
                        {
                            t___17174 = field__896.SqlValue;
                            t___17175 = C::Float64.Format(eq___2835);
                            return__472 = this.AddError(t___17174, "must be equal to " + t___17175);
                            goto fn__898;
                        }
                    }
                    return__472 = this;
                }
                fn__898:
                {
                }
            }
            return return__472;
        }
        public IChangeset ValidateAcceptance(ISafeIdentifier field__908)
        {
            IChangeset return__473;
            string t___17145;
            string t___17147;
            string t___17149;
            bool t___9609;
            bool t___9610;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__473 = this;
                        goto fn__909;
                    }
                    t___17145 = field__908.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17145))
                    {
                        return__473 = this;
                        goto fn__909;
                    }
                    t___17147 = field__908.SqlValue;
                    string val__910 = C::Mapped.GetOrDefault(this._changes__809, t___17147, "");
                    bool accepted__911;
                    if (val__910 == "true")
                    {
                        accepted__911 = true;
                    }
                    else
                    {
                        if (val__910 == "1")
                        {
                            t___9610 = true;
                        }
                        else
                        {
                            if (val__910 == "yes")
                            {
                                t___9609 = true;
                            }
                            else
                            {
                                t___9609 = val__910 == "on";
                            }
                            t___9610 = t___9609;
                        }
                        accepted__911 = t___9610;
                    }
                    if (!accepted__911)
                    {
                        t___17149 = field__908.SqlValue;
                        return__473 = this.AddError(t___17149, "must be accepted");
                        goto fn__909;
                    }
                    return__473 = this;
                }
                fn__909:
                {
                }
            }
            return return__473;
        }
        public IChangeset ValidateConfirmation(ISafeIdentifier field__913, ISafeIdentifier confirmationField__914)
        {
            IChangeset return__474;
            string t___17137;
            string t___17139;
            string t___17141;
            string t___17143;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__474 = this;
                        goto fn__915;
                    }
                    t___17137 = field__913.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17137))
                    {
                        return__474 = this;
                        goto fn__915;
                    }
                    t___17139 = field__913.SqlValue;
                    string val__916 = C::Mapped.GetOrDefault(this._changes__809, t___17139, "");
                    t___17141 = confirmationField__914.SqlValue;
                    string conf__917 = C::Mapped.GetOrDefault(this._changes__809, t___17141, "");
                    if (val__916 != conf__917)
                    {
                        t___17143 = confirmationField__914.SqlValue;
                        return__474 = this.AddError(t___17143, "does not match");
                        goto fn__915;
                    }
                    return__474 = this;
                }
                fn__915:
                {
                }
            }
            return return__474;
        }
        public IChangeset ValidateContains(ISafeIdentifier field__919, string substring__920)
        {
            IChangeset return__475;
            string t___17129;
            string t___17131;
            string t___17135;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__475 = this;
                        goto fn__921;
                    }
                    t___17129 = field__919.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17129))
                    {
                        return__475 = this;
                        goto fn__921;
                    }
                    t___17131 = field__919.SqlValue;
                    string val__922 = C::Mapped.GetOrDefault(this._changes__809, t___17131, "");
                    if (!(val__922.IndexOf(substring__920) >= 0))
                    {
                        t___17135 = field__919.SqlValue;
                        return__475 = this.AddError(t___17135, "must contain the given substring");
                        goto fn__921;
                    }
                    return__475 = this;
                }
                fn__921:
                {
                }
            }
            return return__475;
        }
        public IChangeset ValidateStartsWith(ISafeIdentifier field__924, string prefix__925)
        {
            IChangeset return__476;
            string t___17120;
            string t___17122;
            int t___17126;
            string t___17127;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__476 = this;
                        goto fn__926;
                    }
                    t___17120 = field__924.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17120))
                    {
                        return__476 = this;
                        goto fn__926;
                    }
                    t___17122 = field__924.SqlValue;
                    string val__927 = C::Mapped.GetOrDefault(this._changes__809, t___17122, "");
                    int idx__928 = val__927.IndexOf(prefix__925);
                    bool starts__929;
                    if (idx__928 >= 0)
                    {
                        t___17126 = C::StringUtil.CountBetween(val__927, 0, C::StringUtil.RequireStringIndex(idx__928));
                        starts__929 = t___17126 == 0;
                    }
                    else
                    {
                        starts__929 = false;
                    }
                    if (!starts__929)
                    {
                        t___17127 = field__924.SqlValue;
                        return__476 = this.AddError(t___17127, "must start with the given prefix");
                        goto fn__926;
                    }
                    return__476 = this;
                }
                fn__926:
                {
                }
            }
            return return__476;
        }
        public IChangeset ValidateEndsWith(ISafeIdentifier field__931, string suffix__932)
        {
            IChangeset return__477;
            string t___17100;
            string t___17102;
            int t___17107;
            string t___17109;
            int t___17111;
            bool t___17112;
            int t___17116;
            int t___17117;
            string t___17118;
            bool t___9569;
            {
                {
                    if (!this._isValid__811)
                    {
                        return__477 = this;
                        goto fn__933;
                    }
                    t___17100 = field__931.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__809, t___17100))
                    {
                        return__477 = this;
                        goto fn__933;
                    }
                    t___17102 = field__931.SqlValue;
                    string val__934 = C::Mapped.GetOrDefault(this._changes__809, t___17102, "");
                    int valLen__935 = C::StringUtil.CountBetween(val__934, 0, val__934.Length);
                    t___17107 = suffix__932.Length;
                    int suffixLen__936 = C::StringUtil.CountBetween(suffix__932, 0, t___17107);
                    if (valLen__935 < suffixLen__936)
                    {
                        t___17109 = field__931.SqlValue;
                        return__477 = this.AddError(t___17109, "must end with the given suffix");
                        goto fn__933;
                    }
                    int skipCount__937 = valLen__935 - suffixLen__936;
                    int strIdx__938 = 0;
                    int i__939 = 0;
                    while (i__939 < skipCount__937)
                    {
                        t___17111 = C::StringUtil.Next(val__934, strIdx__938);
                        strIdx__938 = t___17111;
                        i__939 = i__939 + 1;
                    }
                    int sufIdx__940 = 0;
                    bool matches__941 = true;
                    while (true)
                    {
                        if (matches__941)
                        {
                            t___17112 = C::StringUtil.HasIndex(suffix__932, sufIdx__940);
                            t___9569 = t___17112;
                        }
                        else
                        {
                            t___9569 = false;
                        }
                        if (!t___9569) break;
                        if (!C::StringUtil.HasIndex(val__934, strIdx__938))
                        {
                            matches__941 = false;
                        }
                        else if (C::StringUtil.Get(val__934, strIdx__938) != C::StringUtil.Get(suffix__932, sufIdx__940))
                        {
                            matches__941 = false;
                        }
                        else
                        {
                            t___17116 = C::StringUtil.Next(val__934, strIdx__938);
                            strIdx__938 = t___17116;
                            t___17117 = C::StringUtil.Next(suffix__932, sufIdx__940);
                            sufIdx__940 = t___17117;
                        }
                    }
                    if (!matches__941)
                    {
                        t___17118 = field__931.SqlValue;
                        return__477 = this.AddError(t___17118, "must end with the given suffix");
                        goto fn__933;
                    }
                    return__477 = this;
                }
                fn__933:
                {
                }
            }
            return return__477;
        }
        SqlBoolean ParseBoolSqlPart(string val__943)
        {
            SqlBoolean return__478;
            bool t___9547;
            bool t___9548;
            bool t___9549;
            bool t___9551;
            bool t___9552;
            bool t___9553;
            {
                {
                    if (val__943 == "true")
                    {
                        t___9549 = true;
                    }
                    else
                    {
                        if (val__943 == "1")
                        {
                            t___9548 = true;
                        }
                        else
                        {
                            if (val__943 == "yes")
                            {
                                t___9547 = true;
                            }
                            else
                            {
                                t___9547 = val__943 == "on";
                            }
                            t___9548 = t___9547;
                        }
                        t___9549 = t___9548;
                    }
                    if (t___9549)
                    {
                        return__478 = new SqlBoolean(true);
                        goto fn__944;
                    }
                    if (val__943 == "false")
                    {
                        t___9553 = true;
                    }
                    else
                    {
                        if (val__943 == "0")
                        {
                            t___9552 = true;
                        }
                        else
                        {
                            if (val__943 == "no")
                            {
                                t___9551 = true;
                            }
                            else
                            {
                                t___9551 = val__943 == "off";
                            }
                            t___9552 = t___9551;
                        }
                        t___9553 = t___9552;
                    }
                    if (t___9553)
                    {
                        return__478 = new SqlBoolean(false);
                        goto fn__944;
                    }
                    throw new S::Exception();
                }
                fn__944:
                {
                }
            }
            return return__478;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__946, string val__947)
        {
            ISqlPart return__479;
            int t___9534;
            long t___9537;
            double t___9540;
            S::DateTime t___9545;
            {
                {
                    IFieldType ft__949 = fieldDef__946.FieldType;
                    if (ft__949 is StringField)
                    {
                        return__479 = new SqlString(val__947);
                        goto fn__948;
                    }
                    if (ft__949 is IntField)
                    {
                        t___9534 = C::Core.ToInt(val__947);
                        return__479 = new SqlInt32(t___9534);
                        goto fn__948;
                    }
                    if (ft__949 is Int64_Field)
                    {
                        t___9537 = C::Core.ToInt64(val__947);
                        return__479 = new SqlInt64(t___9537);
                        goto fn__948;
                    }
                    if (ft__949 is FloatField)
                    {
                        t___9540 = C::Float64.ToFloat64(val__947);
                        return__479 = new SqlFloat64(t___9540);
                        goto fn__948;
                    }
                    if (ft__949 is BoolField)
                    {
                        return__479 = this.ParseBoolSqlPart(val__947);
                        goto fn__948;
                    }
                    if (ft__949 is DateField)
                    {
                        t___9545 = T::TemporalSupport.FromIsoString(val__947);
                        return__479 = new SqlDate(t___9545);
                        goto fn__948;
                    }
                    throw new S::Exception();
                }
                fn__948:
                {
                }
            }
            return return__479;
        }
        public SqlFragment ToInsertSql()
        {
            int t___17032;
            string t___17039;
            int t___17044;
            string t___17046;
            string t___17051;
            int t___17054;
            string t___17060;
            int t___17080;
            bool t___9484;
            bool t___9485;
            FieldDef t___9492;
            ISqlPart t___9498;
            if (!this._isValid__811) throw new S::Exception();
            int i__952 = 0;
            while (true)
            {
                {
                    {
                        t___17032 = this._tableDef__807.Fields.Count;
                        if (!(i__952 < t___17032)) break;
                        FieldDef f__953 = this._tableDef__807.Fields[i__952];
                        if (f__953.Virtual) goto continue___17375;
                        ISqlPart ? dv__954 = f__953.DefaultValue;
                        if (!f__953.Nullable)
                        {
                            t___17039 = f__953.Name.SqlValue;
                            if (!C::Mapped.ContainsKey(this._changes__809, t___17039))
                            {
                                t___9484 = dv__954 == null;
                            }
                            else
                            {
                                t___9484 = false;
                            }
                            t___9485 = t___9484;
                        }
                        else
                        {
                            t___9485 = false;
                        }
                        if (t___9485) throw new S::Exception();
                    }
                    continue___17375:
                    {
                    }
                }
                i__952 = i__952 + 1;
            }
            G::IList<string> colNames__955 = new G::List<string>();
            G::IList<ISqlPart> valParts__956 = new G::List<ISqlPart>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__957 = C::Mapped.ToList(this._changes__809);
            int i__958 = 0;
            while (true)
            {
                {
                    {
                        t___17044 = pairs__957.Count;
                        if (!(i__958 < t___17044)) break;
                        G::KeyValuePair<string, string> pair__959 = pairs__957[i__958];
                        t___17046 = pair__959.Key;
                        t___9492 = this._tableDef__807.Field(t___17046);
                        FieldDef fd__960 = t___9492;
                        if (fd__960.Virtual) goto continue___17376;
                        C::Listed.Add(colNames__955, fd__960.Name.SqlValue);
                        t___17051 = pair__959.Value;
                        t___9498 = this.ValueToSqlPart(fd__960, t___17051);
                        C::Listed.Add(valParts__956, t___9498);
                    }
                    continue___17376:
                    {
                    }
                }
                i__958 = i__958 + 1;
            }
            int i__961 = 0;
            while (true)
            {
                {
                    {
                        t___17054 = this._tableDef__807.Fields.Count;
                        if (!(i__961 < t___17054)) break;
                        FieldDef f__962 = this._tableDef__807.Fields[i__961];
                        if (f__962.Virtual) goto continue___17377;
                        ISqlPart ? dv__963 = f__962.DefaultValue;
                        if (!(dv__963 == null))
                        {
                            ISqlPart dv___2843 = dv__963!;
                            t___17060 = f__962.Name.SqlValue;
                            if (!C::Mapped.ContainsKey(this._changes__809, t___17060))
                            {
                                C::Listed.Add(colNames__955, f__962.Name.SqlValue);
                                C::Listed.Add(valParts__956, dv___2843);
                            }
                        }
                    }
                    continue___17377:
                    {
                    }
                }
                i__961 = i__961 + 1;
            }
            if (valParts__956.Count == 0) throw new S::Exception();
            SqlBuilder b__964 = new SqlBuilder();
            b__964.AppendSafe("INSERT INTO ");
            b__964.AppendSafe(this._tableDef__807.TableName.SqlValue);
            b__964.AppendSafe(" (");
            G::IReadOnlyList<string> t___17073 = C::Listed.ToReadOnlyList(colNames__955);
            string fn__17030(string c__965)
            {
                return c__965;
            }
            b__964.AppendSafe(C::Listed.Join(t___17073, ", ", (S::Func<string, string>) fn__17030));
            b__964.AppendSafe(") VALUES (");
            b__964.AppendPart(valParts__956[0]);
            int j__966 = 1;
            while (true)
            {
                t___17080 = valParts__956.Count;
                if (!(j__966 < t___17080)) break;
                b__964.AppendSafe(", ");
                b__964.AppendPart(valParts__956[j__966]);
                j__966 = j__966 + 1;
            }
            b__964.AppendSafe(")");
            return b__964.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__968)
        {
            int t___17013;
            string t___17015;
            string t___17022;
            FieldDef t___9459;
            ISqlPart t___9466;
            if (!this._isValid__811) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__970 = C::Mapped.ToList(this._changes__809);
            if (pairs__970.Count == 0) throw new S::Exception();
            SqlBuilder b__971 = new SqlBuilder();
            b__971.AppendSafe("UPDATE ");
            b__971.AppendSafe(this._tableDef__807.TableName.SqlValue);
            b__971.AppendSafe(" SET ");
            int setCount__972 = 0;
            int i__973 = 0;
            while (true)
            {
                {
                    {
                        t___17013 = pairs__970.Count;
                        if (!(i__973 < t___17013)) break;
                        G::KeyValuePair<string, string> pair__974 = pairs__970[i__973];
                        t___17015 = pair__974.Key;
                        t___9459 = this._tableDef__807.Field(t___17015);
                        FieldDef fd__975 = t___9459;
                        if (fd__975.Virtual) goto continue___17378;
                        if (setCount__972 > 0) b__971.AppendSafe(", ");
                        b__971.AppendSafe(fd__975.Name.SqlValue);
                        b__971.AppendSafe(" = ");
                        t___17022 = pair__974.Value;
                        t___9466 = this.ValueToSqlPart(fd__975, t___17022);
                        b__971.AppendPart(t___9466);
                        setCount__972 = setCount__972 + 1;
                    }
                    continue___17378:
                    {
                    }
                }
                i__973 = i__973 + 1;
            }
            if (setCount__972 == 0) throw new S::Exception();
            b__971.AppendSafe(" WHERE ");
            b__971.AppendSafe(this._tableDef__807.PkName());
            b__971.AppendSafe(" = ");
            b__971.AppendInt32(id__968);
            return b__971.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__977, G::IReadOnlyDictionary<string, string> _params__978, G::IReadOnlyDictionary<string, string> _changes__979, G::IReadOnlyList<ChangesetError> _errors__980, bool _isValid__981)
        {
            this._tableDef__807 = _tableDef__977;
            this._params__808 = _params__978;
            this._changes__809 = _changes__979;
            this._errors__810 = _errors__980;
            this._isValid__811 = _isValid__981;
        }
    }
}
