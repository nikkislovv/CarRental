using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICarRepository
    {
        void CreateCar(Car car);
        void DeleteCar(Car car);
        Task<IEnumerable<Car>> GetAllCarsAsync(bool trackChanges, CancellationToken cancellationToken);
        void UpdateCar(Car car);
        Task<Car> GetCarByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken);
    }
}
