namespace CardTdd.Domain.CartAggregate
{
    public class CardProduct
    {
        public string Sku { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalPrice => Price * Quantity;

        public CardProduct(string sku, int quantity, decimal price)
        {
            Sku = sku;
            Quantity = quantity;
            Price = price;
        }

        internal void IncreaseQuantity() => Quantity++;
        internal void DecreaseQuantity() => Quantity--;
    }
}