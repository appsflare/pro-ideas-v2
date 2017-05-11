using ProIdeas.Services.Contracts;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Ideas;

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

        public Task UpdateAsync(IdeaDto idea)
        {
            var command = new UpdateIdeaCommand(idea);

            return _bus.SendCommand(command);
        }

        public Task<IdeaDto> GetIdeaAsync(string ideaId)
        {
            return _ideaLogic.GetIdea(ideaId);
        }

        public Task<IEnumerable<IdeaDto>> GetIdeasAsync(int pageSize, int page)
        {
            return _ideaLogic.GetIdeas(pageSize, page, string.Empty);
        }

        public Task<IEnumerable<IdeaDto>> SearchIdeasAsync(string keyword, int pageSize, int page)
        {
            return _ideaLogic.GetIdeas(pageSize, page, keyword);
        }

        public Task SavePagesAsync(string ideaId, IEnumerable<PageDto> pages)
        {
            return _bus.SendCommand(new SaveIdeaPagesCommand(ideaId, pages));
        }

        public Task PublishAsync(string ideaId)
        {
            return _bus.SendCommand(new PublishIdeaCommand(ideaId));
        }

        public Task UnpublishAsync(string ideaId)
        {
            return _bus.SendCommand(new UnpublishIdeaCommand(ideaId));
        }

        public Task<IEnumerable<IdeaDto>> GetUserIdeasAsync(string userId, int pageSize, int page, string keyword)
        {
            return _ideaLogic.GetUserIdeas(userId, pageSize, page, keyword);
        }
    }
}
