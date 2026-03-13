using S = System;
using G = System.Collections.Generic;
using C = TemperLang.Core;
namespace Orm.Src
{
    public static class SrcGlobal
    {
        public static IChangeset Changeset(TableDef tableDef__583, G::IReadOnlyDictionary<string, string> params__584)
        {
            G::IReadOnlyDictionary<string, string> t___8567 = C::Mapped.ConstructMap(C::Listed.CreateReadOnlyList<G::KeyValuePair<string, string>>());
            return new ChangesetImpl(tableDef__583, params__584, t___8567, C::Listed.CreateReadOnlyList<ChangesetError>(), true);
        }
        internal static bool isIdentStart__444(int c__1037)
        {
            bool return__369;
            bool t___4824;
            bool t___4825;
            if (c__1037 >= 97)
            {
                t___4824 = c__1037 <= 122;
            }
            else
            {
                t___4824 = false;
            }
            if (t___4824)
            {
                return__369 = true;
            }
            else
            {
                if (c__1037 >= 65)
                {
                    t___4825 = c__1037 <= 90;
                }
                else
                {
                    t___4825 = false;
                }
                if (t___4825)
                {
                    return__369 = true;
                }
                else
                {
                    return__369 = c__1037 == 95;
                }
            }
            return return__369;
        }
        internal static bool isIdentPart__445(int c__1039)
        {
            bool return__370;
            if (isIdentStart__444(c__1039))
            {
                return__370 = true;
            }
            else if (c__1039 >= 48)
            {
                return__370 = c__1039 <= 57;
            }
            else
            {
                return__370 = false;
            }
            return return__370;
        }
        public static ISafeIdentifier SafeIdentifier(string name__1041)
        {
            int t___8565;
            if (string.IsNullOrEmpty(name__1041)) throw new S::Exception();
            int idx__1043 = 0;
            if (!isIdentStart__444(C::StringUtil.Get(name__1041, idx__1043))) throw new S::Exception();
            int t___8562 = C::StringUtil.Next(name__1041, idx__1043);
            idx__1043 = t___8562;
            while (true)
            {
                if (!C::StringUtil.HasIndex(name__1041, idx__1043)) break;
                if (!isIdentPart__445(C::StringUtil.Get(name__1041, idx__1043))) throw new S::Exception();
                t___8565 = C::StringUtil.Next(name__1041, idx__1043);
                idx__1043 = t___8565;
            }
            return new ValidatedIdentifier(name__1041);
        }
        public static SqlFragment DeleteSql(TableDef tableDef__673, int id__674)
        {
            SqlBuilder b__676 = new SqlBuilder();
            b__676.AppendSafe("DELETE FROM ");
            b__676.AppendSafe(tableDef__673.TableName.SqlValue);
            b__676.AppendSafe(" WHERE id = ");
            b__676.AppendInt32(id__674);
            return b__676.Accumulated;
        }
        public static Query From(ISafeIdentifier tableName__863)
        {
            return new Query(tableName__863, C::Listed.CreateReadOnlyList<IWhereClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<OrderClause>(), null, null, C::Listed.CreateReadOnlyList<JoinClause>(), C::Listed.CreateReadOnlyList<ISafeIdentifier>(), C::Listed.CreateReadOnlyList<IWhereClause>(), false, C::Listed.CreateReadOnlyList<SqlFragment>());
        }
        public static SqlFragment Col(ISafeIdentifier table__865, ISafeIdentifier column__866)
        {
            SqlBuilder b__868 = new SqlBuilder();
            b__868.AppendSafe(table__865.SqlValue);
            b__868.AppendSafe(".");
            b__868.AppendSafe(column__866.SqlValue);
            return b__868.Accumulated;
        }
        public static SqlFragment CountAll()
        {
            SqlBuilder b__870 = new SqlBuilder();
            b__870.AppendSafe("COUNT(*)");
            return b__870.Accumulated;
        }
        public static SqlFragment CountCol(ISafeIdentifier field__871)
        {
            SqlBuilder b__873 = new SqlBuilder();
            b__873.AppendSafe("COUNT(");
            b__873.AppendSafe(field__871.SqlValue);
            b__873.AppendSafe(")");
            return b__873.Accumulated;
        }
        public static SqlFragment SumCol(ISafeIdentifier field__874)
        {
            SqlBuilder b__876 = new SqlBuilder();
            b__876.AppendSafe("SUM(");
            b__876.AppendSafe(field__874.SqlValue);
            b__876.AppendSafe(")");
            return b__876.Accumulated;
        }
        public static SqlFragment AvgCol(ISafeIdentifier field__877)
        {
            SqlBuilder b__879 = new SqlBuilder();
            b__879.AppendSafe("AVG(");
            b__879.AppendSafe(field__877.SqlValue);
            b__879.AppendSafe(")");
            return b__879.Accumulated;
        }
        public static SqlFragment MinCol(ISafeIdentifier field__880)
        {
            SqlBuilder b__882 = new SqlBuilder();
            b__882.AppendSafe("MIN(");
            b__882.AppendSafe(field__880.SqlValue);
            b__882.AppendSafe(")");
            return b__882.Accumulated;
        }
        public static SqlFragment MaxCol(ISafeIdentifier field__883)
        {
            SqlBuilder b__885 = new SqlBuilder();
            b__885.AppendSafe("MAX(");
            b__885.AppendSafe(field__883.SqlValue);
            b__885.AppendSafe(")");
            return b__885.Accumulated;
        }
    }
}
