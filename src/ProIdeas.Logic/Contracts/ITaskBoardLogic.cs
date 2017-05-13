using ProIdeas.DTO.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Logic.Contracts
{
    public interface ITaskBoardLogic
    {
        Task<TaskBoardDto> GetTaskBoardAsync(string boardId);

        Task<IEnumerable<TaskItemTypeDto>> GetTaskItemTypesAsync(string boardId);

        Task<IEnumerable<TaskItemStateDto>> GetTaskItemStatesAsync(string boardId);

        Task<TaskItemStateDto> GetTaskItemStateAsync(string stateId);

        Task<TaskItemTypeDto> GetTaskItemTypeAsync(string typeId);

        Task<TaskItemDto> GetTaskItemAsync(string taskItemId);

        Task<IEnumerable<TaskItemDto>> GetTaskItemsAsync(string boardId, string taskItemStateId);
    }
}
