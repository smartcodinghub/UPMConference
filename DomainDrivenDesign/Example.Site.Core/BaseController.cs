using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Example.Site.Core
{
    /// <summary>
    /// Clase base para implementar métodos comunes de todos los controllers
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Default settings for the ExceptionFilter
        /// </summary>
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Genera un resultado con el status y el valor pasados
        /// </summary>
        /// <param name="statusResult">Método que genera la respuesta a partir de un estado</param>
        /// <param name="data">Objecto a serializar en formato Json</param>
        /// <returns></returns>
        protected IActionResult Result(Func<object, IActionResult> statusResult, object data)
        {
            return statusResult(Json(data, settings).Value);
        }

        /// <summary>
        /// Genera un resultado con el status y el valor pasados
        /// </summary>
        /// <param name="statusResult">Método que genera la respuesta a partir de un estado</param>
        /// <param name="objectResult">Método que genera una ObjectResult a partir del data</param>
        /// <param name="data">Objecto a serializar en el formato específico</param>
        /// <returns></returns>
        protected IActionResult Result(Func<object, IActionResult> statusResult, Func<object, ObjectResult> objectResult, object data)
        {
            return statusResult(objectResult(data).Value);
        }

        /// <summary>
        /// Genera un resultado con el status y el valor pasados usando el formateador de Json pasado
        /// </summary>
        /// <param name="statusResult">Método que genera la respuesta a partir de un estado</param>
        /// <param name="jsonResult">Método que genera una JsonResult a partir del data</param>
        /// <param name="data">Objecto a serializar en el formato específico</param>
        /// <returns></returns>
        protected IActionResult Result(Func<object, IActionResult> statusResult, Func<object, JsonResult> jsonResult, object data)
        {
            return statusResult(jsonResult(data).Value);
        }
    }
}
