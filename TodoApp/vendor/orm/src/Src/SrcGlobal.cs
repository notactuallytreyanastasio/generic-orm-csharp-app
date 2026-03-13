using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__686, G::IReadOnlyDictionary<string, string> params__687)
        {
            G::IReadOnlyDictionary<string, string> t___11221 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__686, params__687, t___11221, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__547(int c__1351)
        {
            bool return__472;
            bool t___6355;
            bool t___6356;
            if (c__1351 >= 97)
            {
                t___6355 = c__1351 <= 122;
            }
            else
            {
                t___6355 = false;
            }
            if (t___6355)
            {
                return__472 = true;
            }
            else
            {
                if (c__1351 >= 65)
                {
                    t___6356 = c__1351 <= 90;
                }
                else
                {
                    t___6356 = false;
                }
                if (t___6356)
                {
                    return__472 = true;
                }
                else
                {
                    return__472 = c__1351 == 95;
                }
            }
            return return__472;
        }
        internal static bool isIdentPart__548(int c__1353)
        {
            bool return__473;
            if (isIdentStart__547(c__1353))
            {
                return__473 = true;
            }
            else if (c__1353 >= 48)
            {
                return__473 = c__1353 <= 57;
            }
            else
            {
                return__473 = false;
            }
            return return__473;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1355)
        {
            int t___11219;
            if (string.IsNullOrEmpty(name__1355)) throw new S::Exception();
            int idx__1357 = 0;
            if (!isIdentStart__547(C::StringUtil.Get(name__1355, idx__1357))) throw new S::Exception();
            int t___11216 = C::StringUtil.Next(name__1355, idx__1357);
            idx__1357 = t___11216;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1355, idx__1357)) break;
                if (!isIdentPart__548(C::StringUtil.Get(name__1355, idx__1357))) throw new S::Exception();
                t___11219 = C::StringUtil.Next(name__1355, idx__1357);
                idx__1357 = t___11219;
            }
            return new ValidatedIdentifier(name__1355);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__776, int id__777)
        {
            SqlBuilder b__779 = new SqlBuilder();
            b__779.AppendSafe("DELETE FROM ");
            b__779.AppendSafe(tableDef__776.TableName.SqlValue);
            b__779.AppendSafe(" WHERE id = ");
            b__779.AppendInt32(id__777);
            return b__779.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__1011)
        {
            return new Query(tableName__1011, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>(), null);
        }
        public static SqlFragment Col(ISafeIdentifier table__1013, ISafeIdentifier column__1014)
        {
            SqlBuilder b__1016 = new SqlBuilder();
            b__1016.AppendSafe(table__1013.SqlValue);
            b__1016.AppendSafe(".");
            b__1016.AppendSafe(column__1014.SqlValue);
            return b__1016.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__1018 = new SqlBuilder();
            b__1018.AppendSafe("COUNT(*)");
            return b__1018.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__1019)
        {
            SqlBuilder b__1021 = new SqlBuilder();
            b__1021.AppendSafe("COUNT(");
            b__1021.AppendSafe(field__1019.SqlValue);
            b__1021.AppendSafe(")");
            return b__1021.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__1022)
        {
            SqlBuilder b__1024 = new SqlBuilder();
            b__1024.AppendSafe("SUM(");
            b__1024.AppendSafe(field__1022.SqlValue);
            b__1024.AppendSafe(")");
            return b__1024.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__1025)
        {
            SqlBuilder b__1027 = new SqlBuilder();
            b__1027.AppendSafe("AVG(");
            b__1027.AppendSafe(field__1025.SqlValue);
            b__1027.AppendSafe(")");
            return b__1027.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__1028)
        {
            SqlBuilder b__1030 = new SqlBuilder();
            b__1030.AppendSafe("MIN(");
            b__1030.AppendSafe(field__1028.SqlValue);
            b__1030.AppendSafe(")");
            return b__1030.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__1031)
        {
            SqlBuilder b__1033 = new SqlBuilder();
            b__1033.AppendSafe("MAX(");
            b__1033.AppendSafe(field__1031.SqlValue);
            b__1033.AppendSafe(")");
            return b__1033.Accumulated;
        }
        public static SqlFragment UnionSql(Query a__1034, Query b__1035)
        {
            SqlBuilder sb__1037 = new SqlBuilder();
            sb__1037.AppendSafe("(");
            sb__1037.AppendFragment(a__1034.ToSql());
            sb__1037.AppendSafe(") UNION (");
            sb__1037.AppendFragment(b__1035.ToSql());
            sb__1037.AppendSafe(")");
            return sb__1037.Accumulated;
        }
        public static SqlFragment UnionAllSql(Query a__1038, Query b__1039)
        {
            SqlBuilder sb__1041 = new SqlBuilder();
            sb__1041.AppendSafe("(");
            sb__1041.AppendFragment(a__1038.ToSql());
            sb__1041.AppendSafe(") UNION ALL (");
            sb__1041.AppendFragment(b__1039.ToSql());
            sb__1041.AppendSafe(")");
            return sb__1041.Accumulated;
        }
        public static SqlFragment IntersectSql(Query a__1042, Query b__1043)
        {
            SqlBuilder sb__1045 = new SqlBuilder();
            sb__1045.AppendSafe("(");
            sb__1045.AppendFragment(a__1042.ToSql());
            sb__1045.AppendSafe(") INTERSECT (");
            sb__1045.AppendFragment(b__1043.ToSql());
            sb__1045.AppendSafe(")");
            return sb__1045.Accumulated;
        }
        public static SqlFragment ExceptSql(Query a__1046, Query b__1047)
        {
            SqlBuilder sb__1049 = new SqlBuilder();
            sb__1049.AppendSafe("(");
            sb__1049.AppendFragment(a__1046.ToSql());
            sb__1049.AppendSafe(") EXCEPT (");
            sb__1049.AppendFragment(b__1047.ToSql());
            sb__1049.AppendSafe(")");
            return sb__1049.Accumulated;
        }
        public static SqlFragment Subquery(Query q__1050, ISafeIdentifier alias__1051)
        {
            SqlBuilder b__1053 = new SqlBuilder();
            b__1053.AppendSafe("(");
            b__1053.AppendFragment(q__1050.ToSql());
            b__1053.AppendSafe(") AS ");
            b__1053.AppendSafe(alias__1051.SqlValue);
            return b__1053.Accumulated;
        }
        public static SqlFragment ExistsSql(Query q__1054)
        {
            SqlBuilder b__1056 = new SqlBuilder();
            b__1056.AppendSafe("EXISTS (");
            b__1056.AppendFragment(q__1054.ToSql());
            b__1056.AppendSafe(")");
            return b__1056.Accumulated;
        }
        public static UpdateQuery Update(ISafeIdentifier tableName__1116)
        {
            return new UpdateQuery(tableName__1116, C::Listed.CreateReadOnlyList<SetClause>(), C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
        public static DeleteQuery DeleteFrom(ISafeIdentifier tableName__1118)
        {
            return new DeleteQuery(tableName__1118, C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
    }
}
