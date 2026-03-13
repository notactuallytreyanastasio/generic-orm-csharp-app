namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__630;
        public string SqlValue
        {
            get
            {
                return this._value__630;
            }
        }
        public ValidatedIdentifier(string _value__634)
        {
            this._value__630 = _value__634;
        }
    }
}
