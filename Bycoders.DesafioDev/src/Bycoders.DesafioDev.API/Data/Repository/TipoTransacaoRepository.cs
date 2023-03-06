using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public class TipoTransacaoRepository : BaseRepositry<TipoTransacao>, ITipoTransacaoRepository
    {
        public TipoTransacaoRepository(TransacoesFinanceirasContext context) : base(context)
        { }

        public virtual async Task<IEnumerable<TipoTransacao>> GetAllTiposTransacao()
        {
            return await Context.AsNoTracking().ToListAsync();
        }

    }
}
