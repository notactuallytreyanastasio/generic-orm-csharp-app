using S = System;
using G = System.Collections.Generic;
namespace Orm.Src
{
    public class TableDef
    {
        readonly ISafeIdentifier tableName__1792;
        readonly G::IReadOnlyList<FieldDef> fields__1793;
        readonly ISafeIdentifier ? primaryKey__1794;
        public FieldDef Field(string name__1796)
        {
            FieldDef return__603;
            {
                {
                    G::IReadOnlyList<FieldDef> this__9149 = this.fields__1793;
                    int n__9150 = this__9149.Count;
                    int i__9151 = 0;
                    while (i__9151 < n__9150)
                    {
                        FieldDef el__9152 = this__9149[i__9151];
                        i__9151 = i__9151 + 1;
                        FieldDef f__1798 = el__9152;
                        if (f__1798.Name.SqlValue == name__1796)
                        {
                            return__603 = f__1798;
                            goto fn__1797;
                        }
                    }
                    throw new S::Exception();
                }
                fn__1797:
                {
                }
            }
            return return__603;
        }
        public string PkName()
        {
            string return__604;
            {
                {
                    ISafeIdentifier ? pk__1801 = this.primaryKey__1794;
                    if (!(pk__1801 == null))
                    {
                        ISafeIdentifier pk___2609 = pk__1801!;
                        return__604 = pk___2609.SqlValue;
                        goto fn__1800;
                    }
                    return__604 = "id";
                }
                fn__1800:
                {
                }
            }
            return return__604;
        }
        public TableDef(ISafeIdentifier tableName__1803, G::IReadOnlyList<FieldDef> fields__1804, ISafeIdentifier ? primaryKey__1805)
        {
            this.tableName__1792 = tableName__1803;
            this.fields__1793 = fields__1804;
            this.primaryKey__1794 = primaryKey__1805;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1792;
            }
        }
        public G::IReadOnlyList<FieldDef> Fields
        {
            get
            {
                return this.fields__1793;
            }
        }
        public ISafeIdentifier ? PrimaryKey
        {
            get
            {
                return this.primaryKey__1794;
            }
        }
    }
}
