namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__738;
        public string SqlValue
        {
            get
            {
                return this._value__738;
            }
        }
        public ValidatedIdentifier(string _value__742)
        {
            this._value__738 = _value__742;
        }
    }
}
