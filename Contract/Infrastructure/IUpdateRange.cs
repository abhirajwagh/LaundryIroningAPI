using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// This interface need to be implemented by Repositories
    /// which wants to modify record details in entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUpdateRange<TEntity> where TEntity : class
    {
        /// <summary>
        /// Update multiple entities record
        /// </summary>
        /// <param name="entities">Database/DBContext entity</param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    }
}
