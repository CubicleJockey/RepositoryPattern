using System;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.Validators
{
    public class ValidationError : IValidationError
    {
        #region Propreties

        public string Message { get; }
        public string PropertyName { get; }

        #endregion Properties

        #region Constructors

        public ValidationError(string message, string propertyName)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"{nameof(message)} cannot be null, empty string or whitespace.");
            }
            Message = message;

            if(string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"{nameof(propertyName)} cannot be null, empty string or whitepsace.");
            }
            PropertyName = propertyName;
        }

        #endregion Constructors
    }
}