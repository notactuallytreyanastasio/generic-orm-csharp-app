namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__797;
        readonly ISafeIdentifier table__798;
        readonly SqlFragment ? onCondition__799;
        public JoinClause(IJoinType joinType__801, ISafeIdentifier table__802, SqlFragment ? onCondition__803)
        {
            this.joinType__797 = joinType__801;
            this.table__798 = table__802;
            this.onCondition__799 = onCondition__803;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__797;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__798;
            }
        }
        public SqlFragment ? OnCondition
        {
            get
            {
                return this.onCondition__799;
            }
        }
    }
}
