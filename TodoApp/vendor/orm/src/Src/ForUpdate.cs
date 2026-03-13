namespace Orm.Src
{
    public class ForUpdate: ILockMode
    {
        public string Keyword()
        {
            return " FOR UPDATE";
        }
        public ForUpdate()
        {
        }
    }
}
