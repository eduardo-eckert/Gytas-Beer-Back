using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervejaria.Domain.Exceptions
{
    public class Unhandled : BusinessException
    {
        public Unhandled(string message) : base(ErrorCodes.Unhandled, message)
        {
        }
    }
}
