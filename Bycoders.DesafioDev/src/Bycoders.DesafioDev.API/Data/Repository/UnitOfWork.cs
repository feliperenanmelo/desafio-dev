using Bycoders.DesafioDev.API.Domain.Interfaces;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransacoesFinanceirasContext _context;

        public UnitOfWork(TransacoesFinanceirasContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
