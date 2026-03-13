namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__349;
        readonly string message__350;
        public ChangesetError(string field__352, string message__353)
        {
            this.field__349 = field__352;
            this.message__350 = message__353;
        }
        public string Field
        {
            get
            {
                return this.field__349;
            }
        }
        public string Message
        {
            get
            {
                return this.message__350;
            }
        }
    }
}
