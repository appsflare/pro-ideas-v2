using ProIdeas.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Idea;

namespace ProIdeas.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaLogic _ideaLogic;
        private readonly IBus _bus;
        public IdeaService(IIdeaLogic ideaLogic, IBus bus)
        {
            _ideaLogic = ideaLogic;
            _bus = bus;

        }

        public Task<IdeaDto> CreateAsync(IdeaDto idea)
        {
            var command = new CreateIdeaCommand(idea);

            _bus.SendCommand(command);

            return GetIdeaAsync(command.Idea.Id);
        }

        public void Update(IdeaDto idea)
        {
            var command = new UpdateIdeaCommand(idea);

            _bus.SendCommand(command);            
        }

        public Task<IdeaDto> GetIdeaAsync(string ideaId)
        {
            return _ideaLogic.GetIdea(ideaId);
        }

        public Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page)
        {
            return _ideaLogic.GetIdeas(pageSize, page, string.Empty);
        }
    }
}
