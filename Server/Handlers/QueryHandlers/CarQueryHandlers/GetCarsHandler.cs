using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using MediatR;
using Server.Queries;
using Server.Queries.CarQueries;

namespace Server.Handlers.QueryHandlers.CarHandlers
{
    public class GetCarsHandler : IRequestHandler<GetCarsQuery, IEnumerable<CarToShowDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetCarsHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarToShowDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _repository.Car.GetAllCarsAsync(request.trackChanges, cancellationToken);
            return _mapper.Map<IEnumerable<CarToShowDto>>(cars);
        }
    }
}
