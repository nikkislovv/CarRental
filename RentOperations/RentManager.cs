using Contracts;

namespace RentOperations
{
    public class RentManager : IRentManager
    {
        private readonly IRepositoryManager _repository;

        public RentManager(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task RentCar(Guid id, CancellationToken cancellationToken)
        {
            var car = await _repository.Car.GetCarByIdAsync(id, false, cancellationToken);

            if (car != null)
            {
                car.IsAvailable = false;
            }
        }
    }
}