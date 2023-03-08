using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task AdicionarPorLista(List<T> entities);
        Task<IEnumerable<T>> ObterTodos(int pageSize, int page);
        Task<IEnumerable<T>> ObterTodos();
        Task<int> ObterQuantidadeRegistros();
    }
}
