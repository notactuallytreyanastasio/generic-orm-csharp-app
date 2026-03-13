namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1708;
        readonly IFieldType fieldType__1709;
        readonly bool nullable__1710;
        public FieldDef(ISafeIdentifier name__1712, IFieldType fieldType__1713, bool nullable__1714)
        {
            this.name__1708 = name__1712;
            this.fieldType__1709 = fieldType__1713;
            this.nullable__1710 = nullable__1714;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1708;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1709;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1710;
            }
        }
    }
}
