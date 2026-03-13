using S = System;
using G = System.Collections.Generic;
using L = System.Linq;
using C = TemperLang.Core;
namespace Orm.Src
{
    public class Query
    {
        readonly ISafeIdentifier tableName__1189;
        readonly G::IReadOnlyList<IWhereClause> conditions__1190;
        readonly G::IReadOnlyList<ISafeIdentifier> selectedFields__1191;
        readonly G::IReadOnlyList<OrderClause> orderClauses__1192;
        readonly int ? limitVal__1193;
        readonly int ? offsetVal__1194;
        readonly G::IReadOnlyList<JoinClause> joinClauses__1195;
        readonly G::IReadOnlyList<ISafeIdentifier> groupByFields__1196;
        readonly G::IReadOnlyList<IWhereClause> havingConditions__1197;
        readonly bool isDistinct__1198;
        readonly G::IReadOnlyList<SqlFragment> selectExprs__1199;
        readonly ILockMode ? lockMode__1200;
        public Query Where(SqlFragment condition__1202)
        {
            G::IList<IWhereClause> nb__1204 = L::Enumerable.ToList(this.conditions__1190);
            C::Listed.Add(nb__1204, new AndCondition(condition__1202));
            return new Query(this.tableName__1189, C::Listed.ToReadOnlyList(nb__1204), this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query OrWhere(SqlFragment condition__1206)
        {
            G::IList<IWhereClause> nb__1208 = L::Enumerable.ToList(this.conditions__1190);
            C::Listed.Add(nb__1208, new OrCondition(condition__1206));
            return new Query(this.tableName__1189, C::Listed.ToReadOnlyList(nb__1208), this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query WhereNull(ISafeIdentifier field__1210)
        {
            SqlBuilder b__1212 = new SqlBuilder();
            b__1212.AppendSafe(field__1210.SqlValue);
            b__1212.AppendSafe(" IS NULL");
            SqlFragment t___12997 = b__1212.Accumulated;
            return this.Where(t___12997);
        }
        public Query WhereNotNull(ISafeIdentifier field__1214)
        {
            SqlBuilder b__1216 = new SqlBuilder();
            b__1216.AppendSafe(field__1214.SqlValue);
            b__1216.AppendSafe(" IS NOT NULL");
            SqlFragment t___12991 = b__1216.Accumulated;
            return this.Where(t___12991);
        }
        public Query WhereIn(ISafeIdentifier field__1218, G::IReadOnlyList<ISqlPart> values__1219)
        {
            Query return__495;
            SqlFragment t___12972;
            int t___12980;
            SqlFragment t___12985;
            {
                {
                    if (values__1219.Count == 0)
                    {
                        SqlBuilder b__1221 = new SqlBuilder();
                        b__1221.AppendSafe("1 = 0");
                        t___12972 = b__1221.Accumulated;
                        return__495 = this.Where(t___12972);
                        goto fn__1220;
                    }
                    SqlBuilder b__1222 = new SqlBuilder();
                    b__1222.AppendSafe(field__1218.SqlValue);
                    b__1222.AppendSafe(" IN (");
                    b__1222.AppendPart(values__1219[0]);
                    int i__1223 = 1;
                    while (true)
                    {
                        t___12980 = values__1219.Count;
                        if (!(i__1223 < t___12980)) break;
                        b__1222.AppendSafe(", ");
                        b__1222.AppendPart(values__1219[i__1223]);
                        i__1223 = i__1223 + 1;
                    }
                    b__1222.AppendSafe(")");
                    t___12985 = b__1222.Accumulated;
                    return__495 = this.Where(t___12985);
                }
                fn__1220:
                {
                }
            }
            return return__495;
        }
        public Query WhereInSubquery(ISafeIdentifier field__1225, Query sub__1226)
        {
            SqlBuilder b__1228 = new SqlBuilder();
            b__1228.AppendSafe(field__1225.SqlValue);
            b__1228.AppendSafe(" IN (");
            b__1228.AppendFragment(sub__1226.ToSql());
            b__1228.AppendSafe(")");
            SqlFragment t___12967 = b__1228.Accumulated;
            return this.Where(t___12967);
        }
        public Query WhereNot(SqlFragment condition__1230)
        {
            SqlBuilder b__1232 = new SqlBuilder();
            b__1232.AppendSafe("NOT (");
            b__1232.AppendFragment(condition__1230);
            b__1232.AppendSafe(")");
            SqlFragment t___12958 = b__1232.Accumulated;
            return this.Where(t___12958);
        }
        public Query WhereBetween(ISafeIdentifier field__1234, ISqlPart low__1235, ISqlPart high__1236)
        {
            SqlBuilder b__1238 = new SqlBuilder();
            b__1238.AppendSafe(field__1234.SqlValue);
            b__1238.AppendSafe(" BETWEEN ");
            b__1238.AppendPart(low__1235);
            b__1238.AppendSafe(" AND ");
            b__1238.AppendPart(high__1236);
            SqlFragment t___12952 = b__1238.Accumulated;
            return this.Where(t___12952);
        }
        public Query WhereLike(ISafeIdentifier field__1240, string pattern__1241)
        {
            SqlBuilder b__1243 = new SqlBuilder();
            b__1243.AppendSafe(field__1240.SqlValue);
            b__1243.AppendSafe(" LIKE ");
            b__1243.AppendString(pattern__1241);
            SqlFragment t___12943 = b__1243.Accumulated;
            return this.Where(t___12943);
        }
        public Query WhereILike(ISafeIdentifier field__1245, string pattern__1246)
        {
            SqlBuilder b__1248 = new SqlBuilder();
            b__1248.AppendSafe(field__1245.SqlValue);
            b__1248.AppendSafe(" ILIKE ");
            b__1248.AppendString(pattern__1246);
            SqlFragment t___12936 = b__1248.Accumulated;
            return this.Where(t___12936);
        }
        public Query Select(G::IReadOnlyList<ISafeIdentifier> fields__1250)
        {
            return new Query(this.tableName__1189, this.conditions__1190, fields__1250, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query SelectExpr(G::IReadOnlyList<SqlFragment> exprs__1253)
        {
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, exprs__1253, this.lockMode__1200);
        }
        public Query OrderBy(ISafeIdentifier field__1256, bool ascending__1257)
        {
            G::IList<OrderClause> nb__1259 = L::Enumerable.ToList(this.orderClauses__1192);
            C::Listed.Add(nb__1259, new OrderClause(field__1256, ascending__1257, null));
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, C::Listed.ToReadOnlyList(nb__1259), this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query OrderByNulls(ISafeIdentifier field__1261, bool ascending__1262, INullsPosition nulls__1263)
        {
            G::IList<OrderClause> nb__1265 = L::Enumerable.ToList(this.orderClauses__1192);
            C::Listed.Add(nb__1265, new OrderClause(field__1261, ascending__1262, nulls__1263));
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, C::Listed.ToReadOnlyList(nb__1265), this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query Limit(int n__1267)
        {
            if (n__1267 < 0) throw new S::Exception();
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, n__1267, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query Offset(int n__1270)
        {
            if (n__1270 < 0) throw new S::Exception();
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, n__1270, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query Join(IJoinType joinType__1273, ISafeIdentifier table__1274, SqlFragment onCondition__1275)
        {
            G::IList<JoinClause> nb__1277 = L::Enumerable.ToList(this.joinClauses__1195);
            C::Listed.Add(nb__1277, new JoinClause(joinType__1273, table__1274, onCondition__1275));
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, C::Listed.ToReadOnlyList(nb__1277), this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query InnerJoin(ISafeIdentifier table__1279, SqlFragment onCondition__1280)
        {
            InnerJoin t___12898 = new InnerJoin();
            return this.Join(t___12898, table__1279, onCondition__1280);
        }
        public Query LeftJoin(ISafeIdentifier table__1283, SqlFragment onCondition__1284)
        {
            LeftJoin t___12896 = new LeftJoin();
            return this.Join(t___12896, table__1283, onCondition__1284);
        }
        public Query RightJoin(ISafeIdentifier table__1287, SqlFragment onCondition__1288)
        {
            RightJoin t___12894 = new RightJoin();
            return this.Join(t___12894, table__1287, onCondition__1288);
        }
        public Query FullJoin(ISafeIdentifier table__1291, SqlFragment onCondition__1292)
        {
            FullJoin t___12892 = new FullJoin();
            return this.Join(t___12892, table__1291, onCondition__1292);
        }
        public Query CrossJoin(ISafeIdentifier table__1295)
        {
            G::IList<JoinClause> nb__1297 = L::Enumerable.ToList(this.joinClauses__1195);
            C::Listed.Add(nb__1297, new JoinClause(new CrossJoin(), table__1295, null));
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, C::Listed.ToReadOnlyList(nb__1297), this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query GroupBy(ISafeIdentifier field__1299)
        {
            G::IList<ISafeIdentifier> nb__1301 = L::Enumerable.ToList(this.groupByFields__1196);
            C::Listed.Add(nb__1301, field__1299);
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, C::Listed.ToReadOnlyList(nb__1301), this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query Having(SqlFragment condition__1303)
        {
            G::IList<IWhereClause> nb__1305 = L::Enumerable.ToList(this.havingConditions__1197);
            C::Listed.Add(nb__1305, new AndCondition(condition__1303));
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, C::Listed.ToReadOnlyList(nb__1305), this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query OrHaving(SqlFragment condition__1307)
        {
            G::IList<IWhereClause> nb__1309 = L::Enumerable.ToList(this.havingConditions__1197);
            C::Listed.Add(nb__1309, new OrCondition(condition__1307));
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, C::Listed.ToReadOnlyList(nb__1309), this.isDistinct__1198, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query Distinct()
        {
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, true, this.selectExprs__1199, this.lockMode__1200);
        }
        public Query Lock(ILockMode mode__1313)
        {
            return new Query(this.tableName__1189, this.conditions__1190, this.selectedFields__1191, this.orderClauses__1192, this.limitVal__1193, this.offsetVal__1194, this.joinClauses__1195, this.groupByFields__1196, this.havingConditions__1197, this.isDistinct__1198, this.selectExprs__1199, mode__1313);
        }
        public SqlFragment ToSql()
        {
            int t___12783;
            int t___12802;
            int t___12821;
            SqlBuilder b__1317 = new SqlBuilder();
            if (this.isDistinct__1198) b__1317.AppendSafe("SELECT DISTINCT ");
            else b__1317.AppendSafe("SELECT ");
            if (!(this.selectExprs__1199.Count == 0))
            {
                b__1317.AppendFragment(this.selectExprs__1199[0]);
                int i__1318 = 1;
                while (true)
                {
                    t___12783 = this.selectExprs__1199.Count;
                    if (!(i__1318 < t___12783)) break;
                    b__1317.AppendSafe(", ");
                    b__1317.AppendFragment(this.selectExprs__1199[i__1318]);
                    i__1318 = i__1318 + 1;
                }
            }
            else if (this.selectedFields__1191.Count == 0) b__1317.AppendSafe("*");
            else
            {
                string fn__12776(ISafeIdentifier f__1319)
                {
                    return f__1319.SqlValue;
                }
                b__1317.AppendSafe(C::Listed.Join(this.selectedFields__1191, ", ", (S::Func<ISafeIdentifier, string>) fn__12776));
            }
            b__1317.AppendSafe(" FROM ");
            b__1317.AppendSafe(this.tableName__1189.SqlValue);
            void fn__12775(JoinClause jc__1320)
            {
                b__1317.AppendSafe(" ");
                string t___12763 = jc__1320.JoinType.Keyword();
                b__1317.AppendSafe(t___12763);
                b__1317.AppendSafe(" ");
                string t___12767 = jc__1320.Table.SqlValue;
                b__1317.AppendSafe(t___12767);
                SqlFragment ? oc__1321 = jc__1320.OnCondition;
                if (!(oc__1321 == null))
                {
                    SqlFragment oc___2478 = oc__1321!;
                    b__1317.AppendSafe(" ON ");
                    b__1317.AppendFragment(oc___2478);
                }
            }
            C::Listed.ForEach(this.joinClauses__1195, (S::Action<JoinClause>) fn__12775);
            if (!(this.conditions__1190.Count == 0))
            {
                b__1317.AppendSafe(" WHERE ");
                b__1317.AppendFragment(this.conditions__1190[0].Condition);
                int i__1322 = 1;
                while (true)
                {
                    t___12802 = this.conditions__1190.Count;
                    if (!(i__1322 < t___12802)) break;
                    b__1317.AppendSafe(" ");
                    b__1317.AppendSafe(this.conditions__1190[i__1322].Keyword());
                    b__1317.AppendSafe(" ");
                    b__1317.AppendFragment(this.conditions__1190[i__1322].Condition);
                    i__1322 = i__1322 + 1;
                }
            }
            if (!(this.groupByFields__1196.Count == 0))
            {
                b__1317.AppendSafe(" GROUP BY ");
                string fn__12774(ISafeIdentifier f__1323)
                {
                    return f__1323.SqlValue;
                }
                b__1317.AppendSafe(C::Listed.Join(this.groupByFields__1196, ", ", (S::Func<ISafeIdentifier, string>) fn__12774));
            }
            if (!(this.havingConditions__1197.Count == 0))
            {
                b__1317.AppendSafe(" HAVING ");
                b__1317.AppendFragment(this.havingConditions__1197[0].Condition);
                int i__1324 = 1;
                while (true)
                {
                    t___12821 = this.havingConditions__1197.Count;
                    if (!(i__1324 < t___12821)) break;
                    b__1317.AppendSafe(" ");
                    b__1317.AppendSafe(this.havingConditions__1197[i__1324].Keyword());
                    b__1317.AppendSafe(" ");
                    b__1317.AppendFragment(this.havingConditions__1197[i__1324].Condition);
                    i__1324 = i__1324 + 1;
                }
            }
            if (!(this.orderClauses__1192.Count == 0))
            {
                b__1317.AppendSafe(" ORDER BY ");
                bool first__1325 = true;
                void fn__12773(OrderClause orc__1326)
                {
                    string t___12758;
                    string t___6760;
                    if (!first__1325) b__1317.AppendSafe(", ");
                    first__1325 = false;
                    string t___12753 = orc__1326.Field.SqlValue;
                    b__1317.AppendSafe(t___12753);
                    if (orc__1326.Ascending)
                    {
                        t___6760 = " ASC";
                    }
                    else
                    {
                        t___6760 = " DESC";
                    }
                    b__1317.AppendSafe(t___6760);
                    INullsPosition ? np__1327 = orc__1326.NullsPos;
                    if (!(np__1327 == null))
                    {
                        t___12758 = (np__1327!).Keyword();
                        b__1317.AppendSafe(t___12758);
                    }
                }
                C::Listed.ForEach(this.orderClauses__1192, (S::Action<OrderClause>) fn__12773);
            }
            int ? lv__1328 = this.limitVal__1193;
            if (!(lv__1328 == null))
            {
                int lv___2480 = lv__1328.Value;
                b__1317.AppendSafe(" LIMIT ");
                b__1317.AppendInt32(lv___2480);
            }
            int ? ov__1329 = this.offsetVal__1194;
            if (!(ov__1329 == null))
            {
                int ov___2481 = ov__1329.Value;
                b__1317.AppendSafe(" OFFSET ");
                b__1317.AppendInt32(ov___2481);
            }
            ILockMode ? lm__1330 = this.lockMode__1200;
            if (!(lm__1330 == null)) b__1317.AppendSafe((lm__1330!).Keyword());
            return b__1317.Accumulated;
        }
        public SqlFragment CountSql()
        {
            int t___12722;
            int t___12741;
            SqlBuilder b__1333 = new SqlBuilder();
            b__1333.AppendSafe("SELECT COUNT(*) FROM ");
            b__1333.AppendSafe(this.tableName__1189.SqlValue);
            void fn__12710(JoinClause jc__1334)
            {
                b__1333.AppendSafe(" ");
                string t___12700 = jc__1334.JoinType.Keyword();
                b__1333.AppendSafe(t___12700);
                b__1333.AppendSafe(" ");
                string t___12704 = jc__1334.Table.SqlValue;
                b__1333.AppendSafe(t___12704);
                SqlFragment ? oc2__1335 = jc__1334.OnCondition;
                if (!(oc2__1335 == null))
                {
                    SqlFragment oc2___2483 = oc2__1335!;
                    b__1333.AppendSafe(" ON ");
                    b__1333.AppendFragment(oc2___2483);
                }
            }
            C::Listed.ForEach(this.joinClauses__1195, (S::Action<JoinClause>) fn__12710);
            if (!(this.conditions__1190.Count == 0))
            {
                b__1333.AppendSafe(" WHERE ");
                b__1333.AppendFragment(this.conditions__1190[0].Condition);
                int i__1336 = 1;
                while (true)
                {
                    t___12722 = this.conditions__1190.Count;
                    if (!(i__1336 < t___12722)) break;
                    b__1333.AppendSafe(" ");
                    b__1333.AppendSafe(this.conditions__1190[i__1336].Keyword());
                    b__1333.AppendSafe(" ");
                    b__1333.AppendFragment(this.conditions__1190[i__1336].Condition);
                    i__1336 = i__1336 + 1;
                }
            }
            if (!(this.groupByFields__1196.Count == 0))
            {
                b__1333.AppendSafe(" GROUP BY ");
                string fn__12709(ISafeIdentifier f__1337)
                {
                    return f__1337.SqlValue;
                }
                b__1333.AppendSafe(C::Listed.Join(this.groupByFields__1196, ", ", (S::Func<ISafeIdentifier, string>) fn__12709));
            }
            if (!(this.havingConditions__1197.Count == 0))
            {
                b__1333.AppendSafe(" HAVING ");
                b__1333.AppendFragment(this.havingConditions__1197[0].Condition);
                int i__1338 = 1;
                while (true)
                {
                    t___12741 = this.havingConditions__1197.Count;
                    if (!(i__1338 < t___12741)) break;
                    b__1333.AppendSafe(" ");
                    b__1333.AppendSafe(this.havingConditions__1197[i__1338].Keyword());
                    b__1333.AppendSafe(" ");
                    b__1333.AppendFragment(this.havingConditions__1197[i__1338].Condition);
                    i__1338 = i__1338 + 1;
                }
            }
            return b__1333.Accumulated;
        }
        public SqlFragment SafeToSql(int defaultLimit__1340)
        {
            SqlFragment return__520;
            Query t___6710;
            if (defaultLimit__1340 < 0) throw new S::Exception();
            if (!(this.limitVal__1193 == null))
            {
                return__520 = this.ToSql();
            }
            else
            {
                t___6710 = this.Limit(defaultLimit__1340);
                return__520 = t___6710.ToSql();
            }
            return return__520;
        }
        public Query(ISafeIdentifier tableName__1343, G::IReadOnlyList<IWhereClause> conditions__1344, G::IReadOnlyList<ISafeIdentifier> selectedFields__1345, G::IReadOnlyList<OrderClause> orderClauses__1346, int ? limitVal__1347, int ? offsetVal__1348, G::IReadOnlyList<JoinClause> joinClauses__1349, G::IReadOnlyList<ISafeIdentifier> groupByFields__1350, G::IReadOnlyList<IWhereClause> havingConditions__1351, bool isDistinct__1352, G::IReadOnlyList<SqlFragment> selectExprs__1353, ILockMode ? lockMode__1354)
        {
            this.tableName__1189 = tableName__1343;
            this.conditions__1190 = conditions__1344;
            this.selectedFields__1191 = selectedFields__1345;
            this.orderClauses__1192 = orderClauses__1346;
            this.limitVal__1193 = limitVal__1347;
            this.offsetVal__1194 = offsetVal__1348;
            this.joinClauses__1195 = joinClauses__1349;
            this.groupByFields__1196 = groupByFields__1350;
            this.havingConditions__1197 = havingConditions__1351;
            this.isDistinct__1198 = isDistinct__1352;
            this.selectExprs__1199 = selectExprs__1353;
            this.lockMode__1200 = lockMode__1354;
        }
        public ISafeIdentifier TableName
        {
            get
            {
                return this.tableName__1189;
            }
        }
        public G::IReadOnlyList<IWhereClause> Conditions
        {
            get
            {
                return this.conditions__1190;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> SelectedFields
        {
            get
            {
                return this.selectedFields__1191;
            }
        }
        public G::IReadOnlyList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses__1192;
            }
        }
        public int ? LimitVal
        {
            get
            {
                return this.limitVal__1193;
            }
        }
        public int ? OffsetVal
        {
            get
            {
                return this.offsetVal__1194;
            }
        }
        public G::IReadOnlyList<JoinClause> JoinClauses
        {
            get
            {
                return this.joinClauses__1195;
            }
        }
        public G::IReadOnlyList<ISafeIdentifier> GroupByFields
        {
            get
            {
                return this.groupByFields__1196;
            }
        }
        public G::IReadOnlyList<IWhereClause> HavingConditions
        {
            get
            {
                return this.havingConditions__1197;
            }
        }
        public bool IsDistinct
        {
            get
            {
                return this.isDistinct__1198;
            }
        }
        public G::IReadOnlyList<SqlFragment> SelectExprs
        {
            get
            {
                return this.selectExprs__1199;
            }
        }
        public ILockMode ? LockMode
        {
            get
            {
                return this.lockMode__1200;
            }
        }
    }
}
