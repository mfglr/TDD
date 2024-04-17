using CardTdd.Domain.CouponAggregate;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
[assembly : InternalsVisibleTo("CartTdd.Domain.Tests")]
namespace CartTdd.Infrastructure.Database
{
    public class DbContext
    {
        internal IMongoClient Client { get; set; }
        internal string DatabaseName { get; set; }

        public IMongoCollection<Coupon> Coupons { get; set; }

        public DbContext(IOptions<DbSettings> settings)
        {
            DatabaseName = settings.Value.DatabaseName;
            Client = new MongoClient(settings.Value.ConnectionString);
            var database = Client.GetDatabase(settings.Value.DatabaseName);
            Coupons = database.GetCollection<Coupon>("coupons");
        }


    }
}
