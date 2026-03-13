namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1050;
        readonly IFieldType fieldType__1051;
        readonly bool nullable__1052;
        public FieldDef(ISafeIdentifier name__1054, IFieldType fieldType__1055, bool nullable__1056)
        {
            this.name__1050 = name__1054;
            this.fieldType__1051 = fieldType__1055;
            this.nullable__1052 = nullable__1056;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1050;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1051;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1052;
            }
        }
    }
}
