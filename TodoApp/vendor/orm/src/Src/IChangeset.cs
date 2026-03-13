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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__524);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__527);
        IChangeset ValidateLength(ISafeIdentifier field__530, int min__531, int max__532);
        IChangeset ValidateInt(ISafeIdentifier field__535);
        IChangeset ValidateInt64(ISafeIdentifier field__538);
        IChangeset ValidateFloat(ISafeIdentifier field__541);
        IChangeset ValidateBool(ISafeIdentifier field__544);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__549);
    }
}
