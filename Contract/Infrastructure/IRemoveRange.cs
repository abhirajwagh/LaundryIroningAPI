using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    public interface IRemoveRange<TEntity> where TEntity : class
    {

        /// <summary>
        /// Delete Multiple records.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveRangeAsync(List<TEntity> entity);

    }
}
