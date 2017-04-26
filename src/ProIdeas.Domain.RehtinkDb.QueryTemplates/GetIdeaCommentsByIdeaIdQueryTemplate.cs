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

            var query = table.Filter(comment => comment[nameof(IdeaComment.IdeaId)] == queryParam.IdeaId);
            return (await query.RunCursorAsync<IdeaComment>(context.Connection)).ToList();
        }
    }
}
