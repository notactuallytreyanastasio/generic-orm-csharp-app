namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1690;
        public string SqlValue
        {
            get
            {
                return this._value__1690;
            }
        }
        public ValidatedIdentifier(string _value__1694)
        {
            this._value__1690 = _value__1694;
        }
    }
}
