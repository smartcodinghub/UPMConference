using System.Collections.Generic;

namespace Example.Site.Core
{
    /// <summary>
    /// Respresenta una excepción
    /// </summary>
    public class ExceptionResponse
    {
        /// <summary>
        /// El mensaje de la excecpión
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// El mensaje de la excecpión
        /// </summary>
        public List<string> Messages { get; set; }

        /// <summary>
        /// Cualquier información extra que se quiera añadir
        /// </summary>
        public object ExtraInfo { get; set; }
    }
}
