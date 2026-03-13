namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__769;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__769;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__775)
        {
            this._condition__769 = _condition__775;
        }
    }
}
