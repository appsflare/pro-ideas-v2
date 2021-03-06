﻿using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class FilterIdeaQueryTemplate : BaseRethinkQueryTemplate<Idea, FilterIdeaQuery>
    {
        protected override async Task<IEnumerable<Idea>> ExecuteAsync(QueryTemplateContext<FilterIdeaQuery> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(nameof(Idea));
            

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

            var exlcudedProps = new Collection<string>(queryParam.PropertiesToExclude);
            var optimizedQuery = query.Without(exlcudedProps);

            if (queryParam.Skip.HasValue)
            {
                optimizedQuery.Skip(queryParam.Skip);
            }

            if (queryParam.Take.HasValue)
            {
                optimizedQuery.Limit(queryParam.Take);
            }

            var finalQuery = optimizedQuery.Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
            .Table("ApplicationUser")
            .Get(idea.GetField(nameof(Idea.OwnerId)))
            .Pluck(nameof(User.FullName))));

            var result = await finalQuery.RunCursorAsync<Idea>(context.Connection);

            return result.ToList();

        }
    }
}
