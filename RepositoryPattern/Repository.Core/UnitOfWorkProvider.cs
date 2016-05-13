using Repository.Core.UnitOfWork;

namespace Repository.Core
{
    public abstract class UnitOfWorkProvider<T> : IUnitOfWorkProvider<T> where T : IUnitOfWork
    {
        #region Fields

        private static  T _readOnly = default(T);

        #endregion Fields


        protected abstract T GetNew();

        public T GetReadOnly() => _readOnly == null ? GetNew() : _readOnly;

        public T GetTransactional() => GetNew();

        #region IDisposable Support

        public void Dispose()
        {
            if(_readOnly != null)
            {
                _readOnly.Dispose();
            }
        }
        #endregion

    }
}
