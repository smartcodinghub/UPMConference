using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Example.Site.Core.Core
{
    /// <summary>
    /// Filtro de Swashbuckle.Swagger para que identifique si una determinada operación tiene activada la seguridad OAuth2 y el swagger la interprete como tal.
    /// </summary>
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Aplica el filtro específico
        /// Applies the specified operation.
        /// Se evalua la presencia de filtros de tipo Authorize tanto globales como específicos y si se encuentran se marca la operación como segura (oauth2) para Swagger.
        /// </summary>
        /// <param name="operation">La operación.</param>
        /// <param name="context">El esquema del registro.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            // Policy names map to scopes
            var controllerScopes = context.ApiDescription.ControllerAttributes()
                .OfType<AuthorizeAttribute>();

            var actionScopes = context.ApiDescription.ActionAttributes()
                .OfType<AuthorizeAttribute>();

            var requiredScopes = controllerScopes.Union(actionScopes).Distinct();

            if(requiredScopes.Any())
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                {
                    ["oauth2"] = new List<string>() { "api" }
                });
            }
        }
    }
}
