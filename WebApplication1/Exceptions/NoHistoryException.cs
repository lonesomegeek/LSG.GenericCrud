using System;
using System.Runtime.Serialization;

namespace WebApplication1.Exceptions
{
    [Serializable]
    internal class NoHistoryException : Exception
    {
        public NoHistoryException()
        {
        }

        public NoHistoryException(string message) : base(message)
        {
        }

        public NoHistoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoHistoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
