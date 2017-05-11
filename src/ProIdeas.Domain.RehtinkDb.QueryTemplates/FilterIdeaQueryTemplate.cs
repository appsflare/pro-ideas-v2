using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class FilterIdeaQueryTemplate : BaseRethinkQueryTemplate<Idea, FilterIdeaQueryTemplateParameter>
    {
        async protected override Task<IEnumerable<Idea>> ExecuteAsync(QueryTemplateContext<FilterIdeaQueryTemplateParameter> context)
        {
            var queryParam = context.Parameter;



            var table = RethinkDB.R
             .Table(typeof(Idea).Name);
            

            var query = table.Filter(x=>x.HasFields(nameof(Idea.Title)));

            if (!string.IsNullOrEmpty(queryParam.Keyword))
            {
                var keyword = $"(?i){queryParam.Keyword}";

                query = query.Filter(x => x[nameof(Idea.Title)].Match(keyword)
                 .Or(x[nameof(Idea.Description)].Match(keyword)
                 .Or(x[nameof(Idea.FundingRequirement)].Match(keyword))));
            }


            if (!string.IsNullOrEmpty(queryParam.Status))
            {
                query = query.Filter(x => x[nameof(Idea.Status)].Eq(queryParam.Status));
            }

            if (!string.IsNullOrEmpty(queryParam.OwnerId))
            {
                query = query.Filter(x => x[nameof(Idea.OwnerId)].Eq(queryParam.OwnerId));
            }

            if (queryParam.Skip.HasValue)
            {
                query.Skip(queryParam.Skip.Value);
            }

            if (queryParam.Take.HasValue)
            {
                query.Limit(queryParam.Skip.Value);
            }

            var finalQuery = query.Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
            .Table("ApplicationUser")
            .Get(idea.GetField(nameof(Idea.OwnerId)))
            .Pluck(nameof(User.FullName))));

            var result = await finalQuery.RunCursorAsync<Idea>(context.Connection);

            return result.ToList();

        }
    }
}
