using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetSingleCommentByIdQueryTemplate : BaseRethinkQueryTemplate<IdeaComment, GetSingleCommentByIdQueryTemplateParameter>
    {
        async protected override Task<IEnumerable<IdeaComment>> ExecuteAsync(QueryTemplateContext<GetSingleCommentByIdQueryTemplateParameter> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(typeof(IdeaComment).Name);

            var query = table.Get(queryParam.CommentId);
            return new[] { await query.RunAtomAsync<IdeaComment>(context.Connection) };
        }
    }
}
