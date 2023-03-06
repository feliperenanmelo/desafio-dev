using Bycoders.DesafioDev.API.Domain.Interfaces;

namespace Bycoders.DesafioDev.API.Domain.Entities
{
    public abstract class BaseEntity : IAggregateRoot
    {
        public int Id { get; set; }        
    }
}
