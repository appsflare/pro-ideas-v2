using System;
using ProIdeas.DTO;

namespace ProIdeas.UI.Models.TeamViewModels
{
    public enum MemberStatus
    {
        None,
        Request,
        Approved,
        Rejected
    }

    public class TeamMemberStatusViewModel
    {
        public string UserId { get; set; }
        public string IdeaId { get; set; }
        public MemberStatus Status { get; set; }

        public static TeamMemberStatusViewModel MapFrom(TeamMemberDto teamMemberDto, string ideaId,string userId)
        {
            return new TeamMemberStatusViewModel()
            {
                IdeaId = ideaId,
                UserId = userId,
                Status = teamMemberDto?.Status != null ? (MemberStatus)teamMemberDto.Status : MemberStatus.None
            };
        }
    }
}
