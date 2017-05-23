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
        #region Private Readonly Fields
        private readonly ITaskBoardService _taskBoardService; 
        #endregion

        #region Ctors
        public TaskBoardApiController(ITaskBoardService taskBoardService)
        {
            _taskBoardService = taskBoardService;
        } 
        #endregion

        #region Task Board Endpoints
        [HttpPost, Route("ideas/{ideaId}/board")]
        public Task<TaskBoardDto> CreateTaskBoard(string ideaId, TaskBoardDto board)
        {
            return _taskBoardService.CreateBoardAsync(ideaId, board);
        }

        [HttpPut, Route("boards/{boardId}")]
        public async Task<IActionResult> UpdateTaskBoard(TaskBoardDto board)
        {
            await _taskBoardService.UpdateBoardAsync(board);
            return Json(new { message = "Task board successfully updated" });
        } 
        #endregion

        #region Task Item States Endpoints
        [HttpPost, Route("boards/{boardId}/states")]
        public Task<TaskItemStateDto> CreateTaskItemState(string boardId, TaskItemStateDto state)
        {
            return _taskBoardService.CreateTaskItemState(boardId, state);
        }

        [HttpPut, Route("boards/{boardId}/states")]
        public async Task<IActionResult> UpdateTaskItemState(string boardId, TaskItemStateDto state)
        {
            state.TaskBoardId = boardId;
            await _taskBoardService.UpdateTaskItemState(state);
            return Json(new { message = "Task item state successfully updated" });
        }

        [HttpGet, Route("boards/{boardId}/states")]
        public Task<IEnumerable<TaskItemStateDto>> GetTaskItems(string boardId)
        {
            return _taskBoardService.GetTaskItemStatesAsync(boardId);
        }
        #endregion

        #region Task Board Tasks Endpoints

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

        [HttpPost, Route("boards/{boardId}/tasks")]
        public Task<TaskItemDto> CreateTaskItem(string boardId, TaskItemDto item)
        {
            return _taskBoardService.CreateTaskAsync(boardId, item);
        }

        [HttpPut, Route("boards/{boardId}/tasks")]
        public async Task<IActionResult> UpdateTaskItem(string boardId, TaskItemDto item)
        {
            item.TaskBoardId = boardId;
            await _taskBoardService.UpdateTaskAsync(item);

            return Json(new { message = "Task Updated successfully" });
            
        }

        [HttpDelete, Route("tasks/{taskItemId}")]
        public async Task<IActionResult> UpdateTaskItem(string taskItemId)
        {
            
            await _taskBoardService.DeleteTaskAsync(taskItemId);

            return Json(new { message = "Task Deleted successfully" });
        }
        #endregion

        #region Task Item Types Endpoints

        [HttpPost, Route("boards/{boardId}/types")]
        public Task<TaskItemTypeDto> CreateTaskItemType(string boardId, TaskItemTypeDto type)
        {
            return _taskBoardService.CreateTaskItemType(boardId, type);
        }

        [HttpPut, Route("boards/{boardId}/types")]
        public async Task<IActionResult> UpdateTaskItemType(string boardId, TaskItemTypeDto type)
        {
            type.TaskBoardId = boardId;
            await _taskBoardService.UpdateTaskItemType(type);
            return Json(new { message = "Task item type successfully updated" });
        }

        [HttpGet, Route("boards/{boardId}/types")]
        public Task<IEnumerable<TaskItemTypeDto>> GetTaskType(string boardId)
        {
            return _taskBoardService.GetTaskItemTypesAsync(boardId);
        } 
        #endregion

    }
}