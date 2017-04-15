using System.Collections.Generic;

namespace ProIdeas.DTO
{

    public class UserEmailDto
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class UserDto : BaseDto<string>
    {
        public string UserName { get; set; }

        public string PrimaryEmail { get; set; }

        public IList<UserEmailDto> Emails { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
