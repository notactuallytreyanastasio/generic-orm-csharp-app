namespace Orm.Src
{
    public class SetClause
    {
        readonly ISafeIdentifier field__1589;
        readonly ISqlPart value__1590;
        public SetClause(ISafeIdentifier field__1592, ISqlPart value__1593)
        {
            this.field__1589 = field__1592;
            this.value__1590 = value__1593;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1589;
            }
        }
        public ISqlPart Value
        {
            get
            {
                return this.value__1590;
            }
        }
    }
}
