using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__601, G::IReadOnlyDictionary<string, string> params__602)
        {
            G::IReadOnlyDictionary<string, string> t___9441 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__601, params__602, t___9441, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__462(int c__1121)
        {
            bool return__387;
            bool t___5298;
            bool t___5299;
            if (c__1121 >= 97)
            {
                t___5298 = c__1121 <= 122;
            }
            else
            {
                t___5298 = false;
            }
            if (t___5298)
            {
                return__387 = true;
            }
            else
            {
                if (c__1121 >= 65)
                {
                    t___5299 = c__1121 <= 90;
                }
                else
                {
                    t___5299 = false;
                }
                if (t___5299)
                {
                    return__387 = true;
                }
                else
                {
                    return__387 = c__1121 == 95;
                }
            }
            return return__387;
        }
        internal static bool isIdentPart__463(int c__1123)
        {
            bool return__388;
            if (isIdentStart__462(c__1123))
            {
                return__388 = true;
            }
            else if (c__1123 >= 48)
            {
                return__388 = c__1123 <= 57;
            }
            else
            {
                return__388 = false;
            }
            return return__388;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1125)
        {
            int t___9439;
            if (string.IsNullOrEmpty(name__1125)) throw new S::Exception();
            int idx__1127 = 0;
            if (!isIdentStart__462(C::StringUtil.Get(name__1125, idx__1127))) throw new S::Exception();
            int t___9436 = C::StringUtil.Next(name__1125, idx__1127);
            idx__1127 = t___9436;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1125, idx__1127)) break;
                if (!isIdentPart__463(C::StringUtil.Get(name__1125, idx__1127))) throw new S::Exception();
                t___9439 = C::StringUtil.Next(name__1125, idx__1127);
                idx__1127 = t___9439;
            }
            return new ValidatedIdentifier(name__1125);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__691, int id__692)
        {
            SqlBuilder b__694 = new SqlBuilder();
            b__694.AppendSafe("DELETE FROM ");
            b__694.AppendSafe(tableDef__691.TableName.SqlValue);
            b__694.AppendSafe(" WHERE id = ");
            b__694.AppendInt32(id__692);
            return b__694.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__886)
        {
            return new Query(tableName__886, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>());
        }
        public static SqlFragment Col(ISafeIdentifier table__888, ISafeIdentifier column__889)
        {
            SqlBuilder b__891 = new SqlBuilder();
            b__891.AppendSafe(table__888.SqlValue);
            b__891.AppendSafe(".");
            b__891.AppendSafe(column__889.SqlValue);
            return b__891.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__893 = new SqlBuilder();
            b__893.AppendSafe("COUNT(*)");
            return b__893.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__894)
        {
            SqlBuilder b__896 = new SqlBuilder();
            b__896.AppendSafe("COUNT(");
            b__896.AppendSafe(field__894.SqlValue);
            b__896.AppendSafe(")");
            return b__896.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__897)
        {
            SqlBuilder b__899 = new SqlBuilder();
            b__899.AppendSafe("SUM(");
            b__899.AppendSafe(field__897.SqlValue);
            b__899.AppendSafe(")");
            return b__899.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__900)
        {
            SqlBuilder b__902 = new SqlBuilder();
            b__902.AppendSafe("AVG(");
            b__902.AppendSafe(field__900.SqlValue);
            b__902.AppendSafe(")");
            return b__902.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__903)
        {
            SqlBuilder b__905 = new SqlBuilder();
            b__905.AppendSafe("MIN(");
            b__905.AppendSafe(field__903.SqlValue);
            b__905.AppendSafe(")");
            return b__905.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__906)
        {
            SqlBuilder b__908 = new SqlBuilder();
            b__908.AppendSafe("MAX(");
            b__908.AppendSafe(field__906.SqlValue);
            b__908.AppendSafe(")");
            return b__908.Accumulated;
        }
        public static SqlFragment UnionSql(Query a__909, Query b__910)
        {
            SqlBuilder sb__912 = new SqlBuilder();
            sb__912.AppendSafe("(");
            sb__912.AppendFragment(a__909.ToSql());
            sb__912.AppendSafe(") UNION (");
            sb__912.AppendFragment(b__910.ToSql());
            sb__912.AppendSafe(")");
            return sb__912.Accumulated;
        }
        public static SqlFragment UnionAllSql(Query a__913, Query b__914)
        {
            SqlBuilder sb__916 = new SqlBuilder();
            sb__916.AppendSafe("(");
            sb__916.AppendFragment(a__913.ToSql());
            sb__916.AppendSafe(") UNION ALL (");
            sb__916.AppendFragment(b__914.ToSql());
            sb__916.AppendSafe(")");
            return sb__916.Accumulated;
        }
        public static SqlFragment IntersectSql(Query a__917, Query b__918)
        {
            SqlBuilder sb__920 = new SqlBuilder();
            sb__920.AppendSafe("(");
            sb__920.AppendFragment(a__917.ToSql());
            sb__920.AppendSafe(") INTERSECT (");
            sb__920.AppendFragment(b__918.ToSql());
            sb__920.AppendSafe(")");
            return sb__920.Accumulated;
        }
        public static SqlFragment ExceptSql(Query a__921, Query b__922)
        {
            SqlBuilder sb__924 = new SqlBuilder();
            sb__924.AppendSafe("(");
            sb__924.AppendFragment(a__921.ToSql());
            sb__924.AppendSafe(") EXCEPT (");
            sb__924.AppendFragment(b__922.ToSql());
            sb__924.AppendSafe(")");
            return sb__924.Accumulated;
        }
        public static SqlFragment Subquery(Query q__925, ISafeIdentifier alias__926)
        {
            SqlBuilder b__928 = new SqlBuilder();
            b__928.AppendSafe("(");
            b__928.AppendFragment(q__925.ToSql());
            b__928.AppendSafe(") AS ");
            b__928.AppendSafe(alias__926.SqlValue);
            return b__928.Accumulated;
        }
        public static SqlFragment ExistsSql(Query q__929)
        {
            SqlBuilder b__931 = new SqlBuilder();
            b__931.AppendSafe("EXISTS (");
            b__931.AppendFragment(q__929.ToSql());
            b__931.AppendSafe(")");
            return b__931.Accumulated;
        }
    }
}
