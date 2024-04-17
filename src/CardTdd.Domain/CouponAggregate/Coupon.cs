namespace CardTdd.Domain.CouponAggregate
{
    public class Coupon
    {
        public string Code { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public Coupon(string code, decimal amount,DateTime expirationDate)
        {
            Code = code;
            Amount = amount;
            ExpirationDate = expirationDate;
        }
    }
}
