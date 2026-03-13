namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1364;
        readonly IFieldType fieldType__1365;
        readonly bool nullable__1366;
        public FieldDef(ISafeIdentifier name__1368, IFieldType fieldType__1369, bool nullable__1370)
        {
            this.name__1364 = name__1368;
            this.fieldType__1365 = fieldType__1369;
            this.nullable__1366 = nullable__1370;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1364;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1365;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1366;
            }
        }
    }
}
