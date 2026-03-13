using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1930;
        readonly G::IReadOnlyList<FieldDef> fields__1931;
        readonly ISafeIdentifier ? primaryKey__1932;
        public FieldDef Field(string name__1934)
        {
            FieldDef return__646;
            {
                {
                    G::IReadOnlyList<FieldDef> this__10148 = this.fields__1931;
                    int n__10149 = this__10148.Count;
                    int i__10150 = 0;
                    while (i__10150 < n__10149)
                    {
                        FieldDef el__10151 = this__10148[i__10150];
                        i__10150 = i__10150 + 1;
                        FieldDef f__1936 = el__10151;
                        if (f__1936.Name.SqlValue == name__1934)
                        {
                            return__646 = f__1936;
                            goto fn__1935;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1935:
                {
                }
            }
            return return__646;
        }
        public string PkName()
        {
            string return__647;
            {
                {
                    ISafeIdentifier ? pk__1939 = this.primaryKey__1932;
                    if (!(pk__1939 == null))
                    {
                        ISafeIdentifier pk___2830 = pk__1939!;
                        return__647 = pk___2830.SqlValue;
                        goto fn__1938;
                    }
                    return__647 = "id";
                }
                fn__1938:
                {
                }
            }
            return return__647;
        }
        public TableDef(ISafeIdentifier tableName__1941, G::IReadOnlyList<FieldDef> fields__1942, ISafeIdentifier ? primaryKey__1943)
        {
            this.tableName__1930 = tableName__1941;
            this.fields__1931 = fields__1942;
            this.primaryKey__1932 = primaryKey__1943;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1930;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1931;
            }
        }
        public ISafeIdentifier ? PrimaryKey
        {
            get
            {
                return this.primaryKey__1932;
            }
        }
    }
}
