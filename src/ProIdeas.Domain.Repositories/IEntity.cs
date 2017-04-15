using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Domain.Repositories
{
    public interface IEntity
    {
        object Id { get; set; }
    }

    public interface IEntity<TId> : IEntity
    {
        new TId Id { get; set; }
    }
}
