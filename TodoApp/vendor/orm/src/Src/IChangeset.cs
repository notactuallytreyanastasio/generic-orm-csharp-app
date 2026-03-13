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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__419);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__422);
        IChangeset ValidateLength(ISafeIdentifier field__425, int min__426, int max__427);
        IChangeset ValidateInt(ISafeIdentifier field__430);
        IChangeset ValidateInt64(ISafeIdentifier field__433);
        IChangeset ValidateFloat(ISafeIdentifier field__436);
        IChangeset ValidateBool(ISafeIdentifier field__439);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__444);
    }
}
