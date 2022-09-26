using Entities.DataTransferObjects.RentDTO;
using MediatR;

namespace Server.Commands.RentCommands
{
    public class CreateRentCommand : IRequest<Unit>
    {
        public RentToCreateDto RentToCreateDto { get; set; }

        public CreateRentCommand(RentToCreateDto rentToCreateDto)
        {
            RentToCreateDto = rentToCreateDto;
        }
    }
}
