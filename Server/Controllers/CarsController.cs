using Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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




    }
}
