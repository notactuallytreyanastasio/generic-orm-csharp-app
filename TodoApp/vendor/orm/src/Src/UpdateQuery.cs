using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class UpdateQuery
    {
        readonly ISafeIdentifier tableName__1062;
        readonly G::IReadOnlyList<SetClause> setClauses__1063;
        readonly G::IReadOnlyList<IWhereClause> conditions__1064;
        readonly int ? limitVal__1065;
        public UpdateQuery Set(ISafeIdentifier field__1067, ISqlPart value__1068)
        {
            G::IList<SetClause> nb__1070 = L::Enumerable.ToList(this.setClauses__1063);
            C::Listed.Add(nb__1070, new SetClause(field__1067, value__1068));
            return new UpdateQuery(this.tableName__1062, C::Listed.ToReadOnlyList(nb__1070), this.conditions__1064, this.limitVal__1065);
        }
        public UpdateQuery Where(SqlFragment condition__1072)
        {
            G::IList<IWhereClause> nb__1074 = L::Enumerable.ToList(this.conditions__1064);
            C::Listed.Add(nb__1074, new AndCondition(condition__1072));
            return new UpdateQuery(this.tableName__1062, this.setClauses__1063, C::Listed.ToReadOnlyList(nb__1074), this.limitVal__1065);
        }
        public UpdateQuery OrWhere(SqlFragment condition__1076)
        {
            G::IList<IWhereClause> nb__1078 = L::Enumerable.ToList(this.conditions__1064);
            C::Listed.Add(nb__1078, new OrCondition(condition__1076));
            return new UpdateQuery(this.tableName__1062, this.setClauses__1063, C::Listed.ToReadOnlyList(nb__1078), this.limitVal__1065);
        }
        public UpdateQuery Limit(int n__1080)
        {
            if (n__1080 < 0) throw new S::Exception();
            return new UpdateQuery(this.tableName__1062, this.setClauses__1063, this.conditions__1064, n__1080);
        }
        public SqlFragment ToSql()
        {
            int t___10390;
            int t___10404;
            if (this.conditions__1064.Count == 0) throw new S::Exception();
            if (this.setClauses__1063.Count == 0) throw new S::Exception();
            SqlBuilder b__1084 = new SqlBuilder();
            b__1084.AppendSafe("UPDATE ");
            b__1084.AppendSafe(this.tableName__1062.SqlValue);
            b__1084.AppendSafe(" SET ");
            b__1084.AppendSafe(this.setClauses__1063[0].Field.SqlValue);
            b__1084.AppendSafe(" = ");
            b__1084.AppendPart(this.setClauses__1063[0].Value);
            int i__1085 = 1;
            while (true)
            {
                t___10390 = this.setClauses__1063.Count;
                if (!(i__1085 < t___10390)) break;
                b__1084.AppendSafe(", ");
                b__1084.AppendSafe(this.setClauses__1063[i__1085].Field.SqlValue);
                b__1084.AppendSafe(" = ");
                b__1084.AppendPart(this.setClauses__1063[i__1085].Value);
                i__1085 = i__1085 + 1;
            }
            b__1084.AppendSafe(" WHERE ");
            b__1084.AppendFragment(this.conditions__1064[0].Condition);
            int i__1086 = 1;
            while (true)
            {
                t___10404 = this.conditions__1064.Count;
                if (!(i__1086 < t___10404)) break;
                b__1084.AppendSafe(" ");
                b__1084.AppendSafe(this.conditions__1064[i__1086].Keyword());
                b__1084.AppendSafe(" ");
                b__1084.AppendFragment(this.conditions__1064[i__1086].Condition);
                i__1086 = i__1086 + 1;
            }
            int ? lv__1087 = this.limitVal__1065;
            if (!(lv__1087 == null))
            {
                int lv___2075 = lv__1087.Value;
                b__1084.AppendSafe(" LIMIT ");
                b__1084.AppendInt32(lv___2075);
            }
            return b__1084.Accumulated;
        }
        public UpdateQuery(ISafeIdentifier tableName__1089, G::IReadOnlyList<SetClause> setClauses__1090, G::IReadOnlyList<IWhereClause> conditions__1091, int ? limitVal__1092)
        {
            this.tableName__1062 = tableName__1089;
            this.setClauses__1063 = setClauses__1090;
            this.conditions__1064 = conditions__1091;
            this.limitVal__1065 = limitVal__1092;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1062;
            }
        }
        public G::IReadOnlyList<SetClause> SetClauses
        {
            get
            {
                return this.setClauses__1063;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1064;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1065;
            }
        }
    }
}
