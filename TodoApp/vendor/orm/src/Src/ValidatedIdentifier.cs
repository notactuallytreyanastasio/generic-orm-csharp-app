namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1032;
        public string SqlValue
        {
            get
            {
                return this._value__1032;
            }
        }
        public ValidatedIdentifier(string _value__1036)
        {
            this._value__1032 = _value__1036;
        }
    }
}
