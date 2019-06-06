using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Features.Doors.Dto;
using Zapalap.HomeAutomation.Core.Helpers.Results;

namespace Zapalap.HomeAutomation.Core.Features.Doors.Commands.OpenDoor
{
    public class OpenDoorHandler : IRequestHandler<OpenDoor, RequestResult<DoorStateDto>>
    {
        public async Task<RequestResult<DoorStateDto>> Handle(OpenDoor request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Recieved open door request for {request.DoorId}");

            if (request.DoorId == 0)
            {
                Debug.WriteLine("Invalid door id");
                return new DoorStateDto().AsFailureResult("Invalid door");
            }

            if (request.DoorId > 10)
            {
                Debug.WriteLine("Door id too high");
                return new DoorStateDto().AsFailureResult("There are only 10 doors in this house");
            }

            if (request.DoorId == 5 && request.Password != "OpenSesame")
            {
                Debug.WriteLine("Incorrect password");
                return new DoorStateDto().AsFailureResult("Incorrect password");
            }

            return new DoorStateDto
            {
                DoorId = request.DoorId,
                DoorState = "Door Open"
            }.AsSuccessResult();
        }
    }
}
