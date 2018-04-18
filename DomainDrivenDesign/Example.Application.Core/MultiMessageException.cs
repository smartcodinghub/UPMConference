using System;
using System.Collections.Generic;
using Example.Infraestructure.Crosscutting;

namespace Example.Application.Core
{
    /// <summary>
    /// Excepción para incluir lista de mensajes
    /// </summary>
    public class MultiMessageException : ApplicationException
    {
        /// <summary>
        /// La lista de mensajes internos de la excepción
        /// </summary>
        private List<string> messages;

        /// <summary>
        /// Obtiene o establece la lista de mensajes internos de la excepción
        /// </summary>
        public List<string> Messages
        {
            get => messages;
            protected set
            {
                messages = value;
                message = new Lazy<string>(() => string.Join(", ", messages));
            }
        }

        /// <summary>
        /// Obtiene el mensaje concatenando los mensajes de Messages
        /// </summary>
        private Lazy<string> message;

        /// <summary>
        /// Obtiene el mensaje concatenando los mensajes de Messages
        /// </summary>
        public override string Message => message.Value;

        /// <summary>
        /// Contructor por defecto para herencia
        /// </summary>
        protected MultiMessageException() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje que se quiere incluir en la excepción</param>
        public MultiMessageException(string message) : this(new List<string>() { message }) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list">Lista de mensajes que se quieren incluir en la excepción</param>
        public MultiMessageException(List<string> list)
        {
            list.ThrowIfNull(nameof(list));

            this.Messages = list;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exToFlat">Lista de mensajes que se quieren incluir en la excepción</param>
        public MultiMessageException(Exception exToFlat) : this(new List<string>())
        {
            AddInnerExceptions(exToFlat);
        }

        private void AddInnerExceptions(Exception ex)
        {
            if(ex == null) return;
            this.Messages.Add(ex.Message);
            AddInnerExceptions(ex.InnerException);
        }
    }
}
