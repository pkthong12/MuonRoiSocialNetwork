using BaseConfig.EntityObject.EntityObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseConfig.Exeptions
{
    public class CustomException : Exception
    {
        public IReadOnlyCollection<ErrorResult> ErrorMessages;

        public CustomException()
        {
        }

        public CustomException(IReadOnlyCollection<ErrorResult> errorMessages)
            : base(string.Join(';', errorMessages.Select((ErrorResult m) => m.ErrorMessage).ToArray()))
        {
            ErrorMessages = errorMessages;
        }

        public CustomException(IReadOnlyCollection<ErrorResult> errorMessages, Exception innerException)
            : base(string.Join(';', errorMessages.Select((ErrorResult m) => m.ErrorMessage).ToArray()), innerException)
        {
            ErrorMessages = errorMessages;
        }
    }
}
