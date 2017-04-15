using System;

namespace ProIdeas.Domain.Entities
{
    public class IdeaLike : BaseEntity<string>
    {
        public string IdeaId { get; set; }

        public string UserId { get; set; }

        public bool IsLike { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
