using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__950, G::IReadOnlyDictionary<string, string> params__951)
        {
            G::IReadOnlyDictionary<string, string> t___15116 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__950, params__951, t___15116, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__663(int c__1768)
        {
            bool return__581;
            bool t___8366;
            bool t___8367;
            if (c__1768 >= 97)
            {
                t___8366 = c__1768 <= 122;
            }
            else
            {
                t___8366 = false;
            }
            if (t___8366)
            {
                return__581 = true;
            }
            else
            {
                if (c__1768 >= 65)
                {
                    t___8367 = c__1768 <= 90;
                }
                else
                {
                    t___8367 = false;
                }
                if (t___8367)
                {
                    return__581 = true;
                }
                else
                {
                    return__581 = c__1768 == 95;
                }
            }
            return return__581;
        }
        internal static bool isIdentPart__664(int c__1770)
        {
            bool return__582;
            if (isIdentStart__663(c__1770))
            {
                return__582 = true;
            }
            else if (c__1770 >= 48)
            {
                return__582 = c__1770 <= 57;
            }
            else
            {
                return__582 = false;
            }
            return return__582;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1772)
        {
            int t___15114;
            if (string.IsNullOrEmpty(name__1772)) throw new S::Exception();
            int idx__1774 = 0;
            if (!isIdentStart__663(C::StringUtil.Get(name__1772, idx__1774))) throw new S::Exception();
            int t___15111 = C::StringUtil.Next(name__1772, idx__1774);
            idx__1774 = t___15111;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1772, idx__1774)) break;
                if (!isIdentPart__664(C::StringUtil.Get(name__1772, idx__1774))) throw new S::Exception();
                t___15114 = C::StringUtil.Next(name__1772, idx__1774);
                idx__1774 = t___15114;
            }
            return new ValidatedIdentifier(name__1772);
        }
        public static G::IReadOnlyList<FieldDef> Timestamps()
        {
            ISafeIdentifier t___7625;
            t___7625 = SafeIdentifier("inserted_at");
            FieldDef t___14213 = new FieldDef(t___7625, new DateField(), true, new SqlDefault(), false);
            ISafeIdentifier t___7629;
            t___7629 = SafeIdentifier("updated_at");
            return C::Listed.CreateReadOnlyList<FieldDef>(t___14213, new FieldDef(t___7629, new DateField(), true, new SqlDefault(), false));
        }
        public static SqlFragment DeleteSql(TableDef tableDef__1193, int id__1194)
        {
            SqlBuilder b__1196 = new SqlBuilder();
            b__1196.AppendSafe("DELETE FROM ");
            b__1196.AppendSafe(tableDef__1193.TableName.SqlValue);
            b__1196.AppendSafe(" WHERE ");
            b__1196.AppendSafe(tableDef__1193.PkName());
            b__1196.AppendSafe(" = ");
            b__1196.AppendInt32(id__1194);
            return b__1196.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__1428)
        {
            return new Query(tableName__1428, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>(), null);
        }
        public static SqlFragment Col(ISafeIdentifier table__1430, ISafeIdentifier column__1431)
        {
            SqlBuilder b__1433 = new SqlBuilder();
            b__1433.AppendSafe(table__1430.SqlValue);
            b__1433.AppendSafe(".");
            b__1433.AppendSafe(column__1431.SqlValue);
            return b__1433.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__1435 = new SqlBuilder();
            b__1435.AppendSafe("COUNT(*)");
            return b__1435.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__1436)
        {
            SqlBuilder b__1438 = new SqlBuilder();
            b__1438.AppendSafe("COUNT(");
            b__1438.AppendSafe(field__1436.SqlValue);
            b__1438.AppendSafe(")");
            return b__1438.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__1439)
        {
            SqlBuilder b__1441 = new SqlBuilder();
            b__1441.AppendSafe("SUM(");
            b__1441.AppendSafe(field__1439.SqlValue);
            b__1441.AppendSafe(")");
            return b__1441.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__1442)
        {
            SqlBuilder b__1444 = new SqlBuilder();
            b__1444.AppendSafe("AVG(");
            b__1444.AppendSafe(field__1442.SqlValue);
            b__1444.AppendSafe(")");
            return b__1444.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__1445)
        {
            SqlBuilder b__1447 = new SqlBuilder();
            b__1447.AppendSafe("MIN(");
            b__1447.AppendSafe(field__1445.SqlValue);
            b__1447.AppendSafe(")");
            return b__1447.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__1448)
        {
            SqlBuilder b__1450 = new SqlBuilder();
            b__1450.AppendSafe("MAX(");
            b__1450.AppendSafe(field__1448.SqlValue);
            b__1450.AppendSafe(")");
            return b__1450.Accumulated;
        }
        public static SqlFragment UnionSql(Query a__1451, Query b__1452)
        {
            SqlBuilder sb__1454 = new SqlBuilder();
            sb__1454.AppendSafe("(");
            sb__1454.AppendFragment(a__1451.ToSql());
            sb__1454.AppendSafe(") UNION (");
            sb__1454.AppendFragment(b__1452.ToSql());
            sb__1454.AppendSafe(")");
            return sb__1454.Accumulated;
        }
        public static SqlFragment UnionAllSql(Query a__1455, Query b__1456)
        {
            SqlBuilder sb__1458 = new SqlBuilder();
            sb__1458.AppendSafe("(");
            sb__1458.AppendFragment(a__1455.ToSql());
            sb__1458.AppendSafe(") UNION ALL (");
            sb__1458.AppendFragment(b__1456.ToSql());
            sb__1458.AppendSafe(")");
            return sb__1458.Accumulated;
        }
        public static SqlFragment IntersectSql(Query a__1459, Query b__1460)
        {
            SqlBuilder sb__1462 = new SqlBuilder();
            sb__1462.AppendSafe("(");
            sb__1462.AppendFragment(a__1459.ToSql());
            sb__1462.AppendSafe(") INTERSECT (");
            sb__1462.AppendFragment(b__1460.ToSql());
            sb__1462.AppendSafe(")");
            return sb__1462.Accumulated;
        }
        public static SqlFragment ExceptSql(Query a__1463, Query b__1464)
        {
            SqlBuilder sb__1466 = new SqlBuilder();
            sb__1466.AppendSafe("(");
            sb__1466.AppendFragment(a__1463.ToSql());
            sb__1466.AppendSafe(") EXCEPT (");
            sb__1466.AppendFragment(b__1464.ToSql());
            sb__1466.AppendSafe(")");
            return sb__1466.Accumulated;
        }
        public static SqlFragment Subquery(Query q__1467, ISafeIdentifier alias__1468)
        {
            SqlBuilder b__1470 = new SqlBuilder();
            b__1470.AppendSafe("(");
            b__1470.AppendFragment(q__1467.ToSql());
            b__1470.AppendSafe(") AS ");
            b__1470.AppendSafe(alias__1468.SqlValue);
            return b__1470.Accumulated;
        }
        public static SqlFragment ExistsSql(Query q__1471)
        {
            SqlBuilder b__1473 = new SqlBuilder();
            b__1473.AppendSafe("EXISTS (");
            b__1473.AppendFragment(q__1471.ToSql());
            b__1473.AppendSafe(")");
            return b__1473.Accumulated;
        }
        public static UpdateQuery Update(ISafeIdentifier tableName__1533)
        {
            return new UpdateQuery(tableName__1533, C::Listed.CreateReadOnlyList<SetClause>(), C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
        public static DeleteQuery DeleteFrom(ISafeIdentifier tableName__1535)
        {
            return new DeleteQuery(tableName__1535, C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
    }
}
