namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__756;
        readonly IFieldType fieldType__757;
        readonly bool nullable__758;
        public FieldDef(ISafeIdentifier name__760, IFieldType fieldType__761, bool nullable__762)
        {
            this.name__756 = name__760;
            this.fieldType__757 = fieldType__761;
            this.nullable__758 = nullable__762;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__756;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__757;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__758;
            }
        }
    }
}
