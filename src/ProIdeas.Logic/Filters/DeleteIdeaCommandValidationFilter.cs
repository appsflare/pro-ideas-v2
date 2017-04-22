﻿using ProIdeas.Authentication.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Idea;
using System;

namespace ProIdeas.Logic.Filters
{
    public class DeleteIdeaCommandValidationFilter : BaseMessageValidationFilter<DeleteIdeaCommand>
    {
        private readonly IUserIdentityProvider _userIdentityProvider;
        private readonly IRepository _repository;
        public DeleteIdeaCommandValidationFilter(IUserIdentityProvider userIdentityProvider, IRepository repository)
        {
            _userIdentityProvider = userIdentityProvider;
            _repository = repository;
        }

        protected override void Validate(FilterContext<DeleteIdeaCommand> context)
        {
            if (!context.Message.IsValid())
            {
                throw new ArgumentException(nameof(context.Message));
            }

           var idea =  _repository.GetOne<Idea>(context.Message.IdeaId);

            if(idea == null)
            {
                throw new InvalidOperationException("invalid idea");
            }

            if (idea.OwnerId != _userIdentityProvider.GetUserId())
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}