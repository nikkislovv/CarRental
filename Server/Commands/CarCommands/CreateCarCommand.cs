using Entities.DataTransferObjects.CarDTO;
using MediatR;

namespace Server.Commands.CarCommands
{
    public class CreateCarCommand : IRequest<CarToShowDto>
    {
        public CarToCreateDto CarToShow { get; set; }

        public CreateCarCommand(CarToCreateDto carToShow)
        {
            CarToShow = carToShow;
        }
    }
}
