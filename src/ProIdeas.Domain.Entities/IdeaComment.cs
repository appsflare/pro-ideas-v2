using System;

namespace ProIdeas.Domain.Entities
{
    public class IdeaComment : BaseEntity
    {
        public string IdeaId { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
