using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetIdeaLikeByUserIdQueryTemplate : BaseRethinkQueryTemplate<IdeaLike, GetIdeaLikeByUserIdQueryParameter>
    {
        async protected override Task<IEnumerable<IdeaLike>> ExecuteAsync(QueryTemplateContext<GetIdeaLikeByUserIdQueryParameter> context)
        {
            var queryParam = context.Parameter;


            var table = RethinkDB.R
             .Table(typeof(IdeaLike).Name);


            var query = table
             .Filter(x => x[nameof(IdeaLike.OwnerId)] == queryParam.OwnerId)
             .Limit(1)
             .Merge(idea => RethinkDB.R.HashMap(nameof(IdeaLike.Owner), RethinkDB.R
            .Table("ApplicationUser")
            .Get(idea.GetField(nameof(IdeaLike.OwnerId)))
            .Pluck(nameof(User.FullName))));

            return new[] {
                (await query.RunCursorAsync<IdeaLike>(context.Connection)).FirstOrDefault()
            };
        }
    }
}
