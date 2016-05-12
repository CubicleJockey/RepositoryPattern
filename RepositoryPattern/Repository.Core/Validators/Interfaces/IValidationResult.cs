using System.Collections.Generic;

namespace Repository.Core.Validators.Interfaces
{
    public interface IValidationResult
    {
        /// <summary>
        /// List of Validation Errors
        /// </summary>
        IEnumerable<IValidationError> Errors { get; }

        /// <summary>
        /// Is the validation valid or not.
        /// </summary>
        bool IsValid { get; }
    }
}