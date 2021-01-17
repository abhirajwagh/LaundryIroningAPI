using LaundryIroningContract.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LaundryIroningContract.Repository
{
    public interface IBaseRepository<TEntity> : 
        IAddService<TEntity>,
        ICountService<TEntity>,
        IUpdateService<TEntity>,
        IDeleteService<TEntity>,
        IDeleteRange<TEntity>,
        IUpdateRange<TEntity>,
        IAddRange<TEntity>,
        ISelectService<TEntity>,
        ISelectFirstOrDefaultService<TEntity>,
        IAnyService<TEntity>

    where TEntity : class
    {
        Task<IList<TResult>> SelectAsyncList<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selectPredicate);
    }
}
