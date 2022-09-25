using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RentRepository : RepositoryBase<Rent>, IRentRepository
    {
        public RentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateRent(Rent rent)
        {
            Create(rent);
        }

        public async Task<Rent> GetRentByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
        {
            return await FindByCondition(e => e.Id.Equals(id), trackChanges).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
