using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class UpdateQuery
    {
        readonly ISafeIdentifier tableName__1479;
        readonly G::IReadOnlyList<SetClause> setClauses__1480;
        readonly G::IReadOnlyList<IWhereClause> conditions__1481;
        readonly int ? limitVal__1482;
        public UpdateQuery Set(ISafeIdentifier field__1484, ISqlPart value__1485)
        {
            G::IList<SetClause> nb__1487 = L::Enumerable.ToList(this.setClauses__1480);
            C::Listed.Add(nb__1487, new SetClause(field__1484, value__1485));
            return new UpdateQuery(this.tableName__1479, C::Listed.ToReadOnlyList(nb__1487), this.conditions__1481, this.limitVal__1482);
        }
        public UpdateQuery Where(SqlFragment condition__1489)
        {
            G::IList<IWhereClause> nb__1491 = L::Enumerable.ToList(this.conditions__1481);
            C::Listed.Add(nb__1491, new AndCondition(condition__1489));
            return new UpdateQuery(this.tableName__1479, this.setClauses__1480, C::Listed.ToReadOnlyList(nb__1491), this.limitVal__1482);
        }
        public UpdateQuery OrWhere(SqlFragment condition__1493)
        {
            G::IList<IWhereClause> nb__1495 = L::Enumerable.ToList(this.conditions__1481);
            C::Listed.Add(nb__1495, new OrCondition(condition__1493));
            return new UpdateQuery(this.tableName__1479, this.setClauses__1480, C::Listed.ToReadOnlyList(nb__1495), this.limitVal__1482);
        }
        public UpdateQuery Limit(int n__1497)
        {
            if (n__1497 < 0) throw new S::Exception();
            return new UpdateQuery(this.tableName__1479, this.setClauses__1480, this.conditions__1481, n__1497);
        }
        public SqlFragment ToSql()
        {
            int t___13604;
            int t___13618;
            if (this.conditions__1481.Count == 0) throw new S::Exception();
            if (this.setClauses__1480.Count == 0) throw new S::Exception();
            SqlBuilder b__1501 = new SqlBuilder();
            b__1501.AppendSafe("UPDATE ");
            b__1501.AppendSafe(this.tableName__1479.SqlValue);
            b__1501.AppendSafe(" SET ");
            b__1501.AppendSafe(this.setClauses__1480[0].Field.SqlValue);
            b__1501.AppendSafe(" = ");
            b__1501.AppendPart(this.setClauses__1480[0].Value);
            int i__1502 = 1;
            while (true)
            {
                t___13604 = this.setClauses__1480.Count;
                if (!(i__1502 < t___13604)) break;
                b__1501.AppendSafe(", ");
                b__1501.AppendSafe(this.setClauses__1480[i__1502].Field.SqlValue);
                b__1501.AppendSafe(" = ");
                b__1501.AppendPart(this.setClauses__1480[i__1502].Value);
                i__1502 = i__1502 + 1;
            }
            b__1501.AppendSafe(" WHERE ");
            b__1501.AppendFragment(this.conditions__1481[0].Condition);
            int i__1503 = 1;
            while (true)
            {
                t___13618 = this.conditions__1481.Count;
                if (!(i__1503 < t___13618)) break;
                b__1501.AppendSafe(" ");
                b__1501.AppendSafe(this.conditions__1481[i__1503].Keyword());
                b__1501.AppendSafe(" ");
                b__1501.AppendFragment(this.conditions__1481[i__1503].Condition);
                i__1503 = i__1503 + 1;
            }
            int ? lv__1504 = this.limitVal__1482;
            if (!(lv__1504 == null))
            {
                int lv___2629 = lv__1504.Value;
                b__1501.AppendSafe(" LIMIT ");
                b__1501.AppendInt32(lv___2629);
            }
            return b__1501.Accumulated;
        }
        public UpdateQuery(ISafeIdentifier tableName__1506, G::IReadOnlyList<SetClause> setClauses__1507, G::IReadOnlyList<IWhereClause> conditions__1508, int ? limitVal__1509)
        {
            this.tableName__1479 = tableName__1506;
            this.setClauses__1480 = setClauses__1507;
            this.conditions__1481 = conditions__1508;
            this.limitVal__1482 = limitVal__1509;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1479;
            }
        }
        public G::IReadOnlyList<SetClause> SetClauses
        {
            get
            {
                return this.setClauses__1480;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1481;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1482;
            }
        }
    }
}
