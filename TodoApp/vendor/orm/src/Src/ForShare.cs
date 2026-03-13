namespace Orm.Src
{
    public class ForShare: ILockMode
    {
        public string Keyword()
        {
            return " FOR SHARE";
        }
        public ForShare()
        {
        }
    }
}
