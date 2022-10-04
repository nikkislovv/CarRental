using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using MediatR;

namespace Server.Queries.CarQueries
{
    public class GetCarByIdQuery : IRequest<CarToShowDto>
    {
        public Guid Id { get; set; }
        public bool trackChanges { get; set; }

        public GetCarByIdQuery(Guid id, bool trackChanges)
        {
            Id = id;
            this.trackChanges = trackChanges;
        }
    }
}
