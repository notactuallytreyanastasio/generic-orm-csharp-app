namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__667;
        readonly string message__668;
        public ChangesetError(string field__670, string message__671)
        {
            this.field__667 = field__670;
            this.message__668 = message__671;
        }
        public string Field
        {
            get
            {
                return this.field__667;
            }
        }
        public string Message
        {
            get
            {
                return this.message__668;
            }
        }
    }
}
