namespace NetChatBoilerplate.API.Extensions
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using NetChatBoilerplate.API.Infrastructure.Extensions;
    using NetChatBoilerplate.Infrastructure;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class CustomServiceExtensions
    {
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>
            services
            .AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
            .AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV"); // Version format: 'v'major[.minor][-status]

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CosmosContext>(options =>
                options.UseCosmos(
                    configuration["Cosmos:ConnectionString"],
                    configuration["Cosmos:DatabaseName"])
            );

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddControllers()
               .AddJsonOptions(
                options =>
                {
                    var jsonSerializerOptions = options.JsonSerializerOptions;
                    if (environment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        jsonSerializerOptions.WriteIndented = true;
                    }

                    jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
               .AddXmlDataContractSerializerFormatters();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()));

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) =>
            services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
            .AddSwaggerGen();
    }
}
