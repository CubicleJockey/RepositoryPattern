﻿using Repository.Core.Service;
using Repository.Core.UnitOfWork;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.UnitTests.Service
{
    public class MockService<TProvider, TUnitOfWork, TEntity> : ServiceBase<TProvider, TUnitOfWork, TEntity>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork
        where TProvider : IUnitOfWorkProvider<TUnitOfWork>
    {
        public MockService(TProvider workProvider, IValidator<TEntity> validator) : base(workProvider, validator)
        {
        }

        public new TUnitOfWork GetReadonlyUnitOfWork()
        {
            return base.GetReadonlyUnitOfWork();
        }

        public new TUnitOfWork GetTransactionalUnitOfWork()
        {
            return base.GetTransactionalUnitOfWork();
        }

        public new void Validate(TEntity entity)
        {
            base.Validate(entity);
        }
    }
}
