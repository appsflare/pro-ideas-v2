using System;
using System.Collections.Generic;
using System.Text;

namespace ProIdeas.Domain.Entities
{
    public class Page : BaseEntity
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
