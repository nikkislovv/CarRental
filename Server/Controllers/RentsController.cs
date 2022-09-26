using Entities.DataTransferObjects.RentDTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Commands.RentCommands;
using Server.Queries.RentQueries;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var rentDto = await _mediator.Send(new GetRentByIdQuery(id, true), cancellationToken);

            return Ok(rentDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentAsync([FromBody] RentToCreateDto rentToCreateDto,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateRentCommand(rentToCreateDto), cancellationToken);

            return NoContent();
        }
    }
}
