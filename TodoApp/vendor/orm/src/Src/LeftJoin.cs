namespace Orm.Src
{
    public class LeftJoin: IJoinType
    {
        public string Keyword()
        {
            return "LEFT JOIN";
        }
        public LeftJoin()
        {
        }
    }
}
