using System;
using System.Collections.Generic;

namespace ProIdeas.UI.Models.IdeaViewModels
{

    public class IdeaPageInfo
    {
        public IdeaPageInfo()
        {
            Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public string Id { get; private set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }

    public class IdeaInfoViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public bool IsFundingRequired { get; set; }

        public string FundingRequirement { get; set; }

        public IEnumerable<IdeaPageInfo> Pages { get; set; }

        public string OwnerId { get; set; }

        public bool IsOwner(string userId)
        {
            return OwnerId == userId;
        }

        public bool IsPublished => Status == "Published";



    }
}
