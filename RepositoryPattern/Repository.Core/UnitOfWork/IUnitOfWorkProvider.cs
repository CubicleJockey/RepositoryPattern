using System;

namespace Repository.Core.UnitOfWork
{
    public interface IUnitOfWorkProvider<out T> : IDisposable where T : IUnitOfWork
    {
        /// <summary>
        ///  Gets a readonly unit of work.
        /// </summary>
        /// <returns></returns>
        T GetReadOnly();

        /// <summary>
        /// Gets the transactional unit of work.
        /// </summary>
        /// <returns></returns>
        T GetTransactional();
    }
}