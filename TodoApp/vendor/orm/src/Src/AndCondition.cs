namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__1248;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__1248;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__1254)
        {
            this._condition__1248 = _condition__1254;
        }
    }
}
