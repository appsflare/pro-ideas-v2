using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Commands.Ideas;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class IdeaPagesLogic : IIdeaPagesLogic,
         IHandler<SaveIdeaPagesCommand>
    {
        private readonly IRepository _repository;
        private readonly IDataMapper _dataMapper;
        private readonly IBus _bus;
        public IdeaPagesLogic(IRepository repostitory, IDataMapper dataMapper, IBus bus)
        {
            _repository = repostitory;
            _dataMapper = dataMapper;
            _bus = bus;

        }
        async public Task Handle(SaveIdeaPagesCommand message)
        {
            var idea = _repository.GetOne<Idea>(message.IdeaId);

            idea.Pages = new List<Page>(_dataMapper.Map<IEnumerable<Page>>(message.Pages));
            await _repository.UpdateAsync(idea);



            await _bus.RaiseEvent(new IdeaPagesUpdatedEvent(idea.Id, message.Pages));
        }
    }
}
