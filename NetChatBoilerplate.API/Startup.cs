namespace NetChatBoilerplate.API
{
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NetChatBoilerplate.API.Constants;
    using NetChatBoilerplate.API.Extensions;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services
                .AddCustomOptions(this._configuration)
                .AddServerTiming()
                .AddCustomMvc(this._environment, this._configuration)
                .AddCustomResponseCompression(this._configuration)
                .AddCustomDbContext(this._configuration)
                .AddCustomSwagger()
                .AddCustomApiVersioning()
                .AddCustomHealthChecks();
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder application) =>
            application
                .UseIf(
                    this._environment.IsDevelopment(),
                    x => x.UseServerTiming())
                .UseRouting()
                .UseCors(AppConstants.CorsPolicy)
                .UseResponseCaching()
                .UseResponseCompression()
                .UseIf(
                    this._environment.IsDevelopment(),
                    x => x.UseDeveloperExceptionPage())
                .UseStaticFilesWithCacheControl()
                .UseCustomSerilogRequestLogging()
                .UseEndpoints(
                    builder =>
                    {
                        builder.MapControllers();
                        builder.MapHealthChecks("/status");
                        builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
                    })
                .UseSwagger()
                .UseCustomSwaggerUI();
    }
}
