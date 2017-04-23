using ProIdeas.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.UI.Models.IdeaViewModels
{
    public class IndexIdeasViewModel
    {
        private IndexIdeasViewModel()
        {

        }

        public IEnumerable<IdeaInfoViewModel> Ideas { get; set; }

        public static IndexIdeasViewModel MapFrom(IEnumerable<IdeaDto> ideas)
        {
            return new IndexIdeasViewModel
            {
                Ideas = ideas.Select(IdeaInfoViewModel.MapFrom)
            };
        }
    }
}
