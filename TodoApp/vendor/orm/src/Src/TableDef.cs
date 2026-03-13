using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1141;
        readonly G::IReadOnlyList<FieldDef> fields__1142;
        public FieldDef Field(string name__1144)
        {
            FieldDef return__407;
            {
                {
                    G::IReadOnlyList<FieldDef> this__5723 = this.fields__1142;
                    int n__5724 = this__5723.Count;
                    int i__5725 = 0;
                    while (i__5725 < n__5724)
                    {
                        FieldDef el__5726 = this__5723[i__5725];
                        i__5725 = i__5725 + 1;
                        FieldDef f__1146 = el__5726;
                        if (f__1146.Name.SqlValue == name__1144)
                        {
                            return__407 = f__1146;
                            goto fn__1145;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1145:
                {
                }
            }
            return return__407;
        }
        public TableDef(ISafeIdentifier tableName__1148, G::IReadOnlyList<FieldDef> fields__1149)
        {
            this.tableName__1141 = tableName__1148;
            this.fields__1142 = fields__1149;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1141;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1142;
            }
        }
    }
}
