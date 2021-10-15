namespace NetChatBoilerplate.API.Infrastructure.Extensions
{
    using Boxed.AspNetCore.Swagger;
    using Boxed.AspNetCore.Swagger.OperationFilters;
    using Boxed.AspNetCore.Swagger.SchemaFilters;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using NetChatBoilerplate.API.Infrastructure.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this._provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.DescribeAllParametersInCamelCase();
            options.EnableAnnotations();

            // Add the XML comment file for this assembly, so its contents can be displayed.
            options.IncludeXmlCommentsIfExists(typeof(Startup).Assembly);

            options.OperationFilter<ApiVersionOperationFilter>();
            options.OperationFilter<ClaimsOperationFilter>();
            options.OperationFilter<ForbiddenResponseOperationFilter>();
            options.OperationFilter<UnauthorizedResponseOperationFilter>();

            // Show a default and example model for JsonPatchDocument<T>.
            options.SchemaFilter<JsonPatchDocumentSchemaFilter>();

            foreach (var apiVersionDescription in this._provider.ApiVersionDescriptions)
            {
                var info = new OpenApiInfo()
                {
                    Title = "Test Title",
                    Description = apiVersionDescription.IsDeprecated ?
                        $"This API version has been deprecated." :
                        "Test Description",
                    Version = apiVersionDescription.ApiVersion.ToString(),
                };
                options.SwaggerDoc(apiVersionDescription.GroupName, info);
            }
        }
    }
}
