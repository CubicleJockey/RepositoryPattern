using Repository.Core.UnitOfWork;

namespace Repository.Core.UnitTests
{
    public class MockUnitOfWorkProvider<T> : UnitOfWorkProvider<T> 
        where T : IUnitOfWork, new()
    {
        protected override T GetNew()
        {
            return new T();
        }
    }
}
