using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.Domain.Queries.Activities
{
    public class GetActivityStreamByUserId
    {

        public GetActivityStreamByUserId()
        {
            Types = Enumerable.Empty<string>();
        }

        public string UserId { get; set; }

        public IEnumerable<string> Types { get; set; }
    }
}
