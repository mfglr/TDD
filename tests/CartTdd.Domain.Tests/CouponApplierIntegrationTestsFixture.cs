using CardTdd.Domain.CouponAggregate;
using CartTdd.Infrastructure.CouponAggregate;
using CartTdd.Infrastructure.Database;
using Microsoft.Extensions.Options;

namespace CartTdd.Domain.Tests
{
    public class CouponApplierIntegrationTestsFixture : IDisposable
    {
        private readonly DbContext _dbContext;
        public readonly CouponApplier CouponApplier;

        public CouponApplierIntegrationTestsFixture()
        {
            var connectionString = "mongodb://localhost:27017";
            var database = $"CartTdd-{Guid.NewGuid()}";
            _dbContext = new DbContext(Options.Create(new DbSettings(connectionString, database)));
            CouponApplier = new CouponApplier(new CouponRepository(_dbContext));

            var c = new Coupon("coupon100", 100, DateTime.Now.AddDays(1));
            c.SetId();
            var ec = new Coupon("expired_coupon", 100, DateTime.Now.AddDays(-1));
            ec.SetId();
            _dbContext.Coupons.InsertOne(c);
            _dbContext.Coupons.InsertOne(ec);
        }

        public void Dispose() => _dbContext.Client.DropDatabase(_dbContext.DatabaseName);
    }
}
