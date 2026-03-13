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
        IChangeset Cast(G::IReadOnlyList<ISafeIdentifier> allowedFields__668);
        IChangeset ValidateRequired(G::IReadOnlyList<ISafeIdentifier> fields__671);
        IChangeset ValidateLength(ISafeIdentifier field__674, int min__675, int max__676);
        IChangeset ValidateInt(ISafeIdentifier field__679);
        IChangeset ValidateInt64(ISafeIdentifier field__682);
        IChangeset ValidateFloat(ISafeIdentifier field__685);
        IChangeset ValidateBool(ISafeIdentifier field__688);
        IChangeset PutChange(ISafeIdentifier field__691, string value__692);
        string GetChange(ISafeIdentifier field__695);
        IChangeset DeleteChange(ISafeIdentifier field__698);
        IChangeset ValidateInclusion(ISafeIdentifier field__701, G::IReadOnlyList<string> allowed__702);
        IChangeset ValidateExclusion(ISafeIdentifier field__705, G::IReadOnlyList<string> disallowed__706);
        IChangeset ValidateNumber(ISafeIdentifier field__709, NumberValidationOpts opts__710);
        IChangeset ValidateAcceptance(ISafeIdentifier field__713);
        IChangeset ValidateConfirmation(ISafeIdentifier field__716, ISafeIdentifier confirmationField__717);
        IChangeset ValidateContains(ISafeIdentifier field__720, string substring__721);
        IChangeset ValidateStartsWith(ISafeIdentifier field__724, string prefix__725);
        IChangeset ValidateEndsWith(ISafeIdentifier field__728, string suffix__729);
        SqlFragment ToInsertSql();
        SqlFragment ToUpdateSql(int id__734);
    }
}
