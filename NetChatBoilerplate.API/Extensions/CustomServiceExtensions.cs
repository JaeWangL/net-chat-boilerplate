namespace NetChatBoilerplate.API.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NetChatBoilerplate.Infrastructure;

    public static class CustomServiceExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CosmosContext>(options =>
                options.UseCosmos(
                    configuration["Cosmos:ConnectionString"],
                    configuration["Cosmos:DatabaseName"])
            );

            return services;
        }
    }
}
