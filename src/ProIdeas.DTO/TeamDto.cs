using System.Collections.Generic;

namespace ProIdeas.DTO
{
    public enum MemberStatus
    {
        None,
        Request,
        Approved,
        Rejected
    }

    public class TeamMemberDto
    {
        public string MemberUserId { get; set; }

        public UserDto Member { get; set; }

        public IList<string> Roles { get; set; }

        public MemberStatus Status { get; set; }

    }

    public class TeamDto : BaseDto<string>
    {
        public string IdeaId { get; set; }

        public IList<TeamMemberDto> Members { get; set; }

    }
}
