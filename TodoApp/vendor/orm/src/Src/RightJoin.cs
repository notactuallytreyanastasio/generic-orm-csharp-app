namespace Orm.Src
{
    public class RightJoin: IJoinType
    {
        public string Keyword()
        {
            return "RIGHT JOIN";
        }
        public RightJoin()
        {
        }
    }
}
