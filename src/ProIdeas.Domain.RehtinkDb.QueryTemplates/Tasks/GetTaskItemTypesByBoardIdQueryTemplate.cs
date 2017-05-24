using ProIdeas.Domain.Entities.Tasks;
using ProIdeas.Domain.Queries.Tasks;
using RethinkDb.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates.Tasks
{
    public class GetTaskItemTypesByBoardIdQueryTemplate : BaseRethinkQueryTemplate<TaskItemType, GetTaskItemTypesByBoardId>
    {
        protected override async Task<IEnumerable<TaskItemType>> ExecuteAsync(QueryTemplateContext<GetTaskItemTypesByBoardId> context)
        {
            var r = RethinkDB.R;
            var queryParam = context.Parameter;

            var query = r.Table(nameof(TaskItemType))
                .Filter(x => x.GetField(nameof(TaskItem.TaskBoardId)).Eq(queryParam.TaskBoardId));

            return (await query.RunCursorAsync<TaskItemType>(context.Connection)).ToList();
        }
    }
}
