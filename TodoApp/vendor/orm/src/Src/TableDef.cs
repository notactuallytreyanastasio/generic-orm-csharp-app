using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1715;
        readonly G::IReadOnlyList<FieldDef> fields__1716;
        public FieldDef Field(string name__1718)
        {
            FieldDef return__584;
            {
                {
                    G::IReadOnlyList<FieldDef> this__8420 = this.fields__1716;
                    int n__8421 = this__8420.Count;
                    int i__8422 = 0;
                    while (i__8422 < n__8421)
                    {
                        FieldDef el__8423 = this__8420[i__8422];
                        i__8422 = i__8422 + 1;
                        FieldDef f__1720 = el__8423;
                        if (f__1720.Name.SqlValue == name__1718)
                        {
                            return__584 = f__1720;
                            goto fn__1719;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1719:
                {
                }
            }
            return return__584;
        }
        public TableDef(ISafeIdentifier tableName__1722, G::IReadOnlyList<FieldDef> fields__1723)
        {
            this.tableName__1715 = tableName__1722;
            this.fields__1716 = fields__1723;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1715;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1716;
            }
        }
    }
}
