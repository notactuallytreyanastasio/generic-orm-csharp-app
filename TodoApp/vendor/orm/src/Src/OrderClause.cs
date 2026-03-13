namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__716;
        readonly bool ascending__717;
        public OrderClause(ISafeIdentifier field__719, bool ascending__720)
        {
            this.field__716 = field__719;
            this.ascending__717 = ascending__720;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__716;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__717;
            }
        }
    }
}
