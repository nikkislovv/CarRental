using Entities.DataTransferObjects.CarDTO;
using MediatR;

namespace Server.Commands
{
    public class UpdateCarCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public CarToUpdateDto carToUpdateDto { get; set; }

        public UpdateCarCommand(CarToUpdateDto carToUpdateDto, Guid id)
        {
            this.carToUpdateDto = carToUpdateDto;
            Id = id;
        }
    }
}
