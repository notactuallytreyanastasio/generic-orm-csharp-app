using G = System.Collections.Generic;
namespace Orm.Src
{
    public interface IChangeset
    {
        TableDef TableDef
        {
            get;
        }
        G::IReadOnlyDictionary<string, string> Changes
        {
            get;
        }
        G::IReadOnlyList<ChangesetError> Errors
        {
            get;
        }
        bool IsValid
        {
            get;
        }
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__363);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__366);
        IChangeset ValidateLength(ISafeIdentifier field__369, int min__370, int max__371);
        IChangeset ValidateInt(ISafeIdentifier field__374);
        IChangeset ValidateInt64(ISafeIdentifier field__377);
        IChangeset ValidateFloat(ISafeIdentifier field__380);
        IChangeset ValidateBool(ISafeIdentifier field__383);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__388);
    }
}
