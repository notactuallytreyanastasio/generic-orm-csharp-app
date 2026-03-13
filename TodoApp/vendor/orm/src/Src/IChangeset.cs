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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__692);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__695);
        IChangeset ValidateLength(ISafeIdentifier field__698, int min__699, int max__700);
        IChangeset ValidateInt(ISafeIdentifier field__703);
        IChangeset ValidateInt64(ISafeIdentifier field__706);
        IChangeset ValidateFloat(ISafeIdentifier field__709);
        IChangeset ValidateBool(ISafeIdentifier field__712);
        IChangeset PutChange(ISafeIdentifier field__715, string value__716);
        string GetChange(ISafeIdentifier field__719);
        IChangeset DeleteChange(ISafeIdentifier field__722);
        IChangeset ValidateInclusion(ISafeIdentifier field__725, G::IReadOnlyList<string> allowed__726);
        IChangeset ValidateExclusion(ISafeIdentifier field__729, G::IReadOnlyList<string> disallowed__730);
        IChangeset ValidateNumber(ISafeIdentifier field__733, NumberValidationOpts opts__734);
        IChangeset ValidateAcceptance(ISafeIdentifier field__737);
        IChangeset ValidateConfirmation(ISafeIdentifier field__740, ISafeIdentifier confirmationField__741);
        IChangeset ValidateContains(ISafeIdentifier field__744, string substring__745);
        IChangeset ValidateStartsWith(ISafeIdentifier field__748, string prefix__749);
        IChangeset ValidateEndsWith(ISafeIdentifier field__752, string suffix__753);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__758);
    }
}
