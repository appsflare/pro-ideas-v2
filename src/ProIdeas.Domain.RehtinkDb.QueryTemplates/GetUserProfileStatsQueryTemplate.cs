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
        async protected override Task<IEnumerable<UserProfileStats>> ExecuteAsync(QueryTemplateContext<GetUserProfileStatsQuery> context)
        {

            var queryParam = context.Parameter;

            var ideaTable = RethinkDB.R.Table(nameof(Idea));
            var ideaCommentTable = RethinkDB.R.Table(nameof(IdeaComment));
            var ideaLikeTable = RethinkDB.R.Table(nameof(IdeaLike));
            var usersTable = RethinkDB.R.Table("ApplicationUser");

            var ideaLikesQuery = ideaLikeTable.Filter(x => x[nameof(IdeaLike.OwnerId)].Eq(queryParam.UserId) && x[nameof(IdeaLike.IsLike)].Eq(true)).Count();

            var ideaDisLikesQuery = ideaLikeTable.Filter(x => x[nameof(IdeaLike.OwnerId)].Eq(queryParam.UserId) && x[nameof(IdeaLike.IsLike)].Eq(false)).Count();

            var ideaCommentsQuery = ideaCommentTable.Filter(x => x[nameof(IdeaLike.OwnerId)].Eq(queryParam.UserId)).Count();

            var ideasQuery = ideaCommentTable.Filter(x => x[nameof(IdeaLike.OwnerId)].Eq(queryParam.UserId)).Count();


            var stats = await ideaLikesQuery
                .Map(likesCount => RethinkDB.R.HashMap(nameof(UserProfileStats.Likes), likesCount))
                .Merge(RethinkDB.R.HashMap(nameof(UserProfileStats.DisLikes), ideaDisLikesQuery))
                .Merge(RethinkDB.R.HashMap(nameof(UserProfileStats.Comments), ideaCommentsQuery))
                .Merge(RethinkDB.R.HashMap(nameof(UserProfileStats.Ideas), ideasQuery))
                .RunAtomAsync<UserProfileStats>(context.Connection);

            stats.UserId = queryParam.UserId;

            return new[] { stats };
        }
    }
}
