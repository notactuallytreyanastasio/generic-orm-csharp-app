using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__444, G::IReadOnlyDictionary<string, string> params__445)
        {
            G::IReadOnlyDictionary<string, string> t___4872 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__444, params__445, t___4872, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__305(int c__635)
        {
            bool return__230;
            bool t___2795;
            bool t___2796;
            if (c__635 >= 97)
            {
                t___2795 = c__635 <= 122;
            }
            else
            {
                t___2795 = false;
            }
            if (t___2795)
            {
                return__230 = true;
            }
            else
            {
                if (c__635 >= 65)
                {
                    t___2796 = c__635 <= 90;
                }
                else
                {
                    t___2796 = false;
                }
                if (t___2796)
                {
                    return__230 = true;
                }
                else
                {
                    return__230 = c__635 == 95;
                }
            }
            return return__230;
        }
        internal static bool isIdentPart__306(int c__637)
        {
            bool return__231;
            if (isIdentStart__305(c__637))
            {
                return__231 = true;
            }
            else if (c__637 >= 48)
            {
                return__231 = c__637 <= 57;
            }
            else
            {
                return__231 = false;
            }
            return return__231;
        }
        public static ISafeIdentifier SafeIdentifier(string name__639)
        {
            int t___4870;
            if (string.IsNullOrEmpty(name__639)) throw new S::Exception();
            int idx__641 = 0;
            if (!isIdentStart__305(C::StringUtil.Get(name__639, idx__641))) throw new S::Exception();
            int t___4867 = C::StringUtil.Next(name__639, idx__641);
            idx__641 = t___4867;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__639, idx__641)) break;
                if (!isIdentPart__306(C::StringUtil.Get(name__639, idx__641))) throw new S::Exception();
                t___4870 = C::StringUtil.Next(name__639, idx__641);
                idx__641 = t___4870;
            }
            return new ValidatedIdentifier(name__639);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__534, int id__535)
        {
            SqlBuilder b__537 = new SqlBuilder();
            b__537.AppendSafe("DELETE FROM ");
            b__537.AppendSafe(tableDef__534.TableName.SqlValue);
            b__537.AppendSafe(" WHERE id = ");
            b__537.AppendInt32(id__535);
            return b__537.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__586)
        {
            return new Query(tableName__586, C::Listed.CreateReadOnlyList<SqlFragment>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null);
        }
    }
}
