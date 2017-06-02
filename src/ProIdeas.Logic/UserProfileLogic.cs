using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Entities.Model;
using ProIdeas.Domain.Queries;
using ProIdeas.Domain.Repositories;
using ProIdeas.DTO;
using ProIdeas.Infra.Commands.Users;
using ProIdeas.Infra.Events;
using ProIdeas.Logic.Contracts;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class UserProfileLogic : IUserProfileLogic,
        IHandler<CreateUserProfileCommand>,
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

        private async Task<UserProfile> GetUserProfile(string userId)
        {
            return await _repository.QueryOneAsync<UserProfile, GetUserProfileByUserIdQuery>(new GetUserProfileByUserIdQuery
            {
                UserId = userId
            });
        }

        public async Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            var profile = await GetUserProfile(userId);

            return _dataMapper.Map<UserProfileDto>(profile);
        }

        public async Task Handle(IdeaStatsChangedEvent message)
        {
            var profile = await GetUserProfile(message.Idea.OwnerId);

            if (profile == null)
            { return; }


            var stats = await _repository.QueryOneAsync<UserProfileStats, GetUserProfileStatsQuery>(new GetUserProfileStatsQuery
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

        public async Task Handle(CreateUserProfileCommand message)
        {
            var userProfile = await GetUserProfile(message.UserId);

            if (userProfile != null)
            {
                return;
            }

            await _repository.AddAsync(new UserProfile
            {
                OwnerId = message.UserId
            });
        }

    }
}
