namespace Orm.Src
{
    public class OrderClause
    {
        readonly ISafeIdentifier field__1156;
        readonly bool ascending__1157;
        readonly INullsPosition ? nullsPos__1158;
        public OrderClause(ISafeIdentifier field__1160, bool ascending__1161, INullsPosition ? nullsPos__1162)
        {
            this.field__1156 = field__1160;
            this.ascending__1157 = ascending__1161;
            this.nullsPos__1158 = nullsPos__1162;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1156;
            }
        }
        public bool Ascending
        {
            get
            {
                return this.ascending__1157;
            }
        }
        public INullsPosition ? NullsPos
        {
            get
            {
                return this.nullsPos__1158;
            }
        }
    }
}
