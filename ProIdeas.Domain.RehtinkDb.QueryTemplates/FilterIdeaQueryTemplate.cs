﻿using ProIdeas.Domain.Entities;
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

            //var filter =  RethinkDB.R.Js($@"(function(idea){{
            //      return idea.title.indexOf('{queryParam.Keyword}') >-1 ||idea.description.indexOf('{queryParam.Keyword}') > -1;
            //  }})");



            var query = table
             .Filter(x => x[nameof(Idea.Title)].Match(queryParam.Keyword)
             .Or(x[nameof(Idea.Description)].Match(queryParam.Keyword)
             .Or(x[nameof(Idea.FundingRequirement)].Match(queryParam.Keyword))));

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

            var result = await query.RunCursorAsync<Idea>(context.Connection);

            return result.ToList();

        }
    }
}
