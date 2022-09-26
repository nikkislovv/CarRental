using Entities.DataTransferObjects.RentDTO;
using MediatR;

namespace Server.Queries.RentQueries
{
    public class GetRentByIdQuery : IRequest<RentToShowDto>
    {
        public Guid Id { get; set; }
        public bool trackChanges { get; set; }

        public GetRentByIdQuery(Guid id, bool trackChanges)
        {
            Id = id;

            this.trackChanges = trackChanges;
        }
    }
}
