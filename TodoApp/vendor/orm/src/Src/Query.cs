using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__739;
        readonly G::IReadOnlyList<IWhereClause> conditions__740;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__741;
        readonly G::IReadOnlyList<OrderClause> orderClauses__742;
        readonly int ? limitVal__743;
        readonly int ? offsetVal__744;
        readonly G::IReadOnlyList<JoinClause> joinClauses__745;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__746;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__747;
        readonly bool isDistinct__748;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__749;
        public Query Where(SqlFragment condition__751)
        {
            G::IList<IWhereClause> nb__753 = L::Enumerable.ToList(this.conditions__740);
            C::Listed.Add(nb__753, new AndCondition(condition__751));
            return new Query(this.tableName__739, C::Listed.ToReadOnlyList(nb__753), this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query OrWhere(SqlFragment condition__755)
        {
            G::IList<IWhereClause> nb__757 = L::Enumerable.ToList(this.conditions__740);
            C::Listed.Add(nb__757, new OrCondition(condition__755));
            return new Query(this.tableName__739, C::Listed.ToReadOnlyList(nb__757), this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query WhereNull(ISafeIdentifier field__759)
        {
            SqlBuilder b__761 = new SqlBuilder();
            b__761.AppendSafe(field__759.SqlValue);
            b__761.AppendSafe(" IS NULL");
            SqlFragment t___9050 = b__761.Accumulated;
            return this.Where(t___9050);
        }
        public Query WhereNotNull(ISafeIdentifier field__763)
        {
            SqlBuilder b__765 = new SqlBuilder();
            b__765.AppendSafe(field__763.SqlValue);
            b__765.AppendSafe(" IS NOT NULL");
            SqlFragment t___9044 = b__765.Accumulated;
            return this.Where(t___9044);
        }
        public Query WhereIn(ISafeIdentifier field__767, G::IReadOnlyList<ISqlPart> values__768)
        {
            Query return__343;
            SqlFragment t___9025;
            int t___9033;
            SqlFragment t___9038;
            {
                {
                    if (values__768.Count == 0)
                    {
                        SqlBuilder b__770 = new SqlBuilder();
                        b__770.AppendSafe("1 = 0");
                        t___9025 = b__770.Accumulated;
                        return__343 = this.Where(t___9025);
                        goto fn__769;
                    }
                    SqlBuilder b__771 = new SqlBuilder();
                    b__771.AppendSafe(field__767.SqlValue);
                    b__771.AppendSafe(" IN (");
                    b__771.AppendPart(values__768[0]);
                    int i__772 = 1;
                    while (true)
                    {
                        t___9033 = values__768.Count;
                        if (!(i__772 < t___9033)) break;
                        b__771.AppendSafe(", ");
                        b__771.AppendPart(values__768[i__772]);
                        i__772 = i__772 + 1;
                    }
                    b__771.AppendSafe(")");
                    t___9038 = b__771.Accumulated;
                    return__343 = this.Where(t___9038);
                }
                fn__769:
                {
                }
            }
            return return__343;
        }
        public Query WhereInSubquery(ISafeIdentifier field__774, Query sub__775)
        {
            SqlBuilder b__777 = new SqlBuilder();
            b__777.AppendSafe(field__774.SqlValue);
            b__777.AppendSafe(" IN (");
            b__777.AppendFragment(sub__775.ToSql());
            b__777.AppendSafe(")");
            SqlFragment t___9020 = b__777.Accumulated;
            return this.Where(t___9020);
        }
        public Query WhereNot(SqlFragment condition__779)
        {
            SqlBuilder b__781 = new SqlBuilder();
            b__781.AppendSafe("NOT (");
            b__781.AppendFragment(condition__779);
            b__781.AppendSafe(")");
            SqlFragment t___9011 = b__781.Accumulated;
            return this.Where(t___9011);
        }
        public Query WhereBetween(ISafeIdentifier field__783, ISqlPart low__784, ISqlPart high__785)
        {
            SqlBuilder b__787 = new SqlBuilder();
            b__787.AppendSafe(field__783.SqlValue);
            b__787.AppendSafe(" BETWEEN ");
            b__787.AppendPart(low__784);
            b__787.AppendSafe(" AND ");
            b__787.AppendPart(high__785);
            SqlFragment t___9005 = b__787.Accumulated;
            return this.Where(t___9005);
        }
        public Query WhereLike(ISafeIdentifier field__789, string pattern__790)
        {
            SqlBuilder b__792 = new SqlBuilder();
            b__792.AppendSafe(field__789.SqlValue);
            b__792.AppendSafe(" LIKE ");
            b__792.AppendString(pattern__790);
            SqlFragment t___8996 = b__792.Accumulated;
            return this.Where(t___8996);
        }
        public Query WhereILike(ISafeIdentifier field__794, string pattern__795)
        {
            SqlBuilder b__797 = new SqlBuilder();
            b__797.AppendSafe(field__794.SqlValue);
            b__797.AppendSafe(" ILIKE ");
            b__797.AppendString(pattern__795);
            SqlFragment t___8989 = b__797.Accumulated;
            return this.Where(t___8989);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__799)
        {
            return new Query(this.tableName__739, this.conditions__740, fields__799, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__802)
        {
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, exprs__802);
        }
        public Query OrderBy(ISafeIdentifier field__805, bool ascending__806)
        {
            G::IList<OrderClause> nb__808 = L::Enumerable.ToList(this.orderClauses__742);
            C::Listed.Add(nb__808, new OrderClause(field__805, ascending__806));
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, C::Listed.ToReadOnlyList(nb__808), this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query Limit(int n__810)
        {
            if (n__810 < 0) throw new S::Exception();
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, n__810, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query Offset(int n__813)
        {
            if (n__813 < 0) throw new S::Exception();
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, n__813, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query Join(IJoinType joinType__816, ISafeIdentifier table__817, SqlFragment onCondition__818)
        {
            G::IList<JoinClause> nb__820 = L::Enumerable.ToList(this.joinClauses__745);
            C::Listed.Add(nb__820, new JoinClause(joinType__816, table__817, onCondition__818));
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, C::Listed.ToReadOnlyList(nb__820), this.groupByFields__746, this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query InnerJoin(ISafeIdentifier table__822, SqlFragment onCondition__823)
        {
            InnerJoin t___8959 = new InnerJoin();
            return this.Join(t___8959, table__822, onCondition__823);
        }
        public Query LeftJoin(ISafeIdentifier table__826, SqlFragment onCondition__827)
        {
            LeftJoin t___8957 = new LeftJoin();
            return this.Join(t___8957, table__826, onCondition__827);
        }
        public Query RightJoin(ISafeIdentifier table__830, SqlFragment onCondition__831)
        {
            RightJoin t___8955 = new RightJoin();
            return this.Join(t___8955, table__830, onCondition__831);
        }
        public Query FullJoin(ISafeIdentifier table__834, SqlFragment onCondition__835)
        {
            FullJoin t___8953 = new FullJoin();
            return this.Join(t___8953, table__834, onCondition__835);
        }
        public Query GroupBy(ISafeIdentifier field__838)
        {
            G::IList<ISafeIdentifier> nb__840 = L::Enumerable.ToList(this.groupByFields__746);
            C::Listed.Add(nb__840, field__838);
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, C::Listed.ToReadOnlyList(nb__840), this.havingConditions__747, this.isDistinct__748, this.selectExprs__749);
        }
        public Query Having(SqlFragment condition__842)
        {
            G::IList<IWhereClause> nb__844 = L::Enumerable.ToList(this.havingConditions__747);
            C::Listed.Add(nb__844, new AndCondition(condition__842));
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, C::Listed.ToReadOnlyList(nb__844), this.isDistinct__748, this.selectExprs__749);
        }
        public Query OrHaving(SqlFragment condition__846)
        {
            G::IList<IWhereClause> nb__848 = L::Enumerable.ToList(this.havingConditions__747);
            C::Listed.Add(nb__848, new OrCondition(condition__846));
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, C::Listed.ToReadOnlyList(nb__848), this.isDistinct__748, this.selectExprs__749);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__739, this.conditions__740, this.selectedFields__741, this.orderClauses__742, this.limitVal__743, this.offsetVal__744, this.joinClauses__745, this.groupByFields__746, this.havingConditions__747, true, this.selectExprs__749);
        }
        public SqlFragment ToSql()
        {
            int t___8859;
            int t___8878;
            int t___8897;
            SqlBuilder b__853 = new SqlBuilder();
            if (this.isDistinct__748) b__853.AppendSafe("SELECT DISTINCT ");
            else b__853.AppendSafe("SELECT ");
            if (!(this.selectExprs__749.Count == 0))
            {
                b__853.AppendFragment(this.selectExprs__749[0]);
                int i__854 = 1;
                while (true)
                {
                    t___8859 = this.selectExprs__749.Count;
                    if (!(i__854 < t___8859)) break;
                    b__853.AppendSafe(", ");
                    b__853.AppendFragment(this.selectExprs__749[i__854]);
                    i__854 = i__854 + 1;
                }
            }
            else if (this.selectedFields__741.Count == 0) b__853.AppendSafe("*");
            else
            {
                string fn__8852(ISafeIdentifier f__855)
                {
                    return f__855.SqlValue;
                }
                b__853.AppendSafe(C::Listed.Join(this.selectedFields__741, ", ", (S::Func<ISafeIdentifier, string>) fn__8852));
            }
            b__853.AppendSafe(" FROM ");
            b__853.AppendSafe(this.tableName__739.SqlValue);
            void fn__8851(JoinClause jc__856)
            {
                b__853.AppendSafe(" ");
                string t___8839 = jc__856.JoinType.Keyword();
                b__853.AppendSafe(t___8839);
                b__853.AppendSafe(" ");
                string t___8843 = jc__856.Table.SqlValue;
                b__853.AppendSafe(t___8843);
                b__853.AppendSafe(" ON ");
                SqlFragment t___8846 = jc__856.OnCondition;
                b__853.AppendFragment(t___8846);
            }
            C::Listed.ForEach(this.joinClauses__745, (S::Action<JoinClause>) fn__8851);
            if (!(this.conditions__740.Count == 0))
            {
                b__853.AppendSafe(" WHERE ");
                b__853.AppendFragment(this.conditions__740[0].Condition);
                int i__857 = 1;
                while (true)
                {
                    t___8878 = this.conditions__740.Count;
                    if (!(i__857 < t___8878)) break;
                    b__853.AppendSafe(" ");
                    b__853.AppendSafe(this.conditions__740[i__857].Keyword());
                    b__853.AppendSafe(" ");
                    b__853.AppendFragment(this.conditions__740[i__857].Condition);
                    i__857 = i__857 + 1;
                }
            }
            if (!(this.groupByFields__746.Count == 0))
            {
                b__853.AppendSafe(" GROUP BY ");
                string fn__8850(ISafeIdentifier f__858)
                {
                    return f__858.SqlValue;
                }
                b__853.AppendSafe(C::Listed.Join(this.groupByFields__746, ", ", (S::Func<ISafeIdentifier, string>) fn__8850));
            }
            if (!(this.havingConditions__747.Count == 0))
            {
                b__853.AppendSafe(" HAVING ");
                b__853.AppendFragment(this.havingConditions__747[0].Condition);
                int i__859 = 1;
                while (true)
                {
                    t___8897 = this.havingConditions__747.Count;
                    if (!(i__859 < t___8897)) break;
                    b__853.AppendSafe(" ");
                    b__853.AppendSafe(this.havingConditions__747[i__859].Keyword());
                    b__853.AppendSafe(" ");
                    b__853.AppendFragment(this.havingConditions__747[i__859].Condition);
                    i__859 = i__859 + 1;
                }
            }
            if (!(this.orderClauses__742.Count == 0))
            {
                b__853.AppendSafe(" ORDER BY ");
                bool first__860 = true;
                void fn__8849(OrderClause oc__861)
                {
                    string t___4769;
                    if (!first__860) b__853.AppendSafe(", ");
                    first__860 = false;
                    string t___8832 = oc__861.Field.SqlValue;
                    b__853.AppendSafe(t___8832);
                    if (oc__861.Ascending)
                    {
                        t___4769 = " ASC";
                    }
                    else
                    {
                        t___4769 = " DESC";
                    }
                    b__853.AppendSafe(t___4769);
                }
                C::Listed.ForEach(this.orderClauses__742, (S::Action<OrderClause>) fn__8849);
            }
            int ? lv__862 = this.limitVal__743;
            if (!(lv__862 == null))
            {
                int lv___1742 = lv__862.Value;
                b__853.AppendSafe(" LIMIT ");
                b__853.AppendInt32(lv___1742);
            }
            int ? ov__863 = this.offsetVal__744;
            if (!(ov__863 == null))
            {
                int ov___1743 = ov__863.Value;
                b__853.AppendSafe(" OFFSET ");
                b__853.AppendInt32(ov___1743);
            }
            return b__853.Accumulated;
        }
        public SqlFragment CountSql()
        {
            int t___8801;
            int t___8820;
            SqlBuilder b__866 = new SqlBuilder();
            b__866.AppendSafe("SELECT COUNT(*) FROM ");
            b__866.AppendSafe(this.tableName__739.SqlValue);
            void fn__8789(JoinClause jc__867)
            {
                b__866.AppendSafe(" ");
                string t___8779 = jc__867.JoinType.Keyword();
                b__866.AppendSafe(t___8779);
                b__866.AppendSafe(" ");
                string t___8783 = jc__867.Table.SqlValue;
                b__866.AppendSafe(t___8783);
                b__866.AppendSafe(" ON ");
                SqlFragment t___8786 = jc__867.OnCondition;
                b__866.AppendFragment(t___8786);
            }
            C::Listed.ForEach(this.joinClauses__745, (S::Action<JoinClause>) fn__8789);
            if (!(this.conditions__740.Count == 0))
            {
                b__866.AppendSafe(" WHERE ");
                b__866.AppendFragment(this.conditions__740[0].Condition);
                int i__868 = 1;
                while (true)
                {
                    t___8801 = this.conditions__740.Count;
                    if (!(i__868 < t___8801)) break;
                    b__866.AppendSafe(" ");
                    b__866.AppendSafe(this.conditions__740[i__868].Keyword());
                    b__866.AppendSafe(" ");
                    b__866.AppendFragment(this.conditions__740[i__868].Condition);
                    i__868 = i__868 + 1;
                }
            }
            if (!(this.groupByFields__746.Count == 0))
            {
                b__866.AppendSafe(" GROUP BY ");
                string fn__8788(ISafeIdentifier f__869)
                {
                    return f__869.SqlValue;
                }
                b__866.AppendSafe(C::Listed.Join(this.groupByFields__746, ", ", (S::Func<ISafeIdentifier, string>) fn__8788));
            }
            if (!(this.havingConditions__747.Count == 0))
            {
                b__866.AppendSafe(" HAVING ");
                b__866.AppendFragment(this.havingConditions__747[0].Condition);
                int i__870 = 1;
                while (true)
                {
                    t___8820 = this.havingConditions__747.Count;
                    if (!(i__870 < t___8820)) break;
                    b__866.AppendSafe(" ");
                    b__866.AppendSafe(this.havingConditions__747[i__870].Keyword());
                    b__866.AppendSafe(" ");
                    b__866.AppendFragment(this.havingConditions__747[i__870].Condition);
                    i__870 = i__870 + 1;
                }
            }
            return b__866.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__872)
        {
            SqlFragment return__365;
            Query t___4718;
            if (defaultLimit__872 < 0) throw new S::Exception();
            if (!(this.limitVal__743 == null))
            {
                return__365 = this.ToSql();
            }
            else
            {
                t___4718 = this.Limit(defaultLimit__872);
                return__365 = t___4718.ToSql();
            }
            return return__365;
        }
        public Query(ISafeIdentifier tableName__875, G::IReadOnlyList<IWhereClause> conditions__876, G::IReadOnlyList<ISafeIdentifier> selectedFields__877, G::IReadOnlyList<OrderClause> orderClauses__878, int ? limitVal__879, int ? offsetVal__880, G::IReadOnlyList<JoinClause> joinClauses__881, G::IReadOnlyList<ISafeIdentifier> groupByFields__882, G::IReadOnlyList<IWhereClause> havingConditions__883, bool isDistinct__884, G::IReadOnlyList<SqlFragment> selectExprs__885)
        {
            this.tableName__739 = tableName__875;
            this.conditions__740 = conditions__876;
            this.selectedFields__741 = selectedFields__877;
            this.orderClauses__742 = orderClauses__878;
            this.limitVal__743 = limitVal__879;
            this.offsetVal__744 = offsetVal__880;
            this.joinClauses__745 = joinClauses__881;
            this.groupByFields__746 = groupByFields__882;
            this.havingConditions__747 = havingConditions__883;
            this.isDistinct__748 = isDistinct__884;
            this.selectExprs__749 = selectExprs__885;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__739;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__740;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__741;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__742;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__743;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__744;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__745;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__746;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__747;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__748;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__749;
            }
        }
    }
}
