using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Ideas;
using ProIdeas.Logic.Contracts;
using System.Collections.Generic;

namespace ProIdeas.Logic
{
    public class IdeaPagesLogic : IIdeaPagesLogic,
         IHandler<SaveIdeaPagesCommand>
    {
        private readonly IRepository _repository;
        private readonly IDataMapper _dataMapper;
        public IdeaPagesLogic(IRepository repostitory, IDataMapper dataMapper)
        {
            _repository = repostitory;
            _dataMapper = dataMapper;

        }
        public void Handle(SaveIdeaPagesCommand message)
        {
            var idea = _repository.GetOne<Idea>(message.IdeaId);

            idea.Pages = new List<Page>(_dataMapper.Map<IEnumerable<Page>>(message.Pages));
            _repository.Update(idea);
        }
    }
}
