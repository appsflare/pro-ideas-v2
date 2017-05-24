using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Ideas;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Events;
using System.Threading.Tasks;
using ProIdeas.Authentication.Contracts;

namespace ProIdeas.Logic
{
    public class IdeaLogic : IIdeaLogic,
        IHandler<CreateIdeaCommand>,
        IHandler<UpdateIdeaCommand>,
        IHandler<DeleteIdeaCommand>,
        IHandler<PublishIdeaCommand>,
        IHandler<UnpublishIdeaCommand>
    {
        #region Private readonly fields
        private readonly IRepository _repository;
        private readonly IDataMapper _dataMapper;
        private readonly IBus _bus;
        private readonly IUserIdentityProvider _userIdentityProvider;
        #endregion

        #region Ctors
        public IdeaLogic(IRepository repository, IDataMapper dataMapper, IBus bus, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _dataMapper = dataMapper;
            _bus = bus;
            _userIdentityProvider = userIdentityProvider;
        }
        #endregion

        #region IIdeaLogic Implementation
        public async Task<IdeaDto> GetIdea(string ideaId)
        {
            return _dataMapper.Map<IdeaDto>(await _repository.QueryOneAsync<Idea, GetIdeaByIdQuery>(new GetIdeaByIdQuery
            {
                IdeaId = ideaId
            }));
        }

        public async Task<IEnumerable<IdeaDto>> GetIdeas(int pageSize, int page, string keyword)
        {
            var result = await _repository.QueryAsync<Idea, FilterIdeaQuery>(new FilterIdeaQuery
            {
                Take = pageSize,
                Skip = Math.Max(page, 0) * pageSize,
                Keyword = keyword,
                Status = Status.Published.ToString(),
                OrderBy = nameof(Idea.Title)
            });

            return _dataMapper.Map<IEnumerable<IdeaDto>>(result);
        }

        public async Task<IEnumerable<IdeaDto>> GetTopIdeas(int count)
        {
            var result = await _repository.QueryAsync<Idea, GetTopIdeaQuery>(new GetTopIdeaQuery
            {
                Count = count
            });

            return _dataMapper.Map<IEnumerable<IdeaDto>>(result);
        }


        public Task<IEnumerable<IdeaCommentDto>> GetIdeaComments(string ideaId, int pageSize, int page)
        {
            return Task.Factory.StartNew(() =>
            {
                var query = new QueryBuilder<IdeaComment>()
                .WithCondition(i => i.Id == ideaId)
                .Skip(Math.Max(page, 0) * pageSize)
                .Take(pageSize)
                .Build();


                return _dataMapper.Map<IEnumerable<IdeaCommentDto>>(_repository.Query(query));
            });
        }


        public Task<IEnumerable<IdeaLikeDto>> GetFollowers(string ideaId)
        {
            return Task.Factory.StartNew(() =>
            {
                var query = new QueryBuilder<IdeaLike>()
                .WithCondition(i => i.IdeaId == ideaId && i.IsLike)
                .Build();


                return _dataMapper.Map<IEnumerable<IdeaLikeDto>>(_repository.Query(query));
            });
        }

        public async Task<IEnumerable<IdeaDto>> GetUserIdeas(string userId, int pageSize, int page, string keyword)
        {
            var result = await _repository.QueryAsync<Idea, FilterIdeaQuery>(new FilterIdeaQuery
            {
                Take = pageSize,
                Skip = Math.Max(page, 0) * pageSize,
                Keyword = keyword,
                OrderBy = nameof(Idea.Title),
                OwnerId = userId
            });

            return _dataMapper.Map<IEnumerable<IdeaDto>>(result);
        }
        #endregion

        #region CreateIdeaCommand Implementation
        public Task Handle(CreateIdeaCommand message)
        {

            var newIdea = _dataMapper.Map<Idea>(message.Idea);
            newIdea.Status = Status.Draft.ToString();

            newIdea.CreatedAt = DateTime.UtcNow;

            var result = _repository.Add(newIdea);

            var createdIdea = _dataMapper.Map<IdeaDto>(result);

            message.Idea.Id = createdIdea.Id;


            return _bus.RaiseEvent(new IdeaCreatedEvent(_dataMapper.Map<IdeaDto>(result)));
        }
        #endregion

        #region UpdateIdeaCommand Implementation
        public Task Handle(UpdateIdeaCommand message)
        {
            var ideaTobeUpdated = _dataMapper.Map<Idea>(message.Idea);

            var existing = _repository.GetOne<Idea>(message.Idea.Id);


            existing.Title = ideaTobeUpdated.Title;
            existing.Description = ideaTobeUpdated.Description;
            existing.IsFundingRequired = ideaTobeUpdated.IsFundingRequired;
            existing.FundingRequirement = ideaTobeUpdated.FundingRequirement;

            existing.ModifiedAt = DateTime.UtcNow;
            existing.LastModifiedBy = _userIdentityProvider.GetUserId();

            var result = _repository.Update(existing);

            return _bus.RaiseEvent(new IdeaUpdatedEvent(_dataMapper.Map<IdeaDto>(result)));
        }
        #endregion

        #region DeleteIdeaCommand Implementation

        public Task Handle(DeleteIdeaCommand message)
        {
            var idea = _repository.GetOne<Idea>(message.IdeaId);

            if (idea == null)
            { return Task.CompletedTask; }

            _repository.Delete(idea);

            return _bus.RaiseEvent(new IdeaDeletedEvent(_dataMapper.Map<IdeaDto>(idea)));

        }

        #endregion

        #region UnpublishIdeaCommand Implementation
        public Task Handle(UnpublishIdeaCommand message)
        {
            var idea = _repository.GetOne<Idea>(message.IdeaId);

            if (idea == null)
            { return Task.CompletedTask; }

            idea.Status = Status.Unpublished.ToString();

            idea.ModifiedAt = DateTime.UtcNow;
            idea.LastModifiedBy = _userIdentityProvider.GetUserId();

            _repository.Update(idea);

            return _bus.RaiseEvent(new IdeaUnpublishedEvent(idea.Id));
        }
        #endregion

        #region PublishIdeaCommand
        public Task Handle(PublishIdeaCommand message)
        {
            var idea = _repository.GetOne<Idea>(message.IdeaId);

            if (idea == null)
            { return Task.CompletedTask; }

            idea.Status = Status.Published.ToString();
            idea.ModifiedAt = DateTime.UtcNow;
            idea.LastModifiedBy = _userIdentityProvider.GetUserId();

            _repository.Update(idea);

            return _bus.RaiseEvent(new IdeaPublishedEvent(idea.Id));
        }

      
        #endregion


    }
}
