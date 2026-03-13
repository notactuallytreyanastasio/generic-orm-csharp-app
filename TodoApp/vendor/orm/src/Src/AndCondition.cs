namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__664;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__664;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__670)
        {
            this._condition__664 = _condition__670;
        }
    }
}
