using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__1262;
        readonly G::IReadOnlyList<IWhereClause> conditions__1263;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__1264;
        readonly G::IReadOnlyList<OrderClause> orderClauses__1265;
        readonly int ? limitVal__1266;
        readonly int ? offsetVal__1267;
        readonly G::IReadOnlyList<JoinClause> joinClauses__1268;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__1269;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__1270;
        readonly bool isDistinct__1271;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__1272;
        readonly ILockMode ? lockMode__1273;
        public Query Where(SqlFragment condition__1275)
        {
            G::IList<IWhereClause> nb__1277 = L::Enumerable.ToList(this.conditions__1263);
            C::Listed.Add(nb__1277, new AndCondition(condition__1275));
            return new Query(this.tableName__1262, C::Listed.ToReadOnlyList(nb__1277), this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query OrWhere(SqlFragment condition__1279)
        {
            G::IList<IWhereClause> nb__1281 = L::Enumerable.ToList(this.conditions__1263);
            C::Listed.Add(nb__1281, new OrCondition(condition__1279));
            return new Query(this.tableName__1262, C::Listed.ToReadOnlyList(nb__1281), this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query WhereNull(ISafeIdentifier field__1283)
        {
            SqlBuilder b__1285 = new SqlBuilder();
            b__1285.AppendSafe(field__1283.SqlValue);
            b__1285.AppendSafe(" IS NULL");
            SqlFragment t___14044 = b__1285.Accumulated;
            return this.Where(t___14044);
        }
        public Query WhereNotNull(ISafeIdentifier field__1287)
        {
            SqlBuilder b__1289 = new SqlBuilder();
            b__1289.AppendSafe(field__1287.SqlValue);
            b__1289.AppendSafe(" IS NOT NULL");
            SqlFragment t___14038 = b__1289.Accumulated;
            return this.Where(t___14038);
        }
        public Query WhereIn(ISafeIdentifier field__1291, G::IReadOnlyList<ISqlPart> values__1292)
        {
            Query return__512;
            SqlFragment t___14019;
            int t___14027;
            SqlFragment t___14032;
            {
                {
                    if (values__1292.Count == 0)
                    {
                        SqlBuilder b__1294 = new SqlBuilder();
                        b__1294.AppendSafe("1 = 0");
                        t___14019 = b__1294.Accumulated;
                        return__512 = this.Where(t___14019);
                        goto fn__1293;
                    }
                    SqlBuilder b__1295 = new SqlBuilder();
                    b__1295.AppendSafe(field__1291.SqlValue);
                    b__1295.AppendSafe(" IN (");
                    b__1295.AppendPart(values__1292[0]);
                    int i__1296 = 1;
                    while (true)
                    {
                        t___14027 = values__1292.Count;
                        if (!(i__1296 < t___14027)) break;
                        b__1295.AppendSafe(", ");
                        b__1295.AppendPart(values__1292[i__1296]);
                        i__1296 = i__1296 + 1;
                    }
                    b__1295.AppendSafe(")");
                    t___14032 = b__1295.Accumulated;
                    return__512 = this.Where(t___14032);
                }
                fn__1293:
                {
                }
            }
            return return__512;
        }
        public Query WhereInSubquery(ISafeIdentifier field__1298, Query sub__1299)
        {
            SqlBuilder b__1301 = new SqlBuilder();
            b__1301.AppendSafe(field__1298.SqlValue);
            b__1301.AppendSafe(" IN (");
            b__1301.AppendFragment(sub__1299.ToSql());
            b__1301.AppendSafe(")");
            SqlFragment t___14014 = b__1301.Accumulated;
            return this.Where(t___14014);
        }
        public Query WhereNot(SqlFragment condition__1303)
        {
            SqlBuilder b__1305 = new SqlBuilder();
            b__1305.AppendSafe("NOT (");
            b__1305.AppendFragment(condition__1303);
            b__1305.AppendSafe(")");
            SqlFragment t___14005 = b__1305.Accumulated;
            return this.Where(t___14005);
        }
        public Query WhereBetween(ISafeIdentifier field__1307, ISqlPart low__1308, ISqlPart high__1309)
        {
            SqlBuilder b__1311 = new SqlBuilder();
            b__1311.AppendSafe(field__1307.SqlValue);
            b__1311.AppendSafe(" BETWEEN ");
            b__1311.AppendPart(low__1308);
            b__1311.AppendSafe(" AND ");
            b__1311.AppendPart(high__1309);
            SqlFragment t___13999 = b__1311.Accumulated;
            return this.Where(t___13999);
        }
        public Query WhereLike(ISafeIdentifier field__1313, string pattern__1314)
        {
            SqlBuilder b__1316 = new SqlBuilder();
            b__1316.AppendSafe(field__1313.SqlValue);
            b__1316.AppendSafe(" LIKE ");
            b__1316.AppendString(pattern__1314);
            SqlFragment t___13990 = b__1316.Accumulated;
            return this.Where(t___13990);
        }
        public Query WhereILike(ISafeIdentifier field__1318, string pattern__1319)
        {
            SqlBuilder b__1321 = new SqlBuilder();
            b__1321.AppendSafe(field__1318.SqlValue);
            b__1321.AppendSafe(" ILIKE ");
            b__1321.AppendString(pattern__1319);
            SqlFragment t___13983 = b__1321.Accumulated;
            return this.Where(t___13983);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__1323)
        {
            return new Query(this.tableName__1262, this.conditions__1263, fields__1323, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__1326)
        {
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, exprs__1326, this.lockMode__1273);
        }
        public Query OrderBy(ISafeIdentifier field__1329, bool ascending__1330)
        {
            G::IList<OrderClause> nb__1332 = L::Enumerable.ToList(this.orderClauses__1265);
            C::Listed.Add(nb__1332, new OrderClause(field__1329, ascending__1330, null));
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, C::Listed.ToReadOnlyList(nb__1332), this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query OrderByNulls(ISafeIdentifier field__1334, bool ascending__1335, INullsPosition nulls__1336)
        {
            G::IList<OrderClause> nb__1338 = L::Enumerable.ToList(this.orderClauses__1265);
            C::Listed.Add(nb__1338, new OrderClause(field__1334, ascending__1335, nulls__1336));
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, C::Listed.ToReadOnlyList(nb__1338), this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query Limit(int n__1340)
        {
            if (n__1340 < 0) throw new S::Exception();
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, n__1340, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query Offset(int n__1343)
        {
            if (n__1343 < 0) throw new S::Exception();
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, n__1343, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query Join(IJoinType joinType__1346, ISafeIdentifier table__1347, SqlFragment onCondition__1348)
        {
            G::IList<JoinClause> nb__1350 = L::Enumerable.ToList(this.joinClauses__1268);
            C::Listed.Add(nb__1350, new JoinClause(joinType__1346, table__1347, onCondition__1348));
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, C::Listed.ToReadOnlyList(nb__1350), this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query InnerJoin(ISafeIdentifier table__1352, SqlFragment onCondition__1353)
        {
            InnerJoin t___13945 = new InnerJoin();
            return this.Join(t___13945, table__1352, onCondition__1353);
        }
        public Query LeftJoin(ISafeIdentifier table__1356, SqlFragment onCondition__1357)
        {
            LeftJoin t___13943 = new LeftJoin();
            return this.Join(t___13943, table__1356, onCondition__1357);
        }
        public Query RightJoin(ISafeIdentifier table__1360, SqlFragment onCondition__1361)
        {
            RightJoin t___13941 = new RightJoin();
            return this.Join(t___13941, table__1360, onCondition__1361);
        }
        public Query FullJoin(ISafeIdentifier table__1364, SqlFragment onCondition__1365)
        {
            FullJoin t___13939 = new FullJoin();
            return this.Join(t___13939, table__1364, onCondition__1365);
        }
        public Query CrossJoin(ISafeIdentifier table__1368)
        {
            G::IList<JoinClause> nb__1370 = L::Enumerable.ToList(this.joinClauses__1268);
            C::Listed.Add(nb__1370, new JoinClause(new CrossJoin(), table__1368, null));
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, C::Listed.ToReadOnlyList(nb__1370), this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query GroupBy(ISafeIdentifier field__1372)
        {
            G::IList<ISafeIdentifier> nb__1374 = L::Enumerable.ToList(this.groupByFields__1269);
            C::Listed.Add(nb__1374, field__1372);
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, C::Listed.ToReadOnlyList(nb__1374), this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query Having(SqlFragment condition__1376)
        {
            G::IList<IWhereClause> nb__1378 = L::Enumerable.ToList(this.havingConditions__1270);
            C::Listed.Add(nb__1378, new AndCondition(condition__1376));
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, C::Listed.ToReadOnlyList(nb__1378), this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query OrHaving(SqlFragment condition__1380)
        {
            G::IList<IWhereClause> nb__1382 = L::Enumerable.ToList(this.havingConditions__1270);
            C::Listed.Add(nb__1382, new OrCondition(condition__1380));
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, C::Listed.ToReadOnlyList(nb__1382), this.isDistinct__1271, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, true, this.selectExprs__1272, this.lockMode__1273);
        }
        public Query Lock(ILockMode mode__1386)
        {
            return new Query(this.tableName__1262, this.conditions__1263, this.selectedFields__1264, this.orderClauses__1265, this.limitVal__1266, this.offsetVal__1267, this.joinClauses__1268, this.groupByFields__1269, this.havingConditions__1270, this.isDistinct__1271, this.selectExprs__1272, mode__1386);
        }
        public SqlFragment ToSql()
        {
            int t___13830;
            int t___13849;
            int t___13868;
            SqlBuilder b__1390 = new SqlBuilder();
            if (this.isDistinct__1271) b__1390.AppendSafe("SELECT DISTINCT ");
            else b__1390.AppendSafe("SELECT ");
            if (!(this.selectExprs__1272.Count == 0))
            {
                b__1390.AppendFragment(this.selectExprs__1272[0]);
                int i__1391 = 1;
                while (true)
                {
                    t___13830 = this.selectExprs__1272.Count;
                    if (!(i__1391 < t___13830)) break;
                    b__1390.AppendSafe(", ");
                    b__1390.AppendFragment(this.selectExprs__1272[i__1391]);
                    i__1391 = i__1391 + 1;
                }
            }
            else if (this.selectedFields__1264.Count == 0) b__1390.AppendSafe("*");
            else
            {
                string fn__13823(ISafeIdentifier f__1392)
                {
                    return f__1392.SqlValue;
                }
                b__1390.AppendSafe(C::Listed.Join(this.selectedFields__1264, ", ", (S::Func<ISafeIdentifier, string>) fn__13823));
            }
            b__1390.AppendSafe(" FROM ");
            b__1390.AppendSafe(this.tableName__1262.SqlValue);
            void fn__13822(JoinClause jc__1393)
            {
                b__1390.AppendSafe(" ");
                string t___13810 = jc__1393.JoinType.Keyword();
                b__1390.AppendSafe(t___13810);
                b__1390.AppendSafe(" ");
                string t___13814 = jc__1393.Table.SqlValue;
                b__1390.AppendSafe(t___13814);
                SqlFragment ? oc__1394 = jc__1393.OnCondition;
                if (!(oc__1394 == null))
                {
                    SqlFragment oc___2623 = oc__1394!;
                    b__1390.AppendSafe(" ON ");
                    b__1390.AppendFragment(oc___2623);
                }
            }
            C::Listed.ForEach(this.joinClauses__1268, (S::Action<JoinClause>) fn__13822);
            if (!(this.conditions__1263.Count == 0))
            {
                b__1390.AppendSafe(" WHERE ");
                b__1390.AppendFragment(this.conditions__1263[0].Condition);
                int i__1395 = 1;
                while (true)
                {
                    t___13849 = this.conditions__1263.Count;
                    if (!(i__1395 < t___13849)) break;
                    b__1390.AppendSafe(" ");
                    b__1390.AppendSafe(this.conditions__1263[i__1395].Keyword());
                    b__1390.AppendSafe(" ");
                    b__1390.AppendFragment(this.conditions__1263[i__1395].Condition);
                    i__1395 = i__1395 + 1;
                }
            }
            if (!(this.groupByFields__1269.Count == 0))
            {
                b__1390.AppendSafe(" GROUP BY ");
                string fn__13821(ISafeIdentifier f__1396)
                {
                    return f__1396.SqlValue;
                }
                b__1390.AppendSafe(C::Listed.Join(this.groupByFields__1269, ", ", (S::Func<ISafeIdentifier, string>) fn__13821));
            }
            if (!(this.havingConditions__1270.Count == 0))
            {
                b__1390.AppendSafe(" HAVING ");
                b__1390.AppendFragment(this.havingConditions__1270[0].Condition);
                int i__1397 = 1;
                while (true)
                {
                    t___13868 = this.havingConditions__1270.Count;
                    if (!(i__1397 < t___13868)) break;
                    b__1390.AppendSafe(" ");
                    b__1390.AppendSafe(this.havingConditions__1270[i__1397].Keyword());
                    b__1390.AppendSafe(" ");
                    b__1390.AppendFragment(this.havingConditions__1270[i__1397].Condition);
                    i__1397 = i__1397 + 1;
                }
            }
            if (!(this.orderClauses__1265.Count == 0))
            {
                b__1390.AppendSafe(" ORDER BY ");
                bool first__1398 = true;
                void fn__13820(OrderClause orc__1399)
                {
                    string t___13805;
                    string t___7240;
                    if (!first__1398) b__1390.AppendSafe(", ");
                    first__1398 = false;
                    string t___13800 = orc__1399.Field.SqlValue;
                    b__1390.AppendSafe(t___13800);
                    if (orc__1399.Ascending)
                    {
                        t___7240 = " ASC";
                    }
                    else
                    {
                        t___7240 = " DESC";
                    }
                    b__1390.AppendSafe(t___7240);
                    INullsPosition ? np__1400 = orc__1399.NullsPos;
                    if (!(np__1400 == null))
                    {
                        t___13805 = (np__1400!).Keyword();
                        b__1390.AppendSafe(t___13805);
                    }
                }
                C::Listed.ForEach(this.orderClauses__1265, (S::Action<OrderClause>) fn__13820);
            }
            int ? lv__1401 = this.limitVal__1266;
            if (!(lv__1401 == null))
            {
                int lv___2625 = lv__1401.Value;
                b__1390.AppendSafe(" LIMIT ");
                b__1390.AppendInt32(lv___2625);
            }
            int ? ov__1402 = this.offsetVal__1267;
            if (!(ov__1402 == null))
            {
                int ov___2626 = ov__1402.Value;
                b__1390.AppendSafe(" OFFSET ");
                b__1390.AppendInt32(ov___2626);
            }
            ILockMode ? lm__1403 = this.lockMode__1273;
            if (!(lm__1403 == null)) b__1390.AppendSafe((lm__1403!).Keyword());
            return b__1390.Accumulated;
        }
        public SqlFragment CountSql()
        {
            int t___13769;
            int t___13788;
            SqlBuilder b__1406 = new SqlBuilder();
            b__1406.AppendSafe("SELECT COUNT(*) FROM ");
            b__1406.AppendSafe(this.tableName__1262.SqlValue);
            void fn__13757(JoinClause jc__1407)
            {
                b__1406.AppendSafe(" ");
                string t___13747 = jc__1407.JoinType.Keyword();
                b__1406.AppendSafe(t___13747);
                b__1406.AppendSafe(" ");
                string t___13751 = jc__1407.Table.SqlValue;
                b__1406.AppendSafe(t___13751);
                SqlFragment ? oc2__1408 = jc__1407.OnCondition;
                if (!(oc2__1408 == null))
                {
                    SqlFragment oc2___2628 = oc2__1408!;
                    b__1406.AppendSafe(" ON ");
                    b__1406.AppendFragment(oc2___2628);
                }
            }
            C::Listed.ForEach(this.joinClauses__1268, (S::Action<JoinClause>) fn__13757);
            if (!(this.conditions__1263.Count == 0))
            {
                b__1406.AppendSafe(" WHERE ");
                b__1406.AppendFragment(this.conditions__1263[0].Condition);
                int i__1409 = 1;
                while (true)
                {
                    t___13769 = this.conditions__1263.Count;
                    if (!(i__1409 < t___13769)) break;
                    b__1406.AppendSafe(" ");
                    b__1406.AppendSafe(this.conditions__1263[i__1409].Keyword());
                    b__1406.AppendSafe(" ");
                    b__1406.AppendFragment(this.conditions__1263[i__1409].Condition);
                    i__1409 = i__1409 + 1;
                }
            }
            if (!(this.groupByFields__1269.Count == 0))
            {
                b__1406.AppendSafe(" GROUP BY ");
                string fn__13756(ISafeIdentifier f__1410)
                {
                    return f__1410.SqlValue;
                }
                b__1406.AppendSafe(C::Listed.Join(this.groupByFields__1269, ", ", (S::Func<ISafeIdentifier, string>) fn__13756));
            }
            if (!(this.havingConditions__1270.Count == 0))
            {
                b__1406.AppendSafe(" HAVING ");
                b__1406.AppendFragment(this.havingConditions__1270[0].Condition);
                int i__1411 = 1;
                while (true)
                {
                    t___13788 = this.havingConditions__1270.Count;
                    if (!(i__1411 < t___13788)) break;
                    b__1406.AppendSafe(" ");
                    b__1406.AppendSafe(this.havingConditions__1270[i__1411].Keyword());
                    b__1406.AppendSafe(" ");
                    b__1406.AppendFragment(this.havingConditions__1270[i__1411].Condition);
                    i__1411 = i__1411 + 1;
                }
            }
            return b__1406.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__1413)
        {
            SqlFragment return__537;
            Query t___7190;
            if (defaultLimit__1413 < 0) throw new S::Exception();
            if (!(this.limitVal__1266 == null))
            {
                return__537 = this.ToSql();
            }
            else
            {
                t___7190 = this.Limit(defaultLimit__1413);
                return__537 = t___7190.ToSql();
            }
            return return__537;
        }
        public Query(ISafeIdentifier tableName__1416, G::IReadOnlyList<IWhereClause> conditions__1417, G::IReadOnlyList<ISafeIdentifier> selectedFields__1418, G::IReadOnlyList<OrderClause> orderClauses__1419, int ? limitVal__1420, int ? offsetVal__1421, G::IReadOnlyList<JoinClause> joinClauses__1422, G::IReadOnlyList<ISafeIdentifier> groupByFields__1423, G::IReadOnlyList<IWhereClause> havingConditions__1424, bool isDistinct__1425, G::IReadOnlyList<SqlFragment> selectExprs__1426, ILockMode ? lockMode__1427)
        {
            this.tableName__1262 = tableName__1416;
            this.conditions__1263 = conditions__1417;
            this.selectedFields__1264 = selectedFields__1418;
            this.orderClauses__1265 = orderClauses__1419;
            this.limitVal__1266 = limitVal__1420;
            this.offsetVal__1267 = offsetVal__1421;
            this.joinClauses__1268 = joinClauses__1422;
            this.groupByFields__1269 = groupByFields__1423;
            this.havingConditions__1270 = havingConditions__1424;
            this.isDistinct__1271 = isDistinct__1425;
            this.selectExprs__1272 = selectExprs__1426;
            this.lockMode__1273 = lockMode__1427;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1262;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1263;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__1264;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__1265;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1266;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__1267;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__1268;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__1269;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__1270;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__1271;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__1272;
            }
        }
        public ILockMode ? LockMode
        {
            get
            {
                return this.lockMode__1273;
            }
        }
    }
}
