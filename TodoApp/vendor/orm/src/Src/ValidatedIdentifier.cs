namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1249;
        public string SqlValue
        {
            get
            {
                return this._value__1249;
            }
        }
        public ValidatedIdentifier(string _value__1253)
        {
            this._value__1249 = _value__1253;
        }
    }
}
