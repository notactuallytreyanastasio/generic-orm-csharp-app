namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__732;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__732;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__738)
        {
            this._condition__732 = _condition__738;
        }
    }
}
