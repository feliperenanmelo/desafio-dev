using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public abstract class BaseRepositry<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected DbSet<T> DB;

        protected BaseRepositry(TransacoesFinanceirasContext db)
        {
            DB = db.Set<T>();
        }

        public async Task AdicionarPorLista(List<T> entity)
        {
            await DB.AddRangeAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> ObterTodos(int tamanhoPagina, int indicePagina)
        {
            return await DB
                .AsNoTracking()
                .Skip(tamanhoPagina * (indicePagina - 1))
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ObterTodos()
        {
            return await DB
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<int> ObterQuantidadeRegistros()
        {
            return await DB.CountAsync();
        }
    }
}
