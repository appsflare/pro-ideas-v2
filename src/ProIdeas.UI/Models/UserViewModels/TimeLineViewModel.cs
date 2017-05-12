using ProIdeas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProIdeas.UI.Models.TimeLineViewModel
{
    public class TimeLineViewModel
    {
        //public DateTime ActivityDate { get; set; }

        //public IEnumerable<ActivitiesTimeLineViewModel> ActivityTimeLine { get; set; }

        //public UserDetailsViewModel OwnerDetails { get; set; }

        public static dynamic MapFrom(IEnumerable<ActivityDto> activityList)
        {
            var result = activityList
                    //.OrderBy(x => x.CreatedAt)
                    .GroupBy(x => x.CreatedAt.Date)
                    .Select(tl => tl.ToList().Select(a => new ActivitiesTimeLineViewModel {
                        Activity = new ActivityDetailsViewModel
                        {
                            Id = a.Id,
                            Activity = a.Type,
                            Details = a.ActivityDetails,
                            Vote = a.ItemDetails.IsUpVote,
                        },
                        ActivityOwner = new UserDetailsViewModel
                        {
                            FullName = a.ItemOwner.FullName,
                            Id = a.ItemOwnerId,
                            self  = a.ItemOwnerId == a.IdeaOwnerId
                        },
                        ActivityDuration = a.CreatedAt
                    }));
            return result;
        }

    }

    public class ActivitiesTimeLineViewModel
    {
        public DateTime ActivityDuration { get; set; }

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

        public string Details { get; set; }

        public string Activity { get; set; }

        public bool Vote { get; set; }

    }

}
