using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.HomeAutomation.Core.Features.Doors.Commands.CloseDoor;
using Zapalap.HomeAutomation.Core.Features.Doors.Commands.OpenDoor;

namespace Zapalap.HomeAutomation.WebApi.Controllers
{
    [Route("api/doors")]
    public class DoorsController : Controller
    {
        private readonly IMediator Mediator;

        public DoorsController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost("{doorId}/openRequests")]
        public async Task<IActionResult> OpenDoor([FromRoute]int doorId, [FromBody]OpenDoor command)
        {
            command.DoorId = doorId;
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result.Item);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("{doorId}/closeRequests")]
        public async Task<IActionResult> CloseDoor([FromRoute]int doorId, [FromBody]CloseDoor command)
        {
            command.DoorId = doorId;
            var result = await Mediator.Send(command);

            if (result.Success)
            {
                return Ok(result.Item);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
