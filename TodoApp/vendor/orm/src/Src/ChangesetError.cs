namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__510;
        readonly string message__511;
        public ChangesetError(string field__513, string message__514)
        {
            this.field__510 = field__513;
            this.message__511 = message__514;
        }
        public string Field
        {
            get
            {
                return this.field__510;
            }
        }
        public string Message
        {
            get
            {
                return this.message__511;
            }
        }
    }
}
