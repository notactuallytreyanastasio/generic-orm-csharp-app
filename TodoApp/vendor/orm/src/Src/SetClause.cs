namespace Orm.Src
{
    public class SetClause
    {
        readonly ISafeIdentifier field__1474;
        readonly ISqlPart value__1475;
        public SetClause(ISafeIdentifier field__1477, ISqlPart value__1478)
        {
            this.field__1474 = field__1477;
            this.value__1475 = value__1478;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1474;
            }
        }
        public ISqlPart Value
        {
            get
            {
                return this.value__1475;
            }
        }
    }
}
