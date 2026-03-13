namespace Orm.Src
{
    public class SetClause
    {
        readonly ISafeIdentifier field__976;
        readonly ISqlPart value__977;
        public SetClause(ISafeIdentifier field__979, ISqlPart value__980)
        {
            this.field__976 = field__979;
            this.value__977 = value__980;
        }
        public ISafeIdentifier Field
        {
            get
            {
                return this.field__976;
            }
        }
        public ISqlPart Value
        {
            get
            {
                return this.value__977;
            }
        }
    }
}
