namespace Orm.Src
{
    public interface IWhereClause
    {
        SqlFragment Condition
        {
            get;
        }
        string Keyword();
    }
}
