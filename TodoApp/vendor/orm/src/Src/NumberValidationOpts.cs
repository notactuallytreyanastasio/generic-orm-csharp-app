namespace Orm.Src
{
    public class NumberValidationOpts
    {
        readonly double ? greaterThan__719;
        readonly double ? lessThan__720;
        readonly double ? greaterThanOrEqual__721;
        readonly double ? lessThanOrEqual__722;
        readonly double ? equalTo__723;
        public NumberValidationOpts(double ? greaterThan__725, double ? lessThan__726, double ? greaterThanOrEqual__727, double ? lessThanOrEqual__728, double ? equalTo__729)
        {
            this.greaterThan__719 = greaterThan__725;
            this.lessThan__720 = lessThan__726;
            this.greaterThanOrEqual__721 = greaterThanOrEqual__727;
            this.lessThanOrEqual__722 = lessThanOrEqual__728;
            this.equalTo__723 = equalTo__729;
        }
        public double ? GreaterThan
        {
            get
            {
                return this.greaterThan__719;
            }
        }
        public double ? LessThan
        {
            get
            {
                return this.lessThan__720;
            }
        }
        public double ? GreaterThanOrEqual
        {
            get
            {
                return this.greaterThanOrEqual__721;
            }
        }
        public double ? LessThanOrEqual
        {
            get
            {
                return this.lessThanOrEqual__722;
            }
        }
        public double ? EqualTo
        {
            get
            {
                return this.equalTo__723;
            }
        }
    }
}
