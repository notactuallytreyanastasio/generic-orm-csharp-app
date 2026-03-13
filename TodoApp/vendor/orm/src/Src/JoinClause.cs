namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__648;
        readonly ISafeIdentifier table__649;
        readonly SqlFragment onCondition__650;
        public JoinClause(IJoinType joinType__652, ISafeIdentifier table__653, SqlFragment onCondition__654)
        {
            this.joinType__648 = joinType__652;
            this.table__649 = table__653;
            this.onCondition__650 = onCondition__654;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__648;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__649;
            }
        }
        public SqlFragment OnCondition
        {
            get
            {
                return this.onCondition__650;
            }
        }
    }
}
