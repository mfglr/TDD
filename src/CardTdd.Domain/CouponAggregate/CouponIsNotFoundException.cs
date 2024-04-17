namespace CardTdd.Domain.CouponAggregate
{
    public class CouponIsNotFoundException : Exception
    {
        public CouponIsNotFoundException() : base("No coupon found!") { }
    }
}
