using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__678;
        readonly G::IReadOnlyList<IWhereClause> conditions__679;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__680;
        readonly G::IReadOnlyList<OrderClause> orderClauses__681;
        readonly int ? limitVal__682;
        readonly int ? offsetVal__683;
        readonly G::IReadOnlyList<JoinClause> joinClauses__684;
        public Query Where(SqlFragment condition__686)
        {
            G::IList<IWhereClause> nb__688 = L::Enumerable.ToList(this.conditions__679);
            C::Listed.Add(nb__688, new AndCondition(condition__686));
            return new Query(this.tableName__678, C::Listed.ToReadOnlyList(nb__688), this.selectedFields__680, this.orderClauses__681, this.limitVal__682, this.offsetVal__683, this.joinClauses__684);
        }
        public Query OrWhere(SqlFragment condition__690)
        {
            G::IList<IWhereClause> nb__692 = L::Enumerable.ToList(this.conditions__679);
            C::Listed.Add(nb__692, new OrCondition(condition__690));
            return new Query(this.tableName__678, C::Listed.ToReadOnlyList(nb__692), this.selectedFields__680, this.orderClauses__681, this.limitVal__682, this.offsetVal__683, this.joinClauses__684);
        }
        public Query WhereNull(ISafeIdentifier field__694)
        {
            SqlBuilder b__696 = new SqlBuilder();
            b__696.AppendSafe(field__694.SqlValue);
            b__696.AppendSafe(" IS NULL");
            SqlFragment t___6599 = b__696.Accumulated;
            return this.Where(t___6599);
        }
        public Query WhereNotNull(ISafeIdentifier field__698)
        {
            SqlBuilder b__700 = new SqlBuilder();
            b__700.AppendSafe(field__698.SqlValue);
            b__700.AppendSafe(" IS NOT NULL");
            SqlFragment t___6593 = b__700.Accumulated;
            return this.Where(t___6593);
        }
        public Query WhereIn(ISafeIdentifier field__702, G::IReadOnlyList<ISqlPart> values__703)
        {
            Query return__301;
            SqlFragment t___6574;
            int t___6582;
            SqlFragment t___6587;
            {
                {
                    if (values__703.Count == 0)
                    {
                        SqlBuilder b__705 = new SqlBuilder();
                        b__705.AppendSafe("1 = 0");
                        t___6574 = b__705.Accumulated;
                        return__301 = this.Where(t___6574);
                        goto fn__704;
                    }
                    SqlBuilder b__706 = new SqlBuilder();
                    b__706.AppendSafe(field__702.SqlValue);
                    b__706.AppendSafe(" IN (");
                    b__706.AppendPart(values__703[0]);
                    int i__707 = 1;
                    while (true)
                    {
                        t___6582 = values__703.Count;
                        if (!(i__707 < t___6582)) break;
                        b__706.AppendSafe(", ");
                        b__706.AppendPart(values__703[i__707]);
                        i__707 = i__707 + 1;
                    }
                    b__706.AppendSafe(")");
                    t___6587 = b__706.Accumulated;
                    return__301 = this.Where(t___6587);
                }
                fn__704:
                {
                }
            }
            return return__301;
        }
        public Query WhereNot(SqlFragment condition__709)
        {
            SqlBuilder b__711 = new SqlBuilder();
            b__711.AppendSafe("NOT (");
            b__711.AppendFragment(condition__709);
            b__711.AppendSafe(")");
            SqlFragment t___6569 = b__711.Accumulated;
            return this.Where(t___6569);
        }
        public Query WhereBetween(ISafeIdentifier field__713, ISqlPart low__714, ISqlPart high__715)
        {
            SqlBuilder b__717 = new SqlBuilder();
            b__717.AppendSafe(field__713.SqlValue);
            b__717.AppendSafe(" BETWEEN ");
            b__717.AppendPart(low__714);
            b__717.AppendSafe(" AND ");
            b__717.AppendPart(high__715);
            SqlFragment t___6563 = b__717.Accumulated;
            return this.Where(t___6563);
        }
        public Query WhereLike(ISafeIdentifier field__719, string pattern__720)
        {
            SqlBuilder b__722 = new SqlBuilder();
            b__722.AppendSafe(field__719.SqlValue);
            b__722.AppendSafe(" LIKE ");
            b__722.AppendString(pattern__720);
            SqlFragment t___6554 = b__722.Accumulated;
            return this.Where(t___6554);
        }
        public Query WhereILike(ISafeIdentifier field__724, string pattern__725)
        {
            SqlBuilder b__727 = new SqlBuilder();
            b__727.AppendSafe(field__724.SqlValue);
            b__727.AppendSafe(" ILIKE ");
            b__727.AppendString(pattern__725);
            SqlFragment t___6547 = b__727.Accumulated;
            return this.Where(t___6547);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__729)
        {
            return new Query(this.tableName__678, this.conditions__679, fields__729, this.orderClauses__681, this.limitVal__682, this.offsetVal__683, this.joinClauses__684);
        }
        public Query OrderBy(ISafeIdentifier field__732, bool ascending__733)
        {
            G::IList<OrderClause> nb__735 = L::Enumerable.ToList(this.orderClauses__681);
            C::Listed.Add(nb__735, new OrderClause(field__732, ascending__733));
            return new Query(this.tableName__678, this.conditions__679, this.selectedFields__680, C::Listed.ToReadOnlyList(nb__735), this.limitVal__682, this.offsetVal__683, this.joinClauses__684);
        }
        public Query Limit(int n__737)
        {
            if (n__737 < 0) throw new S::Exception();
            return new Query(this.tableName__678, this.conditions__679, this.selectedFields__680, this.orderClauses__681, n__737, this.offsetVal__683, this.joinClauses__684);
        }
        public Query Offset(int n__740)
        {
            if (n__740 < 0) throw new S::Exception();
            return new Query(this.tableName__678, this.conditions__679, this.selectedFields__680, this.orderClauses__681, this.limitVal__682, n__740, this.joinClauses__684);
        }
        public Query Join(IJoinType joinType__743, ISafeIdentifier table__744, SqlFragment onCondition__745)
        {
            G::IList<JoinClause> nb__747 = L::Enumerable.ToList(this.joinClauses__684);
            C::Listed.Add(nb__747, new JoinClause(joinType__743, table__744, onCondition__745));
            return new Query(this.tableName__678, this.conditions__679, this.selectedFields__680, this.orderClauses__681, this.limitVal__682, this.offsetVal__683, C::Listed.ToReadOnlyList(nb__747));
        }
        public Query InnerJoin(ISafeIdentifier table__749, SqlFragment onCondition__750)
        {
            InnerJoin t___6518 = new InnerJoin();
            return this.Join(t___6518, table__749, onCondition__750);
        }
        public Query LeftJoin(ISafeIdentifier table__753, SqlFragment onCondition__754)
        {
            LeftJoin t___6516 = new LeftJoin();
            return this.Join(t___6516, table__753, onCondition__754);
        }
        public Query RightJoin(ISafeIdentifier table__757, SqlFragment onCondition__758)
        {
            RightJoin t___6514 = new RightJoin();
            return this.Join(t___6514, table__757, onCondition__758);
        }
        public Query FullJoin(ISafeIdentifier table__761, SqlFragment onCondition__762)
        {
            FullJoin t___6512 = new FullJoin();
            return this.Join(t___6512, table__761, onCondition__762);
        }
        public SqlFragment ToSql()
        {
            int t___6494;
            SqlBuilder b__766 = new SqlBuilder();
            b__766.AppendSafe("SELECT ");
            if (this.selectedFields__680.Count == 0) b__766.AppendSafe("*");
            else
            {
                string fn__6476(ISafeIdentifier f__767)
                {
                    return f__767.SqlValue;
                }
                b__766.AppendSafe(C::Listed.Join(this.selectedFields__680, ", ", (S::Func<ISafeIdentifier, string>) fn__6476));
            }
            b__766.AppendSafe(" FROM ");
            b__766.AppendSafe(this.tableName__678.SqlValue);
            void fn__6475(JoinClause jc__768)
            {
                b__766.AppendSafe(" ");
                string t___6464 = jc__768.JoinType.Keyword();
                b__766.AppendSafe(t___6464);
                b__766.AppendSafe(" ");
                string t___6468 = jc__768.Table.SqlValue;
                b__766.AppendSafe(t___6468);
                b__766.AppendSafe(" ON ");
                SqlFragment t___6471 = jc__768.OnCondition;
                b__766.AppendFragment(t___6471);
            }
            C::Listed.ForEach(this.joinClauses__684, (S::Action<JoinClause>) fn__6475);
            if (!(this.conditions__679.Count == 0))
            {
                b__766.AppendSafe(" WHERE ");
                b__766.AppendFragment(this.conditions__679[0].Condition);
                int i__769 = 1;
                while (true)
                {
                    t___6494 = this.conditions__679.Count;
                    if (!(i__769 < t___6494)) break;
                    b__766.AppendSafe(" ");
                    b__766.AppendSafe(this.conditions__679[i__769].Keyword());
                    b__766.AppendSafe(" ");
                    b__766.AppendFragment(this.conditions__679[i__769].Condition);
                    i__769 = i__769 + 1;
                }
            }
            if (!(this.orderClauses__681.Count == 0))
            {
                b__766.AppendSafe(" ORDER BY ");
                bool first__770 = true;
                void fn__6474(OrderClause oc__771)
                {
                    string t___3520;
                    if (!first__770) b__766.AppendSafe(", ");
                    first__770 = false;
                    string t___6458 = oc__771.Field.SqlValue;
                    b__766.AppendSafe(t___6458);
                    if (oc__771.Ascending)
                    {
                        t___3520 = " ASC";
                    }
                    else
                    {
                        t___3520 = " DESC";
                    }
                    b__766.AppendSafe(t___3520);
                }
                C::Listed.ForEach(this.orderClauses__681, (S::Action<OrderClause>) fn__6474);
            }
            int ? lv__772 = this.limitVal__682;
            if (!(lv__772 == null))
            {
                int lv___1452 = lv__772.Value;
                b__766.AppendSafe(" LIMIT ");
                b__766.AppendInt32(lv___1452);
            }
            int ? ov__773 = this.offsetVal__683;
            if (!(ov__773 == null))
            {
                int ov___1453 = ov__773.Value;
                b__766.AppendSafe(" OFFSET ");
                b__766.AppendInt32(ov___1453);
            }
            return b__766.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__775)
        {
            SqlFragment return__316;
            Query t___3513;
            if (defaultLimit__775 < 0) throw new S::Exception();
            if (!(this.limitVal__682 == null))
            {
                return__316 = this.ToSql();
            }
            else
            {
                t___3513 = this.Limit(defaultLimit__775);
                return__316 = t___3513.ToSql();
            }
            return return__316;
        }
        public Query(ISafeIdentifier tableName__778, G::IReadOnlyList<IWhereClause> conditions__779, G::IReadOnlyList<ISafeIdentifier> selectedFields__780, G::IReadOnlyList<OrderClause> orderClauses__781, int ? limitVal__782, int ? offsetVal__783, G::IReadOnlyList<JoinClause> joinClauses__784)
        {
            this.tableName__678 = tableName__778;
            this.conditions__679 = conditions__779;
            this.selectedFields__680 = selectedFields__780;
            this.orderClauses__681 = orderClauses__781;
            this.limitVal__682 = limitVal__782;
            this.offsetVal__683 = offsetVal__783;
            this.joinClauses__684 = joinClauses__784;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__678;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__679;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__680;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__681;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__682;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__683;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__684;
            }
        }
    }
}
