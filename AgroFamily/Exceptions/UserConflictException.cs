using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Exceptions
{
    public class UserConflictException : Exception
    {
        public UserConflictException()
        {
        }

        public UserConflictException(string? message) : base(message)
        {
        }

        public UserConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
