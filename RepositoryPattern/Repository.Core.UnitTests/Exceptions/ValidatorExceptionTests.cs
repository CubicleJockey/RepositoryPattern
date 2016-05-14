using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Core.Exceptions;
using Repository.Core.Validators;

namespace Repository.Core.UnitTests.Exceptions
{
    
    public class ValidatorExceptionTests
    {
        [TestClass]
        public class ConstructorTests
        {
            [TestMethod]
            public void Valid_Null()
            {
                var validatorException = new ValidatorException(null);
                validatorException.Should().NotBeNull();
                validatorException.Should().BeAssignableTo<ValidationException>();
                validatorException.Should().BeOfType<ValidatorException>();
            }

            [TestMethod]
            public void Valid()
            {
                var errors = new[]
                {
                    new ValidationError("A", "B"),
                    new ValidationError("C", "D")
                };

                var validatorException = new ValidatorException(errors);
                validatorException.Should().NotBeNull();
                validatorException.Should().BeAssignableTo<ValidationException>();
                validatorException.Should().BeOfType<ValidatorException>();
            }
        }

        [TestClass]
        public class PropertiesTests
        {
            [TestMethod]
            public void Message()
            {
                var EXPECTEDRESULT = new StringBuilder();
                EXPECTEDRESULT.AppendLine("A").AppendLine("C");

                var errors = new[]
                {
                    new ValidationError("A", "B"),
                    new ValidationError("C", "D")
                };

                var validatorException = new ValidatorException(errors);
                validatorException.Message.Should().NotBeNullOrWhiteSpace();
                validatorException.Message.Should().BeEquivalentTo(EXPECTEDRESULT.ToString());
            }
        }
    }
}
