using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Net.Http.Headers;

namespace Example.Site.Core.Core
{
    /// <summary>
    /// Middleware para añadir el header Cache-Control a las peticiones
    /// </summary>
    public static class ClientResponseCachingMiddlewareExtensions
    {
        /// <summary>
        /// Añade un ClientResponseCachingMiddleware al pipeline de Kestrel
        /// </summary>
        /// <param name="app">El builder</param>
        /// <param name="modifyOptions">Las opciones a usar</param>
        /// <returns></returns>
        public static IApplicationBuilder UseClientResponseCaching(this IApplicationBuilder app, Action<ClientResponseCachingOptions> modifyOptions)
        {
            ClientResponseCachingOptions options = new ClientResponseCachingOptions();
            modifyOptions(options);
            return app.UseMiddleware<ClientResponseCachingMiddleware>(options);
        }

        /// <summary>
        /// Añade un ClientResponseCachingMiddleware al pipeline de Kestrel
        /// </summary>
        /// <param name="config">La configuración a modificar</param>
        /// <param name="modify">Las opciones a usar</param>
        /// <returns></returns>
        public static void Modify(this CacheControlHeaderValue config, Action<CacheControlHeaderValue> modify)
        {
            modify(config);
        }
    }
}
