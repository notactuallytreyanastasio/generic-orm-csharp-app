namespace Orm.Src
{
    public class NumberValidationOpts
    {
        readonly double ? greaterThan__648;
        readonly double ? lessThan__649;
        readonly double ? greaterThanOrEqual__650;
        readonly double ? lessThanOrEqual__651;
        readonly double ? equalTo__652;
        public NumberValidationOpts(double ? greaterThan__654, double ? lessThan__655, double ? greaterThanOrEqual__656, double ? lessThanOrEqual__657, double ? equalTo__658)
        {
            this.greaterThan__648 = greaterThan__654;
            this.lessThan__649 = lessThan__655;
            this.greaterThanOrEqual__650 = greaterThanOrEqual__656;
            this.lessThanOrEqual__651 = lessThanOrEqual__657;
            this.equalTo__652 = equalTo__658;
        }
        public double ? GreaterThan
        {
            get
            {
                return this.greaterThan__648;
            }
        }
        public double ? LessThan
        {
            get
            {
                return this.lessThan__649;
            }
        }
        public double ? GreaterThanOrEqual
        {
            get
            {
                return this.greaterThanOrEqual__650;
            }
        }
        public double ? LessThanOrEqual
        {
            get
            {
                return this.lessThanOrEqual__651;
            }
        }
        public double ? EqualTo
        {
            get
            {
                return this.equalTo__652;
            }
        }
    }
}
