namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__405;
        readonly string message__406;
        public ChangesetError(string field__408, string message__409)
        {
            this.field__405 = field__408;
            this.message__406 = message__409;
        }
        public string Field
        {
            get
            {
                return this.field__405;
            }
        }
        public string Message
        {
            get
            {
                return this.message__406;
            }
        }
    }
}
