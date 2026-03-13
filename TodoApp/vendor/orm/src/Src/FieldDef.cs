namespace Orm.Src
{
    public class FieldDef
    {
        readonly ISafeIdentifier name__1781;
        readonly IFieldType fieldType__1782;
        readonly bool nullable__1783;
        readonly ISqlPart ? defaultValue__1784;
        readonly bool virtual__1785;
        public FieldDef(ISafeIdentifier name__1787, IFieldType fieldType__1788, bool nullable__1789, ISqlPart ? defaultValue__1790, bool virtual__1791)
        {
            this.name__1781 = name__1787;
            this.fieldType__1782 = fieldType__1788;
            this.nullable__1783 = nullable__1789;
            this.defaultValue__1784 = defaultValue__1790;
            this.virtual__1785 = virtual__1791;
        }
        public ISafeIdentifier Name
        {
            get
            {
                return this.name__1781;
            }
        }
        public IFieldType FieldType
        {
            get
            {
                return this.fieldType__1782;
            }
        }
        public bool Nullable
        {
            get
            {
                return this.nullable__1783;
            }
        }
        public ISqlPart ? DefaultValue
        {
            get
            {
                return this.defaultValue__1784;
            }
        }
        public bool Virtual
        {
            get
            {
                return this.virtual__1785;
            }
        }
    }
}
