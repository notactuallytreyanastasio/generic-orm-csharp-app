using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__783;
        readonly G::IReadOnlyList<IWhereClause> conditions__784;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__785;
        readonly G::IReadOnlyList<OrderClause> orderClauses__786;
        readonly int ? limitVal__787;
        readonly int ? offsetVal__788;
        readonly G::IReadOnlyList<JoinClause> joinClauses__789;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__790;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__791;
        readonly bool isDistinct__792;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__793;
        public Query Where(SqlFragment condition__795)
        {
            G::IList<IWhereClause> nb__797 = L::Enumerable.ToList(this.conditions__784);
            C::Listed.Add(nb__797, new AndCondition(condition__795));
            return new Query(this.tableName__783, C::Listed.ToReadOnlyList(nb__797), this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query OrWhere(SqlFragment condition__799)
        {
            G::IList<IWhereClause> nb__801 = L::Enumerable.ToList(this.conditions__784);
            C::Listed.Add(nb__801, new OrCondition(condition__799));
            return new Query(this.tableName__783, C::Listed.ToReadOnlyList(nb__801), this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query WhereNull(ISafeIdentifier field__803)
        {
            SqlBuilder b__805 = new SqlBuilder();
            b__805.AppendSafe(field__803.SqlValue);
            b__805.AppendSafe(" IS NULL");
            SqlFragment t___10304 = b__805.Accumulated;
            return this.Where(t___10304);
        }
        public Query WhereNotNull(ISafeIdentifier field__807)
        {
            SqlBuilder b__809 = new SqlBuilder();
            b__809.AppendSafe(field__807.SqlValue);
            b__809.AppendSafe(" IS NOT NULL");
            SqlFragment t___10298 = b__809.Accumulated;
            return this.Where(t___10298);
        }
        public Query WhereIn(ISafeIdentifier field__811, G::IReadOnlyList<ISqlPart> values__812)
        {
            Query return__365;
            SqlFragment t___10279;
            int t___10287;
            SqlFragment t___10292;
            {
                {
                    if (values__812.Count == 0)
                    {
                        SqlBuilder b__814 = new SqlBuilder();
                        b__814.AppendSafe("1 = 0");
                        t___10279 = b__814.Accumulated;
                        return__365 = this.Where(t___10279);
                        goto fn__813;
                    }
                    SqlBuilder b__815 = new SqlBuilder();
                    b__815.AppendSafe(field__811.SqlValue);
                    b__815.AppendSafe(" IN (");
                    b__815.AppendPart(values__812[0]);
                    int i__816 = 1;
                    while (true)
                    {
                        t___10287 = values__812.Count;
                        if (!(i__816 < t___10287)) break;
                        b__815.AppendSafe(", ");
                        b__815.AppendPart(values__812[i__816]);
                        i__816 = i__816 + 1;
                    }
                    b__815.AppendSafe(")");
                    t___10292 = b__815.Accumulated;
                    return__365 = this.Where(t___10292);
                }
                fn__813:
                {
                }
            }
            return return__365;
        }
        public Query WhereInSubquery(ISafeIdentifier field__818, Query sub__819)
        {
            SqlBuilder b__821 = new SqlBuilder();
            b__821.AppendSafe(field__818.SqlValue);
            b__821.AppendSafe(" IN (");
            b__821.AppendFragment(sub__819.ToSql());
            b__821.AppendSafe(")");
            SqlFragment t___10274 = b__821.Accumulated;
            return this.Where(t___10274);
        }
        public Query WhereNot(SqlFragment condition__823)
        {
            SqlBuilder b__825 = new SqlBuilder();
            b__825.AppendSafe("NOT (");
            b__825.AppendFragment(condition__823);
            b__825.AppendSafe(")");
            SqlFragment t___10265 = b__825.Accumulated;
            return this.Where(t___10265);
        }
        public Query WhereBetween(ISafeIdentifier field__827, ISqlPart low__828, ISqlPart high__829)
        {
            SqlBuilder b__831 = new SqlBuilder();
            b__831.AppendSafe(field__827.SqlValue);
            b__831.AppendSafe(" BETWEEN ");
            b__831.AppendPart(low__828);
            b__831.AppendSafe(" AND ");
            b__831.AppendPart(high__829);
            SqlFragment t___10259 = b__831.Accumulated;
            return this.Where(t___10259);
        }
        public Query WhereLike(ISafeIdentifier field__833, string pattern__834)
        {
            SqlBuilder b__836 = new SqlBuilder();
            b__836.AppendSafe(field__833.SqlValue);
            b__836.AppendSafe(" LIKE ");
            b__836.AppendString(pattern__834);
            SqlFragment t___10250 = b__836.Accumulated;
            return this.Where(t___10250);
        }
        public Query WhereILike(ISafeIdentifier field__838, string pattern__839)
        {
            SqlBuilder b__841 = new SqlBuilder();
            b__841.AppendSafe(field__838.SqlValue);
            b__841.AppendSafe(" ILIKE ");
            b__841.AppendString(pattern__839);
            SqlFragment t___10243 = b__841.Accumulated;
            return this.Where(t___10243);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__843)
        {
            return new Query(this.tableName__783, this.conditions__784, fields__843, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__846)
        {
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, exprs__846);
        }
        public Query OrderBy(ISafeIdentifier field__849, bool ascending__850)
        {
            G::IList<OrderClause> nb__852 = L::Enumerable.ToList(this.orderClauses__786);
            C::Listed.Add(nb__852, new OrderClause(field__849, ascending__850));
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, C::Listed.ToReadOnlyList(nb__852), this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query Limit(int n__854)
        {
            if (n__854 < 0) throw new S::Exception();
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, n__854, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query Offset(int n__857)
        {
            if (n__857 < 0) throw new S::Exception();
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, n__857, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query Join(IJoinType joinType__860, ISafeIdentifier table__861, SqlFragment onCondition__862)
        {
            G::IList<JoinClause> nb__864 = L::Enumerable.ToList(this.joinClauses__789);
            C::Listed.Add(nb__864, new JoinClause(joinType__860, table__861, onCondition__862));
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, C::Listed.ToReadOnlyList(nb__864), this.groupByFields__790, this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query InnerJoin(ISafeIdentifier table__866, SqlFragment onCondition__867)
        {
            InnerJoin t___10213 = new InnerJoin();
            return this.Join(t___10213, table__866, onCondition__867);
        }
        public Query LeftJoin(ISafeIdentifier table__870, SqlFragment onCondition__871)
        {
            LeftJoin t___10211 = new LeftJoin();
            return this.Join(t___10211, table__870, onCondition__871);
        }
        public Query RightJoin(ISafeIdentifier table__874, SqlFragment onCondition__875)
        {
            RightJoin t___10209 = new RightJoin();
            return this.Join(t___10209, table__874, onCondition__875);
        }
        public Query FullJoin(ISafeIdentifier table__878, SqlFragment onCondition__879)
        {
            FullJoin t___10207 = new FullJoin();
            return this.Join(t___10207, table__878, onCondition__879);
        }
        public Query GroupBy(ISafeIdentifier field__882)
        {
            G::IList<ISafeIdentifier> nb__884 = L::Enumerable.ToList(this.groupByFields__790);
            C::Listed.Add(nb__884, field__882);
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, C::Listed.ToReadOnlyList(nb__884), this.havingConditions__791, this.isDistinct__792, this.selectExprs__793);
        }
        public Query Having(SqlFragment condition__886)
        {
            G::IList<IWhereClause> nb__888 = L::Enumerable.ToList(this.havingConditions__791);
            C::Listed.Add(nb__888, new AndCondition(condition__886));
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, C::Listed.ToReadOnlyList(nb__888), this.isDistinct__792, this.selectExprs__793);
        }
        public Query OrHaving(SqlFragment condition__890)
        {
            G::IList<IWhereClause> nb__892 = L::Enumerable.ToList(this.havingConditions__791);
            C::Listed.Add(nb__892, new OrCondition(condition__890));
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, C::Listed.ToReadOnlyList(nb__892), this.isDistinct__792, this.selectExprs__793);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__783, this.conditions__784, this.selectedFields__785, this.orderClauses__786, this.limitVal__787, this.offsetVal__788, this.joinClauses__789, this.groupByFields__790, this.havingConditions__791, true, this.selectExprs__793);
        }
        public SqlFragment ToSql()
        {
            int t___10113;
            int t___10132;
            int t___10151;
            SqlBuilder b__897 = new SqlBuilder();
            if (this.isDistinct__792) b__897.AppendSafe("SELECT DISTINCT ");
            else b__897.AppendSafe("SELECT ");
            if (!(this.selectExprs__793.Count == 0))
            {
                b__897.AppendFragment(this.selectExprs__793[0]);
                int i__898 = 1;
                while (true)
                {
                    t___10113 = this.selectExprs__793.Count;
                    if (!(i__898 < t___10113)) break;
                    b__897.AppendSafe(", ");
                    b__897.AppendFragment(this.selectExprs__793[i__898]);
                    i__898 = i__898 + 1;
                }
            }
            else if (this.selectedFields__785.Count == 0) b__897.AppendSafe("*");
            else
            {
                string fn__10106(ISafeIdentifier f__899)
                {
                    return f__899.SqlValue;
                }
                b__897.AppendSafe(C::Listed.Join(this.selectedFields__785, ", ", (S::Func<ISafeIdentifier, string>) fn__10106));
            }
            b__897.AppendSafe(" FROM ");
            b__897.AppendSafe(this.tableName__783.SqlValue);
            void fn__10105(JoinClause jc__900)
            {
                b__897.AppendSafe(" ");
                string t___10093 = jc__900.JoinType.Keyword();
                b__897.AppendSafe(t___10093);
                b__897.AppendSafe(" ");
                string t___10097 = jc__900.Table.SqlValue;
                b__897.AppendSafe(t___10097);
                b__897.AppendSafe(" ON ");
                SqlFragment t___10100 = jc__900.OnCondition;
                b__897.AppendFragment(t___10100);
            }
            C::Listed.ForEach(this.joinClauses__789, (S::Action<JoinClause>) fn__10105);
            if (!(this.conditions__784.Count == 0))
            {
                b__897.AppendSafe(" WHERE ");
                b__897.AppendFragment(this.conditions__784[0].Condition);
                int i__901 = 1;
                while (true)
                {
                    t___10132 = this.conditions__784.Count;
                    if (!(i__901 < t___10132)) break;
                    b__897.AppendSafe(" ");
                    b__897.AppendSafe(this.conditions__784[i__901].Keyword());
                    b__897.AppendSafe(" ");
                    b__897.AppendFragment(this.conditions__784[i__901].Condition);
                    i__901 = i__901 + 1;
                }
            }
            if (!(this.groupByFields__790.Count == 0))
            {
                b__897.AppendSafe(" GROUP BY ");
                string fn__10104(ISafeIdentifier f__902)
                {
                    return f__902.SqlValue;
                }
                b__897.AppendSafe(C::Listed.Join(this.groupByFields__790, ", ", (S::Func<ISafeIdentifier, string>) fn__10104));
            }
            if (!(this.havingConditions__791.Count == 0))
            {
                b__897.AppendSafe(" HAVING ");
                b__897.AppendFragment(this.havingConditions__791[0].Condition);
                int i__903 = 1;
                while (true)
                {
                    t___10151 = this.havingConditions__791.Count;
                    if (!(i__903 < t___10151)) break;
                    b__897.AppendSafe(" ");
                    b__897.AppendSafe(this.havingConditions__791[i__903].Keyword());
                    b__897.AppendSafe(" ");
                    b__897.AppendFragment(this.havingConditions__791[i__903].Condition);
                    i__903 = i__903 + 1;
                }
            }
            if (!(this.orderClauses__786.Count == 0))
            {
                b__897.AppendSafe(" ORDER BY ");
                bool first__904 = true;
                void fn__10103(OrderClause oc__905)
                {
                    string t___5515;
                    if (!first__904) b__897.AppendSafe(", ");
                    first__904 = false;
                    string t___10086 = oc__905.Field.SqlValue;
                    b__897.AppendSafe(t___10086);
                    if (oc__905.Ascending)
                    {
                        t___5515 = " ASC";
                    }
                    else
                    {
                        t___5515 = " DESC";
                    }
                    b__897.AppendSafe(t___5515);
                }
                C::Listed.ForEach(this.orderClauses__786, (S::Action<OrderClause>) fn__10103);
            }
            int ? lv__906 = this.limitVal__787;
            if (!(lv__906 == null))
            {
                int lv___1952 = lv__906.Value;
                b__897.AppendSafe(" LIMIT ");
                b__897.AppendInt32(lv___1952);
            }
            int ? ov__907 = this.offsetVal__788;
            if (!(ov__907 == null))
            {
                int ov___1953 = ov__907.Value;
                b__897.AppendSafe(" OFFSET ");
                b__897.AppendInt32(ov___1953);
            }
            return b__897.Accumulated;
        }
        public SqlFragment CountSql()
        {
            int t___10055;
            int t___10074;
            SqlBuilder b__910 = new SqlBuilder();
            b__910.AppendSafe("SELECT COUNT(*) FROM ");
            b__910.AppendSafe(this.tableName__783.SqlValue);
            void fn__10043(JoinClause jc__911)
            {
                b__910.AppendSafe(" ");
                string t___10033 = jc__911.JoinType.Keyword();
                b__910.AppendSafe(t___10033);
                b__910.AppendSafe(" ");
                string t___10037 = jc__911.Table.SqlValue;
                b__910.AppendSafe(t___10037);
                b__910.AppendSafe(" ON ");
                SqlFragment t___10040 = jc__911.OnCondition;
                b__910.AppendFragment(t___10040);
            }
            C::Listed.ForEach(this.joinClauses__789, (S::Action<JoinClause>) fn__10043);
            if (!(this.conditions__784.Count == 0))
            {
                b__910.AppendSafe(" WHERE ");
                b__910.AppendFragment(this.conditions__784[0].Condition);
                int i__912 = 1;
                while (true)
                {
                    t___10055 = this.conditions__784.Count;
                    if (!(i__912 < t___10055)) break;
                    b__910.AppendSafe(" ");
                    b__910.AppendSafe(this.conditions__784[i__912].Keyword());
                    b__910.AppendSafe(" ");
                    b__910.AppendFragment(this.conditions__784[i__912].Condition);
                    i__912 = i__912 + 1;
                }
            }
            if (!(this.groupByFields__790.Count == 0))
            {
                b__910.AppendSafe(" GROUP BY ");
                string fn__10042(ISafeIdentifier f__913)
                {
                    return f__913.SqlValue;
                }
                b__910.AppendSafe(C::Listed.Join(this.groupByFields__790, ", ", (S::Func<ISafeIdentifier, string>) fn__10042));
            }
            if (!(this.havingConditions__791.Count == 0))
            {
                b__910.AppendSafe(" HAVING ");
                b__910.AppendFragment(this.havingConditions__791[0].Condition);
                int i__914 = 1;
                while (true)
                {
                    t___10074 = this.havingConditions__791.Count;
                    if (!(i__914 < t___10074)) break;
                    b__910.AppendSafe(" ");
                    b__910.AppendSafe(this.havingConditions__791[i__914].Keyword());
                    b__910.AppendSafe(" ");
                    b__910.AppendFragment(this.havingConditions__791[i__914].Condition);
                    i__914 = i__914 + 1;
                }
            }
            return b__910.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__916)
        {
            SqlFragment return__387;
            Query t___5464;
            if (defaultLimit__916 < 0) throw new S::Exception();
            if (!(this.limitVal__787 == null))
            {
                return__387 = this.ToSql();
            }
            else
            {
                t___5464 = this.Limit(defaultLimit__916);
                return__387 = t___5464.ToSql();
            }
            return return__387;
        }
        public Query(ISafeIdentifier tableName__919, G::IReadOnlyList<IWhereClause> conditions__920, G::IReadOnlyList<ISafeIdentifier> selectedFields__921, G::IReadOnlyList<OrderClause> orderClauses__922, int ? limitVal__923, int ? offsetVal__924, G::IReadOnlyList<JoinClause> joinClauses__925, G::IReadOnlyList<ISafeIdentifier> groupByFields__926, G::IReadOnlyList<IWhereClause> havingConditions__927, bool isDistinct__928, G::IReadOnlyList<SqlFragment> selectExprs__929)
        {
            this.tableName__783 = tableName__919;
            this.conditions__784 = conditions__920;
            this.selectedFields__785 = selectedFields__921;
            this.orderClauses__786 = orderClauses__922;
            this.limitVal__787 = limitVal__923;
            this.offsetVal__788 = offsetVal__924;
            this.joinClauses__789 = joinClauses__925;
            this.groupByFields__790 = groupByFields__926;
            this.havingConditions__791 = havingConditions__927;
            this.isDistinct__792 = isDistinct__928;
            this.selectExprs__793 = selectExprs__929;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__783;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__784;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__785;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__786;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__787;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__788;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__789;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__790;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__791;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__792;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__793;
            }
        }
    }
}
