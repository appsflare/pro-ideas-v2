using System;
using System.Collections.Generic;

namespace ProIdeas.DTO
{
    public class IdeaDto : BaseDto<string>
    {
        public IdeaDto()
        {
            Pages = new List<PageDto>();
        }

        public string OwnerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsFundingRequired { get; set; }

        public string FundingRequirement { get; set; }

        public string BannerImage { get; set; }

        public IList<PageDto> Pages { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Status { get; set; }
    }
}
