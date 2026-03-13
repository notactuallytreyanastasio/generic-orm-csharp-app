namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__760;
        readonly bool ascending__761;
        public OrderClause(ISafeIdentifier field__763, bool ascending__764)
        {
            this.field__760 = field__763;
            this.ascending__761 = ascending__764;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__760;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__761;
            }
        }
    }
}
