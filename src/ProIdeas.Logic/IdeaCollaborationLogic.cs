using ProIdeas.Authentication.Contracts;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Entities.Model;
using ProIdeas.Domain.Queries;
using ProIdeas.Domain.Repositories;
using ProIdeas.DTO;
using ProIdeas.Exceptions;
using ProIdeas.Infra.Commands.Collaboration;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class IdeaCollaborationLogic : IIdeaCollaborationLogic,
        IHandler<CreateIdeaCommentCommand>,
        IHandler<UpdateIdeaCommentCommand>,
        IHandler<DeleteIdeaCommentCommand>,
        IHandler<LikeIdeaCommand>,
        IHandler<CreateTeamCommand>,
        IHandler<IdeaLikeChangedEvent>,
        IHandler<IdeaCommentCreatedEvent>,
        IHandler<IdeaCommentUpdatedEvent>,
        IHandler<IdeaCommentDeletedEvent>,
        IHandler<IdeaPublishedEvent>,
        IHandler<IdeaUnpublishedEvent>
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
        public async Task<IdeaCommentDto> GetCommentAsync(string commentId)
        {
            var comment = await _repository.QueryOneAsync<IdeaComment, GetCommentByIdQuery>(new GetCommentByIdQuery
            {
                CommentId = commentId
            });
            return _dataMapper.Map<IdeaCommentDto>(comment);
        }

        public async Task<IEnumerable<IdeaCommentDto>> GetCommentsAsync(string ideaId)
        {
            var comments = await _repository.QueryAsync<IdeaComment, GetIdeaCommentsByIdeaIdQuery>(new GetIdeaCommentsByIdeaIdQuery
            {
                IdeaId = ideaId
            });
            return _dataMapper.Map<IEnumerable<IdeaCommentDto>>(comments);
        }

        public async Task<IdeaCollaborationStatsDto> GetStatsAsync(string ideaId)
        {
            var stats = await _repository.QueryOneAsync<IdeaCollaborationStats, GetIdeaCollaborationStatsQuery>(new GetIdeaCollaborationStatsQuery
            {
                IdeaId = ideaId
            });

            return _dataMapper.Map<IdeaCollaborationStatsDto>(stats);
        }

        public async Task<TeamDto> GetTeamAsync(string ideaId)
        {
            var team = await _repository.QueryOneAsync<Team, GetTeamByIdeaIdQuery>(new GetTeamByIdeaIdQuery
            {
                IdeaId = ideaId
            });

            if (team == null)
            { throw new LogicalException(ErrorCategory.NotFound, "Team not found"); }

            return _dataMapper.Map<TeamDto>(team);
        }

        #endregion

        #region CreateIdeaCommentCommand Implementation
        public Task Handle(CreateIdeaCommentCommand message)
        {
            message.Comment.CreatedOn = DateTime.UtcNow;
            var createdComment = _repository.Add(_dataMapper.Map<IdeaComment>(message.Comment));

            message.SetCommentId(createdComment.Id);

            return _bus.RaiseEvent(new IdeaCommentCreatedEvent(_dataMapper.Map<IdeaCommentDto>(createdComment)));


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

            return _bus.RaiseEvent(new IdeaCommentUpdatedEvent(_dataMapper.Map<IdeaCommentDto>(updatedComment)));
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
        public async Task Handle(LikeIdeaCommand message)
        {
            var likeData = await _repository.QueryOneAsync<IdeaLike, GetIdeaLikeByUserIdQuery>(new GetIdeaLikeByUserIdQuery
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

        #region CreateTeamCommand Implementation
        public async Task Handle(CreateTeamCommand message)
        {
            var team = _dataMapper.Map<Team>(message.Team);

            var createdTeam = await _repository.AddAsync(team);

            await _bus.RaiseEvent(new TeamCreatedEvent(_dataMapper.Map<TeamDto>(team)));
        }
        #endregion


        #region IdeaLikeChangedEvent,IdeaCommentCreatedEvent,IdeaCommentUpdatedEvent,IdeaCommentDeletedEvent Implementation
        public async Task Handle(IdeaLikeChangedEvent message)
        {
            await UpdateIdeaStats(message.IdeaId);
        }

        public async Task Handle(IdeaCommentCreatedEvent message)
        {
            await UpdateIdeaStats(message.Comment.IdeaId);
        }

        public async Task Handle(IdeaCommentUpdatedEvent message)
        {
            await UpdateIdeaStats(message.Comment.IdeaId);
        }

        public async Task Handle(IdeaCommentDeletedEvent message)
        {
            await UpdateIdeaStats(message.Comment.IdeaId);
        }
        private async Task UpdateIdeaStats(string ideaId)
        {
            var stats = await _repository.QueryOneAsync<IdeaCollaborationStats, GetIdeaCollaborationStatsQuery>(new GetIdeaCollaborationStatsQuery
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

        public Task Handle(IdeaPublishedEvent message)
        {
            return UpdateIdeaStats(message.IdeaId);
        }

        public Task Handle(IdeaUnpublishedEvent message)
        {
            return UpdateIdeaStats(message.IdeaId);
        }
        #endregion
    }
}
