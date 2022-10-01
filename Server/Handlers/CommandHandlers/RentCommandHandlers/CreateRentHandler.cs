using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RentDTO;
using Entities.Models;
using MediatR;
using Server.Commands.RentCommands;
using Server.Exeptions;

namespace Server.Handlers.CommandHandlers.RentCommandHandlers
{
    public class CreateRentHandler : IRequestHandler<CreateRentCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        private readonly IRentManager _rentManager;

        public CreateRentHandler(IRepositoryManager repository, IMapper mapper, IRentManager rentManager)
        {
            _repository = repository;
            _mapper = mapper;
            _rentManager = rentManager;
        }

        public async Task<Unit> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var car = await _repository.Car.GetCarByIdAsync(request.RentToCreateDto.CarId, false, cancellationToken);

            if (car == null)
            {
                throw new NotFoundException(nameof(car), request.RentToCreateDto.CarId);
            }

            var rentToCreate = _mapper.Map<Rent>(request.RentToCreateDto);

            _repository.Rent.CreateRent(rentToCreate);

            await _rentManager.RentCar(request.RentToCreateDto.CarId, cancellationToken);

            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }


    }
}
