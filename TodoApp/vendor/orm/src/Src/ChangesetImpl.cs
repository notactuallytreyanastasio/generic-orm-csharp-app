using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
using T = TemperLang.Std.Temporal;
namespace Orm.Src
{
    class ChangesetImpl: IChangeset
    {
        readonly TableDef _tableDef__760;
        readonly G::IReadOnlyDictionary<string, string> _params__761;
        readonly G::IReadOnlyDictionary<string, string> _changes__762;
        readonly G::IReadOnlyList<ChangesetError> _errors__763;
        readonly bool _isValid__764;
        public TableDef TableDef
        {
            get
            {
                return this._tableDef__760;
            }
        }
        public G::IReadOnlyDictionary<string, string> Changes
        {
            get
            {
                return this._changes__762;
            }
        }
        public G::IReadOnlyList<ChangesetError> Errors
        {
            get
            {
                return this._errors__763;
            }
        }
        public bool IsValid
        {
            get
            {
                return this._isValid__764;
            }
        }
        public IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__774)
        {
            G::IDictionary<string, string> mb__776 = new C::OrderedDictionary<string, string>();
            void fn__15462(ISafeIdentifier f__777)
            {
                string t___15460;
                string t___15457 = f__777.SqlValue;
                string val__778 = C::Mapped.GetOrDefault(this._params__761, t___15457, "");
                if (!string.IsNullOrEmpty(val__778))
                {
                    t___15460 = f__777.SqlValue;
                    mb__776[t___15460] = val__778;
                }
            }
            C::Listed.ForEach(allowedFields__774, (S::Action<ISafeIdentifier>) fn__15462);
            return new ChangesetImpl(this._tableDef__760, this._params__761, C::Mapped.ToMap(mb__776), this._errors__763, this._isValid__764);
        }
        public IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__780)
        {
            IChangeset return__422;
            G::IReadOnlyList<ChangesetError> t___15455;
            TableDef t___8795;
            G::IReadOnlyDictionary<string, string> t___8796;
            G::IReadOnlyDictionary<string, string> t___8797;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__422 = this;
                        goto fn__781;
                    }
                    G::IList<ChangesetError> eb__782 = L::Enumerable.ToList(this._errors__763);
                    bool valid__783 = true;
                    void fn__15451(ISafeIdentifier f__784)
                    {
                        ChangesetError t___15449;
                        string t___15446 = f__784.SqlValue;
                        if (!C::Mapped.ContainsKey(this._changes__762, t___15446))
                        {
                            t___15449 = new ChangesetError(f__784.SqlValue, "is required");
                            C::Listed.Add(eb__782, t___15449);
                            valid__783 = false;
                        }
                    }
                    C::Listed.ForEach(fields__780, (S::Action<ISafeIdentifier>) fn__15451);
                    t___8795 = this._tableDef__760;
                    t___8796 = this._params__761;
                    t___8797 = this._changes__762;
                    t___15455 = C::Listed.ToReadOnlyList(eb__782);
                    return__422 = new ChangesetImpl(t___8795, t___8796, t___8797, t___15455, valid__783);
                }
                fn__781:
                {
                }
            }
            return return__422;
        }
        public IChangeset ValidateLength(ISafeIdentifier field__786, int min__787, int max__788)
        {
            IChangeset return__423;
            string t___15433;
            G::IReadOnlyList<ChangesetError> t___15444;
            bool t___8778;
            TableDef t___8784;
            G::IReadOnlyDictionary<string, string> t___8785;
            G::IReadOnlyDictionary<string, string> t___8786;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__423 = this;
                        goto fn__789;
                    }
                    t___15433 = field__786.SqlValue;
                    string val__790 = C::Mapped.GetOrDefault(this._changes__762, t___15433, "");
                    int len__791 = C::StringUtil.CountBetween(val__790, 0, val__790.Length);
                    if (len__791 < min__787)
                    {
                        t___8778 = true;
                    }
                    else
                    {
                        t___8778 = len__791 > max__788;
                    }
                    if (t___8778)
                    {
                        string msg__792 = "must be between " + S::Convert.ToString(min__787) + " and " + S::Convert.ToString(max__788) + " characters";
                        G::IList<ChangesetError> eb__793 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__793, new ChangesetError(field__786.SqlValue, msg__792));
                        t___8784 = this._tableDef__760;
                        t___8785 = this._params__761;
                        t___8786 = this._changes__762;
                        t___15444 = C::Listed.ToReadOnlyList(eb__793);
                        return__423 = new ChangesetImpl(t___8784, t___8785, t___8786, t___15444, false);
                        goto fn__789;
                    }
                    return__423 = this;
                }
                fn__789:
                {
                }
            }
            return return__423;
        }
        public IChangeset ValidateInt(ISafeIdentifier field__795)
        {
            IChangeset return__424;
            string t___15424;
            G::IReadOnlyList<ChangesetError> t___15431;
            TableDef t___8769;
            G::IReadOnlyDictionary<string, string> t___8770;
            G::IReadOnlyDictionary<string, string> t___8771;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__424 = this;
                        goto fn__796;
                    }
                    t___15424 = field__795.SqlValue;
                    string val__797 = C::Mapped.GetOrDefault(this._changes__762, t___15424, "");
                    if (string.IsNullOrEmpty(val__797))
                    {
                        return__424 = this;
                        goto fn__796;
                    }
                    bool parseOk__798;
                    try
                    {
                        C::Core.ToInt(val__797);
                        parseOk__798 = true;
                    }
                    catch
                    {
                        parseOk__798 = false;
                    }
                    if (!parseOk__798)
                    {
                        G::IList<ChangesetError> eb__799 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__799, new ChangesetError(field__795.SqlValue, "must be an integer"));
                        t___8769 = this._tableDef__760;
                        t___8770 = this._params__761;
                        t___8771 = this._changes__762;
                        t___15431 = C::Listed.ToReadOnlyList(eb__799);
                        return__424 = new ChangesetImpl(t___8769, t___8770, t___8771, t___15431, false);
                        goto fn__796;
                    }
                    return__424 = this;
                }
                fn__796:
                {
                }
            }
            return return__424;
        }
        public IChangeset ValidateInt64(ISafeIdentifier field__801)
        {
            IChangeset return__425;
            string t___15415;
            G::IReadOnlyList<ChangesetError> t___15422;
            TableDef t___8756;
            G::IReadOnlyDictionary<string, string> t___8757;
            G::IReadOnlyDictionary<string, string> t___8758;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__425 = this;
                        goto fn__802;
                    }
                    t___15415 = field__801.SqlValue;
                    string val__803 = C::Mapped.GetOrDefault(this._changes__762, t___15415, "");
                    if (string.IsNullOrEmpty(val__803))
                    {
                        return__425 = this;
                        goto fn__802;
                    }
                    bool parseOk__804;
                    try
                    {
                        C::Core.ToInt64(val__803);
                        parseOk__804 = true;
                    }
                    catch
                    {
                        parseOk__804 = false;
                    }
                    if (!parseOk__804)
                    {
                        G::IList<ChangesetError> eb__805 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__805, new ChangesetError(field__801.SqlValue, "must be a 64-bit integer"));
                        t___8756 = this._tableDef__760;
                        t___8757 = this._params__761;
                        t___8758 = this._changes__762;
                        t___15422 = C::Listed.ToReadOnlyList(eb__805);
                        return__425 = new ChangesetImpl(t___8756, t___8757, t___8758, t___15422, false);
                        goto fn__802;
                    }
                    return__425 = this;
                }
                fn__802:
                {
                }
            }
            return return__425;
        }
        public IChangeset ValidateFloat(ISafeIdentifier field__807)
        {
            IChangeset return__426;
            string t___15406;
            G::IReadOnlyList<ChangesetError> t___15413;
            TableDef t___8743;
            G::IReadOnlyDictionary<string, string> t___8744;
            G::IReadOnlyDictionary<string, string> t___8745;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__426 = this;
                        goto fn__808;
                    }
                    t___15406 = field__807.SqlValue;
                    string val__809 = C::Mapped.GetOrDefault(this._changes__762, t___15406, "");
                    if (string.IsNullOrEmpty(val__809))
                    {
                        return__426 = this;
                        goto fn__808;
                    }
                    bool parseOk__810;
                    try
                    {
                        C::Float64.ToFloat64(val__809);
                        parseOk__810 = true;
                    }
                    catch
                    {
                        parseOk__810 = false;
                    }
                    if (!parseOk__810)
                    {
                        G::IList<ChangesetError> eb__811 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__811, new ChangesetError(field__807.SqlValue, "must be a number"));
                        t___8743 = this._tableDef__760;
                        t___8744 = this._params__761;
                        t___8745 = this._changes__762;
                        t___15413 = C::Listed.ToReadOnlyList(eb__811);
                        return__426 = new ChangesetImpl(t___8743, t___8744, t___8745, t___15413, false);
                        goto fn__808;
                    }
                    return__426 = this;
                }
                fn__808:
                {
                }
            }
            return return__426;
        }
        public IChangeset ValidateBool(ISafeIdentifier field__813)
        {
            IChangeset return__427;
            string t___15397;
            G::IReadOnlyList<ChangesetError> t___15404;
            bool t___8718;
            bool t___8719;
            bool t___8721;
            bool t___8722;
            bool t___8724;
            TableDef t___8730;
            G::IReadOnlyDictionary<string, string> t___8731;
            G::IReadOnlyDictionary<string, string> t___8732;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__427 = this;
                        goto fn__814;
                    }
                    t___15397 = field__813.SqlValue;
                    string val__815 = C::Mapped.GetOrDefault(this._changes__762, t___15397, "");
                    if (string.IsNullOrEmpty(val__815))
                    {
                        return__427 = this;
                        goto fn__814;
                    }
                    bool isTrue__816;
                    if (val__815 == "true")
                    {
                        isTrue__816 = true;
                    }
                    else
                    {
                        if (val__815 == "1")
                        {
                            t___8719 = true;
                        }
                        else
                        {
                            if (val__815 == "yes")
                            {
                                t___8718 = true;
                            }
                            else
                            {
                                t___8718 = val__815 == "on";
                            }
                            t___8719 = t___8718;
                        }
                        isTrue__816 = t___8719;
                    }
                    bool isFalse__817;
                    if (val__815 == "false")
                    {
                        isFalse__817 = true;
                    }
                    else
                    {
                        if (val__815 == "0")
                        {
                            t___8722 = true;
                        }
                        else
                        {
                            if (val__815 == "no")
                            {
                                t___8721 = true;
                            }
                            else
                            {
                                t___8721 = val__815 == "off";
                            }
                            t___8722 = t___8721;
                        }
                        isFalse__817 = t___8722;
                    }
                    if (!isTrue__816)
                    {
                        t___8724 = !isFalse__817;
                    }
                    else
                    {
                        t___8724 = false;
                    }
                    if (t___8724)
                    {
                        G::IList<ChangesetError> eb__818 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__818, new ChangesetError(field__813.SqlValue, "must be a boolean (true/false/1/0/yes/no/on/off)"));
                        t___8730 = this._tableDef__760;
                        t___8731 = this._params__761;
                        t___8732 = this._changes__762;
                        t___15404 = C::Listed.ToReadOnlyList(eb__818);
                        return__427 = new ChangesetImpl(t___8730, t___8731, t___8732, t___15404, false);
                        goto fn__814;
                    }
                    return__427 = this;
                }
                fn__814:
                {
                }
            }
            return return__427;
        }
        public IChangeset PutChange(ISafeIdentifier field__820, string value__821)
        {
            int t___15385;
            G::IDictionary<string, string> mb__823 = new C::OrderedDictionary<string, string>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__824 = C::Mapped.ToList(this._changes__762);
            int i__825 = 0;
            while (true)
            {
                t___15385 = pairs__824.Count;
                if (!(i__825 < t___15385)) break;
                mb__823[pairs__824[i__825].Key] = pairs__824[i__825].Value;
                i__825 = i__825 + 1;
            }
            mb__823[field__820.SqlValue] = value__821;
            return new ChangesetImpl(this._tableDef__760, this._params__761, C::Mapped.ToMap(mb__823), this._errors__763, this._isValid__764);
        }
        public string GetChange(ISafeIdentifier field__827)
        {
            string t___15379 = field__827.SqlValue;
            if (!C::Mapped.ContainsKey(this._changes__762, t___15379)) throw new S::Exception();
            string t___15381 = field__827.SqlValue;
            return C::Mapped.GetOrDefault(this._changes__762, t___15381, "");
        }
        public IChangeset DeleteChange(ISafeIdentifier field__830)
        {
            int t___15366;
            G::IDictionary<string, string> mb__832 = new C::OrderedDictionary<string, string>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__833 = C::Mapped.ToList(this._changes__762);
            int i__834 = 0;
            while (true)
            {
                t___15366 = pairs__833.Count;
                if (!(i__834 < t___15366)) break;
                if (pairs__833[i__834].Key != field__830.SqlValue) mb__832[pairs__833[i__834].Key] = pairs__833[i__834].Value;
                i__834 = i__834 + 1;
            }
            return new ChangesetImpl(this._tableDef__760, this._params__761, C::Mapped.ToMap(mb__832), this._errors__763, this._isValid__764);
        }
        public IChangeset ValidateInclusion(ISafeIdentifier field__836, G::IReadOnlyList<string> allowed__837)
        {
            IChangeset return__431;
            string t___15352;
            string t___15354;
            G::IReadOnlyList<ChangesetError> t___15362;
            TableDef t___8680;
            G::IReadOnlyDictionary<string, string> t___8681;
            G::IReadOnlyDictionary<string, string> t___8682;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__431 = this;
                        goto fn__838;
                    }
                    t___15352 = field__836.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15352))
                    {
                        return__431 = this;
                        goto fn__838;
                    }
                    t___15354 = field__836.SqlValue;
                    string val__839 = C::Mapped.GetOrDefault(this._changes__762, t___15354, "");
                    bool found__840 = false;
                    void fn__15351(string a__841)
                    {
                        if (a__841 == val__839)
                        {
                            found__840 = true;
                        }
                    }
                    C::Listed.ForEach(allowed__837, (S::Action<string>) fn__15351);
                    if (!found__840)
                    {
                        G::IList<ChangesetError> eb__842 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__842, new ChangesetError(field__836.SqlValue, "is not included in the list"));
                        t___8680 = this._tableDef__760;
                        t___8681 = this._params__761;
                        t___8682 = this._changes__762;
                        t___15362 = C::Listed.ToReadOnlyList(eb__842);
                        return__431 = new ChangesetImpl(t___8680, t___8681, t___8682, t___15362, false);
                        goto fn__838;
                    }
                    return__431 = this;
                }
                fn__838:
                {
                }
            }
            return return__431;
        }
        public IChangeset ValidateExclusion(ISafeIdentifier field__844, G::IReadOnlyList<string> disallowed__845)
        {
            IChangeset return__432;
            string t___15339;
            string t___15341;
            G::IReadOnlyList<ChangesetError> t___15349;
            TableDef t___8666;
            G::IReadOnlyDictionary<string, string> t___8667;
            G::IReadOnlyDictionary<string, string> t___8668;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__432 = this;
                        goto fn__846;
                    }
                    t___15339 = field__844.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15339))
                    {
                        return__432 = this;
                        goto fn__846;
                    }
                    t___15341 = field__844.SqlValue;
                    string val__847 = C::Mapped.GetOrDefault(this._changes__762, t___15341, "");
                    bool found__848 = false;
                    void fn__15338(string d__849)
                    {
                        if (d__849 == val__847)
                        {
                            found__848 = true;
                        }
                    }
                    C::Listed.ForEach(disallowed__845, (S::Action<string>) fn__15338);
                    if (found__848)
                    {
                        G::IList<ChangesetError> eb__850 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__850, new ChangesetError(field__844.SqlValue, "is reserved"));
                        t___8666 = this._tableDef__760;
                        t___8667 = this._params__761;
                        t___8668 = this._changes__762;
                        t___15349 = C::Listed.ToReadOnlyList(eb__850);
                        return__432 = new ChangesetImpl(t___8666, t___8667, t___8668, t___15349, false);
                        goto fn__846;
                    }
                    return__432 = this;
                }
                fn__846:
                {
                }
            }
            return return__432;
        }
        public IChangeset ValidateNumber(ISafeIdentifier field__852, NumberValidationOpts opts__853)
        {
            IChangeset return__433;
            string t___15288;
            string t___15290;
            G::IReadOnlyList<ChangesetError> t___15296;
            G::IReadOnlyList<ChangesetError> t___15304;
            G::IReadOnlyList<ChangesetError> t___15312;
            G::IReadOnlyList<ChangesetError> t___15320;
            G::IReadOnlyList<ChangesetError> t___15328;
            G::IReadOnlyList<ChangesetError> t___15336;
            TableDef t___8599;
            G::IReadOnlyDictionary<string, string> t___8600;
            G::IReadOnlyDictionary<string, string> t___8601;
            double t___8603;
            TableDef t___8612;
            G::IReadOnlyDictionary<string, string> t___8613;
            G::IReadOnlyDictionary<string, string> t___8614;
            TableDef t___8622;
            G::IReadOnlyDictionary<string, string> t___8623;
            G::IReadOnlyDictionary<string, string> t___8624;
            TableDef t___8632;
            G::IReadOnlyDictionary<string, string> t___8633;
            G::IReadOnlyDictionary<string, string> t___8634;
            TableDef t___8642;
            G::IReadOnlyDictionary<string, string> t___8643;
            G::IReadOnlyDictionary<string, string> t___8644;
            TableDef t___8652;
            G::IReadOnlyDictionary<string, string> t___8653;
            G::IReadOnlyDictionary<string, string> t___8654;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__433 = this;
                        goto fn__854;
                    }
                    t___15288 = field__852.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15288))
                    {
                        return__433 = this;
                        goto fn__854;
                    }
                    t___15290 = field__852.SqlValue;
                    string val__855 = C::Mapped.GetOrDefault(this._changes__762, t___15290, "");
                    bool parseOk__856;
                    try
                    {
                        C::Float64.ToFloat64(val__855);
                        parseOk__856 = true;
                    }
                    catch
                    {
                        parseOk__856 = false;
                    }
                    if (!parseOk__856)
                    {
                        G::IList<ChangesetError> eb__857 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__857, new ChangesetError(field__852.SqlValue, "must be a number"));
                        t___8599 = this._tableDef__760;
                        t___8600 = this._params__761;
                        t___8601 = this._changes__762;
                        t___15296 = C::Listed.ToReadOnlyList(eb__857);
                        return__433 = new ChangesetImpl(t___8599, t___8600, t___8601, t___15296, false);
                        goto fn__854;
                    }
                    double num__858;
                    try
                    {
                        t___8603 = C::Float64.ToFloat64(val__855);
                        num__858 = t___8603;
                    }
                    catch
                    {
                        num__858 = 0.0;
                    }
                    double ? gt__859 = opts__853.GreaterThan;
                    if (!(gt__859 == null))
                    {
                        double gt___2610 = gt__859.Value;
                        if (!(C::Float64.Compare(num__858, gt___2610) > 0.0))
                        {
                            G::IList<ChangesetError> eb__860 = L::Enumerable.ToList(this._errors__763);
                            C::Listed.Add(eb__860, new ChangesetError(field__852.SqlValue, "must be greater than " + C::Float64.Format(gt___2610)));
                            t___8612 = this._tableDef__760;
                            t___8613 = this._params__761;
                            t___8614 = this._changes__762;
                            t___15304 = C::Listed.ToReadOnlyList(eb__860);
                            return__433 = new ChangesetImpl(t___8612, t___8613, t___8614, t___15304, false);
                            goto fn__854;
                        }
                    }
                    double ? lt__861 = opts__853.LessThan;
                    if (!(lt__861 == null))
                    {
                        double lt___2611 = lt__861.Value;
                        if (!(C::Float64.Compare(num__858, lt___2611) < 0.0))
                        {
                            G::IList<ChangesetError> eb__862 = L::Enumerable.ToList(this._errors__763);
                            C::Listed.Add(eb__862, new ChangesetError(field__852.SqlValue, "must be less than " + C::Float64.Format(lt___2611)));
                            t___8622 = this._tableDef__760;
                            t___8623 = this._params__761;
                            t___8624 = this._changes__762;
                            t___15312 = C::Listed.ToReadOnlyList(eb__862);
                            return__433 = new ChangesetImpl(t___8622, t___8623, t___8624, t___15312, false);
                            goto fn__854;
                        }
                    }
                    double ? gte__863 = opts__853.GreaterThanOrEqual;
                    if (!(gte__863 == null))
                    {
                        double gte___2612 = gte__863.Value;
                        if (!(C::Float64.Compare(num__858, gte___2612) >= 0.0))
                        {
                            G::IList<ChangesetError> eb__864 = L::Enumerable.ToList(this._errors__763);
                            C::Listed.Add(eb__864, new ChangesetError(field__852.SqlValue, "must be greater than or equal to " + C::Float64.Format(gte___2612)));
                            t___8632 = this._tableDef__760;
                            t___8633 = this._params__761;
                            t___8634 = this._changes__762;
                            t___15320 = C::Listed.ToReadOnlyList(eb__864);
                            return__433 = new ChangesetImpl(t___8632, t___8633, t___8634, t___15320, false);
                            goto fn__854;
                        }
                    }
                    double ? lte__865 = opts__853.LessThanOrEqual;
                    if (!(lte__865 == null))
                    {
                        double lte___2613 = lte__865.Value;
                        if (!(C::Float64.Compare(num__858, lte___2613) <= 0.0))
                        {
                            G::IList<ChangesetError> eb__866 = L::Enumerable.ToList(this._errors__763);
                            C::Listed.Add(eb__866, new ChangesetError(field__852.SqlValue, "must be less than or equal to " + C::Float64.Format(lte___2613)));
                            t___8642 = this._tableDef__760;
                            t___8643 = this._params__761;
                            t___8644 = this._changes__762;
                            t___15328 = C::Listed.ToReadOnlyList(eb__866);
                            return__433 = new ChangesetImpl(t___8642, t___8643, t___8644, t___15328, false);
                            goto fn__854;
                        }
                    }
                    double ? eq__867 = opts__853.EqualTo;
                    if (!(eq__867 == null))
                    {
                        double eq___2614 = eq__867.Value;
                        if (!(C::Float64.Compare(num__858, eq___2614) == 0.0))
                        {
                            G::IList<ChangesetError> eb__868 = L::Enumerable.ToList(this._errors__763);
                            C::Listed.Add(eb__868, new ChangesetError(field__852.SqlValue, "must be equal to " + C::Float64.Format(eq___2614)));
                            t___8652 = this._tableDef__760;
                            t___8653 = this._params__761;
                            t___8654 = this._changes__762;
                            t___15336 = C::Listed.ToReadOnlyList(eb__868);
                            return__433 = new ChangesetImpl(t___8652, t___8653, t___8654, t___15336, false);
                            goto fn__854;
                        }
                    }
                    return__433 = this;
                }
                fn__854:
                {
                }
            }
            return return__433;
        }
        public IChangeset ValidateAcceptance(ISafeIdentifier field__870)
        {
            IChangeset return__434;
            string t___15278;
            string t___15280;
            G::IReadOnlyList<ChangesetError> t___15286;
            bool t___8577;
            bool t___8578;
            TableDef t___8585;
            G::IReadOnlyDictionary<string, string> t___8586;
            G::IReadOnlyDictionary<string, string> t___8587;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__434 = this;
                        goto fn__871;
                    }
                    t___15278 = field__870.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15278))
                    {
                        return__434 = this;
                        goto fn__871;
                    }
                    t___15280 = field__870.SqlValue;
                    string val__872 = C::Mapped.GetOrDefault(this._changes__762, t___15280, "");
                    bool accepted__873;
                    if (val__872 == "true")
                    {
                        accepted__873 = true;
                    }
                    else
                    {
                        if (val__872 == "1")
                        {
                            t___8578 = true;
                        }
                        else
                        {
                            if (val__872 == "yes")
                            {
                                t___8577 = true;
                            }
                            else
                            {
                                t___8577 = val__872 == "on";
                            }
                            t___8578 = t___8577;
                        }
                        accepted__873 = t___8578;
                    }
                    if (!accepted__873)
                    {
                        G::IList<ChangesetError> eb__874 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__874, new ChangesetError(field__870.SqlValue, "must be accepted"));
                        t___8585 = this._tableDef__760;
                        t___8586 = this._params__761;
                        t___8587 = this._changes__762;
                        t___15286 = C::Listed.ToReadOnlyList(eb__874);
                        return__434 = new ChangesetImpl(t___8585, t___8586, t___8587, t___15286, false);
                        goto fn__871;
                    }
                    return__434 = this;
                }
                fn__871:
                {
                }
            }
            return return__434;
        }
        public IChangeset ValidateConfirmation(ISafeIdentifier field__876, ISafeIdentifier confirmationField__877)
        {
            IChangeset return__435;
            string t___15266;
            string t___15268;
            string t___15270;
            G::IReadOnlyList<ChangesetError> t___15276;
            TableDef t___8569;
            G::IReadOnlyDictionary<string, string> t___8570;
            G::IReadOnlyDictionary<string, string> t___8571;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__435 = this;
                        goto fn__878;
                    }
                    t___15266 = field__876.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15266))
                    {
                        return__435 = this;
                        goto fn__878;
                    }
                    t___15268 = field__876.SqlValue;
                    string val__879 = C::Mapped.GetOrDefault(this._changes__762, t___15268, "");
                    t___15270 = confirmationField__877.SqlValue;
                    string conf__880 = C::Mapped.GetOrDefault(this._changes__762, t___15270, "");
                    if (val__879 != conf__880)
                    {
                        G::IList<ChangesetError> eb__881 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__881, new ChangesetError(confirmationField__877.SqlValue, "does not match"));
                        t___8569 = this._tableDef__760;
                        t___8570 = this._params__761;
                        t___8571 = this._changes__762;
                        t___15276 = C::Listed.ToReadOnlyList(eb__881);
                        return__435 = new ChangesetImpl(t___8569, t___8570, t___8571, t___15276, false);
                        goto fn__878;
                    }
                    return__435 = this;
                }
                fn__878:
                {
                }
            }
            return return__435;
        }
        public IChangeset ValidateContains(ISafeIdentifier field__883, string substring__884)
        {
            IChangeset return__436;
            string t___15254;
            string t___15256;
            G::IReadOnlyList<ChangesetError> t___15264;
            TableDef t___8554;
            G::IReadOnlyDictionary<string, string> t___8555;
            G::IReadOnlyDictionary<string, string> t___8556;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__436 = this;
                        goto fn__885;
                    }
                    t___15254 = field__883.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15254))
                    {
                        return__436 = this;
                        goto fn__885;
                    }
                    t___15256 = field__883.SqlValue;
                    string val__886 = C::Mapped.GetOrDefault(this._changes__762, t___15256, "");
                    if (!(val__886.IndexOf(substring__884) >= 0))
                    {
                        G::IList<ChangesetError> eb__887 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__887, new ChangesetError(field__883.SqlValue, "must contain the given substring"));
                        t___8554 = this._tableDef__760;
                        t___8555 = this._params__761;
                        t___8556 = this._changes__762;
                        t___15264 = C::Listed.ToReadOnlyList(eb__887);
                        return__436 = new ChangesetImpl(t___8554, t___8555, t___8556, t___15264, false);
                        goto fn__885;
                    }
                    return__436 = this;
                }
                fn__885:
                {
                }
            }
            return return__436;
        }
        public IChangeset ValidateStartsWith(ISafeIdentifier field__889, string prefix__890)
        {
            IChangeset return__437;
            string t___15241;
            string t___15243;
            int t___15247;
            G::IReadOnlyList<ChangesetError> t___15252;
            TableDef t___8538;
            G::IReadOnlyDictionary<string, string> t___8539;
            G::IReadOnlyDictionary<string, string> t___8540;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__437 = this;
                        goto fn__891;
                    }
                    t___15241 = field__889.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15241))
                    {
                        return__437 = this;
                        goto fn__891;
                    }
                    t___15243 = field__889.SqlValue;
                    string val__892 = C::Mapped.GetOrDefault(this._changes__762, t___15243, "");
                    int idx__893 = val__892.IndexOf(prefix__890);
                    bool starts__894;
                    if (idx__893 >= 0)
                    {
                        t___15247 = C::StringUtil.CountBetween(val__892, 0, C::StringUtil.RequireStringIndex(idx__893));
                        starts__894 = t___15247 == 0;
                    }
                    else
                    {
                        starts__894 = false;
                    }
                    if (!starts__894)
                    {
                        G::IList<ChangesetError> eb__895 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__895, new ChangesetError(field__889.SqlValue, "must start with the given prefix"));
                        t___8538 = this._tableDef__760;
                        t___8539 = this._params__761;
                        t___8540 = this._changes__762;
                        t___15252 = C::Listed.ToReadOnlyList(eb__895);
                        return__437 = new ChangesetImpl(t___8538, t___8539, t___8540, t___15252, false);
                        goto fn__891;
                    }
                    return__437 = this;
                }
                fn__891:
                {
                }
            }
            return return__437;
        }
        public IChangeset ValidateEndsWith(ISafeIdentifier field__897, string suffix__898)
        {
            IChangeset return__438;
            string t___15213;
            string t___15215;
            int t___15220;
            G::IReadOnlyList<ChangesetError> t___15226;
            int t___15228;
            bool t___15229;
            int t___15233;
            int t___15234;
            G::IReadOnlyList<ChangesetError> t___15239;
            TableDef t___8503;
            G::IReadOnlyDictionary<string, string> t___8504;
            G::IReadOnlyDictionary<string, string> t___8505;
            bool t___8509;
            TableDef t___8520;
            G::IReadOnlyDictionary<string, string> t___8521;
            G::IReadOnlyDictionary<string, string> t___8522;
            {
                {
                    if (!this._isValid__764)
                    {
                        return__438 = this;
                        goto fn__899;
                    }
                    t___15213 = field__897.SqlValue;
                    if (!C::Mapped.ContainsKey(this._changes__762, t___15213))
                    {
                        return__438 = this;
                        goto fn__899;
                    }
                    t___15215 = field__897.SqlValue;
                    string val__900 = C::Mapped.GetOrDefault(this._changes__762, t___15215, "");
                    int valLen__901 = C::StringUtil.CountBetween(val__900, 0, val__900.Length);
                    t___15220 = suffix__898.Length;
                    int suffixLen__902 = C::StringUtil.CountBetween(suffix__898, 0, t___15220);
                    if (valLen__901 < suffixLen__902)
                    {
                        G::IList<ChangesetError> eb__903 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__903, new ChangesetError(field__897.SqlValue, "must end with the given suffix"));
                        t___8503 = this._tableDef__760;
                        t___8504 = this._params__761;
                        t___8505 = this._changes__762;
                        t___15226 = C::Listed.ToReadOnlyList(eb__903);
                        return__438 = new ChangesetImpl(t___8503, t___8504, t___8505, t___15226, false);
                        goto fn__899;
                    }
                    int skipCount__904 = valLen__901 - suffixLen__902;
                    int strIdx__905 = 0;
                    int i__906 = 0;
                    while (i__906 < skipCount__904)
                    {
                        t___15228 = C::StringUtil.Next(val__900, strIdx__905);
                        strIdx__905 = t___15228;
                        i__906 = i__906 + 1;
                    }
                    int sufIdx__907 = 0;
                    bool matches__908 = true;
                    while (true)
                    {
                        if (matches__908)
                        {
                            t___15229 = C::StringUtil.HasIndex(suffix__898, sufIdx__907);
                            t___8509 = t___15229;
                        }
                        else
                        {
                            t___8509 = false;
                        }
                        if (!t___8509) break;
                        if (!C::StringUtil.HasIndex(val__900, strIdx__905))
                        {
                            matches__908 = false;
                        }
                        else if (C::StringUtil.Get(val__900, strIdx__905) != C::StringUtil.Get(suffix__898, sufIdx__907))
                        {
                            matches__908 = false;
                        }
                        else
                        {
                            t___15233 = C::StringUtil.Next(val__900, strIdx__905);
                            strIdx__905 = t___15233;
                            t___15234 = C::StringUtil.Next(suffix__898, sufIdx__907);
                            sufIdx__907 = t___15234;
                        }
                    }
                    if (!matches__908)
                    {
                        G::IList<ChangesetError> eb__909 = L::Enumerable.ToList(this._errors__763);
                        C::Listed.Add(eb__909, new ChangesetError(field__897.SqlValue, "must end with the given suffix"));
                        t___8520 = this._tableDef__760;
                        t___8521 = this._params__761;
                        t___8522 = this._changes__762;
                        t___15239 = C::Listed.ToReadOnlyList(eb__909);
                        return__438 = new ChangesetImpl(t___8520, t___8521, t___8522, t___15239, false);
                        goto fn__899;
                    }
                    return__438 = this;
                }
                fn__899:
                {
                }
            }
            return return__438;
        }
        SqlBoolean ParseBoolSqlPart(string val__911)
        {
            SqlBoolean return__439;
            bool t___8480;
            bool t___8481;
            bool t___8482;
            bool t___8484;
            bool t___8485;
            bool t___8486;
            {
                {
                    if (val__911 == "true")
                    {
                        t___8482 = true;
                    }
                    else
                    {
                        if (val__911 == "1")
                        {
                            t___8481 = true;
                        }
                        else
                        {
                            if (val__911 == "yes")
                            {
                                t___8480 = true;
                            }
                            else
                            {
                                t___8480 = val__911 == "on";
                            }
                            t___8481 = t___8480;
                        }
                        t___8482 = t___8481;
                    }
                    if (t___8482)
                    {
                        return__439 = new SqlBoolean(true);
                        goto fn__912;
                    }
                    if (val__911 == "false")
                    {
                        t___8486 = true;
                    }
                    else
                    {
                        if (val__911 == "0")
                        {
                            t___8485 = true;
                        }
                        else
                        {
                            if (val__911 == "no")
                            {
                                t___8484 = true;
                            }
                            else
                            {
                                t___8484 = val__911 == "off";
                            }
                            t___8485 = t___8484;
                        }
                        t___8486 = t___8485;
                    }
                    if (t___8486)
                    {
                        return__439 = new SqlBoolean(false);
                        goto fn__912;
                    }
                    throw new S::Exception();
                }
                fn__912:
                {
                }
            }
            return return__439;
        }
        ISqlPart ValueToSqlPart(FieldDef fieldDef__914, string val__915)
        {
            ISqlPart return__440;
            int t___8467;
            long t___8470;
            double t___8473;
            S::DateTime t___8478;
            {
                {
                    IFieldType ft__917 = fieldDef__914.FieldType;
                    if (ft__917 is StringField)
                    {
                        return__440 = new SqlString(val__915);
                        goto fn__916;
                    }
                    if (ft__917 is IntField)
                    {
                        t___8467 = C::Core.ToInt(val__915);
                        return__440 = new SqlInt32(t___8467);
                        goto fn__916;
                    }
                    if (ft__917 is Int64_Field)
                    {
                        t___8470 = C::Core.ToInt64(val__915);
                        return__440 = new SqlInt64(t___8470);
                        goto fn__916;
                    }
                    if (ft__917 is FloatField)
                    {
                        t___8473 = C::Float64.ToFloat64(val__915);
                        return__440 = new SqlFloat64(t___8473);
                        goto fn__916;
                    }
                    if (ft__917 is BoolField)
                    {
                        return__440 = this.ParseBoolSqlPart(val__915);
                        goto fn__916;
                    }
                    if (ft__917 is DateField)
                    {
                        t___8478 = T::TemporalSupport.FromIsoString(val__915);
                        return__440 = new SqlDate(t___8478);
                        goto fn__916;
                    }
                    throw new S::Exception();
                }
                fn__916:
                {
                }
            }
            return return__440;
        }
        public SqlFragment ToInsertSql()
        {
            int t___15145;
            string t___15152;
            int t___15157;
            string t___15159;
            string t___15164;
            int t___15167;
            string t___15173;
            int t___15193;
            bool t___8417;
            bool t___8418;
            FieldDef t___8425;
            ISqlPart t___8431;
            if (!this._isValid__764) throw new S::Exception();
            int i__920 = 0;
            while (true)
            {
                {
                    {
                        t___15145 = this._tableDef__760.Fields.Count;
                        if (!(i__920 < t___15145)) break;
                        FieldDef f__921 = this._tableDef__760.Fields[i__920];
                        if (f__921.Virtual) goto continue___15556;
                        ISqlPart ? dv__922 = f__921.DefaultValue;
                        if (!f__921.Nullable)
                        {
                            t___15152 = f__921.Name.SqlValue;
                            if (!C::Mapped.ContainsKey(this._changes__762, t___15152))
                            {
                                t___8417 = dv__922 == null;
                            }
                            else
                            {
                                t___8417 = false;
                            }
                            t___8418 = t___8417;
                        }
                        else
                        {
                            t___8418 = false;
                        }
                        if (t___8418) throw new S::Exception();
                    }
                    continue___15556:
                    {
                    }
                }
                i__920 = i__920 + 1;
            }
            G::IList<string> colNames__923 = new G::List<string>();
            G::IList<ISqlPart> valParts__924 = new G::List<ISqlPart>();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__925 = C::Mapped.ToList(this._changes__762);
            int i__926 = 0;
            while (true)
            {
                {
                    {
                        t___15157 = pairs__925.Count;
                        if (!(i__926 < t___15157)) break;
                        G::KeyValuePair<string, string> pair__927 = pairs__925[i__926];
                        t___15159 = pair__927.Key;
                        t___8425 = this._tableDef__760.Field(t___15159);
                        FieldDef fd__928 = t___8425;
                        if (fd__928.Virtual) goto continue___15557;
                        C::Listed.Add(colNames__923, fd__928.Name.SqlValue);
                        t___15164 = pair__927.Value;
                        t___8431 = this.ValueToSqlPart(fd__928, t___15164);
                        C::Listed.Add(valParts__924, t___8431);
                    }
                    continue___15557:
                    {
                    }
                }
                i__926 = i__926 + 1;
            }
            int i__929 = 0;
            while (true)
            {
                {
                    {
                        t___15167 = this._tableDef__760.Fields.Count;
                        if (!(i__929 < t___15167)) break;
                        FieldDef f__930 = this._tableDef__760.Fields[i__929];
                        if (f__930.Virtual) goto continue___15558;
                        ISqlPart ? dv__931 = f__930.DefaultValue;
                        if (!(dv__931 == null))
                        {
                            ISqlPart dv___2622 = dv__931!;
                            t___15173 = f__930.Name.SqlValue;
                            if (!C::Mapped.ContainsKey(this._changes__762, t___15173))
                            {
                                C::Listed.Add(colNames__923, f__930.Name.SqlValue);
                                C::Listed.Add(valParts__924, dv___2622);
                            }
                        }
                    }
                    continue___15558:
                    {
                    }
                }
                i__929 = i__929 + 1;
            }
            if (valParts__924.Count == 0) throw new S::Exception();
            SqlBuilder b__932 = new SqlBuilder();
            b__932.AppendSafe("INSERT INTO ");
            b__932.AppendSafe(this._tableDef__760.TableName.SqlValue);
            b__932.AppendSafe(" (");
            G::IReadOnlyList<string> t___15186 = C::Listed.ToReadOnlyList(colNames__923);
            string fn__15143(string c__933)
            {
                return c__933;
            }
            b__932.AppendSafe(C::Listed.Join(t___15186, ", ", (S::Func<string, string>) fn__15143));
            b__932.AppendSafe(") VALUES (");
            b__932.AppendPart(valParts__924[0]);
            int j__934 = 1;
            while (true)
            {
                t___15193 = valParts__924.Count;
                if (!(j__934 < t___15193)) break;
                b__932.AppendSafe(", ");
                b__932.AppendPart(valParts__924[j__934]);
                j__934 = j__934 + 1;
            }
            b__932.AppendSafe(")");
            return b__932.Accumulated;
        }
        public SqlFragment ToUpdateSql(int id__936)
        {
            int t___15126;
            string t___15128;
            string t___15135;
            FieldDef t___8392;
            ISqlPart t___8399;
            if (!this._isValid__764) throw new S::Exception();
            G::IReadOnlyList<G::KeyValuePair<string, string>> pairs__938 = C::Mapped.ToList(this._changes__762);
            if (pairs__938.Count == 0) throw new S::Exception();
            SqlBuilder b__939 = new SqlBuilder();
            b__939.AppendSafe("UPDATE ");
            b__939.AppendSafe(this._tableDef__760.TableName.SqlValue);
            b__939.AppendSafe(" SET ");
            int setCount__940 = 0;
            int i__941 = 0;
            while (true)
            {
                {
                    {
                        t___15126 = pairs__938.Count;
                        if (!(i__941 < t___15126)) break;
                        G::KeyValuePair<string, string> pair__942 = pairs__938[i__941];
                        t___15128 = pair__942.Key;
                        t___8392 = this._tableDef__760.Field(t___15128);
                        FieldDef fd__943 = t___8392;
                        if (fd__943.Virtual) goto continue___15559;
                        if (setCount__940 > 0) b__939.AppendSafe(", ");
                        b__939.AppendSafe(fd__943.Name.SqlValue);
                        b__939.AppendSafe(" = ");
                        t___15135 = pair__942.Value;
                        t___8399 = this.ValueToSqlPart(fd__943, t___15135);
                        b__939.AppendPart(t___8399);
                        setCount__940 = setCount__940 + 1;
                    }
                    continue___15559:
                    {
                    }
                }
                i__941 = i__941 + 1;
            }
            if (setCount__940 == 0) throw new S::Exception();
            b__939.AppendSafe(" WHERE ");
            b__939.AppendSafe(this._tableDef__760.PkName());
            b__939.AppendSafe(" = ");
            b__939.AppendInt32(id__936);
            return b__939.Accumulated;
        }
        public ChangesetImpl(TableDef _tableDef__945, G::IReadOnlyDictionary<string, string> _params__946, G::IReadOnlyDictionary<string, string> _changes__947, G::IReadOnlyList<ChangesetError> _errors__948, bool _isValid__949)
        {
            this._tableDef__760 = _tableDef__945;
            this._params__761 = _params__946;
            this._changes__762 = _changes__947;
            this._errors__763 = _errors__948;
            this._isValid__764 = _isValid__949;
        }
    }
}
