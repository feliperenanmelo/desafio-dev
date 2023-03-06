using Bycoders.DesafioDev.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface ITipoTransacaoRepository : IRepository<TipoTransacao>
    {
        Task<IEnumerable<TipoTransacao>> GetAllTiposTransacao();
    }
}
