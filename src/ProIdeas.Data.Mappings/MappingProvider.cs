﻿using AutoMapper;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Entities;
using ProIdeas.DTO;

namespace ProIdeas.Data.Mappings
{
    public class MappingProvider
    {
        public IDataMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<TenantSettings, TenantSettingsDto>();

                cfg.CreateMap<Idea, IdeaDto>().ReverseMap();
                cfg.CreateMap<Page, PageDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();
                cfg.CreateMap<UserEmail, UserEmailDto>().ReverseMap();                
                
                cfg.CreateMap<IdeaLike, IdeaLikeDto>();
                cfg.CreateMap<IdeaComment, IdeaCommentDto>();

            });

            config.CreateMapper();

            return new DefaultDataMapper(config.CreateMapper());
        }
    }
}