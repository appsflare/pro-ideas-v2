using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetTeamMemberByIdQueryTemplate : BaseRethinkQueryTemplate<TeamMember, GetTeamMemberByUserIdQuery>
    {
        async protected override Task<IEnumerable<TeamMember>> ExecuteAsync(QueryTemplateContext<GetTeamMemberByUserIdQuery> context)
        {
            var queryParam = context.Parameter;

            var table = RethinkDB.R
             .Table(nameof(Team));

            var query = table
             .Filter(x => x[nameof(Team.IdeaId)].Eq(queryParam.IdeaId))
             .ConcatMap(x=> x.GetField(nameof(Team.Members)))
             .Filter(y => y[nameof(TeamMember.MemberUserId)].Eq(queryParam.UserId));

           var finalQuery = query.Merge(m => RethinkDB.R.HashMap(nameof(TeamMember.Member),
                RethinkDB.R.Table("ApplicationUser")
                .Get(m.GetField(nameof(TeamMember.MemberUserId))).Pluck(nameof(User.FullName))));

            return (await finalQuery.RunCursorAsync<TeamMember>(context.Connection)).ToList();
        }
    }
}
