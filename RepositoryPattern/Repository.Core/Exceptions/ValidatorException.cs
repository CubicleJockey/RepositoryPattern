using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Repository.Core.Validators.Interfaces;

namespace Repository.Core.Exceptions
{
    public class ValidatorException : ValidationException
    {
        #region Fields

        private readonly IEnumerable<IValidationError> errors;

        #endregion Fields

        #region Overridden

        #region Constructors

        public ValidatorException(IEnumerable<IValidationError> errors)
        {
            this.errors = errors;
        }

        #endregion Constructors

        #region Overrides of Exception

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string ("").
        /// </returns>
        public override string Message {
            get
            {
                if(errors != null)
                {
                    var msg = new StringBuilder();
                    foreach(var error in errors)
                    {
                        msg.AppendLine(error.Message);
                    }
                    return msg.ToString();
                }
                return base.Message;
            } 
        }


        #endregion

        #endregion Overridden
    }
}
