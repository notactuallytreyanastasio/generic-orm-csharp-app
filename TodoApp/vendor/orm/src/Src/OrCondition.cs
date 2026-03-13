namespace Orm.Src
{
    public class OrCondition: IWhereClause
    {
        readonly SqlFragment _condition__714;
        public SqlFragment Condition
        {
            get
            {
                return this._condition__714;
            }
        }
        public string Keyword()
        {
            return "OR";
        }
        public OrCondition(SqlFragment _condition__720)
        {
            this._condition__714 = _condition__720;
        }
    }
}
