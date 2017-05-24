using Microsoft.Extensions.Logging;
using ProIdeas.Domain.Entities;
using ProIdeas.Domain.Queries;
using RethinkDb.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProIdeas.Domain.RehtinkDb.QueryTemplates
{
    public class GetUserProfileByUserIdQueryTemplate : BaseRethinkQueryTemplate<UserProfile, GetUserProfileByUserIdQuery>
    {
        private readonly ILogger _logger;
        public GetUserProfileByUserIdQueryTemplate(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetUserProfileByUserIdQueryTemplate>();
        }

        protected override async Task<IEnumerable<UserProfile>> ExecuteAsync(QueryTemplateContext<GetUserProfileByUserIdQuery> context)
        {
            var queryParam = context.Parameter;


            var table = RethinkDB.R
             .Table(nameof(UserProfile));

            var query = table
             .Filter(x => x.GetField(nameof(UserProfile.OwnerId)).Eq(queryParam.UserId))
             .Limit(1)
             .Merge(profile => RethinkDB.R.HashMap(nameof(UserProfile.Owner), RethinkDB.R
            .Table("ApplicationUser")
            .Get(profile.GetField(nameof(UserProfile.OwnerId)))
            .Pluck(nameof(User.FullName))));

            
            try
            {
                return (await query.RunCursorAsync<UserProfile>(context.Connection)).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return Enumerable.Empty<UserProfile>();
        }
    }
}
