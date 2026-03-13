using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class DeleteQuery
    {
        readonly ISafeIdentifier tableName__1012;
        readonly G::IReadOnlyList<IWhereClause> conditions__1013;
        readonly int ? limitVal__1014;
        public DeleteQuery Where(SqlFragment condition__1016)
        {
            G::IList<IWhereClause> nb__1018 = L::Enumerable.ToList(this.conditions__1013);
            C::Listed.Add(nb__1018, new AndCondition(condition__1016));
            return new DeleteQuery(this.tableName__1012, C::Listed.ToReadOnlyList(nb__1018), this.limitVal__1014);
        }
        public DeleteQuery OrWhere(SqlFragment condition__1020)
        {
            G::IList<IWhereClause> nb__1022 = L::Enumerable.ToList(this.conditions__1013);
            C::Listed.Add(nb__1022, new OrCondition(condition__1020));
            return new DeleteQuery(this.tableName__1012, C::Listed.ToReadOnlyList(nb__1022), this.limitVal__1014);
        }
        public DeleteQuery Limit(int n__1024)
        {
            if (n__1024 < 0) throw new S::Exception();
            return new DeleteQuery(this.tableName__1012, this.conditions__1013, n__1024);
        }
        public SqlFragment ToSql()
        {
            int t___9850;
            if (this.conditions__1013.Count == 0) throw new S::Exception();
            SqlBuilder b__1028 = new SqlBuilder();
            b__1028.AppendSafe("DELETE FROM ");
            b__1028.AppendSafe(this.tableName__1012.SqlValue);
            b__1028.AppendSafe(" WHERE ");
            b__1028.AppendFragment(this.conditions__1013[0].Condition);
            int i__1029 = 1;
            while (true)
            {
                t___9850 = this.conditions__1013.Count;
                if (!(i__1029 < t___9850)) break;
                b__1028.AppendSafe(" ");
                b__1028.AppendSafe(this.conditions__1013[i__1029].Keyword());
                b__1028.AppendSafe(" ");
                b__1028.AppendFragment(this.conditions__1013[i__1029].Condition);
                i__1029 = i__1029 + 1;
            }
            int ? lv__1030 = this.limitVal__1014;
            if (!(lv__1030 == null))
            {
                int lv___1955 = lv__1030.Value;
                b__1028.AppendSafe(" LIMIT ");
                b__1028.AppendInt32(lv___1955);
            }
            return b__1028.Accumulated;
        }
        public DeleteQuery(ISafeIdentifier tableName__1032, G::IReadOnlyList<IWhereClause> conditions__1033, int ? limitVal__1034)
        {
            this.tableName__1012 = tableName__1032;
            this.conditions__1013 = conditions__1033;
            this.limitVal__1014 = limitVal__1034;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1012;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1013;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1014;
            }
        }
    }
}
