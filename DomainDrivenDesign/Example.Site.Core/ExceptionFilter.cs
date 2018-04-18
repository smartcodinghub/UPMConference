using System;
using System.Net;
using Example.Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Example.Site.Core
{
    /// <summary>
    /// Handler global de excepciones
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Default settings for the ExceptionFilter
        /// </summary>
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Delegado para logear excepciones en ApplicationInsights
        /// </summary>
        public ExceptionTelemetry LogException { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logException"></param>
        public ExceptionFilter(ExceptionTelemetry logException)
        {
            this.LogException = logException;
        }

        /// <summary>
        /// Método que captura las excepciones no controlada
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            Exception ex = context.Exception;
            MultiMessageException multi = ex as MultiMessageException ?? new MultiMessageException(ex);

            ExceptionResponse exceptionResponse = new ExceptionResponse() { Messages = multi.Messages };
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            context.Result = new JsonResult(exceptionResponse, settings);

            context.ExceptionHandled = true;

            LogException(ex);
        }
    }
}
