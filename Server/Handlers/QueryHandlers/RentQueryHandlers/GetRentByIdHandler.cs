using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RentDTO;
using MediatR;
using Server.Queries.RentQueries;

namespace Server.Handlers.QueryHandlers.RentQueryHandlers
{
    public class GetRentByIdHandler : IRequestHandler<GetRentByIdQuery, RentToShowDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetRentByIdHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RentToShowDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
        {
            var rent = await _repository.Rent.GetRentByIdAsync(request.Id, request.trackChanges, cancellationToken);

            return _mapper.Map<RentToShowDto>(rent);
        }
    }
}
