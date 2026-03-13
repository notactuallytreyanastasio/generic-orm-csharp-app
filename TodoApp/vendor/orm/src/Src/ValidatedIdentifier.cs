namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1763;
        public string SqlValue
        {
            get
            {
                return this._value__1763;
            }
        }
        public ValidatedIdentifier(string _value__1767)
        {
            this._value__1763 = _value__1767;
        }
    }
}
