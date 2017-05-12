using System;

namespace ProIdeas.DTO.Tasks
{
    public class TaskItemDto : BaseDto<string>
    {
        public string Content { get; set; }

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
