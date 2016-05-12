using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Core.Validators;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.UnitTests.Validators
{
    public class ValidationErrorTests
    {
        [TestClass]
        public class ConstructorTests
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "message cannot be null, empty string or whitespace")]
            public void MessageIsNull()
            {
                var error = new ValidationError(null, "someProperty");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "message cannot be null, empty string or whitespace")]
            public void MessageIsEmptyString()
            {
                var error = new ValidationError(string.Empty, "someProperty");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "message cannot be null, empty string or whitespace")]
            public void MessageIsWhitespace()
            {
                var error = new ValidationError("     ", "someProperty");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "propertyName cannot be null, empty string or whitespace")]
            public void PropertyNameIsNull()
            {
                var error = new ValidationError("A Message", null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "propertyName cannot be null, empty string or whitespace")]
            public void PropertyNameIsEmptyString()
            {
                var error = new ValidationError("A Message", string.Empty);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException), "propertyName cannot be null, empty string or whitespace")]
            public void PropertyNameIsWhitespace()
            {
                var error = new ValidationError("A Message", "     ");
            }

            [TestMethod]
            public void Valid()
            {
                var randomObject = new
                {
                    Thingy = "I am a thingy string."
                };

                const string EXPECTED_MESSAGE = "Random Object as failed";
                const string EXPECTED_PROPERTYNAME = nameof(randomObject.Thingy);

                var error = new ValidationError(EXPECTED_MESSAGE, EXPECTED_PROPERTYNAME);

                error.Should().NotBeNull();
                error.Should().BeAssignableTo<IValidationError>();
                error.Should().BeOfType<ValidationError>();

                error.Message.Should().NotBeNullOrWhiteSpace();
                error.Message.Should().BeEquivalentTo(EXPECTED_MESSAGE);

                error.PropertyName.Should().NotBeNullOrWhiteSpace();
                error.PropertyName.Should().BeEquivalentTo(EXPECTED_PROPERTYNAME);
            }
        }
    }
}
