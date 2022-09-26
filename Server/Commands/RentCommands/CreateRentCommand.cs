using Entities.DataTransferObjects.RentDTO;
using MediatR;

namespace Server.Commands.RentCommands
{
    public class CreateRentCommand : IRequest<RentToShowDto>
    {
        public RentToCreateDto RentToCreateDto { get; set; }

        public CreateRentCommand(RentToCreateDto rentToCreateDto)
        {
            RentToCreateDto = rentToCreateDto;
        }
    }
}
