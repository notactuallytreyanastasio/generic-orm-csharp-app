using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__763;
        readonly G::IReadOnlyList<FieldDef> fields__764;
        public FieldDef Field(string name__766)
        {
            FieldDef return__290;
            {
                {
                    G::IReadOnlyList<FieldDef> this__3570 = this.fields__764;
                    int n__3571 = this__3570.Count;
                    int i__3572 = 0;
                    while (i__3572 < n__3571)
                    {
                        FieldDef el__3573 = this__3570[i__3572];
                        i__3572 = i__3572 + 1;
                        FieldDef f__768 = el__3573;
                        if (f__768.Name.SqlValue == name__766)
                        {
                            return__290 = f__768;
                            goto fn__767;
                        }
                    }
                    throw new S::Exception();
                }
                fn__767:
                {
                }
            }
            return return__290;
        }
        public TableDef(ISafeIdentifier tableName__770, G::IReadOnlyList<FieldDef> fields__771)
        {
            this.tableName__763 = tableName__770;
            this.fields__764 = fields__771;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__763;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__764;
            }
        }
    }
}
