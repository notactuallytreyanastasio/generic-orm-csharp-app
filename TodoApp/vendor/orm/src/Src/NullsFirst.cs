namespace Orm.Src
{
    public class NullsFirst: INullsPosition
    {
        public string Keyword()
        {
            return " NULLS FIRST";
        }
        public NullsFirst()
        {
        }
    }
}
