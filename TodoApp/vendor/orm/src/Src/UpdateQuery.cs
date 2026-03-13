using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class UpdateQuery
    {
        readonly ISafeIdentifier tableName__981;
        readonly G::IReadOnlyList<SetClause> setClauses__982;
        readonly G::IReadOnlyList<IWhereClause> conditions__983;
        readonly int ? limitVal__984;
        public UpdateQuery Set(ISafeIdentifier field__986, ISqlPart value__987)
        {
            G::IList<SetClause> nb__989 = L::Enumerable.ToList(this.setClauses__982);
            C::Listed.Add(nb__989, new SetClause(field__986, value__987));
            return new UpdateQuery(this.tableName__981, C::Listed.ToReadOnlyList(nb__989), this.conditions__983, this.limitVal__984);
        }
        public UpdateQuery Where(SqlFragment condition__991)
        {
            G::IList<IWhereClause> nb__993 = L::Enumerable.ToList(this.conditions__983);
            C::Listed.Add(nb__993, new AndCondition(condition__991));
            return new UpdateQuery(this.tableName__981, this.setClauses__982, C::Listed.ToReadOnlyList(nb__993), this.limitVal__984);
        }
        public UpdateQuery OrWhere(SqlFragment condition__995)
        {
            G::IList<IWhereClause> nb__997 = L::Enumerable.ToList(this.conditions__983);
            C::Listed.Add(nb__997, new OrCondition(condition__995));
            return new UpdateQuery(this.tableName__981, this.setClauses__982, C::Listed.ToReadOnlyList(nb__997), this.limitVal__984);
        }
        public UpdateQuery Limit(int n__999)
        {
            if (n__999 < 0) throw new S::Exception();
            return new UpdateQuery(this.tableName__981, this.setClauses__982, this.conditions__983, n__999);
        }
        public SqlFragment ToSql()
        {
            int t___9890;
            int t___9904;
            if (this.conditions__983.Count == 0) throw new S::Exception();
            if (this.setClauses__982.Count == 0) throw new S::Exception();
            SqlBuilder b__1003 = new SqlBuilder();
            b__1003.AppendSafe("UPDATE ");
            b__1003.AppendSafe(this.tableName__981.SqlValue);
            b__1003.AppendSafe(" SET ");
            b__1003.AppendSafe(this.setClauses__982[0].Field.SqlValue);
            b__1003.AppendSafe(" = ");
            b__1003.AppendPart(this.setClauses__982[0].Value);
            int i__1004 = 1;
            while (true)
            {
                t___9890 = this.setClauses__982.Count;
                if (!(i__1004 < t___9890)) break;
                b__1003.AppendSafe(", ");
                b__1003.AppendSafe(this.setClauses__982[i__1004].Field.SqlValue);
                b__1003.AppendSafe(" = ");
                b__1003.AppendPart(this.setClauses__982[i__1004].Value);
                i__1004 = i__1004 + 1;
            }
            b__1003.AppendSafe(" WHERE ");
            b__1003.AppendFragment(this.conditions__983[0].Condition);
            int i__1005 = 1;
            while (true)
            {
                t___9904 = this.conditions__983.Count;
                if (!(i__1005 < t___9904)) break;
                b__1003.AppendSafe(" ");
                b__1003.AppendSafe(this.conditions__983[i__1005].Keyword());
                b__1003.AppendSafe(" ");
                b__1003.AppendFragment(this.conditions__983[i__1005].Condition);
                i__1005 = i__1005 + 1;
            }
            int ? lv__1006 = this.limitVal__984;
            if (!(lv__1006 == null))
            {
                int lv___1954 = lv__1006.Value;
                b__1003.AppendSafe(" LIMIT ");
                b__1003.AppendInt32(lv___1954);
            }
            return b__1003.Accumulated;
        }
        public UpdateQuery(ISafeIdentifier tableName__1008, G::IReadOnlyList<SetClause> setClauses__1009, G::IReadOnlyList<IWhereClause> conditions__1010, int ? limitVal__1011)
        {
            this.tableName__981 = tableName__1008;
            this.setClauses__982 = setClauses__1009;
            this.conditions__983 = conditions__1010;
            this.limitVal__984 = limitVal__1011;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__981;
            }
        }
        public G::IReadOnlyList<SetClause> SetClauses
        {
            get
            {
                return this.setClauses__982;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__983;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__984;
            }
        }
    }
}
