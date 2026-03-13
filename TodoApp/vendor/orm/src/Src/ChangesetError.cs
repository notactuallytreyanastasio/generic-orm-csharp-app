namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__714;
        readonly string message__715;
        public ChangesetError(string field__717, string message__718)
        {
            this.field__714 = field__717;
            this.message__715 = message__718;
        }
        public string Field
        {
            get
            {
                return this.field__714;
            }
        }
        public string Message
        {
            get
            {
                return this.message__715;
            }
        }
    }
}
