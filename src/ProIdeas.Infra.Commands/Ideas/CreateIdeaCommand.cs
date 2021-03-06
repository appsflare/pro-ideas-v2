﻿using ProIdeas.Domain.Core.Commands;
using ProIdeas.DTO;

namespace ProIdeas.Infra.Commands.Ideas
{
    public class CreateIdeaCommand : Command
    {
        public IdeaDto Idea { get; private set; }

        public CreateIdeaCommand(IdeaDto idea)
        {
            Idea = idea;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Idea?.Title?.Trim()) 
                && !string.IsNullOrEmpty(Idea.Description.Trim())
                && Idea.Title.Length <= 100 && Idea.Description.Length <= 500;
        }
    }
}
