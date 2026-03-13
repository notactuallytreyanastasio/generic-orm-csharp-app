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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__739);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__742);
        IChangeset ValidateLength(ISafeIdentifier field__745, int min__746, int max__747);
        IChangeset ValidateInt(ISafeIdentifier field__750);
        IChangeset ValidateInt64(ISafeIdentifier field__753);
        IChangeset ValidateFloat(ISafeIdentifier field__756);
        IChangeset ValidateBool(ISafeIdentifier field__759);
        IChangeset PutChange(ISafeIdentifier field__762, string value__763);
        string GetChange(ISafeIdentifier field__766);
        IChangeset DeleteChange(ISafeIdentifier field__769);
        IChangeset ValidateInclusion(ISafeIdentifier field__772, G::IReadOnlyList<string> allowed__773);
        IChangeset ValidateExclusion(ISafeIdentifier field__776, G::IReadOnlyList<string> disallowed__777);
        IChangeset ValidateNumber(ISafeIdentifier field__780, NumberValidationOpts opts__781);
        IChangeset ValidateAcceptance(ISafeIdentifier field__784);
        IChangeset ValidateConfirmation(ISafeIdentifier field__787, ISafeIdentifier confirmationField__788);
        IChangeset ValidateContains(ISafeIdentifier field__791, string substring__792);
        IChangeset ValidateStartsWith(ISafeIdentifier field__795, string prefix__796);
        IChangeset ValidateEndsWith(ISafeIdentifier field__799, string suffix__800);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__805);
    }
}
