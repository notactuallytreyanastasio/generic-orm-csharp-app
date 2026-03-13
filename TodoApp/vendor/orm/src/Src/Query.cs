using S0 = Orm.Src;
using S1 = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__1387;
        readonly G::IReadOnlyList<IWhereClause> conditions__1388;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__1389;
        readonly G::IReadOnlyList<OrderClause> orderClauses__1390;
        readonly int ? limitVal__1391;
        readonly int ? offsetVal__1392;
        readonly G::IReadOnlyList<JoinClause> joinClauses__1393;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__1394;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__1395;
        readonly bool isDistinct__1396;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__1397;
        readonly ILockMode ? lockMode__1398;
        public Query Where(SqlFragment condition__1400)
        {
            G::IList<IWhereClause> nb__1402 = L::Enumerable.ToList(this.conditions__1388);
            C::Listed.Add(nb__1402, new AndCondition(condition__1400));
            return new Query(this.tableName__1387, C::Listed.ToReadOnlyList(nb__1402), this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query OrWhere(SqlFragment condition__1404)
        {
            G::IList<IWhereClause> nb__1406 = L::Enumerable.ToList(this.conditions__1388);
            C::Listed.Add(nb__1406, new OrCondition(condition__1404));
            return new Query(this.tableName__1387, C::Listed.ToReadOnlyList(nb__1406), this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query WhereNull(ISafeIdentifier field__1408)
        {
            SqlBuilder b__1410 = new SqlBuilder();
            b__1410.AppendSafe(field__1408.SqlValue);
            b__1410.AppendSafe(" IS NULL");
            SqlFragment t___15470 = b__1410.Accumulated;
            return this.Where(t___15470);
        }
        public Query WhereNotNull(ISafeIdentifier field__1412)
        {
            SqlBuilder b__1414 = new SqlBuilder();
            b__1414.AppendSafe(field__1412.SqlValue);
            b__1414.AppendSafe(" IS NOT NULL");
            SqlFragment t___15464 = b__1414.Accumulated;
            return this.Where(t___15464);
        }
        public Query WhereIn(ISafeIdentifier field__1416, G::IReadOnlyList<ISqlPart> values__1417)
        {
            Query return__555;
            SqlFragment t___15445;
            int t___15453;
            SqlFragment t___15458;
            {
                {
                    if (values__1417.Count == 0)
                    {
                        SqlBuilder b__1419 = new SqlBuilder();
                        b__1419.AppendSafe("1 = 0");
                        t___15445 = b__1419.Accumulated;
                        return__555 = this.Where(t___15445);
                        goto fn__1418;
                    }
                    SqlBuilder b__1420 = new SqlBuilder();
                    b__1420.AppendSafe(field__1416.SqlValue);
                    b__1420.AppendSafe(" IN (");
                    b__1420.AppendPart(values__1417[0]);
                    int i__1421 = 1;
                    while (true)
                    {
                        t___15453 = values__1417.Count;
                        if (!(i__1421 < t___15453)) break;
                        b__1420.AppendSafe(", ");
                        b__1420.AppendPart(values__1417[i__1421]);
                        i__1421 = i__1421 + 1;
                    }
                    b__1420.AppendSafe(")");
                    t___15458 = b__1420.Accumulated;
                    return__555 = this.Where(t___15458);
                }
                fn__1418:
                {
                }
            }
            return return__555;
        }
        public Query WhereInSubquery(ISafeIdentifier field__1423, Query sub__1424)
        {
            SqlBuilder b__1426 = new SqlBuilder();
            b__1426.AppendSafe(field__1423.SqlValue);
            b__1426.AppendSafe(" IN (");
            b__1426.AppendFragment(sub__1424.ToSql());
            b__1426.AppendSafe(")");
            SqlFragment t___15440 = b__1426.Accumulated;
            return this.Where(t___15440);
        }
        public Query WhereNot(SqlFragment condition__1428)
        {
            SqlBuilder b__1430 = new SqlBuilder();
            b__1430.AppendSafe("NOT (");
            b__1430.AppendFragment(condition__1428);
            b__1430.AppendSafe(")");
            SqlFragment t___15431 = b__1430.Accumulated;
            return this.Where(t___15431);
        }
        public Query WhereBetween(ISafeIdentifier field__1432, ISqlPart low__1433, ISqlPart high__1434)
        {
            SqlBuilder b__1436 = new SqlBuilder();
            b__1436.AppendSafe(field__1432.SqlValue);
            b__1436.AppendSafe(" BETWEEN ");
            b__1436.AppendPart(low__1433);
            b__1436.AppendSafe(" AND ");
            b__1436.AppendPart(high__1434);
            SqlFragment t___15425 = b__1436.Accumulated;
            return this.Where(t___15425);
        }
        public Query WhereLike(ISafeIdentifier field__1438, string pattern__1439)
        {
            SqlBuilder b__1441 = new SqlBuilder();
            b__1441.AppendSafe(field__1438.SqlValue);
            b__1441.AppendSafe(" LIKE ");
            b__1441.AppendString(pattern__1439);
            SqlFragment t___15416 = b__1441.Accumulated;
            return this.Where(t___15416);
        }
        public Query WhereILike(ISafeIdentifier field__1443, string pattern__1444)
        {
            SqlBuilder b__1446 = new SqlBuilder();
            b__1446.AppendSafe(field__1443.SqlValue);
            b__1446.AppendSafe(" ILIKE ");
            b__1446.AppendString(pattern__1444);
            SqlFragment t___15409 = b__1446.Accumulated;
            return this.Where(t___15409);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__1448)
        {
            return new Query(this.tableName__1387, this.conditions__1388, fields__1448, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__1451)
        {
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, exprs__1451, this.lockMode__1398);
        }
        public Query OrderBy(ISafeIdentifier field__1454, bool ascending__1455)
        {
            G::IList<OrderClause> nb__1457 = L::Enumerable.ToList(this.orderClauses__1390);
            C::Listed.Add(nb__1457, new OrderClause(field__1454, ascending__1455, null));
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, C::Listed.ToReadOnlyList(nb__1457), this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query OrderByNulls(ISafeIdentifier field__1459, bool ascending__1460, INullsPosition nulls__1461)
        {
            G::IList<OrderClause> nb__1463 = L::Enumerable.ToList(this.orderClauses__1390);
            C::Listed.Add(nb__1463, new OrderClause(field__1459, ascending__1460, nulls__1461));
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, C::Listed.ToReadOnlyList(nb__1463), this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query Limit(int n__1465)
        {
            if (n__1465 < 0) throw new S1::Exception();
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, n__1465, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query Offset(int n__1468)
        {
            if (n__1468 < 0) throw new S1::Exception();
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, n__1468, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query Join(IJoinType joinType__1471, ISafeIdentifier table__1472, SqlFragment onCondition__1473)
        {
            G::IList<JoinClause> nb__1475 = L::Enumerable.ToList(this.joinClauses__1393);
            C::Listed.Add(nb__1475, new JoinClause(joinType__1471, table__1472, onCondition__1473));
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, C::Listed.ToReadOnlyList(nb__1475), this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query InnerJoin(ISafeIdentifier table__1477, SqlFragment onCondition__1478)
        {
            InnerJoin t___15371 = new InnerJoin();
            return this.Join(t___15371, table__1477, onCondition__1478);
        }
        public Query LeftJoin(ISafeIdentifier table__1481, SqlFragment onCondition__1482)
        {
            LeftJoin t___15369 = new LeftJoin();
            return this.Join(t___15369, table__1481, onCondition__1482);
        }
        public Query RightJoin(ISafeIdentifier table__1485, SqlFragment onCondition__1486)
        {
            RightJoin t___15367 = new RightJoin();
            return this.Join(t___15367, table__1485, onCondition__1486);
        }
        public Query FullJoin(ISafeIdentifier table__1489, SqlFragment onCondition__1490)
        {
            FullJoin t___15365 = new FullJoin();
            return this.Join(t___15365, table__1489, onCondition__1490);
        }
        public Query CrossJoin(ISafeIdentifier table__1493)
        {
            G::IList<JoinClause> nb__1495 = L::Enumerable.ToList(this.joinClauses__1393);
            C::Listed.Add(nb__1495, new JoinClause(new CrossJoin(), table__1493, null));
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, C::Listed.ToReadOnlyList(nb__1495), this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query GroupBy(ISafeIdentifier field__1497)
        {
            G::IList<ISafeIdentifier> nb__1499 = L::Enumerable.ToList(this.groupByFields__1394);
            C::Listed.Add(nb__1499, field__1497);
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, C::Listed.ToReadOnlyList(nb__1499), this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query Having(SqlFragment condition__1501)
        {
            G::IList<IWhereClause> nb__1503 = L::Enumerable.ToList(this.havingConditions__1395);
            C::Listed.Add(nb__1503, new AndCondition(condition__1501));
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, C::Listed.ToReadOnlyList(nb__1503), this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query OrHaving(SqlFragment condition__1505)
        {
            G::IList<IWhereClause> nb__1507 = L::Enumerable.ToList(this.havingConditions__1395);
            C::Listed.Add(nb__1507, new OrCondition(condition__1505));
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, C::Listed.ToReadOnlyList(nb__1507), this.isDistinct__1396, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, true, this.selectExprs__1397, this.lockMode__1398);
        }
        public Query Lock(ILockMode mode__1511)
        {
            return new Query(this.tableName__1387, this.conditions__1388, this.selectedFields__1389, this.orderClauses__1390, this.limitVal__1391, this.offsetVal__1392, this.joinClauses__1393, this.groupByFields__1394, this.havingConditions__1395, this.isDistinct__1396, this.selectExprs__1397, mode__1511);
        }
        public SqlFragment ToSql()
        {
            int t___15287;
            SqlBuilder b__1515 = new SqlBuilder();
            if (this.isDistinct__1396) b__1515.AppendSafe("SELECT DISTINCT ");
            else b__1515.AppendSafe("SELECT ");
            if (!(this.selectExprs__1397.Count == 0))
            {
                b__1515.AppendFragment(this.selectExprs__1397[0]);
                int i__1516 = 1;
                while (true)
                {
                    t___15287 = this.selectExprs__1397.Count;
                    if (!(i__1516 < t___15287)) break;
                    b__1515.AppendSafe(", ");
                    b__1515.AppendFragment(this.selectExprs__1397[i__1516]);
                    i__1516 = i__1516 + 1;
                }
            }
            else if (this.selectedFields__1389.Count == 0) b__1515.AppendSafe("*");
            else
            {
                string fn__15280(ISafeIdentifier f__1517)
                {
                    return f__1517.SqlValue;
                }
                b__1515.AppendSafe(C::Listed.Join(this.selectedFields__1389, ", ", (S1::Func<ISafeIdentifier, string>) fn__15280));
            }
            b__1515.AppendSafe(" FROM ");
            b__1515.AppendSafe(this.tableName__1387.SqlValue);
            S0::SrcGlobal.renderJoins__706(b__1515, this.joinClauses__1393);
            S0::SrcGlobal.renderWhere__705(b__1515, this.conditions__1388);
            S0::SrcGlobal.renderGroupBy__707(b__1515, this.groupByFields__1394);
            S0::SrcGlobal.renderHaving__708(b__1515, this.havingConditions__1395);
            if (!(this.orderClauses__1390.Count == 0))
            {
                b__1515.AppendSafe(" ORDER BY ");
                bool first__1518 = true;
                void fn__15279(OrderClause orc__1519)
                {
                    string t___15276;
                    string t___7962;
                    if (!first__1518) b__1515.AppendSafe(", ");
                    first__1518 = false;
                    string t___15271 = orc__1519.Field.SqlValue;
                    b__1515.AppendSafe(t___15271);
                    if (orc__1519.Ascending)
                    {
                        t___7962 = " ASC";
                    }
                    else
                    {
                        t___7962 = " DESC";
                    }
                    b__1515.AppendSafe(t___7962);
                    INullsPosition ? np__1520 = orc__1519.NullsPos;
                    if (!(np__1520 == null))
                    {
                        t___15276 = (np__1520!).Keyword();
                        b__1515.AppendSafe(t___15276);
                    }
                }
                C::Listed.ForEach(this.orderClauses__1390, (S1::Action<OrderClause>) fn__15279);
            }
            int ? lv__1521 = this.limitVal__1391;
            if (!(lv__1521 == null))
            {
                int lv___2846 = lv__1521.Value;
                b__1515.AppendSafe(" LIMIT ");
                b__1515.AppendInt32(lv___2846);
            }
            int ? ov__1522 = this.offsetVal__1392;
            if (!(ov__1522 == null))
            {
                int ov___2847 = ov__1522.Value;
                b__1515.AppendSafe(" OFFSET ");
                b__1515.AppendInt32(ov___2847);
            }
            ILockMode ? lm__1523 = this.lockMode__1398;
            if (!(lm__1523 == null)) b__1515.AppendSafe((lm__1523!).Keyword());
            return b__1515.Accumulated;
        }
        public SqlFragment CountSql()
        {
            SqlBuilder b__1526 = new SqlBuilder();
            b__1526.AppendSafe("SELECT COUNT(*) FROM ");
            b__1526.AppendSafe(this.tableName__1387.SqlValue);
            S0::SrcGlobal.renderJoins__706(b__1526, this.joinClauses__1393);
            S0::SrcGlobal.renderWhere__705(b__1526, this.conditions__1388);
            S0::SrcGlobal.renderGroupBy__707(b__1526, this.groupByFields__1394);
            S0::SrcGlobal.renderHaving__708(b__1526, this.havingConditions__1395);
            return b__1526.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__1528)
        {
            SqlFragment return__580;
            Query t___7946;
            if (defaultLimit__1528 < 0) throw new S1::Exception();
            if (!(this.limitVal__1391 == null))
            {
                return__580 = this.ToSql();
            }
            else
            {
                t___7946 = this.Limit(defaultLimit__1528);
                return__580 = t___7946.ToSql();
            }
            return return__580;
        }
        public Query(ISafeIdentifier tableName__1531, G::IReadOnlyList<IWhereClause> conditions__1532, G::IReadOnlyList<ISafeIdentifier> selectedFields__1533, G::IReadOnlyList<OrderClause> orderClauses__1534, int ? limitVal__1535, int ? offsetVal__1536, G::IReadOnlyList<JoinClause> joinClauses__1537, G::IReadOnlyList<ISafeIdentifier> groupByFields__1538, G::IReadOnlyList<IWhereClause> havingConditions__1539, bool isDistinct__1540, G::IReadOnlyList<SqlFragment> selectExprs__1541, ILockMode ? lockMode__1542)
        {
            this.tableName__1387 = tableName__1531;
            this.conditions__1388 = conditions__1532;
            this.selectedFields__1389 = selectedFields__1533;
            this.orderClauses__1390 = orderClauses__1534;
            this.limitVal__1391 = limitVal__1535;
            this.offsetVal__1392 = offsetVal__1536;
            this.joinClauses__1393 = joinClauses__1537;
            this.groupByFields__1394 = groupByFields__1538;
            this.havingConditions__1395 = havingConditions__1539;
            this.isDistinct__1396 = isDistinct__1540;
            this.selectExprs__1397 = selectExprs__1541;
            this.lockMode__1398 = lockMode__1542;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1387;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1388;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__1389;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__1390;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1391;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__1392;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__1393;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__1394;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__1395;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__1396;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__1397;
            }
        }
        public ILockMode ? LockMode
        {
            get
            {
                return this.lockMode__1398;
            }
        }
    }
}
