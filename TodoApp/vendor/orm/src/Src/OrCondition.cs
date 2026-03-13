namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__838;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__838;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__844)
        {
            this._condition__838 = _condition__844;
        }
    }
}
