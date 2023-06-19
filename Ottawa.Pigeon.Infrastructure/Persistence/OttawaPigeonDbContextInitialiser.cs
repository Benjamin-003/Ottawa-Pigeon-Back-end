
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ottawa.Pigeon.Infrastructure.Persistence;

public class OttawaPigeonDbContextInitialiser
{
    private readonly ILogger<OttawaPigeonDbContextInitialiser> _logger;
    private readonly OttawaPigeonDbContext _context;

    public OttawaPigeonDbContextInitialiser(ILogger<OttawaPigeonDbContextInitialiser> logger, OttawaPigeonDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //TODO : add something in Db if needed
        await _context.SaveChangesAsync();

    }
}
