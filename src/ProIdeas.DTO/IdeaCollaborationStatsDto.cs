using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.DTO
{
    public class IdeaCollaborationStatsDto
    {
        public string IdeaId { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public int Comments { get; set; }
    }
}
