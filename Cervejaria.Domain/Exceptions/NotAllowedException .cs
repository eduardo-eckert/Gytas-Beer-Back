﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cervejaria.Domain.Exceptions
{
    public class NotAllowedException : BusinessException
    {
        public NotAllowedException(string message) : base(ErrorCodes.NotAllowed, message)
        {
        }
    }
}
