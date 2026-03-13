namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__1214;
        readonly ISafeIdentifier table__1215;
        readonly SqlFragment ? onCondition__1216;
        public JoinClause(IJoinType joinType__1218, ISafeIdentifier table__1219, SqlFragment ? onCondition__1220)
        {
            this.joinType__1214 = joinType__1218;
            this.table__1215 = table__1219;
            this.onCondition__1216 = onCondition__1220;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__1214;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__1215;
            }
        }
        public SqlFragment ? OnCondition
        {
            get
            {
                return this.onCondition__1216;
            }
        }
    }
}
