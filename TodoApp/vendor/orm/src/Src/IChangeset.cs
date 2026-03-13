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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__323);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__326);
        IChangeset ValidateLength(ISafeIdentifier field__329, int min__330, int max__331);
        IChangeset ValidateInt(ISafeIdentifier field__334);
        IChangeset ValidateInt64(ISafeIdentifier field__337);
        IChangeset ValidateFloat(ISafeIdentifier field__340);
        IChangeset ValidateBool(ISafeIdentifier field__343);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__348);
    }
}
