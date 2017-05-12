using System;

namespace ProIdeas.Domain.Entities.Tasks
{
    public class TaskItemState : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TaskBoardId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

    }
}
