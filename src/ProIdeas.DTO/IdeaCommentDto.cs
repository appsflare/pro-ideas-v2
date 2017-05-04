using System;

namespace ProIdeas.DTO
{
    public class IdeaCommentDto : BaseDto<string>
    {
        public string IdeaId { get; set; }

        public string OwnerId { get; set; }

        public UserDto Owner { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
