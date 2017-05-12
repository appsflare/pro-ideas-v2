using System.Collections.Generic;

namespace ProIdeas.Domain.Entities
{
    public class TeamMember
    {
        public string MemberUserId { get; set; }

        public User Member { get; set; }

        public IList<string> Roles { get; set; }
    }

    public class Team: BaseEntity
    {
        public string Name { get; set; }

        public string  IdeaId { get; set; }

        public IList<TeamMember> Members { get; set; }


    }
}
