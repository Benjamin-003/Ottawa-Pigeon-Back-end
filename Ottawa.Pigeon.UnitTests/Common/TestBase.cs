
using Ottawa.Pigeon.Infrastructure.Persistence;

namespace Ottawa.Pigeon.UnitTests.Common
{
    public class TestBase : IDisposable
    {
        protected readonly OttawaPigeonDbContext _context;
        public TestBase()
        {
            _context = OttawaPigeonTestDbContextFactory.Create();
        }
        public void Dispose()
        {
            OttawaPigeonTestDbContextFactory.Destroy(_context);
        }
    }
}