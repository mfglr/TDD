namespace CardTdd.Domain.CouponAggregate
{
    public interface ICouponRepository
    {
        Task<Coupon?> GetByCodeAsync(string code);
    }
}