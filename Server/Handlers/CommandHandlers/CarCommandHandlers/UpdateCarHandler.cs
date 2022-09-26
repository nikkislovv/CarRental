using AutoMapper;
using Contracts;
using MediatR;
using Server.Commands;
using Server.Commands.CarCommands;

namespace Server.Handlers.CommandHandlers.CarCommandHandlers
{
    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public UpdateCarHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _repository.Car.GetCarByIdAsync(request.Id, true, cancellationToken);

            _mapper.Map(request.carToUpdateDto, car);

            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
