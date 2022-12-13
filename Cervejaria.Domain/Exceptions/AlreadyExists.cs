using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervejaria.Domain.Exceptions
{
    public class AlreadyExists: BusinessException
    {
        public AlreadyExists(string message) : base(ErrorCodes.AlreadyExists, message)
        {
        }
    }
}
