using CashlessRegistration.Domain.Repositories;
using CashlessRegistration.Infrastructure.Data;
using CashlessRegistration.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashlessRegistration.Infrastructure
{
    public static class DIExtension
    {
        public static IServiceCollection AddCashlessRegistrationDBContext(this IServiceCollection services)
        {
            services.AddDbContext<CashlessRegistrationDBContext>(options => options.UseInMemoryDatabase("CashlessRegistrationDB"));
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerCardRepository, CustomerCardRepository>();
        }
    }
}
