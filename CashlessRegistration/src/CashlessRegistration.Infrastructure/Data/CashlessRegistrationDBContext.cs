using CashlessRegistration.Domain.Data;
using CashlessRegistration.Domain.Entities;
using CashlessRegistration.Infrastructure.EntityConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CashlessRegistration.Infrastructure.Data
{
    public class CashlessRegistrationDBContext : DbContext, IUnitOfWork
    {
        public CashlessRegistrationDBContext(DbContextOptions<CashlessRegistrationDBContext> options) 
            : base(options)
        {
        }

        public DbSet<CustomerCard> CustomerCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerCardConfig());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole((_) => { })
                    .AddFilter((_, _) => true);
            });

            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
