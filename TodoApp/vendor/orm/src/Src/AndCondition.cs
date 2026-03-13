namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__725;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__725;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__731)
        {
            this._condition__725 = _condition__731;
        }
    }
}
