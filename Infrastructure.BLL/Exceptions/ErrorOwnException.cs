using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BLL.Exceptions
{
    [Serializable]
    public class ErrorOwnException : BaseException<ErrorOwnException>
    {
        public ErrorOwnException()
            : base()
        {
        }

        private ErrorOwnException(String message)
            : base(message)
        {
        }

        private ErrorOwnException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
