namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__691;
        readonly ISafeIdentifier table__692;
        readonly SqlFragment onCondition__693;
        public JoinClause(IJoinType joinType__695, ISafeIdentifier table__696, SqlFragment onCondition__697)
        {
            this.joinType__691 = joinType__695;
            this.table__692 = table__696;
            this.onCondition__693 = onCondition__697;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__691;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__692;
            }
        }
        public SqlFragment OnCondition
        {
            get
            {
                return this.onCondition__693;
            }
        }
    }
}
