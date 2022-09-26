using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.RentDTO
{
    public class RentToCreateDto
    {
        public string ClientName { get; set; }
        public Guid CarId { get; set; }
    }
}
