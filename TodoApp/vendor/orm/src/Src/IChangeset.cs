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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__462);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__465);
        IChangeset ValidateLength(ISafeIdentifier field__468, int min__469, int max__470);
        IChangeset ValidateInt(ISafeIdentifier field__473);
        IChangeset ValidateInt64(ISafeIdentifier field__476);
        IChangeset ValidateFloat(ISafeIdentifier field__479);
        IChangeset ValidateBool(ISafeIdentifier field__482);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__487);
    }
}
