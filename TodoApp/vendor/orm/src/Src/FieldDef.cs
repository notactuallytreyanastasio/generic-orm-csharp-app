namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1267;
        readonly IFieldType fieldType__1268;
        readonly bool nullable__1269;
        public FieldDef(ISafeIdentifier name__1271, IFieldType fieldType__1272, bool nullable__1273)
        {
            this.name__1267 = name__1271;
            this.fieldType__1268 = fieldType__1272;
            this.nullable__1269 = nullable__1273;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1267;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1268;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1269;
            }
        }
    }
}
