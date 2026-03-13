namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__707;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__707;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__713)
        {
            this._condition__707 = _condition__713;
        }
    }
}
