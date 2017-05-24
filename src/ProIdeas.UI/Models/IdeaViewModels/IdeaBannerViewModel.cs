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
                    Id = idea.Id,
                    Name = idea.Title,
                    Description = idea.Description,
                    OwnerId = idea.OwnerId,
                    Owner = IdeaOwnerInfo.CreateFrom(idea.Owner)

                }).ToList().AsReadOnly()
            };
        }

        public class IdeaBannerItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string OwnerId { get; set; }
            public IdeaOwnerInfo Owner { get; set; }

        }
    }
}
