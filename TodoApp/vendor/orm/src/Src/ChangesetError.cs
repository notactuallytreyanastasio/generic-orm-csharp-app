namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__643;
        readonly string message__644;
        public ChangesetError(string field__646, string message__647)
        {
            this.field__643 = field__646;
            this.message__644 = message__647;
        }
        public string Field
        {
            get
            {
                return this.field__643;
            }
        }
        public string Message
        {
            get
            {
                return this.message__644;
            }
        }
    }
}
