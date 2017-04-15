﻿using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Commands.Idea
{
    public class UpdateIdeaCommand : Command
    {
        public IdeaDto Idea { get; private set; }

        public UpdateIdeaCommand(IdeaDto idea)
        {
            Idea = idea;
        }

        public override bool IsValid()
        {
            return Idea != null;
        }
    }
}