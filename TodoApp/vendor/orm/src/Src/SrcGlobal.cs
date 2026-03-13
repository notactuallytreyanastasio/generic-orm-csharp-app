using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__921, G::IReadOnlyDictionary<string, string> params__922)
        {
            G::IReadOnlyDictionary<string, string> t___13867 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__921, params__922, t___13867, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__639(int c__1695)
        {
            bool return__564;
            bool t___7693;
            bool t___7694;
            if (c__1695 >= 97)
            {
                t___7693 = c__1695 <= 122;
            }
            else
            {
                t___7693 = false;
            }
            if (t___7693)
            {
                return__564 = true;
            }
            else
            {
                if (c__1695 >= 65)
                {
                    t___7694 = c__1695 <= 90;
                }
                else
                {
                    t___7694 = false;
                }
                if (t___7694)
                {
                    return__564 = true;
                }
                else
                {
                    return__564 = c__1695 == 95;
                }
            }
            return return__564;
        }
        internal static bool isIdentPart__640(int c__1697)
        {
            bool return__565;
            if (isIdentStart__639(c__1697))
            {
                return__565 = true;
            }
            else if (c__1697 >= 48)
            {
                return__565 = c__1697 <= 57;
            }
            else
            {
                return__565 = false;
            }
            return return__565;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1699)
        {
            int t___13865;
            if (string.IsNullOrEmpty(name__1699)) throw new S::Exception();
            int idx__1701 = 0;
            if (!isIdentStart__639(C::StringUtil.Get(name__1699, idx__1701))) throw new S::Exception();
            int t___13862 = C::StringUtil.Next(name__1699, idx__1701);
            idx__1701 = t___13862;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1699, idx__1701)) break;
                if (!isIdentPart__640(C::StringUtil.Get(name__1699, idx__1701))) throw new S::Exception();
                t___13865 = C::StringUtil.Next(name__1699, idx__1701);
                idx__1701 = t___13865;
            }
            return new ValidatedIdentifier(name__1699);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__1120, int id__1121)
        {
            SqlBuilder b__1123 = new SqlBuilder();
            b__1123.AppendSafe("DELETE FROM ");
            b__1123.AppendSafe(tableDef__1120.TableName.SqlValue);
            b__1123.AppendSafe(" WHERE id = ");
            b__1123.AppendInt32(id__1121);
            return b__1123.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__1355)
        {
            return new Query(tableName__1355, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>(), null);
        }
        public static SqlFragment Col(ISafeIdentifier table__1357, ISafeIdentifier column__1358)
        {
            SqlBuilder b__1360 = new SqlBuilder();
            b__1360.AppendSafe(table__1357.SqlValue);
            b__1360.AppendSafe(".");
            b__1360.AppendSafe(column__1358.SqlValue);
            return b__1360.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__1362 = new SqlBuilder();
            b__1362.AppendSafe("COUNT(*)");
            return b__1362.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__1363)
        {
            SqlBuilder b__1365 = new SqlBuilder();
            b__1365.AppendSafe("COUNT(");
            b__1365.AppendSafe(field__1363.SqlValue);
            b__1365.AppendSafe(")");
            return b__1365.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__1366)
        {
            SqlBuilder b__1368 = new SqlBuilder();
            b__1368.AppendSafe("SUM(");
            b__1368.AppendSafe(field__1366.SqlValue);
            b__1368.AppendSafe(")");
            return b__1368.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__1369)
        {
            SqlBuilder b__1371 = new SqlBuilder();
            b__1371.AppendSafe("AVG(");
            b__1371.AppendSafe(field__1369.SqlValue);
            b__1371.AppendSafe(")");
            return b__1371.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__1372)
        {
            SqlBuilder b__1374 = new SqlBuilder();
            b__1374.AppendSafe("MIN(");
            b__1374.AppendSafe(field__1372.SqlValue);
            b__1374.AppendSafe(")");
            return b__1374.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__1375)
        {
            SqlBuilder b__1377 = new SqlBuilder();
            b__1377.AppendSafe("MAX(");
            b__1377.AppendSafe(field__1375.SqlValue);
            b__1377.AppendSafe(")");
            return b__1377.Accumulated;
        }
        public static SqlFragment UnionSql(Query a__1378, Query b__1379)
        {
            SqlBuilder sb__1381 = new SqlBuilder();
            sb__1381.AppendSafe("(");
            sb__1381.AppendFragment(a__1378.ToSql());
            sb__1381.AppendSafe(") UNION (");
            sb__1381.AppendFragment(b__1379.ToSql());
            sb__1381.AppendSafe(")");
            return sb__1381.Accumulated;
        }
        public static SqlFragment UnionAllSql(Query a__1382, Query b__1383)
        {
            SqlBuilder sb__1385 = new SqlBuilder();
            sb__1385.AppendSafe("(");
            sb__1385.AppendFragment(a__1382.ToSql());
            sb__1385.AppendSafe(") UNION ALL (");
            sb__1385.AppendFragment(b__1383.ToSql());
            sb__1385.AppendSafe(")");
            return sb__1385.Accumulated;
        }
        public static SqlFragment IntersectSql(Query a__1386, Query b__1387)
        {
            SqlBuilder sb__1389 = new SqlBuilder();
            sb__1389.AppendSafe("(");
            sb__1389.AppendFragment(a__1386.ToSql());
            sb__1389.AppendSafe(") INTERSECT (");
            sb__1389.AppendFragment(b__1387.ToSql());
            sb__1389.AppendSafe(")");
            return sb__1389.Accumulated;
        }
        public static SqlFragment ExceptSql(Query a__1390, Query b__1391)
        {
            SqlBuilder sb__1393 = new SqlBuilder();
            sb__1393.AppendSafe("(");
            sb__1393.AppendFragment(a__1390.ToSql());
            sb__1393.AppendSafe(") EXCEPT (");
            sb__1393.AppendFragment(b__1391.ToSql());
            sb__1393.AppendSafe(")");
            return sb__1393.Accumulated;
        }
        public static SqlFragment Subquery(Query q__1394, ISafeIdentifier alias__1395)
        {
            SqlBuilder b__1397 = new SqlBuilder();
            b__1397.AppendSafe("(");
            b__1397.AppendFragment(q__1394.ToSql());
            b__1397.AppendSafe(") AS ");
            b__1397.AppendSafe(alias__1395.SqlValue);
            return b__1397.Accumulated;
        }
        public static SqlFragment ExistsSql(Query q__1398)
        {
            SqlBuilder b__1400 = new SqlBuilder();
            b__1400.AppendSafe("EXISTS (");
            b__1400.AppendFragment(q__1398.ToSql());
            b__1400.AppendSafe(")");
            return b__1400.Accumulated;
        }
        public static UpdateQuery Update(ISafeIdentifier tableName__1460)
        {
            return new UpdateQuery(tableName__1460, C::Listed.CreateReadOnlyList<SetClause>(), C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
        public static DeleteQuery DeleteFrom(ISafeIdentifier tableName__1462)
        {
            return new DeleteQuery(tableName__1462, C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
    }
}
