using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using ProIdeas.DTO;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Idea;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Events;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class IdeaLogic : IIdeaLogic,
        IHandler<CreateIdeaCommand>,
        IHandler<UpdateIdeaCommand>,
        IHandler<DeleteIdeaCommand>
    {
        private readonly IRepository _repository;
        private readonly IDataMapper _dataMapper;
        private readonly IBus _bus;

        public IdeaLogic(IRepository repository, IDataMapper dataMapper, IBus bus)
        {
            _repository = repository;
            _dataMapper = dataMapper;
            _bus = bus;
        }



        public Task<IdeaDto> GetIdea(string ideaId)
        {
            return Task.Factory.StartNew(() => _dataMapper.Map<IdeaDto>(_repository.GetOne<Idea>(ideaId)));
        }

        async public Task<IEnumerable<IdeaDto>> GetIdeas(int pageSize, int page, string keyword)
        {
            var result = await _repository.QueryAsync<Idea, FilterIdeaQueryTemplateParameter>(new FilterIdeaQueryTemplateParameter
            {
                Take = pageSize,
                Skip = Math.Max(page, 0) * pageSize,
                Keyword = keyword,
                Status = Status.Published.ToString(),
                OrderBy = "title"
            });

            return _dataMapper.Map<IEnumerable<IdeaDto>>(result);

            //return Task.Factory.StartNew(() =>
            //{
            //    var query = new QueryBuilder<Idea>()
            //    //.WithCondition(i => i.Status == Status.Published.ToString() || i.Title.Contains(keyword) || i.Description.Contains(keyword) || i.FundingRequirement.Contains(keyword))
            //    .WithCondition(i=> i.Title.Contains(keyword))
            //    .OrderBy(i => i.OrderBy(j => j.Title))
            //    .Skip(Math.Max(page, 0) * pageSize)
            //    .Take(pageSize)
            //    .Build();


            //    return _dataMapper.Map<IEnumerable<IdeaDto>>(_repository.Query(query));
            //});
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


        public void Handle(CreateIdeaCommand message)
        {
            if (!message.IsValid())
            { throw new ArgumentException("invalid command message", nameof(message)); }


            var newIdea = _dataMapper.Map<Idea>(message.Idea);
            newIdea.Status = Status.Draft.ToString();


            var result = _repository.Add(newIdea);

            var createdIdea = _dataMapper.Map<IdeaDto>(result);

            message.Idea.Id = createdIdea.Id;


            _bus.RaiseEvent(new IdeaCreatedEvent(_dataMapper.Map<IdeaDto>(result)));
        }

        public void Handle(UpdateIdeaCommand message)
        {
            if (!message.IsValid())
            { throw new ArgumentException("invalid command message", nameof(message)); }

            var ideaTobeUpdated = _dataMapper.Map<Idea>(message.Idea);

            var existing = _repository.GetOne<Idea>(message.Idea.Id);


            existing.Title = ideaTobeUpdated.Title;
            existing.Description = ideaTobeUpdated.Description;
            existing.IsFundingRequired = ideaTobeUpdated.IsFundingRequired;
            existing.FundingRequirement = ideaTobeUpdated.FundingRequirement;

            var result = _repository.Update(existing);

            _bus.RaiseEvent(new IdeaUpdatedEvent(_dataMapper.Map<IdeaDto>(result)));
        }

        public void Handle(DeleteIdeaCommand message)
        {
            if (!message.IsValid())
            { throw new ArgumentException("invalid command message", nameof(message)); }


            var idea = _repository.GetOne<Idea>(message.IdeaId);

            if (idea == null)
            { return; }

            _repository.Delete(idea);

            _bus.RaiseEvent(new IdeaDeletedEvent(_dataMapper.Map<IdeaDto>(idea)));

        }
    }
}
