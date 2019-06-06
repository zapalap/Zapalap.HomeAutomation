using System;
using System.Collections.Generic;
using System.Text;

namespace Zapalap.HomeAutomation.Core.Behaviors.Validation.Exceptions
{
    [Serializable]
    public class InputValidationException : Exception
    {
        public InputValidationException(string message) : base(message)
        {

        }
    }
}
