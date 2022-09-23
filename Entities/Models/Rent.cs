using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Rent
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
