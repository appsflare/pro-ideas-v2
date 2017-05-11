using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetUserProfileByUserIdQueryTemplate : BaseRethinkQueryTemplate<UserProfile, GetUserProfileByUserIdQueryParameter>
    {
        async protected override Task<IEnumerable<UserProfile>> ExecuteAsync(QueryTemplateContext<GetUserProfileByUserIdQueryParameter> context)
        {
            var queryParam = context.Parameter;


            var table = RethinkDB.R
             .Table(typeof(UserProfile).Name);


            var query = table
             .Get(queryParam.UserId)
             .Merge(idea => RethinkDB.R.HashMap(nameof(UserProfile.OwnerId), RethinkDB.R
            .Table("ApplicationUser")
            .Get(idea.GetField(nameof(Idea.OwnerId)))
            .Pluck(nameof(User.FullName))));

            return new[] { await query.RunAtomAsync<UserProfile>(context.Connection) };
        }
    }
}
