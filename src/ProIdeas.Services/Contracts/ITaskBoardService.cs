using ProIdeas.DTO.Tasks;
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

        Task<TaskBoardDto> CreateBoardAsync(string ideaId, TaskBoardDto board);

        Task UpdateBoardAsync(TaskBoardDto board);


        Task<TaskItemDto> CreateTaskAsync(string boardId, TaskItemDto item);

        Task UpdateTaskAsync(TaskItemDto item);

        Task DeleteTaskAsync(string taskItemId);


        Task<TaskItemStateDto> CreateTaskItemState(string boardId, TaskItemStateDto state);

        Task UpdateTaskItemState(TaskItemStateDto state);

        Task<TaskItemTypeDto> CreateTaskItemType(string boardId, TaskItemTypeDto type);

        Task UpdateTaskItemType(TaskItemTypeDto type);
    }
}
