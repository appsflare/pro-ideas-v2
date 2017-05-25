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

    public class TeamMemberViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string IdeaId { get; set; }
        public MemberStatus Status { get; set; }

        public static TeamMemberViewModel MapFrom(TeamMemberDto teamMemberDto, string ideaId, string userId)
        {
            return new TeamMemberViewModel()
            {
                IdeaId = ideaId,
                UserId = userId,
                Name = teamMemberDto != null ? teamMemberDto?.Member.FullName : string.Empty,
                Status = teamMemberDto?.Status != null ? (MemberStatus)teamMemberDto.Status : MemberStatus.None
            };
        }
    }
}
