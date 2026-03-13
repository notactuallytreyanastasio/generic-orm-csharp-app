namespace Orm.Src
{
    public class FullJoin: IJoinType
    {
        public string Keyword()
        {
            return "FULL OUTER JOIN";
        }
        public FullJoin()
        {
        }
    }
}
