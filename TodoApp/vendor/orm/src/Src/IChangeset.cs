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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__565);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__568);
        IChangeset ValidateLength(ISafeIdentifier field__571, int min__572, int max__573);
        IChangeset ValidateInt(ISafeIdentifier field__576);
        IChangeset ValidateInt64(ISafeIdentifier field__579);
        IChangeset ValidateFloat(ISafeIdentifier field__582);
        IChangeset ValidateBool(ISafeIdentifier field__585);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__590);
    }
}
