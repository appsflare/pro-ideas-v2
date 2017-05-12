﻿using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries.Activities;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates.Activities
{
    public class GetActivityStreamByUserIdQueryTemplate : BaseRethinkQueryTemplate<Activity, GetActivityStreamByUserId>
    {
        async protected override Task<IEnumerable<Activity>> ExecuteAsync(QueryTemplateContext<GetActivityStreamByUserId> context)
        {
            var r = RethinkDB.R;
            var queryParam = context.Parameter;

            var activityTable = RethinkDB.R.Table(nameof(Activity));

            var ideaTable = RethinkDB.R.Table(nameof(Idea));

            var commentTable = RethinkDB.R.Table(nameof(Idea));

            var userTable = RethinkDB.R.Table("ApplicationUser");

            var query = activityTable.Filter(x => x.GetField(nameof(Activity.OwnerId)).Eq(queryParam.UserId));

            if (queryParam.Types.Any())
            {
                query = query.Filter(x => r.Expr(queryParam.Types).Contains(x.GetField(nameof(Activity.Type))));
            }

            var finalQuery = query.Merge(activity => r.HashMap(nameof(Activity.Owner), userTable.Get(activity.GetField(nameof(Activity.OwnerId))).Pluck(nameof(User.FullName))))
                .Merge(activity => r.HashMap(nameof(Activity.ItemOwner), userTable.Get(activity.GetField(nameof(Activity.ItemOwnerId))).Pluck(nameof(User.FullName))))
                .Merge(activity => r.HashMap(nameof(Activity.IdeaOwner), userTable.Get(activity.GetField(nameof(Activity.IdeaOwnerId))).Pluck(nameof(User.FullName))));

            return (await finalQuery.RunCursorAsync<Activity>(context.Connection)).ToList();
        }
    }
}
