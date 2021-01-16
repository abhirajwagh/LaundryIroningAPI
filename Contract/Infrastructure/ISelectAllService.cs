using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaundryIroningContract.Infrastructure
{
   public interface ISelectAllService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all records from database
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> SelectAllAsync();
    }
}
