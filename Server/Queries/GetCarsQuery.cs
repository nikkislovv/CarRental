using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using MediatR;

namespace Server.Queries
{
    public class GetCarsQuery : IRequest<IEnumerable<CarToShowDto>>
    { 
        public bool trackChanges { get; set; }

        public GetCarsQuery(bool trackChanges)
        {
            this.trackChanges = trackChanges;
        }
    }
}
