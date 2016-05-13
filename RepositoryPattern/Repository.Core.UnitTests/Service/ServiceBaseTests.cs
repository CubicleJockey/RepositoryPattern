using System;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Core.Service;
using Repository.Core.Service.Interfaces;
using Repository.Core.UnitOfWork;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.UnitTests.Service
{
    
    public class ServiceBaseTests
    {
        [TestClass]
        public class Constructors
        {
            private static IValidator<string> fakeValidator;
            private static IUnitOfWorkProvider<IUnitOfWork> fakeProvider;

            [ClassInitialize]
            public static void ClassInitialize(TestContext context)
            {
                fakeValidator = A.Fake<IValidator<string>>();
                fakeProvider = A.Fake<IUnitOfWorkProvider<IUnitOfWork>>();
            }


            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException), "workProvider")]
            public void NullProvider()
            {
                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(null, fakeValidator);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException), "validator")]
            public void NullValidator()
            {
                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, null);
            }

            [TestMethod]
            public void Valid()
            {
                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);

                service.Should().NotBeNull();
                service.Should().BeAssignableTo<IServiceBase<string>>();
                service.Should().BeAssignableTo<ServiceBase<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>>();
            }
        }

        [TestClass]
        public class MethodTests
        {
            private IValidator<string> fakeValidator;
            private IValidationResult fakeValidationResult;
            private IUnitOfWorkProvider<IUnitOfWork> fakeProvider;

            [TestInitialize]
            public void TestInialize()
            {
                fakeValidator = A.Fake<IValidator<string>>();
                fakeValidationResult = A.Fake<IValidationResult>();
                fakeProvider = A.Fake<IUnitOfWorkProvider<IUnitOfWork>>();
            }

            [TestMethod]
            public void IsValid_FalseByValidationFailure()
            {
                const string ENTITY = "Woot";

                A.CallTo(() => fakeValidationResult.IsValid).Returns(false);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).Returns(fakeValidationResult);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                var valid = service.IsValid(ENTITY);

                valid.Should().BeFalse();

                A.CallTo(() => fakeValidationResult.IsValid).MustHaveHappened();
                A.CallTo(() => fakeValidator.Validate(ENTITY)).MustHaveHappened();
            }

            [TestMethod]
            public void IsValid_NullEntity()
            {
                const string ENTITY = null;
                
                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                var valid = service.IsValid(ENTITY);

                valid.Should().BeFalse();

                A.CallTo(() => fakeValidationResult.IsValid).MustNotHaveHappened();
                A.CallTo(() => fakeValidator.Validate(ENTITY)).MustNotHaveHappened();
            }

            [TestMethod]
            public void IsValid_True()
            {
                const string ENTITY = "Woot";

                A.CallTo(() => fakeValidationResult.IsValid).Returns(true);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).Returns(fakeValidationResult);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                var valid = service.IsValid(ENTITY);

                valid.Should().BeTrue();

                A.CallTo(() => fakeValidationResult.IsValid).MustHaveHappened();
                A.CallTo(() => fakeValidator.Validate(ENTITY)).MustHaveHappened();
            }

            [TestMethod]
            public void Validate_Valid()
            {
                const string ENTITY = "Farts";

                A.CallTo(() => fakeValidationResult.IsValid).Returns(true);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).Returns(fakeValidationResult);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                
            }
        }
    }
}
