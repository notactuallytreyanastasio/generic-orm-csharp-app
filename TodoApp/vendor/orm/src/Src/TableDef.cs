using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__655;
        readonly G::IReadOnlyList<FieldDef> fields__656;
        public FieldDef Field(string name__658)
        {
            FieldDef return__250;
            {
                {
                    G::IReadOnlyList<FieldDef> this__3156 = this.fields__656;
                    int n__3157 = this__3156.Count;
                    int i__3158 = 0;
                    while (i__3158 < n__3157)
                    {
                        FieldDef el__3159 = this__3156[i__3158];
                        i__3158 = i__3158 + 1;
                        FieldDef f__660 = el__3159;
                        if (f__660.Name.SqlValue == name__658)
                        {
                            return__250 = f__660;
                            goto fn__659;
                        }
                    }
                    throw new S::Exception();
                }
                fn__659:
                {
                }
            }
            return return__250;
        }
        public TableDef(ISafeIdentifier tableName__662, G::IReadOnlyList<FieldDef> fields__663)
        {
            this.tableName__655 = tableName__662;
            this.fields__656 = fields__663;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__655;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__656;
            }
        }
    }
}
