namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__812;
        readonly bool ascending__813;
        readonly INullsPosition ? nullsPos__814;
        public OrderClause(ISafeIdentifier field__816, bool ascending__817, INullsPosition ? nullsPos__818)
        {
            this.field__812 = field__816;
            this.ascending__813 = ascending__817;
            this.nullsPos__814 = nullsPos__818;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__812;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__813;
            }
        }
        public INullsPosition ? NullsPos
        {
            get
            {
                return this.nullsPos__814;
            }
        }
    }
}
