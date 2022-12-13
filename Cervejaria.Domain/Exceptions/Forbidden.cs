using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cervejaria.Domain.Exceptions
{
    public class Forbidden : BusinessException
    {
        public Forbidden(string message) : base(ErrorCodes.Forbidden, message)
        {
        }
    }
}