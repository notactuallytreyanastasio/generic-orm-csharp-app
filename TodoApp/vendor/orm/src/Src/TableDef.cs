using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1057;
        readonly G::IReadOnlyList<FieldDef> fields__1058;
        public FieldDef Field(string name__1060)
        {
            FieldDef return__389;
            {
                {
                    G::IReadOnlyList<FieldDef> this__5239 = this.fields__1058;
                    int n__5240 = this__5239.Count;
                    int i__5241 = 0;
                    while (i__5241 < n__5240)
                    {
                        FieldDef el__5242 = this__5239[i__5241];
                        i__5241 = i__5241 + 1;
                        FieldDef f__1062 = el__5242;
                        if (f__1062.Name.SqlValue == name__1060)
                        {
                            return__389 = f__1062;
                            goto fn__1061;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1061:
                {
                }
            }
            return return__389;
        }
        public TableDef(ISafeIdentifier tableName__1064, G::IReadOnlyList<FieldDef> fields__1065)
        {
            this.tableName__1057 = tableName__1064;
            this.fields__1058 = fields__1065;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1057;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1058;
            }
        }
    }
}
