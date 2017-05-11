﻿using ProIdeas.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Contracts
{
    public interface IActivityLogic
    {
        Task<IEnumerable<ActivityDto>> GetActivities(string userId);
    }
}
