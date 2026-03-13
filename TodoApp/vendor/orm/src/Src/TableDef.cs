using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__919;
        readonly G::IReadOnlyList<FieldDef> fields__920;
        public FieldDef Field(string name__922)
        {
            FieldDef return__346;
            {
                {
                    G::IReadOnlyList<FieldDef> this__4373 = this.fields__920;
                    int n__4374 = this__4373.Count;
                    int i__4375 = 0;
                    while (i__4375 < n__4374)
                    {
                        FieldDef el__4376 = this__4373[i__4375];
                        i__4375 = i__4375 + 1;
                        FieldDef f__924 = el__4376;
                        if (f__924.Name.SqlValue == name__922)
                        {
                            return__346 = f__924;
                            goto fn__923;
                        }
                    }
                    throw new S::Exception();
                }
                fn__923:
                {
                }
            }
            return return__346;
        }
        public TableDef(ISafeIdentifier tableName__926, G::IReadOnlyList<FieldDef> fields__927)
        {
            this.tableName__919 = tableName__926;
            this.fields__920 = fields__927;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__919;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__920;
            }
        }
    }
}
