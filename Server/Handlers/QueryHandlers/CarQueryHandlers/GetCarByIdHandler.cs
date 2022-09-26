using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.CarDTO;
using MediatR;
using Server.Queries;
using Server.Queries.CarQueries;

namespace Server.Handlers.QueryHandlers.CarHandlers
{
    public class GetCarByIdHandler : IRequestHandler<GetCarByIdOuery, CarToShowDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetCarByIdHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CarToShowDto> Handle(GetCarByIdOuery request, CancellationToken cancellationToken)
        {
            var carDto = await _repository.Car.GetCarByIdAsync(request.Id, request.trackChanges, cancellationToken);
            return _mapper.Map<CarToShowDto>(carDto);
        }
    }
}
