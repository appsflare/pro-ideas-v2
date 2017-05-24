using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetIdeaByIdQueryTemplate : BaseRethinkQueryTemplate<Idea, GetIdeaByIdQuery>
    {
        protected override async Task<IEnumerable<Idea>> ExecuteAsync(QueryTemplateContext<GetIdeaByIdQuery> context)
        {
            var queryParam = context.Parameter;


            var table = RethinkDB.R
             .Table(nameof(Idea));

            //var filter =  RethinkDB.R.Js($@"(function(idea){{
            //      return idea.title.indexOf('{queryParam.Keyword}') >-1 ||idea.description.indexOf('{queryParam.Keyword}') > -1;
            //  }})");


            var query = table
             .Get(queryParam.IdeaId)
             .Merge(idea => RethinkDB.R.HashMap(nameof(Idea.Owner), RethinkDB.R
            .Table("ApplicationUser")
            .Get(idea.GetField(nameof(Idea.OwnerId)))
            .Pluck(nameof(User.FullName))));

            return new[] { await query.RunAtomAsync<Idea>(context.Connection) };
        }
    }
}
