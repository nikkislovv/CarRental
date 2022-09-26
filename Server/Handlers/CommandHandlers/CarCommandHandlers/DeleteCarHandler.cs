using Contracts;
using MediatR;
using Server.Commands;
using Server.Commands.CarCommands;

namespace Server.Handlers.CommandHandlers.CarCommandHandlers
{
    public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        public DeleteCarHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _repository.Car.GetCarByIdAsync(request.Id, false, cancellationToken);

            _repository.Car.DeleteCar(car);

            await _repository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
