﻿using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.UI.Models.IdeaViewModels
{

    public class IdeaPageInfo
    {
        public IdeaPageInfo()
        {
            Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public string Id { get; private set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }

    public class IdeaOwnerInfo
    {
        private IdeaOwnerInfo()
        {
        }

        public string FullName { get; set; }

        public static IdeaOwnerInfo CreateFrom(UserDto user)
        {
            return new IdeaOwnerInfo
            {
                FullName = user?.FullName
            };

        }
    }

    public class IdeaInfoViewModel
    {
        private IdeaInfoViewModel()
        {

        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public bool IsFundingRequired { get; set; }

        public string FundingRequirement { get; set; }

        public IEnumerable<IdeaPageInfo> Pages { get; set; }

        public string OwnerId { get; set; }

        public bool IsOwner(string userId)
        {
            return OwnerId == userId;
        }

        public IdeaOwnerInfo Owner { get; set; }

        public bool IsPublished => Status == "Published";

        public int Likes { get; set; }

        public int DisLikes { get; set; }

        public int Comments { get; set; }

        private static IdeaInfoViewModel GetIdeaInfoViewModel(IdeaDto idea)
        {
            return new IdeaInfoViewModel
            {
                Id = idea.Id,
                Title = idea.Title,
                Description = idea.Description,
                IsFundingRequired = idea.IsFundingRequired,
                FundingRequirement = idea.FundingRequirement,
                OwnerId = idea.OwnerId,
                Status = idea.Status,
                Owner = IdeaOwnerInfo.CreateFrom(idea.Owner),
                Likes = idea.Likes,
                DisLikes = idea.DisLikes,
                Comments = idea.Comments,
                Pages = idea.Pages.Select(i => new IdeaPageInfo
                {
                    Name = i.Name,
                    Content = i.Content,                    
                }).ToList()
            };
        }


        public static IdeaInfoViewModel MapFrom(IdeaDto idea)
        {
            return GetIdeaInfoViewModel(idea);
        }


        public static IdeaInfoViewModel CreateEmpty()
        {
            return new IdeaInfoViewModel();
        }



    }
}
