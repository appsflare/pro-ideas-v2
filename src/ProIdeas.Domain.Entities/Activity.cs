using System;

namespace ProIdeas.Domain.Entities
{
    public class Activity : BaseEntity
    {
        public const string IDEAS_CREATE = "ideas.create";
        public const string IDEAS_UPDATE = "ideas.create";
        public const string IDEAS_PAGE_UPDATE = "ideas.page.update";
        public const string IDEAS_PUBLISH = "ideas.publish";
        public const string IDEAS_COMMENTS_CREATE = "ideas.comments.create";
        public const string IDEAS_VOTES = "ideas.votes";

        public string Body { get; set; }

        public string Type { get; set; }

        public string ItemId { get; set; }

        public string ItemOwnerId { get; set; }

        public User ItemOwner { get; set; }

        public ActivityItemDetails ItemDetails { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public DateTime CreatedAt { get; set; }

        public string IdeaId { get; set; }

        public string IdeaOwnerId { get; set; }

        public User IdeaOwner { get; set; }

    }

    public class ActivityItemDetails
    {
        public bool IsUpVote { get; set; }

    }
}
