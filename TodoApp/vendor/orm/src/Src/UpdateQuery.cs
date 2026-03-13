using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class UpdateQuery
    {
        readonly ISafeIdentifier tableName__1406;
        readonly G::IReadOnlyList<SetClause> setClauses__1407;
        readonly G::IReadOnlyList<IWhereClause> conditions__1408;
        readonly int ? limitVal__1409;
        public UpdateQuery Set(ISafeIdentifier field__1411, ISqlPart value__1412)
        {
            G::IList<SetClause> nb__1414 = L::Enumerable.ToList(this.setClauses__1407);
            C::Listed.Add(nb__1414, new SetClause(field__1411, value__1412));
            return new UpdateQuery(this.tableName__1406, C::Listed.ToReadOnlyList(nb__1414), this.conditions__1408, this.limitVal__1409);
        }
        public UpdateQuery Where(SqlFragment condition__1416)
        {
            G::IList<IWhereClause> nb__1418 = L::Enumerable.ToList(this.conditions__1408);
            C::Listed.Add(nb__1418, new AndCondition(condition__1416));
            return new UpdateQuery(this.tableName__1406, this.setClauses__1407, C::Listed.ToReadOnlyList(nb__1418), this.limitVal__1409);
        }
        public UpdateQuery OrWhere(SqlFragment condition__1420)
        {
            G::IList<IWhereClause> nb__1422 = L::Enumerable.ToList(this.conditions__1408);
            C::Listed.Add(nb__1422, new OrCondition(condition__1420));
            return new UpdateQuery(this.tableName__1406, this.setClauses__1407, C::Listed.ToReadOnlyList(nb__1422), this.limitVal__1409);
        }
        public UpdateQuery Limit(int n__1424)
        {
            if (n__1424 < 0) throw new S::Exception();
            return new UpdateQuery(this.tableName__1406, this.setClauses__1407, this.conditions__1408, n__1424);
        }
        public SqlFragment ToSql()
        {
            int t___12557;
            int t___12571;
            if (this.conditions__1408.Count == 0) throw new S::Exception();
            if (this.setClauses__1407.Count == 0) throw new S::Exception();
            SqlBuilder b__1428 = new SqlBuilder();
            b__1428.AppendSafe("UPDATE ");
            b__1428.AppendSafe(this.tableName__1406.SqlValue);
            b__1428.AppendSafe(" SET ");
            b__1428.AppendSafe(this.setClauses__1407[0].Field.SqlValue);
            b__1428.AppendSafe(" = ");
            b__1428.AppendPart(this.setClauses__1407[0].Value);
            int i__1429 = 1;
            while (true)
            {
                t___12557 = this.setClauses__1407.Count;
                if (!(i__1429 < t___12557)) break;
                b__1428.AppendSafe(", ");
                b__1428.AppendSafe(this.setClauses__1407[i__1429].Field.SqlValue);
                b__1428.AppendSafe(" = ");
                b__1428.AppendPart(this.setClauses__1407[i__1429].Value);
                i__1429 = i__1429 + 1;
            }
            b__1428.AppendSafe(" WHERE ");
            b__1428.AppendFragment(this.conditions__1408[0].Condition);
            int i__1430 = 1;
            while (true)
            {
                t___12571 = this.conditions__1408.Count;
                if (!(i__1430 < t___12571)) break;
                b__1428.AppendSafe(" ");
                b__1428.AppendSafe(this.conditions__1408[i__1430].Keyword());
                b__1428.AppendSafe(" ");
                b__1428.AppendFragment(this.conditions__1408[i__1430].Condition);
                i__1430 = i__1430 + 1;
            }
            int ? lv__1431 = this.limitVal__1409;
            if (!(lv__1431 == null))
            {
                int lv___2484 = lv__1431.Value;
                b__1428.AppendSafe(" LIMIT ");
                b__1428.AppendInt32(lv___2484);
            }
            return b__1428.Accumulated;
        }
        public UpdateQuery(ISafeIdentifier tableName__1433, G::IReadOnlyList<SetClause> setClauses__1434, G::IReadOnlyList<IWhereClause> conditions__1435, int ? limitVal__1436)
        {
            this.tableName__1406 = tableName__1433;
            this.setClauses__1407 = setClauses__1434;
            this.conditions__1408 = conditions__1435;
            this.limitVal__1409 = limitVal__1436;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1406;
            }
        }
        public G::IReadOnlyList<SetClause> SetClauses
        {
            get
            {
                return this.setClauses__1407;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1408;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1409;
            }
        }
    }
}
