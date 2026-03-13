namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__1182;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__1182;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__1188)
        {
            this._condition__1182 = _condition__1188;
        }
    }
}
