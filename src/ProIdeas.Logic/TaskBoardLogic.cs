using ProIdeas.Authentication.Contracts;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.Domain.Queries.Tasks;
using ProIdeas.Domain.Repositories;
using ProIdeas.DTO.Tasks;
using ProIdeas.Exceptions;
using ProIdeas.Infra.Commands.Tasks;
using ProIdeas.Infra.Events.Tasks;
using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Logic
{
    public class TaskBoardLogic : ITaskBoardLogic,
        IHandler<AddTaskItemCommand>,
        IHandler<UpdateTaskItemCommand>,
        IHandler<DeleteTaskItemCommand>,
        IHandler<AddTaskItemStateCommand>,
        IHandler<UpdateTaskItemStateCommand>,
        IHandler<AddTaskItemTypeCommand>,
        IHandler<UpdateTaskItemTypeCommand>
    {

        #region Private Readonly fields
        private readonly IRepository _repository;
        private readonly IBus _bus;
        private readonly IDataMapper _dataMapper;
        private readonly IUserIdentityProvider _userIdentityProvider;
        #endregion

        #region Ctors
        public TaskBoardLogic(IRepository repository, IBus bus, IDataMapper dataMapper, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _bus = bus;
            _dataMapper = dataMapper;
            _userIdentityProvider = userIdentityProvider;
        }
        #endregion

        #region ITaskBoardLogic Implementation
        public async Task<TaskBoardDto> GetTaskBoardAsync(string boardId)
        {
            var taskBoard = await _repository.GetOneAsync<TaskBoard>(boardId);

            if (taskBoard == null)
            {
                throw new LogicalException(ErrorCategory.NotFound);
            }

            return _dataMapper.Map<TaskBoardDto>(taskBoard);
        }

        public async Task<TaskItemDto> GetTaskItemAsync(string taskItemId)
        {
            var taskItem = await _repository.GetOneAsync<TaskItem>(taskItemId);

            if (taskItem == null)
            {
                throw new LogicalException(ErrorCategory.NotFound);
            }

            return _dataMapper.Map<TaskItemDto>(taskItem);
        }

        public async Task<IEnumerable<TaskItemDto>> GetTaskItemsAsync(string boardId, string taskItemStateId)
        {
            var tasks = await _repository.QueryAsync<TaskItem, GetTaskBoardTasksByStateId>(new GetTaskBoardTasksByStateId
            {
                TaskBoardId = boardId,
                StateId = taskItemStateId
            });

            return _dataMapper.Map<IEnumerable<TaskItemDto>>(tasks);
        }

        public async Task<TaskItemStateDto> GetTaskItemStateAsync(string stateId)
        {
            var taskState = await _repository.GetOneAsync<TaskItemState>(stateId);

            return _dataMapper.Map<TaskItemStateDto>(taskState);
        }

        public async Task<IEnumerable<TaskItemStateDto>> GetTaskItemStatesAsync(string boardId)
        {
            var tasks = await _repository.QueryAsync<TaskItem, GetTaskItemStatesByBoardId>(new GetTaskItemStatesByBoardId
            {
                TaskBoardId = boardId
            });

            return _dataMapper.Map<IEnumerable<TaskItemStateDto>>(tasks);
        }

        public async Task<TaskItemTypeDto> GetTaskItemTypeAsync(string typeId)
        {

            var taskType = await _repository.GetOneAsync<TaskItemType>(typeId);

            return _dataMapper.Map<TaskItemTypeDto>(taskType);
        }

        public async Task<IEnumerable<TaskItemTypeDto>> GetTaskItemTypesAsync(string boardId)
        {
            var tasks = await _repository.QueryAsync<TaskItem, GetTaskItemTypesByBoardId>(new GetTaskItemTypesByBoardId
            {
                TaskBoardId = boardId
            });

            return _dataMapper.Map<IEnumerable<TaskItemTypeDto>>(tasks);
        }
        #endregion

        #region AddTaskItemCommand Implementation
        public async Task Handle(AddTaskItemCommand message)
        {
            var task = _dataMapper.Map<TaskItem>(message.Item);

            task.CreatedAt = DateTime.UtcNow;
            task.CreatedBy = _userIdentityProvider.GetUserId();


            var addedTask = await _repository.AddAsync(task);

            message.SetTaskItemId(task.Id);

            await _bus.RaiseEvent(new TaskItemAddedEvent(_dataMapper.Map<TaskItemDto>(addedTask)));
        }
        #endregion

        #region UpdateTaskItemCommand Implementation
        public async Task Handle(UpdateTaskItemCommand message)
        {
            var task = _dataMapper.Map<TaskItem>(message.Item);

            var existingTask = await _repository.GetOneAsync<TaskItem>(task.Id);

            existingTask.Content = task.Content;
            existingTask.UpdatedAt = DateTime.UtcNow;
            existingTask.LastModifiedBy = _userIdentityProvider.GetUserId();

            var updatedTask = await _repository.UpdateAsync(existingTask);

            await _bus.RaiseEvent(new TaskItemUpdatedEvent(_dataMapper.Map<TaskItemDto>(updatedTask)));
        }
        #endregion

        #region DeleteTaskItemCommand Implementation
        public async Task Handle(DeleteTaskItemCommand message)
        {
            var existingTask = await _repository.GetOneAsync<TaskItem>(message.TaskItemId);

            await _repository.DeleteAsync(existingTask);

            await _bus.RaiseEvent(new TaskItemDeletedEvent(_dataMapper.Map<TaskItemDto>(existingTask)));

        }
        #endregion

        #region AddTaskItemStateCommand Implementation
        public async Task Handle(AddTaskItemStateCommand message)
        {
            var taskItemState = _dataMapper.Map<TaskItemState>(message.State);

            taskItemState.TaskBoardId = message.BoardId;
            taskItemState.CreatedAt = DateTime.UtcNow;
            taskItemState.CreatedBy = _userIdentityProvider.GetUserId();

            var newState = await _repository.AddAsync(taskItemState);

            message.SetStateId(newState.Id);

            await _bus.RaiseEvent(new TaskItemStateAddedEvent(_dataMapper.Map<TaskItemStateDto>(newState)));
        }
        #endregion

        #region UpdateTaskItemStateCommand Implementation
        public async Task Handle(UpdateTaskItemStateCommand message)
        {
            var state = _dataMapper.Map<TaskItemState>(message.State);

            var existingState = await _repository.GetOneAsync<TaskItemState>(state.Id);

            existingState.Name = state.Name;
            existingState.Description = state.Description;

            existingState.ModifiedAt = DateTime.UtcNow;
            existingState.LastModifiedBy = _userIdentityProvider.GetUserId();

            var updatedTask = await _repository.UpdateAsync(existingState);

            await _bus.RaiseEvent(new TaskItemStateUpdatedEvent(_dataMapper.Map<TaskItemStateDto>(updatedTask)));
        }
        #endregion

        #region AddTaskItemTypeCommand Implementation
        public async Task Handle(AddTaskItemTypeCommand message)
        {
            var taskItemType = _dataMapper.Map<TaskItemType>(message.Type);

            taskItemType.TaskBoardId = message.BoardId;
            taskItemType.CreatedAt = DateTime.UtcNow;
            taskItemType.CreatedBy = _userIdentityProvider.GetUserId();

            var newType = await _repository.AddAsync(taskItemType);

            message.SetStateId(newType.Id);

            await _bus.RaiseEvent(new TaskItemTypeAddedEvent(_dataMapper.Map<TaskItemTypeDto>(newType)));
        }
        #endregion

        #region UpdateTaskItemTypeCommand Implementation
        public async Task Handle(UpdateTaskItemTypeCommand message)
        {
            var type = _dataMapper.Map<TaskItemType>(message.Type);

            var existingType = await _repository.GetOneAsync<TaskItemType>(type.Id);

            existingType.Name = type.Name;
            existingType.Description = type.Description;

            existingType.ModifiedAt = DateTime.UtcNow;
            existingType.LastModifiedBy = _userIdentityProvider.GetUserId();

            var updatedTaskType = await _repository.UpdateAsync(existingType);

            await _bus.RaiseEvent(new TaskItemTypeUpdatedEvent(_dataMapper.Map<TaskItemTypeDto>(updatedTaskType)));
        }
        #endregion
    }
}
