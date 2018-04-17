using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Example.Application.Core
{
    /// <summary>
    /// Decorador del método handle del command handler
    /// </summary>
    /// <typeparam name="TRequest">Tipo de los datos de la petición</typeparam>
    /// <typeparam name="TResponse">Tipo de la información de retorno</typeparam>
    public class ValidatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Conjunto de validadores que deben ser superados para admitir los datos de entrada
        /// </summary>
        private readonly IValidator<TRequest>[] validators;

        /// <summary>
        /// Instancia del command handler utilizado en la request
        /// </summary>
        private readonly IRequestHandler<TRequest, TResponse> inner;

        /// <summary>
        /// Localizador de strings
        /// </summary>
        private readonly IStringLocalizer localizer;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="validators">conjunto de validadores específicos que comprobarán la calidad de la petición</param>
        /// <param name="inner">Instancia del command handler utilizado para la request</param>
        public ValidatorPipeline(IValidator<TRequest>[] validators, IRequestHandler<TRequest, TResponse> inner, IStringLocalizerFactory localizerFactory)
        {
            this.inner = inner;
            this.validators = validators;
            this.localizer = localizerFactory.Create("ValidationMessages", new AssemblyName(typeof(ValidatorPipeline<TRequest, TResponse>).Assembly.FullName).Name);
        }
        /// <summary>
        /// Decorador del método principal del request handler
        /// </summary>
        /// <param name="request">Instancia de la petición</param>
        /// <param name="next">Manejador del decorador</param>
        /// <returns>Tarea de retorno delegada desde el handler no decorador</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = validators
                   .Select(v => v.Validate(request))
                   .SelectMany(result => result.Errors)
                   .Where(error => error != null)
                   .ToList();

            if(failures.Any())
            {
                List<string> messages = new List<string>()
                {
                    localizer["VALIDATION_ERRORS_FOR_TYPE", typeof(TRequest).Name]
                };
                messages.AddRange(failures.Select(f => f.ErrorMessage));
                throw new MultiMessageException(messages);
            }

            var response = await next().ConfigureAwait(false);

            return response;
        }
    }
}
