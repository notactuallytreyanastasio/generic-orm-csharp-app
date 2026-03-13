namespace Orm.Src
{
    public class NullsLast: INullsPosition
    {
        public string Keyword()
        {
            return " NULLS LAST";
        }
        public NullsLast()
        {
        }
    }
}
