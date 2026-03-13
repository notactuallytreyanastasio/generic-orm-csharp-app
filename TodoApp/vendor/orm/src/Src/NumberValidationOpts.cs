namespace Orm.Src
{
    public class NumberValidationOpts
    {
        readonly double ? greaterThan__672;
        readonly double ? lessThan__673;
        readonly double ? greaterThanOrEqual__674;
        readonly double ? lessThanOrEqual__675;
        readonly double ? equalTo__676;
        public NumberValidationOpts(double ? greaterThan__678, double ? lessThan__679, double ? greaterThanOrEqual__680, double ? lessThanOrEqual__681, double ? equalTo__682)
        {
            this.greaterThan__672 = greaterThan__678;
            this.lessThan__673 = lessThan__679;
            this.greaterThanOrEqual__674 = greaterThanOrEqual__680;
            this.lessThanOrEqual__675 = lessThanOrEqual__681;
            this.equalTo__676 = equalTo__682;
        }
        public double ? GreaterThan
        {
            get
            {
                return this.greaterThan__672;
            }
        }
        public double ? LessThan
        {
            get
            {
                return this.lessThan__673;
            }
        }
        public double ? GreaterThanOrEqual
        {
            get
            {
                return this.greaterThanOrEqual__674;
            }
        }
        public double ? LessThanOrEqual
        {
            get
            {
                return this.lessThanOrEqual__675;
            }
        }
        public double ? EqualTo
        {
            get
            {
                return this.equalTo__676;
            }
        }
    }
}
