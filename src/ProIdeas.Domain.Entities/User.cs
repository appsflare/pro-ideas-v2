namespace ProIdeas.Domain.Entities
{

    public class User : BaseEntity
    {
        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public virtual string FullName { get; set; }
    }
}
