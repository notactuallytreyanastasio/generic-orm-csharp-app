namespace Orm.Src
{
    public class InnerJoin: IJoinType
    {
        public string Keyword()
        {
            return "INNER JOIN";
        }
        public InnerJoin()
        {
        }
    }
}
