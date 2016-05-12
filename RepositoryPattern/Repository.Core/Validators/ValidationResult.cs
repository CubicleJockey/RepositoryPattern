using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.Validators
{
    public class ValidationResult : IValidationResult
    {
        #region Implementation of IValidationResult

        /// <summary>
        /// List of Validation Errors
        /// </summary>
        public IEnumerable<IValidationError> Errors { get; }

        /// <summary>
        /// Is the validation valid or not.
        /// </summary>
        public bool IsValid { get; }

        #endregion

        #region Constructors

        public ValidationResult(bool isValid, IEnumerable<IValidationError> errors)
        {
            IsValid = isValid;

            if(IsValid)
            {
                Errors = null;
            }
            else
            {
                if(errors == null)
                {
                    throw new ArgumentNullException(nameof(errors), $"{nameof(ValidationResult)} was set to invalid and errors is null.");
                }

                var ers = errors.ToArray();
                if(ers.Length == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(errors), $"{nameof(ValidationResult)} was set to invalid and errors is empty.");
                }

                Errors = ers;
            }

        }

        #endregion Constructors
    }
}
