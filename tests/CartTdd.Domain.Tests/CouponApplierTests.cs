using CardTdd.Domain.CartAggregate;
using CardTdd.Domain.CouponAggregate;
using Moq;

namespace CartTdd.Domain.Tests
{
    public class CouponApplierTests
    {
        private readonly Cart _cart;

        public CouponApplierTests()
        {
            _cart = new Cart();
            _cart.AddProduct(new CardProduct("sku1", 2, 100));
            _cart.AddProduct(new CardProduct("sku2", 1, 200));
        }

        [Fact]
        public async Task Should_Succeed_When_ApplyCoupon()
        {
            var coupon = new Coupon("coupon100", 100M,DateTime.Now.AddDays(1));
            var couponRepository = new Mock<ICouponRepository>();
            couponRepository
                .Setup(x => x.GetByCodeAsync("coupon100"))
                .Returns(Task.FromResult<Coupon?>(coupon));

            var couponApplier = new CouponApplier(couponRepository.Object);
            await couponApplier.ApplyAsync("coupon100",_cart);

            Assert.Equal(300M, _cart.TotalPrice);
            Assert.Equal("coupon100", _cart.Coupon?.Code);
            Assert.Equal(100M, _cart.Coupon?.Amount);
        }

        [Fact]
        public async Task Should_ThrowExcepion_When_ApplyCoupon_If_CouponIsNotFound()
        {
            var coupon = new Coupon("coupon100", 100M, DateTime.Now.AddDays(1));
            var couponRepository = new Mock<ICouponRepository>();
            couponRepository
                .Setup(x => x.GetByCodeAsync("coupon100"))
                .Returns(Task.FromResult<Coupon?>(coupon));
            var couponApplier = new CouponApplier(couponRepository.Object);
            
            var exception = await Assert.ThrowsAsync<CouponIsNotFoundException>(
                async () => await couponApplier.ApplyAsync("coupon", _cart)
            );
            Assert.Equal("No coupon found!", exception.Message);
            Assert.Equal(400M, _cart.TotalPrice);
            Assert.Null(_cart.Coupon);
        }

        [Fact]
        public async Task Should_ThrowException_When_AppCoupon_If_CouponHasExpired()
        {
            var coupon = new Coupon("coupon100", 100M, DateTime.Now.AddDays(-1));
            var couponRepository = new Mock<ICouponRepository>();
            couponRepository
                .Setup(x => x.GetByCodeAsync("coupon100"))
                .Returns(Task.FromResult<Coupon?>(coupon));
            var couponApplier = new CouponApplier(couponRepository.Object);

            var exception = await Assert.ThrowsAsync<CouponHasExpiredException>(
                async () => await couponApplier.ApplyAsync("coupon100", _cart)
            );

            Assert.Equal("Coupon has expired!", exception.Message);
            Assert.Equal(400M, _cart.TotalPrice);
            Assert.Null(_cart.Coupon);
        }
    }
}
