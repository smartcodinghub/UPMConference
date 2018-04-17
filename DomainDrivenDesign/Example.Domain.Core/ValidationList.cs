using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Core
{
    /// <summary>
    /// Implementación propia para los mensajes de validación
    /// </summary>
    public class ValidationList : List<ValidationMessage>
    {
        /// <summary>
        /// Añade un nuevo ValidationMessage
        /// </summary>
        /// <param name="message">Mensaje</param>
        /// <param name="parameters">Parámetros a reemplazar</param>
        public void Add(String message, params object[] parameters) => Add(new ValidationMessage(message, parameters));
    }
}
