using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Infrastructure.Swagger
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated |= apiDescription.IsDeprecated();
            if (operation.Parameters != null)
            {
                foreach (var parameter in operation.Parameters)
                {
                   var description = apiDescription.ParameterDescriptions.First(x => x.Name == parameter.Name);
                   if (string.IsNullOrEmpty(parameter.Description))
                   {
                       parameter.Description = description.ModelMetadata?.Description;
                   }

                   parameter.Required |= description.IsRequired;
                }
            }
        }
    }
}