namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__831;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__831;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__837)
        {
            this._condition__831 = _condition__837;
        }
    }
}
