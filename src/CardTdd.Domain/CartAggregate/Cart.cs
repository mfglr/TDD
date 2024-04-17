using CardTdd.Domain.CouponAggregate;

namespace CardTdd.Domain.CartAggregate
{
    public class Cart
    {
        public decimal TotalPrice => _products.Sum(p => p.TotalPrice) - (Coupon?.Amount ?? 0);
        public CartCoupon? Coupon { get; private set; }
        public IReadOnlyList<CardProduct> Products => _products;
        private readonly List<CardProduct> _products = new();

        public void AddProduct(CardProduct product)
        {
            _products.Add(product);
        }

        public void RemoveProduct(string sku)
        {
            var product = 
                _products.FirstOrDefault(p => p.Sku == sku) ?? 
                throw new CartProductIsNotFoundException();
            _products.Remove(product);
        }

        public void IncreaseProductQuantity(string sku)
        {
            var product =
                _products.FirstOrDefault(p => p.Sku == sku) ??
                throw new CartProductIsNotFoundException();
            product.IncreaseQuantity();
        }

        public void DecreaseProductQuantity(string sku)
        {
            var product =
                _products.FirstOrDefault(p => p.Sku == sku) ??
                throw new CartProductIsNotFoundException();

            if(product.Quantity == 1)
            {
                RemoveProduct(sku);
                return;
            }
            product.DecreaseQuantity();
        }

        public void Clear() => _products.Clear();

        public void ApplyCoupon(CartCoupon coupon)
        {

            if (coupon.ExpirationDate < DateTime.Now)
                throw new CouponHasExpiredException();
            Coupon = coupon;
            
        }

    }
}
