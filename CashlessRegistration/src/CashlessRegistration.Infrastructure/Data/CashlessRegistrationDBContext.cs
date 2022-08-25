using CashlessRegistration.Domain.Data;
using CashlessRegistration.Domain.Entities;
using CashlessRegistration.Infrastructure.EntityConfig;
using CashlessRegistration.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CashlessRegistration.Infrastructure.Data
{
    public class CashlessRegistrationDBContext : DbContext, IUnitOfWork
    {
        private readonly string _connectionString;

        public CashlessRegistrationDBContext(PostgresOptions options)
        {
            _connectionString = options.ConnectionString;
        }

        public DbSet<CustomerCard> CustomerCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerCardConfig());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString)
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                .UseSnakeCaseNamingConvention()
                .UseLazyLoadingProxies();

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
