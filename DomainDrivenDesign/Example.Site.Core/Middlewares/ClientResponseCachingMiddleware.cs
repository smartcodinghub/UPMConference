using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Example.Site.Core.Core
{
    /// <summary>
    /// Middleware para añadir el header Cache-Control a las peticiones
    /// </summary>
    public class ClientResponseCachingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ClientResponseCachingOptions options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public ClientResponseCachingMiddleware(RequestDelegate next, ClientResponseCachingOptions options)
        {
            this.next = next;
            this.options = options;
        }

        /// <summary>
        /// Lógica
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InvokeAsync(HttpContext context)
        {
            if(options.TryGet(context.Request.Method, out CacheControlHeaderValue opts))
                context.Response.GetTypedHeaders().CacheControl = opts;

            return next(context);
        }
    }
}
