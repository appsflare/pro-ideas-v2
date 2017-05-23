using ProIdeas.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.UI.Models.TeamViewModels
{
    public class TeamDetailsViewModel
    {
        public string IdeaId { get; set; }
        public string TeamId { get; set; }

        public IList<TeamMemberViewModel> Members { get; set; }

        public static  TeamDetailsViewModel MapFrom(TeamDto teamDto)
        {
            var team = new TeamDetailsViewModel
            {
                IdeaId = teamDto.IdeaId,
                TeamId = teamDto.Id,
                Members =  new List<TeamMemberViewModel>()
            };

            teamDto.Members.ToList().ForEach
                (x => team.Members.Add(TeamMemberViewModel.MapFrom(x, teamDto.IdeaId, x.MemberUserId)));
            return team;
        }

    }
}
