using MediatR;
using System;
using System.Collections.Generic;
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
            if (request.ShouldLock)
            {
                doorState = "Closed and locked";
            }
            else
            {
                doorState = "Closed but not locked";
            }

            return new DoorStateDto
            {
                DoorId = request.DoorId,
                DoorState = doorState
            }.AsSuccessResult();
        }
    }
}
