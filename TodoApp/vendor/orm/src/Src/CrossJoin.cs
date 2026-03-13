namespace Orm.Src
{
    public class CrossJoin: IJoinType
    {
        public string Keyword()
        {
            return "CROSS JOIN";
        }
        public CrossJoin()
        {
        }
    }
}
