using S0 = Orm.Src;
using S1 = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class DeleteQuery
    {
        readonly ISafeIdentifier tableName__1624;
        readonly G::IReadOnlyList<IWhereClause> conditions__1625;
        readonly int ? limitVal__1626;
        public DeleteQuery Where(SqlFragment condition__1628)
        {
            G::IList<IWhereClause> nb__1630 = L::Enumerable.ToList(this.conditions__1625);
            C::Listed.Add(nb__1630, new AndCondition(condition__1628));
            return new DeleteQuery(this.tableName__1624, C::Listed.ToReadOnlyList(nb__1630), this.limitVal__1626);
        }
        public DeleteQuery OrWhere(SqlFragment condition__1632)
        {
            G::IList<IWhereClause> nb__1634 = L::Enumerable.ToList(this.conditions__1625);
            C::Listed.Add(nb__1634, new OrCondition(condition__1632));
            return new DeleteQuery(this.tableName__1624, C::Listed.ToReadOnlyList(nb__1634), this.limitVal__1626);
        }
        public DeleteQuery Limit(int n__1636)
        {
            if (n__1636 < 0) throw new S1::Exception();
            return new DeleteQuery(this.tableName__1624, this.conditions__1625, n__1636);
        }
        public SqlFragment ToSql()
        {
            if (this.conditions__1625.Count == 0) throw new S1::Exception();
            SqlBuilder b__1640 = new SqlBuilder();
            b__1640.AppendSafe("DELETE FROM ");
            b__1640.AppendSafe(this.tableName__1624.SqlValue);
            S0::SrcGlobal.renderWhere__705(b__1640, this.conditions__1625);
            int ? lv__1641 = this.limitVal__1626;
            if (!(lv__1641 == null))
            {
                int lv___2850 = lv__1641.Value;
                b__1640.AppendSafe(" LIMIT ");
                b__1640.AppendInt32(lv___2850);
            }
            return b__1640.Accumulated;
        }
        public DeleteQuery(ISafeIdentifier tableName__1643, G::IReadOnlyList<IWhereClause> conditions__1644, int ? limitVal__1645)
        {
            this.tableName__1624 = tableName__1643;
            this.conditions__1625 = conditions__1644;
            this.limitVal__1626 = limitVal__1645;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1624;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1625;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1626;
            }
        }
    }
}
