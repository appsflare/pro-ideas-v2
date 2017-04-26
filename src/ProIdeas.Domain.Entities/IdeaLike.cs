using System;

namespace ProIdeas.Domain.Entities
{
    public class IdeaLike : BaseEntity
    {
        public string IdeaId { get; set; }

        public string OwnerId { get; set; }

        public bool IsLike { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public User Owner { get; set; }
    }
}
