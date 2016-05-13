namespace Repository.Core.Service.Interfaces
{
    public interface IServiceBase<in T> where T : class
    {
        /// <summary>
        /// Validates the specified entity
        /// </summary>
        /// <param name="entity">Entity to validate</param>
        /// <returns>True if not null and valid.</returns>
        bool IsValid(T entity);
    }
}