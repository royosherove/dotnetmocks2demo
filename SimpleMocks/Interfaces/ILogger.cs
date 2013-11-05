using System;
using System.Runtime.Serialization;

namespace MyBillingProduct
{
    public interface ILogger
    {
        void  Write(string text);
    }

    public class LoggerException:Exception
    {
        public LoggerException()
        {
        }

        public LoggerException(string message) : base(message)
        {
        }

        public LoggerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}