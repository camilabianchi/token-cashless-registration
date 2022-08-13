using CashlessRegistration.Application.Services;
using CashlessRegistration.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CashlessRegistration.Application
{
    public static class DIExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerCardService, CustomerCardService>();
        }
    }
}
