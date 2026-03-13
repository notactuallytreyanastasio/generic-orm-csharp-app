namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__1255;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__1255;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__1261)
        {
            this._condition__1255 = _condition__1261;
        }
    }
}
