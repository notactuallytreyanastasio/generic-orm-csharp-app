namespace Orm.Src
{
    class ValidatedIdentifier: ISafeIdentifier
    {
        readonly string _value__1116;
        public string SqlValue
        {
            get
            {
                return this._value__1116;
            }
        }
        public ValidatedIdentifier(string _value__1120)
        {
            this._value__1116 = _value__1120;
        }
    }
}
