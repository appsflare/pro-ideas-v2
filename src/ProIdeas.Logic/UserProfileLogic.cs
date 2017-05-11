using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using ProIdeas.Domain.Entities;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class UserProfileLogic : IUserProfileLogic,
        IHandler<IdeaStatsChangedEvent>
    {
        private readonly IRepository _repository;
        public UserProfileLogic(IRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(IdeaStatsChangedEvent message)
        {
            var profile = _repository.GetOne<UserProfile>(message.Idea.OwnerId);

            if (profile == null)
            { return Task.CompletedTask; }

            

            return Task.CompletedTask;
        }
    }
}
