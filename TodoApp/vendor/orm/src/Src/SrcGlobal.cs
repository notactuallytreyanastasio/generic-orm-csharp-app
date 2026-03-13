using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__645, G::IReadOnlyDictionary<string, string> params__646)
        {
            G::IReadOnlyDictionary<string, string> t___10695 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__645, params__646, t___10695, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__506(int c__1254)
        {
            bool return__431;
            bool t___6044;
            bool t___6045;
            if (c__1254 >= 97)
            {
                t___6044 = c__1254 <= 122;
            }
            else
            {
                t___6044 = false;
            }
            if (t___6044)
            {
                return__431 = true;
            }
            else
            {
                if (c__1254 >= 65)
                {
                    t___6045 = c__1254 <= 90;
                }
                else
                {
                    t___6045 = false;
                }
                if (t___6045)
                {
                    return__431 = true;
                }
                else
                {
                    return__431 = c__1254 == 95;
                }
            }
            return return__431;
        }
        internal static bool isIdentPart__507(int c__1256)
        {
            bool return__432;
            if (isIdentStart__506(c__1256))
            {
                return__432 = true;
            }
            else if (c__1256 >= 48)
            {
                return__432 = c__1256 <= 57;
            }
            else
            {
                return__432 = false;
            }
            return return__432;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1258)
        {
            int t___10693;
            if (string.IsNullOrEmpty(name__1258)) throw new S::Exception();
            int idx__1260 = 0;
            if (!isIdentStart__506(C::StringUtil.Get(name__1258, idx__1260))) throw new S::Exception();
            int t___10690 = C::StringUtil.Next(name__1258, idx__1260);
            idx__1260 = t___10690;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1258, idx__1260)) break;
                if (!isIdentPart__507(C::StringUtil.Get(name__1258, idx__1260))) throw new S::Exception();
                t___10693 = C::StringUtil.Next(name__1258, idx__1260);
                idx__1260 = t___10693;
            }
            return new ValidatedIdentifier(name__1258);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__735, int id__736)
        {
            SqlBuilder b__738 = new SqlBuilder();
            b__738.AppendSafe("DELETE FROM ");
            b__738.AppendSafe(tableDef__735.TableName.SqlValue);
            b__738.AppendSafe(" WHERE id = ");
            b__738.AppendInt32(id__736);
            return b__738.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__930)
        {
            return new Query(tableName__930, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>());
        }
        public static SqlFragment Col(ISafeIdentifier table__932, ISafeIdentifier column__933)
        {
            SqlBuilder b__935 = new SqlBuilder();
            b__935.AppendSafe(table__932.SqlValue);
            b__935.AppendSafe(".");
            b__935.AppendSafe(column__933.SqlValue);
            return b__935.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__937 = new SqlBuilder();
            b__937.AppendSafe("COUNT(*)");
            return b__937.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__938)
        {
            SqlBuilder b__940 = new SqlBuilder();
            b__940.AppendSafe("COUNT(");
            b__940.AppendSafe(field__938.SqlValue);
            b__940.AppendSafe(")");
            return b__940.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__941)
        {
            SqlBuilder b__943 = new SqlBuilder();
            b__943.AppendSafe("SUM(");
            b__943.AppendSafe(field__941.SqlValue);
            b__943.AppendSafe(")");
            return b__943.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__944)
        {
            SqlBuilder b__946 = new SqlBuilder();
            b__946.AppendSafe("AVG(");
            b__946.AppendSafe(field__944.SqlValue);
            b__946.AppendSafe(")");
            return b__946.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__947)
        {
            SqlBuilder b__949 = new SqlBuilder();
            b__949.AppendSafe("MIN(");
            b__949.AppendSafe(field__947.SqlValue);
            b__949.AppendSafe(")");
            return b__949.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__950)
        {
            SqlBuilder b__952 = new SqlBuilder();
            b__952.AppendSafe("MAX(");
            b__952.AppendSafe(field__950.SqlValue);
            b__952.AppendSafe(")");
            return b__952.Accumulated;
        }
        public static SqlFragment UnionSql(Query a__953, Query b__954)
        {
            SqlBuilder sb__956 = new SqlBuilder();
            sb__956.AppendSafe("(");
            sb__956.AppendFragment(a__953.ToSql());
            sb__956.AppendSafe(") UNION (");
            sb__956.AppendFragment(b__954.ToSql());
            sb__956.AppendSafe(")");
            return sb__956.Accumulated;
        }
        public static SqlFragment UnionAllSql(Query a__957, Query b__958)
        {
            SqlBuilder sb__960 = new SqlBuilder();
            sb__960.AppendSafe("(");
            sb__960.AppendFragment(a__957.ToSql());
            sb__960.AppendSafe(") UNION ALL (");
            sb__960.AppendFragment(b__958.ToSql());
            sb__960.AppendSafe(")");
            return sb__960.Accumulated;
        }
        public static SqlFragment IntersectSql(Query a__961, Query b__962)
        {
            SqlBuilder sb__964 = new SqlBuilder();
            sb__964.AppendSafe("(");
            sb__964.AppendFragment(a__961.ToSql());
            sb__964.AppendSafe(") INTERSECT (");
            sb__964.AppendFragment(b__962.ToSql());
            sb__964.AppendSafe(")");
            return sb__964.Accumulated;
        }
        public static SqlFragment ExceptSql(Query a__965, Query b__966)
        {
            SqlBuilder sb__968 = new SqlBuilder();
            sb__968.AppendSafe("(");
            sb__968.AppendFragment(a__965.ToSql());
            sb__968.AppendSafe(") EXCEPT (");
            sb__968.AppendFragment(b__966.ToSql());
            sb__968.AppendSafe(")");
            return sb__968.Accumulated;
        }
        public static SqlFragment Subquery(Query q__969, ISafeIdentifier alias__970)
        {
            SqlBuilder b__972 = new SqlBuilder();
            b__972.AppendSafe("(");
            b__972.AppendFragment(q__969.ToSql());
            b__972.AppendSafe(") AS ");
            b__972.AppendSafe(alias__970.SqlValue);
            return b__972.Accumulated;
        }
        public static SqlFragment ExistsSql(Query q__973)
        {
            SqlBuilder b__975 = new SqlBuilder();
            b__975.AppendSafe("EXISTS (");
            b__975.AppendFragment(q__973.ToSql());
            b__975.AppendSafe(")");
            return b__975.Accumulated;
        }
        public static UpdateQuery Update(ISafeIdentifier tableName__1035)
        {
            return new UpdateQuery(tableName__1035, C::Listed.CreateReadOnlyList<SetClause>(), C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
        public static DeleteQuery DeleteFrom(ISafeIdentifier tableName__1037)
        {
            return new DeleteQuery(tableName__1037, C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
    }
}
