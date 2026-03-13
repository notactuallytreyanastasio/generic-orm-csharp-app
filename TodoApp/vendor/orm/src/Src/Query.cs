using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__604;
        readonly G::IReadOnlyList<SqlFragment> conditions__605;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__606;
        readonly G::IReadOnlyList<OrderClause> orderClauses__607;
        readonly int ? limitVal__608;
        readonly int ? offsetVal__609;
        readonly G::IReadOnlyList<JoinClause> joinClauses__610;
        public Query Where(SqlFragment condition__612)
        {
            G::IList<SqlFragment> nb__614 = L::Enumerable.ToList(this.conditions__605);
            C::Listed.Add(nb__614, condition__612);
            return new Query(this.tableName__604, C::Listed.ToReadOnlyList(nb__614), this.selectedFields__606, this.orderClauses__607, this.limitVal__608, this.offsetVal__609, this.joinClauses__610);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__616)
        {
            return new Query(this.tableName__604, this.conditions__605, fields__616, this.orderClauses__607, this.limitVal__608, this.offsetVal__609, this.joinClauses__610);
        }
        public Query OrderBy(ISafeIdentifier field__619, bool ascending__620)
        {
            G::IList<OrderClause> nb__622 = L::Enumerable.ToList(this.orderClauses__607);
            C::Listed.Add(nb__622, new OrderClause(field__619, ascending__620));
            return new Query(this.tableName__604, this.conditions__605, this.selectedFields__606, C::Listed.ToReadOnlyList(nb__622), this.limitVal__608, this.offsetVal__609, this.joinClauses__610);
        }
        public Query Limit(int n__624)
        {
            if (n__624 < 0) throw new S::Exception();
            return new Query(this.tableName__604, this.conditions__605, this.selectedFields__606, this.orderClauses__607, n__624, this.offsetVal__609, this.joinClauses__610);
        }
        public Query Offset(int n__627)
        {
            if (n__627 < 0) throw new S::Exception();
            return new Query(this.tableName__604, this.conditions__605, this.selectedFields__606, this.orderClauses__607, this.limitVal__608, n__627, this.joinClauses__610);
        }
        public Query Join(IJoinType joinType__630, ISafeIdentifier table__631, SqlFragment onCondition__632)
        {
            G::IList<JoinClause> nb__634 = L::Enumerable.ToList(this.joinClauses__610);
            C::Listed.Add(nb__634, new JoinClause(joinType__630, table__631, onCondition__632));
            return new Query(this.tableName__604, this.conditions__605, this.selectedFields__606, this.orderClauses__607, this.limitVal__608, this.offsetVal__609, C::Listed.ToReadOnlyList(nb__634));
        }
        public Query InnerJoin(ISafeIdentifier table__636, SqlFragment onCondition__637)
        {
            InnerJoin t___5152 = new InnerJoin();
            return this.Join(t___5152, table__636, onCondition__637);
        }
        public Query LeftJoin(ISafeIdentifier table__640, SqlFragment onCondition__641)
        {
            LeftJoin t___5150 = new LeftJoin();
            return this.Join(t___5150, table__640, onCondition__641);
        }
        public Query RightJoin(ISafeIdentifier table__644, SqlFragment onCondition__645)
        {
            RightJoin t___5148 = new RightJoin();
            return this.Join(t___5148, table__644, onCondition__645);
        }
        public Query FullJoin(ISafeIdentifier table__648, SqlFragment onCondition__649)
        {
            FullJoin t___5146 = new FullJoin();
            return this.Join(t___5146, table__648, onCondition__649);
        }
        public SqlFragment ToSql()
        {
            int t___5133;
            SqlBuilder b__653 = new SqlBuilder();
            b__653.AppendSafe("SELECT ");
            if (this.selectedFields__606.Count == 0) b__653.AppendSafe("*");
            else
            {
                string fn__5116(ISafeIdentifier f__654)
                {
                    return f__654.SqlValue;
                }
                b__653.AppendSafe(C::Listed.Join(this.selectedFields__606, ", ", (S::Func<ISafeIdentifier, string>) fn__5116));
            }
            b__653.AppendSafe(" FROM ");
            b__653.AppendSafe(this.tableName__604.SqlValue);
            void fn__5115(JoinClause jc__655)
            {
                b__653.AppendSafe(" ");
                string t___5104 = jc__655.JoinType.Keyword();
                b__653.AppendSafe(t___5104);
                b__653.AppendSafe(" ");
                string t___5108 = jc__655.Table.SqlValue;
                b__653.AppendSafe(t___5108);
                b__653.AppendSafe(" ON ");
                SqlFragment t___5111 = jc__655.OnCondition;
                b__653.AppendFragment(t___5111);
            }
            C::Listed.ForEach(this.joinClauses__610, (S::Action<JoinClause>) fn__5115);
            if (!(this.conditions__605.Count == 0))
            {
                b__653.AppendSafe(" WHERE ");
                b__653.AppendFragment(this.conditions__605[0]);
                int i__656 = 1;
                while (true)
                {
                    t___5133 = this.conditions__605.Count;
                    if (!(i__656 < t___5133)) break;
                    b__653.AppendSafe(" AND ");
                    b__653.AppendFragment(this.conditions__605[i__656]);
                    i__656 = i__656 + 1;
                }
            }
            if (!(this.orderClauses__607.Count == 0))
            {
                b__653.AppendSafe(" ORDER BY ");
                bool first__657 = true;
                void fn__5114(OrderClause oc__658)
                {
                    string t___2812;
                    if (!first__657) b__653.AppendSafe(", ");
                    first__657 = false;
                    string t___5098 = oc__658.Field.SqlValue;
                    b__653.AppendSafe(t___5098);
                    if (oc__658.Ascending)
                    {
                        t___2812 = " ASC";
                    }
                    else
                    {
                        t___2812 = " DESC";
                    }
                    b__653.AppendSafe(t___2812);
                }
                C::Listed.ForEach(this.orderClauses__607, (S::Action<OrderClause>) fn__5114);
            }
            int ? lv__659 = this.limitVal__608;
            if (!(lv__659 == null))
            {
                int lv___1257 = lv__659.Value;
                b__653.AppendSafe(" LIMIT ");
                b__653.AppendInt32(lv___1257);
            }
            int ? ov__660 = this.offsetVal__609;
            if (!(ov__660 == null))
            {
                int ov___1258 = ov__660.Value;
                b__653.AppendSafe(" OFFSET ");
                b__653.AppendInt32(ov___1258);
            }
            return b__653.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__662)
        {
            SqlFragment return__260;
            Query t___2805;
            if (defaultLimit__662 < 0) throw new S::Exception();
            if (!(this.limitVal__608 == null))
            {
                return__260 = this.ToSql();
            }
            else
            {
                t___2805 = this.Limit(defaultLimit__662);
                return__260 = t___2805.ToSql();
            }
            return return__260;
        }
        public Query(ISafeIdentifier tableName__665, G::IReadOnlyList<SqlFragment> conditions__666, G::IReadOnlyList<ISafeIdentifier> selectedFields__667, G::IReadOnlyList<OrderClause> orderClauses__668, int ? limitVal__669, int ? offsetVal__670, G::IReadOnlyList<JoinClause> joinClauses__671)
        {
            this.tableName__604 = tableName__665;
            this.conditions__605 = conditions__666;
            this.selectedFields__606 = selectedFields__667;
            this.orderClauses__607 = orderClauses__668;
            this.limitVal__608 = limitVal__669;
            this.offsetVal__609 = offsetVal__670;
            this.joinClauses__610 = joinClauses__671;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__604;
            }
        }
        public G::IReadOnlyList<SqlFragment> Conditions
        {
            get
            {
                return this.conditions__605;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__606;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__607;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__608;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__609;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__610;
            }
        }
    }
}
