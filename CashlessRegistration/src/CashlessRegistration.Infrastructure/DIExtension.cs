using CashlessRegistration.Domain.Repositories;
using CashlessRegistration.Infrastructure.Data;
using CashlessRegistration.Infrastructure.Options;
using CashlessRegistration.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashlessRegistration.Infrastructure
{
    public static class DIExtension
    {
        public static IServiceCollection AddCashlessRegistrationDBContext(this IServiceCollection services)
        {
            services.AddPostgreConfiguration();
            services.AddScoped<CashlessRegistrationDBContext>();
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerCardRepository, CustomerCardRepository>();
        }

        private static IServiceCollection AddPostgreConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetOptions<PostgresOptions>("POSTGRES");
            });

            return services;
        }
    }
}
