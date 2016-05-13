namespace Repository.Core.Validators.Interfaces
{
    public interface IValidator<in T>
    {
        IValidationResult Validate(T entity);
    }
}