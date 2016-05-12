using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Core.Validators;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.UnitTests.Validators
{
    public class ValidationResultTests
    {
        [TestClass]
        public class ConstructorTests
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException), "ValidationResult was set to invalid and errors is null.")]
            public void ResultIsInvalidAndErrorsIsNull()
            {
                var result = new ValidationResult(false, null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException), "ValidationResult was set to invalid and errors is empty.")]
            public void ResultisInvalidAndErrorsIsEmpty()
            {
                var result = new ValidationResult(false, new ValidationError[0]);
            }

            [TestMethod]
            public void InvalidWithResults()
            {
                var errors = new[]
                {
                    new ValidationError("A", "A"),
                    new ValidationError("B", "B")
                };
                var result = new ValidationResult(false, errors);

                result.Should().NotBeNull();
                result.Should().BeAssignableTo<IValidationResult>();
                result.Should().BeOfType<ValidationResult>();

                result.IsValid.Should().BeFalse();

                result.Errors.Should().NotBeNullOrEmpty();
                result.Errors.Count().ShouldBeEquivalentTo(errors.Length);
                result.Errors.ElementAt(0).ShouldBeEquivalentTo(errors[0]);
                result.Errors.ElementAt(1).ShouldBeEquivalentTo(errors[1]);
            }

            [TestMethod]
            public void Valid()
            {
                var result = new ValidationResult(true, null);

                result.Should().NotBeNull();
                result.Should().BeAssignableTo<IValidationResult>();
                result.Should().BeOfType<ValidationResult>();

                result.IsValid.Should().BeTrue();

                result.Errors.Should().BeNull();
            }

            [TestMethod]
            public void Valid_AccidentalErrorsPassedInAndIgnored()
            {
                var accidentalErrors = new []
                {
                    new ValidationError("A", "A") 
                };

                var result = new ValidationResult(true, accidentalErrors);

                result.Should().NotBeNull();
                result.Should().BeAssignableTo<IValidationResult>();
                result.Should().BeOfType<ValidationResult>();

                result.IsValid.Should().BeTrue();

                result.Errors.Should().BeNull();
            }
        }
    }
}
