using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.DTO
{
    public class PageDto : BaseDto<string>
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
