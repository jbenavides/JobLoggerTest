namespace JobLoggerApp.Helpers
{
    using System;

    public class MessageTypeNullException : Exception
    {
        public MessageTypeNullException() :base()
        {
        }

        public MessageTypeNullException(string message) : base(message)
        {
        }

        public MessageTypeNullException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class MessageTypeNotAllowedException : Exception
    {
        public MessageTypeNotAllowedException() : base()
        {
        }

        public MessageTypeNotAllowedException(string message) : base(message)
        {
        }

        public MessageTypeNotAllowedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
