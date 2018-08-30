using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
