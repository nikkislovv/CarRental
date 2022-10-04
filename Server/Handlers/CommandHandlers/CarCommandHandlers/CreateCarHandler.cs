using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using MediatR;
using Server.Commands;
using Server.Commands.CarCommands;

namespace Server.Handlers.CommandHandlers.CarCommandHandlers
{
    public class CreateCarHandler : IRequestHandler<CreateCarCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateCarHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request.CarToCreate);

            _repository.Car.CreateCar(car);

            await _repository.SaveAsync(cancellationToken);

            return car.Id;
        }
    }
}
