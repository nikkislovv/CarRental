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
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public void CreateCar(Car car)
        {
            Create(car);
        }
        public void DeleteCar(Car car)
        {
            Delete(car);
        }
        public async Task<Car> GetCarByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
        {
            return await FindByCondition(e => e.Id.Equals(id), trackChanges).SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<Car>> GetAllCarsAsync(bool trackChanges, CancellationToken cancellationToken)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync(cancellationToken);
        }

        public void UpdateCar(Car car)
        {
            Update(car);
        }
    }
}
