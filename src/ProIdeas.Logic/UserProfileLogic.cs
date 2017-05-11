using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Entities.Model;
using ProIdeas.Domain.Queries;
using ProIdeas.Domain.Repositories;
using ProIdeas.DTO;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class UserProfileLogic : IUserProfileLogic,
        IHandler<IdeaStatsChangedEvent>
    {
        private readonly IRepository _repository;
        private readonly IBus _bus;
        private readonly IDataMapper _dataMapper;
        public UserProfileLogic(IRepository repository, IBus bus, IDataMapper dataMapper)
        {
            _repository = repository;
            _bus = bus;
            _dataMapper = dataMapper;
        }

        async public Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            var profile = await _repository.QueryOneAsync<UserProfile, GetUserProfileByUserIdQueryParameter>(new GetUserProfileByUserIdQueryParameter
            {
                UserId = userId
            });

            return _dataMapper.Map<UserProfileDto>(profile);
        }

        async public Task Handle(IdeaStatsChangedEvent message)
        {
            var profile = await _repository.GetOneAsync<UserProfile>(message.Idea.OwnerId);

            if (profile == null)
            { return; }


            var stats = await _repository.QueryOneAsync<UserProfileStats, GetUserProfileStatsQueryParameter>(new GetUserProfileStatsQueryParameter
            {
                UserId = message.Idea.OwnerId
            });

            profile.LikesCount = stats.Likes;
            profile.DisLikesCount = stats.DisLikes;
            profile.CommentsCount = stats.Comments;
            profile.IdeasCount = stats.Ideas;

            _repository.Update(profile);

            await _bus.RaiseEvent(new UserProfileStatsChangedEvent(_dataMapper.Map<UserProfileStatsDto>(stats)));

        }
    }
}
