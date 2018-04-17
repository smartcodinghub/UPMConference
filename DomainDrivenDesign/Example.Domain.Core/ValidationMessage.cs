using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Core
{
    /// <summary>
    /// Reprensenta un mensaje de validación
    /// </summary>
    public class ValidationMessage
    {
        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Gets or sets the formatted message arguments. These are values for custom formatted
        /// message in validator resource files Same formatted message can be reused in UI
        /// and with same number of format placeholders Like "Value {0} that you entered
        /// should be {1}"
        /// </summary>
        public object[] FormattedMessageParameters { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errorMessage">El mensaje de error</param>
        /// <param name="formattedMessageParameters">Los parámetros para reemplazar</param>
        public ValidationMessage(string errorMessage, params object[] formattedMessageParameters)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentException(nameof(errorMessage));
            FormattedMessageParameters = formattedMessageParameters;
        }
    }
}
