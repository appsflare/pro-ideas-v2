using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Repositories;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using ProIdeas.Domain.Entities;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class UserProfileLogic : IUserProfileLogic,
        IHandler<IdeaLikeChangedEvent>
    {
        private readonly IRepository _repository;
        public UserProfileLogic(IRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(IdeaLikeChangedEvent message)
        {
            var profile = _repository.GetOne<UserProfile>(message.UserId);

            return Task.CompletedTask;
        }
    }
}
