using System;
using System.Collections.Generic;
using System.Text;

namespace Zapalap.HomeAutomation.Core.Behaviors.Validation.Validators
{
    public interface IValidator<in TRequest>
    {
        (bool InputIsInvalid, string Message) Validate(TRequest request);
    }
}
