using System;
using System.Collections.Generic;
using System.Text;
using Zapalap.HomeAutomation.Core.Behaviors.Validators;

namespace Zapalap.HomeAutomation.Core.Features.Doors.Validators
{
    public class GeneralDoorValidator : IValidator<INeedGeneralDoorValidation>
    {
        public (bool InputIsInvalid, string Message) Validate(INeedGeneralDoorValidation request)
        {
            if (request.DoorId == 0)
            {
                return (true, "Invalid door Id");
            }

            if (request.DoorId > 10)
            {
                return (true, "There are only 10 available doors");
            }

            return (false, "");
        }
    }
}
