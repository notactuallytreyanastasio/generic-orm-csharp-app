using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class DeleteQuery
    {
        readonly ISafeIdentifier tableName__1510;
        readonly G::IReadOnlyList<IWhereClause> conditions__1511;
        readonly int ? limitVal__1512;
        public DeleteQuery Where(SqlFragment condition__1514)
        {
            G::IList<IWhereClause> nb__1516 = L::Enumerable.ToList(this.conditions__1511);
            C::Listed.Add(nb__1516, new AndCondition(condition__1514));
            return new DeleteQuery(this.tableName__1510, C::Listed.ToReadOnlyList(nb__1516), this.limitVal__1512);
        }
        public DeleteQuery OrWhere(SqlFragment condition__1518)
        {
            G::IList<IWhereClause> nb__1520 = L::Enumerable.ToList(this.conditions__1511);
            C::Listed.Add(nb__1520, new OrCondition(condition__1518));
            return new DeleteQuery(this.tableName__1510, C::Listed.ToReadOnlyList(nb__1520), this.limitVal__1512);
        }
        public DeleteQuery Limit(int n__1522)
        {
            if (n__1522 < 0) throw new S::Exception();
            return new DeleteQuery(this.tableName__1510, this.conditions__1511, n__1522);
        }
        public SqlFragment ToSql()
        {
            int t___13564;
            if (this.conditions__1511.Count == 0) throw new S::Exception();
            SqlBuilder b__1526 = new SqlBuilder();
            b__1526.AppendSafe("DELETE FROM ");
            b__1526.AppendSafe(this.tableName__1510.SqlValue);
            b__1526.AppendSafe(" WHERE ");
            b__1526.AppendFragment(this.conditions__1511[0].Condition);
            int i__1527 = 1;
            while (true)
            {
                t___13564 = this.conditions__1511.Count;
                if (!(i__1527 < t___13564)) break;
                b__1526.AppendSafe(" ");
                b__1526.AppendSafe(this.conditions__1511[i__1527].Keyword());
                b__1526.AppendSafe(" ");
                b__1526.AppendFragment(this.conditions__1511[i__1527].Condition);
                i__1527 = i__1527 + 1;
            }
            int ? lv__1528 = this.limitVal__1512;
            if (!(lv__1528 == null))
            {
                int lv___2630 = lv__1528.Value;
                b__1526.AppendSafe(" LIMIT ");
                b__1526.AppendInt32(lv___2630);
            }
            return b__1526.Accumulated;
        }
        public DeleteQuery(ISafeIdentifier tableName__1530, G::IReadOnlyList<IWhereClause> conditions__1531, int ? limitVal__1532)
        {
            this.tableName__1510 = tableName__1530;
            this.conditions__1511 = conditions__1531;
            this.limitVal__1512 = limitVal__1532;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1510;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1511;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1512;
            }
        }
    }
}
