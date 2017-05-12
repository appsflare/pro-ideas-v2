using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using RethinkDb.Driver;
using ProIdeas.Domain.Entities.Model;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetIdeaCollaborationStatsQueryTemplate : BaseRethinkQueryTemplate<IdeaCollaborationStats, GetIdeaCollaborationStatsQuery>
    {
        async protected override Task<IEnumerable<IdeaCollaborationStats>> ExecuteAsync(QueryTemplateContext<GetIdeaCollaborationStatsQuery> context)
        {

            var queryParam = context.Parameter;

            var ideaTable = RethinkDB.R.Table(nameof(Idea));
            var ideaCommentTable = RethinkDB.R.Table(nameof(IdeaComment));
            var ideaLikeTable = RethinkDB.R.Table(nameof(IdeaLike));

            var ideaLikesQuery = ideaLikeTable.Filter(x => x[nameof(IdeaLike.IdeaId)].Eq(queryParam.IdeaId) && x[nameof(IdeaLike.IsLike)].Eq(true)).Count();

            var ideaDisLikesQuery = ideaLikeTable.Filter(x => x[nameof(IdeaLike.IdeaId)].Eq(queryParam.IdeaId) && x[nameof(IdeaLike.IsLike)].Eq(false)).Count();

            var ideaCommentsQuery = ideaCommentTable.Filter(x => x[nameof(IdeaLike.IdeaId)].Eq(queryParam.IdeaId)).Count();


            var getStatsTask = ideaTable.Get(queryParam.IdeaId)
                .Pluck(nameof(IdeaLike.IdeaId))
                .Merge(idea => RethinkDB.R.HashMap(nameof(IdeaCollaborationStats.Likes), ideaLikesQuery))
                .Merge(idea => RethinkDB.R.HashMap(nameof(IdeaCollaborationStats.DisLikes), ideaDisLikesQuery))
                .Merge(idea => RethinkDB.R.HashMap(nameof(IdeaCollaborationStats.Comments), ideaCommentsQuery))
                .RunAtomAsync<IdeaCollaborationStats>(context.Connection);

            return new[] { await getStatsTask };
        }
    }
}
