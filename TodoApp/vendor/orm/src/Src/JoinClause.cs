namespace Orm.Src
{
    public class JoinClause
    {
        readonly IJoinType joinType__1322;
        readonly ISafeIdentifier table__1323;
        readonly SqlFragment ? onCondition__1324;
        public JoinClause(IJoinType joinType__1326, ISafeIdentifier table__1327, SqlFragment ? onCondition__1328)
        {
            this.joinType__1322 = joinType__1326;
            this.table__1323 = table__1327;
            this.onCondition__1324 = onCondition__1328;
        }
        public IJoinType JoinType
        {
            get
            {
                return this.joinType__1322;
            }
        }
        public ISafeIdentifier Table
        {
            get
            {
                return this.table__1323;
            }
        }
        public SqlFragment ? OnCondition
        {
            get
            {
                return this.onCondition__1324;
            }
        }
    }
}
