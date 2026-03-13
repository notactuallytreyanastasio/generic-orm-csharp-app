namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1901;
        public string SqlValue
        {
            get
            {
                return this._value__1901;
            }
        }
        public ValidatedIdentifier(string _value__1905)
        {
            this._value__1901 = _value__1905;
        }
    }
}
