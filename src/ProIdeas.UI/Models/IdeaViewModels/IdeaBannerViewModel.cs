using ProIdeas.DTO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace ProIdeas.UI.Models.IdeaViewModels
{
    public class IdeaBannerViewModel
    {
        public IReadOnlyList<IdeaBannerItem> Ideas { get; set; }
        public static IdeaBannerViewModel MapFrom(IEnumerable<IdeaDto> ideas)
        {
            return new IdeaBannerViewModel
            {
                Ideas = ideas.Select(idea => new IdeaBannerItem
                {
                    IdeaId = idea.Id,
                    IdeaName = idea.Title,
                }).ToList().AsReadOnly()
            };
        }

        public class IdeaBannerItem
        {
            public string IdeaId { get; set; }
            public string IdeaName { get; set; }
        }
    }
}
