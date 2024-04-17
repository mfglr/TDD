
using CardTdd.Domain.CartAggregate;

namespace CardTdd.Domain.CouponAggregate
{
    public class CouponApplier
    {
        private readonly ICouponRepository _repository;

        public CouponApplier(ICouponRepository repository)
        {
            _repository = repository;
        }

        public async Task ApplyAsync(string code,Cart cart)
        {
            var coupon = await _repository.GetByCodeAsync(code) ?? throw new CouponIsNotFoundException();
            cart.ApplyCoupon(new CartCoupon(coupon.Code,coupon.Amount,coupon.ExpirationDate));
        }
    }
}
