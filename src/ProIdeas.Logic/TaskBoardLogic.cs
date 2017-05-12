using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ProIdeas.DTO.Tasks;
using System.Threading.Tasks;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Authentication.Contracts;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.Exceptions;

namespace ProIdeas.Logic
{
    public class TaskBoardLogic : ITaskBoardLogic
    {

        private readonly IRepository _repository;
        private readonly IBus _bus;
        private readonly IDataMapper _dataMapper;
        private readonly IUserIdentityProvider _userIdentityProvider;

        public TaskBoardLogic(IRepository repository, IBus bus, IDataMapper dataMapper, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _bus = bus;
            _dataMapper = dataMapper;
            _userIdentityProvider = userIdentityProvider;
        }

        async public Task<TaskBoardDto> GetTaskBoardAsync(string boardId)
        {
            var taskBoard = await _repository.GetOneAsync<TaskBoard>(boardId);

            if (taskBoard == null)
            {
                throw new LogicalException(ErrorCategory.NotFound);
            }

            return _dataMapper.Map<TaskBoardDto>(taskBoard);
        }

        async public Task<TaskItemDto> GetTaskItemAsync(string taskItemId)
        {
            var taskItem = await _repository.GetOneAsync<TaskItem>(taskItemId);

            if (taskItem == null)
            {
                throw new LogicalException(ErrorCategory.NotFound);
            }

            return _dataMapper.Map<TaskItemDto>(taskItem);
        }

        public Task<IEnumerable<TaskItemDto>> GetTaskItemsAsync(string boardId, string taskItemStateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItemStateDto>> GetTaskItemStatesAsync(string boardId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItemTypeDto>> GetTaskItemTypesAsync(string boardId)
        {
            throw new NotImplementedException();
        }
    }
}
