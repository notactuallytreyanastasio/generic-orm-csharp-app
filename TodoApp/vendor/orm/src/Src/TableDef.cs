using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1371;
        readonly G::IReadOnlyList<FieldDef> fields__1372;
        public FieldDef Field(string name__1374)
        {
            FieldDef return__492;
            {
                {
                    G::IReadOnlyList<FieldDef> this__6801 = this.fields__1372;
                    int n__6802 = this__6801.Count;
                    int i__6803 = 0;
                    while (i__6803 < n__6802)
                    {
                        FieldDef el__6804 = this__6801[i__6803];
                        i__6803 = i__6803 + 1;
                        FieldDef f__1376 = el__6804;
                        if (f__1376.Name.SqlValue == name__1374)
                        {
                            return__492 = f__1376;
                            goto fn__1375;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1375:
                {
                }
            }
            return return__492;
        }
        public TableDef(ISafeIdentifier tableName__1378, G::IReadOnlyList<FieldDef> fields__1379)
        {
            this.tableName__1371 = tableName__1378;
            this.fields__1372 = fields__1379;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1371;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1372;
            }
        }
    }
}
