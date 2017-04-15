namespace AspNet.Identity.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using ProIdeas.Domain.Repositories;
    using ProIdeas.Domain.Queries;
    using System.Threading;

    public class RepositoryUserStore<TUser> : IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserRoleStore<TUser>,
        IUserLoginStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserEmailStore<TUser>,
        IUserClaimStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserPhoneNumberStore<TUser>
        //IUserTwoFactorStore<TUser, string>,
        //IUserLockoutStore<TUser, string>
        where TUser : RepositoryIdentityUser, new()
    {
        
        private readonly IRepository _repository;

        public RepositoryUserStore(IRepository repository)
        {            
            _repository = repository; ;
        }

        public virtual void Dispose()
        {

        }

        public virtual Task CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _repository.Add(user);
            }); // _Context.Connection.Run(TableUsers.Insert(user)));
        }

        public virtual Task UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            // todo should add an optimistic concurrency check
            return Task.Run(() => _repository.Update(user)); // _Context.Connection.Run(TableUsers.Insert(user, Conflict.Replace)));
        }

        public virtual Task DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Run(() => _repository.Delete<TUser>(e => e.Id == user.Id)); // _Context.Connection.Run(TableUsers.Get(user.Id).Delete()));
        }

        public virtual Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.Run(() => _repository.First<TUser>(i => i.Id == userId)); // _Context.Connection.Run(TableUsers.Get(userId)));
        }

        public virtual Task<TUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            // todo exception on duplicates? or better to enforce unique index to ensure this
            return Task.Run(() => _repository.First<TUser>(i => i.UserName == userName)); // _Context.Connection.Run(TableUsers.Filter(u => u.UserName == userName)).FirstOrDefault());
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.HasPassword());
        }

        public virtual Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            user.AddRole(roleName);
            return Task.FromResult(0);
        }

        public virtual Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            user.RemoveRole(roleName);
            return Task.FromResult(0);
        }

        public virtual Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult((IList<string>)user.Roles);
        }

        public virtual Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            user.AddLogin(login);

            return Task.FromResult(0);
        }

        public virtual Task RemoveLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            user.RemoveLogin(login);
            return Task.FromResult(0);
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult((IList<UserLoginInfo>)user.Logins);
        }

        public virtual Task<TUser> FindAsync(UserLoginInfo login, CancellationToken cancellationToken)
        {
            //return Task.Factory.StartNew(() => _Context.Connection.Run(TableUsers.Filter(u => u.LoginsWrapper.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey))).FirstOrDefault());
            //return Task.Factory.StartNew(() => TableUsers.Filter(linfo => linfo["LoginProvider"] == login.LoginProvider && linfo["ProviderKey"] == login.ProviderKey).RunCursor<TUser>(_Context.Connection).FirstOrDefault()); // GetAll().RunCursor<TUser>(_Context.Connection).Any(u => u.LoginsWrapper.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey).FirstOrDefault()));
            // Users.FirstOrDefault(u => u.Logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey)));
            return Task.Factory.StartNew(() => _repository.First<TUser>(i => i.Logins.Any(linfo => linfo.LoginProvider == login.LoginProvider && linfo.ProviderKey == login.ProviderKey))); // GetAll().RunCursor<TUser>(_Context.Connection).Any(u => u.LoginsWrapper.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey).FirstOrDefault()));
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public virtual Task<TUser> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            // todo what if a user can have multiple accounts with the same email?
            //return Task.Run(() => _Context.Connection.Run(TableUsers.Filter(u => u.Email == email)).FirstOrDefault());
            return Task.Run(() => _repository.First<TUser>(u => u.Email == email));
        }

        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult((IList<Claim>)user.Claims.Select(c => c.ToSecurityClaim()).ToList());
        }

        public virtual Task AddClaimAsync(TUser user, Claim claim, CancellationToken cancellationToken)
        {
            user.AddClaim(claim);
            return Task.FromResult(0);
        }

        public virtual Task RemoveClaimAsync(TUser user, Claim claim, CancellationToken cancellationToken)
        {
            user.RemoveClaim(claim);
            return Task.FromResult(0);
        }

        public virtual IQueryable<TUser> Users
        {
            // TODO: Performance?!
            get
            {
                //return _Context.Connection.Run(TableUsers).AsQueryable<TUser>();
                return _repository.All<TUser>().AsQueryable();
            }
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPhoneNumberAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.LockoutEndDateUtc ?? new DateTimeOffset());
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEndDateUtc = new DateTime(lockoutEnd.Ticks, DateTimeKind.Utc);
            return Task.FromResult(0);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task ResetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public virtual Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public virtual Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
             {
                 user.UserName = userName;
                 _repository.Update<TUser>(user);
             }, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.UserName = normalizedName;
                //_repository.Update<TUser>(user);
            }, cancellationToken);
        }

        Task<IdentityResult> IUserStore<TUser>.CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {

                var addedUser = _repository.Add(user);
                return IdentityResult.Success;
            }, cancellationToken);
        }

        Task<IdentityResult> IUserStore<TUser>.UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {

                var addedUser = _repository.Update(user);
                return IdentityResult.Success;
            }, cancellationToken);
        }

        Task<IdentityResult> IUserStore<TUser>.DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Delete<TUser>(i => i.Id == user.Id);
                return IdentityResult.Success;
            }, cancellationToken);
        }

        public Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                return _repository.Query(new QueryBuilder<TUser>()
                    .WithCondition(i => i.Roles.Contains(roleName))
                    .Build())
                    .ToList() as IList<TUser>;

            }, cancellationToken);
        }

        public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var info = user.Logins.FirstOrDefault(i => i.LoginProvider == loginProvider && i.ProviderKey == providerKey);
            if (info != null)
            { user.RemoveLogin(info); }

            return Task.FromResult(0);
        }

        public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => _repository.First<TUser>(i => i.Logins.Any(linfo => linfo.LoginProvider == loginProvider && linfo.ProviderKey == providerKey)));
        }

        public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.Email = normalizedEmail;
                //_repository.Update(user);
            }, cancellationToken);
        }

        public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var claim in claims)
                {
                    user.AddClaim(claim);
                }
                _repository.Update(user);

            }, cancellationToken);

        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                user.RemoveClaim(claim);
                user.AddClaim(newClaim);
                _repository.Update(user);

            }, cancellationToken);
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var claim in claims)
                {
                    user.RemoveClaim(claim);
                }
                _repository.Update(user);

            }, cancellationToken);
        }

        public Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {

                return _repository.Query(new QueryBuilder<TUser>()
                    .WithCondition(i => i.Claims.Any(j => j.Type == claim.Type && j.Value == claim.Value))
                    .Build())
                    .ToList() as IList<TUser>;

            }, cancellationToken);
        }
    }
}