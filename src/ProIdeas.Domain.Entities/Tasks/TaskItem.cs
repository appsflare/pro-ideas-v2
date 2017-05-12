using System;

namespace ProIdeas.Domain.Entities.Tasks
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public string LockedBy { get; set; }

        public string StateId { get; set; }

        public string TaskTypeId { get; set; }

        public string TaskBoardId { get; set; }
    }
}
