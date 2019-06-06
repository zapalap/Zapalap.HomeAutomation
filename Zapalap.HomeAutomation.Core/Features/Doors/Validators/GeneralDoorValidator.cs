using System;
using System.Collections.Generic;
using System.Text;
using Zapalap.HomeAutomation.Core.Behaviors.Validation.Validators;

namespace Zapalap.HomeAutomation.Core.Features.Doors.Validators
{
    public class GeneralDoorValidator : IValidator<INeedGeneralDoorValidation>
    {
        public (bool InputIsInvalid, string Message) Validate(INeedGeneralDoorValidation request)
        {
            return (false, "");
        }
    }
}
