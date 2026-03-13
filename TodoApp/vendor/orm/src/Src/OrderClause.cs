namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__1229;
        readonly bool ascending__1230;
        readonly INullsPosition ? nullsPos__1231;
        public OrderClause(ISafeIdentifier field__1233, bool ascending__1234, INullsPosition ? nullsPos__1235)
        {
            this.field__1229 = field__1233;
            this.ascending__1230 = ascending__1234;
            this.nullsPos__1231 = nullsPos__1235;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1229;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__1230;
            }
        }
        public INullsPosition ? NullsPos
        {
            get
            {
                return this.nullsPos__1231;
            }
        }
    }
}
