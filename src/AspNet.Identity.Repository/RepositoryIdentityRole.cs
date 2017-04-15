using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using ProIdeas.Domain.Entities;

namespace AspNet.Identity.Repository
{
	[DataContract]
	public class RepositoryIdentityRole : BaseEntity<string>
	{
		public RepositoryIdentityRole()
		{
			Id = Guid.NewGuid().ToString();
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