namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__309;
        readonly string message__310;
        public ChangesetError(string field__312, string message__313)
        {
            this.field__309 = field__312;
            this.message__310 = message__313;
        }
        public string Field
        {
            get
            {
                return this.field__309;
            }
        }
        public string Message
        {
            get
            {
                return this.message__310;
            }
        }
    }
}
