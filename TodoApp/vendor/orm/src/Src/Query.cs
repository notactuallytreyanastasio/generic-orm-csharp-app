using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__721;
        readonly G::IReadOnlyList<IWhereClause> conditions__722;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__723;
        readonly G::IReadOnlyList<OrderClause> orderClauses__724;
        readonly int ? limitVal__725;
        readonly int ? offsetVal__726;
        readonly G::IReadOnlyList<JoinClause> joinClauses__727;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__728;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__729;
        readonly bool isDistinct__730;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__731;
        public Query Where(SqlFragment condition__733)
        {
            G::IList<IWhereClause> nb__735 = L::Enumerable.ToList(this.conditions__722);
            C::Listed.Add(nb__735, new AndCondition(condition__733));
            return new Query(this.tableName__721, C::Listed.ToReadOnlyList(nb__735), this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query OrWhere(SqlFragment condition__737)
        {
            G::IList<IWhereClause> nb__739 = L::Enumerable.ToList(this.conditions__722);
            C::Listed.Add(nb__739, new OrCondition(condition__737));
            return new Query(this.tableName__721, C::Listed.ToReadOnlyList(nb__739), this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query WhereNull(ISafeIdentifier field__741)
        {
            SqlBuilder b__743 = new SqlBuilder();
            b__743.AppendSafe(field__741.SqlValue);
            b__743.AppendSafe(" IS NULL");
            SqlFragment t___8176 = b__743.Accumulated;
            return this.Where(t___8176);
        }
        public Query WhereNotNull(ISafeIdentifier field__745)
        {
            SqlBuilder b__747 = new SqlBuilder();
            b__747.AppendSafe(field__745.SqlValue);
            b__747.AppendSafe(" IS NOT NULL");
            SqlFragment t___8170 = b__747.Accumulated;
            return this.Where(t___8170);
        }
        public Query WhereIn(ISafeIdentifier field__749, G::IReadOnlyList<ISqlPart> values__750)
        {
            Query return__332;
            SqlFragment t___8151;
            int t___8159;
            SqlFragment t___8164;
            {
                {
                    if (values__750.Count == 0)
                    {
                        SqlBuilder b__752 = new SqlBuilder();
                        b__752.AppendSafe("1 = 0");
                        t___8151 = b__752.Accumulated;
                        return__332 = this.Where(t___8151);
                        goto fn__751;
                    }
                    SqlBuilder b__753 = new SqlBuilder();
                    b__753.AppendSafe(field__749.SqlValue);
                    b__753.AppendSafe(" IN (");
                    b__753.AppendPart(values__750[0]);
                    int i__754 = 1;
                    while (true)
                    {
                        t___8159 = values__750.Count;
                        if (!(i__754 < t___8159)) break;
                        b__753.AppendSafe(", ");
                        b__753.AppendPart(values__750[i__754]);
                        i__754 = i__754 + 1;
                    }
                    b__753.AppendSafe(")");
                    t___8164 = b__753.Accumulated;
                    return__332 = this.Where(t___8164);
                }
                fn__751:
                {
                }
            }
            return return__332;
        }
        public Query WhereNot(SqlFragment condition__756)
        {
            SqlBuilder b__758 = new SqlBuilder();
            b__758.AppendSafe("NOT (");
            b__758.AppendFragment(condition__756);
            b__758.AppendSafe(")");
            SqlFragment t___8146 = b__758.Accumulated;
            return this.Where(t___8146);
        }
        public Query WhereBetween(ISafeIdentifier field__760, ISqlPart low__761, ISqlPart high__762)
        {
            SqlBuilder b__764 = new SqlBuilder();
            b__764.AppendSafe(field__760.SqlValue);
            b__764.AppendSafe(" BETWEEN ");
            b__764.AppendPart(low__761);
            b__764.AppendSafe(" AND ");
            b__764.AppendPart(high__762);
            SqlFragment t___8140 = b__764.Accumulated;
            return this.Where(t___8140);
        }
        public Query WhereLike(ISafeIdentifier field__766, string pattern__767)
        {
            SqlBuilder b__769 = new SqlBuilder();
            b__769.AppendSafe(field__766.SqlValue);
            b__769.AppendSafe(" LIKE ");
            b__769.AppendString(pattern__767);
            SqlFragment t___8131 = b__769.Accumulated;
            return this.Where(t___8131);
        }
        public Query WhereILike(ISafeIdentifier field__771, string pattern__772)
        {
            SqlBuilder b__774 = new SqlBuilder();
            b__774.AppendSafe(field__771.SqlValue);
            b__774.AppendSafe(" ILIKE ");
            b__774.AppendString(pattern__772);
            SqlFragment t___8124 = b__774.Accumulated;
            return this.Where(t___8124);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__776)
        {
            return new Query(this.tableName__721, this.conditions__722, fields__776, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__779)
        {
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, exprs__779);
        }
        public Query OrderBy(ISafeIdentifier field__782, bool ascending__783)
        {
            G::IList<OrderClause> nb__785 = L::Enumerable.ToList(this.orderClauses__724);
            C::Listed.Add(nb__785, new OrderClause(field__782, ascending__783));
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, C::Listed.ToReadOnlyList(nb__785), this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query Limit(int n__787)
        {
            if (n__787 < 0) throw new S::Exception();
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, n__787, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query Offset(int n__790)
        {
            if (n__790 < 0) throw new S::Exception();
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, n__790, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query Join(IJoinType joinType__793, ISafeIdentifier table__794, SqlFragment onCondition__795)
        {
            G::IList<JoinClause> nb__797 = L::Enumerable.ToList(this.joinClauses__727);
            C::Listed.Add(nb__797, new JoinClause(joinType__793, table__794, onCondition__795));
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, C::Listed.ToReadOnlyList(nb__797), this.groupByFields__728, this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query InnerJoin(ISafeIdentifier table__799, SqlFragment onCondition__800)
        {
            InnerJoin t___8094 = new InnerJoin();
            return this.Join(t___8094, table__799, onCondition__800);
        }
        public Query LeftJoin(ISafeIdentifier table__803, SqlFragment onCondition__804)
        {
            LeftJoin t___8092 = new LeftJoin();
            return this.Join(t___8092, table__803, onCondition__804);
        }
        public Query RightJoin(ISafeIdentifier table__807, SqlFragment onCondition__808)
        {
            RightJoin t___8090 = new RightJoin();
            return this.Join(t___8090, table__807, onCondition__808);
        }
        public Query FullJoin(ISafeIdentifier table__811, SqlFragment onCondition__812)
        {
            FullJoin t___8088 = new FullJoin();
            return this.Join(t___8088, table__811, onCondition__812);
        }
        public Query GroupBy(ISafeIdentifier field__815)
        {
            G::IList<ISafeIdentifier> nb__817 = L::Enumerable.ToList(this.groupByFields__728);
            C::Listed.Add(nb__817, field__815);
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, C::Listed.ToReadOnlyList(nb__817), this.havingConditions__729, this.isDistinct__730, this.selectExprs__731);
        }
        public Query Having(SqlFragment condition__819)
        {
            G::IList<IWhereClause> nb__821 = L::Enumerable.ToList(this.havingConditions__729);
            C::Listed.Add(nb__821, new AndCondition(condition__819));
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, C::Listed.ToReadOnlyList(nb__821), this.isDistinct__730, this.selectExprs__731);
        }
        public Query OrHaving(SqlFragment condition__823)
        {
            G::IList<IWhereClause> nb__825 = L::Enumerable.ToList(this.havingConditions__729);
            C::Listed.Add(nb__825, new OrCondition(condition__823));
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, C::Listed.ToReadOnlyList(nb__825), this.isDistinct__730, this.selectExprs__731);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__721, this.conditions__722, this.selectedFields__723, this.orderClauses__724, this.limitVal__725, this.offsetVal__726, this.joinClauses__727, this.groupByFields__728, this.havingConditions__729, true, this.selectExprs__731);
        }
        public SqlFragment ToSql()
        {
            int t___7994;
            int t___8013;
            int t___8032;
            SqlBuilder b__830 = new SqlBuilder();
            if (this.isDistinct__730) b__830.AppendSafe("SELECT DISTINCT ");
            else b__830.AppendSafe("SELECT ");
            if (!(this.selectExprs__731.Count == 0))
            {
                b__830.AppendFragment(this.selectExprs__731[0]);
                int i__831 = 1;
                while (true)
                {
                    t___7994 = this.selectExprs__731.Count;
                    if (!(i__831 < t___7994)) break;
                    b__830.AppendSafe(", ");
                    b__830.AppendFragment(this.selectExprs__731[i__831]);
                    i__831 = i__831 + 1;
                }
            }
            else if (this.selectedFields__723.Count == 0) b__830.AppendSafe("*");
            else
            {
                string fn__7987(ISafeIdentifier f__832)
                {
                    return f__832.SqlValue;
                }
                b__830.AppendSafe(C::Listed.Join(this.selectedFields__723, ", ", (S::Func<ISafeIdentifier, string>) fn__7987));
            }
            b__830.AppendSafe(" FROM ");
            b__830.AppendSafe(this.tableName__721.SqlValue);
            void fn__7986(JoinClause jc__833)
            {
                b__830.AppendSafe(" ");
                string t___7974 = jc__833.JoinType.Keyword();
                b__830.AppendSafe(t___7974);
                b__830.AppendSafe(" ");
                string t___7978 = jc__833.Table.SqlValue;
                b__830.AppendSafe(t___7978);
                b__830.AppendSafe(" ON ");
                SqlFragment t___7981 = jc__833.OnCondition;
                b__830.AppendFragment(t___7981);
            }
            C::Listed.ForEach(this.joinClauses__727, (S::Action<JoinClause>) fn__7986);
            if (!(this.conditions__722.Count == 0))
            {
                b__830.AppendSafe(" WHERE ");
                b__830.AppendFragment(this.conditions__722[0].Condition);
                int i__834 = 1;
                while (true)
                {
                    t___8013 = this.conditions__722.Count;
                    if (!(i__834 < t___8013)) break;
                    b__830.AppendSafe(" ");
                    b__830.AppendSafe(this.conditions__722[i__834].Keyword());
                    b__830.AppendSafe(" ");
                    b__830.AppendFragment(this.conditions__722[i__834].Condition);
                    i__834 = i__834 + 1;
                }
            }
            if (!(this.groupByFields__728.Count == 0))
            {
                b__830.AppendSafe(" GROUP BY ");
                string fn__7985(ISafeIdentifier f__835)
                {
                    return f__835.SqlValue;
                }
                b__830.AppendSafe(C::Listed.Join(this.groupByFields__728, ", ", (S::Func<ISafeIdentifier, string>) fn__7985));
            }
            if (!(this.havingConditions__729.Count == 0))
            {
                b__830.AppendSafe(" HAVING ");
                b__830.AppendFragment(this.havingConditions__729[0].Condition);
                int i__836 = 1;
                while (true)
                {
                    t___8032 = this.havingConditions__729.Count;
                    if (!(i__836 < t___8032)) break;
                    b__830.AppendSafe(" ");
                    b__830.AppendSafe(this.havingConditions__729[i__836].Keyword());
                    b__830.AppendSafe(" ");
                    b__830.AppendFragment(this.havingConditions__729[i__836].Condition);
                    i__836 = i__836 + 1;
                }
            }
            if (!(this.orderClauses__724.Count == 0))
            {
                b__830.AppendSafe(" ORDER BY ");
                bool first__837 = true;
                void fn__7984(OrderClause oc__838)
                {
                    string t___4304;
                    if (!first__837) b__830.AppendSafe(", ");
                    first__837 = false;
                    string t___7967 = oc__838.Field.SqlValue;
                    b__830.AppendSafe(t___7967);
                    if (oc__838.Ascending)
                    {
                        t___4304 = " ASC";
                    }
                    else
                    {
                        t___4304 = " DESC";
                    }
                    b__830.AppendSafe(t___4304);
                }
                C::Listed.ForEach(this.orderClauses__724, (S::Action<OrderClause>) fn__7984);
            }
            int ? lv__839 = this.limitVal__725;
            if (!(lv__839 == null))
            {
                int lv___1638 = lv__839.Value;
                b__830.AppendSafe(" LIMIT ");
                b__830.AppendInt32(lv___1638);
            }
            int ? ov__840 = this.offsetVal__726;
            if (!(ov__840 == null))
            {
                int ov___1639 = ov__840.Value;
                b__830.AppendSafe(" OFFSET ");
                b__830.AppendInt32(ov___1639);
            }
            return b__830.Accumulated;
        }
        public SqlFragment CountSql()
        {
            int t___7936;
            int t___7955;
            SqlBuilder b__843 = new SqlBuilder();
            b__843.AppendSafe("SELECT COUNT(*) FROM ");
            b__843.AppendSafe(this.tableName__721.SqlValue);
            void fn__7924(JoinClause jc__844)
            {
                b__843.AppendSafe(" ");
                string t___7914 = jc__844.JoinType.Keyword();
                b__843.AppendSafe(t___7914);
                b__843.AppendSafe(" ");
                string t___7918 = jc__844.Table.SqlValue;
                b__843.AppendSafe(t___7918);
                b__843.AppendSafe(" ON ");
                SqlFragment t___7921 = jc__844.OnCondition;
                b__843.AppendFragment(t___7921);
            }
            C::Listed.ForEach(this.joinClauses__727, (S::Action<JoinClause>) fn__7924);
            if (!(this.conditions__722.Count == 0))
            {
                b__843.AppendSafe(" WHERE ");
                b__843.AppendFragment(this.conditions__722[0].Condition);
                int i__845 = 1;
                while (true)
                {
                    t___7936 = this.conditions__722.Count;
                    if (!(i__845 < t___7936)) break;
                    b__843.AppendSafe(" ");
                    b__843.AppendSafe(this.conditions__722[i__845].Keyword());
                    b__843.AppendSafe(" ");
                    b__843.AppendFragment(this.conditions__722[i__845].Condition);
                    i__845 = i__845 + 1;
                }
            }
            if (!(this.groupByFields__728.Count == 0))
            {
                b__843.AppendSafe(" GROUP BY ");
                string fn__7923(ISafeIdentifier f__846)
                {
                    return f__846.SqlValue;
                }
                b__843.AppendSafe(C::Listed.Join(this.groupByFields__728, ", ", (S::Func<ISafeIdentifier, string>) fn__7923));
            }
            if (!(this.havingConditions__729.Count == 0))
            {
                b__843.AppendSafe(" HAVING ");
                b__843.AppendFragment(this.havingConditions__729[0].Condition);
                int i__847 = 1;
                while (true)
                {
                    t___7955 = this.havingConditions__729.Count;
                    if (!(i__847 < t___7955)) break;
                    b__843.AppendSafe(" ");
                    b__843.AppendSafe(this.havingConditions__729[i__847].Keyword());
                    b__843.AppendSafe(" ");
                    b__843.AppendFragment(this.havingConditions__729[i__847].Condition);
                    i__847 = i__847 + 1;
                }
            }
            return b__843.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__849)
        {
            SqlFragment return__353;
            Query t___4253;
            if (defaultLimit__849 < 0) throw new S::Exception();
            if (!(this.limitVal__725 == null))
            {
                return__353 = this.ToSql();
            }
            else
            {
                t___4253 = this.Limit(defaultLimit__849);
                return__353 = t___4253.ToSql();
            }
            return return__353;
        }
        public Query(ISafeIdentifier tableName__852, G::IReadOnlyList<IWhereClause> conditions__853, G::IReadOnlyList<ISafeIdentifier> selectedFields__854, G::IReadOnlyList<OrderClause> orderClauses__855, int ? limitVal__856, int ? offsetVal__857, G::IReadOnlyList<JoinClause> joinClauses__858, G::IReadOnlyList<ISafeIdentifier> groupByFields__859, G::IReadOnlyList<IWhereClause> havingConditions__860, bool isDistinct__861, G::IReadOnlyList<SqlFragment> selectExprs__862)
        {
            this.tableName__721 = tableName__852;
            this.conditions__722 = conditions__853;
            this.selectedFields__723 = selectedFields__854;
            this.orderClauses__724 = orderClauses__855;
            this.limitVal__725 = limitVal__856;
            this.offsetVal__726 = offsetVal__857;
            this.joinClauses__727 = joinClauses__858;
            this.groupByFields__728 = groupByFields__859;
            this.havingConditions__729 = havingConditions__860;
            this.isDistinct__730 = isDistinct__861;
            this.selectExprs__731 = selectExprs__862;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__721;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__722;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__723;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__724;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__725;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__726;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__727;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__728;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__729;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__730;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__731;
            }
        }
    }
}
