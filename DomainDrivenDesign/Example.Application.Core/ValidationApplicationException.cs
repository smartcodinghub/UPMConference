using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Template.Domain.Core;
using Template.Infraestructure.Crosscutting;

namespace Example.Application.Core
{
    /// <summary>
    /// Excepción de validaciones del dominio
    /// </summary>
    public class ValidationApplicationException : MultiMessageException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje que se quiere incluir en la excepción</param>
        public ValidationApplicationException(string message) : base(message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list">Lista de mensajes que se quieren incluir en la excepción</param>
        public ValidationApplicationException(List<string> list) : base(list) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">Recursos para localizar</param>
        /// <param name="messages">Lista de mensajes que se quieren incluir en la excepción</param>
        public ValidationApplicationException(ResourceManager manager, IEnumerable<ValidationMessage> messages)
        {
            manager.ThrowIfNull(nameof(manager));
            messages.ThrowIfNull(nameof(messages));

            Messages = messages.Select(vm =>
                String.Format(manager.GetString(vm.ErrorMessage) ?? vm.ErrorMessage,
                vm.FormattedMessageParameters)).ToList();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="manager">Recursos para localizar</param>
        /// <param name="message">El mensaje de error</param>
        /// <param name="messageParameters">Los parámetros para reemplazar</param>
        public ValidationApplicationException(ResourceManager manager, string message, params object[] messageParameters)
        {
            manager.ThrowIfNull(nameof(manager));
            message.ThrowIfNull(nameof(message));

            Messages = new List<string>() { string.Format(manager.GetString(message) ?? message, messageParameters) };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">El mensaje de error</param>
        /// <param name="messageParameters">Los parámetros para reemplazar</param>
        public ValidationApplicationException(string message, params object[] messageParameters)
        {
            message.ThrowIfNull(nameof(message));

            Messages = new List<string>() { string.Format(message, messageParameters) };
        }
    }
}
