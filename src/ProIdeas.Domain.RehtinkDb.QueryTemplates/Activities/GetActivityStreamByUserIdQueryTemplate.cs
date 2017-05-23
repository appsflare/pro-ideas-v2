using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries.Activities;
using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates.Activities
{
    public class GetActivityStreamByUserIdQueryTemplate : BaseRethinkQueryTemplate<Activity, GetActivityStreamByUserId>
    {
        private readonly Table _activityTable;
        private readonly Table _ideaTable;
        private readonly Table _commentTable;
        private readonly Table _userTable;
        public GetActivityStreamByUserIdQueryTemplate()
        {
            _activityTable = RethinkDB.R.Table(nameof(Activity));
            _ideaTable = RethinkDB.R.Table(nameof(Idea));
            _commentTable = RethinkDB.R.Table(nameof(IdeaComment));
            _userTable = RethinkDB.R.Table("ApplicationUser");
        }

        private Pluck GetUserFullName(GetField field)
        {
            return _userTable.Get(field).Pluck(nameof(User.FullName));
        }

        Or GetFilter(ReqlExpr activity, GetActivityStreamByUserId queryParam)
        {
            return activity.GetField(nameof(Activity.OwnerId)).Eq(queryParam.UserId)
                 .Or(activity.GetField(nameof(Activity.ItemOwnerId)).Eq(queryParam.UserId))
              .Or(activity.GetField(nameof(Activity.IdeaOwnerId)).Eq(queryParam.UserId));
        }

        async protected override Task<IEnumerable<Activity>> ExecuteAsync(QueryTemplateContext<GetActivityStreamByUserId> context)
        {
            var r = RethinkDB.R;
            var queryParam = context.Parameter;

            var query = _activityTable
                .OrderBy()
                .OptArg("index", RethinkDB.R.Desc(nameof(Activity.CreatedAt)));

            ReqlExpr condtQuery;
            if (queryParam.IncludeAllUserActivities)
            {
                condtQuery = query.Filter(x => GetFilter(x, queryParam));
            }
            else
            {
                condtQuery = query.Filter(x => x.GetField(nameof(Activity.OwnerId)).Eq(queryParam.UserId));

            }

            if (queryParam.Types.Any())
            {
                condtQuery = condtQuery.Filter(x => r.Expr(queryParam.Types).Contains(x.GetField(nameof(Activity.Type))));
            }

            var finalQuery = condtQuery.Merge(activity => r.HashMap(nameof(Activity.Owner), GetUserFullName(activity.GetField(nameof(Activity.OwnerId)))))
                .Merge(activity => r.HashMap(nameof(Activity.ItemOwner), GetUserFullName(activity.GetField(nameof(Activity.ItemOwnerId)))))
                .Merge(activity => r.HashMap(nameof(Activity.IdeaOwner), GetUserFullName(activity.GetField(nameof(Activity.IdeaOwnerId)))));

            var results = await finalQuery.RunCursorAsync<Activity>(context.Connection);

            return results.ToList();
        }
    }
}
