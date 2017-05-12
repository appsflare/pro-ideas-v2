using ProIdeas.DTO.Tasks;
using ProIdeas.Logic.Contracts;
using ProIdeas.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly ITaskBoardLogic _taskBoardLogic;
        public TaskBoardService(ITaskBoardLogic taskBoardLogic)
        {
             _taskBoardLogic = taskBoardLogic;
        }

        public Task<TaskBoardDto> GetTaskBoardAsync(string boardId)
        {
            return _taskBoardLogic.GetTaskBoardAsync(boardId);
        }

        public Task<TaskItemDto> GetTaskItemAsync(string taskItemId)
        {
            return _taskBoardLogic.GetTaskItemAsync(taskItemId);
        }

        public Task<IEnumerable<TaskItemDto>> GetTaskItemsAsync(string boardId, string taskItemStateId)
        {
            return _taskBoardLogic.GetTaskItemsAsync(boardId, taskItemStateId);
        }

        public Task<IEnumerable<TaskItemStateDto>> GetTaskItemStatesAsync(string boardId)
        {
            return _taskBoardLogic.GetTaskItemStatesAsync(boardId);
        }

        public Task<IEnumerable<TaskItemTypeDto>> GetTaskItemTypesAsync(string boardId)
        {
            return _taskBoardLogic.GetTaskItemTypesAsync(boardId);
        }
    }
}
