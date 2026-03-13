namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__671;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__671;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__677)
        {
            this._condition__671 = _condition__677;
        }
    }
}
