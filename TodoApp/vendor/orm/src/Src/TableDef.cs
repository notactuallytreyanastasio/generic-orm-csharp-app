using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1274;
        readonly G::IReadOnlyList<FieldDef> fields__1275;
        public FieldDef Field(string name__1277)
        {
            FieldDef return__451;
            {
                {
                    G::IReadOnlyList<FieldDef> this__6482 = this.fields__1275;
                    int n__6483 = this__6482.Count;
                    int i__6484 = 0;
                    while (i__6484 < n__6483)
                    {
                        FieldDef el__6485 = this__6482[i__6484];
                        i__6484 = i__6484 + 1;
                        FieldDef f__1279 = el__6485;
                        if (f__1279.Name.SqlValue == name__1277)
                        {
                            return__451 = f__1279;
                            goto fn__1278;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1278:
                {
                }
            }
            return return__451;
        }
        public TableDef(ISafeIdentifier tableName__1281, G::IReadOnlyList<FieldDef> fields__1282)
        {
            this.tableName__1274 = tableName__1281;
            this.fields__1275 = fields__1282;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1274;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1275;
            }
        }
    }
}
