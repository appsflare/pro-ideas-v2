﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProIdeas.Domain.Entities;

namespace AspNet.Identity.Repository
{
    [DataContract]
    public class RepositoryIdentityUser : User
    {
        public RepositoryIdentityUser()
        {
            Roles = new List<string>();
            LoginsWrapper = new List<UserLoginInfoWrapper>();
            Claims = new List<IdentityUserClaim>();
        }

        [DataMember]
        public override string UserName { get; set; }

        [DataMember]
        public override string Email { get; set; }

        [DataMember]
        public override string FullName { get; set; }



        [DataMember]
        public virtual string SecurityStamp { get; set; }
        

        [DataMember]
        public virtual bool EmailConfirmed { get; set; }

        [DataMember]
        public virtual string PhoneNumber { get; set; }

        [DataMember]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [DataMember]
        public virtual bool TwoFactorEnabled { get; set; }

        [DataMember]
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        [DataMember]
        public virtual bool LockoutEnabled { get; set; }

        [DataMember]
        public virtual int AccessFailedCount { get; set; }

        [DataMember]
        public List<string> Roles { get; set; }

        public virtual void AddRole(string role)
        {
            Roles.Add(role);
        }

        public virtual void RemoveRole(string role)
        {
            Roles.Remove(role);
        }

        [DataMember]
        public virtual string PasswordHash { get; set; }

        public List<UserLoginInfo> Logins
        {
            get
            {
                return LoginsWrapper.Select(l => l.UserLoginInfo).ToList();
            }
        }

        /// <summary>
        /// This wraps the Logins property because UserLoginInfo is sealed and cannot be changed. But we need to decorate that class with DataContract and DataMember.
        /// TODO: Possibly there is a more elegant solution that could be used for this.
        /// TODO: Also this should not be visible outside this lib (not public). But this collides with the need for auto serialization/deserialization.
        /// </summary>
        [DataMember(Name = "Logins")]
        public List<UserLoginInfoWrapper> LoginsWrapper { get; set; }

        public virtual void AddLogin(UserLoginInfo login)
        {
            LoginsWrapper.Add(new UserLoginInfoWrapper(login.LoginProvider, login.ProviderKey));
            //Logins.Add(login);
        }

        public virtual void RemoveLogin(UserLoginInfo login)
        {
            var loginsToRemove = LoginsWrapper
                .Where(l => l.LoginProvider == login.LoginProvider)
                .Where(l => l.ProviderKey == login.ProviderKey);

            LoginsWrapper = LoginsWrapper.Except(loginsToRemove).ToList();
        }

        public virtual bool HasPassword()
        {
            return !string.IsNullOrEmpty(PasswordHash);
        }

        [DataMember]
        public List<IdentityUserClaim> Claims { get; set; }

        public virtual void AddClaim(Claim claim)
        {
            Claims.Add(new IdentityUserClaim(claim));
        }

        public virtual void RemoveClaim(Claim claim)
        {
            var claimsToRemove = Claims
                .Where(c => c.Type == claim.Type)
                .Where(c => c.Value == claim.Value);

            Claims = Claims.Except(claimsToRemove).ToList();
        }
    }
}