using System;

namespace ProIdeas.DTO
{
    public class IdeaLikeDto : BaseDto<string>
    {
        public string IdeaId { get; set; }

        public string OwnerId { get; set; }

        public UserDto User { get; set; }

        public string IsLike { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
