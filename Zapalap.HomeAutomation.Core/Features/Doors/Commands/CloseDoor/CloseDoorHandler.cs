using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Features.Doors.Dto;
using Zapalap.HomeAutomation.Core.Helpers.Results;

namespace Zapalap.HomeAutomation.Core.Features.Doors.Commands.CloseDoor
{
    public class CloseDoorHandler : IRequestHandler<CloseDoor, RequestResult<DoorStateDto>>
    {
        public async Task<RequestResult<DoorStateDto>> Handle(CloseDoor request, CancellationToken cancellationToken)
        {
            string doorState;

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

            if (request.ShouldLock)
            {
                Debug.WriteLine($"Closing door with id {request.DoorId}");
                doorState = "Closed and locked";
            }
            else
            {
                Debug.WriteLine($"Closing and locking door with id {request.DoorId}");
                doorState = "Closed but not locked";
            }

            Debug.WriteLine("Successfully closed door");

            return new DoorStateDto
            {
                DoorId = request.DoorId,
                DoorState = doorState
            }.AsSuccessResult();
        }
    }
}
