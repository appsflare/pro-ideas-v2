namespace ProIdeas.Domain.Repositories
{
    public interface IQueryTemplateFinder
    {
        IQueryTemplate<TEntity, TQueryParam> Find<TEntity, TQueryParam>()
            where TQueryParam : class
            where TEntity : class, new();
    }
}
