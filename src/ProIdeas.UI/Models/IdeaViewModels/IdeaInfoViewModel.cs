using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.UI.Models.IdeaViewModels
{
    public class IdeaInfoViewModel
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsFundingRequired { get; set; }

        public string FundingRequirement { get; set; }
    }
}
