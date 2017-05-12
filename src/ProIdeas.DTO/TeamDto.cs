using System.Collections.Generic;

namespace ProIdeas.DTO
{
    public class TeamMemberDto
    {
        public string MemberUserId { get; set; }

        public UserDto Member { get; set; }

        public IList<string> Roles { get; set; }
    }

    public class TeamDto : BaseDto<string>
    {
        public string Name { get; set; }

        public string IdeaId { get; set; }

        public IList<TeamMemberDto> Members { get; set; }

    }
}
