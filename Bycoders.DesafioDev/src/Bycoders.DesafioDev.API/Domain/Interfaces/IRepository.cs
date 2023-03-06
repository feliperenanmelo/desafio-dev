using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task AddRange(List<T> entities);
        Task<IEnumerable<T>> GetAll(int pageSize, int page);
        Task<int> GetCount();
    }
}
