using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Core.UnitOfWork;

namespace Repository.Core.UnitTests
{
    public class UnitOfWorkProviderTests
    {
        [TestClass]
        public class ConstructorTests
        {
            [TestMethod]
            public void Valid()
            {
                var provider = new MockUnitOfWorkProvider<MockUnitOfWork>();

                provider.Should().NotBeNull();
                provider.Should().BeAssignableTo<IUnitOfWorkProvider<IUnitOfWork>>();
                provider.Should().BeAssignableTo<UnitOfWorkProvider<MockUnitOfWork>>();
                provider.Should().BeOfType<MockUnitOfWorkProvider<MockUnitOfWork>>();
            }
        }

        [TestClass]
        public class MethodTests
        {
            [TestMethod]
            public void GetReadonly()
            {
                var provider = new MockUnitOfWorkProvider<MockUnitOfWork>();

                var unitOfWork = provider.GetReadOnly();

                unitOfWork.Should().NotBeNull();
                unitOfWork.Should().BeOfType<MockUnitOfWork>();
            }

            [TestMethod]
            public void GetTransactional()
            {
                var provider = new MockUnitOfWorkProvider<MockUnitOfWork>();

                var unitOfWork = provider.GetTransactional();

                unitOfWork.Should().NotBeNull();
                unitOfWork.Should().BeOfType<MockUnitOfWork>();
            }
        }
    }
}
