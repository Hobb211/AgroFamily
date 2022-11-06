using AgroFamily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgroFamily.Exceptions
{
    public class SaleConflictException : Exception
    {
        public ProductModel product { get; }

        public SaleConflictException()
        {
        }

        public SaleConflictException(string? message) : base(message)
        {
        }

        public SaleConflictException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SaleConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
