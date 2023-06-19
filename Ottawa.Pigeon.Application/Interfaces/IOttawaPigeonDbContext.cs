using Microsoft.EntityFrameworkCore;

using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Application.Common.Interfaces
{
    public interface IOttawaPigeonDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Language> Languages { get; }
        DbSet<Currency> Currencies{ get; }
        DbSet<Subscription> Subscriptions{ get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}