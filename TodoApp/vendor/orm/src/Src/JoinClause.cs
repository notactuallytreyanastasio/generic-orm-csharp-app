namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__709;
        readonly ISafeIdentifier table__710;
        readonly SqlFragment onCondition__711;
        public JoinClause(IJoinType joinType__713, ISafeIdentifier table__714, SqlFragment onCondition__715)
        {
            this.joinType__709 = joinType__713;
            this.table__710 = table__714;
            this.onCondition__711 = onCondition__715;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__709;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__710;
            }
        }
        public SqlFragment OnCondition
        {
            get
            {
                return this.onCondition__711;
            }
        }
    }
}
