using Microsoft.Extensions.DependencyInjection;
using NSwag;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Presentations.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void UseSwaggerMiddleware(this IServiceCollection services)
        {
            var apiVersion = GetApiVersion();

            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = $"V{apiVersion}";
                config.PostProcess = document =>
                {
                    document.Info.Version = apiVersion;
                    document.Info.Title = "Hexagonal Architecture Example";
                    document.Info.Description = "API Example";
                    SetMultipartFormDataForUploadFile(document);
                };
            });
        }

        private static void SetMultipartFormDataForUploadFile(OpenApiDocument document)
        {
            foreach (var operation in document.Operations)
            {
                var fileParameters = operation.Operation.Parameters
                    .Where(p => p.Type == NJsonSchema.JsonObjectType.File).ToList();

                if (fileParameters.Any())
                {
                    operation.Operation.RequestBody = new OpenApiRequestBody();

                    var requestBodyContent = new OpenApiMediaType
                    {
                        Schema = new NJsonSchema.JsonSchema()
                    };

                    requestBodyContent.Schema.Type = NJsonSchema.JsonObjectType.Object;

                    foreach (var fileParameter in fileParameters)
                    {
                        requestBodyContent.Schema.Properties.Add(fileParameter.Name, new NJsonSchema.JsonSchemaProperty
                        {
                            Type = NJsonSchema.JsonObjectType.String,
                            Format = "binary",
                            Description = fileParameter.Description
                        });

                        operation.Operation.Parameters.Remove(fileParameter);
                    }

                    operation.Operation.RequestBody.Content.Add("multipart/form-data", requestBodyContent);
                }
            }
        }

        private static string GetApiVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}