using System.Collections.Generic;

namespace ProIdeas.DTO
{
    public class UserDto : BaseDto<string>
    {
        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual string FullName { get; set; }
    }
}
