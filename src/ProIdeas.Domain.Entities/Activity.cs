using System;

namespace ProIdeas.Domain.Entities
{
    public class Activity : BaseEntity
    {

        public string Body { get; set; }

        public string Type { get; set; }

        public string ItemId { get; set; }

        public string ItemOwnerId { get; set; }

        public User ItemOwner { get; set; }

        public ActivityItemDetails ItemDetails { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public DateTime CreatedAt { get; set; }

    }

    public class ActivityItemDetails
    {
        public string IdeaId { get; set; }

        public string TargetOwnerId { get; set; }

        public bool IsUpVote { get; set; }

        public User TargetOwner { get; set; }
    }
}
