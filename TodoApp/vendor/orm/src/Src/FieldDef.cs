namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1919;
        readonly IFieldType fieldType__1920;
        readonly bool nullable__1921;
        readonly ISqlPart ? defaultValue__1922;
        readonly bool virtual__1923;
        public FieldDef(ISafeIdentifier name__1925, IFieldType fieldType__1926, bool nullable__1927, ISqlPart ? defaultValue__1928, bool virtual__1929)
        {
            this.name__1919 = name__1925;
            this.fieldType__1920 = fieldType__1926;
            this.nullable__1921 = nullable__1927;
            this.defaultValue__1922 = defaultValue__1928;
            this.virtual__1923 = virtual__1929;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1919;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1920;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1921;
            }
        }
        public ISqlPart ? DefaultValue
        {
            get
            {
                return this.defaultValue__1922;
            }
        }
        public bool Virtual
        {
            get
            {
                return this.virtual__1923;
            }
        }
    }
}
