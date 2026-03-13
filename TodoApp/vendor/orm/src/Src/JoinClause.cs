namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__1141;
        readonly ISafeIdentifier table__1142;
        readonly SqlFragment ? onCondition__1143;
        public JoinClause(IJoinType joinType__1145, ISafeIdentifier table__1146, SqlFragment ? onCondition__1147)
        {
            this.joinType__1141 = joinType__1145;
            this.table__1142 = table__1146;
            this.onCondition__1143 = onCondition__1147;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__1141;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__1142;
            }
        }
        public SqlFragment ? OnCondition
        {
            get
            {
                return this.onCondition__1143;
            }
        }
    }
}
