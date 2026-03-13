namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__538;
        readonly bool ascending__539;
        public OrderClause(ISafeIdentifier field__541, bool ascending__542)
        {
            this.field__538 = field__541;
            this.ascending__539 = ascending__542;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__538;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__539;
            }
        }
    }
}
