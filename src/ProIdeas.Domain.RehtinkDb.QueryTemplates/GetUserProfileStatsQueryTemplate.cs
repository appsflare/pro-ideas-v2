using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Entities.Model;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetUserProfileStatsQueryTemplate : BaseRethinkQueryTemplate<UserProfileStats, GetUserProfileStatsQuery>
    {
        protected override async Task<IEnumerable<UserProfileStats>> ExecuteAsync(QueryTemplateContext<GetUserProfileStatsQuery> context)
        {

            var queryParam = context.Parameter;

            var ideaTable = RethinkDB.R.Table(nameof(Idea));
            var ideaCommentTable = RethinkDB.R.Table(nameof(IdeaComment));
            var ideaLikeTable = RethinkDB.R.Table(nameof(IdeaLike));
            var usersTable = RethinkDB.R.Table("ApplicationUser");

            var ideaLikesQuery = ideaLikeTable.Filter(x => x[nameof(IdeaLike.OwnerId)].Eq(queryParam.UserId) && x[nameof(IdeaLike.IsLike)].Eq(true)).Count();

            var ideaDisLikesQuery = ideaLikeTable.Filter(x => x[nameof(IdeaLike.OwnerId)].Eq(queryParam.UserId) && x[nameof(IdeaLike.IsLike)].Eq(false)).Count();

            var ideaCommentsQuery = ideaCommentTable.Filter(x => x[nameof(IdeaComment.OwnerId)].Eq(queryParam.UserId)).Count();

            var ideasQuery = ideaTable.Filter(x => x[nameof(Idea.OwnerId)].Eq(queryParam.UserId)).Count();


            var stats = await usersTable.Get(queryParam.UserId).Pluck("id")
                .Merge(user => RethinkDB.R.HashMap(nameof(UserProfileStats.Likes), ideaLikesQuery))
                .Merge(RethinkDB.R.HashMap(nameof(UserProfileStats.DisLikes), ideaDisLikesQuery))
                .Merge(RethinkDB.R.HashMap(nameof(UserProfileStats.Comments), ideaCommentsQuery))
                .Merge(RethinkDB.R.HashMap(nameof(UserProfileStats.Ideas), ideasQuery))
                .RunAtomAsync<UserProfileStats>(context.Connection);

            stats.UserId = queryParam.UserId;

            return new[] { stats };
        }
    }
}
