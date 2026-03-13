using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__540, G::IReadOnlyDictionary<string, string> params__541)
        {
            G::IReadOnlyDictionary<string, string> t___6990 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__540, params__541, t___6990, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__401(int c__899)
        {
            bool return__326;
            bool t___3981;
            bool t___3982;
            if (c__899 >= 97)
            {
                t___3981 = c__899 <= 122;
            }
            else
            {
                t___3981 = false;
            }
            if (t___3981)
            {
                return__326 = true;
            }
            else
            {
                if (c__899 >= 65)
                {
                    t___3982 = c__899 <= 90;
                }
                else
                {
                    t___3982 = false;
                }
                if (t___3982)
                {
                    return__326 = true;
                }
                else
                {
                    return__326 = c__899 == 95;
                }
            }
            return return__326;
        }
        internal static bool isIdentPart__402(int c__901)
        {
            bool return__327;
            if (isIdentStart__401(c__901))
            {
                return__327 = true;
            }
            else if (c__901 >= 48)
            {
                return__327 = c__901 <= 57;
            }
            else
            {
                return__327 = false;
            }
            return return__327;
        }
        public static ISafeIdentifier SafeIdentifier(string name__903)
        {
            int t___6988;
            if (string.IsNullOrEmpty(name__903)) throw new S::Exception();
            int idx__905 = 0;
            if (!isIdentStart__401(C::StringUtil.Get(name__903, idx__905))) throw new S::Exception();
            int t___6985 = C::StringUtil.Next(name__903, idx__905);
            idx__905 = t___6985;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__903, idx__905)) break;
                if (!isIdentPart__402(C::StringUtil.Get(name__903, idx__905))) throw new S::Exception();
                t___6988 = C::StringUtil.Next(name__903, idx__905);
                idx__905 = t___6988;
            }
            return new ValidatedIdentifier(name__903);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__630, int id__631)
        {
            SqlBuilder b__633 = new SqlBuilder();
            b__633.AppendSafe("DELETE FROM ");
            b__633.AppendSafe(tableDef__630.TableName.SqlValue);
            b__633.AppendSafe(" WHERE id = ");
            b__633.AppendInt32(id__631);
            return b__633.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__785)
        {
            return new Query(tableName__785, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>());
        }
        public static SqlFragment Col(ISafeIdentifier table__787, ISafeIdentifier column__788)
        {
            SqlBuilder b__790 = new SqlBuilder();
            b__790.AppendSafe(table__787.SqlValue);
            b__790.AppendSafe(".");
            b__790.AppendSafe(column__788.SqlValue);
            return b__790.Accumulated;
        }
    }
}
