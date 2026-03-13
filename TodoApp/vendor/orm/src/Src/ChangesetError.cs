namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__551;
        readonly string message__552;
        public ChangesetError(string field__554, string message__555)
        {
            this.field__551 = field__554;
            this.message__552 = message__555;
        }
        public string Field
        {
            get
            {
                return this.field__551;
            }
        }
        public string Message
        {
            get
            {
                return this.message__552;
            }
        }
    }
}
