using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__845;
        readonly G::IReadOnlyList<IWhereClause> conditions__846;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__847;
        readonly G::IReadOnlyList<OrderClause> orderClauses__848;
        readonly int ? limitVal__849;
        readonly int ? offsetVal__850;
        readonly G::IReadOnlyList<JoinClause> joinClauses__851;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__852;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__853;
        readonly bool isDistinct__854;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__855;
        readonly ILockMode ? lockMode__856;
        public Query Where(SqlFragment condition__858)
        {
            G::IList<IWhereClause> nb__860 = L::Enumerable.ToList(this.conditions__846);
            C::Listed.Add(nb__860, new AndCondition(condition__858));
            return new Query(this.tableName__845, C::Listed.ToReadOnlyList(nb__860), this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query OrWhere(SqlFragment condition__862)
        {
            G::IList<IWhereClause> nb__864 = L::Enumerable.ToList(this.conditions__846);
            C::Listed.Add(nb__864, new OrCondition(condition__862));
            return new Query(this.tableName__845, C::Listed.ToReadOnlyList(nb__864), this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query WhereNull(ISafeIdentifier field__866)
        {
            SqlBuilder b__868 = new SqlBuilder();
            b__868.AppendSafe(field__866.SqlValue);
            b__868.AppendSafe(" IS NULL");
            SqlFragment t___10830 = b__868.Accumulated;
            return this.Where(t___10830);
        }
        public Query WhereNotNull(ISafeIdentifier field__870)
        {
            SqlBuilder b__872 = new SqlBuilder();
            b__872.AppendSafe(field__870.SqlValue);
            b__872.AppendSafe(" IS NOT NULL");
            SqlFragment t___10824 = b__872.Accumulated;
            return this.Where(t___10824);
        }
        public Query WhereIn(ISafeIdentifier field__874, G::IReadOnlyList<ISqlPart> values__875)
        {
            Query return__403;
            SqlFragment t___10805;
            int t___10813;
            SqlFragment t___10818;
            {
                {
                    if (values__875.Count == 0)
                    {
                        SqlBuilder b__877 = new SqlBuilder();
                        b__877.AppendSafe("1 = 0");
                        t___10805 = b__877.Accumulated;
                        return__403 = this.Where(t___10805);
                        goto fn__876;
                    }
                    SqlBuilder b__878 = new SqlBuilder();
                    b__878.AppendSafe(field__874.SqlValue);
                    b__878.AppendSafe(" IN (");
                    b__878.AppendPart(values__875[0]);
                    int i__879 = 1;
                    while (true)
                    {
                        t___10813 = values__875.Count;
                        if (!(i__879 < t___10813)) break;
                        b__878.AppendSafe(", ");
                        b__878.AppendPart(values__875[i__879]);
                        i__879 = i__879 + 1;
                    }
                    b__878.AppendSafe(")");
                    t___10818 = b__878.Accumulated;
                    return__403 = this.Where(t___10818);
                }
                fn__876:
                {
                }
            }
            return return__403;
        }
        public Query WhereInSubquery(ISafeIdentifier field__881, Query sub__882)
        {
            SqlBuilder b__884 = new SqlBuilder();
            b__884.AppendSafe(field__881.SqlValue);
            b__884.AppendSafe(" IN (");
            b__884.AppendFragment(sub__882.ToSql());
            b__884.AppendSafe(")");
            SqlFragment t___10800 = b__884.Accumulated;
            return this.Where(t___10800);
        }
        public Query WhereNot(SqlFragment condition__886)
        {
            SqlBuilder b__888 = new SqlBuilder();
            b__888.AppendSafe("NOT (");
            b__888.AppendFragment(condition__886);
            b__888.AppendSafe(")");
            SqlFragment t___10791 = b__888.Accumulated;
            return this.Where(t___10791);
        }
        public Query WhereBetween(ISafeIdentifier field__890, ISqlPart low__891, ISqlPart high__892)
        {
            SqlBuilder b__894 = new SqlBuilder();
            b__894.AppendSafe(field__890.SqlValue);
            b__894.AppendSafe(" BETWEEN ");
            b__894.AppendPart(low__891);
            b__894.AppendSafe(" AND ");
            b__894.AppendPart(high__892);
            SqlFragment t___10785 = b__894.Accumulated;
            return this.Where(t___10785);
        }
        public Query WhereLike(ISafeIdentifier field__896, string pattern__897)
        {
            SqlBuilder b__899 = new SqlBuilder();
            b__899.AppendSafe(field__896.SqlValue);
            b__899.AppendSafe(" LIKE ");
            b__899.AppendString(pattern__897);
            SqlFragment t___10776 = b__899.Accumulated;
            return this.Where(t___10776);
        }
        public Query WhereILike(ISafeIdentifier field__901, string pattern__902)
        {
            SqlBuilder b__904 = new SqlBuilder();
            b__904.AppendSafe(field__901.SqlValue);
            b__904.AppendSafe(" ILIKE ");
            b__904.AppendString(pattern__902);
            SqlFragment t___10769 = b__904.Accumulated;
            return this.Where(t___10769);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__906)
        {
            return new Query(this.tableName__845, this.conditions__846, fields__906, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__909)
        {
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, exprs__909, this.lockMode__856);
        }
        public Query OrderBy(ISafeIdentifier field__912, bool ascending__913)
        {
            G::IList<OrderClause> nb__915 = L::Enumerable.ToList(this.orderClauses__848);
            C::Listed.Add(nb__915, new OrderClause(field__912, ascending__913, null));
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, C::Listed.ToReadOnlyList(nb__915), this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query OrderByNulls(ISafeIdentifier field__917, bool ascending__918, INullsPosition nulls__919)
        {
            G::IList<OrderClause> nb__921 = L::Enumerable.ToList(this.orderClauses__848);
            C::Listed.Add(nb__921, new OrderClause(field__917, ascending__918, nulls__919));
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, C::Listed.ToReadOnlyList(nb__921), this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query Limit(int n__923)
        {
            if (n__923 < 0) throw new S::Exception();
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, n__923, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query Offset(int n__926)
        {
            if (n__926 < 0) throw new S::Exception();
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, n__926, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query Join(IJoinType joinType__929, ISafeIdentifier table__930, SqlFragment onCondition__931)
        {
            G::IList<JoinClause> nb__933 = L::Enumerable.ToList(this.joinClauses__851);
            C::Listed.Add(nb__933, new JoinClause(joinType__929, table__930, onCondition__931));
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, C::Listed.ToReadOnlyList(nb__933), this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query InnerJoin(ISafeIdentifier table__935, SqlFragment onCondition__936)
        {
            InnerJoin t___10731 = new InnerJoin();
            return this.Join(t___10731, table__935, onCondition__936);
        }
        public Query LeftJoin(ISafeIdentifier table__939, SqlFragment onCondition__940)
        {
            LeftJoin t___10729 = new LeftJoin();
            return this.Join(t___10729, table__939, onCondition__940);
        }
        public Query RightJoin(ISafeIdentifier table__943, SqlFragment onCondition__944)
        {
            RightJoin t___10727 = new RightJoin();
            return this.Join(t___10727, table__943, onCondition__944);
        }
        public Query FullJoin(ISafeIdentifier table__947, SqlFragment onCondition__948)
        {
            FullJoin t___10725 = new FullJoin();
            return this.Join(t___10725, table__947, onCondition__948);
        }
        public Query CrossJoin(ISafeIdentifier table__951)
        {
            G::IList<JoinClause> nb__953 = L::Enumerable.ToList(this.joinClauses__851);
            C::Listed.Add(nb__953, new JoinClause(new CrossJoin(), table__951, null));
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, C::Listed.ToReadOnlyList(nb__953), this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query GroupBy(ISafeIdentifier field__955)
        {
            G::IList<ISafeIdentifier> nb__957 = L::Enumerable.ToList(this.groupByFields__852);
            C::Listed.Add(nb__957, field__955);
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, C::Listed.ToReadOnlyList(nb__957), this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query Having(SqlFragment condition__959)
        {
            G::IList<IWhereClause> nb__961 = L::Enumerable.ToList(this.havingConditions__853);
            C::Listed.Add(nb__961, new AndCondition(condition__959));
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, C::Listed.ToReadOnlyList(nb__961), this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query OrHaving(SqlFragment condition__963)
        {
            G::IList<IWhereClause> nb__965 = L::Enumerable.ToList(this.havingConditions__853);
            C::Listed.Add(nb__965, new OrCondition(condition__963));
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, C::Listed.ToReadOnlyList(nb__965), this.isDistinct__854, this.selectExprs__855, this.lockMode__856);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, true, this.selectExprs__855, this.lockMode__856);
        }
        public Query Lock(ILockMode mode__969)
        {
            return new Query(this.tableName__845, this.conditions__846, this.selectedFields__847, this.orderClauses__848, this.limitVal__849, this.offsetVal__850, this.joinClauses__851, this.groupByFields__852, this.havingConditions__853, this.isDistinct__854, this.selectExprs__855, mode__969);
        }
        public SqlFragment ToSql()
        {
            int t___10616;
            int t___10635;
            int t___10654;
            SqlBuilder b__973 = new SqlBuilder();
            if (this.isDistinct__854) b__973.AppendSafe("SELECT DISTINCT ");
            else b__973.AppendSafe("SELECT ");
            if (!(this.selectExprs__855.Count == 0))
            {
                b__973.AppendFragment(this.selectExprs__855[0]);
                int i__974 = 1;
                while (true)
                {
                    t___10616 = this.selectExprs__855.Count;
                    if (!(i__974 < t___10616)) break;
                    b__973.AppendSafe(", ");
                    b__973.AppendFragment(this.selectExprs__855[i__974]);
                    i__974 = i__974 + 1;
                }
            }
            else if (this.selectedFields__847.Count == 0) b__973.AppendSafe("*");
            else
            {
                string fn__10609(ISafeIdentifier f__975)
                {
                    return f__975.SqlValue;
                }
                b__973.AppendSafe(C::Listed.Join(this.selectedFields__847, ", ", (S::Func<ISafeIdentifier, string>) fn__10609));
            }
            b__973.AppendSafe(" FROM ");
            b__973.AppendSafe(this.tableName__845.SqlValue);
            void fn__10608(JoinClause jc__976)
            {
                b__973.AppendSafe(" ");
                string t___10596 = jc__976.JoinType.Keyword();
                b__973.AppendSafe(t___10596);
                b__973.AppendSafe(" ");
                string t___10600 = jc__976.Table.SqlValue;
                b__973.AppendSafe(t___10600);
                SqlFragment ? oc__977 = jc__976.OnCondition;
                if (!(oc__977 == null))
                {
                    SqlFragment oc___2069 = oc__977!;
                    b__973.AppendSafe(" ON ");
                    b__973.AppendFragment(oc___2069);
                }
            }
            C::Listed.ForEach(this.joinClauses__851, (S::Action<JoinClause>) fn__10608);
            if (!(this.conditions__846.Count == 0))
            {
                b__973.AppendSafe(" WHERE ");
                b__973.AppendFragment(this.conditions__846[0].Condition);
                int i__978 = 1;
                while (true)
                {
                    t___10635 = this.conditions__846.Count;
                    if (!(i__978 < t___10635)) break;
                    b__973.AppendSafe(" ");
                    b__973.AppendSafe(this.conditions__846[i__978].Keyword());
                    b__973.AppendSafe(" ");
                    b__973.AppendFragment(this.conditions__846[i__978].Condition);
                    i__978 = i__978 + 1;
                }
            }
            if (!(this.groupByFields__852.Count == 0))
            {
                b__973.AppendSafe(" GROUP BY ");
                string fn__10607(ISafeIdentifier f__979)
                {
                    return f__979.SqlValue;
                }
                b__973.AppendSafe(C::Listed.Join(this.groupByFields__852, ", ", (S::Func<ISafeIdentifier, string>) fn__10607));
            }
            if (!(this.havingConditions__853.Count == 0))
            {
                b__973.AppendSafe(" HAVING ");
                b__973.AppendFragment(this.havingConditions__853[0].Condition);
                int i__980 = 1;
                while (true)
                {
                    t___10654 = this.havingConditions__853.Count;
                    if (!(i__980 < t___10654)) break;
                    b__973.AppendSafe(" ");
                    b__973.AppendSafe(this.havingConditions__853[i__980].Keyword());
                    b__973.AppendSafe(" ");
                    b__973.AppendFragment(this.havingConditions__853[i__980].Condition);
                    i__980 = i__980 + 1;
                }
            }
            if (!(this.orderClauses__848.Count == 0))
            {
                b__973.AppendSafe(" ORDER BY ");
                bool first__981 = true;
                void fn__10606(OrderClause orc__982)
                {
                    string t___10591;
                    string t___5801;
                    if (!first__981) b__973.AppendSafe(", ");
                    first__981 = false;
                    string t___10586 = orc__982.Field.SqlValue;
                    b__973.AppendSafe(t___10586);
                    if (orc__982.Ascending)
                    {
                        t___5801 = " ASC";
                    }
                    else
                    {
                        t___5801 = " DESC";
                    }
                    b__973.AppendSafe(t___5801);
                    INullsPosition ? np__983 = orc__982.NullsPos;
                    if (!(np__983 == null))
                    {
                        t___10591 = (np__983!).Keyword();
                        b__973.AppendSafe(t___10591);
                    }
                }
                C::Listed.ForEach(this.orderClauses__848, (S::Action<OrderClause>) fn__10606);
            }
            int ? lv__984 = this.limitVal__849;
            if (!(lv__984 == null))
            {
                int lv___2071 = lv__984.Value;
                b__973.AppendSafe(" LIMIT ");
                b__973.AppendInt32(lv___2071);
            }
            int ? ov__985 = this.offsetVal__850;
            if (!(ov__985 == null))
            {
                int ov___2072 = ov__985.Value;
                b__973.AppendSafe(" OFFSET ");
                b__973.AppendInt32(ov___2072);
            }
            ILockMode ? lm__986 = this.lockMode__856;
            if (!(lm__986 == null)) b__973.AppendSafe((lm__986!).Keyword());
            return b__973.Accumulated;
        }
        public SqlFragment CountSql()
        {
            int t___10555;
            int t___10574;
            SqlBuilder b__989 = new SqlBuilder();
            b__989.AppendSafe("SELECT COUNT(*) FROM ");
            b__989.AppendSafe(this.tableName__845.SqlValue);
            void fn__10543(JoinClause jc__990)
            {
                b__989.AppendSafe(" ");
                string t___10533 = jc__990.JoinType.Keyword();
                b__989.AppendSafe(t___10533);
                b__989.AppendSafe(" ");
                string t___10537 = jc__990.Table.SqlValue;
                b__989.AppendSafe(t___10537);
                SqlFragment ? oc2__991 = jc__990.OnCondition;
                if (!(oc2__991 == null))
                {
                    SqlFragment oc2___2074 = oc2__991!;
                    b__989.AppendSafe(" ON ");
                    b__989.AppendFragment(oc2___2074);
                }
            }
            C::Listed.ForEach(this.joinClauses__851, (S::Action<JoinClause>) fn__10543);
            if (!(this.conditions__846.Count == 0))
            {
                b__989.AppendSafe(" WHERE ");
                b__989.AppendFragment(this.conditions__846[0].Condition);
                int i__992 = 1;
                while (true)
                {
                    t___10555 = this.conditions__846.Count;
                    if (!(i__992 < t___10555)) break;
                    b__989.AppendSafe(" ");
                    b__989.AppendSafe(this.conditions__846[i__992].Keyword());
                    b__989.AppendSafe(" ");
                    b__989.AppendFragment(this.conditions__846[i__992].Condition);
                    i__992 = i__992 + 1;
                }
            }
            if (!(this.groupByFields__852.Count == 0))
            {
                b__989.AppendSafe(" GROUP BY ");
                string fn__10542(ISafeIdentifier f__993)
                {
                    return f__993.SqlValue;
                }
                b__989.AppendSafe(C::Listed.Join(this.groupByFields__852, ", ", (S::Func<ISafeIdentifier, string>) fn__10542));
            }
            if (!(this.havingConditions__853.Count == 0))
            {
                b__989.AppendSafe(" HAVING ");
                b__989.AppendFragment(this.havingConditions__853[0].Condition);
                int i__994 = 1;
                while (true)
                {
                    t___10574 = this.havingConditions__853.Count;
                    if (!(i__994 < t___10574)) break;
                    b__989.AppendSafe(" ");
                    b__989.AppendSafe(this.havingConditions__853[i__994].Keyword());
                    b__989.AppendSafe(" ");
                    b__989.AppendFragment(this.havingConditions__853[i__994].Condition);
                    i__994 = i__994 + 1;
                }
            }
            return b__989.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__996)
        {
            SqlFragment return__428;
            Query t___5751;
            if (defaultLimit__996 < 0) throw new S::Exception();
            if (!(this.limitVal__849 == null))
            {
                return__428 = this.ToSql();
            }
            else
            {
                t___5751 = this.Limit(defaultLimit__996);
                return__428 = t___5751.ToSql();
            }
            return return__428;
        }
        public Query(ISafeIdentifier tableName__999, G::IReadOnlyList<IWhereClause> conditions__1000, G::IReadOnlyList<ISafeIdentifier> selectedFields__1001, G::IReadOnlyList<OrderClause> orderClauses__1002, int ? limitVal__1003, int ? offsetVal__1004, G::IReadOnlyList<JoinClause> joinClauses__1005, G::IReadOnlyList<ISafeIdentifier> groupByFields__1006, G::IReadOnlyList<IWhereClause> havingConditions__1007, bool isDistinct__1008, G::IReadOnlyList<SqlFragment> selectExprs__1009, ILockMode ? lockMode__1010)
        {
            this.tableName__845 = tableName__999;
            this.conditions__846 = conditions__1000;
            this.selectedFields__847 = selectedFields__1001;
            this.orderClauses__848 = orderClauses__1002;
            this.limitVal__849 = limitVal__1003;
            this.offsetVal__850 = offsetVal__1004;
            this.joinClauses__851 = joinClauses__1005;
            this.groupByFields__852 = groupByFields__1006;
            this.havingConditions__853 = havingConditions__1007;
            this.isDistinct__854 = isDistinct__1008;
            this.selectExprs__855 = selectExprs__1009;
            this.lockMode__856 = lockMode__1010;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__845;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__846;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__847;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__848;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__849;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__850;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__851;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__852;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__853;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__854;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__855;
            }
        }
        public ILockMode ? LockMode
        {
            get
            {
                return this.lockMode__856;
            }
        }
    }
}
