using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Repository.Core.Validators;

namespace Repository.Core.Exceptions
{
    public class CustomValidationException : ValidationException
    {
        #region Fields

        private readonly IEnumerable<ValidationError> errors;

        #endregion Fields

        #region Overridden

        #region Constructors

        public CustomValidationException(IEnumerable<ValidationError> errors)
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
                        msg.AppendLine(error);
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
