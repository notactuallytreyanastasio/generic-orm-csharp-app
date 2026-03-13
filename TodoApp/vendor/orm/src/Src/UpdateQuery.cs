using S0 = Orm.Src;
using S1 = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class UpdateQuery
    {
        readonly ISafeIdentifier tableName__1594;
        readonly G::IReadOnlyList<SetClause> setClauses__1595;
        readonly G::IReadOnlyList<IWhereClause> conditions__1596;
        readonly int ? limitVal__1597;
        public UpdateQuery Set(ISafeIdentifier field__1599, ISqlPart value__1600)
        {
            G::IList<SetClause> nb__1602 = L::Enumerable.ToList(this.setClauses__1595);
            C::Listed.Add(nb__1602, new SetClause(field__1599, value__1600));
            return new UpdateQuery(this.tableName__1594, C::Listed.ToReadOnlyList(nb__1602), this.conditions__1596, this.limitVal__1597);
        }
        public UpdateQuery Where(SqlFragment condition__1604)
        {
            G::IList<IWhereClause> nb__1606 = L::Enumerable.ToList(this.conditions__1596);
            C::Listed.Add(nb__1606, new AndCondition(condition__1604));
            return new UpdateQuery(this.tableName__1594, this.setClauses__1595, C::Listed.ToReadOnlyList(nb__1606), this.limitVal__1597);
        }
        public UpdateQuery OrWhere(SqlFragment condition__1608)
        {
            G::IList<IWhereClause> nb__1610 = L::Enumerable.ToList(this.conditions__1596);
            C::Listed.Add(nb__1610, new OrCondition(condition__1608));
            return new UpdateQuery(this.tableName__1594, this.setClauses__1595, C::Listed.ToReadOnlyList(nb__1610), this.limitVal__1597);
        }
        public UpdateQuery Limit(int n__1612)
        {
            if (n__1612 < 0) throw new S1::Exception();
            return new UpdateQuery(this.tableName__1594, this.setClauses__1595, this.conditions__1596, n__1612);
        }
        public SqlFragment ToSql()
        {
            int t___15132;
            if (this.conditions__1596.Count == 0) throw new S1::Exception();
            if (this.setClauses__1595.Count == 0) throw new S1::Exception();
            SqlBuilder b__1616 = new SqlBuilder();
            b__1616.AppendSafe("UPDATE ");
            b__1616.AppendSafe(this.tableName__1594.SqlValue);
            b__1616.AppendSafe(" SET ");
            b__1616.AppendSafe(this.setClauses__1595[0].Field.SqlValue);
            b__1616.AppendSafe(" = ");
            b__1616.AppendPart(this.setClauses__1595[0].Value);
            int i__1617 = 1;
            while (true)
            {
                t___15132 = this.setClauses__1595.Count;
                if (!(i__1617 < t___15132)) break;
                b__1616.AppendSafe(", ");
                b__1616.AppendSafe(this.setClauses__1595[i__1617].Field.SqlValue);
                b__1616.AppendSafe(" = ");
                b__1616.AppendPart(this.setClauses__1595[i__1617].Value);
                i__1617 = i__1617 + 1;
            }
            S0::SrcGlobal.renderWhere__705(b__1616, this.conditions__1596);
            int ? lv__1618 = this.limitVal__1597;
            if (!(lv__1618 == null))
            {
                int lv___2849 = lv__1618.Value;
                b__1616.AppendSafe(" LIMIT ");
                b__1616.AppendInt32(lv___2849);
            }
            return b__1616.Accumulated;
        }
        public UpdateQuery(ISafeIdentifier tableName__1620, G::IReadOnlyList<SetClause> setClauses__1621, G::IReadOnlyList<IWhereClause> conditions__1622, int ? limitVal__1623)
        {
            this.tableName__1594 = tableName__1620;
            this.setClauses__1595 = setClauses__1621;
            this.conditions__1596 = conditions__1622;
            this.limitVal__1597 = limitVal__1623;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1594;
            }
        }
        public G::IReadOnlyList<SetClause> SetClauses
        {
            get
            {
                return this.setClauses__1595;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1596;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1597;
            }
        }
    }
}
