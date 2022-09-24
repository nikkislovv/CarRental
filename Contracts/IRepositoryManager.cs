using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICarRepository Car { get; }
        IRentRepository Rent { get; }

        void Save();
        Task SaveAsync();
    }
}
