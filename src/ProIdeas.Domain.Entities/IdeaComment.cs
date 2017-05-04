using System;

namespace ProIdeas.Domain.Entities
{
    public class IdeaComment : BaseEntity
    {
        public string IdeaId { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
