using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.Domain.Queries.Tasks;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates.Tasks
{
    public class GetTaskBoardTasksByStateIdQueryTemplate : BaseRethinkQueryTemplate<TaskItem, GetTaskBoardTasksByStateId>
    {
        async protected override Task<IEnumerable<TaskItem>> ExecuteAsync(QueryTemplateContext<GetTaskBoardTasksByStateId> context)
        {
            var r = RethinkDB.R;
            var queryParam = context.Parameter;

            var query = r.Table(nameof(TaskItem))
                .Filter(x => x.GetField(nameof(TaskItem.TaskBoardId)).Eq(queryParam.TaskBoardId));

            if (!string.IsNullOrEmpty(queryParam.StateId))
            {
                query = query.Filter(x => x.GetField(nameof(TaskItem.StateId)).Eq(queryParam.StateId));
            }            

            return (await query.RunCursorAsync<TaskItem>(context.Connection)).ToList();
        }
    }
}
