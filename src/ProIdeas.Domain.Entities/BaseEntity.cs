using ProIdeas.Domain.Repositories;

namespace ProIdeas.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public object Id { get; set; }
    }

    public abstract class BaseEntity<TId> : BaseEntity, IEntity<TId>
    {
        public new TId Id { get; set; }
    }
}
