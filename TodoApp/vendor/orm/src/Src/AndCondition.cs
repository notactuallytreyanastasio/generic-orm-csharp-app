namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__1356;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__1356;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__1362)
        {
            this._condition__1356 = _condition__1362;
        }
    }
}
