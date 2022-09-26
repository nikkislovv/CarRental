using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.RentDTO
{
    public class RentToShowDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string RentalCarName { get; set; }
    }
}
