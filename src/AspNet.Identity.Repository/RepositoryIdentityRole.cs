using System.Runtime.Serialization;
using ProIdeas.Domain.Entities;

namespace AspNet.Identity.Repository
{
	[DataContract]
	public class RepositoryIdentityRole : BaseEntity
	{
		public RepositoryIdentityRole()
		{			
		}

		public RepositoryIdentityRole(string roleName)
			: this()
		{
			Name = roleName;
		}   		

		[DataMember]
		public string Name { get; set; }
	}
}