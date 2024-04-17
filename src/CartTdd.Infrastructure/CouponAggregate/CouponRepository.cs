using CardTdd.Domain.CouponAggregate;
using CartTdd.Infrastructure.Database;
using MongoDB.Driver;

namespace CartTdd.Infrastructure.CouponAggregate
{
    public class CouponRepository : ICouponRepository
    {

        private readonly DbContext _context;

        public CouponRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Coupon?> GetByCodeAsync(string code)
        {
            var filter = Builders<Coupon>.Filter.Eq(x => x.Code, code);
            var documents = await _context.Coupons.FindAsync<Coupon>(filter);
            return await documents.FirstOrDefaultAsync();
        }
    }
}
