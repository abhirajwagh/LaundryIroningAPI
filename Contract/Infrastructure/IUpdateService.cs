using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// This interface need to be implemented by Repositories
    /// which wants to modify record details in entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUpdateService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Update record in to entity
        /// </summary>
        /// <param name="entity">Database/DBContext entity</param>
        /// <returns></returns>
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}
