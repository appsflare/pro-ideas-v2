using AutoMapper;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Entities.Model;
using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.DTO;
using ProIdeas.DTO.Tasks;

namespace ProIdeas.Data.Mappings
{
    public class MappingProvider
    {
        public IDataMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<TenantSettings, TenantSettingsDto>();
                cfg.CreateMap<IdeaCollaborationStats, IdeaCollaborationStatsDto>();
                cfg.CreateMap<Idea, IdeaDto>().ReverseMap();
                cfg.CreateMap<Page, PageDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();                
                
                cfg.CreateMap<IdeaLike, IdeaLikeDto>();
                cfg.CreateMap<IdeaComment, IdeaCommentDto>().ReverseMap();
                cfg.CreateMap<UserProfile, UserProfileDto>();
                cfg.CreateMap<UserProfileStats, UserProfileStatsDto>();

                cfg.CreateMap<Activity, ActivityDto>();
                cfg.CreateMap<ActivityItemDetails, ActivityItemDetailsDto>();

                cfg.CreateMap<Team, TeamDto>().ReverseMap();
                cfg.CreateMap<TeamMember, TeamMemberDto>().ReverseMap();

                cfg.CreateMap<TaskItem, TaskItemDto>().ReverseMap();
                cfg.CreateMap<TaskItemType, TaskItemTypeDto>().ReverseMap();
                cfg.CreateMap<TaskItemState, TaskItemStateDto>().ReverseMap();


            });

            config.CreateMapper();

            return new DefaultDataMapper(config.CreateMapper());
        }
    }
}
