namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__655;
        readonly bool ascending__656;
        public OrderClause(ISafeIdentifier field__658, bool ascending__659)
        {
            this.field__655 = field__658;
            this.ascending__656 = ascending__659;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__655;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__656;
            }
        }
    }
}
