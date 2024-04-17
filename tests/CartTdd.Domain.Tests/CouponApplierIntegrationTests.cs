using CardTdd.Domain.CartAggregate;
using CardTdd.Domain.CouponAggregate;

namespace CartTdd.Domain.Tests
{
    public class CouponApplierIntegrationTests : IClassFixture<CouponApplierIntegrationTestsFixture>
    {
        private readonly Cart _cart;
        private readonly CouponApplierIntegrationTestsFixture _fixture;

        public CouponApplierIntegrationTests(CouponApplierIntegrationTestsFixture fixture)
        {
            _cart = new Cart();
            _cart.AddProduct(new CardProduct("sku1", 2, 100));
            _cart.AddProduct(new CardProduct("sku2", 1, 200));
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Succeed_When_ApplyCoupon()
        {
            await _fixture.CouponApplier.ApplyAsync("coupon100",_cart);

            Assert.Equal(300M, _cart.TotalPrice);
            Assert.Equal("coupon100", _cart.Coupon?.Code);
            Assert.Equal(100M, _cart.Coupon?.Amount);
        }

        [Fact]
        public async Task Should_ThrowExcepion_When_ApplyCoupon_If_CouponIsNotFound()
        {
            var exception = await Assert.ThrowsAsync<CouponIsNotFoundException>(
                async () => await _fixture.CouponApplier.ApplyAsync("coupon", _cart)
            );
            Assert.Equal("No coupon found!", exception.Message);
            Assert.Equal(400M, _cart.TotalPrice);
            Assert.Null(_cart.Coupon);
        }

        [Fact]
        public async Task Should_ThrowException_When_AppCoupon_If_CouponHasExpired()
        {
            var exception = await Assert.ThrowsAsync<CouponHasExpiredException>(
                async () => await _fixture.CouponApplier.ApplyAsync("expired_coupon", _cart)
            );

            Assert.Equal("Coupon has expired!", exception.Message);
            Assert.Equal(400M, _cart.TotalPrice);
            Assert.Null(_cart.Coupon);
        }
    }
}
