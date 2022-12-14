using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRentRepository
    {
        void CreateRent(Rent rent);
        Task<Rent> GetRentByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken);

    }
}
