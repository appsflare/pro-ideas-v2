using ProIdeas.Authentication.Contracts;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.DTO;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class ActivityLogic : IActivityLogic,
        IHandler<IdeaLikeChangedEvent>,
        IHandler<IdeaCommentCreatedEvent>,
        IHandler<IdeaPagesUpdatedEvent>,
        IHandler<IdeaPublishedEvent>,
        IHandler<IdeaUpdatedEvent>
    {
        private readonly IRepository _repository;
        private readonly IBus _bus;
        private readonly IDataMapper _dataMapper;
        private readonly IUserIdentityProvider _userIdentityProvider;

        private const string IDEAS_CREATE = "ideas.create";
        private const string IDEAS_UPDATE = "ideas.create";
        private const string IDEAS_PGAE_UPDATE = "ideas.page.update";
        private const string IDEAS_PUBLISH = "ideas.publish";
        private const string IDEAS_COMMENTS_CREATE = "ideas.comments.create";
        private const string IDEAS_VOTES = "ideas.votes";

        public ActivityLogic(IRepository repository, IBus bus, IDataMapper dataMapper, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _bus = bus;
            _dataMapper = dataMapper;
            _userIdentityProvider = userIdentityProvider;
        }

        public Task<IEnumerable<ActivityDto>> GetActivities(string userId)
        {
            throw new NotImplementedException();
        }

        async public Task Handle(IdeaLikeChangedEvent message)
        {
            var idea = await GetIdea(message.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = IDEAS_VOTES,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemOwnerId = message.UserId,                
                ItemDetails = new ActivityItemDetails
                {
                    IdeaId = message.IdeaId,
                    IsUpVote = message.Like,
                    TargetOwnerId = idea.OwnerId
                }
            });

        }

        private async Task<Idea> GetIdea(string id)
        {
            return await _repository.GetOneAsync<Idea>(id);
        }

        async public Task Handle(IdeaCommentCreatedEvent message)
        {
            var idea = await GetIdea(message.Comment.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = IDEAS_COMMENTS_CREATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.Comment.Id,
                ItemOwnerId = message.Comment.OwnerId,
                ItemDetails = new ActivityItemDetails
                {
                    IdeaId = message.Comment.IdeaId,
                    TargetOwnerId = idea.OwnerId
                }
            });
        }

        async public Task Handle(IdeaPagesUpdatedEvent message)
        {
            var idea = await GetIdea(message.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = IDEAS_UPDATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.IdeaId,
                ItemOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails
                {
                    IdeaId = message.IdeaId
                }
            });
        }

        async public Task Handle(IdeaPublishedEvent message)
        {
            var idea = await GetIdea(message.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = IDEAS_COMMENTS_CREATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.IdeaId,
                ItemOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails
                {
                    IdeaId = message.IdeaId
                }
            });
        }

        async public Task Handle(IdeaUpdatedEvent message)
        {
            var idea = await GetIdea(message.Idea.Id);
            await _repository.AddAsync(new Activity
            {
                Type = IDEAS_UPDATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.Idea.Id,
                ItemOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails
                {
                    IdeaId = message.Idea.Id
                }
            });
        }
    }
}
