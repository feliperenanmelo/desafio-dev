using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public class TransacaoFinanceiraRepository : BaseRepositry<TransacaoFinanceira>, ITransacaoFinanceiraRepository
    {
        public TransacaoFinanceiraRepository(TransacoesFinanceirasContext context) : base(context)
        { }

        public override async Task<IEnumerable<TransacaoFinanceira>> GetAll(int pageSize, int page)
        {
            return await Context
                .Include(tran => tran.TipoTransacao)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }     
    }
}
