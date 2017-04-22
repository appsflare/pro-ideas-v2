using System;
using System.Threading.Tasks;

namespace ProIdeas.Authentication.Contracts
{
    public interface IUserIdentityProvider
    {
        string GetUserId();
    }
}
