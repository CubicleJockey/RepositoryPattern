using System;
using System.Text;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Core.Exceptions;
using Repository.Core.Service;
using Repository.Core.Service.Interfaces;
using Repository.Core.UnitOfWork;
using Repository.Core.Validators;
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

                A.CallTo(() => fakeValidationResult.IsValid).MustHaveHappened(Repeated.Exactly.Once);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).MustHaveHappened(Repeated.Exactly.Once);
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

                A.CallTo(() => fakeValidationResult.IsValid).MustHaveHappened(Repeated.Exactly.Once);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            public void Validate_Valid()
            {
                const string ENTITY = "Farts";

                A.CallTo(() => fakeValidationResult.IsValid).Returns(true);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).Returns(fakeValidationResult);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                service.Validate(ENTITY);

                A.CallTo(() => fakeValidator.Validate(ENTITY)).MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            public void Validate_Errors()
            {
                const string ENTITY = "Entity";
                var errors = new[]
                {
                    new ValidationError("A", "A"),
                    new ValidationError("B", "B")
                };

                A.CallTo(() => fakeValidationResult.IsValid).Returns(false);
                A.CallTo(() => fakeValidationResult.Errors).Returns(errors);
                A.CallTo(() => fakeValidator.Validate(ENTITY)).Returns(fakeValidationResult);

                try
                {
                    var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                    service.Validate(ENTITY);

                }
                catch(ValidatorException vx)
                {
                    A.CallTo(() => fakeValidator.Validate(ENTITY)).MustHaveHappened();
                    A.CallTo(() => fakeValidationResult.IsValid).MustHaveHappened();

                    var sb = new StringBuilder();
                    sb.AppendLine("A").AppendLine("B");

                    vx.Message.Should().NotBeNullOrWhiteSpace();
                    vx.Message.Should().BeEquivalentTo(sb.ToString());

                    return;
                }
                Assert.Fail($"Should have gotten an exception of type {typeof(ValidationError)}.");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException), "unitOfWork")]
            public void GetTransactionalUnitOfWork_Null()
            {
                A.CallTo(() => fakeProvider.GetTransactional()).Returns(null);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                service.GetTransactionalUnitOfWork();

                A.CallTo(() => fakeProvider.GetTransactional()).MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            public void GetTransactionalUnitOfWork()
            {
                var unitOfWork = A.Dummy<IUnitOfWork>();
                A.CallTo(() => fakeProvider.GetTransactional()).Returns(unitOfWork);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                var returnedUnitOfWork = service.GetTransactionalUnitOfWork();

                returnedUnitOfWork.Should().NotBeNull();
                returnedUnitOfWork.Should().BeAssignableTo<IUnitOfWork>();
                returnedUnitOfWork.ShouldBeEquivalentTo(unitOfWork);

                A.CallTo(() => fakeProvider.GetTransactional()).MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException), "unitOfWork")]
            public void GetReadonlyUnitOfWork_Null()
            {
                A.CallTo(() => fakeProvider.GetReadOnly()).Returns(null);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                service.GetReadonlyUnitOfWork();

                A.CallTo(() => fakeProvider.GetReadOnly()).MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            public void GetReadonlyUnitOfWork()
            {
                var unitOfWork = A.Dummy<IUnitOfWork>();
                A.CallTo(() => fakeProvider.GetReadOnly()).Returns(unitOfWork);

                var service = new MockService<IUnitOfWorkProvider<IUnitOfWork>, IUnitOfWork, string>(fakeProvider, fakeValidator);
                var returnedUnitOfWork = service.GetReadonlyUnitOfWork();

                returnedUnitOfWork.Should().NotBeNull();
                returnedUnitOfWork.Should().BeAssignableTo<IUnitOfWork>();
                returnedUnitOfWork.ShouldBeEquivalentTo(unitOfWork);

                A.CallTo(() => fakeProvider.GetReadOnly()).MustHaveHappened(Repeated.Exactly.Once);
            }
        }
    }
}
