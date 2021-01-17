using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// This interface need to be implemented by repositories 
    /// which wants to add new record in entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAddService<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        /// <summary>
        /// Add new record in to entity
        /// </summary>
        /// <param name="entity">Database/DBContext entity</param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);
    }
}
