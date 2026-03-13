namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__1337;
        readonly bool ascending__1338;
        readonly INullsPosition ? nullsPos__1339;
        public OrderClause(ISafeIdentifier field__1341, bool ascending__1342, INullsPosition ? nullsPos__1343)
        {
            this.field__1337 = field__1341;
            this.ascending__1338 = ascending__1342;
            this.nullsPos__1339 = nullsPos__1343;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1337;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__1338;
            }
        }
        public INullsPosition ? NullsPos
        {
            get
            {
                return this.nullsPos__1339;
            }
        }
    }
}
