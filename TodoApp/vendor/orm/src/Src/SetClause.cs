namespace Orm.Src
{
    public class SetClause
    {
        readonly ISafeIdentifier field__1401;
        readonly ISqlPart value__1402;
        public SetClause(ISafeIdentifier field__1404, ISqlPart value__1405)
        {
            this.field__1401 = field__1404;
            this.value__1402 = value__1405;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__1401;
            }
        }
        public ISqlPart Value
        {
            get
            {
                return this.value__1402;
            }
        }
    }
}
