namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__912;
        readonly IFieldType fieldType__913;
        readonly bool nullable__914;
        public FieldDef(ISafeIdentifier name__916, IFieldType fieldType__917, bool nullable__918)
        {
            this.name__912 = name__916;
            this.fieldType__913 = fieldType__917;
            this.nullable__914 = nullable__918;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__912;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__913;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__914;
            }
        }
    }
}
