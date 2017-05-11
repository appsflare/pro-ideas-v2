using System;
using System.Collections.Generic;

namespace ProIdeas.Domain.Entities
{

    public enum Status
    {
        Draft,
        Published,
        Unpublished
    }

    public class Idea : BaseEntity
    {

        public string OwnerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsFundingRequired { get; set; }

        public string FundingRequirement { get; set; }

        public string BannerImage { get; set; }

        public int Likes { get; set; }

        public int DisLikes { get; set; }

        public int Comments { get; set; }

        public IList<Page> Pages { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public string Status { get; set; }

        public User Owner { get; set; }

    }
}
