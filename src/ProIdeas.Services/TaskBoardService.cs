using ProIdeas.DTO.Tasks;
using ProIdeas.Logic.Contracts;
using ProIdeas.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Commands.Tasks;

namespace ProIdeas.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly ITaskBoardLogic _taskBoardLogic;
        private readonly IBus _bus;

        public TaskBoardService(ITaskBoardLogic taskBoardLogic, IBus bus)
        {
            _taskBoardLogic = taskBoardLogic;
            _bus = bus;
        }

        public async Task<TaskBoardDto> CreateBoardAsync(string ideaId, TaskBoardDto board)
        {
            await _bus.SendCommand(new CreateTaskBoardCommand(ideaId, board));

            return await _taskBoardLogic.GetTaskBoardAsync(board.Id);
        }

        public async Task<TaskItemDto> CreateTaskAsync(string boardId, TaskItemDto item)
        {
            await _bus.SendCommand(new AddTaskItemCommand(boardId, item));

            return await _taskBoardLogic.GetTaskItemAsync(item.Id);
        }

        public async Task<TaskItemStateDto> CreateTaskItemState(string boardId, TaskItemStateDto state)
        {
            await _bus.SendCommand(new AddTaskItemStateCommand(boardId, state));

            return await _taskBoardLogic.GetTaskItemStateAsync(state.Id);
        }

        public async Task<TaskItemTypeDto> CreateTaskItemType(string boardId, TaskItemTypeDto type)
        {
            await _bus.SendCommand(new AddTaskItemTypeCommand(boardId, type));

            return await _taskBoardLogic.GetTaskItemTypeAsync(type.Id);
        }

        public Task DeleteTaskAsync(string taskItemId)
        {
            return _bus.SendCommand(new DeleteTaskItemCommand(taskItemId));
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

        public Task UpdateBoardAsync(TaskBoardDto board)
        {
            return _bus.SendCommand(new UpdateTaskBoardCommand(board));
        }

        public Task UpdateTaskAsync(TaskItemDto item)
        {
            return _bus.SendCommand(new UpdateTaskItemCommand(item));
        }

        public Task UpdateTaskItemState(TaskItemStateDto state)
        {
            return _bus.SendCommand(new UpdateTaskItemStateCommand(state));
        }

        public Task UpdateTaskItemType(TaskItemTypeDto type)
        {
            return _bus.SendCommand(new UpdateTaskItemTypeCommand(type));
        }
       
    }
}
