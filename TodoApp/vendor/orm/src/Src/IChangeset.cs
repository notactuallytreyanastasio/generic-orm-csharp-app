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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__480);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__483);
        IChangeset ValidateLength(ISafeIdentifier field__486, int min__487, int max__488);
        IChangeset ValidateInt(ISafeIdentifier field__491);
        IChangeset ValidateInt64(ISafeIdentifier field__494);
        IChangeset ValidateFloat(ISafeIdentifier field__497);
        IChangeset ValidateBool(ISafeIdentifier field__500);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__505);
    }
}
