namespace Orm.Src
{
    public class AndCondition: IWhereClause
    {
        readonly SqlFragment _condition__1175;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__1175;
            }
        }
        public string Keyword()
        {
            return "AND";
        }
        public AndCondition(SqlFragment _condition__1181)
        {
            this._condition__1175 = _condition__1181;
        }
    }
}
