namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__698;
        readonly bool ascending__699;
        public OrderClause(ISafeIdentifier field__701, bool ascending__702)
        {
            this.field__698 = field__701;
            this.ascending__699 = ascending__702;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__698;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__699;
            }
        }
    }
}
