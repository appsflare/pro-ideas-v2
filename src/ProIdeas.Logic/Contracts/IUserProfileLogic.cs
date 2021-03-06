﻿using ProIdeas.DTO;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Contracts
{
    public interface IUserProfileLogic
    {
        Task<UserProfileDto> GetUserProfileAsync(string userId);
    }
}
