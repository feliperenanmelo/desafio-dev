using Bycoders.DesafioDev.API.Domain.Interfaces;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransacoesFinanceirasContext _db;

        public UnitOfWork(TransacoesFinanceirasContext context)
        {
            _db = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
