namespace ProIdeas.Domain.Queries
{
    public class FilterIdeaQuery
    {
        public string Keyword { get; set; }

        public string Status { get; set; }

        public string OrderBy { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public string OwnerId { get; set; }
    }
}
