using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace Example.Site.Core.Core
{
    /// <summary>
    /// Opciones para el middleware de caché de las respuestas en cliente
    /// </summary>
    public class ClientResponseCachingOptions
    {
        /// <summary>
        /// Diccionario donde se almacena la configuración
        /// </summary>
        private Dictionary<string, CacheControlHeaderValue> innerOptions = new Dictionary<string, CacheControlHeaderValue>();

        /// <summary>
        /// Obtiene la configuración de caché de un método web
        /// </summary>
        /// <param name="method">Método http</param>
        /// <returns></returns>
        public CacheControlHeaderValue this[HttpMethod method]
        {
            get
            {
                if(!TryGet(method.Method, out CacheControlHeaderValue options))
                {
                    options = new CacheControlHeaderValue();
                    innerOptions[method.Method] = options;
                }

                return options;
            }
        }

        /// <summary>
        /// Intenta obtener la configuración de un método
        /// </summary>
        /// <param name="method"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool TryGet(String method, out CacheControlHeaderValue options) => innerOptions.TryGetValue(method, out options);
    }
}
