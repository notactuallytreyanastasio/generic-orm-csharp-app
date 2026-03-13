using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__736;
        readonly G::IReadOnlyDictionary<string, string> _params__737;
        readonly G::IReadOnlyDictionary<string, string> _changes__738;
        readonly G::IReadOnlyList<ChangesetError> _errors__739;
        readonly bool _isValid__740;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__736;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__738;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__739;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__740;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__750)
        {
            G::IDictionary<string, string> mb__752 = new C::OrderedDictionary<string, string>();
            void fn__14193(ISafeIdentifier f__753)
            {
                string t___14191;
                string t___14188 = f__753.SqlValue;
                string val__754 = C::Mapped.GetOrDefault(this._params__737, t___14188, "");
                if (!string.IsNullOrEmpty(val__754))
                {
                    t___14191 = f__753.SqlValue;
                    mb__752[t___14191] = val__754;
                }
            }
            C::Listed.ForEach(allowedFields__750, (S::Action<ISafeIdentifier>) fn__14193);
            return new ChangesetImpl(this._tableDef__736, this._params__737, C::Mapped.ToMap(mb__752), this._errors__739, this._isValid__740);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__756)
        {
            IChangeset return__405;
            G::IReadOnlyList<ChangesetError> t___14186;
            TableDef t___8102;
            G::IReadOnlyDictionary<string, string> t___8103;
            G::IReadOnlyDictionary<string, string> t___8104;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__405 = this;
                        goto fn__757;
                    }
                    G::IList<ChangesetError> eb__758 = L::Enumerable.ToList(this._errors__739);
                    bool valid__759 = true;
                    void fn__14182(ISafeIdentifier f__760)
                    {
                        ChangesetError t___14180;
                        string t___14177 = f__760.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__738, t___14177))
                        {
                            t___14180 = new ChangesetError(f__760.SqlValue, "is required");
                            C::Listed.Add(eb__758, t___14180);
                            valid__759 = false;
                        }
                    }
                    C::Listed.ForEach(fields__756, (S::Action<ISafeIdentifier>) fn__14182);
                    t___8102 = this._tableDef__736;
                    t___8103 = this._params__737;
                    t___8104 = this._changes__738;
                    t___14186 = C::Listed.ToReadOnlyList(eb__758);
                    return__405 = new ChangesetImpl(t___8102, t___8103, t___8104, t___14186, valid__759);
                }
                fn__757:
                {
                }
            }
            return return__405;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__762, int min__763, int max__764)
        {
            IChangeset return__406;
            string t___14164;
            G::IReadOnlyList<ChangesetError> t___14175;
            bool t___8085;
            TableDef t___8091;
            G::IReadOnlyDictionary<string, string> t___8092;
            G::IReadOnlyDictionary<string, string> t___8093;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__406 = this;
                        goto fn__765;
                    }
                    t___14164 = field__762.SqlValue;
                    string val__766 = C::Mapped.GetOrDefault(this._changes__738, t___14164, "");
                    int len__767 = C::StringUtil.CountBetween(val__766, 0, val__766.Length);
                    if (len__767 < min__763)
                    {
                        t___8085 = true;
                    }
                    else
                    {
                        t___8085 = len__767 > max__764;
                    }
                    if (t___8085)
                    {
                        string msg__768 = "must be between " + S::Convert.ToString(min__763) + " and " + S::Convert.ToString(max__764) + " characters";
                        G::IList<ChangesetError> eb__769 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__769, new ChangesetError(field__762.SqlValue, msg__768));
                        t___8091 = this._tableDef__736;
                        t___8092 = this._params__737;
                        t___8093 = this._changes__738;
                        t___14175 = C::Listed.ToReadOnlyList(eb__769);
                        return__406 = new ChangesetImpl(t___8091, t___8092, t___8093, t___14175, false);
                        goto fn__765;
                    }
                    return__406 = this;
                }
                fn__765:
                {
                }
            }
            return return__406;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__771)
        {
            IChangeset return__407;
            string t___14155;
            G::IReadOnlyList<ChangesetError> t___14162;
            TableDef t___8076;
            G::IReadOnlyDictionary<string, string> t___8077;
            G::IReadOnlyDictionary<string, string> t___8078;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__407 = this;
                        goto fn__772;
                    }
                    t___14155 = field__771.SqlValue;
                    string val__773 = C::Mapped.GetOrDefault(this._changes__738, t___14155, "");
                    if (string.IsNullOrEmpty(val__773))
                    {
                        return__407 = this;
                        goto fn__772;
                    }
                    bool parseOk__774;
                    try
                    {
                        C::Core.ToInt(val__773);
                        parseOk__774 = true;
                    }
                    catch
                    {
                        parseOk__774 = false;
                    }
                    if (!parseOk__774)
                    {
                        G::IList<ChangesetError> eb__775 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__775, new ChangesetError(field__771.SqlValue, "must be an integer"));
                        t___8076 = this._tableDef__736;
                        t___8077 = this._params__737;
                        t___8078 = this._changes__738;
                        t___14162 = C::Listed.ToReadOnlyList(eb__775);
                        return__407 = new ChangesetImpl(t___8076, t___8077, t___8078, t___14162, false);
                        goto fn__772;
                    }
                    return__407 = this;
                }
                fn__772:
                {
                }
            }
            return return__407;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__777)
        {
            IChangeset return__408;
            string t___14146;
            G::IReadOnlyList<ChangesetError> t___14153;
            TableDef t___8063;
            G::IReadOnlyDictionary<string, string> t___8064;
            G::IReadOnlyDictionary<string, string> t___8065;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__408 = this;
                        goto fn__778;
                    }
                    t___14146 = field__777.SqlValue;
                    string val__779 = C::Mapped.GetOrDefault(this._changes__738, t___14146, "");
                    if (string.IsNullOrEmpty(val__779))
                    {
                        return__408 = this;
                        goto fn__778;
                    }
                    bool parseOk__780;
                    try
                    {
                        C::Core.ToInt64(val__779);
                        parseOk__780 = true;
                    }
                    catch
                    {
                        parseOk__780 = false;
                    }
                    if (!parseOk__780)
                    {
                        G::IList<ChangesetError> eb__781 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__781, new ChangesetError(field__777.SqlValue, "must be a 64-bit integer"));
                        t___8063 = this._tableDef__736;
                        t___8064 = this._params__737;
                        t___8065 = this._changes__738;
                        t___14153 = C::Listed.ToReadOnlyList(eb__781);
                        return__408 = new ChangesetImpl(t___8063, t___8064, t___8065, t___14153, false);
                        goto fn__778;
                    }
                    return__408 = this;
                }
                fn__778:
                {
                }
            }
            return return__408;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__783)
        {
            IChangeset return__409;
            string t___14137;
            G::IReadOnlyList<ChangesetError> t___14144;
            TableDef t___8050;
            G::IReadOnlyDictionary<string, string> t___8051;
            G::IReadOnlyDictionary<string, string> t___8052;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__409 = this;
                        goto fn__784;
                    }
                    t___14137 = field__783.SqlValue;
                    string val__785 = C::Mapped.GetOrDefault(this._changes__738, t___14137, "");
                    if (string.IsNullOrEmpty(val__785))
                    {
                        return__409 = this;
                        goto fn__784;
                    }
                    bool parseOk__786;
                    try
                    {
                        C::Float64.ToFloat64(val__785);
                        parseOk__786 = true;
                    }
                    catch
                    {
                        parseOk__786 = false;
                    }
                    if (!parseOk__786)
                    {
                        G::IList<ChangesetError> eb__787 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__787, new ChangesetError(field__783.SqlValue, "must be a number"));
                        t___8050 = this._tableDef__736;
                        t___8051 = this._params__737;
                        t___8052 = this._changes__738;
                        t___14144 = C::Listed.ToReadOnlyList(eb__787);
                        return__409 = new ChangesetImpl(t___8050, t___8051, t___8052, t___14144, false);
                        goto fn__784;
                    }
                    return__409 = this;
                }
                fn__784:
                {
                }
            }
            return return__409;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__789)
        {
            IChangeset return__410;
            string t___14128;
            G::IReadOnlyList<ChangesetError> t___14135;
            bool t___8025;
            bool t___8026;
            bool t___8028;
            bool t___8029;
            bool t___8031;
            TableDef t___8037;
            G::IReadOnlyDictionary<string, string> t___8038;
            G::IReadOnlyDictionary<string, string> t___8039;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__410 = this;
                        goto fn__790;
                    }
                    t___14128 = field__789.SqlValue;
                    string val__791 = C::Mapped.GetOrDefault(this._changes__738, t___14128, "");
                    if (string.IsNullOrEmpty(val__791))
                    {
                        return__410 = this;
                        goto fn__790;
                    }
                    bool isTrue__792;
                    if (val__791 == "true")
                    {
                        isTrue__792 = true;
                    }
                    else
                    {
                        if (val__791 == "1")
                        {
                            t___8026 = true;
                        }
                        else
                        {
                            if (val__791 == "yes")
                            {
                                t___8025 = true;
                            }
                            else
                            {
                                t___8025 = val__791 == "on";
                            }
                            t___8026 = t___8025;
                        }
                        isTrue__792 = t___8026;
                    }
                    bool isFalse__793;
                    if (val__791 == "false")
                    {
                        isFalse__793 = true;
                    }
                    else
                    {
                        if (val__791 == "0")
                        {
                            t___8029 = true;
                        }
                        else
                        {
                            if (val__791 == "no")
                            {
                                t___8028 = true;
                            }
                            else
                            {
                                t___8028 = val__791 == "off";
                            }
                            t___8029 = t___8028;
                        }
                        isFalse__793 = t___8029;
                    }
                    if (!isTrue__792)
                    {
                        t___8031 = !isFalse__793;
                    }
                    else
                    {
                        t___8031 = false;
                    }
                    if (t___8031)
                    {
                        G::IList<ChangesetError> eb__794 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__794, new ChangesetError(field__789.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___8037 = this._tableDef__736;
                        t___8038 = this._params__737;
                        t___8039 = this._changes__738;
                        t___14135 = C::Listed.ToReadOnlyList(eb__794);
                        return__410 = new ChangesetImpl(t___8037, t___8038, t___8039, t___14135, false);
                        goto fn__790;
                    }
                    return__410 = this;
                }
                fn__790:
                {
                }
            }
            return return__410;
        }
        public IChangeset PutChange(ISafeIdentifier field__796, string value__797)
        {
            int t___14116;
            G::IDictionary<string, string> mb__799 = new C::OrderedDictionary<string, string>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__800 = C::Mapped.ToList(this._changes__738);
            int i__801 = 0;
            while (true)
            {
                t___14116 = pairs__800.Count;
                if (!(i__801 < t___14116)) break;
                mb__799[pairs__800[i__801].Key] = pairs__800[i__801].Value;
                i__801 = i__801 + 1;
            }
            mb__799[field__796.SqlValue] = value__797;
            return new ChangesetImpl(this._tableDef__736, this._params__737, C::Mapped.ToMap(mb__799), this._errors__739, this._isValid__740);
        }
        public string GetChange(ISafeIdentifier field__803)
        {
            string t___14110 = field__803.SqlValue;
            if (!C::Mapped.ContainsKey(this._changes__738, t___14110)) throw new S::Exception();
            string t___14112 = field__803.SqlValue;
            return C::Mapped.GetOrDefault(this._changes__738, t___14112, "");
        }
        public IChangeset DeleteChange(ISafeIdentifier field__806)
        {
            int t___14097;
            G::IDictionary<string, string> mb__808 = new C::OrderedDictionary<string, string>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__809 = C::Mapped.ToList(this._changes__738);
            int i__810 = 0;
            while (true)
            {
                t___14097 = pairs__809.Count;
                if (!(i__810 < t___14097)) break;
                if (pairs__809[i__810].Key != field__806.SqlValue) mb__808[pairs__809[i__810].Key] = pairs__809[i__810].Value;
                i__810 = i__810 + 1;
            }
            return new ChangesetImpl(this._tableDef__736, this._params__737, C::Mapped.ToMap(mb__808), this._errors__739, this._isValid__740);
        }
        public IChangeset ValidateInclusion(ISafeIdentifier field__812, G::IReadOnlyList<string> allowed__813)
        {
            IChangeset return__414;
            string t___14083;
            string t___14085;
            G::IReadOnlyList<ChangesetError> t___14093;
            TableDef t___7987;
            G::IReadOnlyDictionary<string, string> t___7988;
            G::IReadOnlyDictionary<string, string> t___7989;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__414 = this;
                        goto fn__814;
                    }
                    t___14083 = field__812.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___14083))
                    {
                        return__414 = this;
                        goto fn__814;
                    }
                    t___14085 = field__812.SqlValue;
                    string val__815 = C::Mapped.GetOrDefault(this._changes__738, t___14085, "");
                    bool found__816 = false;
                    void fn__14082(string a__817)
                    {
                        if (a__817 == val__815)
                        {
                            found__816 = true;
                        }
                    }
                    C::Listed.ForEach(allowed__813, (S::Action<string>) fn__14082);
                    if (!found__816)
                    {
                        G::IList<ChangesetError> eb__818 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__818, new ChangesetError(field__812.SqlValue, "is not included in the list"));
                        t___7987 = this._tableDef__736;
                        t___7988 = this._params__737;
                        t___7989 = this._changes__738;
                        t___14093 = C::Listed.ToReadOnlyList(eb__818);
                        return__414 = new ChangesetImpl(t___7987, t___7988, t___7989, t___14093, false);
                        goto fn__814;
                    }
                    return__414 = this;
                }
                fn__814:
                {
                }
            }
            return return__414;
        }
        public IChangeset ValidateExclusion(ISafeIdentifier field__820, G::IReadOnlyList<string> disallowed__821)
        {
            IChangeset return__415;
            string t___14070;
            string t___14072;
            G::IReadOnlyList<ChangesetError> t___14080;
            TableDef t___7973;
            G::IReadOnlyDictionary<string, string> t___7974;
            G::IReadOnlyDictionary<string, string> t___7975;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__415 = this;
                        goto fn__822;
                    }
                    t___14070 = field__820.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___14070))
                    {
                        return__415 = this;
                        goto fn__822;
                    }
                    t___14072 = field__820.SqlValue;
                    string val__823 = C::Mapped.GetOrDefault(this._changes__738, t___14072, "");
                    bool found__824 = false;
                    void fn__14069(string d__825)
                    {
                        if (d__825 == val__823)
                        {
                            found__824 = true;
                        }
                    }
                    C::Listed.ForEach(disallowed__821, (S::Action<string>) fn__14069);
                    if (found__824)
                    {
                        G::IList<ChangesetError> eb__826 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__826, new ChangesetError(field__820.SqlValue, "is reserved"));
                        t___7973 = this._tableDef__736;
                        t___7974 = this._params__737;
                        t___7975 = this._changes__738;
                        t___14080 = C::Listed.ToReadOnlyList(eb__826);
                        return__415 = new ChangesetImpl(t___7973, t___7974, t___7975, t___14080, false);
                        goto fn__822;
                    }
                    return__415 = this;
                }
                fn__822:
                {
                }
            }
            return return__415;
        }
        public IChangeset ValidateNumber(ISafeIdentifier field__828, NumberValidationOpts opts__829)
        {
            IChangeset return__416;
            string t___14019;
            string t___14021;
            G::IReadOnlyList<ChangesetError> t___14027;
            G::IReadOnlyList<ChangesetError> t___14035;
            G::IReadOnlyList<ChangesetError> t___14043;
            G::IReadOnlyList<ChangesetError> t___14051;
            G::IReadOnlyList<ChangesetError> t___14059;
            G::IReadOnlyList<ChangesetError> t___14067;
            TableDef t___7906;
            G::IReadOnlyDictionary<string, string> t___7907;
            G::IReadOnlyDictionary<string, string> t___7908;
            double t___7910;
            TableDef t___7919;
            G::IReadOnlyDictionary<string, string> t___7920;
            G::IReadOnlyDictionary<string, string> t___7921;
            TableDef t___7929;
            G::IReadOnlyDictionary<string, string> t___7930;
            G::IReadOnlyDictionary<string, string> t___7931;
            TableDef t___7939;
            G::IReadOnlyDictionary<string, string> t___7940;
            G::IReadOnlyDictionary<string, string> t___7941;
            TableDef t___7949;
            G::IReadOnlyDictionary<string, string> t___7950;
            G::IReadOnlyDictionary<string, string> t___7951;
            TableDef t___7959;
            G::IReadOnlyDictionary<string, string> t___7960;
            G::IReadOnlyDictionary<string, string> t___7961;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__416 = this;
                        goto fn__830;
                    }
                    t___14019 = field__828.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___14019))
                    {
                        return__416 = this;
                        goto fn__830;
                    }
                    t___14021 = field__828.SqlValue;
                    string val__831 = C::Mapped.GetOrDefault(this._changes__738, t___14021, "");
                    bool parseOk__832;
                    try
                    {
                        C::Float64.ToFloat64(val__831);
                        parseOk__832 = true;
                    }
                    catch
                    {
                        parseOk__832 = false;
                    }
                    if (!parseOk__832)
                    {
                        G::IList<ChangesetError> eb__833 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__833, new ChangesetError(field__828.SqlValue, "must be a number"));
                        t___7906 = this._tableDef__736;
                        t___7907 = this._params__737;
                        t___7908 = this._changes__738;
                        t___14027 = C::Listed.ToReadOnlyList(eb__833);
                        return__416 = new ChangesetImpl(t___7906, t___7907, t___7908, t___14027, false);
                        goto fn__830;
                    }
                    double num__834;
                    try
                    {
                        t___7910 = C::Float64.ToFloat64(val__831);
                        num__834 = t___7910;
                    }
                    catch
                    {
                        num__834 = 0.0;
                    }
                    double ? gt__835 = opts__829.GreaterThan;
                    if (!(gt__835 == null))
                    {
                        double gt___2466 = gt__835.Value;
                        if (!(C::Float64.Compare(num__834, gt___2466) > 0.0))
                        {
                            G::IList<ChangesetError> eb__836 = L::Enumerable.ToList(this._errors__739);
                            C::Listed.Add(eb__836, new ChangesetError(field__828.SqlValue, "must be greater than " + C::Float64.Format(gt___2466)));
                            t___7919 = this._tableDef__736;
                            t___7920 = this._params__737;
                            t___7921 = this._changes__738;
                            t___14035 = C::Listed.ToReadOnlyList(eb__836);
                            return__416 = new ChangesetImpl(t___7919, t___7920, t___7921, t___14035, false);
                            goto fn__830;
                        }
                    }
                    double ? lt__837 = opts__829.LessThan;
                    if (!(lt__837 == null))
                    {
                        double lt___2467 = lt__837.Value;
                        if (!(C::Float64.Compare(num__834, lt___2467) < 0.0))
                        {
                            G::IList<ChangesetError> eb__838 = L::Enumerable.ToList(this._errors__739);
                            C::Listed.Add(eb__838, new ChangesetError(field__828.SqlValue, "must be less than " + C::Float64.Format(lt___2467)));
                            t___7929 = this._tableDef__736;
                            t___7930 = this._params__737;
                            t___7931 = this._changes__738;
                            t___14043 = C::Listed.ToReadOnlyList(eb__838);
                            return__416 = new ChangesetImpl(t___7929, t___7930, t___7931, t___14043, false);
                            goto fn__830;
                        }
                    }
                    double ? gte__839 = opts__829.GreaterThanOrEqual;
                    if (!(gte__839 == null))
                    {
                        double gte___2468 = gte__839.Value;
                        if (!(C::Float64.Compare(num__834, gte___2468) >= 0.0))
                        {
                            G::IList<ChangesetError> eb__840 = L::Enumerable.ToList(this._errors__739);
                            C::Listed.Add(eb__840, new ChangesetError(field__828.SqlValue, "must be greater than or equal to " + C::Float64.Format(gte___2468)));
                            t___7939 = this._tableDef__736;
                            t___7940 = this._params__737;
                            t___7941 = this._changes__738;
                            t___14051 = C::Listed.ToReadOnlyList(eb__840);
                            return__416 = new ChangesetImpl(t___7939, t___7940, t___7941, t___14051, false);
                            goto fn__830;
                        }
                    }
                    double ? lte__841 = opts__829.LessThanOrEqual;
                    if (!(lte__841 == null))
                    {
                        double lte___2469 = lte__841.Value;
                        if (!(C::Float64.Compare(num__834, lte___2469) <= 0.0))
                        {
                            G::IList<ChangesetError> eb__842 = L::Enumerable.ToList(this._errors__739);
                            C::Listed.Add(eb__842, new ChangesetError(field__828.SqlValue, "must be less than or equal to " + C::Float64.Format(lte___2469)));
                            t___7949 = this._tableDef__736;
                            t___7950 = this._params__737;
                            t___7951 = this._changes__738;
                            t___14059 = C::Listed.ToReadOnlyList(eb__842);
                            return__416 = new ChangesetImpl(t___7949, t___7950, t___7951, t___14059, false);
                            goto fn__830;
                        }
                    }
                    double ? eq__843 = opts__829.EqualTo;
                    if (!(eq__843 == null))
                    {
                        double eq___2470 = eq__843.Value;
                        if (!(C::Float64.Compare(num__834, eq___2470) == 0.0))
                        {
                            G::IList<ChangesetError> eb__844 = L::Enumerable.ToList(this._errors__739);
                            C::Listed.Add(eb__844, new ChangesetError(field__828.SqlValue, "must be equal to " + C::Float64.Format(eq___2470)));
                            t___7959 = this._tableDef__736;
                            t___7960 = this._params__737;
                            t___7961 = this._changes__738;
                            t___14067 = C::Listed.ToReadOnlyList(eb__844);
                            return__416 = new ChangesetImpl(t___7959, t___7960, t___7961, t___14067, false);
                            goto fn__830;
                        }
                    }
                    return__416 = this;
                }
                fn__830:
                {
                }
            }
            return return__416;
        }
        public IChangeset ValidateAcceptance(ISafeIdentifier field__846)
        {
            IChangeset return__417;
            string t___14009;
            string t___14011;
            G::IReadOnlyList<ChangesetError> t___14017;
            bool t___7884;
            bool t___7885;
            TableDef t___7892;
            G::IReadOnlyDictionary<string, string> t___7893;
            G::IReadOnlyDictionary<string, string> t___7894;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__417 = this;
                        goto fn__847;
                    }
                    t___14009 = field__846.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___14009))
                    {
                        return__417 = this;
                        goto fn__847;
                    }
                    t___14011 = field__846.SqlValue;
                    string val__848 = C::Mapped.GetOrDefault(this._changes__738, t___14011, "");
                    bool accepted__849;
                    if (val__848 == "true")
                    {
                        accepted__849 = true;
                    }
                    else
                    {
                        if (val__848 == "1")
                        {
                            t___7885 = true;
                        }
                        else
                        {
                            if (val__848 == "yes")
                            {
                                t___7884 = true;
                            }
                            else
                            {
                                t___7884 = val__848 == "on";
                            }
                            t___7885 = t___7884;
                        }
                        accepted__849 = t___7885;
                    }
                    if (!accepted__849)
                    {
                        G::IList<ChangesetError> eb__850 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__850, new ChangesetError(field__846.SqlValue, "must be accepted"));
                        t___7892 = this._tableDef__736;
                        t___7893 = this._params__737;
                        t___7894 = this._changes__738;
                        t___14017 = C::Listed.ToReadOnlyList(eb__850);
                        return__417 = new ChangesetImpl(t___7892, t___7893, t___7894, t___14017, false);
                        goto fn__847;
                    }
                    return__417 = this;
                }
                fn__847:
                {
                }
            }
            return return__417;
        }
        public IChangeset ValidateConfirmation(ISafeIdentifier field__852, ISafeIdentifier confirmationField__853)
        {
            IChangeset return__418;
            string t___13997;
            string t___13999;
            string t___14001;
            G::IReadOnlyList<ChangesetError> t___14007;
            TableDef t___7876;
            G::IReadOnlyDictionary<string, string> t___7877;
            G::IReadOnlyDictionary<string, string> t___7878;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__418 = this;
                        goto fn__854;
                    }
                    t___13997 = field__852.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___13997))
                    {
                        return__418 = this;
                        goto fn__854;
                    }
                    t___13999 = field__852.SqlValue;
                    string val__855 = C::Mapped.GetOrDefault(this._changes__738, t___13999, "");
                    t___14001 = confirmationField__853.SqlValue;
                    string conf__856 = C::Mapped.GetOrDefault(this._changes__738, t___14001, "");
                    if (val__855 != conf__856)
                    {
                        G::IList<ChangesetError> eb__857 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__857, new ChangesetError(confirmationField__853.SqlValue, "does not match"));
                        t___7876 = this._tableDef__736;
                        t___7877 = this._params__737;
                        t___7878 = this._changes__738;
                        t___14007 = C::Listed.ToReadOnlyList(eb__857);
                        return__418 = new ChangesetImpl(t___7876, t___7877, t___7878, t___14007, false);
                        goto fn__854;
                    }
                    return__418 = this;
                }
                fn__854:
                {
                }
            }
            return return__418;
        }
        public IChangeset ValidateContains(ISafeIdentifier field__859, string substring__860)
        {
            IChangeset return__419;
            string t___13985;
            string t___13987;
            G::IReadOnlyList<ChangesetError> t___13995;
            TableDef t___7861;
            G::IReadOnlyDictionary<string, string> t___7862;
            G::IReadOnlyDictionary<string, string> t___7863;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__419 = this;
                        goto fn__861;
                    }
                    t___13985 = field__859.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___13985))
                    {
                        return__419 = this;
                        goto fn__861;
                    }
                    t___13987 = field__859.SqlValue;
                    string val__862 = C::Mapped.GetOrDefault(this._changes__738, t___13987, "");
                    if (!(val__862.IndexOf(substring__860) >= 0))
                    {
                        G::IList<ChangesetError> eb__863 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__863, new ChangesetError(field__859.SqlValue, "must contain the given substring"));
                        t___7861 = this._tableDef__736;
                        t___7862 = this._params__737;
                        t___7863 = this._changes__738;
                        t___13995 = C::Listed.ToReadOnlyList(eb__863);
                        return__419 = new ChangesetImpl(t___7861, t___7862, t___7863, t___13995, false);
                        goto fn__861;
                    }
                    return__419 = this;
                }
                fn__861:
                {
                }
            }
            return return__419;
        }
        public IChangeset ValidateStartsWith(ISafeIdentifier field__865, string prefix__866)
        {
            IChangeset return__420;
            string t___13972;
            string t___13974;
            int t___13978;
            G::IReadOnlyList<ChangesetError> t___13983;
            TableDef t___7845;
            G::IReadOnlyDictionary<string, string> t___7846;
            G::IReadOnlyDictionary<string, string> t___7847;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__420 = this;
                        goto fn__867;
                    }
                    t___13972 = field__865.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___13972))
                    {
                        return__420 = this;
                        goto fn__867;
                    }
                    t___13974 = field__865.SqlValue;
                    string val__868 = C::Mapped.GetOrDefault(this._changes__738, t___13974, "");
                    int idx__869 = val__868.IndexOf(prefix__866);
                    bool starts__870;
                    if (idx__869 >= 0)
                    {
                        t___13978 = C::StringUtil.CountBetween(val__868, 0, C::StringUtil.RequireStringIndex(idx__869));
                        starts__870 = t___13978 == 0;
                    }
                    else
                    {
                        starts__870 = false;
                    }
                    if (!starts__870)
                    {
                        G::IList<ChangesetError> eb__871 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__871, new ChangesetError(field__865.SqlValue, "must start with the given prefix"));
                        t___7845 = this._tableDef__736;
                        t___7846 = this._params__737;
                        t___7847 = this._changes__738;
                        t___13983 = C::Listed.ToReadOnlyList(eb__871);
                        return__420 = new ChangesetImpl(t___7845, t___7846, t___7847, t___13983, false);
                        goto fn__867;
                    }
                    return__420 = this;
                }
                fn__867:
                {
                }
            }
            return return__420;
        }
        public IChangeset ValidateEndsWith(ISafeIdentifier field__873, string suffix__874)
        {
            IChangeset return__421;
            string t___13944;
            string t___13946;
            int t___13951;
            G::IReadOnlyList<ChangesetError> t___13957;
            int t___13959;
            bool t___13960;
            int t___13964;
            int t___13965;
            G::IReadOnlyList<ChangesetError> t___13970;
            TableDef t___7810;
            G::IReadOnlyDictionary<string, string> t___7811;
            G::IReadOnlyDictionary<string, string> t___7812;
            bool t___7816;
            TableDef t___7827;
            G::IReadOnlyDictionary<string, string> t___7828;
            G::IReadOnlyDictionary<string, string> t___7829;
            {
                {
                    if (!this._isValid__740)
                    {
                        return__421 = this;
                        goto fn__875;
                    }
                    t___13944 = field__873.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__738, t___13944))
                    {
                        return__421 = this;
                        goto fn__875;
                    }
                    t___13946 = field__873.SqlValue;
                    string val__876 = C::Mapped.GetOrDefault(this._changes__738, t___13946, "");
                    int valLen__877 = C::StringUtil.CountBetween(val__876, 0, val__876.Length);
                    t___13951 = suffix__874.Length;
                    int suffixLen__878 = C::StringUtil.CountBetween(suffix__874, 0, t___13951);
                    if (valLen__877 < suffixLen__878)
                    {
                        G::IList<ChangesetError> eb__879 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__879, new ChangesetError(field__873.SqlValue, "must end with the given suffix"));
                        t___7810 = this._tableDef__736;
                        t___7811 = this._params__737;
                        t___7812 = this._changes__738;
                        t___13957 = C::Listed.ToReadOnlyList(eb__879);
                        return__421 = new ChangesetImpl(t___7810, t___7811, t___7812, t___13957, false);
                        goto fn__875;
                    }
                    int skipCount__880 = valLen__877 - suffixLen__878;
                    int strIdx__881 = 0;
                    int i__882 = 0;
                    while (i__882 < skipCount__880)
                    {
                        t___13959 = C::StringUtil.Next(val__876, strIdx__881);
                        strIdx__881 = t___13959;
                        i__882 = i__882 + 1;
                    }
                    int sufIdx__883 = 0;
                    bool matches__884 = true;
                    while (true)
                    {
                        if (matches__884)
                        {
                            t___13960 = C::StringUtil.HasIndex(suffix__874, sufIdx__883);
                            t___7816 = t___13960;
                        }
                        else
                        {
                            t___7816 = false;
                        }
                        if (!t___7816) break;
                        if (!C::StringUtil.HasIndex(val__876, strIdx__881))
                        {
                            matches__884 = false;
                        }
                        else if (C::StringUtil.Get(val__876, strIdx__881) != C::StringUtil.Get(suffix__874, sufIdx__883))
                        {
                            matches__884 = false;
                        }
                        else
                        {
                            t___13964 = C::StringUtil.Next(val__876, strIdx__881);
                            strIdx__881 = t___13964;
                            t___13965 = C::StringUtil.Next(suffix__874, sufIdx__883);
                            sufIdx__883 = t___13965;
                        }
                    }
                    if (!matches__884)
                    {
                        G::IList<ChangesetError> eb__885 = L::Enumerable.ToList(this._errors__739);
                        C::Listed.Add(eb__885, new ChangesetError(field__873.SqlValue, "must end with the given suffix"));
                        t___7827 = this._tableDef__736;
                        t___7828 = this._params__737;
                        t___7829 = this._changes__738;
                        t___13970 = C::Listed.ToReadOnlyList(eb__885);
                        return__421 = new ChangesetImpl(t___7827, t___7828, t___7829, t___13970, false);
                        goto fn__875;
                    }
                    return__421 = this;
                }
                fn__875:
                {
                }
            }
            return return__421;
        }
        SqlBoolean ParseBoolSqlPart(string val__887)
        {
            SqlBoolean return__422;
            bool t___7787;
            bool t___7788;
            bool t___7789;
            bool t___7791;
            bool t___7792;
            bool t___7793;
            {
                {
                    if (val__887 == "true")
                    {
                        t___7789 = true;
                    }
                    else
                    {
                        if (val__887 == "1")
                        {
                            t___7788 = true;
                        }
                        else
                        {
                            if (val__887 == "yes")
                            {
                                t___7787 = true;
                            }
                            else
                            {
                                t___7787 = val__887 == "on";
                            }
                            t___7788 = t___7787;
                        }
                        t___7789 = t___7788;
                    }
                    if (t___7789)
                    {
                        return__422 = new SqlBoolean(true);
                        goto fn__888;
                    }
                    if (val__887 == "false")
                    {
                        t___7793 = true;
                    }
                    else
                    {
                        if (val__887 == "0")
                        {
                            t___7792 = true;
                        }
                        else
                        {
                            if (val__887 == "no")
                            {
                                t___7791 = true;
                            }
                            else
                            {
                                t___7791 = val__887 == "off";
                            }
                            t___7792 = t___7791;
                        }
                        t___7793 = t___7792;
                    }
                    if (t___7793)
                    {
                        return__422 = new SqlBoolean(false);
                        goto fn__888;
                    }
                    throw new S::Exception();
                }
                fn__888:
                {
                }
            }
            return return__422;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__890, string val__891)
        {
            ISqlPart return__423;
            int t___7774;
            long t___7777;
            double t___7780;
            S::DateTime t___7785;
            {
                {
                    IFieldType ft__893 = fieldDef__890.FieldType;
                    if (ft__893 is StringField)
                    {
                        return__423 = new SqlString(val__891);
                        goto fn__892;
                    }
                    if (ft__893 is IntField)
                    {
                        t___7774 = C::Core.ToInt(val__891);
                        return__423 = new SqlInt32(t___7774);
                        goto fn__892;
                    }
                    if (ft__893 is Int64_Field)
                    {
                        t___7777 = C::Core.ToInt64(val__891);
                        return__423 = new SqlInt64(t___7777);
                        goto fn__892;
                    }
                    if (ft__893 is FloatField)
                    {
                        t___7780 = C::Float64.ToFloat64(val__891);
                        return__423 = new SqlFloat64(t___7780);
                        goto fn__892;
                    }
                    if (ft__893 is BoolField)
                    {
                        return__423 = this.ParseBoolSqlPart(val__891);
                        goto fn__892;
                    }
                    if (ft__893 is DateField)
                    {
                        t___7785 = T::TemporalSupport.FromIsoString(val__891);
                        return__423 = new SqlDate(t___7785);
                        goto fn__892;
                    }
                    throw new S::Exception();
                }
                fn__892:
                {
                }
            }
            return return__423;
        }
        public SqlFragment ToInsertSql()
        {
            int t___13892;
            string t___13897;
            bool t___13898;
            int t___13903;
            string t___13905;
            string t___13909;
            int t___13924;
            bool t___7738;
            FieldDef t___7746;
            ISqlPart t___7751;
            if (!this._isValid__740) throw new S::Exception();
            int i__896 = 0;
            while (true)
            {
                t___13892 = this._tableDef__736.Fields.Count;
                if (!(i__896 < t___13892)) break;
                FieldDef f__897 = this._tableDef__736.Fields[i__896];
                if (!f__897.Nullable)
                {
                    t___13897 = f__897.Name.SqlValue;
                    t___13898 = C::Mapped.ContainsKey(this._changes__738, t___13897);
                    t___7738 = !t___13898;
                }
                else
                {
                    t___7738 = false;
                }
                if (t___7738) throw new S::Exception();
                i__896 = i__896 + 1;
            }
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__898 = C::Mapped.ToList(this._changes__738);
            if (pairs__898.Count == 0) throw new S::Exception();
            G::IList<string> colNames__899 = new G::List<string>();
            G::IList<ISqlPart> valParts__900 = new G::List<ISqlPart>();
            int i__901 = 0;
            while (true)
            {
                t___13903 = pairs__898.Count;
                if (!(i__901 < t___13903)) break;
                G::KeyValuePair<string, string> pair__902 = pairs__898[i__901];
                t___13905 = pair__902.Key;
                t___7746 = this._tableDef__736.Field(t___13905);
                FieldDef fd__903 = t___7746;
                C::Listed.Add(colNames__899, fd__903.Name.SqlValue);
                t___13909 = pair__902.Value;
                t___7751 = this.ValueToSqlPart(fd__903, t___13909);
                C::Listed.Add(valParts__900, t___7751);
                i__901 = i__901 + 1;
            }
            SqlBuilder b__904 = new SqlBuilder();
            b__904.AppendSafe("INSERT INTO ");
            b__904.AppendSafe(this._tableDef__736.TableName.SqlValue);
            b__904.AppendSafe(" (");
            G::IReadOnlyList<string> t___13917 = C::Listed.ToReadOnlyList(colNames__899);
            string fn__13890(string c__905)
            {
                return c__905;
            }
            b__904.AppendSafe(C::Listed.Join(t___13917, ", ", (S::Func<string, string>) fn__13890));
            b__904.AppendSafe(") VALUES (");
            b__904.AppendPart(valParts__900[0]);
            int j__906 = 1;
            while (true)
            {
                t___13924 = valParts__900.Count;
                if (!(j__906 < t___13924)) break;
                b__904.AppendSafe(", ");
                b__904.AppendPart(valParts__900[j__906]);
                j__906 = j__906 + 1;
            }
            b__904.AppendSafe(")");
            return b__904.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__908)
        {
            int t___13877;
            string t___13880;
            string t___13885;
            FieldDef t___7719;
            ISqlPart t___7725;
            if (!this._isValid__740) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__910 = C::Mapped.ToList(this._changes__738);
            if (pairs__910.Count == 0) throw new S::Exception();
            SqlBuilder b__911 = new SqlBuilder();
            b__911.AppendSafe("UPDATE ");
            b__911.AppendSafe(this._tableDef__736.TableName.SqlValue);
            b__911.AppendSafe(" SET ");
            int i__912 = 0;
            while (true)
            {
                t___13877 = pairs__910.Count;
                if (!(i__912 < t___13877)) break;
                if (i__912 > 0) b__911.AppendSafe(", ");
                G::KeyValuePair<string, string> pair__913 = pairs__910[i__912];
                t___13880 = pair__913.Key;
                t___7719 = this._tableDef__736.Field(t___13880);
                FieldDef fd__914 = t___7719;
                b__911.AppendSafe(fd__914.Name.SqlValue);
                b__911.AppendSafe(" = ");
                t___13885 = pair__913.Value;
                t___7725 = this.ValueToSqlPart(fd__914, t___13885);
                b__911.AppendPart(t___7725);
                i__912 = i__912 + 1;
            }
            b__911.AppendSafe(" WHERE id = ");
            b__911.AppendInt32(id__908);
            return b__911.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__916, G::IReadOnlyDictionary<string, string> _params__917, G::IReadOnlyDictionary<string, string> _changes__918, G::IReadOnlyList<ChangesetError> _errors__919, bool _isValid__920)
        {
            this._tableDef__736 = _tableDef__916;
            this._params__737 = _params__917;
            this._changes__738 = _changes__918;
            this._errors__739 = _errors__919;
            this._isValid__740 = _isValid__920;
        }
    }
}
