using GelStock.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GelStock.Tests.Helpers
{
    class DbContextFactory
    {
        public static GelStockDbContext Create()
        {
            var options = new DbContextOptionsBuilder<GelStockDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new GelStockDbContext(options);
        }
    }
}
