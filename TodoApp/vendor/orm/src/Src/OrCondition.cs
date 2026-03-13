namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__1363;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__1363;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__1369)
        {
            this._condition__1363 = _condition__1369;
        }
    }
}
