using ProIdeas.DTO;
using System.Collections.Generic;
using System.Linq;

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
                    IdeaDescription = idea.Description,
                    Owner = IdeaOwnerInfo.CreateFrom(idea.Owner)

                }).ToList().AsReadOnly()
            };
        }

        public class IdeaBannerItem
        {
            public string IdeaId { get; set; }
            public string IdeaName { get; set; }

            public string IdeaDescription { get; set; }

            public IdeaOwnerInfo Owner { get; set; }

        }
    }
}
