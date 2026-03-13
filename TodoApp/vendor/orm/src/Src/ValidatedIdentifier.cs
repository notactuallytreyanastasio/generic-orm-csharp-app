namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1346;
        public string SqlValue
        {
            get
            {
                return this._value__1346;
            }
        }
        public ValidatedIdentifier(string _value__1350)
        {
            this._value__1346 = _value__1350;
        }
    }
}
