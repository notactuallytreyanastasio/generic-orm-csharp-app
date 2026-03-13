namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1134;
        readonly IFieldType fieldType__1135;
        readonly bool nullable__1136;
        public FieldDef(ISafeIdentifier name__1138, IFieldType fieldType__1139, bool nullable__1140)
        {
            this.name__1134 = name__1138;
            this.fieldType__1135 = fieldType__1139;
            this.nullable__1136 = nullable__1140;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1134;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1135;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1136;
            }
        }
    }
}
