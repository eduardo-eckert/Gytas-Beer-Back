using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervejaria.Domain.Exceptions
{
    public class InvalidObject : BusinessException
    {
        public InvalidObject(string message) : base(ErrorCodes.InvalidObject, message)
        {
        }
    }
}
