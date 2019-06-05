using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zapalap.HomeAutomation.Core.Features.Doors.Dto;
using Zapalap.HomeAutomation.Core.Features.Doors.Validators;
using Zapalap.HomeAutomation.Core.Helpers.Results;

namespace Zapalap.HomeAutomation.Core.Features.Doors.Commands.OpenDoor
{
    public class OpenDoor : IRequest<RequestResult<DoorStateDto>>, INeedGeneralDoorValidation
    {
        public int DoorId { get; set; }
        public string Password { get; set; }
    }
}
