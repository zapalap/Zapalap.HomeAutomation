using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Zapalap.HomeAutomation.Core.Features.Doors.Dto;
using Zapalap.HomeAutomation.Core.Features.Doors.Validators;
using Zapalap.HomeAutomation.Core.Helpers.Results;

namespace Zapalap.HomeAutomation.Core.Features.Doors.Commands.CloseDoor
{
    public class CloseDoor : IRequest<RequestResult<DoorStateDto>>
    {
        public int DoorId { get; set; }
        public bool ShouldLock { get; set; }
    }
}
