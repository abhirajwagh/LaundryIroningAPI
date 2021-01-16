using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    public interface IDeleteRange<TEntity> where TEntity : class
    {
        /// <summary>
        /// Delete range in to entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
