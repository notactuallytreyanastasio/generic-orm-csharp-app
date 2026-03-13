using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class DeleteQuery
    {
        readonly ISafeIdentifier tableName__1437;
        readonly G::IReadOnlyList<IWhereClause> conditions__1438;
        readonly int ? limitVal__1439;
        public DeleteQuery Where(SqlFragment condition__1441)
        {
            G::IList<IWhereClause> nb__1443 = L::Enumerable.ToList(this.conditions__1438);
            C::Listed.Add(nb__1443, new AndCondition(condition__1441));
            return new DeleteQuery(this.tableName__1437, C::Listed.ToReadOnlyList(nb__1443), this.limitVal__1439);
        }
        public DeleteQuery OrWhere(SqlFragment condition__1445)
        {
            G::IList<IWhereClause> nb__1447 = L::Enumerable.ToList(this.conditions__1438);
            C::Listed.Add(nb__1447, new OrCondition(condition__1445));
            return new DeleteQuery(this.tableName__1437, C::Listed.ToReadOnlyList(nb__1447), this.limitVal__1439);
        }
        public DeleteQuery Limit(int n__1449)
        {
            if (n__1449 < 0) throw new S::Exception();
            return new DeleteQuery(this.tableName__1437, this.conditions__1438, n__1449);
        }
        public SqlFragment ToSql()
        {
            int t___12517;
            if (this.conditions__1438.Count == 0) throw new S::Exception();
            SqlBuilder b__1453 = new SqlBuilder();
            b__1453.AppendSafe("DELETE FROM ");
            b__1453.AppendSafe(this.tableName__1437.SqlValue);
            b__1453.AppendSafe(" WHERE ");
            b__1453.AppendFragment(this.conditions__1438[0].Condition);
            int i__1454 = 1;
            while (true)
            {
                t___12517 = this.conditions__1438.Count;
                if (!(i__1454 < t___12517)) break;
                b__1453.AppendSafe(" ");
                b__1453.AppendSafe(this.conditions__1438[i__1454].Keyword());
                b__1453.AppendSafe(" ");
                b__1453.AppendFragment(this.conditions__1438[i__1454].Condition);
                i__1454 = i__1454 + 1;
            }
            int ? lv__1455 = this.limitVal__1439;
            if (!(lv__1455 == null))
            {
                int lv___2485 = lv__1455.Value;
                b__1453.AppendSafe(" LIMIT ");
                b__1453.AppendInt32(lv___2485);
            }
            return b__1453.Accumulated;
        }
        public DeleteQuery(ISafeIdentifier tableName__1457, G::IReadOnlyList<IWhereClause> conditions__1458, int ? limitVal__1459)
        {
            this.tableName__1437 = tableName__1457;
            this.conditions__1438 = conditions__1458;
            this.limitVal__1439 = limitVal__1459;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1437;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1438;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1439;
            }
        }
    }
}
