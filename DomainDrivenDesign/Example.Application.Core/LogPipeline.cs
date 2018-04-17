using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Example.Application.Core
{
    /// <summary>
    /// Clase decoradora mediar para generar los logs necesario antes de llamar a la capa de apliacion.
    /// Clase interceptora
    /// </summary>
    /// <typeparam name="TRequest">Clase de solicitud</typeparam>
    /// <typeparam name="TResponse">Clase de respuesta</typeparam>
    public class LogPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Campo que representa la instancia del handler
        /// </summary>
        private readonly IRequestHandler<TRequest, TResponse> inner;

        /// <summary>
        /// Logger usado
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="inner">Campo que representa la instancia del handler</param>
        /// <param name="logger"></param>
        public LogPipeline(IRequestHandler<TRequest, TResponse> inner, ILogger<IRequestHandler<TRequest, TResponse>> logger)
        {
            this.inner = inner;
            this.logger = logger;
        }

        /// <summary>
        /// Metodo manejadora de la clase. Este metodo sera invocado antes de llama a la funcion handler de la clase de aplicacion 
        /// </summary>
        /// <param name="request">Instancia de la clase de solicitud de la capa de aplicacion</param>
        /// <param name="next">Delegado que representa el siguiente decorador</param>
        /// <returns>Respuesta de la funcion</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            logger.LogInformation("Executing command {@Command}", new { Info = inner.GetType().Name, Request = request });

            try
            {
                var response = await next().ConfigureAwait(false);

                logger.LogInformation("Command {@Command} executed successfully, with response: {@Response}", inner.GetType().Name, response);

                return response;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error captured in LogPipeline");
                throw;
            }
        }
    }
}
