using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.DTO.Tasks;
using ProIdeas.Services.Contracts;

namespace ProIdeas.UI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class TaskBoardApiController : Controller
    {
        private readonly ITaskBoardService _taskBoardService;
        public TaskBoardApiController(ITaskBoardService taskBoardService)
        {
            _taskBoardService = taskBoardService;
        }

        [HttpGet, Route("tasks/{taskItemId}")]
        public Task<TaskItemDto> GetTaskItem(string taskItemId)
        {
            return _taskBoardService.GetTaskItemAsync(taskItemId);
        }

        [HttpGet, Route("boards/{boardId}/tasks")]
        public Task<IEnumerable<TaskItemDto>> GetTaskItems(string boardId, string taskItemStateId)
        {
            return _taskBoardService.GetTaskItemsAsync(boardId, taskItemStateId);
        }

        [HttpGet, Route("boards/{boardId}/states")]
        public Task<IEnumerable<TaskItemStateDto>> GetTaskItems(string boardId)
        {
            return _taskBoardService.GetTaskItemStatesAsync(boardId);
        }

        [HttpGet, Route("boards/{boardId}/types")]
        public Task<IEnumerable<TaskItemTypeDto>> GetTaskType(string boardId)
        {
            return _taskBoardService.GetTaskItemTypesAsync(boardId);
        }

    }
}