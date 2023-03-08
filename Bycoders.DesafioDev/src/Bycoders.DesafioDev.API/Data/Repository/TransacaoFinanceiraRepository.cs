using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public class TransacaoFinanceiraRepository : BaseRepositry<TransacaoFinanceira>, ITransacaoFinanceiraRepository
    {
        public TransacaoFinanceiraRepository(TransacoesFinanceirasContext context) : base(context)
        { }        
    }
}
