using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.Exceptions
{
    public class DuplicateObjectException : ApplicationException
    {
        public DuplicateObjectException(string message) : base(message)
        {
        }
        public DuplicateObjectException(string objectName, string value)
            : base($"Invalid duplicated {objectName}: {value}")
        {
        }
    }
}
