namespace CardTdd.Domain.CartAggregate
{
    public class CartCoupon
    {
        public string Code { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime ExpirationDate { get; private set; }

        public CartCoupon(string code, decimal amount, DateTime expirationDate)
        {
            Code = code;
            Amount = amount;
            ExpirationDate = expirationDate;
        }
    }
}
