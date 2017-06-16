using System;
using System.Linq;
using System.Linq.Expressions;
using ProIdeas.Domain.Entities;

namespace ProIdeas.Domain.Queries
{
    public class FilterIdeaQuery
    {

        public FilterIdeaQuery()
        {
            Exclude(i => i.Pages, i => i.FundingRequirement);
        }

        public string Keyword { get; set; }

        public string Status { get; set; }

        public string OrderBy { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string OwnerId { get; set; }

        public string[] PropertiesToExclude { get; private set; }

        public void Exclude(params Expression<Func<Idea, object>>[] propertSelectors)
        {
            PropertiesToExclude = propertSelectors
                .Select(i => i.Body as MemberExpression)
                .Where(i => i != null).Select(i => i.Member.Name)
                .ToArray();
        }
    }
}
