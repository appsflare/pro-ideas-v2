using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetTeamByUserIdQueryTemplate : BaseRethinkQueryTemplate<Team, GetTeamByIdeaIdQuery>
    {
        async protected override Task<IEnumerable<Team>> ExecuteAsync(QueryTemplateContext<GetTeamByIdeaIdQuery> context)
        {
            var queryParam = context.Parameter;

            var teamTable = RethinkDB.R.Table(nameof(Team));
            var userTable = RethinkDB.R.Table("ApplicationUser");

            var filterTeams = teamTable.Filter(x => x[nameof(Team.IdeaId)].Eq(queryParam.IdeaId));

             var query = filterTeams.Map(team =>
             {
                 var teamMembers = team
                 .GetField(nameof(Team.Members))
                 .Map(member=> {
                     return member.Merge(RethinkDB.R.HashMap(nameof(TeamMember.Member),
                         userTable.Get(member.GetField(nameof(TeamMember.MemberUserId))).Pluck(nameof(User.FullName))));
                 });                

                 return team.Merge(RethinkDB.R.HashMap(nameof(Team.Members),teamMembers));
             });

            return (await query.RunCursorAsync<Team>(context.Connection)).ToList();
        }
    }
}
