namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__776;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__776;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__782)
        {
            this._condition__776 = _condition__782;
        }
    }
}
