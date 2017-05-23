using System.Collections.Generic;

namespace ProIdeas.Domain.Entities
{

    public enum MemberStatus
    {
        None,
        Request,
        Approved,
        Rejected
    }

    public class TeamMember
    {
        public string MemberUserId { get; set; }

        public User Member { get; set; }

        public IList<string> Roles { get; set; }

        public MemberStatus Status { get; set; }
    }

    public class Team : BaseEntity
    {        
        public string IdeaId { get; set; }

        private IList<TeamMember> _members;
        public IList<TeamMember> Members
        {
            get
            {
                return _members ?? (_members = new List<TeamMember>());
            }

            set
            {
                _members = value;
            }

        }
    }
}
