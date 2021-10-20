namespace NetChatBoilerplate.API.Extensions
{
    using System;
    using System.Linq;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using NetChatBoilerplate.API.Constants;
    using NetChatBoilerplate.API.Infrastructure.Options;
    using Serilog;
    using Serilog.Events;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSerilogRequestLogging(this IApplicationBuilder application) =>
                application.UseSerilogRequestLogging(
                    options =>
                    {
                        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                        {
                            var request = httpContext.Request;
                            var response = httpContext.Response;
                            var endpoint = httpContext.GetEndpoint();

                            diagnosticContext.Set("Host", request.Host);
                            diagnosticContext.Set("Protocol", request.Protocol);
                            diagnosticContext.Set("Scheme", request.Scheme);

                            if (request.QueryString.HasValue)
                            {
                                diagnosticContext.Set("QueryString", request.QueryString.Value);
                            }

                            if (endpoint is not null)
                            {
                                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
                            }

                            diagnosticContext.Set("ContentType", response.ContentType);
                        };
                    options.GetLevel = GetLevel;
                });

        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application) =>
            application.UseSwaggerUI(
                options =>
                {
                    // Set the Swagger UI browser document title.
                    options.DocumentTitle = AssemblyInformation.Current.Product;
                    // Set the Swagger UI to render at '/docs'.
                    options.RoutePrefix = "docs";

                    options.DisplayOperationId();
                    options.DisplayRequestDuration();

                    var provider = application.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider
                        .ApiVersionDescriptions
                        .OrderByDescending(x => x.ApiVersion))
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                            $"Version {apiVersionDescription.ApiVersion}");
                    }
                });

        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            var cacheProfile = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptions>()
                .Where(x => string.Equals(x.Key, AppConstants.CacheProfileName, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault();

            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            if (cacheProfile is not null)
                            {
                                context.Context.ApplyCacheProfile(cacheProfile);
                            }
                        },
                    });
        }

        private static LogEventLevel GetLevel(HttpContext httpContext, double elapsedMilliseconds, Exception exception)
        {
            if (exception == null && httpContext.Response.StatusCode <= 499)
            {
                if (IsHealthCheckEndpoint(httpContext))
                {
                    return LogEventLevel.Verbose;
                }

                return LogEventLevel.Information;
            }

            return LogEventLevel.Error;
        }

        private static bool IsHealthCheckEndpoint(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is not null)
            {
                return endpoint.DisplayName == "Health checks";
            }

            return false;
        }
    }
}
