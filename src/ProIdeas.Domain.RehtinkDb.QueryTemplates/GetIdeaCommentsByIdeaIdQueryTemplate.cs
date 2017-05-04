using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetIdeaCommentsByIdeaIdQueryTemplate : BaseRethinkQueryTemplate<IdeaComment, GetIdeaCommentsByIdeaIdQueryTemplateParameter>
    {
        async protected override Task<IEnumerable<IdeaComment>> ExecuteAsync(QueryTemplateContext<GetIdeaCommentsByIdeaIdQueryTemplateParameter> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(typeof(IdeaComment).Name);

            var query = table
                .OrderBy()
                .OptArg("index", RethinkDB.R.Desc(nameof(IdeaComment.CreatedOn)))
                .Filter(comment => comment.GetField(nameof(IdeaComment.IdeaId)).Eq(queryParam.IdeaId));

            var finalQuery = query.Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
              .Table("ApplicationUser")
              .Get(idea.GetField(nameof(IdeaComment.OwnerId)))
              .Pluck(nameof(User.FullName))));

            return (await finalQuery.RunCursorAsync<IdeaComment>(context.Connection)).ToList();
        }
    }
}
