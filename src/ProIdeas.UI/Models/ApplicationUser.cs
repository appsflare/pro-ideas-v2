using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNet.Identity.Repository;

namespace ProIdeas.UI.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : RepositoryIdentityUser
    {
    }
}
