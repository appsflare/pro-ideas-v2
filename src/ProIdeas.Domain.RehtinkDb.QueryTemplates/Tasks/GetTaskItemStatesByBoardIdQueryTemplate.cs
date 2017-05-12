using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.Domain.Queries.Tasks;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates.Tasks
{
    public class GetTaskItemStatesByBoardIdQueryTemplate : BaseRethinkQueryTemplate<TaskItemState, GetTaskItemStatesByBoardId>
    {
        async protected override Task<IEnumerable<TaskItemState>> ExecuteAsync(QueryTemplateContext<GetTaskItemStatesByBoardId> context)
        {
            var r = RethinkDB.R;
            var queryParam = context.Parameter;

            var query = r.Table(nameof(TaskItemState))
                .Filter(x => x.GetField(nameof(TaskItem.TaskBoardId)).Eq(queryParam.TaskBoardId));

            return (await query.RunCursorAsync<TaskItemState>(context.Connection)).ToList();
        }
    }
}
