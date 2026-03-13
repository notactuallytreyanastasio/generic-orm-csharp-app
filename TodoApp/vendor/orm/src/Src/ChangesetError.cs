namespace Orm.Src
{
    public class ChangesetError
    {
        readonly string field__466;
        readonly string message__467;
        public ChangesetError(string field__469, string message__470)
        {
            this.field__466 = field__469;
            this.message__467 = message__470;
        }
        public string Field
        {
            get
            {
                return this.field__466;
            }
        }
        public string Message
        {
            get
            {
                return this.message__467;
            }
        }
    }
}
