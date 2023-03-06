using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public abstract class BaseRepositry<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        protected DbSet<T> Context;

        protected BaseRepositry(TransacoesFinanceirasContext context)
        {
            Context = context.Set<T>();
        }

        public async Task AddRange(List<T> entities)
        {
            await Context.AddRangeAsync(entities);
        }

        public virtual async Task<IEnumerable<T>> GetAll(int pageSize, int page)
        {
            return await Context
                .AsNoTracking().Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<int> GetCount()
        {
            return await Context.CountAsync();
        }
    }
}
