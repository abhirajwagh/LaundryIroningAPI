using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
    /// <summary>
    /// This interface need to be implemented by repositories 
    /// which wants to check if any records are present in the entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAnyService<TEntity> where TEntity : class
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}
