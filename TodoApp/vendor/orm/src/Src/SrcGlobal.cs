using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__484, G::IReadOnlyDictionary<string, string> params__485)
        {
            G::IReadOnlyDictionary<string, string> t___5558 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__484, params__485, t___5558, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__345(int c__743)
        {
            bool return__270;
            bool t___3200;
            bool t___3201;
            if (c__743 >= 97)
            {
                t___3200 = c__743 <= 122;
            }
            else
            {
                t___3200 = false;
            }
            if (t___3200)
            {
                return__270 = true;
            }
            else
            {
                if (c__743 >= 65)
                {
                    t___3201 = c__743 <= 90;
                }
                else
                {
                    t___3201 = false;
                }
                if (t___3201)
                {
                    return__270 = true;
                }
                else
                {
                    return__270 = c__743 == 95;
                }
            }
            return return__270;
        }
        internal static bool isIdentPart__346(int c__745)
        {
            bool return__271;
            if (isIdentStart__345(c__745))
            {
                return__271 = true;
            }
            else if (c__745 >= 48)
            {
                return__271 = c__745 <= 57;
            }
            else
            {
                return__271 = false;
            }
            return return__271;
        }
        public static ISafeIdentifier SafeIdentifier(string name__747)
        {
            int t___5556;
            if (string.IsNullOrEmpty(name__747)) throw new S::Exception();
            int idx__749 = 0;
            if (!isIdentStart__345(C::StringUtil.Get(name__747, idx__749))) throw new S::Exception();
            int t___5553 = C::StringUtil.Next(name__747, idx__749);
            idx__749 = t___5553;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__747, idx__749)) break;
                if (!isIdentPart__346(C::StringUtil.Get(name__747, idx__749))) throw new S::Exception();
                t___5556 = C::StringUtil.Next(name__747, idx__749);
                idx__749 = t___5556;
            }
            return new ValidatedIdentifier(name__747);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__574, int id__575)
        {
            SqlBuilder b__577 = new SqlBuilder();
            b__577.AppendSafe("DELETE FROM ");
            b__577.AppendSafe(tableDef__574.TableName.SqlValue);
            b__577.AppendSafe(" WHERE id = ");
            b__577.AppendInt32(id__575);
            return b__577.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__672)
        {
            return new Query(tableName__672, C::Listed.CreateReadOnlyList<SqlFragment>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>());
        }
        public static SqlFragment Col(ISafeIdentifier table__674, ISafeIdentifier column__675)
        {
            SqlBuilder b__677 = new SqlBuilder();
            b__677.AppendSafe(table__674.SqlValue);
            b__677.AppendSafe(".");
            b__677.AppendSafe(column__675.SqlValue);
            return b__677.Accumulated;
        }
    }
}
