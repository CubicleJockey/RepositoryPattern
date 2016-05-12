namespace Repository.Core.Validators.Interfaces
{
    public interface IValidationError
    {
        /// <summary>
        /// Message about the error.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Name of the property that is having the error.
        /// </summary>
        string PropertyName { get; } 
    }
}