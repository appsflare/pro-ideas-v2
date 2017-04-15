namespace AspNet.Identity.Repository
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    //using global::MongoDB.Bson;
    //using global::MongoDB.Driver.Builders;
    //using global::MongoDB.Driver.Linq;
    using Microsoft.AspNetCore.Identity;
    using ProIdeas.Domain.Repositories;


    /// <summary>
    ///     Note: Deleting and updating do not modify the roles stored on a user document. If you desire this dynamic
    ///     capability, override the appropriate operations on RoleStore as desired for your application. For example you could
    ///     perform a document modification on the users collection before a delete or a rename.
    /// </summary>
    /// <typeparam name="TRole"></typeparam>
    public class RepositoryRoleStore<TRole> : IRoleStore<TRole>, IQueryableRoleStore<TRole>
        where TRole : RepositoryIdentityRole, new()
    {
        private readonly IRepository _repository;
        //private readonly ITableQuery<TRole> TableRoles;


        public RepositoryRoleStore(IRepository repository)
        {

            _repository = repository;

        }

        public virtual IQueryable<TRole> Roles
        {
            // TODO: Performance?!
            get
            {
                IQueryable<TRole> result = _repository.All<TRole>().AsQueryable();
                return result;
            }
        }

        public virtual void Dispose()
        {
            // no need to dispose of anything, mongodb handles connection pooling automatically
        }

        public virtual Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                _repository.Add(role);
                return IdentityResult.Success;
            }, cancellationToken);
        }

        public virtual Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _repository.Update(role);
                return IdentityResult.Success;
            }, cancellationToken); //_Context.Connection.Run(TableRoles.Insert(role, Conflict.Replace)));
        }

        public virtual Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _repository.Delete(role);
                return IdentityResult.Success;
            }, cancellationToken); // _Context.Connection.Run(TableRoles.Get(role.Id).Delete())); // Select(r => r.Id == role.Id).Delete()));
        }

        public virtual Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.Run(() => _repository.First<TRole>(i => i.Id == roleId)); // _Context.Connection.Run(TableRoles.Filter(r => r.Id == roleId)).FirstOrDefault()); // FindOneByIdAs<TRole>(ObjectId.Parse(roleId)));
        }

        public virtual Task<TRole> FindByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            //var queryByName = Query.Filter<TRole>(r,  Query<TRole>.EQ(r => r.Name, roleName);
            return Task.Run(() => _repository.First<TRole>(role => role.Name == roleName));//  _Context.Connection.Run(TableRoles.Filter<TRole>(r => r.Name == roleName)).FirstOrDefault());
        }


        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {            
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName;
            return Task.FromResult(0);
        }


    }
}