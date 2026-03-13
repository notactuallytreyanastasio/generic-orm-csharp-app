using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__543;
        readonly G::IReadOnlyList<SqlFragment> conditions__544;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__545;
        readonly G::IReadOnlyList<OrderClause> orderClauses__546;
        readonly int ? limitVal__547;
        readonly int ? offsetVal__548;
        public Query Where(SqlFragment condition__550)
        {
            G::IList<SqlFragment> nb__552 = L::Enumerable.ToList(this.conditions__544);
            C::Listed.Add(nb__552, condition__550);
            return new Query(this.tableName__543, C::Listed.ToReadOnlyList(nb__552), this.selectedFields__545, this.orderClauses__546, this.limitVal__547, this.offsetVal__548);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__554)
        {
            return new Query(this.tableName__543, this.conditions__544, fields__554, this.orderClauses__546, this.limitVal__547, this.offsetVal__548);
        }
        public Query OrderBy(ISafeIdentifier field__557, bool ascending__558)
        {
            G::IList<OrderClause> nb__560 = L::Enumerable.ToList(this.orderClauses__546);
            C::Listed.Add(nb__560, new OrderClause(field__557, ascending__558));
            return new Query(this.tableName__543, this.conditions__544, this.selectedFields__545, C::Listed.ToReadOnlyList(nb__560), this.limitVal__547, this.offsetVal__548);
        }
        public Query Limit(int n__562)
        {
            if (n__562 < 0) throw new S::Exception();
            return new Query(this.tableName__543, this.conditions__544, this.selectedFields__545, this.orderClauses__546, n__562, this.offsetVal__548);
        }
        public Query Offset(int n__565)
        {
            if (n__565 < 0) throw new S::Exception();
            return new Query(this.tableName__543, this.conditions__544, this.selectedFields__545, this.orderClauses__546, this.limitVal__547, n__565);
        }
        public SqlFragment ToSql()
        {
            int t___4466;
            SqlBuilder b__569 = new SqlBuilder();
            b__569.AppendSafe("SELECT ");
            if (this.selectedFields__545.Count == 0) b__569.AppendSafe("*");
            else
            {
                string fn__4451(ISafeIdentifier f__570)
                {
                    return f__570.SqlValue;
                }
                b__569.AppendSafe(C::Listed.Join(this.selectedFields__545, ", ", (S::Func<ISafeIdentifier, string>) fn__4451));
            }
            b__569.AppendSafe(" FROM ");
            b__569.AppendSafe(this.tableName__543.SqlValue);
            if (!(this.conditions__544.Count == 0))
            {
                b__569.AppendSafe(" WHERE ");
                b__569.AppendFragment(this.conditions__544[0]);
                int i__571 = 1;
                while (true)
                {
                    t___4466 = this.conditions__544.Count;
                    if (!(i__571 < t___4466)) break;
                    b__569.AppendSafe(" AND ");
                    b__569.AppendFragment(this.conditions__544[i__571]);
                    i__571 = i__571 + 1;
                }
            }
            if (!(this.orderClauses__546.Count == 0))
            {
                b__569.AppendSafe(" ORDER BY ");
                bool first__572 = true;
                void fn__4450(OrderClause oc__573)
                {
                    string t___2442;
                    if (!first__572) b__569.AppendSafe(", ");
                    first__572 = false;
                    string t___4445 = oc__573.Field.SqlValue;
                    b__569.AppendSafe(t___4445);
                    if (oc__573.Ascending)
                    {
                        t___2442 = " ASC";
                    }
                    else
                    {
                        t___2442 = " DESC";
                    }
                    b__569.AppendSafe(t___2442);
                }
                C::Listed.ForEach(this.orderClauses__546, (S::Action<OrderClause>) fn__4450);
            }
            int ? lv__574 = this.limitVal__547;
            if (!(lv__574 == null))
            {
                int lv___1116 = lv__574.Value;
                b__569.AppendSafe(" LIMIT ");
                b__569.AppendInt32(lv___1116);
            }
            int ? ov__575 = this.offsetVal__548;
            if (!(ov__575 == null))
            {
                int ov___1117 = ov__575.Value;
                b__569.AppendSafe(" OFFSET ");
                b__569.AppendInt32(ov___1117);
            }
            return b__569.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__577)
        {
            SqlFragment return__221;
            Query t___2435;
            if (defaultLimit__577 < 0) throw new S::Exception();
            if (!(this.limitVal__547 == null))
            {
                return__221 = this.ToSql();
            }
            else
            {
                t___2435 = this.Limit(defaultLimit__577);
                return__221 = t___2435.ToSql();
            }
            return return__221;
        }
        public Query(ISafeIdentifier tableName__580, G::IReadOnlyList<SqlFragment> conditions__581, G::IReadOnlyList<ISafeIdentifier> selectedFields__582, G::IReadOnlyList<OrderClause> orderClauses__583, int ? limitVal__584, int ? offsetVal__585)
        {
            this.tableName__543 = tableName__580;
            this.conditions__544 = conditions__581;
            this.selectedFields__545 = selectedFields__582;
            this.orderClauses__546 = orderClauses__583;
            this.limitVal__547 = limitVal__584;
            this.offsetVal__548 = offsetVal__585;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__543;
            }
        }
        public G::IReadOnlyList<SqlFragment> Conditions
        {
            get
            {
                return this.conditions__544;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__545;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__546;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__547;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__548;
            }
        }
    }
}
