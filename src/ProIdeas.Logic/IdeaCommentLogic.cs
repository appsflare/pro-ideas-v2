using ProIdeas.Logic.Contracts;
using System;
using System.Collections.Generic;
using ProIdeas.DTO;
using ProIdeas.Domain.Repositories;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using ProIdeas.DataMappings.Data.Mappings.Contracts;
using ProIdeas.Domain.Core.Events;
using ProIdeas.Infra.Commands.Ideas;
using ProIdeas.Domain.Core.Bus;
using ProIdeas.Infra.Events;
using System.Threading.Tasks;
using ProIdeas.Authentication.Contracts;
using ProIdeas.Infra.Commands.Comments;

namespace ProIdeas.Logic
{
    public class IdeaCommentLogic : IIdeaCommentLogic,
        IHandler<CreateIdeaCommentCommand>,
        IHandler<UpdateIdeaCommentCommand>,
        IHandler<DeleteIdeaCommentCommand>
    {
        #region Private readonly fields
        private readonly IRepository _repository;
        private readonly IDataMapper _dataMapper;
        private readonly IBus _bus;
        private readonly IUserIdentityProvider _userIdentityProvider;
        #endregion

        #region Ctors
        public IdeaCommentLogic(IRepository repository, IDataMapper dataMapper, IBus bus, IUserIdentityProvider userIdentityProvider)
        {
            _repository = repository;
            _dataMapper = dataMapper;
            _bus = bus;
            _userIdentityProvider = userIdentityProvider;
        }
        #endregion

        #region IIdeaCommentLogic Implementation
        async public Task<IdeaCommentDto> GetComment(string commentId)
        {
            var comment = await _repository.QueryOneAsync<IdeaComment, GetSingleCommentByIdQueryTemplateParameter>(new GetSingleCommentByIdQueryTemplateParameter
            {
                CommentId = commentId
            });
            return _dataMapper.Map<IdeaCommentDto>(comment);
        }

        async public Task<IEnumerable<IdeaCommentDto>> GetComments(string ideaId)
        {
            var comments = await _repository.QueryAsync<IdeaComment, GetIdeaCommentsByIdeaIdQueryTemplateParameter>(new GetIdeaCommentsByIdeaIdQueryTemplateParameter
            {
                IdeaId = ideaId
            });
            return _dataMapper.Map<IEnumerable<IdeaCommentDto>>(comments);
        }
        #endregion

        #region CreateIdeaCommentCommand Implementation
        public void Handle(CreateIdeaCommentCommand message)
        {
            var createdComment = _repository.Add(_dataMapper.Map<IdeaComment>(message.Comment));

            message.SetCommentId(createdComment.Id);

           _bus.RaiseEvent(new IdeaCommentCreatedEvent( _dataMapper.Map<IdeaCommentDto>(createdComment)));


        }
        #endregion

        #region UpdateIdeaCommentCommand Implementation
        public void Handle(UpdateIdeaCommentCommand message)
        {
            var updatedComment = _repository.Update(_dataMapper.Map<IdeaComment>(message.Comment));

            _bus.RaiseEvent(new IdeaCommentUpdatedEvent(_dataMapper.Map<IdeaCommentDto>(updatedComment)));
        }
        #endregion

        #region DeleteIdeaCommentCommand Implementation
        public void Handle(DeleteIdeaCommentCommand message)
        {
            var ideaComment = _repository.GetOne<IdeaComment>(message.CommentId);
            _repository.Delete(ideaComment);
            _bus.RaiseEvent(new IdeaCommentDeletedEvent(_dataMapper.Map<IdeaCommentDto>(ideaComment)));
        }
        #endregion



    }
}
