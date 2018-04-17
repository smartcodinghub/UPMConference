using System;
using FluentValidation;
using FluentValidation.Validators;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// Clase con extensiones para el Framework FluentValidations
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Defines a enum value validator on the current rule builder that ensures that the specific value is a valid enum value.
        /// </summary>
        /// <typeparam name="T">Type of Enum being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> IsInEnum<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Type enumType)
        {
            return ruleBuilder.SetValidator(new EnumValidator(enumType));
        }

        /// <summary>
        /// Specifies a custom error message to use if validation fails.
        /// </summary>
        /// <param name="rule">The current rule</param>
        /// <param name="errorMessage">The error message to use</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> WithFormatMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage, params object[] parameters)
        {
            return DefaultValidatorOptions.WithMessage(rule, String.Format(errorMessage, parameters));
        }
    }
}
