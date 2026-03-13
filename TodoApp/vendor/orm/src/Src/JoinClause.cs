namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__753;
        readonly ISafeIdentifier table__754;
        readonly SqlFragment onCondition__755;
        public JoinClause(IJoinType joinType__757, ISafeIdentifier table__758, SqlFragment onCondition__759)
        {
            this.joinType__753 = joinType__757;
            this.table__754 = table__758;
            this.onCondition__755 = onCondition__759;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__753;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__754;
            }
        }
        public SqlFragment OnCondition
        {
            get
            {
                return this.onCondition__755;
            }
        }
    }
}
