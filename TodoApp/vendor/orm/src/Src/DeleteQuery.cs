using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class DeleteQuery
    {
        readonly ISafeIdentifier tableName__1093;
        readonly G::IReadOnlyList<IWhereClause> conditions__1094;
        readonly int ? limitVal__1095;
        public DeleteQuery Where(SqlFragment condition__1097)
        {
            G::IList<IWhereClause> nb__1099 = L::Enumerable.ToList(this.conditions__1094);
            C::Listed.Add(nb__1099, new AndCondition(condition__1097));
            return new DeleteQuery(this.tableName__1093, C::Listed.ToReadOnlyList(nb__1099), this.limitVal__1095);
        }
        public DeleteQuery OrWhere(SqlFragment condition__1101)
        {
            G::IList<IWhereClause> nb__1103 = L::Enumerable.ToList(this.conditions__1094);
            C::Listed.Add(nb__1103, new OrCondition(condition__1101));
            return new DeleteQuery(this.tableName__1093, C::Listed.ToReadOnlyList(nb__1103), this.limitVal__1095);
        }
        public DeleteQuery Limit(int n__1105)
        {
            if (n__1105 < 0) throw new S::Exception();
            return new DeleteQuery(this.tableName__1093, this.conditions__1094, n__1105);
        }
        public SqlFragment ToSql()
        {
            int t___10350;
            if (this.conditions__1094.Count == 0) throw new S::Exception();
            SqlBuilder b__1109 = new SqlBuilder();
            b__1109.AppendSafe("DELETE FROM ");
            b__1109.AppendSafe(this.tableName__1093.SqlValue);
            b__1109.AppendSafe(" WHERE ");
            b__1109.AppendFragment(this.conditions__1094[0].Condition);
            int i__1110 = 1;
            while (true)
            {
                t___10350 = this.conditions__1094.Count;
                if (!(i__1110 < t___10350)) break;
                b__1109.AppendSafe(" ");
                b__1109.AppendSafe(this.conditions__1094[i__1110].Keyword());
                b__1109.AppendSafe(" ");
                b__1109.AppendFragment(this.conditions__1094[i__1110].Condition);
                i__1110 = i__1110 + 1;
            }
            int ? lv__1111 = this.limitVal__1095;
            if (!(lv__1111 == null))
            {
                int lv___2076 = lv__1111.Value;
                b__1109.AppendSafe(" LIMIT ");
                b__1109.AppendInt32(lv___2076);
            }
            return b__1109.Accumulated;
        }
        public DeleteQuery(ISafeIdentifier tableName__1113, G::IReadOnlyList<IWhereClause> conditions__1114, int ? limitVal__1115)
        {
            this.tableName__1093 = tableName__1113;
            this.conditions__1094 = conditions__1114;
            this.limitVal__1095 = limitVal__1115;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1093;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1094;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1095;
            }
        }
    }
}
