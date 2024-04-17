namespace CardTdd.Domain.CouponAggregate
{
    public class Coupon
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public void SetId() => Id = Guid.NewGuid();

        public Coupon(string code, decimal amount,DateTime expirationDate)
        {
            Code = code;
            Amount = amount;
            ExpirationDate = expirationDate;
        }
    }
}
