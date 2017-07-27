namespace Common.Database
{
    using System;
    using System.Runtime.Serialization;

    public class SqlNotFoundException : Exception
    {
        public SqlNotFoundException()
        {
        }

        public SqlNotFoundException(string message) : base(message)
        {
        }

        protected SqlNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SqlNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

