using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private ICarRepository _carRepository;
        private IRentRepository _entRepository;
        private RepositoryContext _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICarRepository Car
        {
            get
            {
                if (_carRepository == null)
                    _carRepository = new CarRepository(_repositoryContext);
                return _carRepository;
            }
        }
        public IRentRepository Rent
        {
            get
            {
                if (_entRepository == null)
                    _entRepository = new RentRepository(_repositoryContext);
                return _entRepository;
            }
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _repositoryContext.SaveChangesAsync(cancellationToken);
        }

    }
}
