using Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IRepositoryManager _repository;

        public CarsController(IMediator mediator, IRepositoryManager repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        

    }
}
