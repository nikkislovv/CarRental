using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RentDTO;
using Entities.Models;
using MediatR;
using Server.Commands.RentCommands;

namespace Server.Handlers.CommandHandlers.RentCommandHandlers
{
    public class CreateRentHandler : IRequestHandler<CreateRentCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateRentHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var rentToCreate = _mapper.Map<Rent>(request.RentToCreateDto);

            _repository.Rent.CreateRent(rentToCreate);

            var car = await _repository.Car.GetCarByIdAsync(request.RentToCreateDto.CarId, true, cancellationToken);

            if (car != null)
            {
                car.IsAvailable = false;
            }

            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }


    }
}
