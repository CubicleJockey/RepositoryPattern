using System;
using Repository.Core.UnitOfWork;

namespace Repository.Core.UnitTests
{
    public class MockUnitOfWork : IUnitOfWork
    {
        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
