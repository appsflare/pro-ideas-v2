using System;

namespace ProIdeas.DTO
{
    public class IdeaLikeDto : BaseDto<string>
    {
        public string IdeaId { get; set; }

        public string UserId { get; set; }

        public string IsLike { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
