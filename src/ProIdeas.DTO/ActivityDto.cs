using System;

namespace ProIdeas.DTO
{
    public class ActivityDto : BaseDto<string>
    {

        public string Body { get; set; }

        public string Type { get; set; }

        public string ItemId { get; set; }

        public string ItemOwnerId { get; set; }

        public UserDto ItemOwner { get; set; }

        public ActivityItemDetailsDto ItemDetails { get; set; }

        public string OwnerId { get; set; }

        public UserDto Owner { get; set; }

        public DateTime CreatedAt { get; set; }

        public string IdeaId { get; set; }

        public string IdeaOwnerId { get; set; }

        public UserDto IdeaOwner { get; set; }

    }

    public class ActivityItemDetailsDto
    {        
        public bool IsUpVote { get; set; }        
    }
}
