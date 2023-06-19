

using Microsoft.EntityFrameworkCore;

using Ottawa.Pigeon.Infrastructure.Persistence;

namespace Ottawa.Pigeon.UnitTests.Common
{
    public class OttawaPigeonTestDbContextFactory
    {
        public static OttawaPigeonDbContext Create()
        {
            var options = new DbContextOptionsBuilder<OttawaPigeonDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new OttawaPigeonDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(OttawaPigeonDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}