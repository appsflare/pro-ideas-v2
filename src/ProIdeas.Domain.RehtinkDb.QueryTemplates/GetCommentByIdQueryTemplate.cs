using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetCommentByIdQueryTemplate : BaseRethinkQueryTemplate<IdeaComment, GetCommentByIdQuery>
    {
        async protected override Task<IEnumerable<IdeaComment>> ExecuteAsync(QueryTemplateContext<GetCommentByIdQuery> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(nameof(IdeaComment));

            var query = table.Get(queryParam.CommentId)
                .Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
              .Table("ApplicationUser")
              .Get(idea.GetField(nameof(IdeaComment.OwnerId)))
              .Pluck(nameof(User.FullName))));

            return new[] { await query.RunAtomAsync<IdeaComment>(context.Connection) };
        }
    }
}
