using ProIdeas.Authentication.Contracts;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries.Activities;
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
        #region Private Readonly Fields
        private readonly IRepository _repository;
        private readonly IBus _bus;
        private readonly IDataMapper _dataMapper;
        private readonly IUserIdentityProvider _userIdentityProvider; 
        #endregion

        #region Ctors
        public ActivityLogic(IRepository repository, IBus bus, IDataMapper dataMapper, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _bus = bus;
            _dataMapper = dataMapper;
            _userIdentityProvider = userIdentityProvider;
        }
        #endregion

        #region IActivityLogic Implementations
        async public Task<IEnumerable<ActivityDto>> GetActivitiesAsync(string userId)
        {
            return await GetActivities(userId);
        }

        private async Task<IEnumerable<ActivityDto>> GetActivities(string userId, params string[] types)
        {
            var activities = await _repository.QueryAsync<Activity, GetActivityStreamByUserId>(new GetActivityStreamByUserId
            {
                UserId = userId,
                Types = types
            });
            return _dataMapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        public Task<IEnumerable<ActivityDto>> GetContributionsAsync(string userId)
        {
            return GetActivities(userId,
                    Activity.IDEAS_COMMENTS_CREATE,
                    Activity.IDEAS_CREATE,
                    Activity.IDEAS_PAGE_UPDATE,
                    Activity.IDEAS_PUBLISH,
                    Activity.IDEAS_UPDATE);
        }
        #endregion


        #region Domain Event Handler Implementations

        async public Task Handle(IdeaLikeChangedEvent message)
        {
            var idea = await GetIdea(message.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = Activity.IDEAS_VOTES,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemOwnerId = message.UserId,
                IdeaId = message.IdeaId,
                IdeaOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails
                {
                    IsUpVote = message.Like
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
                Type = Activity.IDEAS_COMMENTS_CREATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.Comment.Id,
                ItemOwnerId = message.Comment.OwnerId,
                IdeaId = message.Comment.IdeaId,
                IdeaOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails()
            });
        }

        async public Task Handle(IdeaPagesUpdatedEvent message)
        {
            var idea = await GetIdea(message.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = Activity.IDEAS_UPDATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.IdeaId,
                ItemOwnerId = idea.OwnerId,
                IdeaId = message.IdeaId,
                IdeaOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails()
            });
        }

        async public Task Handle(IdeaPublishedEvent message)
        {
            var idea = await GetIdea(message.IdeaId);
            await _repository.AddAsync(new Activity
            {
                Type = Activity.IDEAS_COMMENTS_CREATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.IdeaId,
                ItemOwnerId = idea.OwnerId,
                IdeaId = message.IdeaId,
                IdeaOwnerId = idea.OwnerId,
                ItemDetails = new ActivityItemDetails()
            });
        }

        async public Task Handle(IdeaUpdatedEvent message)
        {
            var idea = await GetIdea(message.Idea.Id);
            await _repository.AddAsync(new Activity
            {
                Type = Activity.IDEAS_UPDATE,
                Body = string.Empty,
                OwnerId = _userIdentityProvider.GetUserId(),
                CreatedAt = DateTime.UtcNow,
                ItemId = message.Idea.Id,
                ItemOwnerId = idea.OwnerId,
                IdeaId = message.Idea.Id,
                IdeaOwnerId = message.Idea.OwnerId,
                ItemDetails = new ActivityItemDetails()
            });
        } 
        #endregion


    }
}
