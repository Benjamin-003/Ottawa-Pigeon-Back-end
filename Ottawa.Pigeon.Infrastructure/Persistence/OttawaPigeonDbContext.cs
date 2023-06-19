using Ottawa.Pigeon.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

using Ottawa.Pigeon.Domain.Entities;

using System.Reflection;

namespace Ottawa.Pigeon.Infrastructure.Persistence
{
    public partial class OttawaPigeonDbContext : DbContext , IOttawaPigeonDbContext
    {



        public OttawaPigeonDbContext(DbContextOptions<OttawaPigeonDbContext> options)
            : base(options)
        {
         
        }

        public DbSet<User> Users{ get; set; } = null!;
        public DbSet<Language> Languages{ get; set; } = null!;
        public DbSet<Currency> Currencies{ get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseCollation("French_CS_AS");
            base.OnModelCreating(modelBuilder);
        }

    }
}
