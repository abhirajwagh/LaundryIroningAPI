using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{

    /// <summary>
    /// Implement this interface to get count using linq expression
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICountService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get filterd count as per linq expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
