using System;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Example.Site.Core.Core
{
    /// <summary>
    /// Añade el tipo de respuesta al Swagger
    /// </summary>
    public class ResponseTypeOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Añade el tipo de respuesta cuando es un IRequest
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            Type requestInterface = context.ApiDescription.ParameterDescriptions
                .SelectMany(p => p.Type.GetInterfaces()
                    .Where(i => i.Name.StartsWith("IRequest", StringComparison.InvariantCulture)
                        && i.IsGenericType))
                    .FirstOrDefault();

            if(requestInterface != null)
            {
                Type responseType = requestInterface.GetGenericArguments()[0];
                var key = "200";

                if(!operation.Responses.TryGetValue(key, out Response response))
                {
                    response = new Response();
                }

                response.Schema = context.SchemaRegistry.GetOrRegister(responseType);

                operation.Responses[key] = response;
            }
        }
    }
}
