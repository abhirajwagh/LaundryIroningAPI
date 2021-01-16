using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// Implement this interface to get data using linq expression
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISelectService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get filterd data as per linq expression
        /// </summary>
        /// <param name="predicate">linq expression</param>
        /// <returns></returns>
        Task<IList<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate=null);
    }
}
