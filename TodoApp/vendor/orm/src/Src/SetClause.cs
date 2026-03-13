namespace Orm.Src
{
    public class SetClause
    {
        readonly ISafeIdentifier field__1057;
        readonly ISqlPart value__1058;
        public SetClause(ISafeIdentifier field__1060, ISqlPart value__1061)
        {
            this.field__1057 = field__1060;
            this.value__1058 = value__1061;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1057;
            }
        }
        public ISqlPart Value
        {
            get
            {
                return this.value__1058;
            }
        }
    }
}
