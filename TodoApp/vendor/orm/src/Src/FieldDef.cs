namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__648;
        readonly IFieldType fieldType__649;
        readonly bool nullable__650;
        public FieldDef(ISafeIdentifier name__652, IFieldType fieldType__653, bool nullable__654)
        {
            this.name__648 = name__652;
            this.fieldType__649 = fieldType__653;
            this.nullable__650 = nullable__654;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__648;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__649;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__650;
            }
        }
    }
}
