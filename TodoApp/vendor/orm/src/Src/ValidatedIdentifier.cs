namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__894;
        public string SqlValue
        {
            get
            {
                return this._value__894;
            }
        }
        public ValidatedIdentifier(string _value__898)
        {
            this._value__894 = _value__898;
        }
    }
}
