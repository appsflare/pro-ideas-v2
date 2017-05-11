using ProIdeas.Logic.Contracts;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Events;
using System.Threading.Tasks;
using ProIdeas.Authentication.Contracts;
using ProIdeas.Infra.Commands.Collaboration;
using ProIdeas.Domain.Entities.Model;
using System;

namespace ProIdeas.Logic
{
    public class IdeaCollaborationLogic : IIdeaCollaborationLogic,
        IHandler<CreateIdeaCommentCommand>,
        IHandler<UpdateIdeaCommentCommand>,
        IHandler<DeleteIdeaCommentCommand>,
        IHandler<LikeIdeaCommand>,
        IHandler<IdeaLikeChangedEvent>,
        IHandler<IdeaCommentCreatedEvent>,
        IHandler<IdeaCommentUpdatedEvent>,
        IHandler<IdeaCommentDeletedEvent>
    {
        #region Private readonly fields
        private readonly IRepository _repository;
        private readonly IDataMapper _dataMapper;
        private readonly IBus _bus;
        private readonly IUserIdentityProvider _userIdentityProvider;
        #endregion

        #region Ctors
        public IdeaCollaborationLogic(IRepository repository, IDataMapper dataMapper, IBus bus, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _dataMapper = dataMapper;
            _bus = bus;
            _userIdentityProvider = userIdentityProvider;
        }
        #endregion

        #region IIdeaCollaborationLogic Implementation
        async public Task<IdeaCommentDto> GetComment(string commentId)
        {
            var comment = await _repository.QueryOneAsync<IdeaComment, GetCommentByIdQueryParameter>(new GetCommentByIdQueryParameter
            {
                CommentId = commentId
            });
            return _dataMapper.Map<IdeaCommentDto>(comment);
        }

        async public Task<IEnumerable<IdeaCommentDto>> GetComments(string ideaId)
        {
            var comments = await _repository.QueryAsync<IdeaComment, GetIdeaCommentsByIdeaIdQueryParameter>(new GetIdeaCommentsByIdeaIdQueryParameter
            {
                IdeaId = ideaId
            });
            return _dataMapper.Map<IEnumerable<IdeaCommentDto>>(comments);
        }

        async public Task<IdeaCollaborationStatsDto> GetStats(string ideaId)
        {
            var stats = await _repository.QueryOneAsync<IdeaCollaborationStats, GetIdeaCollaborationStatsQueryParameter>(new GetIdeaCollaborationStatsQueryParameter
            {
                IdeaId = ideaId
            });

            return _dataMapper.Map<IdeaCollaborationStatsDto>(stats);
        }

        #endregion

        #region CreateIdeaCommentCommand Implementation
        public Task Handle(CreateIdeaCommentCommand message)
        {
            message.Comment.CreatedOn = DateTime.UtcNow;
            var createdComment = _repository.Add(_dataMapper.Map<IdeaComment>(message.Comment));

            message.SetCommentId(createdComment.Id);

            _bus.RaiseEvent(new IdeaCommentCreatedEvent(_dataMapper.Map<IdeaCommentDto>(createdComment)));

            return Task.CompletedTask;

        }
        #endregion

        #region UpdateIdeaCommentCommand Implementation
        public Task Handle(UpdateIdeaCommentCommand message)
        {
            var existingComment = _repository.GetOne<IdeaComment>(message.Comment.Id);

            if (existingComment == null)
            { return Task.CompletedTask; }

            existingComment.Content = message.Comment.Content;
            existingComment.ModifiedOn = DateTime.UtcNow;

            var updatedComment = _repository.Update(existingComment);

            _bus.RaiseEvent(new IdeaCommentUpdatedEvent(_dataMapper.Map<IdeaCommentDto>(updatedComment)));

            return Task.CompletedTask;
        }
        #endregion

        #region DeleteIdeaCommentCommand Implementation
        public Task Handle(DeleteIdeaCommentCommand message)
        {
            var ideaComment = _repository.GetOne<IdeaComment>(message.CommentId);
            _repository.Delete(ideaComment);
            return _bus.RaiseEvent(new IdeaCommentDeletedEvent(_dataMapper.Map<IdeaCommentDto>(ideaComment)));
        }
        #endregion

        #region LikeIdeaCommand Implementation
        async public Task Handle(LikeIdeaCommand message)
        {
            var likeData = await _repository.QueryOneAsync<IdeaLike, GetIdeaLikeByUserIdQueryParameter>(new GetIdeaLikeByUserIdQueryParameter
            {
                IdeaId = message.IdeaId,
                OwnerId = message.UserId
            });

            var hasChanged = false;

            if (likeData == null)
            {
                likeData = new IdeaLike
                {
                    IdeaId = message.IdeaId,
                    OwnerId = message.UserId,
                    IsLike = message.Like
                };
                _repository.Add(likeData);
                hasChanged = true;
            }
            else
            {
                hasChanged = likeData.IsLike != message.Like;

                likeData.IsLike = message.Like;
                _repository.Update(likeData);

            }

            if (!hasChanged)
            { return; }

            await _bus.RaiseEvent(new IdeaLikeChangedEvent(message.IdeaId, message.UserId, message.Like));
        }
        #endregion


        #region IdeaLikeChangedEvent,IdeaCommentCreatedEvent,IdeaCommentUpdatedEvent,IdeaCommentDeletedEvent Implementation
        async public Task Handle(IdeaLikeChangedEvent message)
        {
            await UpdateIdeaStats(message.IdeaId);
        }

        async public Task Handle(IdeaCommentCreatedEvent message)
        {
            await UpdateIdeaStats(message.Comment.IdeaId);
        }

        async public Task Handle(IdeaCommentUpdatedEvent message)
        {
            await UpdateIdeaStats(message.Comment.IdeaId);
        }

        async public Task Handle(IdeaCommentDeletedEvent message)
        {
            await UpdateIdeaStats(message.Comment.IdeaId);
        }

        private async Task UpdateIdeaStats(string ideaId)
        {
            var stats = await _repository.QueryOneAsync<IdeaCollaborationStats, GetIdeaCollaborationStatsQueryParameter>(new GetIdeaCollaborationStatsQueryParameter
            {
                IdeaId = ideaId
            });

            if (stats == null)
            { return; }

            var idea = await _repository.GetOneAsync<Idea>(ideaId);

            idea.Likes = stats.Likes;
            idea.DisLikes = stats.DisLikes;
            idea.Comments = stats.Comments;

            _repository.Update(idea);

            await _bus.RaiseEvent(new IdeaStatsChangedEvent(_dataMapper.Map<IdeaDto>(idea)));
        }
        #endregion



    }
}
