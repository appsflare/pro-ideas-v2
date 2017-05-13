using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.UI.Models.User
{
    public class UserActivityViewModel
    {
        public string OwnerId { get; set; }

        public IEnumerable<TimeLineViewModel> UserActivities { get; set; }

        public IEnumerable<TimeLineViewModel> UserContributions { get; set; }

        public static UserActivityViewModel MapFrom(IEnumerable<ActivityDto> userActivities, IEnumerable<ActivityDto> userContributions, string userId)
        {
            var datepattern = "dd-MMM-yyyy";
            return new UserActivityViewModel()
            {
                OwnerId = userId,
                UserContributions = BuildActivityStrem(userContributions, userId, datepattern),
                UserActivities = BuildActivityStrem(userActivities, userId, datepattern)
            };
        }

        private static IEnumerable<TimeLineViewModel> BuildActivityStrem(IEnumerable<ActivityDto> activityList, string userId, string datepattern)
        {
            return activityList
                                .GroupBy(x => x.CreatedAt.Date)
                                .Select(tl => new TimeLineViewModel
                                {
                                    ActivityDate = tl.Key.ToString(datepattern),
                                    ActivityTimeLine = tl.ToList().Select(a => new ActivitiesTimeLineViewModel
                                    {
                                        Activity = new ActivityDetailsViewModel
                                        {
                                            Id = a.Id,
                                            ActivityType = a.Type,
                                            Details = a.Body,
                                            Vote = a.ItemDetails.IsUpVote,
                                            IdeaId = a.IdeaId
                                        },
                                        ActivityOwner = new UserDetailsViewModel
                                        {
                                            FullName = a.ItemOwner.FullName,
                                            Id = a.ItemOwnerId,
                                            self = a.ItemOwnerId == userId
                                        },
                                        ActivityDuration = a.CreatedAt.ToString(datepattern)
                                    })
                                });
        }
    }

    public class TimeLineViewModel
    {

        public string ActivityDate { get; set; }

        public IEnumerable<ActivitiesTimeLineViewModel> ActivityTimeLine { get; set; }

    }

    public class ActivitiesTimeLineViewModel
    {
        public string ActivityDuration { get; set; }

        public ActivityDetailsViewModel Activity { get; set; }

        public UserDetailsViewModel ActivityOwner { get; set; }

    }

    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public bool self { get; set; }

    }

    public class ActivityDetailsViewModel
    {


        public string Id { get; set; }

        public string IdeaId { get; set; }

        public string Details { get; set; }

        public string ActivityType { get; set; }

        public bool Vote { get; set; }

    }

}
