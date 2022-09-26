using Contracts;
using Entities.DataTransferObjects.CarDTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Commands;
using Server.Queries;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsAsync(CancellationToken cancellationToken)
        {
            var carsDto = await _mediator.Send(new GetCarsQuery(false), cancellationToken);

            return Ok(carsDto);
        }

        [HttpGet("{id}", Name = "GetCarById")]
        public async Task<ActionResult> GetCarById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetCarByIdOuery(id, false), cancellationToken);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarAsync([FromBody] CarToCreateDto carToCreateDto,
            CancellationToken cancellationToken)
        {
            var carToReturn = await _mediator.Send(new CreateCarCommand(carToCreateDto), cancellationToken);

            return CreatedAtRoute("GetCarById", new { id = carToReturn.Id }, carToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] Guid id, [FromBody] CarToUpdateDto carToUpdateDto,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateCarCommand(carToUpdateDto, id), cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCarCommand(id), cancellationToken);

            return NoContent();
        }

    }
}
