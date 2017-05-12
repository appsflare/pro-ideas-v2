using System;

namespace ProIdeas.Domain.Entities.Tasks
{
    public class TaskItemType : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TaskBoardId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
