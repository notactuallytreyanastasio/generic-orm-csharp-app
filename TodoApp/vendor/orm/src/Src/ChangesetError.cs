namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__448;
        readonly string message__449;
        public ChangesetError(string field__451, string message__452)
        {
            this.field__448 = field__451;
            this.message__449 = message__452;
        }
        public string Field
        {
            get
            {
                return this.field__448;
            }
        }
        public string Message
        {
            get
            {
                return this.message__449;
            }
        }
    }
}
