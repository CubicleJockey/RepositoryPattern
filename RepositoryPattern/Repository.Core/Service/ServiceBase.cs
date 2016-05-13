using System;
using Repository.Core.Exceptions;
using Repository.Core.Service.Interfaces;
using Repository.Core.UnitOfWork;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.Service
{
    public abstract class ServiceBase<TProvider, TUnitOfWork, TEntity> : IServiceBase<TEntity>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork
        where TProvider : IUnitOfWorkProvider<TUnitOfWork>
    {
        #region Fields

        private readonly IValidator<TEntity> _validator;
        private readonly TProvider _workProvider; 

        #endregion Fields

        #region Contructors

        protected ServiceBase(TProvider workProvider, IValidator<TEntity> validator)
        {
            if(workProvider == null)
            {
                throw new ArgumentNullException(nameof(workProvider));
            }
            if(validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }
            _workProvider = workProvider;
            _validator = validator;
        } 

        #endregion Constructors

        protected TUnitOfWork GetReadonlyUnitOfWork()
        {
            var unitOfWork = _workProvider.GetReadOnly();
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            return unitOfWork;
        }

        protected TUnitOfWork GetTransactionalUnitOfWork()
        {
            var unitOfWork = _workProvider.GetTransactional();
            if(unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            return unitOfWork;
        }

        protected void Validate(TEntity entity)
        {
            var result = _validator.Validate(entity);
            if(!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
        }

        #region Implementation of IServiceBase<in TEntity>

        /// <summary>
        /// Validates the specified entity
        /// </summary>
        /// <param name="entity">Entity to validate</param>
        /// <returns>True if not null and valid.</returns>
        public bool IsValid(TEntity entity)
        {
            if(entity == null)
            {
                return false;
            }
            var valid = _validator.Validate(entity);
            return valid.IsValid;
        }

        #endregion
    }
}
