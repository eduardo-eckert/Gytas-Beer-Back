using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervejaria.Domain.Exceptions
{
        public class BusinessException : Exception
        {
            public BusinessException(ErrorCodes errorCode, string message, object value = null) : base(message)
            {
                ErrorCode = errorCode;
                Value = value;
            }

            public ErrorCodes ErrorCode { get; }
            public object Value { get; }
        }
}
