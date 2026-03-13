namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__592;
        readonly ISafeIdentifier table__593;
        readonly SqlFragment onCondition__594;
        public JoinClause(IJoinType joinType__596, ISafeIdentifier table__597, SqlFragment onCondition__598)
        {
            this.joinType__592 = joinType__596;
            this.table__593 = table__597;
            this.onCondition__594 = onCondition__598;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__592;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__593;
            }
        }
        public SqlFragment OnCondition
        {
            get
            {
                return this.onCondition__594;
            }
        }
    }
}
