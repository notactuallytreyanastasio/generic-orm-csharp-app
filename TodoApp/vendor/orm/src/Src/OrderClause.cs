namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__599;
        readonly bool ascending__600;
        public OrderClause(ISafeIdentifier field__602, bool ascending__603)
        {
            this.field__599 = field__602;
            this.ascending__600 = ascending__603;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__599;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__600;
            }
        }
    }
}
