using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RentDTO;
using Entities.Models;
using MediatR;
using Server.Commands.RentCommands;

namespace Server.Handlers.CommandHandlers.RentCommandHandlers
{
    public class CreateRentHandler:IRequestHandler<CreateRentCommand,RentToShowDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateRentHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RentToShowDto> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var rent = _mapper.Map<Rent>(request.RentToCreateDto);

            _repository.Rent.CreateRent(rent);

            await _repository.SaveAsync(cancellationToken);

            return _mapper.Map<RentToShowDto>(rent);
        }


    }
}
