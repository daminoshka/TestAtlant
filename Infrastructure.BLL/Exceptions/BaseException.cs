using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BLL.Exceptions
{
    public abstract class BaseException<TU> : Exception where TU : Exception, new()

    {
        protected BaseException()
            : base()
        {
        }

        protected BaseException(String message)
            : base(message)
        {
        }

        protected BaseException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected static TU ThrowException()
        {
            var type = typeof(TU);
            ConstructorInfo ctor = type.GetConstructor(new Type[] { });
            var instance = ctor?.Invoke(new object[] { });
            var exception = (TU)instance;
            return exception;
        }

        public static TU ThrowException<T>(T innerException) where T : Exception
        {
            var exception = (TU)(object)innerException;
            return exception;
        }

        public static TU ThrowException(string message)
        {
            var type = typeof(TU);
            ConstructorInfo ctor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(String) }, null);
            var instance = ctor?.Invoke(new object[] { message });
            var exception = (TU)instance;

            return exception;
        }


        public static TU ThrowException<T>(string message, T innerException) where T : Exception
        {
            Type type = typeof(TU);
            Type innerExceptionType = typeof(T);
            if (innerException is TU)
            {
                message = $"{message} ({innerException.Message})"; // TODO модифицировать сообщение
            }
            ConstructorInfo ctor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(String), typeof(Exception) }, null);
            var instance = ctor?.Invoke(new object[] { message, innerException });
            var exception = (TU)instance;

            return exception;
        }

        public static string ConcatenateInnerStackTrace(Exception ex)
        {
            var stackTrace = "";
            Exception exception = ex;

            while (true)
            {
                if (exception != null)
                {
                    stackTrace = string.Format("Message: {0}\r\nStackTrace:\r\n{1} \r\n\r\n" + stackTrace, exception.Message, exception.StackTrace);
                    exception = exception.InnerException;
                }
                else
                {
                    return stackTrace;
                }
            }
        }

    }
}
