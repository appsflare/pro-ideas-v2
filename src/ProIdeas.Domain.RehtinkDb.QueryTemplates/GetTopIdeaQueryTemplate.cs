using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetTopIdeaQueryTemplate : BaseRethinkQueryTemplate<Idea, GetTopIdeaQuery>
    {
        protected override async Task<IEnumerable<Idea>> ExecuteAsync(QueryTemplateContext<GetTopIdeaQuery> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(nameof(Idea));

            var query = table
                .OrderBy()
                .OptArg("index", RethinkDB.R.Desc(nameof(Idea.Comments)))
                .Limit(queryParam.Count)
                .Filter(x => x.GetField(nameof(Idea.Status)).Eq(Status.Published.ToString()))
                .Filter(x => x.HasFields(nameof(Idea.Title)));

            var finalQuery = query.Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
            .Table("ApplicationUser")
            .Get(idea.GetField(nameof(Idea.OwnerId)))
            .Pluck(nameof(User.FullName))));

            var result = await finalQuery.RunCursorAsync<Idea>(context.Connection);

            return result.ToList();

        }
    }
}
