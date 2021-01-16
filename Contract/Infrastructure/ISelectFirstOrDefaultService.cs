using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    public interface ISelectFirstOrDefaultService<TEntity> where TEntity : class
    {
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Get filterd data as per linq expression
        /// </summary>
        /// <param name="predicate">linq expression</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);

    }
}
