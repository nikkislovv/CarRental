using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using MediatR;
using Server.Commands;

namespace Server.Handlers.CommandHandlers
{
    public class CreateCarHandler : IRequestHandler<CreateCarCommand, CarToShowDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateCarHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public async Task<CarToShowDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request.CarToShow);

            _repository.Car.CreateCar(car);

            await _repository.SaveAsync(cancellationToken);

            return _mapper.Map<CarToShowDto>(car);
        }
    }
}
