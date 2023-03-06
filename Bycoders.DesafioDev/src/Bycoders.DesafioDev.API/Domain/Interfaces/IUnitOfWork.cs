using System;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {   
        Task<bool> CommitAsync();
    }
}
