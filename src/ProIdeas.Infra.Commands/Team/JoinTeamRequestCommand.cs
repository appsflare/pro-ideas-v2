﻿using ProIdeas.Domain.Core.Commands;

namespace ProIdeas.Infra.Commands.Team
{

    public class JoinTeamRequestCommand:Command
    {
        public string IdeaId { get; private set; }
        public string UserId { get; private set; }

        public JoinTeamRequestCommand(string userId, string ideaId)
        {
            UserId = userId;
            IdeaId = ideaId;
            
        }

        public override bool IsValid()
        {
            return (!string.IsNullOrEmpty(IdeaId) || !string.IsNullOrEmpty(UserId));
        }
    }
}
