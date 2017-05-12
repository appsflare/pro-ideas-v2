﻿using ProIdeas.DTO.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services.Contracts
{
    public interface ITaskBoardService
    {
        Task<TaskBoardDto> GetTaskBoardAsync(string boardId);

        Task<IEnumerable<TaskItemTypeDto>> GetTaskItemTypesAsync(string boardId);

        Task<IEnumerable<TaskItemStateDto>> GetTaskItemStatesAsync(string boardId);

        Task<TaskItemDto> GetTaskItemAsync(string taskItemId);

        Task<IEnumerable<TaskItemDto>> GetTaskItemsAsync(string boardId, string taskItemStateId);
    }
}
