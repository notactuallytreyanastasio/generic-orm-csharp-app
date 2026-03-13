using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__982, G::IReadOnlyDictionary<string, string> params__983)
        {
            G::IReadOnlyDictionary<string, string> t___17003 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__982, params__983, t___17003, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__710(int c__1906)
        {
            bool return__624;
            bool t___9433;
            bool t___9434;
            if (c__1906 >= 97)
            {
                t___9433 = c__1906 <= 122;
            }
            else
            {
                t___9433 = false;
            }
            if (t___9433)
            {
                return__624 = true;
            }
            else
            {
                if (c__1906 >= 65)
                {
                    t___9434 = c__1906 <= 90;
                }
                else
                {
                    t___9434 = false;
                }
                if (t___9434)
                {
                    return__624 = true;
                }
                else
                {
                    return__624 = c__1906 == 95;
                }
            }
            return return__624;
        }
        internal static bool isIdentPart__711(int c__1908)
        {
            bool return__625;
            if (isIdentStart__710(c__1908))
            {
                return__625 = true;
            }
            else if (c__1908 >= 48)
            {
                return__625 = c__1908 <= 57;
            }
            else
            {
                return__625 = false;
            }
            return return__625;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1910)
        {
            int t___17001;
            if (string.IsNullOrEmpty(name__1910)) throw new S::Exception();
            int idx__1912 = 0;
            if (!isIdentStart__710(C::StringUtil.Get(name__1910, idx__1912))) throw new S::Exception();
            int t___16998 = C::StringUtil.Next(name__1910, idx__1912);
            idx__1912 = t___16998;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1910, idx__1912)) break;
                if (!isIdentPart__711(C::StringUtil.Get(name__1910, idx__1912))) throw new S::Exception();
                t___17001 = C::StringUtil.Next(name__1910, idx__1912);
                idx__1912 = t___17001;
            }
            return new ValidatedIdentifier(name__1910);
        }
        public static G::IReadOnlyList<FieldDef> Timestamps()
        {
            ISafeIdentifier t___8692;
            t___8692 = SafeIdentifier("inserted_at");
            FieldDef t___16100 = new FieldDef(t___8692, new DateField(), true, new SqlDefault(), false);
            ISafeIdentifier t___8696;
            t___8696 = SafeIdentifier("updated_at");
            return C::Listed.CreateReadOnlyList<FieldDef>(t___16100, new FieldDef(t___8696, new DateField(), true, new SqlDefault(), false));
        }
        public static SqlFragment DeleteSql(TableDef tableDef__1301, int id__1302)
        {
            SqlBuilder b__1304 = new SqlBuilder();
            b__1304.AppendSafe("DELETE FROM ");
            b__1304.AppendSafe(tableDef__1301.TableName.SqlValue);
            b__1304.AppendSafe(" WHERE ");
            b__1304.AppendSafe(tableDef__1301.PkName());
            b__1304.AppendSafe(" = ");
            b__1304.AppendInt32(id__1302);
            return b__1304.Accumulated;
        }
        internal static void renderWhere__705(SqlBuilder b__1370, G::IReadOnlyList<IWhereClause> conditions__1371)
        {
            SqlFragment t___15522;
            int t___15524;
            string t___15527;
            SqlFragment t___15531;
            if (!(conditions__1371.Count == 0))
            {
                b__1370.AppendSafe(" WHERE ");
                t___15522 = conditions__1371[0].Condition;
                b__1370.AppendFragment(t___15522);
                int i__1373 = 1;
                while (true)
                {
                    t___15524 = conditions__1371.Count;
                    if (!(i__1373 < t___15524)) break;
                    b__1370.AppendSafe(" ");
                    t___15527 = conditions__1371[i__1373].Keyword();
                    b__1370.AppendSafe(t___15527);
                    b__1370.AppendSafe(" ");
                    t___15531 = conditions__1371[i__1373].Condition;
                    b__1370.AppendFragment(t___15531);
                    i__1373 = i__1373 + 1;
                }
            }
        }
        internal static void renderJoins__706(SqlBuilder b__1374, G::IReadOnlyList<JoinClause> joinClauses__1375)
        {
            void fn__15516(JoinClause jc__1377)
            {
                b__1374.AppendSafe(" ");
                string t___15507 = jc__1377.JoinType.Keyword();
                b__1374.AppendSafe(t___15507);
                b__1374.AppendSafe(" ");
                string t___15511 = jc__1377.Table.SqlValue;
                b__1374.AppendSafe(t___15511);
                SqlFragment ? oc__1378 = jc__1377.OnCondition;
                if (!(oc__1378 == null))
                {
                    SqlFragment oc___2844 = oc__1378!;
                    b__1374.AppendSafe(" ON ");
                    b__1374.AppendFragment(oc___2844);
                }
            }
            C::Listed.ForEach(joinClauses__1375, (S::Action<JoinClause>) fn__15516);
        }
        internal static void renderGroupBy__707(SqlBuilder b__1379, G::IReadOnlyList<ISafeIdentifier> groupByFields__1380)
        {
            string t___15503;
            if (!(groupByFields__1380.Count == 0))
            {
                b__1379.AppendSafe(" GROUP BY ");
                string fn__15499(ISafeIdentifier f__1382)
                {
                    return f__1382.SqlValue;
                }
                t___15503 = C::Listed.Join(groupByFields__1380, ", ", (S::Func<ISafeIdentifier, string>) fn__15499);
                b__1379.AppendSafe(t___15503);
            }
        }
        internal static void renderHaving__708(SqlBuilder b__1383, G::IReadOnlyList<IWhereClause> havingConditions__1384)
        {
            SqlFragment t___15487;
            int t___15489;
            string t___15492;
            SqlFragment t___15496;
            if (!(havingConditions__1384.Count == 0))
            {
                b__1383.AppendSafe(" HAVING ");
                t___15487 = havingConditions__1384[0].Condition;
                b__1383.AppendFragment(t___15487);
                int i__1386 = 1;
                while (true)
                {
                    t___15489 = havingConditions__1384.Count;
                    if (!(i__1386 < t___15489)) break;
                    b__1383.AppendSafe(" ");
                    t___15492 = havingConditions__1384[i__1386].Keyword();
                    b__1383.AppendSafe(t___15492);
                    b__1383.AppendSafe(" ");
                    t___15496 = havingConditions__1384[i__1386].Condition;
                    b__1383.AppendFragment(t___15496);
                    i__1386 = i__1386 + 1;
                }
            }
        }
        public static Query From(ISafeIdentifier tableName__1543)
        {
            return new Query(tableName__1543, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>(), null);
        }
        public static SqlFragment Col(ISafeIdentifier table__1545, ISafeIdentifier column__1546)
        {
            SqlBuilder b__1548 = new SqlBuilder();
            b__1548.AppendSafe(table__1545.SqlValue);
            b__1548.AppendSafe(".");
            b__1548.AppendSafe(column__1546.SqlValue);
            return b__1548.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__1550 = new SqlBuilder();
            b__1550.AppendSafe("COUNT(*)");
            return b__1550.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__1551)
        {
            SqlBuilder b__1553 = new SqlBuilder();
            b__1553.AppendSafe("COUNT(");
            b__1553.AppendSafe(field__1551.SqlValue);
            b__1553.AppendSafe(")");
            return b__1553.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__1554)
        {
            SqlBuilder b__1556 = new SqlBuilder();
            b__1556.AppendSafe("SUM(");
            b__1556.AppendSafe(field__1554.SqlValue);
            b__1556.AppendSafe(")");
            return b__1556.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__1557)
        {
            SqlBuilder b__1559 = new SqlBuilder();
            b__1559.AppendSafe("AVG(");
            b__1559.AppendSafe(field__1557.SqlValue);
            b__1559.AppendSafe(")");
            return b__1559.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__1560)
        {
            SqlBuilder b__1562 = new SqlBuilder();
            b__1562.AppendSafe("MIN(");
            b__1562.AppendSafe(field__1560.SqlValue);
            b__1562.AppendSafe(")");
            return b__1562.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__1563)
        {
            SqlBuilder b__1565 = new SqlBuilder();
            b__1565.AppendSafe("MAX(");
            b__1565.AppendSafe(field__1563.SqlValue);
            b__1565.AppendSafe(")");
            return b__1565.Accumulated;
        }
        public static SqlFragment UnionSql(Query a__1566, Query b__1567)
        {
            SqlBuilder sb__1569 = new SqlBuilder();
            sb__1569.AppendSafe("(");
            sb__1569.AppendFragment(a__1566.ToSql());
            sb__1569.AppendSafe(") UNION (");
            sb__1569.AppendFragment(b__1567.ToSql());
            sb__1569.AppendSafe(")");
            return sb__1569.Accumulated;
        }
        public static SqlFragment UnionAllSql(Query a__1570, Query b__1571)
        {
            SqlBuilder sb__1573 = new SqlBuilder();
            sb__1573.AppendSafe("(");
            sb__1573.AppendFragment(a__1570.ToSql());
            sb__1573.AppendSafe(") UNION ALL (");
            sb__1573.AppendFragment(b__1571.ToSql());
            sb__1573.AppendSafe(")");
            return sb__1573.Accumulated;
        }
        public static SqlFragment IntersectSql(Query a__1574, Query b__1575)
        {
            SqlBuilder sb__1577 = new SqlBuilder();
            sb__1577.AppendSafe("(");
            sb__1577.AppendFragment(a__1574.ToSql());
            sb__1577.AppendSafe(") INTERSECT (");
            sb__1577.AppendFragment(b__1575.ToSql());
            sb__1577.AppendSafe(")");
            return sb__1577.Accumulated;
        }
        public static SqlFragment ExceptSql(Query a__1578, Query b__1579)
        {
            SqlBuilder sb__1581 = new SqlBuilder();
            sb__1581.AppendSafe("(");
            sb__1581.AppendFragment(a__1578.ToSql());
            sb__1581.AppendSafe(") EXCEPT (");
            sb__1581.AppendFragment(b__1579.ToSql());
            sb__1581.AppendSafe(")");
            return sb__1581.Accumulated;
        }
        public static SqlFragment Subquery(Query q__1582, ISafeIdentifier alias__1583)
        {
            SqlBuilder b__1585 = new SqlBuilder();
            b__1585.AppendSafe("(");
            b__1585.AppendFragment(q__1582.ToSql());
            b__1585.AppendSafe(") AS ");
            b__1585.AppendSafe(alias__1583.SqlValue);
            return b__1585.Accumulated;
        }
        public static SqlFragment ExistsSql(Query q__1586)
        {
            SqlBuilder b__1588 = new SqlBuilder();
            b__1588.AppendSafe("EXISTS (");
            b__1588.AppendFragment(q__1586.ToSql());
            b__1588.AppendSafe(")");
            return b__1588.Accumulated;
        }
        public static UpdateQuery Update(ISafeIdentifier tableName__1646)
        {
            return new UpdateQuery(tableName__1646, C::Listed.CreateReadOnlyList<SetClause>(), C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
        public static DeleteQuery DeleteFrom(ISafeIdentifier tableName__1648)
        {
            return new DeleteQuery(tableName__1648, C::Listed.CreateReadOnlyList<IWhereClause>(), null);
        }
    }
}
