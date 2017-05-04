using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetSingleCommentByIdQueryTemplate : BaseRethinkQueryTemplate<IdeaComment, GetSingleCommentByIdQueryParameter>
    {
        async protected override Task<IEnumerable<IdeaComment>> ExecuteAsync(QueryTemplateContext<GetSingleCommentByIdQueryParameter> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(typeof(IdeaComment).Name);

            var query = table.Get(queryParam.CommentId)
                .Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
              .Table("ApplicationUser")
              .Get(idea.GetField(nameof(IdeaComment.OwnerId)))
              .Pluck(nameof(User.FullName))));

            return new[] { await query.RunAtomAsync<IdeaComment>(context.Connection) };
        }
    }
}
